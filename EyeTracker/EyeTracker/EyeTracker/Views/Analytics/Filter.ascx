﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<EyeTracker.Model.Filter.FilterModel>" %>
<script type="text/javascript">
    var analytics = {
        dateFrom: '<%=Model.DateFrom.ToString("dd MMM yyyy") %>',
        dateFromMin: '<%=DateTime.UtcNow.AddYears(-20).ToString("dd MMM yyyy") %>',
        dateFromMax: '<%=Model.DateTo.ToString("dd MMM yyyy") %>',
        dateTo: '<%=Model.DateTo.ToString("dd MMM yyyy") %>',
        dateToMin: '<%=Model.DateFrom.ToString("dd MMM yyyy") %>',
        dateToMax: '<%=DateTime.UtcNow.ToString("dd MMM yyyy") %>',
        pApps: <%=Model.PortfoliosData %>,
        action: '<%= Model.FormAction %>'
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
<div class="filter">
    <div class="title">
        <a id="filter_title">
            <ul>
                <li>Portfolio:</li>
                <li><%: Model.PortfolioName%></li>
                <li>Application:</li>
                <li><%: Model.ApplicationName%></li>
                <li><%= Model.DateFrom.ToString("dd MMM yyyy")%> - <%= Model.DateTo.ToString("dd MMM yyyy")%></li>
                <li><span class="button"><span class="icon"></span>Filter</span></li>
            </ul>
        </a>
    </div>
    <div class="body" id="filter_body"><a id="close_btn" class="cancel-btn"></a>
        <div id="date_range">
            <div>
                <h3>Date range</h3>
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
                <input id="date_from" name="fromDate" value="<%= Model.DateFrom.ToString("dd MMM yyyy")%>" /> - <input id="date_to" name="toDate" value="<%= Model.DateTo.ToString("dd MMM yyyy")%>"/>
                </div>
            </div>
        </div>
        <div id="advanced_filter">
            <div>
                <h3>Filter</h3>
                <label>Portfolio: <%= Html.DropDownList("portfolioId", Model.Portfolios) %></label>
                <label>Application: <%= Html.DropDownList("applicationId", Model.Applications) %></label>
                <label>Screen size <%= Html.DropDownList("screenSize", Model.ScreenSizes) %></label>
                <label>Path <%= Html.DropDownList("path", Model.Pathes) %></label>
                <label>OS <select><option>All</option></select></label>
                <label>Language <select><option>All</option></select></label>
                <label>Country <select><option>All</option></select></label>
                <label>City <select><option>All</option></select></label>
            </div>
            <div class="actions"><a class="button" id="apply_btn">Apply</a><a class="link" id="cancel_btn">cancel</a></div>
        </div>
    </div>
<%--    <% Html.RenderPartial("Selector", ViewData["Pathes"]); %>
    <% Html.RenderPartial("Selector", ViewData["Languages"]); %>
--%>    <!--1. Usage: Application (All), screen size (All), path (All), language (All), OS (All), location (All)  -->
    <!--2. Fingerprint: Application (All), screen size (All), path (All), language (All), OS (All), location (All)  -->
    <!--3. Eyetracker: Application (All), screen size (All), path (All), language (All), OS (All), location (All)  -->
</div>

