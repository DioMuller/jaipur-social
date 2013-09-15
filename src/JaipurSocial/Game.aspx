<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Game.aspx.cs" Inherits="Game" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Body" Runat="Server">
    <table>
    <tr>
        <td>
            <!-- Enemy Data -->
            <asp:DataList ID="DlEnemyCards" RepeatDirection="Horizontal" runat="server">
                <ItemTemplate>
                    <asp:Image ID="ImgEnemyCard" ImageUrl='<%# Bind("Image") %>' runat="server"/>
                </ItemTemplate>
            </asp:DataList>
        </td>
    </tr>
        <tr>
        <td>
            <!-- Player Data -->
            <asp:DataList ID="DlMyCards" RepeatDirection="Horizontal" runat="server">
                <ItemTemplate>
                    <asp:Image ID="ImgMyCard" ImageUrl='<%# Bind("Image") %>' runat="server"/>
                </ItemTemplate>
            </asp:DataList>
        </td>
    </tr>
    </table>
</asp:Content>

