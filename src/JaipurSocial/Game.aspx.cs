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
        if( Request.QueryString["GameId"] == null )
        {
            Response.Redirect("default.aspx");
        }
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

        BtnBuy.Enabled = GameData.IsCurrentTurn(UserData.User);
        BtnTrade.Enabled = GameData.IsCurrentTurn(UserData.User);
        BtnBuyAllCamels.Enabled = GameData.IsCurrentTurn(UserData.User);
        BtnSell.Enabled = GameData.IsCurrentTurn(UserData.User);

        if( !IsPostBack )
        {
            UcPlayer.LoadData(UserData, true);
            UcEnemy.LoadData(EnemyData, false);

            var container = CardContainer.GetContainer(GameData.OnTable ,true);
            DlChecks.Items.Clear();

            foreach (CardContainer c in container)
            {
                ListItem item = new ListItem("<img src='" + c.Image + "' height='121' width='97' />", ((int)c.Type).ToString());
                item.Enabled = (c.Type != Card.Camel);
                DlChecks.Items.Add(item);
            }
        }
    }

    protected void BtnBuy_OnClick(object sender, EventArgs e)
    {
        Card card = (Card) int.Parse(DlChecks.SelectedItem.Value);

        try
        {
            GameData.TakeCard(UserData.User, card);
            GameData.Save();

            Response.Redirect(Request.Url.ToString());
        }
        catch (Exception ex)
        {
            //TODO: Show error message.
        }
    }

    protected void BtnTrade_OnClick(object sender, EventArgs e)
    {
        
    }

    protected void BtnBuyAllCamels_OnClick(object sender, EventArgs e)
    {
        try
        {
            GameData.TakeCamels(UserData.User);
            GameData.Save();

            Response.Redirect(Request.Url.ToString());
        }
        catch(Exception ex)
        {
            //TODO: Show error message.
        }
    }

    protected void BtnSell_OnClick(object sender, EventArgs e)
    {
        List<Card> cards = UcPlayer.SelectedCards;

        try
        {
            GameData.SellCards(UserData.User, cards);
            GameData.Save();

            Response.Redirect(Request.Url.ToString());
        }
        catch (Exception ex)
        {
            //TODO: Show error message.
        }
    }
}