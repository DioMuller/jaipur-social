<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UcPlayer.ascx.cs" Inherits="Controls_UcPlayer" %>

<table>
    <tr>
        <td colspan="3">
            <asp:Label ID="LabelName" runat="server"/>
        </td>
    </tr>
    <tr>
        <td>
            <asp:DataList ID="DlCards" RepeatDirection="Horizontal" runat="server">
                <ItemTemplate>
                    <asp:Image ID="ImgCard" Height="121" Width="97" ImageUrl='<%# Bind("RelativeImage") %>' runat="server"/>
                </ItemTemplate>
            </asp:DataList>
        </td>
        <td>
            <asp:Image ID="ImgCamel" Height="60" Width="48" ImageUrl="../Images/card-camel.png" runat="server"/>
        </td>
        <td>
            <asp:Label ID="LabelCamels" runat="server" />
        </td>
    </tr>
</table>