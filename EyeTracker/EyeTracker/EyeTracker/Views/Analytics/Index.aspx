<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/AfterLogin.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Import Namespace="EyeTracker.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitleContent" runat="server">Portfolios</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    <script src="<%: Url.Content("~/Scripts/Analytics.Index.js") %>" type="text/javascript"></script>
    <link href="<%: Url.Content("~/Content/Analytics.Index.css")%>" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var dashboardUrl = '/Analytics/Dashboard/{0}/{1}';
    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3 class="title">Portfolios</h3>
    <table>
        <caption><a href="Portfolio/New">New Portfolio</a></caption>
        <tbody>
        <%if (ViewBag.Count == 0)
          {%>
           <tr><td>No Portfolios</td></tr>
        <%}
          else
          {%>
        <%foreach (var item in ViewBag.Data)
          {%>
        <tr class="main" pid="<%: item.Id %>">
            <td class="expand space"></td>
            <td><%: item.Description%></td>
            <td> Visits: <%: item.Visits%></td>
        </tr>
        <tr class="sub">
            <td></td>
            <td colspan="2">
            <table>
                <tbody>
                <%if (item.Applications.Count > 0)
                  { %>
                    <%foreach (var app in item.Applications)
                      { %>
                        <tr pid="<%: item.Id %>" aid="<%: app.Id %>">
                            <td><%: app.Description%></td>
                            <td> Visits: <%: app.Visits%></td>
                        </tr>
                    <%} %>
                <%}
                  else
                  {%>
                    <tr><td>The portfolio does not have applications, please <a href="/Application/New/<%: item.Id %>">add application.</a></td></tr>
                <%} %>
                </tbody>
            </table>
            </td>
        </tr>
        <%} %>
        <%} %>
        </tbody>
    </table>
</asp:Content>

