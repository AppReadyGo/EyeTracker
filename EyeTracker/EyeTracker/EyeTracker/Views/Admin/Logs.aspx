<%@ Page Title="" Language="C#" 
MasterPageFile="~/Views/Shared/AfterLogin.Master" 
Inherits="ViewPage<ViewModelWrapper<AfterLoginMasterModel, EyeTracker.Model.Pages.Admin.LogsModel>>" %>
<asp:Content ContentPlaceHolderID="PageTitleContent" runat="server">Log On</asp:Content>
<asp:Content ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<%: Html.ActionLink("Clear Logs", "ClearLogs") %>

<% using (Html.BeginForm()) {%>
<div>
    <div><label>Search String: <%: Html.TextBoxFor(m => m.View.SearchStr)%></label>
    <label>Category: <%: Html.DropDownListFor(m => m.View.CategoryId, (IEnumerable<SelectListItem>)ViewBag.Categories)%></label>
    <label>Severity: <%: Html.DropDownListFor(m => m.View.Severity, (IEnumerable<SelectListItem>)ViewBag.Severities)%></label>
    <label>From: <%: Html.TextBoxFor(m => m.View.FromDate)%></label>
    <label>To: <%: Html.TextBoxFor(m => m.View.ToDate)%></label>
    <label>Process Id: <%: Html.TextBoxFor(m => m.View.ProcessId)%></label>
    <label>Thread Id: <%: Html.TextBoxFor(m => m.View.ThreadId)%></label></div>
</div>
<p>
    <input type="submit" value="Search" />
</p>
<%} %>
<textarea runat="server" ID="output" enableviewstate="false" wrap="false" readonly style="width:100%;height:600px;">
<%= ViewBag.Logs%>
</textarea>
</asp:Content>
