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
		(8, 3, 'unsubscrubed-successful');

SET IDENTITY_INSERT [cont].[Pages] OFF
GO		
INSERT INTO [cont].[Items]([PageID], [IsHTML], [SubKey], [Value])
VALUES (1, 0, 'title', 'Touch Map'),
		(1, 1, 'content', '<div style="background-color: #fff; padding: 50px 263px;"><h3>Touch map:</h3>
<p>Get to know everywhere your visitors touch on your application, whether it''s links, images, text or dead space.</p>
<ul>
<li>See where links are needed</li>
<li>Capture every finger move, touch and scroll</li>
<li>See what areas are most touching!</li>
</ul></div>'),
		(2, 0, 'title', 'Play Back'),
		(2, 1, 'content', '<div style="background-color: #fff; padding: 50px 114px;">
<h3 style="text-align:center;">Play back</h3>
<img src="/Resources/play_back1.jpg" alt="Play back preview" style="float:right;margin-left:20px;">
<p>Do you need to track what your users are doing with your application?  Playback is like you’re looking right over their shoulder and helping them every step of the way. Help your users understand your application and make the most out of it. Once they understand your application they’ll want to use it more often. Rewind and watch their browsing sessions to see what they looked at and how they used it. This is a powerful analysis tool.</p>
<ul>
<li>Improve the experience of each and every user that uses your application.</li>
<li>See what your users do with your app.</li>
<li>Fix the UI so everything makes sense for your customers.</li>
</ul>
<p>Use Play Back to make the most out of your applications. Improve them using the data you collect so your users have the best experience possible. If your application is a great one, people will want to use it.  Play back and understand exactly how your customers use your application and know how to improve it.</p>
<div style="clear:both;"></div></div>'),
		(3, 0, 'title', 'Eye Track'),
		(3, 1, 'content', '<div style="background-color: #fff; padding: 50px 263px;"><h3>Eye track:</h3>
<p>Understand how much attention your visitors pay to a content in your app.</p>
<ul>
<li>Know, how far down your visitors scroll</li>
<li>For how long users look at each area or fold</li>
<li>Identify the boring areas or content.</li>
</ul></div>'),
		(4, 0, 'title', 'Thank You'),
		(4, 1, 'content', '<div style="background-color: #fff; padding: 50px 263px;"><h2 style="color:#333;display:block;font-family:Arial;font-size:30px;font-weight:bold;line-height:120%;margin-top:40px;margin-right:0;margin-bottom:15px;margin-left:0;text-align:left">Thank You</h2>
		<p style="margin-bottom:16px">Dear customer,</p>
<p style="margin-bottom:16px">Thank you for signing up for our mobile apps analytical service. Due to high demand
        for our analytics services, we are constantly increasing our capacity and processing
        hundreds of new customers weekly. Now you are in fast moving line and we shall open
        your account within two weeks, notification will be sent to your email.</p>
<p style="margin-bottom:16px">Thanks again for your patience.</p>
<p style="margin-bottom:16px"><em>Looking forward to serve you,<br>Mobillify team</em></p></div>'),
		(5, 0, 'title', 'Activation email was sent'),
		(5, 1, 'content', '<div class="content-wrapper">
    <div style="background-color: #fff; padding: 50px 263px;">Activation email was sent, please check your email.</div>
</div>'),
		(6, 0, 'title', 'Your account was activated'),
		(6, 1, 'content', '<div class="content-wrapper">
    <div style="background-color: #fff; padding: 50px 263px;">Your account was activated</div>
</div>'),
		(7, 0, 'title', 'Forgot password email was sent'),
		(7, 1, 'content', '<div class="content-wrapper">
    <div style="background-color: #fff; padding: 50px 263px;">Forgot password email was sent.</div>
</div>'),
		(8, 0, 'title', 'Unsubscrubed successful'),
		(8, 1, 'content', '<div class="content-wrapper">
    <div style="background-color: #fff; padding: 50px 263px;">Unsubscrubed successful.</div>
</div>');

GO

--=====================  System Mailes =========================-

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
