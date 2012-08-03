<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<EyeTracker.Model.Pages.Application.ScreenModel>" %>
<%: Html.HiddenFor(m => m.ApplicationId) %>
<%: Html.HiddenFor(m => m.FileExtention) %>
<table>
    <tbody>
        <tr><td class="label"><label for="file">Filename:</label></td><td><input type="file" name="file" id="file" /><br /><%=Html.ValidationMessage("file")%> </td></tr>
        <tr><td class="label"><%: Html.LabelFor(m => m.Path)%></td><td><%: Html.TextBoxFor(m => m.Path, new { MaxLength = 256, @class = "name" })%><br /><%: Html.ValidationMessageFor(m => m.Path)%></td></tr>
        <tr><td class="label"><%: Html.LabelFor(m => m.Width)%></td><td><%: Html.TextBoxFor(m => m.Width, new { MaxLength = 5 })%><br /><%: Html.ValidationMessageFor(m => m.Width)%></td></tr>
        <tr><td class="label"><%: Html.LabelFor(m => m.Height)%></td><td><%: Html.TextBoxFor(m => m.Height, new { MaxLength = 5 })%><br /><%: Html.ValidationMessageFor(m => m.Height)%></td></tr>
        <tr><td colspan="2" class="actions"><a class="link2" onclick="$(this).closest('form').submit();"><span><span>Save</span></span></a>&nbsp;<a class="link4" href="/Application/Screens/<%: Model.ApplicationId %>"><span><span>Cancel</span></span></a></td></tr>
    </tbody>
</table>
