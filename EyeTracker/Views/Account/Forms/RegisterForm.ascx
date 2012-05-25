<%@ Control Language="C#" Inherits="ViewUserControl<EyeTracker.Models.Account.RegisterModel>" %>
<% using (Html.BeginForm()) { %>
    <h3>Account Information</h3>
    <p>Passwords are required to be a minimum of <%: ViewData["PasswordLength"] %> characters in length.</p>
                                
    <div class="editor-label">
        <%: Html.LabelFor(m => m.Email) %>
    </div>
    <div class="editor-field">
        <%: Html.TextBoxFor(m => m.Email, new { autocomplete="off" })%>
        <%: Html.ValidationMessageFor(m => m.Email) %>
    </div>
                
    <div class="editor-label">
        <%: Html.LabelFor(m => m.Password) %>
    </div>
    <div class="editor-field">
        <%: Html.PasswordFor(m => m.Password, new { autocomplete = "off" })%>
        <%: Html.ValidationMessageFor(m => m.Password) %>
    </div>
                
    <div class="editor-label">
        <%: Html.LabelFor(m => m.ConfirmPassword) %>
    </div>
    <div class="editor-field">
        <%: Html.PasswordFor(m => m.ConfirmPassword, new { autocomplete = "off" })%>
        <%: Html.ValidationMessageFor(m => m.ConfirmPassword) %>
    </div>               
    <%: Html.ValidationSummary(true) %>
    <p>
        <input type="submit" value="Register" />
    </p>
<% } %>