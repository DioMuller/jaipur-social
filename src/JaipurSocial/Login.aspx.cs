using JaipurSocial.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : LocalizablePage
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
        try
        {
            using (var db = new JaipurEntities())
            {
                var user = db.User.FirstOrDefault(u => u.Login.ToLower() == TxtLogin.Text.ToLower());

                if (user != null && user.CheckPassword(TxtPassword.Text))
                {
                    Session["User"] = user;
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    if (user == null)
                    {
                        ShowMessage(Resources.Localization.InvalidUser);
                    }
                    else
                    {
                        ShowMessage(Resources.Localization.InvalidPassword);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(Resources.Localization.Exception + ex.Message);
        }
        
    }
}