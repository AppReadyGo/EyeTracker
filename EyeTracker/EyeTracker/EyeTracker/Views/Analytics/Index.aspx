<%@ Import Namespace="EyeTracker.Helpers" %>
<%@ Import Namespace="EyeTracker.Model.Pages.Analytics" %>
<%@ Page Title="" Language="C#" 
MasterPageFile="~/Views/Shared/Analytics.Master" 
Inherits="ViewPage<ViewModelWrapper<AfterLoginMasterModel, AnalyticsMasterModel, IndexViewModel>>" %>

<asp:Content ID="PageTitleContent" ContentPlaceHolderID="PageTitleContent" runat="server">Portfolios</asp:Content>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
    <script src="<%: Url.Content("~/Scripts/analytics.index.js") %>" type="text/javascript"></script>
    <link href="<%: Url.Content("~/Content/analytics.index.css")%>" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="title">Portfolios</h2>
    <%if ((bool)ViewData["IsAdmin"])
      { %>
    <div class="actions">
        <a href="/portfolio/new" class="link2"><span><span>Add Portfolio</span></span></a>
    </div>
    <%} %>
    <div class="table-header">
        <div style="width:5%;"></div>
        <div title="Status" class="center" style="width:10%;">Status</div>
        <div title="Description" style="width:50%;">Description</div>
        <div title="Visits" style="width:20%;">Activity</div>
        <div title="Properties" class="center" style="width:10%;">
        <%if ((bool)ViewData["IsAdmin"])
        { %>
        Properties
        <%} %>
        </div>
    </div>
    <div class="table" style="height: 364px;">
        <ul class="computerdata detail" id="target">
        <%foreach (var item in Model.View.PortfoliosInfo)
          {%>
            <li class="row portfolio<%: item.Applications.Any() ? "" : " disabled" %>" url="/analytics/dashboard/?pid=<%: item.Id %>" pid="<%: item.Id %>">
                <div style="width:5%;" class="center">
                    <div class="expand"></div>
                </div>
                <div style="width:10%;" class="center">
                    <div class="<%: item.Applications.Any(a => a.Visits == 0) ? "status-alert" : "status-ok"%>"></div>
                </div>
                <div style="width:50%;" class="nav">
                    <%: item.Description%>
                </div>
                <div style="width:20%;">
                    <%:item.Visits%>
                </div>
                <div style="width:10%;" class="center">
                <%if ((bool)ViewData["IsAdmin"])
                  { %>
                    <a href="/Application/New/<%: item.Id %>" title="add application" class="new-app"><span></span></a>
                    <a href="/Portfolio/Edit/<%: item.Id%>" title="edit" class="edit"><span></span></a>
                    <a href="/Portfolio/Remove/<%: item.Id %>" title="remove portfolio" class="remove" onclick="javascript:return confirm('Are you realy want to remove the portfolio and all analytics data?');"><span></span></a>
                <%} %>
                </div>
            </li>
            <%if (item.Applications.Any())
                { 
                   foreach (var app in item.Applications)
                    { %>
                    <li class="row app portfolio-<%: item.Id %><%: app.Visits != 0 ? "" : " disabled" %>" url="/analytics/dashboard/?pid=<%: item.Id %>&aid=<%: app.Id %>">
                        <div style="width:5%;" class="center"></div>
                        <div style="width:10%;" class="center">
                            <div class="<%: app.Visits > 0 ? "status-ok" : "status-alert"%>"></div>
                        </div>
                        <div style="width:50%;" class="nav">
                            <%: app.Description%>
                        </div>
                        <div style="width:20%;">
                            <%: app.Visits %>
                        </div>
                        <div style="width:10%;" class="center">
                        <%if ((bool)ViewData["IsAdmin"])
                          { %>
                            <a href="/Application/Edit/<%: app.Id%>" title="edit" class="edit"><span></span></a>
                            <a href="/Application/Remove/<%: app.Id%>" title="remove application" class="remove" onclick="javascript:return confirm('Are you realy want to remove the application and all analytics data?');"><span></span></a>
                        <%} %>
                        </div>
                    </li>
                    <%}
                }
         } %>
       </ul>
    </div>
</asp:Content>

