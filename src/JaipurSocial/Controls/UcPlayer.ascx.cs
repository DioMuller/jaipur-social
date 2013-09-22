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
    public List<Card> SelectedCards
    {
        get
        {
            List<Card> cards = new List<Card>();
            var items = DlChecks.Items.Cast<ListItem>()
                   .Where(item => item.Selected);

            foreach (ListItem item in items)
            {
                Card card = (Card)int.Parse(item.Value);
                cards.Add(card);
            }

            return cards;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void LoadData(PlayerData data, bool visible)
    {
        var container = CardContainer.GetContainer(data.Hand, visible);
        DlChecks.Items.Clear();

        foreach( CardContainer c in container )
        {
            ListItem item = new ListItem("<img src='" + c.Image + "' height='121' width='97' />", ((int)c.Type).ToString());
            item.Enabled = (c.Type != Card.Camel) && c.Visible;
            DlChecks.Items.Add(item);
        }
        LabelName.Text = data.User.Login;
        LabelCamels.Text = data.Camels.ToString();
        LabelCoins.Text = data.Points.ToString();
    }
}