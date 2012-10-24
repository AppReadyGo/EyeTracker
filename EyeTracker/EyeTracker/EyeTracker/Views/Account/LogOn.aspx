<%@ Page Title="" Language="C#" 
MasterPageFile="~/Views/Shared/Content.Master" 
Inherits="ViewPage<ViewModelWrapper<BeforeLoginMasterModel, EyeTracker.Models.Account.LogOnModel>>" %>
<asp:Content ID="PageTitleContent" ContentPlaceHolderID="PageTitleContent" runat="server">Log On</asp:Content>
<asp:Content ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<article class="center">
    <% Html.RenderPartial("Forms/LogOnForm", Model.View); %>
</article>
</asp:Content>