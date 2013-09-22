<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UcPlayer.ascx.cs" Inherits="Controls_UcPlayer" %>

<table>
    <tr>
        <td colspan="3">
            <asp:Label ID="LabelName" runat="server"/>
        </td>
    </tr>
    <tr>
        <td>
            <asp:CheckBoxList ID="DlChecks" RepeatDirection="Horizontal" runat="server" Width="700" />
        </td>
        <td>
            <table>
                <tr>
                    <td>
                        <asp:Image ID="ImgCamel" Height="60" Width="48" ImageUrl="../Images/card-camel.png" runat="server"/>
                    </td>
                    <td>
                        <asp:Label ID="LabelCamels" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Image ID="ImgCoins" Height="32" Width="32" ImageUrl="../Images/coins.png" runat="server"/>
                    </td>
                    <td>
                        <asp:Label ID="LabelCoins" runat="server" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>