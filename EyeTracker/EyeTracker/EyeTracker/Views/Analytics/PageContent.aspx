<%@ Import Namespace="EyeTracker.Model.Pages.Home" %>
<%@ Page Title="" Language="C#" 
MasterPageFile="~/Views/Shared/AfterLogin.Master" 
Inherits="ViewPage<ViewModelWrapper<AfterLoginMasterModel, ContentModel>>" %>
<asp:Content ID="PageTitleContent" ContentPlaceHolderID="PageTitleContent" runat="server"><%: Model.View.Title %></asp:Content>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="<%: Url.Content("~/Content/template/css/home.pagecontent.css") %>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
 <div class="inside">
	<div class="wrap">
    <%= Model.View.Content %>
    <div class="clear"></div>
	</div>
</div>   
</asp:Content>


