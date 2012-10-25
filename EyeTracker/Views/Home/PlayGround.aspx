<%@ Page Title="" Language="C#" 
MasterPageFile="~/Views/Shared/Content.Master" 
Inherits="ViewPage<ViewModelWrapper<BeforeLoginMasterModel, EyeTracker.Model.Pages.Home.PricingModel>>" %>
<asp:Content ID="PageTitleContent" ContentPlaceHolderID="PageTitleContent" runat="server">Log On</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<div id="page_features">
<article>
    <img alt="Touch map preview" src="/Resources/Feature_TouchMap.jpg" />
    <h3>Touch map:</h3>
    <p>TouchMap is a unique technology which analyzes touches in any area of your application. Links, images, text, or empty spaces - see what’s being clicked and what parts users ignore. Use this data in order to:</p>
    <p class="clear"></p>
</article>
<article>
    <img alt="Touch map preview" src="/Resources/Feature_EyeTrack.jpg" />
    <h3>Eye Track:</h3>
    <p>EyeTrack technology will indicate for you how much time and attention your application or website users spend per each part of your content.</p>
    <p class="clear"></p>
</article>
<article>
    <img alt="Touch map preview" src="/Resources/Feature_PlayBack.jpg" />
    <h3>Play Back:</h3>
    <p>PlayBack to make the most out of you applications. Improve them using the data you collect for your users to have the best experience possible.</p>
    <p class="clear"></p>
</article>
<p class="get-started">Are you ready to try Mobillify? &nbsp; <a href="/p/getstarted" class="button green big">Get Started</a> </p>
</div>
</asp:Content>