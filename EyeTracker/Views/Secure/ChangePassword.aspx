<%@ Page Title="" Language="C#" 
MasterPageFile="~/Views/Shared/AfterLoginContent.Master" 
Inherits="ViewPage<ViewModelWrapper<AfterLoginMasterModel, EyeTracker.Models.Account.ChangePasswordModel>>" %>

<asp:Content ID="PageTitleContent" ContentPlaceHolderID="PageTitleContent" runat="server">
    Change Password
</asp:Content>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
    <link rel="stylesheet" href="<%: Url.Content("~/Content/template/css/form.css") %>" type="text/css" media="all">
    <link rel="stylesheet" href="<%: Url.Content("~/Content/template/css/secure.change.password.css") %>" type="text/css" media="all">
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<% Html.RenderPartial("Forms/ChangePasswordForm", Model.View); %>
</asp:Content>


