<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/m/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<EyeTracker.Model.Pages.Home.ContentModel>" %>
<asp:Content ID="PageTitleContent" ContentPlaceHolderID="PageTitleContent" runat="server">
<%: Model.Title %>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <%= Model.Content%>
</asp:Content>


