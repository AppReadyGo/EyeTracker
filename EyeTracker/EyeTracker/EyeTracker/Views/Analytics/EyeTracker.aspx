<%@ Page Title="" Language="C#" MasterPageFile="LeftMenu.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">Eye Tracker</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Title" runat="server">Eye Tracker</asp:Content>
<asp:Content ID="TopMenu" ContentPlaceHolderID="TopMenu" runat="server">
    <%:Html.ActionLink("Applications", "", "Application") %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>EyeTracker</h2>

</asp:Content>

