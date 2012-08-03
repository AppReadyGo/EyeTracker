<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<EyeTracker.Model.Pages.Portfolio.PortfolioModel>" %>
<table>
    <caption>Portfolio Information</caption>
    <tbody>
        <tr><td class="label"><%: Html.LabelFor(m => m.Description)%></td><td><%: Html.TextBoxFor(m => m.Description, new { MaxLength = 100, @class = "name" })%><br /><%: Html.ValidationMessageFor(m => m.Description)%></td></tr>
    </tbody>
</table>
