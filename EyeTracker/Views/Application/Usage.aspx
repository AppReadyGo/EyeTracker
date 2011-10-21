<%@ Page Title="" Language="C#" MasterPageFile="Analytics.master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content4" ContentPlaceHolderID="Title" runat="server">Usage</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
<script src="<%: Url.Content("~/Scripts/ThridParty/DateFormat.js")%>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/ThridParty/Flot/jquery.flot.min.js")%>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/ThridParty/Flot/jquery.flot.resize.min.js")%>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/ApplicationUsage.js")%>" type="text/javascript"></script>
<script type="text/javascript">
    var chartData = <%= ViewBag.ChartInitData %>;
</script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">Usage</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="charts">
    <div id="charts_place_holder" style="height:300px;"></div>
</div>
</asp:Content>


