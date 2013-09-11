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
        if( Session["User"] != null )
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
            byte[] pass = CryptoHelper.GetHash(TxtPassword.Text);
            var users = db.User.Where( (u) => (u.Login.ToLower() == TxtLogin.Text.ToLower() && Array.Equals(pass, u.Password) ) );

            if( users.Count() > 0 )
            {
                Session["User"] = users.First();
                Response.Redirect("Default.aspx");
            }
            else
            {
                //TODO: Show error message: Wrong User or password.
            }
        }
    }
}