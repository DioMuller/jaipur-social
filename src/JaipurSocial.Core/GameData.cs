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
        public bool EnemyTurn { get; private set; }

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
                if (!IsGameFinished)
                    return null;
                return new[] { ChallengerData, EnemyData }.OrderByDescending(d => d.Points).FirstOrDefault().User;
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

        public bool IsCurrentTurn(User player)
        {
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
        }

        public void TradeCards(User player, List<Card> hand, List<Card> table)
        {
            var data = GetPlayerData(player, true);

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
                if (Resources.Remove(card))
                    data.Points += points;
            }

            // TODO: Ammount count Bonus.
        }
        #endregion

        #region DataBase
        public Game ToGame()
        {
            Func<IEnumerable<Card>, string> serializeCards =
                list => list.Aggregate(new StringBuilder(), (sb, c) => sb.Append((int)c)).ToString();

            Func<Dictionary<Card, int>, string> serializeResources =
                list => list.Aggregate(new StringBuilder(), (sb, kv) => sb.Append((int)kv.Key).Append(kv.Value)).ToString();

            return new Game
            {
                EnemyTurn = EnemyTurn,

                OnTable = serializeCards(OnTable),
                OnDeck = serializeCards(OnDeck),
                Resources = serializeResources(Resources),

                ChallengerId = ChallengerData.User.Id,
                ChallengerHand = serializeCards(ChallengerData.Hand),
                ChallengerCamels = ChallengerData.Camels,
                ChallengerPoints = ChallengerData.Points,

                EnemyId = EnemyData.User.Id,
                EnemyHand = serializeCards(EnemyData.Hand),
                EnemyCamels = EnemyData.Camels,
                EnemyPoints = EnemyData.Points,
            };
        }

        public GameData(Game game, User challenger, User enemy)
        {
            Func<string, IEnumerable<Card>> deserializeCards =
                str => str.Select(c => (Card)int.Parse(c.ToString()));

            Func<string, Dictionary<Card, int>> deserializeResources =
                str =>
                {
                    var serializedCards = new Dictionary<Card, int>();
                    for (int i = 0; i < str.Length; i += 2)
                        serializedCards.Add((Card)int.Parse(str[i].ToString()), int.Parse(str[i + 1].ToString()));
                    return serializedCards;
                };

            EnemyTurn = game.EnemyTurn;

            OnDeck = new Stack<Card>(deserializeCards(game.OnDeck));
            OnTable = deserializeCards(game.OnTable).ToList();
            Resources = deserializeResources(game.Resources);


            ChallengerData = new PlayerData(challenger) { Points = game.ChallengerPoints };

            foreach (var card in deserializeCards(game.ChallengerHand))
                ChallengerData.GiveCard(card);
            foreach (var card in Enumerable.Repeat(Card.Camel, game.ChallengerCamels))
                ChallengerData.GiveCard(card);


            EnemyData = new PlayerData(enemy) { Points = game.EnemyPoints };

            foreach (var card in deserializeCards(game.EnemyHand))
                EnemyData.GiveCard(card);
            foreach (var card in Enumerable.Repeat(Card.Camel, game.EnemyCamels))
                EnemyData.GiveCard(card);
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
