<%@ Page Title="" Language="C#" 
MasterPageFile="~/Views/Shared/Analytics.Master" 
Inherits="ViewPage<ViewModelWrapper<AfterLoginMasterModel, AnalyticsMasterModel, EyeTracker.Model.Filter.FilterModel>>" %>

<asp:Content ID="PageTitleContent" ContentPlaceHolderID="PageTitleContent" runat="server">Eye Tracker</asp:Content>


<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
<script src="<%: Url.Content("~/Scripts/ThridParty/DateFormat.js")%>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/ThridParty/Flot/jquery.flot.min.js")%>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/ThridParty/Flot/jquery.flot.resize.min.js")%>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/ThridParty/jquery-ui.min.js")%>" type="text/javascript"></script>
<link href="<%: Url.Content("~/Content/themes/cupertino/jquery-ui.css") %>" rel="stylesheet" type="text/css" />
<link href="<%: Url.Content("~/Content/after.login.master.css")%>" rel="stylesheet" type="text/css" />
<link href="<%: Url.Content("~/Content/shared/filter.css")%>" rel="stylesheet" type="text/css" />
<link href="<%: Url.Content("~/Content/analytics.screen.css")%>" rel="stylesheet" type="text/css" />
<script src="<%: Url.Content("~/Scripts/filter.js")%>" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<% Html.RenderPartial("Filter", Model.View); %>
<%if (Model.View.Screens.Any())
  { %>
<div class="thumbnails">
<%foreach (var screen in Model.View.Screens)
  { %>
    <a href="<%=Model.View.GetUrlPart(Model.View.SelectedPortfolioId, screen.ApplicationId, screen.Size.ToFormatedString(), screen.Path, Model.View.SelectedDateFrom, Model.View.SelectedDateTo) %>"><img src="/Thumbnails/<%=screen.Id %><%=screen.FileExtension %>"/></a>    
<%} %>
</div>
<%} %>
<div>
    <%if (!Model.View.HasScrolls)
      {%>
        <img alt="Uh-oh! Nobody used your application yet." class="notice" src="/Content/New/Images/notice_nobody_used.png" />
    <%}
      else if (!Model.View.HasFilteredScrolls)
      { %>
        <img alt="Oops, ther is no data for this time period" class="notice" src="/Content/New/Images/notice_no-data.png" />
     }
     else if (!Model.View.HasScrolls && !Model.View.HasFilteredScrolls)
      { %>
        <img alt="No data" src="/Content/New/Images/notice_no-data.png" />
    <%}
      else
      { %>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#show_image').click(function () {
                var url = $('#image').attr('src');
                if (url.indexOf('&cscreen=true') > -1) {
                    url = url.replace('&cscreen=true', '');
                    $('#image').attr('src', url);
                    $('#show_image').text('Show Report');
                } else {
                    $('#image').attr('src', url + '&cscreen=true');
                    $('#show_image').text('Show Screen');
                }
            });
        });
    </script>
    <p><a id="show_image" style="cursor:pointer;">Show Screen</a></p>
    <img id="image" src="/Analytics/ViewHeatMapImage/<%=Model.SubMaster.FilterUrlPart %>" />
<%} %>
</div></asp:Content>