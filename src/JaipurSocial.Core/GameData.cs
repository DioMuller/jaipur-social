using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JaipurSocial.Data;

namespace JaipurSocial.Core
{
    public class GameData
    {
        #region Properties
        public int Id { get; private set; }
        bool EnemyTurn { get; set; }
        bool ChallengerDeleted { get; set; }
        bool EnemyDeleted { get; set; }

        public PlayerData ChallengerData { get; private set; }
        public PlayerData EnemyData { get; private set; }

        public List<Card> OnTable { get; private set; }
        public Stack<Card> OnDeck { get; private set; }
        public Dictionary<Card, int> Resources { get; private set; }

        public bool IsGameFinished
        {
            get { return !OnDeck.Any() || Resources.Count(r => r.Value <= 0) >= 3; }
        }

        public User Winner
        {
            get
            {
                if (IsGameFinished)
                    return new[] { ChallengerData, EnemyData }.OrderByDescending(d => d.Points).FirstOrDefault().User;

                if (EnemyDeleted)
                    return ChallengerData.User;
                if (ChallengerDeleted)
                    return EnemyData.User;

                return null;
            }
        }
        #endregion

        #region Constructors
        private GameData(User challenger, User enemy)
        {
            ChallengerData = new PlayerData(challenger);
            EnemyData = new PlayerData(enemy);
            OnTable = new List<Card>(5);
            Resources = new Dictionary<Card, int>
            {
                {Card.Gold, 5},
                {Card.Ruby, 5},
                {Card.Silver, 5},
                {Card.Silk, 7},
                {Card.Spices, 7},
                {Card.Leather, 9},
            };

            var cards = Enumerable.Repeat(Card.Ruby, 6).Concat(
                        Enumerable.Repeat(Card.Gold, 6)).Concat(
                        Enumerable.Repeat(Card.Silver, 6)).Concat(
                        Enumerable.Repeat(Card.Silk, 8)).Concat(
                        Enumerable.Repeat(Card.Spices, 8)).Concat(
                        Enumerable.Repeat(Card.Leather, 10)).Concat(
                        Enumerable.Repeat(Card.Camel, 11));

            OnDeck = new Stack<Card>(cards);
        }
        #endregion

        public static GameData CreateNewGame(User challenger, User enemy)
        {
            var data = new GameData(challenger, enemy);

            data.ShuffleCards();

            for (int i = 0; i < 5; i++)
            {
                data.ChallengerData.GiveCard(data.OnDeck.Pop());
                data.EnemyData.GiveCard(data.OnDeck.Pop());
            }

            data.DrawCards();

            return data;
        }

        public PlayerData GetEnemyData(User currentUser)
        {
            if (currentUser.Id == ChallengerData.User.Id)
                return EnemyData;
            return ChallengerData;
        }

        public bool IsCurrentTurn(User player)
        {
            if (EnemyDeleted || ChallengerDeleted)
                return false;

            if (IsGameFinished)
                return false;

            if (EnemyTurn)
                return player.Login.ToLower() == EnemyData.User.Login.ToLower();
            return player.Login.ToLower() == ChallengerData.User.Login.ToLower();
        }

        public PlayerData GetPlayerData(User player, bool validateTurn = false)
        {
            if (validateTurn && !IsCurrentTurn(player))
                throw new InvalidOperationException("Game is not on player's turn");

            return new[] { ChallengerData, EnemyData }
                .First(d => d.User.Login.ToLower() == player.Login.ToLower());
        }

        #region Game Operations
        public void TakeCard(User player, Card selectedCard)
        {
            var data = GetPlayerData(player, true);

            if (selectedCard == Card.Camel)
                throw new InvalidOperationException("Use TakeCamels instead");

            if (data.Hand.Count() >= 7)
                throw new InvalidOperationException("Current player already have 7 cards at hand");

            if (!OnTable.Contains(selectedCard))
                throw new InvalidOperationException("Selected card is not on the table");

            OnTable.Remove(selectedCard);
            data.GiveCard(selectedCard);
            DrawCards();
            EnemyTurn = !EnemyTurn;
        }

        public void TradeCards(User player, List<Card> hand, List<Card> table)
        {
            var data = GetPlayerData(player, true);

            if (hand.Count < table.Count)
                hand.AddRange(Enumerable.Repeat(Card.Camel, Math.Min(table.Count - hand.Count, data.Camels)));

            if (hand.Count() != table.Count())
                throw new InvalidOperationException("Cards must be traded equally");

            if (!hand.Any())
                throw new InvalidOperationException("No cards to trade");

            Dictionary<Card, int> handCardsToTrade = hand.GroupBy(c => c).ToDictionary(k => k.Key, k => k.Count());
            Dictionary<Card, int> tableCardsToTrade = table.GroupBy(c => c).ToDictionary(k => k.Key, k => k.Count());

            if (handCardsToTrade.Any(c => (c.Key == Card.Camel && data.Camels < c.Value)
                || (c.Key != Card.Camel && data.Hand.Count(h => h == c.Key) < c.Value)))
                throw new InvalidOperationException("Invalid hand cards");

            if (tableCardsToTrade.Any(c => OnTable.Count(t => t == c.Key) < c.Value))
                throw new InvalidOperationException("Invalid table cards");

            foreach (var card in hand)
            {
                data.TakeCard(card);
                OnTable.Add(card);
            }

            foreach (var card in table)
            {
                OnTable.Remove(card);
                data.GiveCard(card);
            }

            EnemyTurn = !EnemyTurn;
        }

        public void TakeCamels(User player)
        {
            var data = GetPlayerData(player, true);
            bool tookCamels = false;

            foreach (var card in OnTable.Where(c => c == Card.Camel).ToList())
            {
                OnTable.Remove(card);
                data.GiveCard(card);
                tookCamels = true;
            }

            if (!tookCamels)
                throw new InvalidOperationException("No camels on the table");

            DrawCards();

            EnemyTurn = !EnemyTurn;
        }

        public void SellCards(User player, Card card, int ammount)
        {
            SellCards(player, Enumerable.Repeat(card, ammount).ToList());
        }

        public void SellCards(User player, List<Card> cards)
        {
            var data = GetPlayerData(player, true);

            if (!cards.Any())
                throw new InvalidOperationException("No cards selected to sell");

            var type = cards.First();
            if (cards.Skip(1).Any(c => c != type))
                throw new InvalidOperationException("All cards must be of the same type");

            if (type == Card.Camel)
                throw new Exception("Camels cannot be sold");

            foreach (var card in cards)
            {
                var points = GetNextValue(card);
                data.TakeCard(card);
                if (Resources.ContainsKey(card) && Resources[card] > 0)
                {
                    Resources[card]--;
                    data.Points += points;
                }
            }

            EnemyTurn = !EnemyTurn;

            // TODO: Ammount count Bonus.
        }
        #endregion

        #region DB Operations
        public void Save()
        {
            using (var db = new JaipurEntities())
            {
                Game game = db.Game.First( (g) => g.Id == this.Id);
                
                game.EnemyTurn = EnemyTurn;

                game.OnTable = SerializeCards(OnTable);
                game.OnDeck = SerializeCards(OnDeck);
                game.Resources = SerializeResources(Resources);

                game.ChallengerId = ChallengerData.User.Id;
                game.ChallengerHand = SerializeCards(ChallengerData.Hand);
                game.ChallengerCamels = ChallengerData.Camels;
                game.ChallengerPoints = ChallengerData.Points;

                game.EnemyId = EnemyData.User.Id;
                game.EnemyHand = SerializeCards(EnemyData.Hand);
                game.EnemyCamels = EnemyData.Camels;
                game.EnemyPoints = EnemyData.Points;

                game.ChallengerDeleted = ChallengerDeleted;
                game.EnemyDeleted = EnemyDeleted;

                db.SaveChanges();
            }
        }

        public void MarkAsDeleted(User player)
        {
            using (var db = new JaipurEntities())
            {
                Game game = db.Game.FirstOrDefault((g) => g.Id == this.Id);
                if (game == null)
                    return;

                if (player.Id == ChallengerData.User.Id)
                    game.ChallengerDeleted = ChallengerDeleted = true;
                else if (player.Id == EnemyData.User.Id)
                    game.EnemyDeleted = EnemyDeleted = true;

                if (EnemyDeleted && ChallengerDeleted)
                    db.Game.Remove(game);

                db.SaveChanges();
            }
        }

        public bool IsDeleted(User player)
        {
            if (player.Id == ChallengerData.User.Id)
                return ChallengerDeleted;
            else if (player.Id == EnemyData.User.Id)
                return EnemyDeleted;

            return true;
        }
        #endregion DB Operations

        #region DataBase
        public Game ToGame()
        {
            return new Game
            {
                EnemyTurn = EnemyTurn,

                OnTable = SerializeCards(OnTable),
                OnDeck = SerializeCards(OnDeck),
                Resources = SerializeResources(Resources),

                ChallengerId = ChallengerData.User.Id,
                ChallengerHand = SerializeCards(ChallengerData.Hand),
                ChallengerCamels = ChallengerData.Camels,
                ChallengerPoints = ChallengerData.Points,

                EnemyId = EnemyData.User.Id,
                EnemyHand = SerializeCards(EnemyData.Hand),
                EnemyCamels = EnemyData.Camels,
                EnemyPoints = EnemyData.Points,

                ChallengerDeleted = ChallengerDeleted,
                EnemyDeleted = EnemyDeleted,
            };
        }

        public GameData(Game game)
        {
            Id = game.Id;
            EnemyTurn = game.EnemyTurn;

            OnDeck = new Stack<Card>(DeserializeCards(game.OnDeck));
            OnTable = DeserializeCards(game.OnTable).ToList();
            Resources = DeserializeResources(game.Resources);

            using (var db = new JaipurEntities())
            {
                var challenger = db.User.FirstOrDefault(u => u.Id == game.ChallengerId);
                var enemy = db.User.FirstOrDefault(u => u.Id == game.EnemyId);

                ChallengerData = new PlayerData(challenger) { Points = game.ChallengerPoints };
                EnemyData = new PlayerData(enemy) { Points = game.EnemyPoints };
            }

            foreach (var card in DeserializeCards(game.ChallengerHand))
                ChallengerData.GiveCard(card);
            foreach (var card in Enumerable.Repeat(Card.Camel, game.ChallengerCamels))
                ChallengerData.GiveCard(card);

            foreach (var card in DeserializeCards(game.EnemyHand))
                EnemyData.GiveCard(card);
            foreach (var card in Enumerable.Repeat(Card.Camel, game.EnemyCamels))
                EnemyData.GiveCard(card);

            EnemyDeleted = game.EnemyDeleted;
            ChallengerDeleted = game.ChallengerDeleted;
        }

        static string SerializeCards(IEnumerable<Card> cards)
        {
            return cards.Aggregate(new StringBuilder(), (sb, c) => sb.Append((int)c)).ToString();
        }

        static string SerializeResources(Dictionary<Card, int> resources)
        {
            return resources.Aggregate(new StringBuilder(), (sb, kv) => sb.Append((int)kv.Key).Append(kv.Value)).ToString();
        }

        static IEnumerable<Card> DeserializeCards(string str)
        {
            return str.Select(c => (Card)int.Parse(c.ToString()));
        }

        static Dictionary<Card, int> DeserializeResources(string str)
        {
            var serializedCards = new Dictionary<Card, int>();
            for (int i = 0; i < str.Length; i += 2)
                serializedCards.Add((Card)int.Parse(str[i].ToString()), int.Parse(str[i + 1].ToString()));
            return serializedCards;
        }
        #endregion

        #region Private
        private void DrawCards()
        {
            while (OnTable.Count < 5)
            {
                OnTable.Add(OnDeck.Pop());
            }
        }

        private void ShuffleCards()
        {
            Random rng = new Random(Environment.TickCount);
            var ordered = OnDeck.OrderBy(c => rng.Next()).ToList();

            OnDeck.Clear();

            foreach (Card card in ordered)
            {
                OnDeck.Push(card);
            }
        }

        private int GetNextValue(Card card)
        {
            switch (card)
            {
                case Card.Gold:
                    if (Resources[card] >= 4)
                        return 6;
                    if (Resources[card] >= 1)
                        return 5;
                    break;

                case Card.Ruby:
                    if (Resources[card] >= 4)
                        return 7;
                    if (Resources[card] >= 1)
                        return 5;
                    break;

                case Card.Silver:
                    if (Resources[card] >= 1)
                        return 5;
                    break;

                case Card.Silk:
                    if (Resources[card] >= 7)
                        return 5;
                    if (Resources[card] >= 5)
                        return 3;
                    if (Resources[card] >= 3)
                        return 2;
                    if (Resources[card] >= 1)
                        return 1;
                    break;

                case Card.Spices:
                    if (Resources[card] >= 7)
                        return 5;
                    if (Resources[card] >= 5)
                        return 3;
                    if (Resources[card] >= 3)
                        return 2;
                    if (Resources[card] >= 1)
                        return 1;
                    break;

                case Card.Leather:
                    if (Resources[card] >= 9)
                        return 4;
                    if (Resources[card] >= 8)
                        return 3;
                    if (Resources[card] >= 7)
                        return 2;
                    if (Resources[card] >= 1)
                        return 1;
                    break;
            }

            throw new InvalidOperationException("Invalid card");
        }
        #endregion
    }
}
