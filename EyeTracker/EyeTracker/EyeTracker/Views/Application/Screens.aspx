<%@ Page Title="" Language="C#" 
MasterPageFile="~/Views/Shared/Analytics.Master" 
Inherits="ViewPage<ViewModelWrapper<AfterLoginMasterModel, AnalyticsMasterModel, EyeTracker.Model.Pages.Application.ApplicationModel>>" %>

<asp:Content ID="PageTitleContent" ContentPlaceHolderID="PageTitleContent" runat="server">Create Application</asp:Content>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="<%: Url.Content("~/Content/shared/form.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%: Url.Content("~/Content/application.new.css")%>" rel="stylesheet" type="text/css" />
    <script src="<%: Url.Content("~/Scripts/application.new.js")%>" type="text/javascript"></script>
    <link href="<%: Url.Content("~/Content/after.login.master.css")%>" rel="stylesheet" type="text/css" />
    <script src="<%: Url.Content("~/Scripts/ThridParty/ajaxfileupload.js")%>" type="text/javascript"></script>
	<script type="text/javascript" src="<%: Url.Content("~/Scripts/ThridParty/fancybox/jquery.fancybox-1.3.4.pack.js")%>"></script>
	<link rel="stylesheet" type="text/css" href="<%: Url.Content("~/Scripts/ThridParty/fancybox/jquery.fancybox-1.3.4.css")%>" media="screen" />
    <script type="text/javascript">
        var newAppURL = '/Application/New/<%: Model.View.PortfolioId %>/';
        var appId = <%: Model.View.Id %>;
        var addScreenURL = '/Application/AddScreen/<%: Model.View.PortfolioId %>/';
        var screenImgURL = '/Application/Screen/';
    </script>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<h2 class="title">Create Application</h2>
<div id="screens" class="step" style="display:none;">
    <div id="img_preview" class="img-preview"><div><img src="" /><a>X</a></div></div>
    <h3>2. Screenshots</h3>
    <ul id="screens_list">
    <%foreach (var curScreen in Model.ViewData.Screens)
        { %>
        <li><a class="remove-btn">&nbsp</a><a class="img-lnk" href="/Application/Screen/<%: Model.Id %>/<%:curScreen.Width %>/<%:curScreen.Height %>/screen.jpg"><%:curScreen.Width %>X<%:curScreen.Height %></a></li>
    <%} %>
    </ul>
    <fieldset>
        <legend>New Screen</legend>
        <div class="error" id="screen_error"></div>
        <div>Width: <input id="screen_width"/></div>
        <div>Height: <input id="screen_height"/></div>
        <div>Image: <input type="file" id="screen_img" name="screen_img"/></div>
        <div><a id="add_screen_btn">add</a></div>
    </fieldset>
    </div>
</asp:Content>


