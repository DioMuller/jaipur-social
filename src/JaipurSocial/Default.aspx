<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Body" Runat="Server">
    <table style="width:100%">
        <tr>
            <td style="width:30%"><asp:Label ID="Label2" runat="server" Text="<%$ Resources:Localization, CurrentGames %>" /></td>
            <td style="width:40%"></td>
            <td style="width:30%"><asp:Label ID="Label1" runat="server" Text="<%$ Resources:Localization, ChallengePeople %>" /></td>
        </tr>
        <tr>
            <!-- Current Games -->
            <td>
                
            </td>
            <td>
            <!-- Something? -->
            </td>
            <td>
                <!-- Challenge People -->
                
                <asp:GridView ID="GridUsers" CssClass="gridview" runat="server" AutoGenerateColumns="false" OnRowCommand="GridUsers_RowCommand" DataKeyNames="Id">
                        <Columns> 
                        <asp:TemplateField>
                            <ItemTemplate>
                            <ajaxToolkit:Gravatar ID="Gravatar1" runat="server"
                                Email='<%#Bind("Email") %>'
                                Size="32"
                                Rating="R"
                                DefaultImageBehavior="MysteryMan"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Login" HeaderText="<%$ Resources:Localization, Merchant %>" />
                        <asp:ButtonField ButtonType="Link" Text="<%$ Resources:Localization, Challenge %>" CommandName="Challenge" />
                        </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>

</asp:Content>

