<%@ Page Title="" Language="C#" 
MasterPageFile="~/Views/Shared/Analytics.Master" 
Inherits="ViewPage<ViewModelWrapper<AfterLoginMasterModel, AnalyticsMasterModel, EyeTracker.Model.Pages.Analytics.DashboardModel>>" %>

<asp:Content ContentPlaceHolderID="PageTitleContent" runat="server">Dashboard</asp:Content>

<asp:Content ContentPlaceHolderID="HeaderContent" runat="server">
<script src="<%: Url.Content("~/Scripts/ThridParty/jquery-ui.min.js")%>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/ThridParty/DateFormat.js")%>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/ThridParty/Flot/jquery.flot.min.js")%>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/ThridParty/Flot/jquery.flot.resize.min.js")%>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/analytics.dashboard.js")%>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/analytics.selector.js")%>" type="text/javascript"></script>
<link href="<%: Url.Content("~/Content/themes/cupertino/jquery-ui.css") %>" rel="stylesheet" type="text/css" />
<link href="<%: Url.Content("~/Content/shared/filter.css")%>" rel="stylesheet" type="text/css" />
<script src="<%: Url.Content("~/Scripts/filter.js")%>" type="text/javascript"></script>
<link href="<%: Url.Content("~/Content/analytics.dashboard.css")%>" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    var usageChartData = <%= Model.View.UsageChartData %>;
</script>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<% Html.RenderPartial("Filter", Model.View); %>
<table class="dashboard">
<tr>
    <td colspan="2">
        <div class="charts">
            <div class="title"><span>Usage</span></div>
            <div id="usage_charts_place_holder" style="height:200px;width:875px;"></div>
        </div>
    </td>
</tr><tr>  
    <td>
        <div class="title"><span>Visitors</span></div>
        <div id="visitors_charts_place_holder" style="height:200px;width:437px;"></div>
    </td>
    <td>
        <div class="title"><span>Map Overlay</span></div>
        <img src="/Content/img/world_map.jpg" class="item" />
    </td>
</tr><tr>  
    <td>
        <div class="title"><span>Trafic Sources</span></div>
        <div class="item"></div>
    </td>
    <td>
        <div class="title"><span>Content Overview</span></div>
        <table>
            <thead><tr><th>Path</th><th>Views</th><th>% Views</th></tr></thead>
            <tbody>
            <tr><td></td><td></td><td></td></tr>
            <tr><td></td><td></td><td></td></tr>
            <tr><td></td><td></td><td></td></tr>
            <tr><td></td><td></td><td></td></tr>
            <tr><td></td><td></td><td></td></tr>
            </tbody>
        </table>
        <div class="item"></div>
    </td>
</tr>
</table>
</asp:Content>



