<%@ Page Title="" Language="C#" 
MasterPageFile="~/Views/Shared/Content.Master" 
Inherits="ViewPage<ViewModelWrapper<BeforeLoginMasterModel, EyeTracker.Model.Pages.Account.ForgotPasswordModel>>" %>

<asp:Content ID="PageTitleContent" ContentPlaceHolderID="PageTitleContent" runat="server">
    Forgot Password
</asp:Content>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
    <link rel="stylesheet" href="<%: Url.Content("~/Content/template/css/form.css") %>" type="text/css" media="all">
    <link rel="stylesheet" href="<%: Url.Content("~/Content/template/css/account.forgotpassword.css") %>" type="text/css" media="all">
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<article class="col-2">
    <% Html.RenderPartial("Forms/ForgotPasswordForm", Model.View); %>
</article>
</asp:Content>