<%@ Import Namespace="EyeTracker.Model.Pages.Portfolio" %>
<%@ Page Title="" Language="C#" 
MasterPageFile="~/Views/Shared/Analytics.master" 
Inherits="ViewPage<ViewModelWrapper<AfterLoginMasterModel, AnalyticsMasterModel,PortfolioModel>>" %>

<asp:Content ID="PageTitleContent" ContentPlaceHolderID="PageTitleContent" runat="server">Create Portfolio</asp:Content>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="<%: Url.Content("~/Content/shared/form.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%: Url.Content("~/Content/profile.new.css")%>" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<h3 class="title">Create Portfolio</h3>
<% using (Html.BeginForm()) {%>
<% Html.RenderPartial("Form", Model.View); %>
<p><input type="submit" value="Create Portfolio" /><a href="/Analytics">Cancel</a></p>
<% }%>
</asp:Content>