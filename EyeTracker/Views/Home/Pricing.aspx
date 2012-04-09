<%@ Import Namespace="EyeTracker.Model.Pages.Home" %>
<%@ Page Title="" Language="C#" 
MasterPageFile="~/Views/Shared/BeforeLogin.Master" 
Inherits="ViewPage<ViewModelWrapper<BeforeLoginMasterModel, PricingModel>>" %>

<asp:Content ID="PageTitleContent" ContentPlaceHolderID="PageTitleContent" runat="server">
    Pricing
</asp:Content>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="<%: Url.Content("~/Content/home.pricing.css") %>" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<div class="content-wrapper">
    <div class="price-list">
	    <section class="first">
		    <header>
			    <hgroup class="top">
				    <h1>Basic</h1>
			    </hgroup>
			    <hgroup class="price">
				    <h2>Free</h2>
			    </hgroup>
		    </header>
		    <footer>
			    <ul>
				    <li>Up to <strong>3</strong> active applications</li>
				    <li><strong>1000</strong> sessions per application</li>
				    <li><strong>No</strong> Eye-Track</li>
				    <li><strong>No</strong> PlayBack</li>
                    <li><strong>Weekly</strong> alerts</li>
				    <li class="last"><a href="/account/register" class="btn">Select</a></li>
			    </ul>
		    </footer>
	    </section>
	    <section class="on">
		    <header>
			    <hgroup class="top">
				    <h1>Plus</h1>
			    </hgroup>
			    <hgroup class="price">
				    <h2><span class="style1">$</span><span class="style2">49</span><span class="style3">Monthly</span></h2>
			    </hgroup>
		    </header>
		    <footer>
			    <ul>
				    <li>Up to <strong>5</strong> active applications</li>
				    <li><strong>10000</strong> sessions per application</li>
				    <li><strong>Yes</strong> Eye-Track</li>
				    <li><strong>Yes</strong> PlayBack</li>
                    <li><strong>Daily</strong> alerts</li>
				    <li class="last"><a href="/account/register" class="btn">Select</a></li>
			    </ul>
		    </footer>
	    </section>
	    <section class="last">
		    <header>
			    <hgroup class="top">
				    <h1>Pro</h1>
			    </hgroup>
			    <hgroup class="price">
				    <h2><span class="style1">$</span><span class="style2">99</span><span class="style3">Monthly</span></h2>
			    </hgroup>
		    </header>
		    <footer>
			    <ul>
				    <li>Up to <strong>15</strong> active applications</li>
				    <li><strong>30000</strong> sessions per application</li>
				    <li><strong>Yes</strong> Eye-Track</li>
				    <li><strong>Yes</strong> PlayBack</li>
                    <li><strong>Hourly</strong> alerts</li>
				    <li class="last"><a href="/account/register" class="btn">Select</a></li>
			    </ul>
		    </footer>
	    </section>
    </div>
    <div style="clear:both;"></div>
</div>
</asp:Content>
