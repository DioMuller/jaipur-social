using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        #region jQuery
        ScriptResourceDefinition myScriptResDef = new ScriptResourceDefinition();
        myScriptResDef.Path = "~/Scripts/jquery-1.10.2.min.js";
        myScriptResDef.DebugPath = "~/Scripts/jquery-1.10.2.js";
        myScriptResDef.CdnPath = "http://ajax.microsoft.com/ajax/jQuery/jquery-1.10.2.min.js";
        myScriptResDef.CdnDebugPath = "http://ajax.microsoft.com/ajax/jQuery/jquery-1.10.2.js";
        ScriptManager.ScriptResourceMapping.AddDefinition("jquery", null, myScriptResDef);
        #endregion jQuery

        #region Logged User
        if( Session["User"] == null )
        {
            string name = Path.GetFileName(Request.PhysicalPath).ToLower();
            if( name != "login.aspx" && name != "register.aspx" )
            {
                Response.Redirect("login.aspx");
            }
        }
        #endregion Logged User

        string culture = Session["Culture"] as string;

        if( culture != null )
        {
            Page.Culture = culture;
            Page.UICulture = culture;
        }
    }

    protected void ButtonEnglish_Click(object sender, ImageClickEventArgs e)
    {
        ChangeCulture("en");
    }
    protected void ButtonPortuguese_Click(object sender, ImageClickEventArgs e)
    {
        ChangeCulture("pt-br");
    }

    private void ChangeCulture(string culture)
    {
        Session["Culture"] = culture;
        Response.Redirect(Request.RawUrl);
    }

    protected void ImageTitle_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("default.aspx");
    }
}
