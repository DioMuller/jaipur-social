using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JaipurSocial.Data;

namespace JaipurSocial.Core
{
    class PlayerData
    {
        public User User { get; private set; }
        public List<Card> Hand { get; private set; }
        public int Camels { get; internal set; }

        public PlayerData(User user)
        {
            User = user;
            Hand = new List<Card>(7);
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
                Hand.Add(card);
            }
        }
    }
}
