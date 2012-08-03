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
    <br/><a href="/Resources/Packages/fingerprint.jar" target="_blank">Android Package 1.0</a>
    <br/><a href="/Resources/Packages/fingerprint.properties" target="_blank">FingerPrint.properties</a>
</p>
<p dir="LTR">
    <strong>Setup</strong>
    <strong></strong>
</p>
<ol start="1" type="1">
    <li dir="LTR">
        &#183; Add fingerptint.jar to your project''s /libs directory.//-- ad screenshot
    </li>
    <li dir="LTR">
        &#183; Add fingerprint.properties file to assets folder.
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
    Add new application your Portfolio and create a AppID.
</p>
<p dir="LTR">
    <strong>Starting the Tracker</strong>
    <strong></strong>
</p>
<p dir="LTR">
    1. Call <em>FingerPrint.init</em> method in onCreate method of your activity.
</p>
<p dir="LTR">
    2. Call <em>FingerPrint.start</em><em>(this, viewUri)</em> method in onStart() method
</p>
<p dir="LTR">
    3. Call <em>FingerPrint.finsih</em><em>(</em><em>this, viewUri)</em> in onStop() method passing the View''s URI and activity being tracked.
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

//Activity and View Details
private static final String appID = "MA-XXXX-YY";
private static final String viewPath = "ViewID1";
 
protected void onCreate(Bundle savedInstanceState) {
super.onCreate(savedInstanceState);
setContentView(R.layout.main);
FingerPrint.init(activityName, viewUri, this);
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
FingerPrint.start(this, viewUri);
}
 
@Override
protected void onStop() {
super.onStop();
//Finishing FingerPrint service
FingerPrint.finish(this, viewUri);
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

