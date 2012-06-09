<%@ Control Language="C#" Inherits="ViewUserControl<EyeTracker.Models.Account.LogOnModel>" %>
<% using (Html.BeginForm()) { %>
    <h2>Log On</h2>
    <p>
        Please enter your username and password. 
    </p>
               
    <fieldset>
	    <div class="field text">
            <%: Html.LabelFor(m => m.UserName) %> <%: Html.TextBoxFor(m => m.UserName)%>
            <div class="error"><%: Html.ValidationMessageFor(m => m.UserName)%></div>
        </div>
                
	    <div class="field text">
            <%: Html.LabelFor(m => m.Password)%> <%: Html.PasswordFor(m => m.Password)%>
            <div class="error"><%: Html.ValidationMessageFor(m => m.Password)%></div>
        </div>
        <div class="error"><%: Html.ValidationSummary(true, "Login was unsuccessful. Please correct the errors and try again.") %></div>
                
	    <div class="subactions">
            <span class="forgot-password"><%: Html.ActionLink("forgot password", "ForgotPassword") %></span>
            <%: Html.CheckBoxFor(m => m.RememberMe)%>
            <%: Html.LabelFor(m => m.RememberMe)%>
            <div style="clear:both;"></div>
        </div>
                
	    <div class="alignright"><a href="#" class="link2" onClick="$(this).closest('form').submit();"><span><span>LogOn</span></span></a></div>
    </fieldset>
<% } %>