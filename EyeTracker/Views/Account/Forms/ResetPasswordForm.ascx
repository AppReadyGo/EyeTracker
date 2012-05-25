<%@ Control Language="C#" Inherits="ViewUserControl<EyeTracker.Models.Account.ResetPasswordModel>" %>
    <h2>Reset Password</h2>
    <p>
        Use the form below to reset your password. 
    </p>
    <p>
        New passwords are required to be a minimum of <%: ViewData["PasswordLength"] %> characters in length.
    </p>

    <% using (Html.BeginForm()) { %>
        <%: Html.ValidationSummary(true, "Password change was unsuccessful. Please correct the errors and try again.") %>
        <div>
            <fieldset>                
                <div class="editor-label">
                    <%: Html.LabelFor(m => m.NewPassword) %>
                </div>
                <div class="editor-field">
                    <%: Html.PasswordFor(m => m.NewPassword) %>
                    <%: Html.ValidationMessageFor(m => m.NewPassword) %>
                </div>
                
                <div class="editor-label">
                    <%: Html.LabelFor(m => m.ConfirmPassword) %>
                </div>
                <div class="editor-field">
                    <%: Html.PasswordFor(m => m.ConfirmPassword) %>
                    <%: Html.ValidationMessageFor(m => m.ConfirmPassword) %>
                </div>
                
                <p>
                    <input type="submit" value="Reset Password" />
                </p>
            </fieldset>
        </div>
    <% } %>
