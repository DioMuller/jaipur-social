using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JaipurSocial.Core;

public partial class Controls_UcPlayer : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void LoadData(PlayerData data)
    {
        DlCards.DataSource = CardContainer.GetContainer(data.Hand, true);
        DlCards.DataBind();

        LabelName.Text = data.User.Login;
        LabelCamels.Text = data.Camels.ToString();
    }
}