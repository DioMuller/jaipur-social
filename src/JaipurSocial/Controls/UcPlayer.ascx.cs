using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JaipurSocial.Core;
using JaipurSocial.Data;

public partial class Controls_UcPlayer : System.Web.UI.UserControl
{
    public IEnumerable<Card> SelectedCards
    {
        get
        {
            return from ListItem ck in DlChecks.Items
                   where ck.Selected
                   select (Card)int.Parse(ck.Value);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void LoadData(PlayerData data, bool visible)
    {
        var container = CardContainer.GetContainer(data.Hand, visible);

        foreach (CardContainer c in container)
        {
            ListItem item = new ListItem("<img src='" + c.Image + "' height='121' width='97' />", ((int)c.Type).ToString());
            item.Enabled = (c.Type != Card.Camel) && c.Visible;
            DlChecks.Items.Add(item);
        }
        LabelName.Text = data.User.Login;
        LabelCamels.Text = data.Camels.ToString();
    }
}