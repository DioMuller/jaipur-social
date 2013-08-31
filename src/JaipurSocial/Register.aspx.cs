using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JaipurSocial.Data;

public partial class Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BtnBack.Attributes.Add("onClick", "javascript:history.back(); return false;");
    }
    protected void BtnRegister_Click(object sender, EventArgs e)
    {
        if( IsValid )
        {
            SHA256 hash = SHA256.Create();
            char[] array = TxtPassword.Text.ToCharArray();
            byte[] chars = new byte[array.Length];
            System.Buffer.BlockCopy(array, 0, chars, 0, chars.Length);
            byte[] bytes = hash.ComputeHash(chars);

            using (var db = new JaipurEntities())
            {
                db.User.Add(new User() 
                { 
                    Login = TxtLogin.Text,
                    Email = TxtEmail.Text, 
                    Password = bytes
                });
                db.SaveChanges();
            }
        }
    }
}