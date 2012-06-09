<%@ Control Language="C#" Inherits="ViewUserControl<EyeTracker.Model.Pages.Account.ForgotPasswordModel>" %>
<% using (Html.BeginForm()) { %>
	<h2>Forgot Password</h2>
    <p>
        Use the form below to send email with reset password instructions. 
    </p>
    <fieldset>
	    <div class="field text">
            <%: Html.LabelFor(m => m.Email) %><%: Html.TextBoxFor(m => m.Email, new { autocomplete="off" })%>
            <%: Html.ValidationMessageFor(m => m.Email) %>
        </div>
        <%: Html.ValidationSummary(true) %>
	    <div class="alignright"><a href="#" class="link2" onClick="$(this).closest('form').submit();"><span><span>Send</span></span></a></div>
    </fieldset>
<% } %>
