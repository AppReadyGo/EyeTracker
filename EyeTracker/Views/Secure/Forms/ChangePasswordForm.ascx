<%@ Control Language="C#" Inherits="ViewUserControl<EyeTracker.Models.Account.ChangePasswordModel>" %>
<h2>Change Password</h2>
<p>New passwords are required to be a minimum of <%: ViewData["PasswordLength"] %> characters in length.</p>
<% using (Html.BeginForm()) { %>
<fieldset>                
    <div class="field text">
        <%: Html.LabelFor(m => m.OldPassword) %> <%: Html.PasswordFor(m => m.OldPassword)%>
        <div class="error"><%: Html.ValidationMessageFor(m => m.OldPassword) %></div>
    </div>         
    <div class="field text">
        <%: Html.LabelFor(m => m.NewPassword) %> <%: Html.PasswordFor(m => m.NewPassword) %>
        <div class="error"><%: Html.ValidationMessageFor(m => m.NewPassword) %></div>
    </div>         
    <div class="field text">
        <%: Html.LabelFor(m => m.ConfirmPassword) %> <%: Html.PasswordFor(m => m.ConfirmPassword) %>
        <div class="error"><%: Html.ValidationMessageFor(m => m.ConfirmPassword) %></div>
    </div>
    <div class="error"><%: Html.ValidationSummary(true, "Password change was unsuccessful. Please correct the errors and try again.") %></div>
    <div class="alignright"><a href="#" class="link2" onClick="$(this).closest('form').submit();"><span><span>Change Password</span></span></a></div>
</fieldset>
<% } %>
