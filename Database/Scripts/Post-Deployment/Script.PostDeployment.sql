/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

DELETE [cont].[Items]
GO
DELETE [cont].[Keys]
GO
DELETE [cont].[Mails]
GO
DELETE [cont].[Pages] 
GO
DELETE [cont].[Themes]  
GO

INSERT INTO [cont].[Themes] ([ID], [Name], [Url], [Type])
VALUES (1, 'System Email', 'Mails\System.aspx', 1),
		(2, 'Promotion Email', 'Mails\Promotion.aspx', 2),
		(3, 'Base page', 'Home\PageContent.aspx', 3);
GO

--=====================  Pages =========================-

GO
SET IDENTITY_INSERT [cont].[Pages] ON

INSERT INTO [cont].[Pages] ([ID], [ThemeID], [Url])
VALUES (1, 3, 'products/touch-map'),
		(2, 3, 'products/play-back'),
		(3, 3, 'products/eye-track'),
		(4, 3, 'thank-you'),
		(5, 3, 'activation-email-sent'),
		(6, 3, 'account-activated'),
		(7, 3, 'forgot-password-email-sent'),
		(8, 3, 'unsubscrubed-successful'),
		(9, 3, 'products'),
		(10, 3, 'planandpricing'),
		(11, 3, 'm/products/touch-map'),
		(12, 3, 'm/products/play-back'),
		(13, 3, 'm/products/eye-track'),
		(14, 3, 'm/products'),
		(15, 3, 'm/planandpricing'),
		(16, 3, 'special-access-required'),
		(17, 3, 'tutorials');

SET IDENTITY_INSERT [cont].[Pages] OFF
GO		
INSERT INTO [cont].[Items]([PageID], [IsHTML], [SubKey], [Value])
VALUES (1, 0, 'title', 'Touch Map'),
		(1, 1, 'content', '<article class="col-1-2">
							<h2>Products</h2>
							<ul class="list1">
								<li><a href="/p/products/touch-map">Touch Map</a></li>
								<li><a href="/p/products/eye-track">Eye Track</a></li>
								<li><a href="/p/products/play-back">Play Back</a></li>
							</ul>
							</article><article class="col-2-2"><h2>Touch map:</h2>
<p><strong>Touch Map</strong> gives you the marketing power to know everywhere your visitors touch on your application, whether it''s links, images, text or dead space. Know what ads, images and links aren''t getting enough click attention and what call to action buttons are being completely ignored.</p>
<img alt="Touch map preview" style="float:right;margin-left:20px;" src="/Resources/TouchMap1.jpg" />
<p><strong>Discover if visitors are clicking on</strong> parts of your web pages that aren''t links but should be. Are visitors clicking on your images and text, such as “Special Offers” banners, buttons or icons, without going anywhere? If these critical page elements are not linked to anything you are losing potential customers and sales!</p>
<p><strong>Quickly and easily conduct A/B testing</strong> to dramatically increase your conversion rates. Learn how even the smallest design and layout changes can help improve your visitors'' interactions and ultimately increase your ROI (return on investment).</p>
<p><strong>Conduct accurate eye-tracking on a massive scale at a fraction of the cost.</strong> Now Eye-tracking is open to everyone and at incredibly affordable pricing.
Having the cumulative statistics of your website visitor’s interaction within your website will empower you to design one of the most attractive and efficient profit pulling marketing websites on the Internet.  Your visitors are visually showing you what they want and expect of you to make them your profit source.  Eye-tracking allows you to listen and perform for them!</p>
<p><strong>Now Eye-tracking is open to everyone and at an incredibly affordable price.</p>
</article>'),
		(2, 0, 'title', 'Play Back'),
		(2, 1, 'content', '<article class="col-1-2">
							<h2>Products</h2>
							<ul class="list1">
								<li><a href="/p/products/touch-map">Touch Map</a></li>
								<li><a href="/p/products/eye-track">Eye Track</a></li>
								<li><a href="/p/products/play-back">Play Back</a></li>
							</ul>
							</article><article class="col-2-2">
<h2>Play back</h2>
<p>Do you need to track what your users are doing with your application? PlayBack is like looking right over their shoulder and helping them every step of the way. Understand exactly how your customers use your application to know how to improve it.</p>
<img src="/Resources/play_back1.jpg" alt="Play back preview" style="float:right;margin-left:20px;">
<p><strong>Help</strong> your users to understand your application and make the most out of it. <br />
                    <strong>Rewind and watch</strong> their browsing sessions to see what they looked at and how they used it. <br />
                    <strong>Improve</strong> the user experience of your application.<br />
                    <strong>See</strong> what your users do with your app.<br />
                    <strong>Fix</strong> the UI so everything makes sense for your customers.<br /><br />
                    PlayBack is a powerful analysis tool. Improve your app to have the best user experience possible using the data you collect. 
                    If your application is a great one, people will want to use it. </p>
<div style="clear:both;"></div></article>'),
		(3, 0, 'title', 'Eye Track'),
		(3, 1, 'content', '<article class="col-1-2">
							<h2>Products</h2>
							<ul class="list1">
								<li><a href="/p/products/touch-map">Touch Map</a></li>
								<li><a href="/p/products/eye-track">Eye Track</a></li>
								<li><a href="/p/products/play-back">Play Back</a></li>
							</ul>
							</article><article class="col-2-2"><h2>Eye track</h2>
<p><strong>Understanding</strong> how much attention your visitors pay to a specific spot on your application and knowing what your visitors care about the most, what they read, and what content was completely skipped over is pure gold to increased monetization of your business.</p>
<img src="/Resources/play_back1.jpg" alt="Play back preview" style="float:right;margin-left:20px;">
<p><strong>Knowing what your potential customers are viewing</strong> and clicking on and what they are not will enable you to tweak your website in the most creative and effective way to attract more sales. Give your potential buying visitors the exact information they want in the least amount of time and watch your sales increase dramatically.</p>
<p><strong>Pinpointing your visitor’s hotspots</strong> of product or service interest will allow you to design more hotspots within your web pages. By knowing your visitors browsing habits, you''re closer to writing the best attention grabbing copy that will keep your potential buyers on your website longer resulting in increased percentage of sales.</p>
<p><strong>By identifying the boring areas of your web pages</strong> you will be able to revise and monitor your edits for best marketing performance indicated by increased viewing and purchase touches. Optimizing ideal ad and button locations dramatically impacts profit performance. Turn skipped over pages into more touched over pages.</p>
<p><strong>Now you can know the fold of your apps!</strong>  Learn what keeps your visitor on pages longer and how far down they are willing to scroll.  Lengthen some pages and shorten others. Turn more visitors into buyers because you give them exactly what they want in the least amount of words and time and do this with the most efficient and profit pulling attractive page layout.</p>
</article>'),
		(4, 0, 'title', 'Thank You'),
		(4, 1, 'content', '<article class="col"><h2 style="color:#333;display:block;font-family:Arial;font-size:30px;font-weight:bold;line-height:120%;margin-top:40px;margin-right:0;margin-bottom:15px;margin-left:0;text-align:left">Thank You</h2>
		<p style="margin-bottom:16px">Dear customer,</p>
<p style="margin-bottom:16px">Thank you for signing up for our mobile apps analytical service. Due to high demand
        for our analytics services, we are constantly increasing our capacity and processing
        hundreds of new customers weekly. Now you are in fast moving line and we shall open
        your account within two weeks, notification will be sent to your email.</p>
<p style="margin-bottom:16px">Thanks again for your patience.</p>
<p style="margin-bottom:16px"><em>Looking forward to serve you,<br>Mobillify team</em></p></article>'),
		(5, 0, 'title', 'Activation email was sent'),
		(5, 1, 'content', ' <article class="col center"><h2>Activation email was sent</h2>
							<p>Please check your email.</p>
							<p>And use activation link in the email to activate your account.</p>
							</article>'),
		(6, 0, 'title', 'Your account was activated'),
		(6, 1, 'content', ' <article class="col center"><h2>Your account was activated</h2>
							<p>Account was activated.</p>
							<p>You can use your user name and password to log in.</p>
							<p><a href="/Account/LogOn" class="link2"><span><span>Log On</span></span></a></p>
							</article>'),
		(7, 0, 'title', 'Forgot password email was sent'),
		(7, 1, 'content', '<article class="col center"><h2>Forgot password email was sent</h2></article>'),
		(8, 0, 'title', 'Unsubscrubed successful'),
		(8, 1, 'content', '<article class="col center"><h2>Unsubscrubed successful</h2></article>'),
		(9, 0, 'title', 'Products'),
		(9, 1, 'content', '<article class="col-1-3">
                <h2>
                    TouchMap</h2>
                <a href="/p/products/touch-map">
                    <img alt="" src="/Resources/TouchMap1.jpg" style="width: 270px;" /></a>
                <p>
                    <strong>TouchMap</strong> is a unique technology which analyzes touches in any area
                    of your application. Links, images, text or empty spaces - see what’s being clicked
                    and what parts users ignore. Use this data in order to:</p>
                <ul class="list2">
                    <li>Improve designs to increase visitor interaction</li>
                    <li>Improve your ROI (Return on Investment) by placing important content and banners
                        at the “right” places</li>
                    <li>See how page changes improve sales</li>
                </ul>
                <a href="/p/products/touch-map" class="link2"><span><span>Read more</span></span></a>
            </article>
            <article class="col-2-3">
                <h2>
                    EyeTrack</h2>
                <a href="/p/products/eye-track">
                    <img alt="" src="/Resources/EyeTrack2.jpg" style="width: 270px;" /></a>
                <p>
                    The<strong> EyeTrack</strong> technology will indicate for you how much time and
                    attention your application or website users spend per each part of the content.
                    You’ll know what they read and what they skip over. You’ll be able to fit your content
                    to match the interests of your visitors. Attract their attention to make them stay
                    on your app and make more sales.
                </p>
                <ul class="list2">
                    <li>Know your fold and how far visitors scroll your content</li>
                    <li>Decide which pages need to be shorten or lengthen</li>
                    <li>Rearrange your page layout to make the most of your content</li>
                    <li>Identify the boring content in your application that is usually skipped</li>
                    <li>Boost your advertisement revenues by placing ads on high traffic page areas.</li>
                </ul>
                <p>
                    EyeTrack is available for everyone with affordable rates.</p>
                <a href="/p/products/eye-track" class="link2"><span><span>Read more</span></span></a>
            </article>
            <article class="col-3-3">
                <h2>
                    PlayBack</h2>
                <a href="/p/products/play-back">
                    <img alt="" src="/Resources/play_back1.jpg" style="width: 270px;" /></a>
                <p>
                    Use <strong>PlayBack</strong> to make the most out of your applications. Improve
                    them using the data you collect for your users to have the best experience possible.
                    Play back and understand exactly how your customers use your application to know
                    how to improve it.</p>
                <ul class="list2">
                    <li>Track the user experience of your application.</li>
                    <li>See what users do with your app.</li>
                    <li>Fix the UI so everything makes sense for your customers.</li>
                </ul>
                <a href="/p/products/play-back" class="link2"><span><span>Read more</span></span></a>
            </article>
			<div style="clear:both;"></div>'),
		(10, 0, 'title', 'Plan And Pricing'),
		(10, 1, 'content', '<div class="wrapper row-1">
					<div class="box col-1-3 maxheight">
						<div class="border-right maxheight">
							<div class="border-bot maxheight">
								<div class="border-left maxheight">
									<div class="left-top-corner maxheight">
										<div class="right-top-corner maxheight">
											<div class="right-bot-corner maxheight">
												<div class="left-bot-corner maxheight">
													<div class="inner">
														<h3>Basic</h3>
														<ul class="info-list">
															<li><span>Active applications</span>up to 3</li>
															<li><span>Sessions per application</span>1000</li>
															<li><span>Eye-Track</span>No</li>
															<li><span>Play-Back</span>No</li>
															<li><span>Alerts</span>Weekly</li>
														</ul>
														<span class="price">Free</span>
														<div class="aligncenter"><a href="/account/register" class="link1"><span><span>Learn More</span></span></a></div>
													</div>
												</div>
											</div>
										</div>
									</div>
								</div>
							</div>
						</div>
					</div>
					<div class="box col-2-3 maxheight">
						<div class="border-right maxheight">
							<div class="border-bot maxheight">
								<div class="border-left maxheight">
									<div class="left-top-corner maxheight">
										<div class="right-top-corner maxheight">
											<div class="right-bot-corner maxheight">
												<div class="left-bot-corner maxheight">
													<div class="inner">
														<h3>Plus</h3>
														<ul class="info-list">
															<li><span>Active applications</span>up to 5</li>
															<li><span>Sessions per application</span>10000</li>
															<li><span>Eye-Track</span>Yes</li>
															<li><span>Play-Back</span>Yes</li>
															<li><span>Alerts</span>Daily</li>
														</ul>
														<span class="price">$ 49 p/m</span>
														<div class="aligncenter"><a href="/account/register" class="link1"><span><span>Learn More</span></span></a></div>
													</div>
												</div>
											</div>
										</div>
									</div>
								</div>
							</div>
						</div>
					</div>
					<div class="box col-3-3 maxheight">
						<div class="border-right maxheight">
							<div class="border-bot maxheight">
								<div class="border-left maxheight">
									<div class="left-top-corner maxheight">
										<div class="right-top-corner maxheight">
											<div class="right-bot-corner maxheight">
												<div class="left-bot-corner maxheight">
													<div class="inner">
														<h3>Pro</h3>
														<ul class="info-list">
															<li><span>Active applications</span>up to 15</li>
															<li><span>Sessions per application</span>30000</li>
															<li><span>Eye-Track</span>Yes</li>
															<li><span>Play-Back</span>Yes</li>
															<li><span>Alerts</span>Hourly</li>
														</ul>
														<span class="price">$ 99 p/m</span>
														<div class="aligncenter"><a href="/account/register" class="link1"><span><span>Learn More</span></span></a></div>
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
'),
(11, 0, 'title', 'Touch Map'),
		(11, 1, 'content', '<article class="col-1-2">
							<h2>Products</h2>
							<ul class="list1">
								<li><a href="/m/products/touch-map">Touch Map</a></li>
								<li><a href="/m/products/eye-track">Eye Track</a></li>
								<li><a href="/m/products/play-back">Play Back</a></li>
							</ul>
							</article><article class="col-2-2"><h2>Touch map:</h2>
<p><strong>Touch Map</strong> gives you the marketing power to know everywhere your visitors touch on your application, whether it''s links, images, text or dead space. Know what ads, images and links aren''t getting enough click attention and what call to action buttons are being completely ignored.</p>
<img alt="Touch map preview" style="float:right;margin-left:20px;" src="/Resources/TouchMap1.jpg" />
<p><strong>Discover if visitors are clicking on</strong> parts of your web pages that aren''t links but should be. Are visitors clicking on your images and text, such as “Special Offers” banners, buttons or icons, without going anywhere? If these critical page elements are not linked to anything you are losing potential customers and sales!</p>
<p><strong>Quickly and easily conduct A/B testing</strong> to dramatically increase your conversion rates. Learn how even the smallest design and layout changes can help improve your visitors'' interactions and ultimately increase your ROI (return on investment).</p>
<p><strong>Conduct accurate eye-tracking on a massive scale at a fraction of the cost.</strong> Now Eye-tracking is open to everyone and at incredibly affordable pricing.
Having the cumulative statistics of your website visitor’s interaction within your website will empower you to design one of the most attractive and efficient profit pulling marketing websites on the Internet.  Your visitors are visually showing you what they want and expect of you to make them your profit source.  Eye-tracking allows you to listen and perform for them!</p>
<p><strong>Now Eye-tracking is open to everyone and at an incredibly affordable price.</p>
</article>'),
		(12, 0, 'title', 'Play Back'),
		(12, 1, 'content', '<article class="col-1-2">
							<h2>Products</h2>
							<ul class="list1">
								<li><a href="/m/products/touch-map">Touch Map</a></li>
								<li><a href="/m/products/eye-track">Eye Track</a></li>
								<li><a href="/m/products/play-back">Play Back</a></li>
							</ul>
							</article><article class="col-2-2">
<h2>Play back</h2>
<p>Do you need to track what your users are doing with your application? PlayBack is like looking right over their shoulder and helping them every step of the way. Understand exactly how your customers use your application to know how to improve it.</p>
<img src="/Resources/play_back1.jpg" alt="Play back preview" style="float:right;margin-left:20px;">
<p><strong>Help</strong> your users to understand your application and make the most out of it. <br />
                    <strong>Rewind and watch</strong> their browsing sessions to see what they looked at and how they used it. <br />
                    <strong>Improve</strong> the user experience of your application.<br />
                    <strong>See</strong> what your users do with your app.<br />
                    <strong>Fix</strong> the UI so everything makes sense for your customers.<br /><br />
                    PlayBack is a powerful analysis tool. Improve your app to have the best user experience possible using the data you collect. 
                    If your application is a great one, people will want to use it. </p>
<div style="clear:both;"></div></article>'),
		(13, 0, 'title', 'Eye Track'),
		(13, 1, 'content', '<article class="col-1-2">
							<h2>Products</h2>
							<ul class="list1">
								<li><a href="/m/products/touch-map">Touch Map</a></li>
								<li><a href="/m/products/eye-track">Eye Track</a></li>
								<li><a href="/m/products/play-back">Play Back</a></li>
							</ul>
							</article><article class="col-2-2"><h2>Eye track</h2>
<p><strong>Understanding</strong> how much attention your visitors pay to a specific spot on your application and knowing what your visitors care about the most, what they read, and what content was completely skipped over is pure gold to increased monetization of your business.</p>
<img src="/Resources/play_back1.jpg" alt="Play back preview" style="float:right;margin-left:20px;">
<p><strong>Knowing what your potential customers are viewing</strong> and clicking on and what they are not will enable you to tweak your website in the most creative and effective way to attract more sales. Give your potential buying visitors the exact information they want in the least amount of time and watch your sales increase dramatically.</p>
<p><strong>Pinpointing your visitor’s hotspots</strong> of product or service interest will allow you to design more hotspots within your web pages. By knowing your visitors browsing habits, you''re closer to writing the best attention grabbing copy that will keep your potential buyers on your website longer resulting in increased percentage of sales.</p>
<p><strong>By identifying the boring areas of your web pages</strong> you will be able to revise and monitor your edits for best marketing performance indicated by increased viewing and purchase touches. Optimizing ideal ad and button locations dramatically impacts profit performance. Turn skipped over pages into more touched over pages.</p>
<p><strong>Now you can know the fold of your apps!</strong>  Learn what keeps your visitor on pages longer and how far down they are willing to scroll.  Lengthen some pages and shorten others. Turn more visitors into buyers because you give them exactly what they want in the least amount of words and time and do this with the most efficient and profit pulling attractive page layout.</p>
</article>'),
(14, 0, 'title', 'Products'),
		(14, 1, 'content', '<article class="col-1-3">
                <h2>
                    TouchMap</h2>
                <a href="/m/products/touch-map">
                    <img alt="" src="/Resources/TouchMap1.jpg" style="width: 270px;" /></a>
                <p>
                    <strong>TouchMap</strong> is a unique technology which analyzes touches in any area
                    of your application. Links, images, text or empty spaces - see what’s being clicked
                    and what parts users ignore. Use this data in order to:</p>
                <ul class="list2">
                    <li>Improve designs to increase visitor interaction</li>
                    <li>Improve your ROI (Return on Investment) by placing important content and banners
                        at the “right” places</li>
                    <li>See how page changes improve sales</li>
                </ul>
                <a href="/m/products/touch-map" class="link2"><span><span>Read more</span></span></a>
            </article>
            <article class="col-2-3">
                <h2>
                    EyeTrack</h2>
                <a href="/m/products/eye-track">
                    <img alt="" src="/Resources/EyeTrack2.jpg" style="width: 270px;" /></a>
                <p>
                    The<strong> EyeTrack</strong> technology will indicate for you how much time and
                    attention your application or website users spend per each part of the content.
                    You’ll know what they read and what they skip over. You’ll be able to fit your content
                    to match the interests of your visitors. Attract their attention to make them stay
                    on your app and make more sales.
                </p>
                <ul class="list2">
                    <li>Know your fold and how far visitors scroll your content</li>
                    <li>Decide which pages need to be shorten or lengthen</li>
                    <li>Rearrange your page layout to make the most of your content</li>
                    <li>Identify the boring content in your application that is usually skipped</li>
                    <li>Boost your advertisement revenues by placing ads on high traffic page areas.</li>
                </ul>
                <p>
                    EyeTrack is available for everyone with affordable rates.</p>
                <a href="/m/products/eye-track" class="link2"><span><span>Read more</span></span></a>
            </article>
            <article class="col-3-3">
                <h2>
                    PlayBack</h2>
                <a href="/m/products/play-back">
                    <img alt="" src="/Resources/play_back1.jpg" style="width: 270px;" /></a>
                <p>
                    Use <strong>PlayBack</strong> to make the most out of your applications. Improve
                    them using the data you collect for your users to have the best experience possible.
                    Play back and understand exactly how your customers use your application to know
                    how to improve it.</p>
                <ul class="list2">
                    <li>Track the user experience of your application.</li>
                    <li>See what users do with your app.</li>
                    <li>Fix the UI so everything makes sense for your customers.</li>
                </ul>
                <a href="/m/products/play-back" class="link2"><span><span>Read more</span></span></a>
            </article>
			<div style="clear:both;"></div>'),
		(15, 0, 'title', 'Plan And Pricing'),
		(15, 1, 'content', '<style>
    .box{font-size:13px;width:290px;margin: 30px auto;background: url("/Content/template/images/box-tail.gif") repeat-x scroll left top #FFFFFF;}
    .box .border-right { background: url("/Content/template/images/border-right.gif") repeat-y scroll right top transparent;}
    .box .border-bot { background: url("/Content/template/images/border-bot.gif") repeat-x scroll left bottom transparent;}
    .box .border-left { background: url("/Content/template/images/border-left.gif") repeat-y scroll left top transparent;}
    .box .left-top-corner { background: url("/Content/template/images/left-top-corner.gif") no-repeat scroll left top transparent;}
    .box .right-top-corner { background: url("/Content/template/images/right-top-corner.gif") no-repeat scroll right top transparent; }
    .box .right-bot-corner { background: url("/Content/template/images/right-bot-corner.gif") no-repeat scroll right bottom transparent; }
    .box .left-bot-corner { background: url("./Content/template/images/left-bot-corner.gif") no-repeat scroll left bottom transparent; width: 100%;}
    .box .inner { padding: 15px 38px 26px 43px;}
    .box h3 { color: #FFFFFF; font-size: 26px; line-height: 1.2em; margin: 0px 0px 30px 0px; font-weight:normal;}
    .box .info-list { padding-bottom: 5px; }
    .box ul { list-style: none outside none;margin:0;padding:0;}
    .box .info-list li { border-bottom: 1px solid #DFDFDF; margin: 0 0 6px -5px; overflow: hidden; padding: 0 0 6px 5px; text-align: right;vertical-align: top;width: 100%;}
    .box .info-list li span {float: left;}
    .box .price {color:	#464646; display: block; font-size: 30px; letter-spacing: -1px; line-height: 1.2em; padding-bottom: 10px; text-align: center;}
    .box .aligncenter {text-align: center;}
</style>						
<div class="box col-1-3">
    <div class="border-right">
		<div class="border-bot">
			<div class="border-left">
				<div class="left-top-corner">
					<div class="right-top-corner">
						<div class="right-bot-corner">
							<div class="left-bot-corner">
								<div class="inner">
									<h3>Basic</h3>
									<ul class="info-list">
										<li><span>Active applications</span>up to 3</li>
										<li><span>Sessions per application</span>1000</li>
										<li><span>Eye-Track</span>No</li>
										<li><span>Play-Back</span>No</li>
										<li><span>Alerts</span>Weekly</li>
									</ul>
									<span class="price">Free</span>
									<div class="aligncenter"><a data-role="button" data-inline="true" data-transition="slide" data-icon="arrow-r" data-iconpos="right" href="/m/account/register">Learn More</a></div>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>
</div>
<div class="box col-2-3 maxheight">
	<div class="border-right maxheight">
		<div class="border-bot maxheight">
			<div class="border-left maxheight">
				<div class="left-top-corner maxheight">
					<div class="right-top-corner maxheight">
						<div class="right-bot-corner maxheight">
							<div class="left-bot-corner maxheight">
								<div class="inner">
									<h3>Plus</h3>
									<ul class="info-list">
										<li><span>Active applications</span>up to 5</li>
										<li><span>Sessions per application</span>10000</li>
										<li><span>Eye-Track</span>Yes</li>
										<li><span>Play-Back</span>Yes</li>
										<li><span>Alerts</span>Daily</li>
									</ul>
									<span class="price">$ 49 p/m</span>
									<div class="aligncenter"><a data-role="button" data-inline="true" data-transition="slide" data-icon="arrow-r" data-iconpos="right" href="/m/account/register">Learn More</a></div>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>
</div>
<div class="box col-3-3 maxheight">
	<div class="border-right maxheight">
		<div class="border-bot maxheight">
			<div class="border-left maxheight">
				<div class="left-top-corner maxheight">
					<div class="right-top-corner maxheight">
						<div class="right-bot-corner maxheight">
							<div class="left-bot-corner maxheight">
								<div class="inner">
									<h3>Pro</h3>
									<ul class="info-list">
										<li><span>Active applications</span>up to 15</li>
										<li><span>Sessions per application</span>30000</li>
										<li><span>Eye-Track</span>Yes</li>
										<li><span>Play-Back</span>Yes</li>
										<li><span>Alerts</span>Hourly</li>
									</ul>
									<span class="price">$ 99 p/m</span>
									<div class="aligncenter"><a data-role="button" data-inline="true" data-transition="slide" data-icon="arrow-r" data-iconpos="right" href="/m/account/register">Learn More</a></div>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>
</div>'),
		(16, 0, 'title', 'Special Access Required'),
		(16, 1, 'content', ' <article class="col center"><h2>Special Access Required</h2>
							<p>At this stage we provide access just for selected number of users, </br>if you would like to get the access please contact us by the email: <a href="mailto:support@mobillify.com">support@mobillify.com</a>.</p>
							</article>'),
		(17, 0, 'title', 'Get Started'),
		(17, 1, 'content', '<h2>Getting Started</h2>
<p dir="LTR">
    <strong>Requirements</strong>
    <strong></strong>
</p>
<p dir="LTR">
    To integrate FingerPrint with your Android app, you will need the following:
    <br/><a href="/Packages/fingerprint-{AndroidPackageVersion}.jar" target="_blank">Android Package {AndroidPackageVersion}</a>
    <br/><a href="/Packages/fingerprint.properties" target="_blank">FingerPrint-{AndroidPackageVersion}.properties</a>
</p>
<p dir="LTR">
    <strong>Setup</strong>
    <strong></strong>
</p>
<ol start="1" type="1">
    <li dir="LTR">
        &#183; Add fingerptint.jar to your project''s <em><strong>/libs</strong></em> folder in your project.
    </li>
    <li dir="LTR">
        &#183; Add fingerprint.properties file to <em><strong>/assets</strong></em> folder in your project.
    </li>
    <li dir="LTR">
        &#183; Add the following permissions to your project''s AndroidManifest.xml manifest file:
    </li>
</ol>
<p dir="LTR">
    &lt;uses-permission android:name="android.permission.INTERNET" /&gt;
</p>
<p dir="LTR">
    &lt;uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" /&gt;
</p>
<p dir="LTR">
    <br/>
    Add a new application to your Portfolio and create a AppID.
</p>
<p dir="LTR">
    <strong>Starting the Tracker</strong>
    <strong></strong>
</p>
<p dir="LTR">
    1. Call <em>FingerPrint.init</em><em>(this)</em> method in <em><strong>onCreate</strong></em> method of your activity.
</p>
<p dir="LTR">
    2. Call <em>FingerPrint.start</em><em>(this)</em> method in <em><strong>onStart</strong></em> method
</p>
<p dir="LTR">
    3. Call <em>FingerPrint.finsih</em><em>(</em><em>this)</em> in <em><strong>onStop</strong></em> method passing the View''s URI and activity being tracked.
    <br/>
<pre class="java">
import com.mobillify.fingerprint.FingerPrint;
import android.app.Activity;
import android.os.Bundle;
import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnTouchListener;
import android.widget.ImageView;

public class TestActivity extends Activity {

protected void onCreate(Bundle savedInstanceState) {
super.onCreate(savedInstanceState);
setContentView(R.layout.main);
FingerPrint.init(viewUri);
///////////////////////////////////////////////////////////////
//// Only if you are setting custom onTouchListener,
//call FingerPrint.onTouch(view, event) method explicitly
//insert FingerPrint.onTouch(view, event) method call
///////////////////////////////////////////////////////////////
ImageView imageView = (ImageView)findViewById(R.id.ImageView);
imageView.setOnTouchListener(new OnTouchListener() {
@Override
public boolean onTouch(View view, MotionEvent event) {
//Only if you are overriding onTouch method for a certain view, use this method
FingerPrint.onTouch(view, event);
return false;}
////////////////////////////////////////////////////////////////////////
});
}
 
protected void onStart() {
super.onStart();
//Starting FingerPrint service
FingerPrint.start(this);
}
 
@Override
protected void onStop() {
super.onStop();
//Finishing FingerPrint service
FingerPrint.finish(this);
}
}

</pre>
<br/>
</p>');

GO

--=====================  System Mails =========================-

GO
SET IDENTITY_INSERT [cont].[Mails] ON

INSERT INTO [cont].[Mails] ([ID], [IsSystem], [ThemeID], [Url])
VALUES (1, 1, 1, 'mails/activationemail'),
		(2, 1, 1, 'mails/forgotpasswordmail');

SET IDENTITY_INSERT [cont].[Mails] OFF

GO		
INSERT INTO [cont].[Items]([MailID], [IsHTML], [SubKey], [Value])
VALUES (1, 0, 'subject', 'Please activate your account'),
		(1, 1, 'body', 'Please activate your account by the link: {activation_link}'),
		(2, 0, 'subject', 'Reset password'),
		(2, 1, 'body', 'Please use the link to reset password: {reset_password_link}');

GO

--=====================  Promotion Mailes =========================-

GO
SET IDENTITY_INSERT [cont].[Mails] ON

INSERT INTO [cont].[Mails] ([ID], [IsSystem], [ThemeID], [Url])
VALUES (3, 0, 2, 'mails/thank-you');

SET IDENTITY_INSERT [cont].[Mails] OFF

GO		
INSERT INTO [cont].[Items]([MailID], [IsHTML], [SubKey], [Value])
VALUES (3, 0, 'subject', 'Thank You'),
		(3, 1, 'body', '<h2 style="color:#333;display:block;font-family:Arial;font-size:30px;font-weight:bold;line-height:120%;margin-top:40px;margin-right:0;margin-bottom:15px;margin-left:0;text-align:left">Thank You</h2>
		<p style="margin-bottom:16px">Dear customer,</p>
<p style="margin-bottom:16px">Thank you for signing up for our mobile apps analytical service. Due to high demand
        for our analytics services, we are constantly increasing our capacity and processing
        hundreds of new customers weekly. Now you are in fast moving line and we shall open
        your account within two weeks, notification will be sent to your email.</p>
<p style="margin-bottom:16px">Thanks again for your patience.</p>
<p style="margin-bottom:16px"><em>Looking forward to serve you,<br>Mobillify team</em></p>');

GO

--====================== Keys ====================----
SET IDENTITY_INSERT [cont].[Keys] ON

INSERT INTO [cont].[Keys] ([ID], [Url])
VALUES (1, 'account/terms-and-coditions');

SET IDENTITY_INSERT [cont].[Keys] OFF

INSERT INTO [cont].[Items]([KeyID], [IsHTML], [SubKey], [Value])
VALUES (1, 1, 'content', '<h2>ANALYTICS TERMS OF SERVICE</h2>
<p>
These Google Analytics Terms of Service (this "Agreement") are entered into by Google Inc. ("Google") and the entity executing this Agreement ("You"). This Agreement governs Your use of the standard Google Analytics (the "Service"). BY CLICKING THE "I ACCEPT" BUTTON, COMPLETING THE REGISTRATION PROCESS, OR USING THE SERVICE, YOU ACKNOWLEDGE THAT YOU HAVE REVIEWED AND ACCEPT THIS AGREEMENT AND ARE AUTHORIZED TO ACT ON BEHALF OF, AND BIND TO THIS AGREEMENT, THE OWNER OF THIS ACCOUNT. In consideration of the foregoing, the parties agree as follows:
</p>
<h5>1. Definitions.</h5>
<p>
"Account" refers to the billing account for the Service. All Profiles linked to a single Property will have their Hits aggregated before determining the charge for the Service for that Property.
</p><p>
"Confidential Information" includes any proprietary data and any other information disclosed by one party to the other in writing and marked "confidential" or disclosed orally and, within five business days, reduced to writing and marked "confidential". However, Confidential Information will not include any information that is or becomes known to the general public, which is already in the receiving party''s possession prior to disclosure by a party or which is independently developed by the receiving party without the use of Confidential Information.
</p><p>
"Customer Data" means the data concerning the characteristics and activities of Visitors that is collected through use of the GATC and then forwarded to the Servers and analyzed by the Processing Software.
</p><p>
"Documentation" means any accompanying documentation made available to You by Google for use with the Processing Software, including any documentation available online.
</p><p>
"GATC" means the Google Analytics Tracking Code, which is installed on a Property for the purpose of collecting Customer Data, together with any fixes, updates and upgrades provided to You.
</p><p>
"Hit" means the base unit that the Google Analytics system processes. A Hit may be a call to the Google Analytics system by various libraries, including, Javascript (ga.js, urchin.js), Silverlight, Flash, and Mobile. A Hit may currently be a page view, a transaction, item, or event. Hits may also be delivered to the Google Analytics system without using one of the various libraries by other Google Analytics-supported protocols and mechanisms the Service makes available to You.
</p><p>
"Processing Software" means the Google Analytics server-side software and any upgrades, which analyzes the Customer Data and generates the Reports.
</p><p>
"Profile" means the collection of settings that together determine the information to be included in, or excluded from, a particular Report. For example, a Profile could be established to view a small portion of a web site as a unique Report. There can be multiple Profiles established under a single Property.
</p><p>
"Property" means a group of web pages or apps that are linked to an Account and use the same GATC. Each Property includes a default Profile that measures all pages within the Property.
</p><p>
"Privacy Policy" means the privacy policy on a Property.
</p><p>
"Report" means the resulting analysis shown at http://finger.mobillify.com for a Profile.
</p><p>
"Servers" means the servers controlled by Google (or its wholly owned subsidiaries) on which the Processing Software and Customer Data are stored.
</p><p>
"Software" means the GATC and the Processing Software.
</p><p>
"Third Party" means any third party (i) to which You provide access to Your Account or (i) for which You use the Service to collect information on the third party’s behalf.
</p><p>
"Visitors" means visitors to Your Properties.
</p><p>
The words "include" and "including" mean "including but not limited to."
</p>
<h5>2. Fees and Service.</h5>
<p>
Subject to Section 15, the Service is provided without charge to You for up to 10 million Hits per month per account. Google may change its fees and payment policies for the Service from time to time including the addition of costs for geographic data, the importing of cost data from search engines, or other fees charged to Google or its wholly-owned subsidiaries by third party vendors for the inclusion of data in the Service reports. The changes to the fees or payment policies are effective upon Your acceptance of those changes which will be posted at http://finger.mobillify.com. Unless otherwise stated, all fees are quoted in U.S. Dollars. Any outstanding balance becomes immediately due and payable upon termination of this Agreement and any collection expenses (including attorneys'' fees) incurred by Google will be included in the amount owed, and may be charged to the credit card or other billing mechanism associated with Your AdWords account.
</p>
<h5>3. Member Account, Password, and Security.</h5>
<p>
To register for the Service, You must complete the registration process by providing Google with current, complete and accurate information as prompted by the registration form, including Your e-mail address (username) and password. You will protect Your passwords and take full responsibility for Your own, and third party, use of Your accounts. You are solely responsible for any and all activities that occur under Your Account. You will notify Google immediately upon learning of any unauthorized use of Your Account or any other breach of security. Google''s (or its wholly-owned subsidiaries'') support staff may, from time to time, log in to the Service under Your customer password in order to maintain or improve service, including to provide You assistance with technical or billing issues.
</p>
<h5>4. Nonexclusive License.</h5>
<p>
Subject to the terms and conditions of this Agreement, (a) Google grants You a limited, revocable, non-exclusive, non-sublicensable license to install, copy and use the GATC solely as necessary for You to use the Service on Your Properties or Third Party’s Properties; and (b) You may remotely access, view and download Your Reports stored at http://www.google.com/analytics. You will not (and You will not allow any third party to) (i) copy, modify, adapt, translate or otherwise create derivative works of the Software or the Documentation; (ii) reverse engineer, decompile, disassemble or otherwise attempt to discover the source code of the Software, except as expressly permitted by the law in effect in the jurisdiction in which You are located; (iii) rent, lease, sell, assign or otherwise transfer rights in or to the Software, the Documentation or the Service; (iv) remove any proprietary notices or labels on the Software or placed by the Service; (v) use, post, transmit or introduce any device, software or routine which interferes or attempts to interfere with the operation of the Service or the Software; or (vi) use data labeled as belonging to a third party in the Service for purposes other than generating, viewing, and downloading Reports. You will comply with all applicable laws and regulations in Your use of and access to the Documentation, Software, Service and Reports.
</p>
<h5>5. Confidentiality.</h5>
<p>
Neither party will use or disclose the other party''s Confidential Information without the other''s prior written consent except for the purpose of performing its obligations under this Agreement or if required by law, regulation or court order; in which case, the party being compelled to disclose Confidential Information will give the other party as much notice as is reasonably practicable prior to disclosing the Confidential Information. Upon termination of this Agreement, the parties will promptly either return or destroy all Confidential Information and, upon request, provide written certification of such.
</p>
<h5>6. Information Rights and Publicity.</h5>
<p>
Google and its wholly owned subsidiaries may retain and use, subject to the terms of its privacy policy (located at http://www.google.com/privacy.html), information collected in Your use of the Service. Google will not share Your Customer Data or any Third Party’s Customer Data with any third parties unless Google (i) has Your consent for any Customer Data or any Third Party’s consent for the Third Party’s Customer Data; (ii) concludes that it is required by law or has a good faith belief that access, preservation or disclosure of Customer Data is reasonably necessary to protect the rights, property or safety of Google, its users or the public; or (iii) provides Customer Data in certain limited circumstances to third parties to carry out tasks on Google''s behalf (e.g., billing or data storage) with strict restrictions that prevent the data from being used or shared except as directed by Google. When this is done, it is subject to agreements that oblige those parties to process Customer Data only on Google''s instructions and in compliance with this Agreement and appropriate confidentiality and security measures.
</p>
<h5>7. Privacy.</h5>
<p>
You will not (and will not allow any third party to) use the Service to track, collect or upload any data that personally identifies an individual (such as a name, email address or billing information), or other data which can be reasonably linked to such information by Google. You will have and abide by an appropriate Privacy Policy and will comply with all applicable laws and regulations relating to the collection of information from Visitors. You must post a Privacy Policy and that Privacy Policy must provide notice of Your use of cookies that are used to collect traffic data, and You must not circumvent any privacy features (e.g., an opt-out) that are part of the Service.
</p><p>
You may participate in an integrated version of Google Analytics and any DoubleClick product or service or any other Google display ads product or service ("Google Analytics for Display Advertisers"). If You use Google Analytics for Display Advertisers, You will comply with the Google Analytics for Display Advertisers Policy (available at http://support.google.com/analytics/bin/answer.py?hl=en&topic=2611283&answer=2700409) and, as set forth in the policy, disclose in Your Privacy Policy (i) Your use of Google Analytics for Display Advertisers and its features You use, and (ii) how Visitors can opt-out from Google Analytics for Display Advertisers. Your access to and use of any DoubleClick or Google display ads data is subject to the applicable terms between You and Google.
</p>
<h5>8. Indemnification.</h5>
<p>
To the extent permitted by applicable law, You will indemnify, hold harmless and defend Google and its wholly owned subsidiaries, at Your expense, from any and all third-party claims, actions, proceedings, and suits brought against Google or any of its officers, directors, employees, agents or affiliates, and all related liabilities, damages, settlements, penalties, fines, costs or expenses (including, reasonable attorneys'' fees and other litigation expenses) incurred by Google or any of its officers, directors, employees, agents or affiliates, arising out of or relating to (i) Your breach of any term or condition of this Agreement, (ii) Your use of the Service, (iii) Your violations of applicable laws, rules or regulations in connection with the Service, (iv) any representations and warranties made by You concerning any aspect of the Service, the Software or Reports to any Third Party; (v) any claims made by or on behalf of any Third Party pertaining directly or indirectly to Your use of the Service, the Software or Reports; (vi) violations of Your obligations of privacy to any Third Party; and (vii) any claims with respect to acts or omissions of any Third Party in connection with the Service, the Software or Reports. Google will provide You with written notice of any claim, suit or action from which You must indemnify Google. You will cooperate as fully as reasonably required in the defense of any claim. Google reserves the right, at its own expense, to assume the exclusive defense and control of any matter subject to indemnification by You.
</p>
<h5>9. Third Parties.</h5>
<p>
If You use the Service on behalf of the Third Party or a Third Party otherwise uses the Service through Your Account, whether or not You are authorized by Google to do so, then You represent and warrant that (a) You are authorized to act on behalf of, and bind to this Agreement, the Third Party to all obligations that You have under this Agreement, (b) Google may share with the Third Party any Customer Data that is specific to the Third Party’s Properties, and (c) You will not disclose Third Party''s Customer Data to any other party without the Third Party''s consent.
</p>
<h5>10. DISCLAIMER OF WARRANTIES.</h5>
<p>
TO THE FULLEST EXTENT PERMITTED BY APPLICABLE LAW, EXCEPT AS EXPRESSLY PROVIDED FOR IN THIS AGREEMENT, GOOGLE MAKES NO OTHER WARRANTY OF ANY KIND, WHETHER EXPRESS, IMPLIED, STATUTORY OR OTHERWISE, INCLUDING WITHOUT LIMITATION WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR USE AND NONINFRINGEMENT.
</p>
<h5>11. LIMITATION OF LIABILITY.</h5>
<p>
TO THE EXTENT PERMITTED BY APPLICABLE LAW, GOOGLE WILL NOT BE LIABLE FOR YOUR LOST REVENUES OR INDIRECT, SPECIAL, INCIDENTAL, CONSEQUENTIAL, EXEMPLARY, OR PUNITIVE DAMAGES, EVEN IF THE GOOGLE OR ITS SUBSIDIARIES AND AFFILIATES HAVE BEEN ADVISED OF, KNEW OR SHOULD HAVE KNOWN THAT SUCH DAMAGES WERE POSSIBLE AND EVEN IF DIRECT DAMAGES DO NOT SATISFY A REMEDY. GOOGLE’S (AND ITS WHOLLY OWNED SUBSIDIARIES’ TOTAL CUMULATIVE LIABILITY TO YOU OR ANY OTHER PARTY FOR ANY LOSS OR DAMAGES RESULTING FROM CLAIMS, DEMANDS, OR ACTIONS ARISING OUT OF OR RELATING TO THIS AGREEMENT WILL NOT EXCEED $500 (USD).
</p>
<h5>12. Proprietary Rights Notice.</h5>
<p>
The Service, which includes the Software and all Intellectual Property Rights therein are, and will remain, the property of Google (and its wholly owned subsidiaries). All rights in and to the Software not expressly granted to You in this Agreement are reserved and retained by Google and its licensors without restriction, including, Google''s (and its wholly owned subsidiaries'') right to sole ownership of the Software and Documentation. Without limiting the generality of the foregoing, You agree not to (and not to allow any third party to): (a) sublicense, distribute, or use the Service or Software outside of the scope of the license granted in this Agreement; (b) copy, modify, adapt, translate, prepare derivative works from, reverse engineer, disassemble, or decompile the Software or otherwise attempt to discover any source code or trade secrets related to the Service; (c) rent, lease, sell, assign or otherwise transfer rights in or to the Software or the Service; (d) use, post, transmit or introduce any device, software or routine which interferes or attempts to interfere with the operation of the Service or the Software; (e) use the trademarks, trade names, service marks, logos, domain names and other distinctive brand features or any copyright or other proprietary rights associated with the Service for any purpose without the express written consent of Google; (f) register, attempt to register, or assist anyone else to register any trademark, trade name, serve marks, logos, domain names and other distinctive brand features, copyright or other proprietary rights associated with Google (or its wholly owned subsidiaries) other than in the name of Google (or its wholly owned subsidiaries, as the case may be); or (g) remove, obscure, or alter any notice of copyright, trademark, or other proprietary right appearing in or on any item included with the Service.
</p>
<h5>13. U.S. Government Rights.</h5>
<p>
If the use of the Service is being acquired by or on behalf of the U.S. Government or by a U.S. Government prime contractor or subcontractor (at any tier), in accordance with 48 C.F.R. 227.7202-4 (for Department of Defense (DOD) acquisitions) and 48 C.F.R. 2.101 and 12.212 (for non-DOD acquisitions), the Government''s rights in the Software, including its rights to use, modify, reproduce, release, perform, display or disclose the Software or Documentation, will be subject in all respects to the commercial license rights and restrictions provided in this Agreement.
</p>
<h5>14. Term and Termination.</h5>
<p>
Either party may terminate this Agreement at any time with notice. Upon any termination of this Agreement, Google will stop providing, and You will stop accessing the Service; and You will delete all copies of the GATC from all Properties and certify thereto in writing to Google within 3 business days of such termination. In the event of any termination (a) You will not be entitled to any refunds of any usage fees or any other fees, and (b) any (i) outstanding balance for Service rendered through the date of termination, and (ii) other unpaid payment obligations during the remainder of the Initial Term will be immediately due and payable in full and (c) all of Your historical Report data will no longer be available to You.
</p>
<h5>15. Modifications to Terms of Service and Other Policies.</h5>
<p>
Google may modify these terms or any additional terms that apply to the Service to, for example, reflect changes to the law or changes to the Service. You should look at the terms regularly. Google will post notice of modifications to these terms at http://www.google.com/analytics or policies referenced in these terms at the applicable URL for such policies. Changes will not apply retroactively and will become effective no sooner than 14 days after they are posted. If You do not agree to the modified terms for the Service, You should discontinue Your use Google Analytics. No amendment to or modification of this Agreement will be binding unless (i) in writing and signed by a duly authorized representative of Google, (ii) You accept updated terms online, or (iii) You continue to use the Service after Google has posted updates to the Agreement or to any policy governing the Service.
</p>
<h5>16. Miscellaneous, Applicable Law and Venue.</h5>
<p>
Google will be excused from performance in this Agreement to the extent that performance is prevented, delayed or obstructed by causes beyond its reasonable control. This Agreement (including any amendment agreed upon by the parties in writing) represents the complete agreement between You and Google concerning its subject matter, and supersedes all prior agreements and representations between the parties. If any provision of this Agreement is held to be unenforceable for any reason, such provision will be reformed to the extent necessary to make it enforceable to the maximum extent permissible so as to effect the intent of the parties, and the remainder of this Agreement will continue in full force and effect. This Agreement will be governed by and construed under the laws of the state of California without reference to its conflict of law principles. In the event of any conflicts between foreign law, rules, and regulations, and California law, rules, and regulations, California law, rules and regulations will prevail and govern. Each party agrees to submit to the exclusive and personal jurisdiction of the courts located in Santa Clara County, California. The United Nations Convention on Contracts for the International Sale of Goods and the Uniform Computer Information Transactions Act do not apply to this Agreement. The Software is controlled by U.S. Export Regulations, and it may be not be exported to or used by embargoed countries or individuals. Any notices to Google must be sent to: Google Inc., 1600 Amphitheatre Parkway, Mountain View, CA 94043, USA, with a copy to Legal Department, via first class or air mail or overnight courier, and are deemed given upon receipt. A waiver of any default is not a waiver of any subsequent default. You may not assign or otherwise transfer any of Your rights in this Agreement without Google''s prior written consent, and any such attempt is void. The relationship between Google and You is not one of a legal partnership relationship, but is one of independent contractors. This Agreement will be binding upon and inure to the benefit of the respective successors and assigns of the parties hereto. The following sections of this Agreement will survive any termination thereof: 1, 4, 5, 6 (except the last two sentences), 7, 8, 9, 10, 11, 12, 14, and 16.
</p>');