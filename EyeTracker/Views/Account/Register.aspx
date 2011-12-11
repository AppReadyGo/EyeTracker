<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Content.Master" Inherits="System.Web.Mvc.ViewPage<EyeTracker.Models.RegisterModel>" %>
<asp:Content ID="PageTitleContent" ContentPlaceHolderID="PageTitleContent" runat="server">Log On</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="<%: Url.Content("~/Content/shared/Form.css") %>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<% using (Html.BeginForm()) { %>
    <%: Html.ValidationSummary(true, "Account creation was unsuccessful. Please correct the errors and try again.") %>
<fieldset>
    <h3>Account Information</h3>
    <p>Passwords are required to be a minimum of <%: ViewData["PasswordLength"] %> characters in length.</p>
                                
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
<% } %>
</asp:Content>
