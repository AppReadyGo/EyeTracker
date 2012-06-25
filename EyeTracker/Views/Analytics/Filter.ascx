<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<EyeTracker.Model.Filter.FilterModel>" %>
<script type="text/javascript">
    var analytics = {
        dateFrom: '<%=Model.DateFrom.ToString("dd MMM yyyy") %>',
        dateFromMin: '<%=DateTime.UtcNow.AddYears(-20).ToString("dd MMM yyyy") %>',
        dateFromMax: '<%=Model.DateTo.ToString("dd MMM yyyy") %>',
        dateTo: '<%=Model.DateTo.ToString("dd MMM yyyy") %>',
        dateToMin: '<%=Model.DateFrom.ToString("dd MMM yyyy") %>',
        dateToMax: '<%=DateTime.UtcNow.ToString("dd MMM yyyy") %>',
        pData: <%=Model.PortfoliosData %>,
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
        $('#applicationId').val(<%=Model.ApplicationId %>);
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
                <h2>Date range</h2>
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
                <h2>Filter</h2>
                <label>Portfolio: <%= Html.DropDownList("portfolioId", Model.Portfolios) %></label>
                <label>Application: <%= Html.DropDownList("applicationId", Model.Applications) %></label>
                <label>Screen size <%= Html.DropDownList("screenSize", Model.ScreenSizes) %></label>
                <label>Path <%= Html.DropDownList("path", Model.Pathes) %></label>
                <!--label>OS <select><option>All</option></select></label>
                <label>Language <select><option>All</option></select></label>
                <label>Country <select><option>All</option></select></label>
                <label>City <select><option>All</option></select></label-->
            </div>
            <div class="actions"><a class="button" id="apply_btn">Apply</a><a class="link" id="cancel_btn">cancel</a></div>
        </div>
    </div>
</div>


