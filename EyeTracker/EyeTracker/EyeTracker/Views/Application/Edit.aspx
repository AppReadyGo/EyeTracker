<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/AfterLogin.Master" Inherits="System.Web.Mvc.ViewPage<EyeTracker.Model.ViewModelWrapper<EyeTracker.Model.Master.AfterLoginViewModel,EyeTracker.Model.Pages.Application.ApplicationEditModel>>" %>

<asp:Content ID="PageTitleContent" ContentPlaceHolderID="PageTitleContent" runat="server">Create Application</asp:Content>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="<%: Url.Content("~/Content/shared/form.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%: Url.Content("~/Content/application.new.css")%>" rel="stylesheet" type="text/css" />
    <script src="<%: Url.Content("~/Scripts/application.new.js")%>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/ThridParty/ajaxfileupload.js")%>" type="text/javascript"></script>
	<script type="text/javascript" src="<%: Url.Content("~/Scripts/ThridParty/fancybox/jquery.fancybox-1.3.4.pack.js")%>"></script>
	<link rel="stylesheet" type="text/css" href="<%: Url.Content("~/Scripts/ThridParty/fancybox/jquery.fancybox-1.3.4.css")%>" media="screen" />
    <script type="text/javascript">
        var newAppURL = '/Application/New/<%: Model.View.ViewData.PortfolioId %>/';
        var appId = <%: Model.View.Id %>;
        var addScreenURL = '/Application/AddScreen/<%: Model.View.ViewData.PortfolioId %>/';
        var screenImgURL = '/Application/Screen/';
    </script>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<h3 class="title">Edit Application</h3>
<% Html.RenderPartial("Form", Model.View); %>
</asp:Content>


