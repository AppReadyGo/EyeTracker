<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TopMenu.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Import Namespace="EyeTracker.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">Portfolios</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="../../Content/Table.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Title" runat="server">Portfolios</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="TopMenu" runat="server">
    <%:Html.ActionLink("New Portfolio", "New") %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.Table((List<List<EyeTracker.Helpers.HTMLTable.Cell>>)ViewData["data"], (EyeTracker.Helpers.HTMLTable.Cell)ViewData["caption"], (List<EyeTracker.Helpers.HTMLTable.Cell>)ViewData["columnHeaders"], null, null, "grey", "acc_table")%>
</asp:Content>

