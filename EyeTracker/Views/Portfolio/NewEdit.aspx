<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/AfterLogin.Master" Inherits="System.Web.Mvc.ViewPage<EyeTracker.Model.PortfolioModel>" %>

<asp:Content ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="<%: Url.Content("~/Content/Form.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%: Url.Content("~/Content/ProfileNewEdit.css")%>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<div class="form">
<% using (Html.BeginForm()) {%>
    <%: Html.HiddenFor(m => m.Id) %>
    <fieldset>
        <legend>Portfolio Information</legend>
        <%: Html.ValidationSummary(true, "Account creation was unsuccessful. Please correct the errors and try again.")%>
        <div><%: Html.LabelFor(m => m.Description)%> <%: Html.TextBoxFor(m => m.Description)%></div>
        <div><%: Html.ValidationMessageFor(m => m.Description)%></div>
        <div><%: Html.LabelFor(m => m.TimeZone) %><%: Html.DropDownListFor(m => m.TimeZone, new SelectList((IEnumerable<object>)ViewData["TimeZoneList"], "Id", "DisplayName"))%></div>
        <br />
        <p><input type="submit" value="Save" /></p>
    </fieldset>
<% }%>
</div>
</asp:Content>

