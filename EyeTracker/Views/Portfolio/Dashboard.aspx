<%@ Page Title="" Language="C#" MasterPageFile="Analytics.master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="PageTitleContent" runat="server">Dashboard</asp:Content>
<asp:Content ContentPlaceHolderID="HeaderContent" runat="server">
<script src="<%: Url.Content("~/Scripts/ThridParty/DateFormat.js")%>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/ThridParty/Flot/jquery.flot.min.js")%>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/ThridParty/Flot/jquery.flot.resize.min.js")%>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/PortfolioDashboard.js")%>" type="text/javascript"></script>
<link href="<%: Url.Content("~/Content/Dashboard.css")%>" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    var usageChartData = <%= ViewBag.UsageInitData %>;
</script>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<table class="dashboard">
<tr>
    <td colspan="2">
        <div class="charts">
            <h3>Usage</h3>
            <div id="usage_charts_place_holder" style="height:200px;width:875px;"></div>
        </div>
    </td>
</tr><tr>  
    <td>
        <h3>Visitors</h3>
        <div id="visitors_charts_place_holder" style="height:200px;width:437px;"></div>
    </td>
    <td>
        <h3>Map Overlay</h3>
        <img src="/Content/img/world_map.jpg" class="item" />
    </td>
</tr><tr>  
    <td>
        <h3>Trafic Sources</h3>
        <div class="item"></div>
    </td>
    <td>
        <h3>Content Overview</h3>
        <table>
            <thead><tr><th>Path</th><th>Views</th><th>% Views</th></tr></thead>
            <tr><td></td><td></td><td></td></tr>
            <tr><td></td><td></td><td></td></tr>
            <tr><td></td><td></td><td></td></tr>
            <tr><td></td><td></td><td></td></tr>
            <tr><td></td><td></td><td></td></tr>
        </table>
        <div class="item"></div>
    </td>
</tr>
</table>
</asp:Content>



