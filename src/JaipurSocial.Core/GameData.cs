using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JaipurSocial.Core
{
    class GameData
    {
        List<Card> Player1Hand { get; private set; }
        int Player1Camels { get; private set; }
        List<Card> Player2Hand { get; private set; }
        int Player2Camels { get; private set; }
        List<Card> OnTable { get; private set; }
        Dictionary<Card, int> Resources { get; private set; }
        Stack<Card> OnDeck { get; private set; }

        public void CreateGame()
        {
            Player1Hand = new List<Card>(7);
            Player2Hand = new List<Card>(7);
            OnTable = new List<Card>(5);

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
        }

        public void DrawCards()
        {
            while( OnTable.Count < 5 )
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
