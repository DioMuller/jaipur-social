using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JaipurSocial.Core;
using JaipurSocial.Data;

public partial class Game : LocalizablePage
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

        PlayerData player;
        PlayerData other;

        if (GameData.ChallengerData.User.Id == CurrentUser.Id) //That's me!
        {
            player = GameData.ChallengerData;
            other = GameData.EnemyData;
        }
        else
        {
            other = GameData.ChallengerData;
            player = GameData.EnemyData;
        }

        LblGold.Text = GameData.Resources[Card.Gold].ToString();
        LblSilk.Text = GameData.Resources[Card.Silk].ToString();
        LblSilver.Text = GameData.Resources[Card.Silver].ToString();
        LblSpices.Text = GameData.Resources[Card.Spices].ToString();
        LblRuby.Text = GameData.Resources[Card.Ruby].ToString();
        LblLeather.Text = GameData.Resources[Card.Leather].ToString();

        UcPlayer.LoadData(player, true);
        UcEnemy.LoadData(other, false);

        DlCards.DataSource = CardContainer.GetContainer(GameData.OnTable, true);
        DlCards.DataBind();
    }
}