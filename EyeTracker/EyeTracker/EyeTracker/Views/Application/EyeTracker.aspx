<%@ Page Title="" Language="C#" MasterPageFile="Analytics.master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">Eye Tracker</asp:Content>
<asp:Content ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server">Eye Tracker</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
Screen Size: <%: Html.DropDownList("ScreenSize", (IEnumerable<SelectListItem>)ViewData["ScreenSizes"]) %>
Paths:
<ul>
    <li><a>Some path/path</a></li>
    <li><a>Some path/path</a></li>
    <li><a>Some path/path</a></li>
    <li><a>Some path/path</a></li>
</ul>
From date: <input />
To date: <input />
<img src="<%: ViewBag.EyeTrackerImageUrl %>"/>
</asp:Content>
