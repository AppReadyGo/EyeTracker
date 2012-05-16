<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    if (Request.IsAuthenticated) {
%>Welcome <b><%: Page.User.Identity.Name.Take(10) %></b>![ <%: Html.ActionLink("Log Off", "LogOff", "Account") %> ]<%
    }
    else {
%> 
        [ <%: Html.ActionLink("Log On", "LogOn", "Account") %> ]
<%
    }
%>
