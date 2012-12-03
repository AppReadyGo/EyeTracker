<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<EyeTracker.Model.Filter.FilterModel>" %>
<script type="text/javascript">
    var analytics = {
        dateFrom: '<%=Model.SelectedDateFrom.ToString("dd MMM yyyy") %>',
        dateFromMin: '<%=DateTime.UtcNow.AddYears(-20).ToString("dd MMM yyyy") %>',
        dateFromMax: '<%=Model.SelectedDateTo.ToString("dd MMM yyyy") %>',
        dateTo: '<%=Model.SelectedDateTo.ToString("dd MMM yyyy") %>',
        dateToMin: '<%=Model.SelectedDateFrom.ToString("dd MMM yyyy") %>',
        dateToMax: '<%=DateTime.UtcNow.ToString("dd MMM yyyy") %>',
        pData: <%=Model.PortfoliosData %>,
        aid: <%=Model.SelectedApplicationId %>,
        pid: <%=Model.SelectedPortfolioId %>,
        aData: <%=Model.ApplicationsData %>,
        action: '<%= Model.FormAction %>'
    };
    $(document).ready(function(){
        $('#portfolioId').change(function(){
            var id = $(this).val();
            $('#applicationId').empty();
            $('#applicationId').append('<option value="0">All</option>');
            var data = analytics.pData[id];
            for(var i=0;i<data.length;i++){
                $('#applicationId').append('<option value="'+data[i].id+'">'+data[i].desc+'</option>');
            } 
        });
        $('#applicationId').change(function(){
            var id = $(this).val();
            $('#screenSize').empty();
            $('#screenSize').append('<option value="">All</option>');
            $('#path').empty();
            $('#path').append('<option value="">All</option>');
            if(id != '0'){
                var data = analytics.aData[id];
                for(var i=0;i<data.scr.length;i++){
                    $('#screenSize').append('<option value="'+data.scr[i]+'">'+data.scr[i]+'</option>');
                } 
                for(var i=0;i<data.pth.length;i++){
                    $('#path').append('<option value="'+data.pth[i]+'">'+data.pth[i]+'</option>');
                } 
            }
        });
        $('#portfolioId').change();
        $('#applicationId').val(<%=Model.SelectedApplicationId %>);
    });
</script>
<div class="date-range">
    <span id="date_range_btn" class="date-button"><%= Model.SelectedDateFrom.ToString("dd MMM yyyy")%> - <%= Model.SelectedDateTo.ToString("dd MMM yyyy")%></span>
    <div id="date_range_pnl" class="selector">
        <table>
            <tr>
                <td class="picker-wrapper">
                    <span id="datepicker_from"></span>
                </td>
                <td class="picker-wrapper">
                    <span id="datepicker_to"></span>
                </td>
                <td class="form-wrapper">
                    <p>
                        <label>
                            <strong>Preset range:</strong>
                            <select id="preset_range">
                                <option value="custom">Custom</option>
                                <option value="today">Today</option>
                                <option value="yesterday">Yesterday</option>
                                <option value="lastweek">Last week</option>
                                <option value="lastmonth">Last month</option>
                            </select></label>
                    </p>
                    <p class="input-wrapper">
                        <input id="date_from" name="fromDate" value="30 May 2012" />
                        -
                        <input id="date_to" name="toDate" value="29 Jun 2012" />
                    </p>
                    <p class="actions">
                        <a id="date_range_apply">Apply</a><a id="date_range_cancel">Cancel</a>
                    </p>
                </td>
            </tr>
        </table>
    </div>
</div>
<h2 class="title"><%:Model.Title%></h2>
<div class="filter">
    <div class="title">
        <ul>
            <%if(!string.IsNullOrEmpty(Model.PlaceHolderHTML)){ %>
            <li><%= Model.PlaceHolderHTML%></li>
            <%}%>
            <%if(!string.IsNullOrEmpty(Model.SelectedScreenSize)){ %>
            <li><strong>Size:</strong></li>
            <li><%: Model.SelectedScreenSize%></li>
            <%}%>
            <li><strong>Portfolio:</strong></li>
            <li><%: Model.PortfolioName%></li>
            <li><strong>Application:</strong></li>
            <li><%: Model.ApplicationName%></li>
            <li class="multiselect"><button id="advanced_filter_btn">Advanced filter</button></li>
        </ul>
    </div>
    <div class="advanced_filter" id="advanced_filter">
       <%--By PM <p>
            <label>
                <strong>Portfolio:</strong>
                <%= Html.DropDownList("SelectedPortfolioId", Model.Portfolios)%>
            </label>
            <label>
                <strong>Application:</strong>
                <%= Html.DropDownList("SelectedApplicationId", Model.Applications)%>
            </label>
        </p>--%>
        <p>
            <label>
                <strong>Screen size:</strong>
                <%= Html.DropDownList("SelectedScreenSize", Model.ScreenSizes)%>
            </label>
            <label>
                <strong>Path:</strong>
                <%= Html.DropDownList("SelectedPath", Model.Pathes)%>
            </label>
            <!--label>OS <select><option>All</option></select></label>
        <label>Language <select><option>All</option></select></label>
        <label>Country <select><option>All</option></select></label>
        <label>City <select><option>All</option></select></label-->
        </p>
        <p class="actions">
            <a id="advanced_filter_apply">Apply</a><a id="advanced_filter_cancel">Cancel</a></p>
    </div>
</div>
<div id="current_filter">
</div>

