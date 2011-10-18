<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<EyeTracker.Domain.Model.User>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">User Edit</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<script src="<%:Url.Content("~/Scripts/jquery.validate.min.js")%>" type="text/javascript"></script>
<script src="<%:Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js")%>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>User Edit</h2>
<p>
    Use the form below to create a new account. 
</p>
<p>
    Passwords are required to be a minimum of <%: Membership.MinRequiredPasswordLength %> characters in length.
</p>

<% using (Html.BeginForm()) {%>
    <%: Html.ValidationSummary(true, "Account creation was unsuccessful. Please correct the errors and try again.")%>
    <div>
        <fieldset>
            <legend>Account Information</legend>

            <div class="editor-label">
                <%: Html.LabelFor(m => m.Name)%>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(m => m.Name)%>
                <%: Html.ValidationMessageFor(m => m.Name)%>
            </div>

            <div class="editor-label">
                <%: Html.LabelFor(m => m.Email)%>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(m => m.Email)%>
                <%: Html.ValidationMessageFor(m => m.Email)%>
            </div>

            <div class="editor-label">
                <%: Html.LabelFor(m => m.Roles)%>
            </div>
            <div class="editor-field">
                <ul>
                <% foreach (var curRole in ViewBag.Roles)
                {%>
                    <%string ch = Model.Roles.Contains(curRole.Id) ? "checked" : "";%>
                    <li><label><input type="checkbox" name="Roles" value="<%:curRole.Id%>" <%:ch%> />&nbsp;<%:curRole.Name%></label></li>
                <%}%>
                </ul>
            </div>

            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>
    </div>
}
