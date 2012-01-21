<%@ Page Language="C#" MasterPageFile="~/Views/Shared/BeforeLogin.Master" Inherits="System.Web.Mvc.ViewPage<System.Web.Mvc.HandleErrorInfo>" %>

<asp:Content ID="errorTitle" ContentPlaceHolderID="PageTitleContent" runat="server">
    Error
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
   <link href="<%: Url.Content("~/Content/error.css") %>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="errorContent" ContentPlaceHolderID="MainContent" runat="server">
<div class="error">
    Sorry, an error occurred while processing your request.<br />
    Please contact to administrator
</div>
</asp:Content>
