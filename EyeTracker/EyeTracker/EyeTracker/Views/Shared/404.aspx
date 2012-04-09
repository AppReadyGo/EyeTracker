<%@ Import Namespace="EyeTracker.Model.Pages.Home" %>
<%@ Page Title="" Language="C#" 
MasterPageFile="~/Views/Shared/BeforeLogin.Master" 
Inherits="ViewPage<ViewModelWrapper<BeforeLoginMasterModel, PricingModel>>" %>

<asp:Content ID="PageTitleContent" ContentPlaceHolderID="PageTitleContent" runat="server">
    404 Error
</asp:Content>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">

</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<div class="content-wrapper">
    <div style="background-color: #fff; padding: 50px 263px;">
        <strong>We Couldn't Find Your Page! (404 Error)</strong>
        <p>Unfortunately, the page you've requested cannot be displayed. It appears that you've lost your way either through an outdated link or a typo on the page you were trying to reach.</p>
    </div>
</div>
</asp:Content>
