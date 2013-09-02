<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Body" Runat="Server">
    <table>
        <tr>
            <td><asp:Label ID="LblLogin" runat="server" Text="<%$ Resources:Localization, Login %>" /></td>
            <td><asp:TextBox ID="TxtLogin" runat="server" /></td>
            <td><asp:RequiredFieldValidator ID="RfvLogin" runat="server" ControlToValidate="TxtLogin" Text="<%$ Resources:Localization, RequiredLogin %>" /></td>
        </tr>
        <tr>
            <td><asp:Label ID="LblEmail" runat="server" Text="<%$ Resources:Localization, Email %>" /></td>
            <td><asp:TextBox ID="TxtEmail" runat="server" /></td>
            <td><asp:RequiredFieldValidator ID="RfvEmail" runat="server" ControlToValidate="TxtEmail" Text="<%$ Resources:Localization, RequiredEmail %>" /></td>
        </tr>
        <tr>
            <td><asp:Label ID="LblConfirmEmail" runat="server" Text="<%$ Resources:Localization, ConfirmEmail %>" /></td>
            <td><asp:TextBox ID="TxtConfirmEmail" runat="server" /></td>
            <td>
                <asp:RequiredFieldValidator ID="RfvConfirmEmail" runat="server" ControlToValidate="TxtConfirmEmail" Text="<%$ Resources:Localization, RequiredEmail %>" />
                <asp:CompareValidator ID="CmvEmail" runat="server" ControlToValidate="TxtConfirmEmail" ControlToCompare="TxtEmail" Text="<%$ Resources:Localization, RequiredValidateEmail %>" />
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="LblPassword" runat="server" Text="<%$ Resources:Localization, Password %>" /></td>
            <td><asp:TextBox ID="TxtPassword" TextMode="Password" runat="server" /></td>
            <td><asp:RequiredFieldValidator ID="RfvPassword" runat="server" ControlToValidate="TxtPassword" Text="<%$ Resources:Localization, RequiredPassword %>" /></td>
        </tr>
        <tr>
            <td><asp:Label ID="LblConfirmPassword" runat="server" Text="<%$ Resources:Localization, ConfirmPassword %>" /></td>
            <td><asp:TextBox ID="TxtConfirmPassword" TextMode="Password" runat="server" /></td>
            <td>
                <asp:RequiredFieldValidator ID="RfvConfirmPassword" runat="server" ControlToValidate="TxtConfirmPassword" Text="<%$ Resources:Localization, RequiredPassword %>" />
                <asp:CompareValidator ID="CmvPassword" runat="server" ControlToValidate="TxtConfirmPassword" ControlToCompare="TxtPassword" Text="<%$ Resources:Localization, RequiredValidatePassword %>" />
                <ajaxControlToolkit
            </td>
        </tr>
        <tr>
            <td><asp:Button ID="BtnRegister" Text="<%$ Resources:Localization, Register %>" OnClick="BtnRegister_Click" runat="server" /></td>
            <td><asp:Button ID="BtnBack" Text="<%$ Resources:Localization, Back %>" runat="server" /></td>
        </tr>
    </table>
</asp:Content>

