<%@ Import Namespace="EyeTracker.Model.Pages.Home" %>
<%@ Page Language="C#" 
MasterPageFile="~/Views/Shared/Home.Master" 
Inherits="ViewPage<ViewModelWrapper<BeforeLoginMasterModel, IndexModel>>" %>

<asp:Content ID="PageTitleContent" ContentPlaceHolderID="PageTitleContent" runat="server">Home</asp:Content>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
    <script src="<%: Url.Content("~/Scripts/ThridParty/jquery.roundabout.min.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/home.index.js") %>" type="text/javascript"></script>
    <link href="<%: Url.Content("~/Content/home.index.css") %>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<h2>VISULAZE YOUR VISITORS</h2>
<h3>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;to manage your business...</h3>
<div>
	<ul class="roundabout">
		<li>
            <div>
            <img src="/Resources/eye_track.jpg"/>
            <h3>Eye tracker</h3>
            <h4>Know what people look at!</h4>
            </div>
        </li>
		<li>
            <div>
            <img src="/Resources/play_back.jpg" />
            <h3>Play Back</h3>
            <h4>Rewind any session!</h4>
            </div>
        </li>
		<li>
            <div>
            <img src="/Resources/touch_map.jpg"/>
            <h3>Touch Map</h3>
            <h4>Get to see every touch!</h4>
            </div>
        </li>
	</ul>
</div>
<a href="/Account/Register" class="btn-access">REGISTER</a>
</asp:Content>


