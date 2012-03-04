<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<EyeTracker.Model.Filter.DateModel>" %>
<script type="text/javascript">
    var analytics = {
        dateFrom: '<%=Model.DateFrom.ToString("dd MMM yyyy") %>',
        dateFromMin: '<%=DateTime.UtcNow.AddYears(-20).ToString("dd MMM yyyy") %>',
        dateFromMax: '<%=Model.DateTo.ToString("dd MMM yyyy") %>',
        dateTo: '<%=Model.DateTo.ToString("dd MMM yyyy") %>',
        dateToMin: '<%=Model.DateFrom.ToString("dd MMM yyyy") %>',
        dateToMax: '<%=DateTime.UtcNow.ToString("dd MMM yyyy") %>',
        pid: <%: Model.PortfolioId %>
    };
</script>
<a id="date_btn"><%= Model.DateFrom.ToString("dd MMM yyyy")%> - <%= Model.DateTo.ToString("dd MMM yyyy")%></a>
<div id="date_panel" class="date-panel">
    <div id="datepicker_to"></div><div id="datepicker_from"></div>
    <div class="actions"><a id="cance_btn">Cancel</a><a id="apply_btn">Apply</a></div>
</div>
