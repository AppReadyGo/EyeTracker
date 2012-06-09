<%@ Page Title="" Language="C#" 
MasterPageFile="~/Views/Shared/Content.Master" 
Inherits="ViewPage<ViewModelWrapper<BeforeLoginMasterModel, EyeTracker.Models.Account.RegisterModel>>" %>
<asp:Content ID="PageTitleContent" ContentPlaceHolderID="PageTitleContent" runat="server">
    Register
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="<%: Url.Content("~/Content/template/css/account.register.css") %>" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="<%: Url.Content("~/Content/template/css/form.css") %>" type="text/css" media="all">
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<article class="col-2">
<% Html.RenderPartial("Forms/RegisterForm", Model.View); %>
</article>
</asp:Content>
