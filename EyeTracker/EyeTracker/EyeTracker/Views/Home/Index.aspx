<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Analytics
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: ViewData["Message"] %></h2>
    <p>
    </p>
    <table>
        <tr><td>Views</td><td>Clicks</td></tr>
        <tr><td>
            <img src="<%: ViewData["viewHeatMapImgUrl"] %>" alt="Alternate Text" width="320" />
        </td><td>
            <img src="<%: ViewData["clickHeatMapImgUrl"] %>" alt="Alternate Text" width="320" />
        </td></tr>
    </table>
</asp:Content>
