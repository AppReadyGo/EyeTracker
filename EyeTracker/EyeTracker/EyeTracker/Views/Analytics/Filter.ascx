<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<EyeTracker.Model.Filter.FilterModel>" %>

<div class="filter">
    <div class="title">
        <ul>
            <li><span>Portfolios:</span></li>
            <li><select id="ddlPortfolios" name="ddlPortfolios"><option>Portfolio 1</option></select></li>
            <li><span>Report:</span></li>
            <li><select id="ddlApplications" name="ddlApplications"><option>All applications</option></select></li>
            <li><a class="button"><span class="icon"></span>Filter</a></li>
            <li><a class="button"><span class="icon"></span>Show</a></li>
        </ul>
    </div>
    <div class="body">
        <p><% Html.RenderPartial("Date", Model.Date); %></p>
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

