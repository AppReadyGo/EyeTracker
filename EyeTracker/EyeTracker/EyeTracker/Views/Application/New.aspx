<%@ Page Title="" Language="C#" 
MasterPageFile="~/Views/Shared/AfterLogin.Master" 
Inherits="ViewPage<ViewModelWrapper<AfterLoginMasterModel, EyeTracker.Model.Pages.Application.ApplicationModel>>" %>

<asp:Content ID="PageTitleContent" ContentPlaceHolderID="PageTitleContent" runat="server">Create Application</asp:Content>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="<%: Url.Content("~/Content/shared/form.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%: Url.Content("~/Content/application.new.css")%>" rel="stylesheet" type="text/css" />
    <script src="<%: Url.Content("~/Scripts/application.new.0.1.js")%>" type="text/javascript"></script>
    <link href="<%: Url.Content("~/Content/after.login.master.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%: Url.Content("~/Content/shared/bredcrumbs.css")%>" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var newAppURL = '/Application/New/<%: Model.View.PortfolioId %>/';
        var addScreenURL = '/Application/AddScreen/<%: Model.View.PortfolioId %>/';
        var screenImgURL = '/Application/Screen/';
    </script>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<div class="crumb">
    <div class="border-right">
		<div class="border-left">
			<div class="inner">
                <ul>
		           <li><a href="/Portfolio"><h4>Portfolios</h4></a></li>
		           <li><a href="/Application/<%: Model.View.PortfolioId %>"><h4><%: ViewBag.PortfolioDescritpion%> - Applications</h4></a></li>
		        </ul>
			</div>
		</div>
    </div>
</div>
<div style="clear:both;"></div>
<div class="content-wrapper">
<h2>Create Application</h2>
<% using (Html.BeginForm()) {%>
<% Html.RenderPartial("Forms/ApplicationForm", Model.View); %>
<%} %>
</div>
</asp:Content>


