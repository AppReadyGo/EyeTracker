<%@ Import Namespace="EyeTracker.Helpers" %>
<%@ Import Namespace="EyeTracker.Model.Pages.Analytics" %>
<%@ Page Title="" Language="C#" 
MasterPageFile="~/Views/Shared/Analytics.Master" 
Inherits="ViewPage<ViewModelWrapper<AfterLoginMasterModel, AnalyticsMasterModel, IndexViewModel>>" %>

<asp:Content ID="PageTitleContent" ContentPlaceHolderID="PageTitleContent" runat="server">Portfolios</asp:Content>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
    <script src="<%: Url.Content("~/Scripts/Analytics.Index.js") %>" type="text/javascript"></script>
    <link href="<%: Url.Content("~/Content/Analytics.Index.css")%>" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var dashboardUrl = '/Analytics/Dashboard/{0}/{1}';
    </script>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <h3 class="title">Portfolios</h3>
    <div class="actions">
        <a class="button" href="Portfolio/New"><span class="icon"></span>Add Portfolio</a>
    </div>
    <div class="table-header">
        <div style="width:5%;"></div>
        <div title="Status" class="center" style="width:10%;">Status</div>
        <div title="Description" style="width:50%;">Description</div>
        <div title="Visits" style="width:20%;">Visits</div>
        <div title="Properties" class="center" style="width:10%;">Properties</div>
    </div>
    <div class="table" style="height: 364px;">
        <ul class="computerdata detail" id="target">
        <%foreach (var item in Model.View.Portfolios)
          {%>
            <li class="row portfolio" pid="<%: item.Id %>">
                <div style="width:5%;" class="center">
                    <div class="expand<%: item.Applications.Any() ? "" : " disabled" %>"></div>
                </div>
                <div style="width:10%;" class="center">
                    <div class="status-ok"></div>
                </div>
                <div style="width:50%;" class="nav">
                    <%: item.Description%>
                </div>
                <div style="width:20%;">
                    <%: item.Visits%>
                </div>
                <div style="width:10%;" class="center">
                    <a href="Portfolio/Edit/<%: item.Id%>" title="edit">
                    <div class="properties"></div>
                    </a>
                    <a href="/Application/New/<%: item.Id %>" title="add application">
                    <div class="new-app"></div>
                    </a>
                </div>
            </li>
            <%if (item.Applications.Any())
                { 
                   foreach (var app in item.Applications)
                    { %>
                    <li class="row app portfolio-<%: item.Id %>" pid="<%: item.Id %>" aid="<%: app.Id %>">
                        <div style="width:5%;" class="center">
                        </div>
                        <div style="width:10%;" class="center">
                            <div class="status-ok"></div>
                        </div>
                        <div style="width:50%;" class="nav">
                            <%: app.Description%>
                        </div>
                        <div style="width:20%;">
                            <%: app.Visits%>
                        </div>
                        <div style="width:10%;" class="center">
                            <a href="Application/Edit/<%: item.Id%>/<%: app.Id%>" title="edit">
                            <div class="properties"></div>
                            </a>
                        </div>
                    </li>
                    <%}
                }
         } %>
       </ul>
    </div>
</asp:Content>

