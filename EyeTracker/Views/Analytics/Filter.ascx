<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<EyeTracker.Model.Filter.FilterModel>" %>
<script type="text/javascript">
    var analytics = {
        dateFrom: '<%=Model.Date.DateFrom.ToString("dd MMM yyyy") %>',
        dateFromMin: '<%=DateTime.UtcNow.AddYears(-20).ToString("dd MMM yyyy") %>',
        dateFromMax: '<%=Model.Date.DateTo.ToString("dd MMM yyyy") %>',
        dateTo: '<%=Model.Date.DateTo.ToString("dd MMM yyyy") %>',
        dateToMin: '<%=Model.Date.DateFrom.ToString("dd MMM yyyy") %>',
        dateToMax: '<%=DateTime.UtcNow.ToString("dd MMM yyyy") %>',
    };
</script>
<div class="filter">
    <div class="title">
        <ul>
            <li><span>Portfolios:</span></li>
            <li><select id="ddlPortfolios" name="ddlPortfolios"><option>Portfolio 1</option></select></li>
            <li><span>Report:</span></li>
            <li><select id="ddlApplications" name="ddlApplications"><option>All applications</option></select></li>
            <li><a class="date-btn"><%= Model.Date.DateFrom.ToString("dd MMM yyyy")%> - <%= Model.Date.DateTo.ToString("dd MMM yyyy")%></a></li>
            <li><a class="button"><span class="icon"></span>Filter</a></li>
            <li><a class="button"><span class="icon"></span>Refresh</a></li>
        </ul>
    </div>
    <div class="body"><a id="cancel_btn" class="cancel-btn"></a>
        <div><div id="datepicker_to"></div><div id="datepicker_from"></div></div>
        <div class="actions"><a id="apply_btn">Apply</a></div>
    </div>
<%--    <%
    if (Model.ShowDateSelector)
    {
        Html.RenderPartial("Date", Model.Date);
    }
    if (Model.ShowApplicationsSelector)
    {
        Html.RenderPartial("Selector", Model.Applications);
    }
    if (Model.ShowScreenSizesSelector)
    {
        Html.RenderPartial("Selector", Model.ScreenSizes);
    } 
    %>--%>
<%--    <% Html.RenderPartial("Selector", ViewData["Pathes"]); %>
    <% Html.RenderPartial("Selector", ViewData["Languages"]); %>
--%>    <!--1. Usage: Application (All), screen size (All), path (All), language (All), OS (All), location (All)  -->
    <!--2. Fingerprint: Application (All), screen size (All), path (All), language (All), OS (All), location (All)  -->
    <!--3. Eyetracker: Application (All), screen size (All), path (All), language (All), OS (All), location (All)  -->
</div>

