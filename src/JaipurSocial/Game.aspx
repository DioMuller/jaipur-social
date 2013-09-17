<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Game.aspx.cs" Inherits="Game" %>

<%@ Register Src="~/Controls/UcPlayer.ascx" TagPrefix="uc1" TagName="UcPlayer" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Body" Runat="Server">
    <table>
    <tr>
        <td>
            <!-- Enemy Data -->
            <uc1:UcPlayer runat="server" ID="UcEnemy" />
        </td>
    </tr>
    <tr>
        <asp:DataList ID="DlCards" RepeatDirection="Horizontal" Width="700" runat="server" BackColor="DarkOliveGreen">
            <ItemTemplate>
                <asp:Image ID="ImgCard" Height="121" Width="97" ImageUrl='<%# Bind("RelativeImage") %>' runat="server"/>
            </ItemTemplate>
        </asp:DataList>
    </tr>
    <tr>
        <td>
            <!-- Player Data -->
            <uc1:UcPlayer runat="server" ID="UcPlayer" />
        </td>
    </tr>
    </table>
</asp:Content>

