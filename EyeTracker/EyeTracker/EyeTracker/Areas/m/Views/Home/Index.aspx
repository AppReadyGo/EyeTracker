<%@ Page Language="C#" MasterPageFile="~/Areas/m/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="PageTitleContent" runat="server">
   Home
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div data-role="collapsible-set">
	<div data-role="collapsible" data-collapsed="false" data-theme="g" data-content-theme="d">
		<h3 class="ui-collapsible-heading-custom">Eye Tracker</h3>
		<p>Realize what your visitors look at or read, and what part of content was completely skipped over.</p>
		<a data-role="button" data-inline="true" data-transition="slide" data-icon="arrow-r" data-iconpos="right" href="/m/products/eye-track">Learn More</a>
	</div>
	<div data-role="collapsible" data-theme="g" data-content-theme="d">
		<h3>Touch Map</h3>
		<p>Visualized reports of end-user attention held by each area, on every page of a given application.</p>
		<a data-role="button" data-inline="true" data-transition="slide" data-icon="arrow-r" data-iconpos="right" href="/m/products/touch-map">Learn More</a>
	</div>
	<div data-role="collapsible" data-theme="g" data-content-theme="d">
		<h3>Play Back</h3>
		<p>Rewind or watch your visitors' full browsing sessions to discover exactly how they use your application.</p>
		<a data-role="button" data-inline="true" data-transition="slide" data-icon="arrow-r" data-iconpos="right" href="/m/products/play-back">Learn More</a>
	</div>
</div>
</asp:Content>
