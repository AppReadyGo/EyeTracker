<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/AfterLogin.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Import Namespace="EyeTracker.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitleContent" runat="server">Applications</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="../../Content/Table.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Applications List</h3>
    <%= Html.Table((List<List<EyeTracker.Helpers.HTMLTable.Cell>>)ViewData["data"], null, (List<EyeTracker.Helpers.HTMLTable.Cell>)ViewData["columnHeaders"], null, null, "grey", "acc_table")%>
</asp:Content>
