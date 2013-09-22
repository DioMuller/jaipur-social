<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Game.aspx.cs" Inherits="Game" %>

<%@ Register Src="~/Controls/UcPlayer.ascx" TagPrefix="uc1" TagName="UcPlayer" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Body" Runat="Server">
    <table>
    <tr>
        <td colspan="2">
            <!-- Enemy Data -->
            <uc1:UcPlayer runat="server" ID="UcEnemy" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:CheckBoxList ID="DlChecks" RepeatDirection="Horizontal" runat="server" Width="700" />
        </td>
        <td>
            <!-- Resources -->
            <table>
                <tr>
                    <td><asp:Image ID="ImgRuby" Width="32" ImageUrl="Images/resource-ruby.png" runat="server"/></td>
                    <td><asp:Label ID="LblRuby" runat="server"></asp:Label></td>
                    <td><asp:Image ID="ImgSilk" Width="32" ImageUrl="Images/resource-silk.png" runat="server"/></td>
                    <td><asp:Label ID="LblSilk" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td><asp:Image ID="ImgGold" Width="32" ImageUrl="Images/resource-gold.png" runat="server"/></td>
                    <td><asp:Label ID="LblGold" runat="server"></asp:Label></td>
                    <td><asp:Image ID="ImgSpices" Width="32" ImageUrl="Images/resource-spices.png" runat="server"/></td>
                    <td><asp:Label ID="LblSpices" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td><asp:Image ID="ImgSilver" Width="32" ImageUrl="Images/resource-silver.png" runat="server"/></td>
                    <td><asp:Label ID="LblSilver" runat="server"></asp:Label></td>
                    <td><asp:Image ID="ImgLeather" Width="32" ImageUrl="Images/resource-leather.png" runat="server"/></td>
                    <td><asp:Label ID="LblLeather" runat="server"></asp:Label></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <!-- Player Data -->
            <uc1:UcPlayer runat="server" ID="UcPlayer" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <div class="center">
                <asp:Button ID="BtnBuy" Text="<%$ Resources:Localization, BuyResource %>" OnClick="BtnBuy_OnClick" runat="server" />
                <asp:Button ID="BtnTrade" Text="<%$ Resources:Localization, TradeResources %>" OnClick="BtnTrade_OnClick"  runat="server" />
                <asp:Button ID="BtnBuyAllCamels" Text="<%$ Resources:Localization, BuyCamels %>" OnClick="BtnBuyAllCamels_OnClick" runat="server" />
                <asp:Button ID="BtnSell" Text="<%$ Resources:Localization, SellResources %>" OnClick="BtnSell_OnClick"  runat="server" />
            </div>
        </td>
    </tr>
    </table>
</asp:Content>

