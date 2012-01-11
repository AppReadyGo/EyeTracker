﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/AfterLogin.Master" Inherits="System.Web.Mvc.ViewPage<EyeTracker.Model.PortfolioModel>" %>

<asp:Content ID="PageTitleContent" ContentPlaceHolderID="PageTitleContent" runat="server">Create Portfolio</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="<%: Url.Content("~/Content/shared/form.css")%>" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<h3 class="title">Create Portfolio</h3>
<% using (Html.BeginForm()) {%>
    <table>
        <caption>Portfolio Information</caption>
        <tbody>
            <tr><td colspan="2"><%: Html.ValidationSummary(true, "Portfolio creation was unsuccessful. Please correct the errors and try again.")%></td></tr>
            <tr><td><%: Html.LabelFor(m => m.Description)%></td><td><%: Html.TextBoxFor(m => m.Description)%><%: Html.ValidationMessageFor(m => m.Description)%></td></tr>
            <tr><td><%: Html.LabelFor(m => m.TimeZone) %></td><td><%: Html.DropDownListFor(m => m.TimeZone, new SelectList((IEnumerable<object>)ViewData["TimeZoneList"], "Id", "DisplayName"))%></td></tr>
        </tbody>
    </table>
    <p><input type="submit" value="Save" /></p>
<% }%>
</asp:Content>