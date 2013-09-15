using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JaipurSocial.Core;
using JaipurSocial.Data;

public partial class Game : System.Web.UI.Page
{
    #region Session Properties
    private GameData GameData
    {
        get
        {
            return Session["CurrentGame"] as GameData;
        }
        set
        {
            Session["CurrentGame"] = value;
        }
    }

    private User CurrentUser
    {
        get
        {
            return Session["User"] as User;
        }
        set
        {
            Session["User"] = value;
        }
    }

    private User Enemy
    {
        get
        {
            return Session["Enemy"] as User;
        }
        set
        {
            Session["Enemy"] = value;
        }
    }
    #endregion Session Properties


    protected void Page_Load(object sender, EventArgs e)
    {
        #region New Game
        if( GameData == null )
        {
            if( Enemy != null )
            {
                GameData = GameData.CreateNewGame(CurrentUser, Enemy);
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
        #endregion New Game

        if (GameData.ChallengerData.User.Id == CurrentUser.Id) //That's me!
        {
            DlMyCards.DataSource = GetContainer(GameData.ChallengerData.Hand, true);
            DlEnemyCards.DataSource = GetContainer(GameData.EnemyData.Hand, false);
        }
        else
        {
            DlMyCards.DataSource = GetContainer(GameData.EnemyData.Hand, true);
            DlEnemyCards.DataSource = GetContainer(GameData.ChallengerData.Hand, false);
        }
        DlMyCards.DataBind();
        DlEnemyCards.DataBind();
    }


    #region Methods
    List<CardContainer> GetContainer(IReadOnlyList<Card> cards, bool visible)
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