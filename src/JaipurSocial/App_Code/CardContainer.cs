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

	public CardContainer()
	{

	}
}