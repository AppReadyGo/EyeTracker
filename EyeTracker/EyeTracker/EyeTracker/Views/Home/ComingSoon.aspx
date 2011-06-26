<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Finger Print - Mobillify</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Content-Language" content="en-gb" />
    <meta name="robots" content="all" />
    <meta name="revisit-after" content="3 days" />
    <meta name="author" content="Yuri Panshin" />
    <meta name="copyright" content="Mobillify" />
    <meta name="company" content="Mobillify" />
    <meta name="keywords" content="Finger Print,Analytics,Eyetracker" />
    <meta name="description" content="Get insight into your users behaviour." />

    <link href="../../Content/ComingSoon.css" rel="stylesheet" type="text/css" />
</head>
<body>
 	<h1>Finger Print</h1> 
	<h2>Get insight into your users behaviour</h2> 
	<div class="signup"> 
	    <% using (Html.BeginForm("Subscribe", "Home")) %>
        <% { %> 
		    <p><label for="email">Do not miss out when Finger Print launches:</label><br /><p><input type="text" name="email" id="email" size="25" /><input type="image" value="Notify Me" src="../../Content/img/btn.png" class="submit" /></p></p> 
	        <p class="error"><%: ViewData["ErrorMessage"] %></p>
        <% } %> 
	</div> 	
	<div id="copyright"> 
		<p><a href="http://www.mobillify.com" title="Mobillify">&copy; Mobillify 2011</a> - <%: Html.ActionLink("admin","Index") %></p> 
	</div>
</body>
</html>