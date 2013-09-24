using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JaipurSocial.Data;

public partial class Register : LocalizablePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] != null)
        {
            Response.Redirect("Default.aspx");
        }

        BtnBack.Attributes.Add("onClick", "javascript:history.back(); return false;");
    }
    protected void BtnRegister_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            byte[] bytes = CryptoHelper.GetHash(TxtPassword.Text);

            using (var db = new JaipurEntities())
            {
                if (!db.User.Any(u => u.Login.ToLower() == TxtLogin.Text.ToLower()))
                {
                    db.User.Add(new User
                    {
                        Login = TxtLogin.Text,
                        Email = TxtEmail.Text,
                        Name = TxtName.Text,
                        Coins = 100,
                        Password = bytes
                    });
                    db.SaveChanges();

                    Response.Redirect("Login.aspx");
                }
                else
                {
                    //TODO: Show error message: User already exists
                }
            }
        }
    }
}