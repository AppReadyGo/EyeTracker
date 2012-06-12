<%@ Control Language="C#" Inherits="ViewUserControl<EyeTracker.Models.Account.RegisterModel>" %>
<% using (Html.BeginForm()) { %>
    <h2>Account Information</h2>
    <p>Passwords are required to be a minimum of 6 characters in length.</p>
    <fieldset>
	    <div class="field text">
            <%: Html.LabelFor(m => m.Email) %> <%: Html.TextBoxFor(m => m.Email, new { autocomplete="off" })%>
            <div class="error"><%: Html.ValidationMessageFor(m => m.Email) %></div>
        </div>
	    <div class="field text">
            <%: Html.LabelFor(m => m.Password) %><%: Html.PasswordFor(m => m.Password, new { autocomplete = "off" })%>
            <div class="error"><%: Html.ValidationMessageFor(m => m.Password) %></div>
        </div>
	    <div class="field text">
            <%: Html.LabelFor(m => m.ConfirmPassword) %><%: Html.PasswordFor(m => m.ConfirmPassword, new { autocomplete = "off" })%>
            <div class="error"><%: Html.ValidationMessageFor(m => m.ConfirmPassword) %></div>
        </div>
        <div class="error"><%: Html.ValidationSummary(true) %>
	    <div class="alignright"><a href="#" class="link2" onClick="$(this).closest('form').submit();"><span><span>Register</span></span></a></div>
    </fieldset>
<% } %>