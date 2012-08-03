<%@ Import Namespace="EyeTracker.Model.Pages.Portfolio" %>
<%@ Page Title="" Language="C#" 
MasterPageFile="~/Views/Shared/AfterLogin.Master" 
Inherits="ViewPage<ViewModelWrapper<AfterLoginMasterModel, PortfolioModel>>" %>

<asp:Content ID="PageTitleContent" ContentPlaceHolderID="PageTitleContent" runat="server">Create Portfolio</asp:Content>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="<%: Url.Content("~/Content/shared/form.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%: Url.Content("~/Content/profile.new.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%: Url.Content("~/Content/after.login.master.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%: Url.Content("~/Content/shared/bredcrumbs.css")%>" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<div class="crumb">
    <div class="border-right">
		<div class="border-left">
			<div class="inner">
                <ul>
		           <li><a href="/Portfolio"><h4>Portfolios</h4></a></li>
		        </ul>
			</div>
		</div>
    </div>
</div>
<div style="clear:both;"></div>
<div class="content-wrapper">
<h2>Create Portfolio</h2>
<% using (Html.BeginForm()) {%>
<% Html.RenderPartial("Form", Model.View); %>
<p><a href="#" class="link2" onClick="$(this).closest('form').submit();"><span><span>Create</span></span></a>&nbsp;<a class="link4" href="/Portfolio"><span><span>Cancel</span></span></a></p>
<% }%>
</div>
</asp:Content>