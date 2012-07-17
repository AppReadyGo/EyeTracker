<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<EyeTracker.Model.Pages.Admin.LogsModel>" %>
<% using (Html.BeginForm()) {%>
<div>
    <div><label>Search String: <%: Html.TextBoxFor(m => m.SearchStr)%></label>
    <label>Category: <%: Html.DropDownListFor(m => m.CategoryId, (IEnumerable<SelectListItem>)ViewBag.Categories)%></label>
    <label>Severity: <%: Html.DropDownListFor(m => m.Severity, (IEnumerable<SelectListItem>)ViewBag.Severities)%></label>
    <label>From: <%: Html.TextBoxFor(m => m.FromDate)%></label>
    <label>To: <%: Html.TextBoxFor(m => m.ToDate)%></label>
    <label>Process Id: <%: Html.TextBoxFor(m => m.ProcessId)%></label>
    <label>Thread Id: <%: Html.TextBoxFor(m => m.ThreadId)%></label><label>Current Server Time: <%: DateTime.UtcNow.ToString() %></label></div>
</div>
<p>
    <a href="/Admin/ClearLogs" class="link2 btn-clear"><span><span>Clear Logs</span></span></a>
    <a href="#" class="link2" onClick="$(this).closest('form').submit();"><span><span>Search</span></span></a>
</p>
<%} %>