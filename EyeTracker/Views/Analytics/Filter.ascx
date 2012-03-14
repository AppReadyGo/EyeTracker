<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<EyeTracker.Model.Filter.FilterModel>" %>
<script type="text/javascript">
    var analytics = {
        dateFrom: '<%=Model.Date.DateFrom.ToString("dd MMM yyyy") %>',
        dateFromMin: '<%=DateTime.UtcNow.AddYears(-20).ToString("dd MMM yyyy") %>',
        dateFromMax: '<%=Model.Date.DateTo.ToString("dd MMM yyyy") %>',
        dateTo: '<%=Model.Date.DateTo.ToString("dd MMM yyyy") %>',
        dateToMin: '<%=Model.Date.DateFrom.ToString("dd MMM yyyy") %>',
        dateToMax: '<%=DateTime.UtcNow.ToString("dd MMM yyyy") %>',
        pApps: <%=Model.PortfoliosData %>
    };
    $(document).ready(function(){
        $('#portfolioId').change(function(){
            var id = $(this).val();
            $('#applicationId').empty();
            $('#applicationId').append('<option value="0">All Applications</option>');
            var apps = analytics.pApps[id];
            for(var i=0;i<apps.length;i++){
                $('#applicationId').append('<option value="'+apps[i].id+'">'+apps[i].desc+'</option>');
            } 
        });
        $('#portfolioId').change();
        $('#applicationId').val(<%=Model.AppId %>);
    });
</script>
<form id="filter_form" action="/Analytics/<%: Model.FormAction %>" method="post">
<div class="filter">
    <div class="title">
        <ul>
            <li><span>Portfolios:</span></li>
            <li><%= Html.DropDownList("portfolioId", Model.Portfolios) %></li>
            <li><span>Report:</span></li>
            <li><select id="applicationId" name="applicationId"><option value="0">All applications</option></select></li>
            <li><a class="date-btn" id="date_from_lnk"><%= Model.Date.DateFrom.ToString("dd MMM yyyy")%></a> - <a class="date-btn" id="date_to_lnk"><%= Model.Date.DateTo.ToString("dd MMM yyyy")%></a></li>
            <li><a class="button"><span class="icon"></span>Filter</a></li>
            <li><a class="button"><span class="icon"></span>Refresh</a></li>
        </ul>
    </div>
    <div class="body"><a id="cancel_btn" class="cancel-btn"></a>
        <div>
            <fieldset>
                <legend>Date range</legend>
                <span id="datepicker_from"></span>
                <span id="datepicker_to"></span>
                <div>
                    <label>Preset range: <select id="preset_range">
                        <option value="custom">Custom</option>
                        <option value="today">Today</option>
                        <option value="yesterday">Yesterday</option>
                        <option value="lastweek">Last week</option>
                        <option value="lastmonth">Last month</option>
                    </select></label>
                </div>
                <div class="input-wrapper">
                <input id="date_from" name="fromDate" value="<%= Model.Date.DateFrom.ToString("dd MMM yyyy")%>" /> - <input id="date_to" name="toDate" value="<%= Model.Date.DateTo.ToString("dd MMM yyyy")%>"/>
                </div>
            </fieldset>

        </div>
        <div class="actions"><a class="button" onclick="javascript:$('#filter_form').submit()">Apply</a></div>
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
</form>


