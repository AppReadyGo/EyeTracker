﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/AfterLogin.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Import Namespace="EyeTracker.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitleContent" runat="server">Portfolios</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    <script src="<%: Url.Content("~/Scripts/Analytics.Index.js") %>" type="text/javascript"></script>
    <link href="<%: Url.Content("~/Content/Analytics.Index.css")%>" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var dashboardUrl = '/Analytics/{0}/Dashboard/{1}';
        var type = { Portfolio:'Portfolio', Application:'Application'};
    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Portfolios</h3>
    <table>
        <thead></thead>
        <tbody>
        <%foreach (var item in ViewBag.Data)
          {%>
        <tr class="main" itemid="<%: item.Id %>">
            <td class="expand space"></td>
            <td><%: item.Description %></td>
            <td> Visits: <%: item.Visits %></td>
        </tr>
        <tr class="sub" itemid="<%: item.Id %>">
            <td></td>
            <td colspan="2">
            <table>
                <tbody>
                <%foreach (var app in item.Applications)
                  { %>
                <tr itemid="<%: app.Id %>">
                <td><%: app.Description%></td>
                <td> Visits: <%: app.Visits%></td>
                </tr>
                <%} %>
                </tbody>
            </table>
            </td>
        </tr>
        <%} %>
        </tbody>
    </table>
</asp:Content>
