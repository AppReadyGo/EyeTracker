<%@ Page Title="" Language="C#" 
MasterPageFile="~/Views/Shared/Content.Master" 
Inherits="ViewPage<ViewModelWrapper<BeforeLoginMasterModel, EyeTracker.Models.Account.ForgotPasswordModel>>" %>

<asp:Content ID="PageTitleContent" ContentPlaceHolderID="PageTitleContent" runat="server">
    Unsubscribe
</asp:Content>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="<%: Url.Content("~/Content/account.css") %>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<div>
<% Html.RenderPartial("Forms/UnsubscribeForm", Model.View); %>
</div>
</asp:Content>