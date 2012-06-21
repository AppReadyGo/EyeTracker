<%@ Page Title="" Language="C#" 
MasterPageFile="~/Views/Shared/Analytics.Master" 
Inherits="ViewPage<ViewModelWrapper<AfterLoginMasterModel, AnalyticsMasterModel, EyeTracker.Model.Pages.Analytics.DashboardModel>>" %>

<asp:Content ContentPlaceHolderID="PageTitleContent" runat="server">Dashboard</asp:Content>

<asp:Content ContentPlaceHolderID="HeaderContent" runat="server">
<script src="<%: Url.Content("~/Scripts/ThridParty/jquery-ui.min.js")%>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/ThridParty/DateFormat.js")%>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/ThridParty/Flot/jquery.flot.min.js")%>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/ThridParty/Flot/jquery.flot.resize.min.js")%>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/analytics.dashboard.js")%>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/analytics.selector.js")%>" type="text/javascript"></script>
<link href="<%: Url.Content("~/Content/after.login.master.css")%>" rel="stylesheet" type="text/css" />
<link href="<%: Url.Content("~/Content/themes/cupertino/jquery-ui.css") %>" rel="stylesheet" type="text/css" />
<link href="<%: Url.Content("~/Content/shared/filter.css")%>" rel="stylesheet" type="text/css" />
<script src="<%: Url.Content("~/Scripts/filter.js")%>" type="text/javascript"></script>
<link href="<%: Url.Content("~/Content/analytics.dashboard.css")%>" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    var usageChartData = <%= Model.View.UsageChartData %>;
</script>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<h2 class="title">Dashboard</h2>
<% Html.RenderPartial("Filter", Model.View); %>
<table class="dashboard">
<tr>
    <td colspan="2">
        <div class="charts">
            <div class="title"><span>Usage</span></div>
            <div id="usage_charts_place_holder" style="height:200px;width:875px;"></div>
        </div>
    </td>
</tr>
<%--<tr>  
    <td>
        <div class="title"><span>Visitors</span></div>
        <div id="visitors_charts_place_holder" style="height:200px;width:437px;"></div>
    </td>
    <td>
        <div class="title"><span>Map Overlay</span></div>
        <img src="/Content/images/world_map.jpg" class="item" />
    </td>
</tr>--%>
<tr>  
<%--    <td>
        <div class="title"><span>Trafic Sources</span></div>
        <div class="item"></div>
    </td>--%>
    <td colspan="2">
        <div class="title"><span>Content Overview</span></div>
        <section id="content" class="content-overview">
	        <div class="box extra">
		        <div class="border-right">
			        <div class="border-bot">
				        <div class="border-left">
					        <div class="left-top-corner1">
						        <div class="right-top-corner1">
							        <div class="right-bot-corner">
								        <div class="left-bot-corner">
									        <div class="inner">
										        <div class="left-indent line-ver1">
											        <div class="line-ver2">
												        <article class="col-1 indent">
													        <h4>Pathes</h4>
													        <ul class="info-list1">
                                                            <%if (Model.View.ContentOverviewData.Any())
                                                            {
                                                                foreach (var item in Model.View.ContentOverviewData)
                                                                { %>
														            <li><%= item.Path %></li>
														        <% }
                                                            }%>
													        </ul>
												        </article>
												        <article class="col-2 indent">
													        <h4 class="aligncenter">Views</h4>
													        <ul class="info-list1 alt">
                                                            <%if (Model.View.ContentOverviewData.Any())
                                                            {
                                                                foreach (var item in Model.View.ContentOverviewData)
                                                                { %>
														            <li><%= item.Views%></li>
														        <% }
                                                            }%>
													        </ul>
												        </article>
												        <article class="col-3 indent">
													        <h4 class="aligncenter">% Views</h4>
													        <ul class="info-list1 alt">
                                                            <%if (Model.View.ContentOverviewData.Any())
                                                            {
                                                                foreach (var item in Model.View.ContentOverviewData)
                                                                { %>
														            <li></li>
														        <% }
                                                            }%>
													        </ul>
												        </article>
												        <div class="clear"></div>
											        </div>
										        </div>
									        </div>
								        </div>
							        </div>
						        </div>
					        </div>
				        </div>
			        </div>
		        </div>
	        </div>
        </section>
        <div class="item"></div>
    </td>
</tr>
</table>
</asp:Content>



