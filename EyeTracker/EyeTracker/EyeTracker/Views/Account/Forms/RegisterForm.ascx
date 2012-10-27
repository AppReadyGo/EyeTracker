<%@ Control Language="C#" Inherits="ViewUserControl<EyeTracker.Models.Account.RegisterModel>" %>
<h2>Sign Up, Free</h2>
<p class="subtitle">Passwords are required to be a minimum of 6 characters in length.</p>
<% using (Html.BeginForm()) { %>
    <fieldset>
        <%: Html.LabelFor(m => m.Email) %> <%: Html.TextBoxFor(m => m.Email, new { autocomplete="off" })%>
        <div class="error"><%: Html.ValidationMessageFor(m => m.Email) %></div>
        <%: Html.LabelFor(m => m.Password) %><%: Html.PasswordFor(m => m.Password, new { autocomplete = "off" })%>
        <div class="error"><%: Html.ValidationMessageFor(m => m.Password) %></div>
        <%: Html.LabelFor(m => m.ConfirmPassword) %><%: Html.PasswordFor(m => m.ConfirmPassword, new { autocomplete = "off" })%>
        <div class="error"><%: Html.ValidationMessageFor(m => m.ConfirmPassword) %></div>
        <div class="error"><%: Html.ValidationSummary(true) %></div>
    </fieldset>
    <input type="submit" value="Register" class="button green medium" id="btn_register"/>
<% } %>