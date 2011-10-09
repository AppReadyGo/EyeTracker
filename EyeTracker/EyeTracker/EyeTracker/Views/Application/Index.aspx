<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TopMenu.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="../../Content/Table.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContent" runat="server">Applications</asp:Content>
<asp:Content ID="Title" ContentPlaceHolderID="Title" runat="server">Applications</asp:Content>
<asp:Content ID="TopMenu" ContentPlaceHolderID="TopMenu" runat="server">
    <%:Html.ActionLink("New Application", "New") %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
</asp:Content>
