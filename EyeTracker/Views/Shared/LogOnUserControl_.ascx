<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    if (Request.IsAuthenticated) {
%>Welcome <br /><b><%: Page.User.Identity.Name %></b>!<br />[ <%: Html.ActionLink("Log Off", "LogOff", "Account") %> ]<%
    }
    else {
%> 
        [ <%: Html.ActionLink("Log On", "LogOn", "Account") %> ]
<%
    }
%>
