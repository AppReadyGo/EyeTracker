<%@ Control Language="C#" Inherits="ViewUserControl<EyeTracker.Models.Account.LogOnModel>" %>
<h2>Log On</h2>
<p class="subtitle">
    Please enter your username and password. 
</p>
<% using (Html.BeginForm()) { %>
<%= ViewData["SpecialMessage"] %>    
    <fieldset>       
        <%: Html.LabelFor(m => m.UserName) %> <%: Html.TextBoxFor(m => m.UserName)%>
        <div class="error"><%: Html.ValidationMessageFor(m => m.UserName)%></div>
        <%: Html.LabelFor(m => m.Password)%> <%: Html.PasswordFor(m => m.Password)%>
        <div class="error"><%: Html.ValidationMessageFor(m => m.Password)%></div>
        <div class="error"><%: Html.ValidationSummary(true) %></div>
        <div class="remember-me"><label><%: Html.CheckBoxFor(m => m.RememberMe)%> Remember Me</label><%: Html.ActionLink("Forgot Password ?", "ForgotPassword") %></div>
    </fieldset>
	<div class="actions">
        <input type="submit" value="Log in" class="button green medium" id="btn_login"/> or <%: Html.ActionLink("Register", "Register") %>
    </div>
    <div class="clear"></div>
<% } %>