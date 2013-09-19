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
    private GameData GameData { get; set; }
    private PlayerData EnemyData { get; set; }
    private PlayerData UserData { get; set; }

    #region Session Properties
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
    #endregion Session Properties

    protected void Page_Load(object sender, EventArgs e)
    {
        #region Load Game
        using (var db = new JaipurEntities())
        {
            var gameId = int.Parse(Request.QueryString["GameId"].ToString());
            var game = db.Game.FirstOrDefault(g => g.Id == gameId);

            if (game == null)
            {
                //throw new Exception("Game not found");
                Response.Redirect("Default.aspx");
                return;
            }

            if (game.ChallengerId != CurrentUser.Id && game.EnemyId != CurrentUser.Id)
            {
                //throw new Exception("No permission");
                Response.Redirect("Default.aspx");
            }

            GameData = new GameData(game);

            if (game.ChallengerId == CurrentUser.Id)
            {
                UserData = GameData.ChallengerData;
                EnemyData = GameData.EnemyData;
            }
            else
            {
                UserData = GameData.EnemyData;
                EnemyData = GameData.ChallengerData;
            }
        }
        #endregion Load Game

        var x = Request.QueryString["a"];

        LblGold.Text = GameData.Resources[Card.Gold].ToString();
        LblSilk.Text = GameData.Resources[Card.Silk].ToString();
        LblSilver.Text = GameData.Resources[Card.Silver].ToString();
        LblSpices.Text = GameData.Resources[Card.Spices].ToString();
        LblRuby.Text = GameData.Resources[Card.Ruby].ToString();
        LblLeather.Text = GameData.Resources[Card.Leather].ToString();

        UcPlayer.LoadData(UserData, true);
        UcEnemy.LoadData(EnemyData, false);

        DlCards.DataSource = CardContainer.GetContainer(GameData.OnTable, true);
        DlCards.DataBind();
    }
}