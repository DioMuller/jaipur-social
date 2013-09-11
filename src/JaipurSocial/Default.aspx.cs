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
                GridUsers.DataSource = db.User.Where( (u) => u.Id != logged.Id );
                GridUsers.DataBind();
            }
        }
    }
}