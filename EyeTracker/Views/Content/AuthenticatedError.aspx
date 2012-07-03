<%@ Import Namespace="EyeTracker.Model.Pages.Home" %>
<%@ Page Title="" Language="C#" 
MasterPageFile="~/Views/Shared/AfterLoginContent.Master" 
Inherits="ViewPage<ViewModelWrapper<AfterLoginMasterModel, ContentModel>>" %>

<asp:Content ID="PageTitleContent" ContentPlaceHolderID="PageTitleContent" runat="server">
    Error
</asp:Content>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
   <link href="<%: Url.Content("~/Content/error.css") %>" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<article class="col-2">
    <h2>An error occurred while processing your request!</h2>
    <p>Sorry, an error occurred while processing your request. Please contact to administrator.</p>
</article>
</asp:Content>
