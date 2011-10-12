<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">Membership</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<table>
    <caption>Applications</caption>
    <thead>
        <tr><th>Id</th><th>Name</th><th>Description</th></tr>
    </thead>
    <tbody>
        <% foreach (var curApp in ViewBag.Applications)
        {%>
            <tr><td><%:curApp.Id%></td><td><%:curApp.Name%></td><td><%:curApp.Description%></td></tr>
        <%}%>
    </tbody>
</table>
<br />
<table>
    <caption>Roles</caption>
    <thead>
        <tr><th>Id</th><th>Application Id</th><th>Name</th><th>Description</th></tr>
    </thead>
    <tbody>
        <% foreach (var curRole in ViewBag.Roles)
        {%>
            <tr><td><%:curRole.Id%></td><td><%:curRole.App.Id%></td><td><%:curRole.Name%></td><td><%:curRole.Description%></td></tr>
        <%}%>
    </tbody>
</table>
<br />
<table>
    <caption>Users</caption>
    <thead>
        <tr><th>Id</th><th>Application Id</th><th>Name</th><th>Last Activity</th><th></th><th></th></tr>
    </thead>
    <tbody>
        <% foreach (var curUser in ViewBag.Users)
        {%>
            <tr><td><%:curUser.Id%></td><td><%:curUser.App.Id%></td><td><%:curUser.Name%></td><td><%:curUser.Email%></td><td><%:curUser.LastActivityDate%></td><td><%:Html.ActionLink("edit", "UserEdit", new { id = curUser.Id })%>&nbsp; | &nbsp;<%:Html.ActionLink("remove", "UserRemove", new { id = curUser.Id })%></td></tr>
        <%}%>
    </tbody>
</table>
</asp:Content>
