<%@ Page Title="" Language="C#" MasterPageFile="Analytics.master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">Usage</asp:Content>
<asp:Content ContentPlaceHolderID="HeaderContent" runat="server">
<script src="<%: Url.Content("~/Scripts/ThridParty/DateFormat.js")%>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/ThridParty/Flot/jquery.flot.min.js")%>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/ThridParty/Flot/jquery.flot.resize.min.js")%>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/PortfolioUsage.js")%>" type="text/javascript"></script>
<script type="text/javascript">
    var chartData = <%= ViewBag.ChartInitData %>;
</script>
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server">Usage</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="charts">
    <div id="charts_place_holder" style="height:300px;"></div>
</div>
</asp:Content>



