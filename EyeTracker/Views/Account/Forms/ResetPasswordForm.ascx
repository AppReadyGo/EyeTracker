<%@ Control Language="C#" Inherits="ViewUserControl<EyeTracker.Model.Pages.Account.ResetPasswordModel>" %>
    <h2>Reset Password</h2>
    <p>Use the form below to reset your password. <br />New passwords are required to be a minimum of <%: ViewData["PasswordLength"] %> characters in length.</p>

    <% using (Html.BeginForm()) { %>
        <%: Html.ValidationSummary(true, "Password change was unsuccessful. Please correct the errors and try again.") %>
        <fieldset>                
	        <div class="field text">
                <%: Html.LabelFor(m => m.NewPassword) %> <%: Html.PasswordFor(m => m.NewPassword, new { autocomplete="off" })%>
                <div class="error"><%: Html.ValidationMessageFor(m => m.NewPassword) %></div>
            </div>
                
	        <div class="field text">
                <%: Html.LabelFor(m => m.ConfirmPassword) %> <%: Html.PasswordFor(m => m.ConfirmPassword, new { autocomplete = "off" })%>
                <div class="error"><%: Html.ValidationMessageFor(m => m.ConfirmPassword) %></div>
            </div>
	        <div class="alignright"><a href="#" class="link2" onClick="$(this).closest('form').submit();"><span><span>Reset Password</span></span></a></div>
        </fieldset>
    <% } %>
