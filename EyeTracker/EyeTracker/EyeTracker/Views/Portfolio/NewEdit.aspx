<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TopMenu.Master" Inherits="System.Web.Mvc.ViewPage<EyeTracker.Model.PortfolioModel>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server"><%:ViewBag.Title %> Portfolio</asp:Content>
<asp:Content ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="../../Content/Form.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server"><%:ViewBag.Title %> Portfolio</asp:Content>
<asp:Content ContentPlaceHolderID="TopMenu" runat="server">
    <%:Html.ActionLink("All Portfolios", "") %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="form">
<% using (Html.BeginForm()) {%>
    <fieldset>
        <legend>Portfolio Information</legend>
    <%: Html.ValidationSummary(true, "Account creation was unsuccessful. Please correct the errors and try again.")%>
        <div><%: Html.LabelFor(m => m.Description)%> <%: Html.TextBoxFor(m => m.Description)%></div>
        <div><%: Html.ValidationMessageFor(m => m.Description)%></div>
        <div><%: Html.LabelFor(m => m.CountryId)%> <%: Html.DropDownListFor(m => m.CountryId, (IEnumerable<SelectListItem>)ViewData["CountriesList"]) %></div>
        <p><input type="submit" value="Create" /></p>
    </fieldset>
<% }%>
</div>
</asp:Content>

