<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<asp:Content ID="ContentBody" ContentPlaceHolderID="Body" Runat="Server">
    <table>
        <tr>
            <td><asp:Label ID="LblLogin" runat="server" Text="<%$ Resources:Localization, Login %>" /></td>
            <td><asp:TextBox ID="TxtLogin" runat="server" /></td>
        </tr>
         <tr>
            <td><asp:Label ID="LblPassword" runat="server" Text="<%$ Resources:Localization, Password %>" /></td>
            <td><asp:TextBox ID="TxtPassword" TextMode="Password" runat="server" /></td>
        </tr>
        <tr>
            <td><asp:Button ID="BtnLogin" Text="<%$ Resources:Localization, Login %>" runat="server" /></td>
            <td><asp:Button ID="BtnRegister" Text="<%$ Resources:Localization, Register %>" OnClick="BtnRegister_Click"  runat="server" /></td>
        </tr>
    </table>
</asp:Content>

