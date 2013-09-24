using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JaipurSocial.Data;
using JaipurSocial.Core;

public partial class _Default : LocalizablePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        using (var db = new JaipurEntities())
        {
            User logged = (Session["User"] as User);

            if( logged != null )
            {
                List<User> users = db.User.Where((u) => u.Id != logged.Id).ToList<User>();
                GridUsers.DataSource = users;
                GridUsers.DataBind();

                try
                {
                    var dbGames = from game in db.Game.Where(g => g.EnemyId == logged.Id || g.ChallengerId == logged.Id).ToList()
                                  select new GameData(game);

                    var games = from game in dbGames
                                let enemy = game.GetEnemyData(logged)
                                where !game.IsDeleted(logged)
                                select new RunninGameInfo
                                {
                                    GameId = game.Id,
                                    EnemyLogin = enemy.User.Name,
                                    EnemyEmail = enemy.User.Email,
                                    GameStatus = game.GetStatus(logged)
                                };
                    GridGames.DataSource = games.ToList();
                    GridGames.DataBind();
                }
                catch { }
            }
        }
    }

    protected void GridGames_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewGame")
        {
            GridGames.SelectedIndex = int.Parse(e.CommandArgument.ToString());
            GridGames.DataBind();
            Response.Redirect("Game.aspx?GameId=" + GridGames.SelectedDataKey.Value);
        }

        else if (e.CommandName == "DeleteGame")
        {
            GridGames.SelectedIndex = int.Parse(e.CommandArgument.ToString());
            GridGames.DataBind();

            using (var db = new JaipurEntities())
            {
                User logged = (Session["User"] as User);
                var gameId = int.Parse(GridGames.SelectedDataKey.Value.ToString());
                var game = db.Game.FirstOrDefault(g => g.Id == gameId);

                var data = new GameData(game);
                data.MarkAsDeleted(logged);
                data.Save();

                db.SaveChanges();
            }
        }
    }

    protected void GridUsers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if( e.CommandName == "Challenge" )
        {
            GridUsers.SelectedIndex = int.Parse(e.CommandArgument.ToString());
            GridUsers.DataBind();

            using (var db = new JaipurEntities())
            {
                var enemy = db.User.FirstOrDefault((u) => u.Id == (int)GridUsers.SelectedDataKey.Value);

                var gameData = JaipurSocial.Core.GameData.CreateNewGame(Session["User"] as User, enemy);
                var saved = db.Game.Add(gameData.ToGame());
                db.SaveChanges();
                Response.Redirect("Game.aspx?GameId=" + saved.Id);
            }
        }
    }
}