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
        <li><a class="active">Portfolios</a></li>
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
        <%if (Model.View.PortfoliosCount == 0)
          {%>
           <tr><td>No Portfolios</td></tr>
        <%}
          else
          {%>
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
        <%} %>
        </tbody>
    </table>
</asp:Content>

