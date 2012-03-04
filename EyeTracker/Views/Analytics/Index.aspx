<%@ Import Namespace="EyeTracker.Helpers" %>
<%@ Page Title="" Language="C#" 
MasterPageFile="~/Views/Shared/AfterLogin.Master" 
Inherits="ViewPage<ViewModelWrapper<AfterLoginViewModel,EyeTracker.Model.Pages.Analytics.IndexViewModel>>" %>

<asp:Content ID="PageTitleContent" ContentPlaceHolderID="PageTitleContent" runat="server">Portfolios</asp:Content>


<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
    <script src="<%: Url.Content("~/Scripts/Analytics.Index.js") %>" type="text/javascript"></script>
    <link href="<%: Url.Content("~/Content/Analytics.Index.css")%>" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var dashboardUrl = '/Analytics/Dashboard/{0}/{1}';
    </script>
</asp:Content>

<asp:Content ID="LeftMenuContent" ContentPlaceHolderID="LeftMenuContent" runat="server">
    <ul>
        <li class="active"><span></span><a>Portfolios</a></li>
        <li><span></span><a href="/Analytics/Dashboard/1">Dashboard</a></li>
        <li><span></span><a href="/Analytics/FingerPrint/1">Finger Print</a></li>
        <li><span></span><a href="/Analytics/EyeTracker/1">Eye Tracker</a></li>
        <li><span></span><a href="/Analytics/Usage/1">Usage</a></li>
        <li class="disabled"><span></span><a <%--href="/Portfolio/Visitors/1"--%>>Visitors</a></li>
        <li class="disabled"><span></span><a <%--href="/Portfolio/MapOverlay/1"--%>>Map Overlay</a></li>
        <li class="disabled"><span></span><a <%--href="/Portfolio/TraficSources/1"--%>>Trafic Sources</a></li>
        <li class="disabled"><span></span><a <%--href="/Portfolio/ContentOverview/1"--%>>Content Overview</a></li>
    </ul>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <h3 class="title">Portfolios</h3>
    <div class="actions">
        <a class="button" href="Portfolio/New"><span class="icon"></span>Add Portfolio</a>
    </div>
    <div class="table-header">
        <div title="Status" class="center" style="width:10%;">Status</div>
        <div title="Description" style="width:56%;">Description</div>
        <div title="Visits" style="width:20%;">Visits</div>
        <div title="Properties" class="center" style="width:10%;">Properties</div>
    </div>
    <div class="table" style="height: 364px;">
        <ul class="computerdata detail" id="target">
            <li class="row">
                <div style="width:10%;" class="center">
                    <div class="status-ok"></div>
                </div>
                <div style="width:56%;">
                    Some description
                </div>
                <div style="width:20%;">
                    20
                </div>
                <div style="width:10%;" class="center">
                    <a href="">
                    <div class="properties"></div>
                    </a>
                </div>
            </li>
        </ul>
    </div>
    <table>
        <tbody>
        <%foreach (var item in Model.View.Portfolios)
          {%>
        <tr class="main" pid="<%: item.Id %>">
            <td class="expand space"></td>
            <td><%: item.Description%></td>
            <td> Visits: <%: item.Visits%></td>
            <td> <a href="Portfolio/Edit/<%: item.Id%>">edit</a> | <a href="/Application/New/<%: item.Id %>">add application</a></td>
        </tr>
        <tr class="sub">
            <td></td>
            <td colspan="3">
            <table>
                <tbody>
                <%if (item.Applications.Count() > 0)
                  { %>
                    <%foreach (var app in item.Applications)
                      { %>
                        <tr pid="<%: item.Id %>" aid="<%: app.Id %>">
                            <td><%: app.Description%></td>
                            <td> Visits: <%: app.Visits%></td>
                            <td> <a href="Application/Edit/<%: item.Id%>/<%: app.Id%>">edit</a></td>
                        </tr>
                    <%} %>
                <%}
                  else
                  {%>
                    <tr><td>The portfolio does not have applications, please <a href="/Application/New/<%: item.Id %>">add application.</a></td></tr>
                <%} %>
                </tbody>
            </table>
            </td>
        </tr>
        <%} %>
        </tbody>
    </table>
</asp:Content>

