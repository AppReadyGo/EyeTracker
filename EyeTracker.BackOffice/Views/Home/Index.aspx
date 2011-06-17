<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<% using (Html.BeginForm()) { %>
<p><label for="searchStr">Search String: </label><%: Html.TextBox("searchStr", ViewData["searchStr"]) %>
<label for="categoriesList">Category: </label><%: Html.DropDownList("categoriesList", (IEnumerable<SelectListItem>)ViewData["categoriesList"])%>
<label for="severityList">Severity: </label><%: Html.DropDownList("severityList", (IEnumerable<SelectListItem>)ViewData["severityList"])%>
</p><p><label for="fromDate">From: </label><%: Html.TextBox("fromDate", ViewData["fromDate"])%>
<label for="toDate">To: </label><%: Html.TextBox("toDate", ViewData["toDate"])%>
<label for="processId">Process Id: </label><%: Html.TextBox("processId", ViewData["processId"])%>
<label for="threadId">Thread Id: </label><%: Html.TextBox("threadId", ViewData["threadId"])%>
</p><input type="submit" value="Search" />
<% } %>
<p id="errorMessage" style="color:Red;"><%: ViewData["errorMessage"]%></p>
<textarea runat="server" ID="output" enableviewstate="false" wrap="false" readonly style="width:100%;height:600px;"><%: ViewData["output"]%></textarea>
<span style="float:right;"><%: Html.ActionLink("Clear Log", "Clear")%></span>
</asp:Content>
