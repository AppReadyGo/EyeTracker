<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TopMenu.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Import Namespace="EyeTracker.Helpers" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="../../Content/Table.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContent" runat="server">Applications</asp:Content>
<asp:Content ID="Title" ContentPlaceHolderID="Title" runat="server">Applications</asp:Content>
<asp:Content ID="TopMenu" ContentPlaceHolderID="TopMenu" runat="server">
    <a href="/Application/New/<%= ViewBag.PortfolioId %>">New Application</a>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.Table((List<List<EyeTracker.Helpers.HTMLTable.Cell>>)ViewData["data"], (EyeTracker.Helpers.HTMLTable.Cell)ViewData["caption"], (List<EyeTracker.Helpers.HTMLTable.Cell>)ViewData["columnHeaders"], null, null, "grey", "acc_table")%>
</asp:Content>
