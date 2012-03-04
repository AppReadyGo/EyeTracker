<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<EyeTracker.Model.Pages.Portfolio.PortfolioModel>" %>
<table>
    <caption>Portfolio Information</caption>
    <tbody>
        <tr><td class="label"><%: Html.LabelFor(m => m.Description)%></td><td><%: Html.TextBoxFor(m => m.Description, new { MaxLength = 50, @class = "name" })%><br /><%: Html.ValidationMessageFor(m => m.Description)%></td></tr>
        <tr><td class="label"><%: Html.LabelFor(m => m.TimeZone)%></td><td><%: Html.DropDownListFor(m => m.TimeZone, Model.ViewData)%></td></tr>
    </tbody>
</table>
