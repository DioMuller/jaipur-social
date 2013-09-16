using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JaipurSocial.Core;

/// <summary>
/// Summary description for CardContainer
/// </summary>
public class CardContainer
{
    public Card Type { get; set; }
    public bool Visible { get; set; }

    public string Image
    {
        get
        {
            if (Visible) return Type.GetCardImage() + ".png";
            else return "Images/card-hidden.png";
        }
    }

    public string RelativeImage
    {
        get { return "../" + Image; }
    }

	public CardContainer()
	{

	}

    #region Methods
    public static List<CardContainer> GetContainer(IReadOnlyList<Card> cards, bool visible)
    {
        #region Cards
        List<CardContainer> container = new List<CardContainer>();

        foreach (Card c in cards)
        {
            container.Add(new CardContainer() { Type = c, Visible = visible });
        }
        #endregion Cards

        return container;
    }
    #endregion Methods
}