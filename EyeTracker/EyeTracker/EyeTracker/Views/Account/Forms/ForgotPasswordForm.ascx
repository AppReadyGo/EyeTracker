<%@ Control Language="C#" Inherits="ViewUserControl<EyeTracker.Model.Pages.Account.ForgotPasswordModel>" %>
<h2>Forgot Password</h2>
<p>
    Use the form below to send email with reset password instructions. 
</p>
<% using (Html.BeginForm()) { %>
    <fieldset>
        <%: Html.LabelFor(m => m.Email) %><%: Html.TextBoxFor(m => m.Email, new { autocomplete="off" })%>
        <div class="error"><%: Html.ValidationMessageFor(m => m.Email) %></div>
        <div class="error"><%: Html.ValidationSummary(true) %></div>
    </fieldset>
	<input type="submit" value="Send" class="button green medium"/>
<% } %>
