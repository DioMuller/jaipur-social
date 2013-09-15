using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JaipurSocial.Data;

namespace JaipurSocial.Core
{
    public class PlayerData
    {
        #region Attributes
        List<Card> _hand;
        #endregion

        public User User { get; private set; }
        public IReadOnlyList<Card> Hand { get { return _hand; } }
        public int Camels { get; private set; }
        public int Points { get; internal set; }

        public PlayerData(User user)
        {
            User = user;
            _hand = new List<Card>(7);
            Camels = 0;
        }

        public void GiveCard(Card card)
        {
            if( card == Card.Camel )
            {
                Camels++;
            }
            else
            {
                _hand.Add(card);
            }
        }

        public void TakeCard(Card card)
        {
            if ((card == Card.Camel && Camels <= 0) || (card != Card.Camel && !Hand.Contains(card)))
                throw new InvalidOperationException("Player does not have the specified card");

            if (card == Card.Camel)
            {
                Camels--;
            }
            else
            {
                _hand.Remove(card);
            }
        }
    }
}
