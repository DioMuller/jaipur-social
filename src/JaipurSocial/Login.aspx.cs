using JaipurSocial.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] != null)
        {
            Response.Redirect("Default.aspx");
        }
    }

    protected void BtnRegister_Click(object sender, EventArgs e)
    {
        Response.Redirect("Register.aspx");
    }

    protected void BtnLogin_Click(object sender, EventArgs e)
    {
        using (var db = new JaipurEntities())
        {
            var user = db.User.FirstOrDefault(u => u.CheckLogin(TxtLogin.Text));

            if (user != null && user.CheckPassword(TxtPassword.Text))
            {
                Session["User"] = user;
                Response.Redirect("Default.aspx");
            }
            else
            {
                //TODO: Show error message: Wrong User or password.
            }
        }
    }
}