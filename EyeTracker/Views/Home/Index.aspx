<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Analytics
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: ViewData["Message"] %></h2>
    <p>
<% using (Html.BeginForm()) { %>
    <%: Html.DropDownList("appId", (IEnumerable<SelectListItem>)ViewData["applications"])%>
    <%: Html.DropDownList("pageUri", (IEnumerable<SelectListItem>)ViewData["uriList"])%>
    <%: Html.DropDownList("clientSize", (IEnumerable<SelectListItem>)ViewData["clientSizes"])%>
    <input type="submit" value="Show" />
    <p style="color:Red;"><%:ViewData["errorMessage"]%></p>
<% } %>
    </p>
    <%if (!string.IsNullOrEmpty((string)ViewData["viewHeatMapImgUrl"]))
      { %>
    <table>
        <tr><td>Views</td><td>Clicks</td></tr>
        <tr><td>
            <img src="<%: ViewData["viewHeatMapImgUrl"] %>" alt="Alternate Text" width="<%: ViewData["width"]%>" />
        </td><td>
            <img src="<%: ViewData["clickHeatMapImgUrl"] %>" alt="Alternate Text" width="<%: ViewData["width"]%>" />
        </td></tr>
    </table>
    <% }%>
</asp:Content>
