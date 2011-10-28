<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<EyeTracker.Models.RegisterModel>" %>

<asp:Content ID="registerTitle" ContentPlaceHolderID="TitleContent" runat="server">Register</asp:Content>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="../../Content/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Content/FixedContent.css" rel="stylesheet" type="text/css" />
    <link href="../../Content/LogOn.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Title" ContentPlaceHolderID="Title" runat="server">Register</asp:Content>

<asp:Content ID="registerContent" ContentPlaceHolderID="MainContent" runat="server">
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
