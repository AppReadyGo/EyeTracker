<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Content.Master" Inherits="System.Web.Mvc.ViewPage<EyeTracker.Model.ViewModelWrapper<EyeTracker.Model.Master.BeforeLoginViewModel ,EyeTracker.Model.Pages.Home.ContentModel>>" %>
<asp:Content ID="PageTitleContent" ContentPlaceHolderID="PageTitleContent" runat="server"><%: Model.View.Title %></asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<%= Model.View.Content %>
</asp:Content>


