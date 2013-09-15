<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Body" Runat="Server">
    <asp:GridView ID="GridUsers" runat="server" AutoGenerateColumns="false" OnRowCommand="GridUsers_RowCommand" DataKeyNames="Id">
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
</asp:Content>

