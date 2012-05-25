<%@ Page Title="" Language="C#" 
MasterPageFile="~/Views/Shared/Content.Master" 
Inherits="ViewPage<ViewModelWrapper<BeforeLoginMasterModel, EyeTracker.Models.Account.ChangePasswordModel>>" %>

<asp:Content ID="PageTitleContent" ContentPlaceHolderID="PageTitleContent" runat="server">
    Change Password
</asp:Content>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="<%: Url.Content("~/Content/account.css") %>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<div>
<% Html.RenderPartial("Forms/ChangePasswordForm", Model.View); %>
</div>
</asp:Content>
