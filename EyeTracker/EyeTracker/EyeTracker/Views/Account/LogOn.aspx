<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Content.Master" Inherits="System.Web.Mvc.ViewPage<EyeTracker.Model.ViewModelWrapper<EyeTracker.Model.Master.BeforeLoginViewModel, EyeTracker.Models.Account.LogOnModel>>" %>
<asp:Content ID="PageTitleContent" ContentPlaceHolderID="PageTitleContent" runat="server">Log On</asp:Content>
<asp:Content ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="<%: Url.Content("~/Content/account.css") %>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<div>
<% using (Html.BeginForm()) { %>
    <%: Html.ValidationSummary(true, "Login was unsuccessful. Please correct the errors and try again.") %>
        <h3>Log On</h3>
        <p>
            Please enter your username and password. 
        </p>
               
        <div class="editor-label">
            <%: Html.LabelFor(m => m.View.UserName) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(m => m.View.UserName)%>
            <%: Html.ValidationMessageFor(m => m.View.UserName)%>
        </div>
                
        <div class="editor-label">
            <%: Html.LabelFor(m => m.View.Password)%>
        </div>
        <div class="editor-field">
            <%: Html.PasswordFor(m => m.View.Password)%>
            <%: Html.ValidationMessageFor(m => m.View.Password)%>
        </div>
                
        <div class="editor-label">
            <%: Html.CheckBoxFor(m => m.View.RememberMe)%>
            <%: Html.LabelFor(m => m.View.RememberMe)%>
        </div>
                
        <p>
            <input type="submit" value="Log On" />
        </p>
<% } %>
</div>
<p style="clear:both;"></p>
</asp:Content>