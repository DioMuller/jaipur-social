<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Game.aspx.cs" Inherits="Game" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Body" Runat="Server">
    <!-- Enemy Data -->
    <asp:DataList ID="DlEnemyCards" runat="server">
        <ItemTemplate>
            <asp:Image ID="ImgEnemyCard" ImageUrl='<%# Bind("Image") %>' runat="server"/>
        </ItemTemplate>
    </asp:DataList>

    <!-- Player Data -->
    <asp:DataList ID="DlMyCards" runat="server">
        <ItemTemplate>
            <asp:Image ID="ImgMyCard" ImageUrl='<%# Bind("Image") %>' runat="server"/>
        </ItemTemplate>
    </asp:DataList>
</asp:Content>

