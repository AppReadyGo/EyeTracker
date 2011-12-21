<%@ Page Title="" Language="C#" MasterPageFile="Analytics.master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="PageTitleContent" runat="server">Dashboard</asp:Content>
<asp:Content ContentPlaceHolderID="HeaderContent" runat="server">
<script src="<%: Url.Content("~/Scripts/ThridParty/DateFormat.js")%>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/ThridParty/Flot/jquery.flot.min.js")%>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/ThridParty/Flot/jquery.flot.resize.min.js")%>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/ApplicationDashboard.js")%>" type="text/javascript"></script>
<link href="<%: Url.Content("~/Content/Dashboard.css")%>" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    var chartData = <%= ViewBag.ChartInitData %>;
</script>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<table>
<tr>
    <td colspan="2">
        <div class="charts">
            <p>Usage</p>
            <div id="usage_charts_place_holder" style="height:200px;width:875px;"></div>
        </div>
    </td>
</tr><%--<tr>  
    <td>
        <p>Visitors</p>
        <div id="visitors_charts_place_holder" style="height:200px;width:437px;"></div>
    </td>
    <td>
        <p>Map Overlay</p>
        <img src="/Content/img/world_map.jpg" class="item" />
    </td>
</tr><tr>  
    <td>
        <p>Trafic Sources</p>
        <div class="item"></div>
    </td>
    <td>
        <p>Content Overview</p>
        <table>
            <thead><tr><th>Path</th><th>Views</th><th>% Views</th></tr></thead>
            <tr><td></td><td></td><td></td></tr>
            <tr><td></td><td></td><td></td></tr>
            <tr><td></td><td></td><td></td></tr>
            <tr><td></td><td></td><td></td></tr>
            <tr><td></td><td></td><td></td></tr>
        </table>
        <div class="item"></div>
    </td></tr>--%>
</table></asp:Content>
