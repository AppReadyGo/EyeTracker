<%@ Page Title="" 
Language="C#" 
MasterPageFile="~/Views/Shared/Analytics.Master" 
Inherits="ViewPage<ViewModelWrapper<AfterLoginMasterModel, AnalyticsMasterModel, EyeTracker.Model.Pages.Analytics.ABCompareModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitleContent" runat="server">Finger Print</asp:Content>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
<script src="<%: Url.Content("~/Scripts/ThridParty/DateFormat.js")%>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/ThridParty/Flot/jquery.flot.min.js")%>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/ThridParty/Flot/jquery.flot.resize.min.js")%>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/ThridParty/jquery-ui.min.js")%>" type="text/javascript"></script>
<link href="<%: Url.Content("~/Content/themes/cupertino/jquery-ui.css") %>" rel="stylesheet" type="text/css" />
<link href="<%: Url.Content("~/Content/shared/filter.css")%>" rel="stylesheet" type="text/css" />
<link href="<%: Url.Content("~/Content/analytics.screen.css")%>" rel="stylesheet" type="text/css" />
<script src="<%: Url.Content("~/Scripts/filter.js")%>" type="text/javascript"></script>
<style type="text/css">
    /*Some CSS*/
* {margin: 0; padding: 0;}
.magnify {position: relative;}

/*Lets create the magnifying glass*/
.large {
	width: 175px; height: 175px;
	position: absolute;
	border-radius: 100%;
	
	/*Multiple box shadows to achieve the glass effect*/
	box-shadow: 0 0 0 7px rgba(255, 255, 255, 0.85), 
	0 0 7px 7px rgba(0, 0, 0, 0.25), 
	inset 0 0 40px 2px rgba(0, 0, 0, 0.25);
	
	/*Lets load up the large image first*/
	background: url('/Analytics/ClickHeatMapImage/<%=Model.SubMaster.FilterUrlPart %>') no-repeat;
	
	/*hide the glass by default*/
	display: none;
}

/*To solve overlap bug at the edges during magnification*/
.small { display: block; }
article .magnify{border:5px solid #000;
        -webkit-background-clip: padding-box;-moz-background-clip: padding;background-clip: padding-box;
        -moz-border-radius: 0.5em;-webkit-border-radius: 0.5em;border-radius: 0.5em;
}
article .large{z-index:1000;}
article img{width:100%;}
article{width:47%;float:left;padding:0 10px;}
</style>
<script type="text/javascript">
    $(document).ready(function () {

        $('#firstScreen,#secondScreen').change(function () {
            var from = $("#datepicker_from").datepicker("getDate");
            var to = $("#datepicker_to").datepicker("getDate");
            var portfolios = $('#SelectedPortfolioId').val();
            var applications = $('#SelectedApplicationId').val();
            var screenSizes = $('#SelectedScreenSize').val();
            var paths = $('#firstScreen').val();
            var secondPath = $('#secondScreen').val();
            var url = '/Analytics/' + analytics.action +
                '/?pid=' + portfolios +
                '&fd=' + $.datepicker.formatDate('dd-M-yy', from) +
                '&td=' + $.datepicker.formatDate('dd-M-yy', to);
            if (applications) url += '&aid=' + applications;
            if (screenSizes) url += '&ss=' + screenSizes;
            if (paths) url += '&p=' + paths;
            if (secondPath) url += '&sp=' + secondPath;

            document.location.href = url;
        });


        var native_width = 0;
        var native_height = 0;

        //Now the mousemove function
        $(".magnify").mousemove(function (e) {
            var large = $(this).find('.large');
            var small = $(this).find('.small');
            //When the user hovers on the image, the script will first calculate
            //the native dimensions if they don't exist. Only after the native dimensions
            //are available, the script will show the zoomed version.
            if (!native_width && !native_height) {
                //This will create a new image object with the same image as that in .small
                //We cannot directly get the dimensions from .small because of the 
                //width specified to 200px in the html. To get the actual dimensions we have
                //created this image object.
                var image_object = new Image();
                image_object.src = small.attr("src");

                //This code is wrapped in the .load function which is important.
                //width and height of the object would return 0 if accessed before 
                //the image gets loaded.
                native_width = image_object.width;
                native_height = image_object.height;
            } else {
                //x/y coordinates of the mouse
                //This is the position of .magnify with respect to the document.
                var magnify_offset = $(this).offset();
                //We will deduct the positions of .magnify from the mouse positions with
                //respect to the document to get the mouse positions with respect to the 
                //container(.magnify)
                var mx = e.pageX - magnify_offset.left;
                var my = e.pageY - magnify_offset.top;

                //Finally the code to fade out the glass if the mouse is outside the container
                if (mx < $(this).width() && my < $(this).height() && mx > 0 && my > 0) {
                    large.fadeIn(100);
                }
                else {
                    large.fadeOut(100);
                }
                if (large.is(":visible")) {
                    //The background position of .large will be changed according to the position
                    //of the mouse over the .small image. So we will get the ratio of the pixel
                    //under the mouse pointer with respect to the image and use that to position the 
                    //large image inside the magnifying glass
                    var rx = Math.round(mx / small.width() * native_width - large.width() / 2) * -1;
                    var ry = Math.round(my / small.height() * native_height - large.height() / 2) * -1;
                    var bgp = rx + "px " + ry + "px";

                    //Time to move the magnifying glass with the mouse
                    var px = mx - large.width() / 2;
                    var py = my - large.height() / 2;
                    //Now the glass moves with the mouse
                    //The logic is to deduct half of the glass's width and height from the 
                    //mouse coordinates to place it with its center at the mouse coordinates

                    //If you hover on the image now, you should see the magnifying glass in action
                    large.css({ left: px, top: py, backgroundPosition: bgp });
                }
            }
        })
    })
</script>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<% Html.RenderPartial("Filter", Model.View); %>
<div>
    <article>
        <p><a id="showFirstImage" style="cursor:pointer;">Show Screen</a> &nbsp; <%= Html.DropDownList("firstScreen", Model.View.FirstScreenPathes) %></p>
        <div class="magnify">
            <div class="large"></div>
            <img class="small" src="/Analytics/ClickHeatMapImage/<%=Model.View.FirstScreenUrlPart %>"/>
        </div>
    </article>
    <article>
        <p><a id="showSecondImage" style="cursor:pointer;">Show Screen</a> &nbsp; <%= Html.DropDownList("secondScreen", Model.View.SecondScreenPathes)%></p>
        <div class="magnify">
            <div class="large"></div>
            <img class="small" src="/Analytics/ClickHeatMapImage/<%=Model.View.SecondScreenUrlPart %>"/>
        </div>
    </article>
    <div style="clear:both;"></div>
</div>
</asp:Content>