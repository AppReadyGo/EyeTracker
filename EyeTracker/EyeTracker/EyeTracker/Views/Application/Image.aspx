<%@ Page Title="" Language="C#" MasterPageFile="Analytics.master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">Eye Tracker</asp:Content>
<asp:Content ContentPlaceHolderID="HeaderContent" runat="server">
    <script src="../../Scripts/Image.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server">Eye Tracker</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<%if (ViewBag.NoData)
  {%>
    <img src="/content/img/no_data.jpg"/>
<%}
  else
  { %>
    Screen Size: <%: Html.DropDownList("ScreenSize", (IEnumerable<SelectListItem>)ViewData["ScreenSizes"])%>
    Paths: <%: Html.DropDownList("PageUris", (IEnumerable<SelectListItem>)ViewData["PageUris"])%><br />
    From date: <input />
    To date: <input /><br />
    <img src="<%: ViewBag.EyeTrackerImageUrl %>"/>
<%} %>
</asp:Content>
