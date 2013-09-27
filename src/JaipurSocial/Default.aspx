<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Body" Runat="Server">
    <table style="width:100%">
        <tr>
            <td style="width:50%"><asp:Label ID="LabelHowToPlay" runat="server" Text="<%$ Resources:Localization, HowToPlay %>" /></td>
            <td><asp:Label ID="LabelCurrentGames" runat="server" Text="<%$ Resources:Localization, CurrentGames %>" /></td>

            <td><asp:Label ID="LabelChallengePeople" runat="server" Text="<%$ Resources:Localization, ChallengePeople %>" /></td>
        </tr>
        <tr style="height: 100%;">
            <td style="height: 100%;">
            <!-- How to Play -->
                <iframe id="frameHowToPlay" src="<%$ Resources:Localization, HelpPage %>" style="width:100%; height: 100%;" frameborder="0" runat="server"/>
            </td>
            <!-- Current Games -->
            <td>
                <asp:GridView ID="GridGames" CssClass="gridview" runat="server" AutoGenerateColumns="false" OnRowCommand="GridGames_RowCommand" DataKeyNames="GameId">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                            <ajaxToolkit:Gravatar ID="Gravatar1" runat="server"
                                Email='<%#Bind("EnemyEmail") %>'
                                Size="32"
                                Rating="R"
                                DefaultImageBehavior="MysteryMan"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="EnemyLogin" HeaderText="<%$ Resources:Localization, Merchant %>" />
                        <asp:TemplateField HeaderText="<%$ Resources:Localization, GameStatus %>">
                            <ItemTemplate>
                                <asp:Label ID="GameStatus" Text='<%#Bind("GameStatus") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:ButtonField ButtonType="Link" Text="<%$ Resources:Localization, ViewGame %>" CommandName="ViewGame" />
                        <asp:ButtonField ButtonType="Link" Text="<%$ Resources:Localization, DeleteGame %>" CommandName="DeleteGame" />
                    </Columns>
                </asp:GridView>
            </td>
            <td style="vertical-align: top;">
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
                        <asp:BoundField DataField="Name" HeaderText="<%$ Resources:Localization, Merchant %>" />
                        <asp:ButtonField ButtonType="Link" Text="<%$ Resources:Localization, Challenge %>" CommandName="Challenge" />
                        </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>

    <!-- Bet Modal -->
    <!--
    <ajaxToolkit:ModalPopupExtender ID="ModalBet" PopupControlID="PanelBet" TargetControlID="GridUsers" runat="server" />
    <asp:Panel ID="PanelBet" runat="server" Visible="false">
        <asp:Label ID="LabelBetAmount" Text="<%$ Resources:Localization, BetAmount %>" runat="server" />
        <asp:TextBox ID="TxtBetAmount" runat="server" />
        <ajaxToolkit:MaskedEditExtender MaskType="Number" Mask="9999" TargetControlID="TxtBetAmount" runat="server" />
        <asp:Button ID="ButtonChallenge" Text="<%$ Resources:Localization, Challenge %>" runat="server" />
    </asp:Panel>
    -->
</asp:Content>

