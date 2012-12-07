<%@ Page Title="" 
Language="C#" 
MasterPageFile="~/Views/Shared/Analytics.Master" 
Inherits="ViewPage<ViewModelWrapper<AfterLoginMasterModel, AnalyticsMasterModel, EyeTracker.Model.Filter.FilterModel>>" %>

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
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<% Html.RenderPartial("Filter", Model.View); %>
<div>
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
    <img id="image" width="320" height="480" src="/Analytics/ClickHeatMapImage/<%=Model.SubMaster.FilterUrlPart %>" />
</div>
</asp:Content>