<%@ Import Namespace="EyeTracker.Common" %>
<%@ Page Title="" 
Language="C#" 
MasterPageFile="~/Views/Shared/Analytics.Master" 
Inherits="ViewPage<ViewModelWrapper<AfterLoginMasterModel, AnalyticsMasterModel, EyeTracker.Model.Pages.Analytics.FingerPrintModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitleContent" runat="server">Finger Print</asp:Content>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
<script src="<%: Url.Content("~/Scripts/ThridParty/DateFormat.js")%>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/ThridParty/Flot/jquery.flot.min.js")%>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/ThridParty/Flot/jquery.flot.resize.min.js")%>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/ThridParty/jquery-ui.min.js")%>" type="text/javascript"></script>
<link href="<%: Url.Content("~/Content/themes/cupertino/jquery-ui.css") %>" rel="stylesheet" type="text/css" />
<link href="<%: Url.Content("~/Content/shared/filter.css")%>" rel="stylesheet" type="text/css" />
<script src="<%: Url.Content("~/Scripts/filter.js")%>" type="text/javascript"></script>
<script type="text/javascript">
    var graphsData = <%= Model.View.GraphsData %>;
    $(document).ready(function () {
    var plot = $.plot($("#visits_graph"), graphsData.visits, {
        xaxis: { mode: "time", timeformat: '%d %b %y' },
        yaxis: { min: 0, tickDecimals: 0 },
        series: {
            lines: { show: true },
            points: { show: true }
        },
        grid: { hoverable: true, clickable: true }
    });
    var plot = $.plot($("#clicks_graph"), graphsData.clicks, {
        xaxis: { mode: "time", timeformat: '%d %b %y' },
        yaxis: { min: 0, tickDecimals: 0 },
        series: {
            lines: { show: true },
            points: { show: true }
        },
        grid: { hoverable: true, clickable: true }
    });
    var plot = $.plot($("#scrolls_graph"), graphsData.scrolls, {
        xaxis: { mode: "time", timeformat: '%d %b %y' },
        yaxis: { min: 0, tickDecimals: 0 },
        series: {
            lines: { show: true },
            points: { show: true }
        },
        grid: { hoverable: true, clickable: true }
    });
});
</script>
<style>
.fingerprint td{vertical-align:top;padding:0px;}
.fingerprint .title{border-bottom:1px solid #666; margin-bottom:10px;}
.fingerprint .title span{line-height:23px;color:#fff;
                       background-color:#666;border:1px solid #666;border-bottom-width:2px;
                       border-radius:2px 2px 0px 0px;padding:2px 5px 2px 5px;}
.fingerprint .charts{margin-left:20px;}
</style>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<% Html.RenderPartial("Filter", Model.View); %>
<div>
    <table class="fingerprint">
        <tr>
            <td>
                <%if (Model.View.Screens.Any())
                  { %>
                        <div class="thumbnails">
                        <%foreach (var screen in Model.View.Screens)
                            { %>
                            <a href="<%=Model.View.GetUrlPart(Model.View.SelectedPortfolioId, screen.ApplicationId, screen.Size.ToFormatedString(), screen.Path, Model.View.SelectedDateFrom, Model.View.SelectedDateTo) %>"><img src="/Thumbnails/<%=screen.Id %><%=screen.FileExtension %>"/></a>    
                        <%} %>
                        </div>
                <%} %>
                <%if (!Model.View.HasClicks)
                    { %>
                    <img alt="Uh-oh! Nobody used your application yet." class="notice" src="/Content/New/Images/notice_nobody_used.png" />
                <%}
                    else if (Model.View.ClicksAmount == 0)
                    { %>
                    <img alt="Oops, ther is no data for this time period" class="notice" src="/Content/New/Images/notice_no-data.png" />
                <%}
                    else if (!Model.View.ScreenId.HasValue && !Model.View.HasScrolls)
                    {%>
                    <img alt="Ahem! We have the data but no screenshot." class="notice" src="/Content/New/Images/notice_no_screen_shot.png" style="margin-bottom:40px;" />
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
                                    $(this).text('Show Report');
                                } else {
                                    $('#image').attr('src', url + '&cscreen=true');
                                    $(this).text('Show Screen');
                                }
                            });
                        });
                    </script>
                    <p><a id="show_image" style="cursor:pointer;">Show Screen</a></p>
                    <div><img sc id="image" width="320" src="/Analytics/ClickHeatMapImage/<%=Model.SubMaster.FilterUrlPart %>" /></div>
                <%} %>
            </td>
            <td>
                <%if (Model.View.HasClicks && Model.View.ClicksAmount > 0 && (Model.View.ScreenId.HasValue || Model.View.HasScrolls))
                    {%>
                <div class="charts">
                    <div class="title"><span>Visits (<%=Model.View.VisitsAmount%>)</span></div>
                    <div id="visits_graph" style="height:200px;width:575px;"></div>
                </div>
                <div class="charts">
                    <div class="title"><span>Clicks (<%=Model.View.ClicksAmount%>)</span></div>
                    <div id="clicks_graph" style="height:200px;width:575px;"></div>
                <div class="charts">
                    <div class="title"><span>Scrolls (<%=Model.View.ScrollsAmount%>)</span></div>
                    <div id="scrolls_graph" style="height:200px;width:575px;"></div>
                </div>
                <%} %>
            </td>
         </tr>
    </table>
</div>
</asp:Content>