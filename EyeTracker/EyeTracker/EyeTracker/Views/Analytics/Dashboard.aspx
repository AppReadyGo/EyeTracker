<%@ Page Title="" Language="C#" MasterPageFile="Analytics.master" Inherits="System.Web.Mvc.ViewPage<EyeTracker.Model.ViewModelSubMaterWrapper<EyeTracker.Model.Master.AfterLoginViewModel, EyeTracker.Model.Master.AnalyticsModel, EyeTracker.Model.Pages.Analytics.DashboardModel>>" %>

<asp:Content ContentPlaceHolderID="PageTitleContent" runat="server">Dashboard</asp:Content>


<asp:Content ContentPlaceHolderID="HeaderContent" runat="server">
<script src="<%: Url.Content("~/Scripts/ThridParty/DateFormat.js")%>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/ThridParty/Flot/jquery.flot.min.js")%>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/ThridParty/Flot/jquery.flot.resize.min.js")%>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/PortfolioDashboard.js")%>" type="text/javascript"></script>
<link href="<%: Url.Content("~/Content/shared/dashboard.css")%>" rel="stylesheet" type="text/css" />
<link href="<%: Url.Content("~/Content/Portfolio.Dashboard.css")%>" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    var usageChartData = <%= Model.View.UsageInitData %>;
</script>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
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



