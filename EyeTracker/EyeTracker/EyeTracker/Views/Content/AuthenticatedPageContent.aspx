<%@ Import Namespace="EyeTracker.Model.Pages.Home" %>
<%@ Page Title="" Language="C#" 
MasterPageFile="~/Views/Shared/AfterLoginContent.Master" 
Inherits="ViewPage<ViewModelWrapper<AfterLoginMasterModel, ContentModel>>" %>
<asp:Content ID="PageTitleContent" ContentPlaceHolderID="PageTitleContent" runat="server"><%: Model.View.Title %></asp:Content>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript" src="Scripts/ThridParty/jquery-1.4.1.js"></script>
    <script type="text/javascript" src="/Scripts/ThridParty/snippet/jquery.snippet.min.js"></script>
    <link rel="stylesheet" type="text/css" href="/Scripts/ThridParty/snippet/jquery.snippet.min.css" />
    <script type="text/javascript" >
        $(document).ready(function () {
            $("pre.java").snippet("java", { style: "bright" });
        });
    </script>
    <link href="<%: Url.Content("~/Content/template/css/content.authenticated.page.css") %>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <%= Model.View.Content %>
</asp:Content>


