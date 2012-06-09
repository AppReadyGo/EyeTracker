<%@ Import Namespace="EyeTracker.Model.Pages.Portfolio" %>
<%@ Page Title="" Language="C#" 
MasterPageFile="~/Views/Shared/Analytics.Master" 
Inherits="ViewPage<ViewModelWrapper<AfterLoginMasterModel, AnalyticsMasterModel, PortfolioModel>>" %>

<asp:Content ID="PageTitleContent" ContentPlaceHolderID="PageTitleContent" runat="server">Create Portfolio</asp:Content>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="<%: Url.Content("~/Content/shared/form.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%: Url.Content("~/Content/profile.new.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%: Url.Content("~/Content/after.login.master.css")%>" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<h2 class="title">Edit Portfolio</h2>
<% using (Html.BeginForm()) {%>
<% Html.RenderPartial("Form", Model.View); %>
<p><a href="#" class="link2" onClick="$(this).closest('form').submit();"><span><span>Save</span></span></a>&nbsp;<a class="link4" href="/Analytics"><span><span>Cancel</span></span></a></p>
<% }%>
</asp:Content>