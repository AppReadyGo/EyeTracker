<%@ Control Language="C#" Inherits="ViewUserControl<EyeTracker.Model.Pages.Account.ResetPasswordModel>" %>
<h2>Reset Password</h2>
<p>Use the form below to reset your password. <br />New passwords are required to be a minimum of 6 characters in length.</p>

<% using (Html.BeginForm()) { %>
    <fieldset>                
        <%: Html.LabelFor(m => m.NewPassword) %> <%: Html.PasswordFor(m => m.NewPassword, new { autocomplete="off" })%>
        <div class="error"><%: Html.ValidationMessageFor(m => m.NewPassword) %></div>
                
        <%: Html.LabelFor(m => m.ConfirmPassword) %> <%: Html.PasswordFor(m => m.ConfirmPassword, new { autocomplete = "off" })%>
        <div class="error"><%: Html.ValidationMessageFor(m => m.ConfirmPassword) %></div>
        <div class="error"><%: Html.ValidationSummary(true) %></div>
    </fieldset>
	<input type="submit" value="Reset Password" class="button green medium"/>
<% } %>
