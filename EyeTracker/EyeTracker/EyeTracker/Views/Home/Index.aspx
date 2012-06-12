<%@ Import Namespace="EyeTracker.Model.Pages.Home" %>
<%@ Page Language="C#" 
MasterPageFile="~/Views/Shared/BeforeLogin.Master" 
Inherits="ViewPage<ViewModelWrapper<BeforeLoginMasterModel, IndexModel>>" %>

<asp:Content ID="PageTitleContent" ContentPlaceHolderID="PageTitleContent" runat="server">Home</asp:Content>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
<script type="text/javascript">
    $(function () {
        $("#faded").faded({
            speed: 500,
            crossfade: true,
            autoplay: 10000,
            autopagination: false
        });

        $('#domain-form').jqTransform({ imgPath: 'jqtransformplugin/img/' });
    });
</script>
<link rel="stylesheet" href="<%: Url.Content("~/Content/template/css/home.index.css") %>" type="text/css" media="all">
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<div id="faded">
	<ul class="slides">
		<li>
            <div>
            <img style="top:18px; position:absolute; right:375px; width:200px;" src="/Resources/eye_track.png"/>
            <h3 style="position: absolute;right: 62px;top: 92px;width: 200px;">Eye Tracker</h3>
            <h4 style="position: absolute;right: 62px;top: 147px;width: 200px;">Know what people look at!</h4>
            </div>
            <a href="/account/register"><span><span>Register</span></span></a>
        </li>
		<li>
            <div>
            <img style="top:18px; position:absolute; right:375px; width:200px;" src="/Resources/touch_map.png"/>
            <h3 style="position: absolute;right: 62px;top: 92px;width: 200px;">Touch Map</h3>
            <h4 style="position: absolute;right: 62px;top: 147px;width: 200px;">Get to see every touch!</h4>
            </div>
            <a href="/account/register"><span><span>Register</span></span></a>
        </li>
		<li>
            <div>
            <img style="top:18px; position:absolute; right:375px; width:200px;" src="/Resources/play_back.png"/>
            <h3 style="position: absolute;right: 62px;top: 92px;width: 200px;">Play Back</h3>
            <h4 style="position: absolute;right: 62px;top: 147px;width: 200px;">Rewind any session!</h4>
            </div>
            <a href="/account/register"><span><span>Register</span></span></a>
         </li>
		 <li>
            <div>
            <img style="top:18px; position:absolute; right:375px; width:200px;" src="/Resources/life_cycle.png"/>
            <h3 style="position: absolute;right: 62px;top: 92px;width: 200px;">Life-cycle</h3>
            <h4 style="position: absolute;right: 62px;top: 147px;width: 200px;">Understand UI changes!</h4>
            </div>
            <a href="/account/register"><span><span>Register</span></span></a>
         </li>
	</ul>
	<ul class="pagination">
		<li><a href="/products/eye-track" rel="0"><span>Eye tracker</span><small>Get more information</small></a></li>
		<li><a href="/products/touch-map" rel="1"><span>Touch Map</span><small>Get more information</small></a></li>
		<li><a href="/products/play-back" rel="2"><span>Play Back</span><small>Get more information</small></a></li>
		<li><a href="/account/register" rel="3"><span>Life-cycle</span><small>Get more information</small></a></li>
	</ul>
</div>
<div class="inside">
	<div class="wrapper row-1">
		<div class="box col-1 maxheight">
			<div class="border-right maxheight">
				<div class="border-bot maxheight">
					<div class="border-left maxheight">
						<div class="left-top-corner maxheight">
							<div class="right-top-corner maxheight">
								<div class="right-bot-corner maxheight">
									<div class="left-bot-corner maxheight">
										<div class="inner">
											<h3>Touch Map</h3>
											<p>Visualized reports of end-user attention held by each area, on every page of a given application.</p>
											<div class="aligncenter"><a href="/products/touch-map" class="link1"><span><span>Learn More</span></span></a></div>
										</div>
									</div>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
		<div class="box col-2 maxheight">
			<div class="border-right maxheight">
				<div class="border-bot maxheight">
					<div class="border-left maxheight">
						<div class="left-top-corner maxheight">
							<div class="right-top-corner maxheight">
								<div class="right-bot-corner maxheight">
									<div class="left-bot-corner maxheight">
										<div class="inner">
											<h3>Eye Track</h3>
											<p>Realize what your visitors look at or read, and what part of content was completely skipped over.</p>
											<div class="aligncenter"><a href="/products/eye-track" class="link1"><span><span>Learn More</span></span></a></div>
										</div>
									</div>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
		<div class="box col-3 maxheight">
			<div class="border-right maxheight">
				<div class="border-bot maxheight">
					<div class="border-left maxheight">
						<div class="left-top-corner maxheight">
							<div class="right-top-corner maxheight">
								<div class="right-bot-corner maxheight">
									<div class="left-bot-corner maxheight">
										<div class="inner">
											<h3>Play Back</h3>
											<p>Rewind or watch your visitors' full browsing sessions to discover exactly how they use your application.</p>
											<div class="aligncenter"><a href="/products/play-back" class="link1"><span><span>Learn More</span></span></a></div>
										</div>
									</div>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>
</div>


<%--<h2>VISULAZE YOUR VISITORS</h2>
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
--%>
<%--<div class="middle-panel">
    <asp:ContentPlaceHolder ID="ContentPlaceHolder1" runat="server" />
</div>
<div class="features">
    <div>
        <h3>Touch Map</h3>
        <p>Visualized reports of end-user attention held by each area, on every page of a given application.</p>
        <div class="action"><a href="/products/touch-map">Read more</a></div>
    </div>
    <div>
        <h3>Play Back</h3>
        <p>Rewind or watch your visitors' full browsing sessions to discover exactly how they use your application.</p>
        <div class="action"><a href="/products/play-back">Read more</a></div>
    </div>
    <div>
        <h3>Eye Track</h3>
        <p>Realize what your visitors look at or read, and what part of content was completely skipped over.</p>
        <div class="action"><a href="/products/eye-track">Read more</a></div>
    </div>
    <p style="clear:both;"></p>
</div>--%>
</asp:Content>


