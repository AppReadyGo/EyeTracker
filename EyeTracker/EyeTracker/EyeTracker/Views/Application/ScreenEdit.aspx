<%@ Page Title="" Language="C#" 
MasterPageFile="~/Views/Shared/AfterLogin.Master" 
Inherits="ViewPage<ViewModelWrapper<AfterLoginMasterModel, EyeTracker.Model.Pages.Application.ScreenModel>>" %>

<asp:Content ID="PageTitleContent" ContentPlaceHolderID="PageTitleContent" runat="server">Create Application</asp:Content>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="<%: Url.Content("~/Content/shared/form.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%: Url.Content("~/Content/application.screenedit.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%: Url.Content("~/Content/after.login.master.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%: Url.Content("~/Content/shared/bredcrumbs.css")%>" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="<%: Url.Content("~/Scripts/ThridParty/fancybox/jquery.fancybox.css?v=2.0.6")%>" type="text/css" media="screen" />
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/ThridParty/fancybox/jquery.fancybox.pack.js?v=2.0.6")%>"></script>

    <!-- Optionally add helpers - button, thumbnail and/or media -->
    <link rel="stylesheet" href="<%: Url.Content("~/Scripts/ThridParty/fancybox/helpers/jquery.fancybox-buttons.css?v=1.0.2")%>" type="text/css" media="screen" />
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/ThridParty/fancybox/helpers/jquery.fancybox-buttons.js?v=1.0.2")%>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/ThridParty/fancybox/helpers/jquery.fancybox-media.js?v=1.0.0")%>"></script>

    <link rel="stylesheet" href="<%: Url.Content("~/Scripts/ThridParty/fancybox/helpers/jquery.fancybox-thumbs.css?v=2.0.6")%>" type="text/css" media="screen" />
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/ThridParty/fancybox/helpers/jquery.fancybox-thumbs.js?v=2.0.6")%>"></script>
    <script type="text/javascript">
        $(function () {
            $('.screen a').fancybox();
        });
    </script>
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
<h2>Edit Screen</h2>
<form action="" method="post" enctype="multipart/form-data">
<div class="screen">
    <a href="/Screens/<%= Model.View.Id %><%= Model.View.FileExtention %>"><img title="Screen" width="320" src="/Screens/<%= Model.View.Id %><%= Model.View.FileExtention %>" /></a>
</div>
<% Html.RenderPartial("Forms/ScreenForm", Model.View); %>
<div style="clear:both;"></div>
</form>
</div>
</asp:Content>


