<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Home.Master" Inherits="System.Web.Mvc.ViewPage<EyeTracker.Models.RegisterModel>" %>
<asp:Content ID="PageTitleContent" ContentPlaceHolderID="PageTitleContent" runat="server">Log On</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="<%: Url.Content("~/Content/Account.Register.css") %>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<form method="post" class="signin js-signin" action="https://twitter.com/sessions?phx=1">
    <h4>Sign in to Twitter</h4>
    <fieldset class="textbox">
  <label class="username js-username">
    <span>Username or email</span>
    <input type="text" autocomplete="on" name="session[username_or_email]" value="" class="js-username-field">
  </label>
  <label class="password">
    <span>Password</span>
    <input type="password" name="session[password]" value="" class="js-password-field">
  </label>
  <label class="remember">
    <input type="checkbox" name="remember_me" value="1">
    <span>Remember me</span>
  </label>
  <button class="submit button" type="submit">Sign in</button>
</fieldset>
</form>


    <div id="main" class="panel">
    <% using (Html.BeginForm()) { %>
        <%: Html.ValidationSummary(true, "Account creation was unsuccessful. Please correct the errors and try again.") %>
        <div>
            <fieldset>
                <legend>Account Information</legend>
                <div>Passwords are required to be a minimum of <%: ViewData["PasswordLength"] %> characters in length.</div>
                                
                <div class="editor-label">
                    <%: Html.LabelFor(m => m.Email) %>
                </div>
                <div class="editor-field">
                    <%: Html.TextBoxFor(m => m.Email) %>
                    <%: Html.ValidationMessageFor(m => m.Email) %>
                </div>
                
                <div class="editor-label">
                    <%: Html.LabelFor(m => m.Password) %>
                </div>
                <div class="editor-field">
                    <%: Html.PasswordFor(m => m.Password) %>
                    <%: Html.ValidationMessageFor(m => m.Password) %>
                </div>
                
                <div class="editor-label">
                    <%: Html.LabelFor(m => m.ConfirmPassword) %>
                </div>
                <div class="editor-field">
                    <%: Html.PasswordFor(m => m.ConfirmPassword) %>
                    <%: Html.ValidationMessageFor(m => m.ConfirmPassword) %>
                </div>               
                <p>
                    <input type="submit" value="Register" />
                </p>
             </fieldset>
       </div>
    <% } %>
    </div>
</asp:Content>
