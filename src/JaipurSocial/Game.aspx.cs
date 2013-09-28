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

    #region UI Properties
    public IEnumerable<Card> SelectedCards
    {
        get
        {
            return from ListItem ck in DlChecks.Items
                   where ck.Selected
                   select (Card)int.Parse(ck.Value);
        }
    }
    #endregion

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
            return;
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

        GameStatus.Text = GameData.GetStatus(CurrentUser);

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

        BtnBuy.Enabled = GameData.IsCurrentTurn(CurrentUser);
        BtnTrade.Enabled = GameData.IsCurrentTurn(CurrentUser);
        BtnBuyAllCamels.Enabled = GameData.IsCurrentTurn(CurrentUser);
        BtnSell.Enabled = GameData.IsCurrentTurn(CurrentUser);

        if (!GameData.IsCurrentTurn(CurrentUser))
        {
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(GetType(),
                "auto_refresh",
                "<script type=\"text/javascript\">window.setInterval(function() { window.location.reload(); }, 10000);</script>",
                false);
        }

        if (!IsPostBack)
        {
            UcPlayer.LoadData(UserData, true);
            UcEnemy.LoadData(EnemyData, false);

            var container = CardContainer.GetContainer(GameData.OnTable, true);
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
        try
        {
            var card = SelectedCards.Single();

            GameData.TakeCard(UserData.User, card);
            GameData.Save();

            Response.Redirect(Request.Url.ToString());

            CheckGameEnd();
        }
        catch (Exception ex)
        {
            //TODO: Show error message.
        }
    }

    protected void BtnTrade_OnClick(object sender, EventArgs e)
    {
        try
        {
            var userCards = UcPlayer.SelectedCards.ToList();
            var tableCards = SelectedCards.ToList();

            GameData.TradeCards(UserData.User, userCards, tableCards);
            GameData.Save();

            Response.Redirect(Request.Url.ToString());
        }
        catch (Exception ex)
        {
            //TODO: Show error message.
        }
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
        try
        {
            var cards = UcPlayer.SelectedCards.GroupBy(c => c).Single();
            GameData.SellCards(UserData.User, cards.ToList());
            GameData.Save();

            Response.Redirect(Request.Url.ToString());

            CheckGameEnd();
        }
        catch (Exception ex)
        {
            //TODO: Show error message.
        }
    }

    private void CheckGameEnd()
    {
        if (GameData.IsGameFinished)
            GameData.PayWinner();
    }
}