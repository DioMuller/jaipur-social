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
        public PlayerData Player1 { get; private set; }
        public PlayerData Player2 { get; private set; }
        public List<Card> OnTable { get; private set; }
        public Dictionary<Card, int> Resources { get; private set; }
        public Stack<Card> OnDeck { get; private set; }

        public GameData(User challenger, User enemy)
        {
            CreateNewGame(challenger, enemy);
        }

        public void CreateNewGame(User challenger, User enemy)
        {
            Player1 = new PlayerData(challenger);
            Player2 = new PlayerData(enemy);
            OnTable = new List<Card>(5);
            OnDeck = new Stack<Card>();

            #region Initialize Deck
            for( int i = 0; i < 11; i++ )
            {
                if( i < 6 ) //6 Cards on Game
                {
                    OnDeck.Push(Card.Ruby);
                    OnDeck.Push(Card.Gold);
                    OnDeck.Push(Card.Silver);
                }
                if( i < 8 ) //8 Cards on Game
                {
                    OnDeck.Push(Card.Silk);
                    OnDeck.Push(Card.Spices);
                }
                if( i < 10 ) //10 Cards on Game
                {
                    OnDeck.Push(Card.Leather);
                }

                //11 Cards on Game
                OnDeck.Push(Card.Camel);
            }

            ShuffleCards();
            #endregion Initialize Deck

            Resources = new Dictionary<Card,int>();
            Resources[Card.Gold] = 5;
            Resources[Card.Ruby] = 5;
            Resources[Card.Silver] = 5;
            Resources[Card.Silk] = 7;
            Resources[Card.Spices] = 7;
            Resources[Card.Leather] = 9;

            for( int i = 0; i < 5; i++ )
            {
                Player1.GiveCard(OnDeck.Pop());
                Player2.GiveCard(OnDeck.Pop());
            }
        }

        public void DrawCards()
        {
            while (OnTable.Count < 5)
            {
                OnTable.Add(OnDeck.Pop());
            }
        }

        public void ShuffleCards()
        {
            Random rng = new Random(DateTime.Now.Millisecond);
            var ordered = OnDeck.OrderBy( (c) => rng.Next() );

            OnDeck.Clear();

            foreach( Card card in ordered )
            {
                OnDeck.Push(card);
            }
        }
    }
}
