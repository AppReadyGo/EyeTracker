<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TopMenu.Master" Inherits="System.Web.Mvc.ViewPage<EyeTracker.Model.PortfolioModel>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server"><%:ViewBag.Title %> Portfolio</asp:Content>
<asp:Content ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="<%: Url.Content("~/Content/Form.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%: Url.Content("~/Content/ProfileNewEdit.css")%>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server"><%:ViewBag.Title %> Portfolio</asp:Content>
<asp:Content ContentPlaceHolderID="TopMenu" runat="server">
    <%:Html.ActionLink("All Portfolios", "") %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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

