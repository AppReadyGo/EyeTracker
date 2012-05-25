<%@ Control Language="C#" Inherits="ViewUserControl<EyeTracker.Model.Pages.Account.ForgotPasswordModel>" %>
<% using (Html.BeginForm()) { %>
    <h3>Forgot Password</h3>
    <p>
        Use the form below to send email with reset password instructions. 
    </p>
                                
    <div class="editor-label">
        <%: Html.LabelFor(m => m.Email) %>
    </div>
    <div class="editor-field">
        <%: Html.TextBoxFor(m => m.Email, new { autocomplete="off" })%>
        <%: Html.ValidationMessageFor(m => m.Email) %>
    </div>
    <%: Html.ValidationSummary(true) %>
    <p>
        <input type="submit" value="Send" />
    </p>
<% } %>