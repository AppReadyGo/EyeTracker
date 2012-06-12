<%@ Page Language="C#" MasterPageFile="~/Areas/m/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<EyeTracker.Models.Account.RegisterModel>" %>

<asp:Content ContentPlaceHolderID="PageTitleContent" runat="server">
   Register
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<% using (Html.BeginForm()) { %>
    <h2>Account Information</h2>
    <p>Passwords are required to be a minimum of 6 characters in length.</p>
    <label for="Email">Email:</label>
	<input type="email" name="Email" id="Email" data-mini="true" autocomplete="off" value="<%: Model.Email %>" />
    <%: Html.ValidationMessageFor(m => m.Email) %>
	<label for="Password">Password:</label>
	<input type="password" name="Password" id="Password" data-mini="true" autocomplete="off" value="<%: Model.Password %>" />
    <%: Html.ValidationMessageFor(m => m.Password)%>
	<label for="ConfirmPassword">Re Password:</label>
	<input type="password" name="ConfirmPassword" id="ConfirmPassword" data-mini="true" autocomplete="off" value="<%: Model.ConfirmPassword %>" />
    <%: Html.ValidationMessageFor(m => m.ConfirmPassword)%>
    <%: Html.ValidationSummary(true) %>
	<button type="submit">Submit</button>
<% } %>
</asp:Content>