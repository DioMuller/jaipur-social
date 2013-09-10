<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="_Default" %>
<asp:Content ID="ContentBody" ContentPlaceHolderID="Body" Runat="Server">
    <div class="center">
        <table class="center">
            <tr>
                <td>
                    <asp:Label ID="LblLogin" runat="server" Text="<%$ Resources:Localization, Login %>" />
                </td>
                <td>
                    <asp:TextBox ID="TxtLogin" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LblPassword" runat="server" Text="<%$ Resources:Localization, Password %>" />
                </td>
                <td>
                    <asp:TextBox ID="TxtPassword" TextMode="Password" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div class="center">
                        <asp:Button ID="BtnLogin" Text="<%$ Resources:Localization, Login %>" OnClick="BtnLogin_Click" runat="server" />
                        <asp:Button ID="BtnRegister" Text="<%$ Resources:Localization, Register %>" OnClick="BtnRegister_Click"  runat="server" />
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

