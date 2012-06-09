<%@ Page Title="" Language="C#" 
MasterPageFile="~/Views/Shared/Content.Master" 
Inherits="ViewPage<ViewModelWrapper<BeforeLoginMasterModel, EyeTracker.Models.Account.LogOnModel>>" %>
<asp:Content ID="PageTitleContent" ContentPlaceHolderID="PageTitleContent" runat="server">Log On</asp:Content>
<asp:Content ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="<%: Url.Content("~/Content/template/css/account.logon.css") %>" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="<%: Url.Content("~/Content/template/css/form.css") %>" type="text/css" media="all">
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<article class="col-2">
<% Html.RenderPartial("Forms/LogOnForm", Model.View); %>
</article>
</asp:Content>