<%@ Page Title="" Language="C#" 
MasterPageFile="~/Views/Shared/Content.Master" 
Inherits="ViewPage<ViewModelWrapper<BeforeLoginMasterModel, EyeTracker.Model.Pages.Home.PricingModel>>" %>
<asp:Content ID="PageTitleContent" ContentPlaceHolderID="PageTitleContent" runat="server">Log On</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<style type="text/css">
    article h1{font-size:25px;}
    article ol{list-style:none;}
    article span{font-size:95px;float:left;margin-right:20px;color:#6E9F31;font-weight:bold;}
    article .arrow{text-align:center;margin:30px;clear:both;}
    article .button{margin:20px;}
    #intro .button{margin:20px;}
    #intro
</style>
<article id="intro">    
	<h1>How It Works</h1>
    <a style="float:right;" href="/account/register" class="button green big">Get started now »</a>
	<h4>Getting something designed at 99designs is as easy as 1-2-3.</h4>
	<p>
        We help you host a “design contest”, where thousands of designers compete to create a design you love, or your money back!
	</p>
</article>
<article>
	<ol>
		<li>
			<span>1</span>
			<h3>Launch your design contest</h3>
			<p>
			 Create a design brief which is simply a <strong>clear outline of what you need designed</strong>. To begin, post this brief to 99designs and <strong>choose a design package</strong>.
			</p>
			<p class="arrow"><img src="/content/new/images/arrow-down.png" alt=" "></p>
		</li>
		<li>
			<span>2</span>
			<h3>Collaborate with the designers</h3>
			<p>
				Designers then submit concepts to <strong>compete for your prize</strong>.  Be sure to <strong>provide continual feedback</strong> to help the designers deliver a concept you love!
			</p>
			<p class="arrow"><img src="/content/new/images/arrow-down.png" alt=" "></p>
		</li>
		<li>
			<span>3</span>
			<h3>Choose your favorite design</h3>
			<p>
				At the completion of your contest, you'll need to <strong>pick your favorite design</strong> and <strong>award a winner</strong>. You'll then <strong>receive the final design</strong> along with copyright to the original art work.
			</p>
			<p><em>It's that easy!</em></p>
		</li>
	</ol>
</article>
</asp:Content>