<%@ Import Namespace="EyeTracker.Model.Pages.Home" %>
<%@ Page Language="C#" 
MasterPageFile="~/Views/Shared/BeforeLogin.Master" 
Inherits="ViewPage<ViewModelWrapper<BeforeLoginMasterModel, IndexModel>>" %>

<asp:Content ID="PageTitleContent" ContentPlaceHolderID="PageTitleContent" runat="server">Home</asp:Content>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <section id="slideshow">
        <div class="content-wrapper">
        <img src="/content/new/images/screens.png" />
        <h1><strong>Get</strong> you user<br /> 
            <strong>Improve</strong> your app</h1>
        <h2>
           FingerPrint helps you understand how users interact with your application        
        </h2>
        <a href="/account/register" class="button yellow btn-getstarted">Get Started Now</a>
        </div>
    </section><!-- /#slideshow --> 
    <section id="content">
        <div class="content-wrapper">
        <article id="features">
            <h2>Improve you application with the features of FingePrint</h2>
            <ul>
                <li>
                    <img src="/content/new/images/touchmap_grey.png" />
                    <h3>TouchMap</h3>
                    <p>See where users touch in any area of an application.</p>
                </li>
                <li>
                    <img src="/content/new/images/eyetrack_grey.png" />
                    <h3>EyeTrack</h3>
                    <p>Check you how much attention users spend on each part of the content.</p>
                </li>
                <li>
                    <img src="/content/new/images/playback_grey.png" />
                    <h3>PlayBack</h3>
                    <p>Understand how users reach specific application features.</p>
                </li>
                <li>
                    <img src="/content/new/images/eyetrack_grey.png" />
                    <h3>A/B compare</h3>
                    <p>Compare two versions of a design to see which performs better.</p>
                </li>
            </ul>
            <p class="clear"></p>
        </article><!-- /#features --> 
        <article id="action">
            <span>If you are still not convinced, try it free!</span>
            <a href="/account/register" class="button green btn-getstarted">Get Started</a>
        </article><!-- /#action --> 
        <article id="info">
            <iframe width="320" height="240" src="http://www.youtube.com/embed/hTausXI9aYw" frameborder="0" allowfullscreen class="video"></iframe>
            <h2>Why Mobillify?</h2>
            <p>
                We help developers understand how users interact with a mobile application or a mobile web site and compare two versions of a design for a length of time to see which performs better. Using FingerPrint, you are able to test you UI and UX design completely, finding out exactly what you need to do to optimize your users experience. 
            </p>
            <p class="clear"></p>
        </article><!-- /#info -->
        </div><!-- /.content-wrapper --> 
    </section><!-- /#content --> 
</asp:Content>


