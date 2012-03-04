<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<EyeTracker.Model.FilterModel>" %>

<div class="filter">
    <%
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
    %>
<%--    <% Html.RenderPartial("Selector", ViewData["Pathes"]); %>
    <% Html.RenderPartial("Selector", ViewData["Languages"]); %>
--%>    <!--1. Usage: Application (All), screen size (All), path (All), language (All), OS (All), location (All)  -->
    <!--2. Fingerprint: Application (All), screen size (All), path (All), language (All), OS (All), location (All)  -->
    <!--3. Eyetracker: Application (All), screen size (All), path (All), language (All), OS (All), location (All)  -->
    <div style="clear:both;"></div>
</div>

