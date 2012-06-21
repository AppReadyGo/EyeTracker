<%@ Page Title="" Language="C#" 
MasterPageFile="~/Views/Shared/Analytics.Master" 
Inherits="ViewPage<ViewModelWrapper<AfterLoginMasterModel, AnalyticsMasterModel, EyeTracker.Model.Pages.Analytics.UsageModel>>" %>

<asp:Content ContentPlaceHolderID="PageTitleContent" runat="server">Usage</asp:Content>

<asp:Content ContentPlaceHolderID="HeaderContent" runat="server">
<script src="<%: Url.Content("~/Scripts/ThridParty/DateFormat.js")%>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/ThridParty/Flot/jquery.flot.min.js")%>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/ThridParty/Flot/jquery.flot.resize.min.js")%>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/ThridParty/jquery-ui.min.js")%>" type="text/javascript"></script>
<link href="<%: Url.Content("~/Content/themes/cupertino/jquery-ui.css") %>" rel="stylesheet" type="text/css" />
<link href="<%: Url.Content("~/Content/shared/filter.css")%>" rel="stylesheet" type="text/css" />
<script src="<%: Url.Content("~/Scripts/filter.js")%>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/PortfolioUsage.js")%>" type="text/javascript"></script>
<script type="text/javascript">
    var chartData = <%= Model.View.UsageChartData %>;
</script>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<h2 class="title">Usage</h2>
<% Html.RenderPartial("Filter", Model.View); %>
<div class="charts">
    <div id="charts_place_holder" style="height:300px;"></div>
</div>
</asp:Content>



