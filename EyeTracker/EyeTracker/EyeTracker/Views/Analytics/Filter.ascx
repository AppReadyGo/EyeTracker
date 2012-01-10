<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<script type="text/javascript">
    var analytics = {
        dateFrom: '<%=ViewBag.FromDate.ToString("dd MMM yyyy") %>',
        dateFromMin: '<%=DateTime.UtcNow.AddYears(-20).ToString("dd MMM yyyy") %>',
        dateFromMax: '<%=ViewBag.ToDate.ToString("dd MMM yyyy") %>',
        dateTo: '<%=ViewBag.ToDate.ToString("dd MMM yyyy") %>',
        dateToMin: '<%=ViewBag.FromDate.ToString("dd MMM yyyy") %>',
        dateToMax: '<%=DateTime.UtcNow.ToString("dd MMM yyyy") %>',
        type: '<%=ViewBag.Type%>',
        id: <%: ViewBag.Id %>
    };
</script>
<div class="filter">
    <a id="date_btn"><%= ViewBag.FromDate.ToString("dd MMM yyyy") %> - <%= ViewBag.ToDate.ToString("dd MMM yyyy") %></a>
    <div id="date_panel" class="date-panel">
        <div id="datepicker_to"></div><div id="datepicker_from"></div>
        <div class="actions"><a id="cance_btn">Cancel</a><a id="apply_btn">Apply</a></div>
    </div>
    <% Html.RenderPartial("Selector", ViewData["Applications"]); %>
    <% Html.RenderPartial("Selector", ViewData["ScreenSizes"]); %>
<%--    <% Html.RenderPartial("Selector", ViewData["Pathes"]); %>
    <% Html.RenderPartial("Selector", ViewData["Languages"]); %>
--%>    <!--1. Usage: Application (All), screen size (All), path (All), language (All), OS (All), location (All)  -->
    <!--2. Fingerprint: Application (All), screen size (All), path (All), language (All), OS (All), location (All)  -->
    <!--3. Eyetracker: Application (All), screen size (All), path (All), language (All), OS (All), location (All)  -->
    <div style="clear:both;"></div>
</div>

