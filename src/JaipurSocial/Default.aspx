<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Body" Runat="Server">
    <asp:GridView ID="GridUsers" runat="server">
         <Columns> 
            <asp:BoundField DataField="Login" HeaderText="<%$ Resources:Localization, Login %>" />
            <asp:ButtonField ButtonType="Link" Text="<%$ Resources:Localization, Challenge %>" />
         </Columns>
    </asp:GridView>
</asp:Content>

