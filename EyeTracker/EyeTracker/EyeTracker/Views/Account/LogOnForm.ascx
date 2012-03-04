<%@ Control Language="C#" Inherits="ViewUserControl<EyeTracker.Models.Account.LogOnModel>" %>
<% using (Html.BeginForm()) { %>
    <%: Html.ValidationSummary(true, "Login was unsuccessful. Please correct the errors and try again.") %>
        <h3>Log On</h3>
        <p>
            Please enter your username and password. 
        </p>
               
        <div class="editor-label">
            <%: Html.LabelFor(m => m.UserName) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(m => m.UserName)%>
            <%: Html.ValidationMessageFor(m => m.UserName)%>
        </div>
                
        <div class="editor-label">
            <%: Html.LabelFor(m => m.Password)%>
        </div>
        <div class="editor-field">
            <%: Html.PasswordFor(m => m.Password)%>
            <%: Html.ValidationMessageFor(m => m.Password)%>
        </div>
                
        <div class="editor-label">
            <%: Html.CheckBoxFor(m => m.RememberMe)%>
            <%: Html.LabelFor(m => m.RememberMe)%>
        </div>
                
        <p>
            <input type="submit" value="Log On" />
        </p>
<% } %>