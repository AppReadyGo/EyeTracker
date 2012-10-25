<%@ Import Namespace="EyeTracker.Model.Pages.Home" %>
<%@ Page Title="" Language="C#" 
MasterPageFile="~/Views/Shared/Content.Master" 
Inherits="ViewPage<ViewModelWrapper<BeforeLoginMasterModel, ContentModel>>" %>

<asp:Content ID="PageTitleContent" ContentPlaceHolderID="PageTitleContent" runat="server">
    404 Error
</asp:Content>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="<%: Url.Content("~/Content/template/css/404.css") %>" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<article class="borders">
    <h2>We Couldn't Find Your Page! (404 Error)</h2>
    <p>Unfortunately, the page you've requested cannot be displayed. It appears that you've lost your way either through an outdated link or a typo on the page you were trying to reach.</p>
</article>
</asp:Content>
