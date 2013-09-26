using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;

/// <summary>
/// Summary description for LocalizablePage
/// </summary>
public class LocalizablePage : Page
{
    protected override void InitializeCulture()
    {
        string culture = Convert.ToString(Session["Culture"]);

        if( culture != null )
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
        }

        base.InitializeCulture();
    }

    public void ShowMessage(string message)
    {
        System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('" + message + "')</SCRIPT>");
    }

    public void ShowMessage(string message, string redirectAfter)
    {
        System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('" + message + "')</SCRIPT>;document.location='" + ResolveClientUrl(redirectAfter) +"';</script>");
    }
}