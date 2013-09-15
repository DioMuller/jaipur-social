<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Game.aspx.cs" Inherits="Game" %>
<%@ Register Src="~/UserControls/UcPlayerData.ascx" TagPrefix="uc" TagName="UcPlayerData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Body" Runat="Server">
    <uc1:UcPlayerData ID="UcPlayerData" />
</asp:Content>

