<%@ Page Title="" Language="C#" 
MasterPageFile="~/Views/Shared/AfterLogin.Master" 
Inherits="ViewPage<ViewModelWrapper<AfterLoginMasterModel, EyeTracker.Model.Pages.Application.ScreenModel>>" %>

<asp:Content ID="PageTitleContent" ContentPlaceHolderID="PageTitleContent" runat="server">Create Application</asp:Content>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="<%: Url.Content("~/Content/shared/form.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%: Url.Content("~/Content/application.screennew.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%: Url.Content("~/Content/shared/bredcrumbs.css")%>" rel="stylesheet" type="text/css" />
    <script src="<%: Url.Content("~/scripts/application.screen.js")%>" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<div class="crumb">
    <div class="border-right">
		<div class="border-left">
			<div class="inner">
                <ul>
		           <li><a href="/Portfolio"><h4>Portfolios</h4></a></li>
		           <li><a href="/Application/<%: ViewBag.PortfolioId %>"><h4><%: ViewBag.PortfolioDescription %> - Applications</h4></a></li>
		           <li><a href="/Application/Screens/<%: Model.View.ApplicationId %>"><h4><%: ViewBag.ApplicationDescription %> - Screens</h4></a></li>
		        </ul>
			</div>
		</div>
    </div>
</div>
<div style="clear:both;"></div>
<div class="content-wrapper">
<h2>Add Screen</h2>
<form action="" method="post" enctype="multipart/form-data">
<% Html.RenderPartial("Forms/ScreenForm", Model.View); %>
</form>
</div>
</asp:Content>


