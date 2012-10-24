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
DELETE [cont].[KEYS]
GO
DELETE [cont].[Mails]
GO
DELETE [cont].[Pages] 
GO
DELETE [cont].[Themes]  
GO

INSERT INTO [cont].[Themes] ([ID], [Name], [Url], [TYPE])
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
                (17, 3, 'tutorials'),
				(18, 3, 'm/activation-email-sent');

SET IDENTITY_INSERT [cont].[Pages] OFF
GO              
INSERT INTO [cont].[Items]([PageID], [IsHTML], [SubKey], [VALUE])
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
    .box .price {color: #464646; display: block; font-size: 30px; letter-spacing: -1px; line-height: 1.2em; padding-bottom: 10px; text-align: center;}
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
                (16, 1, 'content', ' <article class="col center"><h2>Welcome on board!</h2>
                                                        <p>The Mobilify crew members are really excited because with your help, we are creating something really new.
Please keep in mind that we are only in the first (closed) stage of the BETA. This version of Fingerprint is not a final product. 
Rather, it is a tool that will allow us to demonstrate Fingerprint’s great potential as we continue to improve and expand the site’s content, accessibility, and functionality.</p>
<p>We appreciate your help and feedback for improving our service.</p>
<p>The skipper.</p>
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
</p>'),                
				(18, 0, 'title', 'Activation email was sent'),
                (18, 1, 'content', ' <article class="col center"><h2>Activation email was sent</h2>
                                                        <p>Please check your email.</p>
                                                        <p>And use activation link in the email to activate your account.</p>
                                                        </article>');

GO

--=====================  System Mails =========================-

GO
SET IDENTITY_INSERT [cont].[Mails] ON

INSERT INTO [cont].[Mails] ([ID], [IsSystem], [ThemeID], [Url])
VALUES (1, 1, 1, 'mails/activationemail'),
                (2, 1, 1, 'mails/forgotpasswordmail'),
                (4, 1, 1, 'mails/specialaccessemail');

SET IDENTITY_INSERT [cont].[Mails] OFF

GO              
INSERT INTO [cont].[Items]([MailID], [IsHTML], [SubKey], [VALUE])
VALUES (1, 0, 'subject', 'Please activate your account'),
		(1, 1, 'body', 'Please activate your account by the link: {activation_link}'),
		(2, 0, 'subject', 'Reset password'),
		(2, 1, 'body', 'Please use the link to reset password: {reset_password_link}'),
		(4, 0, 'subject', 'Hello from Mobillify! The wait is over'),
		(4, 1, 'body', '<p style="margin-bottom:16px">Dear Friend,</p>
                <p style="margin-bottom:16px">Welcome on board!</p>
                <p style="margin-bottom:16px">Your account has been activated.</p>
<p style="margin-bottom:16px">The Mobilify crew members are really excited because with your help, we are creating something really new. 
This version of Fingerprint is not a final product. Rather, it is a tool that will allow us to demonstrate Fingerprint’s great potential as we continue to improve and expand the site’s content, accessibility, and functionality.</p>
<p style="margin-bottom:16px">We appreciate your help and feedback for improving our service.</p>
<p style="margin-bottom:16px"><em>The skipper</em></p>
<p style="margin-bottom:16px">http://finger.mobillify.com</p>
');

GO

--=====================  Promotion Mails =========================-

GO
SET IDENTITY_INSERT [cont].[Mails] ON

INSERT INTO [cont].[Mails] ([ID], [IsSystem], [ThemeID], [Url])
VALUES (3, 0, 2, 'mails/thank-you');

SET IDENTITY_INSERT [cont].[Mails] OFF

GO              
INSERT INTO [cont].[Items]([MailID], [IsHTML], [SubKey], [VALUE])
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
SET IDENTITY_INSERT [cont].[KEYS] ON

INSERT INTO [cont].[KEYS] ([ID], [Url])
VALUES (1, 'account/terms-and-coditions');

SET IDENTITY_INSERT [cont].[KEYS] OFF

INSERT INTO [cont].[Items]([KeyID], [IsHTML], [SubKey], [VALUE])
VALUES (1, 1, 'content', '<h2>FingerPrint Terms Of Service</h2>
<h5>Acceptance of Terms</h5>
<p>
FingerPrint welcomes you. By checking the "Terms and Conditions" box in our account sign-up, or registering for FingerPrint''s Analytics Service (as defined below), you agree that you have read, understand, and accept the terms and conditions described below (the "Terms of Service") and you agree to be bound by these Terms of Service and all terms, policies and guidelines incorporated in the Terms of Service by reference, including, but not limited to, FingerPrint''s Privacy Policy (this "Agreement"). If you do not agree to this Agreement, you should not use the Analytics Service in any way. The Analytics Service is offered to you conditioned on your acceptance without modification of this Agreement, including without limitation, FingerPrint''s right to use all data collected and analyzed by the Analytics Service.
</p><p>
The Analytics Service is available only to individuals who are at least 18 years old and to companies that are appropriately licensed and otherwise legally permitted to conduct business. You represent and warrant that (a), if you are an individual, you are at least 18 years old, and (b) if you are a company, that you are appropriately licensed and is legally permitted to conduct business.
</p>
<h5>Modification of Agreement</h5>
<p>
FingerPrint reserves the right to change or modify any of the terms and conditions contained in this Agreement at any time, in its sole discretion, by posting changes at http://www.finger.mobillify.com or such other URL that FingerPrint may provide from time to time). Your continued use of any part of the Analytics Service following the posting of such changes or modifications will constitute your acceptance of such changes or modifications.
</p>
<h5>Description and Use of the Analytics Service</h5>
<p>
The "Analytics Service" means, collectively, the "Software", the "Reports" and the "Documentation", all as defined below in this Agreement. Under this Agreement, FingerPrint may allow you to access the Analytics Service by using FingerPrint''s analytics site code (the "Agent") and any fixes, updates and upgrades provided to you, provided that you have an active FingerPrint account. In addition, FingerPrint may provide you with on-line access to a variety of analytics reports (the "Reports") generated by FingerPrint''s processing code and any fixes, updates and upgrades. The Agent and FingerPrint''s processing code are defined collectively herein as "Software". The processing code analyzes the data collected by the Agent. This data concerns the characteristics and activities of end users of your applications.
</p>
<h5>Fees and Payment</h5>
<p>
FingerPrint Analytics is provided to you free of charge. FingerPrint may change its fees and payment policies for the Analytics Service from time to time. The changes will be posted at http://www.finger.mobillify.com (or such other URL that FingerPrint may provide from time to time).
</p>
<h5>Incidental Costs Associated with Use of the Analytics Service</h5>
<p>
You agree that you are solely liable for all costs, fees, and other expenses resulting from your use of the Analytics Service. This specifically includes, but is not limited to, incidental costs incurred by you in connection with your use of the Analytics Service, including, but not limited to, costs owed to your cell phone carrier or mobile provider, monthly cell phone coverage fees, data plan costs, and any other additional fees incurred from your cell phone carrier or mobile provider.
</p>
<h5>Registration</h5>
<p>
To register for the Analytics Service, you must complete the registration process at http://www.finger.mobillify.com (or such other URL that FingerPrint may provide from time to time) by providing FingerPrint with current, complete and accurate information. Upon registration for the FingerPrint Analytics Services, you will be required to provide FingerPrint with your email and password. You understand that you are solely responsible for maintaining the confidentiality of your password and that you shall be solely and fully responsible for all activities that occur under your username and password. FingerPrint shall not be responsible for any loss, claim or other liability that may arise from the unauthorized use of any password. You agree to immediately notify FingerPrint of any unauthorized use of your password or username or any other breach of security. If a password is lost or stolen, it is the user''s responsibility to change the password, and immediately notify FingerPrint, so that your account remains both secure and functional.
</p>
<h5>Reports and Results</h5>
<p>
Subject to the terms and conditions of this Agreement, you may remotely access, view and download the Reports, which will be stored at http://www.finger.mobillify.com (or such other URL that FingerPrint may provide from time to time). FingerPrint shall own and retains all right, title and interest in and to Reports and all other results, data and/or information provided to you through the service (collectively, "Results"). You may use the Reports only in connection with your use of FingerPrint Analytics pursuant to this Agreement and not for any other purpose.
</p>
<h5>Limited License</h5>
<p>
You are hereby granted a nonexclusive, limited, non-transferable, revocable and non-sublicensable license to install, use, copy and distribute the Agent solely as necessary to use the Analytics Service pursuant to this Agreement for applications that you own and control. Your use of the Software and accompanying documentation ("Documentation") is subject to this Agreement and does not include: (a) any resale, lease, rental, assignment or other transfer of rights of the Software or Documentation; (b) the distribution, public performance or public display of the Software or Documentation (except as expressly set forth above with respect to the Agent); (c) modification, revision, creation of derivative works from or otherwise making any derivative uses of the Software or Documentation or any information or content therein; (d) decompilation, reverse engineering or otherwise attempting to derive the source code for the Software (except to the extent applicable laws specifically prohibit restriction of such activities); or (e) any use of the Software or Documentation other than for its intended purpose. FingerPrint hereby reserves all rights not expressly granted herein. Any use of the Software or Documentation other than as specifically authorized herein, without the prior written permission of FingerPrint, is strictly prohibited and will terminate the license granted herein. Such unauthorized use may also violate applicable laws, including, without limitation, copyright and trademark and other intellectual property laws. 
</p>
<h5>Privacy and Information Collection</h5>
<p>
As a condition of your access to the Analytics Service, you agree that FingerPrint has the right, for any purpose, to collect, retain, use, and publish in an aggregate manner, subject to the terms of its Privacy Policy located here (or such other URL that FingerPrint may provide from time to time), information collected in your use of the Analytics Service, including without limitation, User Data. FingerPrint will not disclose to any third parties any User Data collected by the Analytics Service from your applications that is specifically attributable to you, your applications or your customers. You will not (and will not allow any third party to) use the Analytics Service to track or collect personally identifiable information of end users, nor will you (or will you allow any third party to) associate any data gathered from your application(s) with any personally identifying information from any source as part of your use (or such third parties'' use) of the Analytics Service. You agree that you have and will abide by a privacy policy that complies with all applicable laws and industry standards and that you will comply with all applicable laws relating to the collection of information from end users of your applications.
</p><p>
You must post a privacy policy. That policy must (i) provide notice of your use of a tracking pixel, agent or any other visitor identification technology that collects, uses, shares and stores data about end users of your applications and (ii) contain a link to FingerPrint''s Privacy Policy so that your end users can opt-out of FingerPrint Analytics tracking. The opt-out is specific to FingerPrint activities and does not affect the activities of other ad networks or analytics providers that you use. If an end user opts-out, FingerPrint will stop tracking data for the device identified by the provided MAC address and/or device identifier going forward. The FingerPrint Analytics tracking will stop across all applications within the FingerPrint network. You agree to obtain all end-user consents required by applicable law before you use the Analytics Service.
</p><p>
You agree that you will not use the Analytics Service in connection with any application labeled or described as a "Kids" or "Children" application and will not use the FingerPrint Analytics Services a) in connection with any application, advertisement or service directed towards children or b) to collect any personal information of children.
</p>
<h5>Confidential Information</h5>
<p>
"Confidential Information" includes any proprietary data and any other information disclosed by one party to the other in writing and marked "confidential" or disclosed orally and, within ten business days, indicated in writing as "confidential". Notwithstanding the foregoing, Confidential Information will not include any information that is or becomes publicly known, which is already in the receiving party''s possession prior to disclosure by a party or which is independently developed or collected by the receiving party without the use of Confidential Information. Neither party will use or disclose the other party''s Confidential Information without the other''s prior written consent except for the purpose of performing its obligations under this Agreement or if required by law, regulation or court order. Upon termination of this Agreement, the parties will promptly either return or destroy all Confidential Information and, upon request, provide written certification of such.
</p>
<h5>Indemnity</h5>
<p>
You agree to indemnify, defend and hold harmless FingerPrint, its employees, officers and directors, or users from and against any and all claims, liabilities, penalties, settlements, judgments, fees (including reasonable attorneys'' fees) arising from (i) any information that you or anyone using your account may submit or access in the course of using the Analytics Service; (ii) your violation of the terms of this Agreement; and (iii) any violation or failure by you to comply with all laws and regulations in connection with your use of the Analytics Service, whether or not described herein.
</p>
<h5>Third Parties</h5>
<p>
If you use the Analytics Service on behalf of any third party, you represent and warrant that you are authorized to act on behalf of, and bind to this Agreement, that third party. You shall ensure that each third party is bound by and abides by the terms of this Agreement. You agree to indemnify, hold harmless and defend FingerPrint and its parents, subsidiaries, affiliates, officers and employees, at your expense, against any and all third-party claims, actions, proceedings, and suits and all related liabilities, damages, settlements, penalties, fines, costs or expenses (including, without limitation, reasonable attorneys'' fees and other litigation expenses) incurred by FingerPrint, arising out of or relating to (a) any representations and warranties made by you concerning any aspect of the Analytics Service; (b) any claims made by or on behalf of any third party pertaining directly or indirectly to your use of the Analytics Service; (c) violations of your obligations of privacy to any third party; and (d) any claims with respect to acts or omissions of third parties in connection with the Analytics Service.
</p>
<h5>Disclaimer of Warranties and Limitation of Liability</h5>
<p>
The information and services included in or available through the Analytics Service, including the Reports, may include inaccuracies or typographical errors. FingerPrint may make improvements and/or changes in the Analytics Service at any time, with or without notice. You specifically agree that FingerPrint shall not be responsible for unauthorized access to or alteration of the User Data or data from your applications.
</p><p>
FINGERPRINT DISCLAIMS ANY AND ALL WARRANTIES, EXPRESS, IMPLIED OR STATUTORY REGARDING THE SERVICE TO THE FULL EXTENT PERMITTED BY LAW. WITHOUT LIMITING THE GENERALITY OF THE FOREGOING, THE ANALYTICS SERVICE IS PROVIDED "AS-IS" AND WITHOUT WARRANTIES OF ANY KIND, INCLUDING, WITHOUT LIMITATION, ANY WARRANTIES OF PERFORMANCE OR IMPLIED WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE, TITLE AND NON-INFRINGEMENT. FURTHER, FINGERPRINT DOES NOT MAKE, AND HAS NOT MADE, ANY REPRESENTATION OR WARRANTY THAT THE ANALYTICS SERVICE IS ACCURATE, COMPLETE, RELIABLE, CURRENT, ERROR-FREE, OR VIRUS-FREE OR THAT THE OPERATION OF THE ANALYTICS SERVICE WILL BE UNINTERRUPTED. SOME STATES DO NOT ALLOW EXCLUSION OF AN IMPLIED WARRANTY, SO THIS DISCLAIMER MAY NOT APPLY TO YOU.
</p><p>
IN NO EVENT WILL FINGERPRINT, ITS SUBSIDIARIES, AFFILIATES OR ANY OF THEIR RESPECTIVE DIRECTORS, OFFICERS, EMPLOYEES OR AGENTS (COLLECTIVELY, THE "FINGERPRINT PARTIES"), BE LIABLE TO YOU OR ANY OTHER PERSON OR ENTITY UNDER ANY THEORY FOR INDIRECT, INCIDENTAL, PUNITIVE, SPECIAL OR CONSEQUENTIAL DAMAGES, LOST INCOME, REVENUE OR PROFITS, LOST OR DAMAGED DATA, OR OTHER COMMERCIAL OR ECONOMIC LOSS, ARISING OUT OF THIS AGREEMENT OR FINGERPRINT ANALYTICS, EVEN IF FINGERPRINT HAS BEEN ADVISED OF THE POSSIBILITY OF SUCH DAMAGES OR SUCH DAMAGES ARE FORESEEABLE. THE FINGERPRINT PARTIES'' AGGREGATE LIABILITY ARISING OUT OF THIS AGREEMENT OR FINGERPRINT ANALYTICS WILL NOT EXCEED THE GREATER OF ONE HUNDRED DOLLARS ($100) OR THE AMOUNT YOU HAVE PAID US IN THE PAST TWELVE MONTHS. APPLICABLE LAW MAY NOT ALLOW THE LIMITATION OR EXCLUSION OF LIABILITY OR INCIDENTAL OR CONSEQUENTIAL DAMAGES, SO THE ABOVE LIMITATION OR EXCLUSION MAY NOT APPLY TO YOU. IN SUCH CASES, THE FINGERPRINT PARTIES'' LIABILITY WILL BE LIMITED TO THE FULLEST EXTENT PERMITTED BY APPLICABLE LAW.
</p>
<h5>Prohibited Conduct</h5>
<p>
You shall not, directly or indirectly, take any fraudulent action in the use of the Services, including without limitation, click fraud or fraudulent downloads. FingerPrint may terminate your account at anytime for any reason, including without limitation, such fraudulent activity, in its sole discretion.
</p>
<h5>Modifications to and Termination of the Analytics Service</h5>
<p>
FingerPrint reserves the right to discontinue offering the Analytics Service or to modify the Analytics Service at any time in its sole discretion. If you are dissatisfied with any aspect of the Analytics Service at any time, your sole and exclusive remedy is to cease using it. Notwithstanding anything contained in this Agreement to the contrary, FingerPrint may also, in its sole discretion, terminate or suspend your access to the Analytics Service at any time. Upon any termination of this Agreement, FingerPrint will cease providing the Analytics Service, and you will delete all copies of FingerPrint''s analytics site code from your applications and certify thereto in writing to FingerPrint within three (3) business days of such termination.
</p>
<h5>Waiver and Severability</h5>
<p>
If any provision of this Agreement is held to be invalid or unenforceable by a court of competent jurisdiction, the parties nevertheless agree that the court should endeavor to give effect to the parties'' intentions as reflected in the provision, and the other provisions of this Agreement remain in full force and effect. FingerPrint''s acquiescence in the breach of a provision of this Agreement or failure to act upon such breach does not waive FingerPrint''s right to act with respect to subsequent or similar breaches. Likewise, the delay or failure of FingerPrint to exercise or enforce any right or provision of this Agreement shall not constitute a waiver of such right or provision.
</p>
<h5>CHOICE OF LAW AND FORUM</h5>
<p>
This Agreement and the relationship between you and FingerPrint shall be interpreted in accordance with the laws of the State of California without regard to conflict of laws principles. Subject to the arbitration provisions below, you and FingerPrint hereby agree to submit exclusively, to the personal jurisdiction of the state courts with jurisdiction over San Francisco, California and/or the U.S. District Court for the Northern District of California.
</p>
<h5>BINDING ARBITRATION</h5>
<p>
Certain portions of this Section are deemed to be a "written agreement to arbitrate" pursuant to the Federal Arbitration Act. You and FingerPrint agree that we intend that this Section satisfies the "writing" requirement of the Federal Arbitration Act.
</p><p>
You or FingerPrint may elect to have any controversy, allegation or claim arising out of or relating to this Agreement, the Analytics Service or the User Data, including but not limited to claims for indemnification, contribution, or cross-claims in a pending action involving one or more third parties (collectively, a "Dispute") finally and exclusively resolved by binding arbitration before a sole arbitrator under the rules and regulations of the American Arbitration Association. If an in-person arbitration hearing is required, then it will be conducted in San Francisco, California; but if the applicable arbitration rules or laws require the arbitration to be conducted in the "metropolitan statistical area" (as defined by the U.S. Census Bureau) where you are a resident at the time the Dispute is submitted to arbitration, FingerPrint shall have the right to elect to proceed to arbitration in such location. All parties to the arbitration will have the right, at their own expense, to be represented by an attorney or other advocate of their choosing. You and FingerPrint will pay the administrative and arbitrator''s fees and other costs in accordance with the applicable arbitration rules; but if applicable arbitration rules or laws require FingerPrint to pay a greater portion or all of such fees and costs in order for this Section to be enforceable, then FingerPrint will have the right to elect to pay the fees and costs and proceed to arbitration.
</p><p>
TO THE FULLEST EXTENT PERMITTED BY LAW, YOU AGREE THAT (I) NO ARBITRATION SHALL BE JOINED WITH ANY OTHER; (II) THERE IS NO RIGHT OR AUTHORITY FOR ANY DISPUTE TO BE ARBITRATED ON A CLASS-ACTION BASIS OR TO UTILIZE CLASS ACTION PROCEDURES; AND (III) THERE IS NO RIGHT OR AUTHORITY FOR ANY DISPUTE TO BE BROUGHT IN A PURPORTED REPRESENTATIVE CAPACITY ON BEHALF OF THE GENERAL PUBLIC OR ANY OTHER PERSONS.
</p><p>
YOU AGREE TO WAIVE YOUR RIGHT TO A JURY TRIAL AND UNDERSTAND THAT, ABSENT THIS PROVISION, YOU WOULD HAVE THE RIGHT TO SUE IN COURT. THE SCOPE OF THIS WAIVER IS INTENDED TO BE ALL-ENCOMPASSING OF ANY AND ALL DISPUTES THAT MAY BE FILED IN ANY COURT AND THAT RELATE TO THE SUBJECT MATTER OF THIS AGREEMENT, INCLUDING, WITHOUT LIMITATION, CONTRACT CLAIMS, TORT CLAIMS AND ALL OTHER COMMON LAW AND STATUTORY CLAIMS.
</p><p>
TO THE FULLEST EXTENT PERMITTED BY APPLICABLE LAW, IF YOU OR FINGERPRINT WANT TO ASSERT A DISPUTE AGAINST THE OTHER, THEN YOU OR FINGERPRINT MUST COMMENCE IT WITHIN ONE (1) YEAR AFTER THE DISPUTE ARISES - OR IT WILL BE FOREVER BARRED.
</p><p>
In the event either you or FingerPrint elects arbitration, for any Dispute where the total amount of the award sought is less than $10,000 USD, the party requesting relief may further elect to resolve the dispute in a cost effective manner through binding non-appearance-based arbitration through an established alternative dispute resolution ("ADR") provider mutually agreed upon by the parties. The ADR provider and the parties must comply with the following rules: (i) the arbitration shall be conducted by telephone, online and/or be solely based on written submissions, and the specific manner shall be chosen by the party initiating the arbitration; (ii) the arbitration shall not involve any personal appearance by the parties or witnesses unless otherwise mutually agreed by the parties; and (iii) any judgment on the award rendered by the arbitrator shall be final and may be entered in any court of competent jurisdiction. 
</p>
<h5>Entire Agreement</h5>
<p>
This Agreement constitutes the entire agreement between you and FingerPrint and governs your use of the Analytics Service, superseding any prior agreements between you and FingerPrint with respect to the Analytics Service.
</p>
<h5>Survival</h5>
<p>
The terms and conditions contained in this Agreement that by their sense and context are intended to survive the performance hereof by the Parties hereunder, including but not limited to the provisions relating to Indemnity, Disclaimer of Warranties and Limitation of Liability, shall so survive the completion of the performance, cancellation or termination of this Agreement.
</p>');