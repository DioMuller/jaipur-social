using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JaipurSocial.Core
{
    public enum Card
    {
        Silk,
        Ruby,
        Leather,
        Gold,
        Camel,
        Silver,
        Spices
    }

    public static class CardExtensions
    {
        #region Methods
        public static string GetCardImage(this Card card)
        {
            return "Images/card-" + card.GetName();
        }

        public static string GetResourceImage(this Card card)
        {
            return "Images/resource-" + card.GetName();
        }
        
        public static string GetName(this Card card)
        {
            return Enum.GetName(typeof(Card), card).ToLower();
        }
        #endregion Methods
    }
}
