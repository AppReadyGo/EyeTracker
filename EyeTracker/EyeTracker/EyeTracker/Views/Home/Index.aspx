<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Home.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="PageTitleContent" ContentPlaceHolderID="PageTitleContent" runat="server">Home</asp:Content>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
    <script src="<%: Url.Content("~/Scripts/ThridParty/jquery.roundabout.min.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/home.index.js") %>" type="text/javascript"></script>
    <link href="<%: Url.Content("~/Content/home.index.css") %>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<h2>VISULAZE YOUR VISITORS</h2>
<h3>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;to manage your business...</h3>
<div>
	<ul class="roundabout">
		<li><img src="/Content/images/traffic.jpg" /></li>
		<li><img src="/Content/images/traffic.jpg" /></li>
		<li><img src="/Content/images/traffic.jpg" /></li>
		<li><img src="/Content/images/traffic.jpg" /></li>
	</ul>
</div>
<a href="/register" class="btn-access">ACCESS ANALITYCS</a>
</asp:Content>


