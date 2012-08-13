<%@ Page Title="" Language="C#" 
MasterPageFile="~/Views/Shared/Content.Master" 
Inherits="ViewPage<ViewModelWrapper<BeforeLoginMasterModel, object>>" %>

<asp:Content ID="PageTitleContent" ContentPlaceHolderID="PageTitleContent" runat="server">
    Terms And Coditions
</asp:Content>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="<%: Url.Content("~/Content/account.termsandconditions.css") %>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<form action="/Account/AcceptTermsAndCoditions" method="post">
<div>
<%= ViewBag.Content %>
</div>
<div class="actions"><a href="#" class="link2" onClick="$(this).closest('form').submit();"><span><span>Accept</span></span></a><a class="link4" href="/"><span><span>Cancel</span></span></a></div>
</form>
</asp:Content>