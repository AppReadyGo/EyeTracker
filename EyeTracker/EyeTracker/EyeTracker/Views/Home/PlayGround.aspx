<%@ Import Namespace="EyeTracker.Model.Pages.Home" %>

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/BeforeLogin.Master"
    Inherits="ViewPage<ViewModelWrapper<BeforeLoginMasterModel, PricingModel>>" %>

<asp:Content ID="PageTitleContent" ContentPlaceHolderID="PageTitleContent" runat="server">
    Pricing
</asp:Content>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="background-color: #fff; padding: 50px 263px;">
        <p>
            Dear customer,</p>
        <p>
            Thank you for signing up for our mobile apps analytical service. Due to high demand
            for our analytics services, we are constantly increasing our capacity and processing
            hundreds of new customers weekly. Now you are in fast moving line and we shall open
            your account within two weeks, notification will be sent to your email.</p>
        <p>
            Thanks again for your patience.</p>
        <p>
            Looking forward to serve you,</p>
        <p>
            Mobillify team</p>

            <br /><br />
            <h3>Play back:</h3>
<p>Record & track what visitors do with your app. It's as if you're looking over their shoulder!</p>
<ul>
<li>Understand how people use your app</li>
<li>What users are trying to achieve and where they encounter errors.</li>
<li>Find specific videos of customers who complete or drop out.</li>
</ul>
    </div>
</asp:Content>
