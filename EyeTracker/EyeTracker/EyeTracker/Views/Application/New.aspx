﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/AfterLogin.Master" Inherits="System.Web.Mvc.ViewPage<EyeTracker.Model.ApplicationModel>" %>

<asp:Content ID="PageTitleContent" ContentPlaceHolderID="PageTitleContent" runat="server">Create Application</asp:Content>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="<%: Url.Content("~/Content/shared/form.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%: Url.Content("~/Content/application.new.css")%>" rel="stylesheet" type="text/css" />
    <script src="<%: Url.Content("~/Scripts/application.new.js")%>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/ThridParty/ajaxfileupload.js")%>" type="text/javascript"></script>
	<script type="text/javascript" src="<%: Url.Content("~/Scripts/ThridParty/fancybox/jquery.fancybox-1.3.4.pack.js")%>"></script>
	<link rel="stylesheet" type="text/css" href="<%: Url.Content("~/Scripts/ThridParty/fancybox/jquery.fancybox-1.3.4.css")%>" media="screen" />
    <script type="text/javascript">
        var newAppURL = '/Application/New/<%: ViewBag.PortfolioId %>/';
        var appId = <%: Model.Id %>;
        var addScreenURL = '/Application/AddScreen/<%: ViewBag.PortfolioId %>/';
        var screenImgURL = '/Application/Screen/';
    </script>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<h3 class="title">Create Application</h3>
<form id="edit_form" action="/Application/Edit/<%: ViewBag.PortfolioId %>/" method="post">
    <%:Html.HiddenFor(m => m.Id) %>
    <table>
        <caption>1. Application Configuration</caption>
        <tbody id="tbody">
            <tr><td class="label"><%: Html.LabelFor(m => m.Description)%></td><td><%: Html.TextBoxFor(m => m.Description, new { MaxLength = 50, @class = "name" })%><br /><%: Html.ValidationMessageFor(m => m.Description)%></td></tr>
            <tr><td class="label"><%: Html.LabelFor(m => m.Type)%></td><td><%: Html.DropDownListFor(m => m.Type, (IEnumerable<SelectListItem>)ViewData["TypesList"])%></td></tr>
        </tbody>
    </table>
    <p id="action_pnl"><input type="button" id="create_lnk" value="Create Application" /><a href="/Analytics">Cancel</a></p>
<div id="sample_code">
    <!--div id="screens" class="step" style="display:none;">
        <div id="img_preview" class="img-preview"><div><img src="" /><a>X</a></div></div>
        <h3>2. Screenshots</h3>
        <ul id="screens_list">
        <%foreach (var curScreen in ViewBag.Screens)
          { %>
            <li><a class="remove-btn">&nbsp</a><a class="img-lnk" href="/Application/Screen/<%: Model.Id %>/<%:curScreen.Width %>/<%:curScreen.Height %>/screen.jpg"><%:curScreen.Width %>X<%:curScreen.Height %></a></li>
        <%} %>
        </ul>
        <fieldset>
            <legend>New Screen</legend>
            <div class="error" id="screen_error"></div>
            <div>Width: <input id="screen_width"/></div>
            <div>Height: <input id="screen_height"/></div>
            <div>Image: <input type="file" id="screen_img" name="screen_img"/></div>
            <div><a id="add_screen_btn">add</a></div>
        </fieldset>
     </div-->
    <div id="sample_web_code" class="step">
        <h3>3. Paste this code on your site</h3>
        <div>Copy the following code, then paste it onto every page you want to track immediately before the closing &lt;/head&gt; tag.</div>
        <textarea readonly="readonly" id="web_code">
            <script type="text/javascript">

                var _mfyaq = _mfyaq || {};
                _mfyaq.key = '<%: ViewBag.PropertyId%>';

                (function () {
                    var mfyaq = document.createElement('script'); mfyaq.type = 'text/javascript'; mfyaq.async = true;
                    mfyaq.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www')
                    mfyaq.src += 'fingerprint.mobillify.com/analytics.js';
                    var scr = document.getElementsByTagName('script')[0]; scr.parentNode.insertBefore(mfyaq, scr);
                })();

            </script>
        </textarea>
    </div>
    <div id="sample_android_code" class="step">
        <h3>3. Download package and insert into your code</h3>
        <div>Package: <a href="<%: ViewBag.PackageLink %>">Android Package 1.0.1</a></div>
        <div>Property ID: <strong id="property_id"><%: ViewBag.PropertyId%></strong></div>
        <textarea readonly="readonly" id="android_code">
    public static void main(String[] args) {
 
    int radius = 0;
    System.out.println("Please enter radius of a circle");
 
    try
    {
    //get the radius from console
    BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
    radius = Integer.parseInt(br.readLine());
    }
    //if invalid value was entered
    catch(NumberFormatException ne)
    {
    System.out.println("Invalid radius value" + ne);
    System.exit(0);
    }
    catch(IOException ioe)
    {
    System.out.println("IO Error :" + ioe);
    System.exit(0);
    }
 
    /*
    * Area of a circle is
    * pi * r * r
    * where r is a radius of a circle.
    */
 
    //NOTE : use Math.PI constant to get value of pi
    double area = Math.PI * radius * radius;
 
    System.out.println("Area of a circle is " + area);
    }

        </textarea>
    </div>
    <p><input type="submit" value="Save Application" /><a href="/Analytics">Cancel</a></p>
</div>
</form>
<div id="overlay" class="overlay"></div>
</asp:Content>

