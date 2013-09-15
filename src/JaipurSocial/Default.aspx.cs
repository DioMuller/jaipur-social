using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JaipurSocial.Data;

public partial class _Default : System.Web.UI.Page
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
                Session["Enemy"] = db.User.FirstOrDefault((u) => u.Id == (int)GridUsers.SelectedDataKey.Value);
            }

            Response.Redirect("Game.aspx");
        }
    }
}