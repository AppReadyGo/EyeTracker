<%@ Import Namespace="EyeTracker.Model.Pages.Portfolio" %>
<%@ Page Title="" Language="C#" 
MasterPageFile="~/Views/Shared/AfterLogin.Master" 
Inherits="ViewPage<ViewModelWrapper<AfterLoginMasterModel, PortfolioIndexModel>>" %>

<asp:Content ID="PageTitleContent" ContentPlaceHolderID="PageTitleContent" runat="server">Portfolios</asp:Content>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="<%: Url.Content("~/Content/after.login.master.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%: Url.Content("~/Content/shared/table.css") %>" rel="stylesheet" type="text/css" />
    <script src="<%: Url.Content("~/Scripts/table.js")%>" type="text/javascript"></script>
    <link href="<%: Url.Content("~/Content/portfolio.index.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%: Url.Content("~/Content/shared/bredcrumbs.css")%>" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<div class="crumb">
    <div class="border-right">
		<div class="border-left">
			<div class="inner">
                <ul>
		        </ul>
			</div>
		</div>
    </div>
</div>
<div style="clear:both;"></div>
<div class="content-wrapper">
<h2>Portfolios</h2>
<div class="a-r top-panel">
	<table class="tbl fixed" cellspacing="0">
		<thead>
		<tr>
			<th scope='col' class='manage-column column-active'>Most Active Applications</th>
		</tr>
		</thead>
        <tbody>
        <%if (Model.View.TopApplications.Any()) {
              foreach (var item in Model.View.TopApplications) { %>	
        <tr class="<%= item.IsAlternative ? "" : "alternate" %>"><td><a href="/Analytics/Dashboard/?pid=<%: item.PortfolioId %>&aid=<%: item.Id %>"><%: item.Description %></a></td></tr>
        <%  } 
          } else {%>
          <tr class="no-items"><td class="colspanchange">No applications.</td></tr>
        <%} %>
        </tbody>
    </table>
	<table class="tbl fixed" cellspacing="0">
		<thead>
		<tr>
			<th scope='col' class='manage-column column-active'>Most Active Screens</th>
		</tr>
		</thead>
        <tbody>
        <%if (Model.View.TopScreens.Any()) {
              foreach (var item in Model.View.TopScreens) { %>	
        <tr class="<%= item.IsAlternative ? "" : "alternate" %>"><td><a href="/Analytics/Dashboard/?pid=<%: item.PortfolioId %>&aid=<%: item.ApplicationId %>&ss=<%: item.ScreenSize %>&p=<%: item.Path %>"><%: item.Path %>: <%: item.ScreenSize %></a></td></tr>
        <%  } 
          } else {%>
          <tr class="no-items"><td class="colspanchange">No screens.</td></tr>
        <%} %>
        </tbody>
    </table>
</div>
<div class="wrap">
	<div class="t-nav">
        <div class="a-l actions">
            <a href="/Portfolio/New/" class="link2"><span><span>+ Add Portfolio</span></span></a>
        </div>
		<div class="a-l search-box">
			<input type="search" id="search-input" name="s" value="<%=Model.View.SearchStr %>" />
            <a class="link2" onclick="javascript: document.location.href = '/Portfolio/?cp=1&srch=' + escape($('#search-input').val());"><span><span>Search</span></span></a>
		</div>
		<div class='t-nav-pages <%= Model.View.IsOnePage ? "one-page" : "" %>'>
			<span class="d-num"><%= Model.View.Count %> items</span>
			<span class='pagination-links'>
				<a class='first-page <%= Model.View.IsOnePage ? "disabled" : "" %>' title='Go to the first page' href='/Portfolio/?cp=1<%= Model.View.UrlPart %>'>&laquo;</a>
				<a class='prev-page <%= Model.View.PreviousPage.HasValue ? "" : "disabled" %>' title='Go to the previous page' href='/Portfolio/?cp=<%= Model.View.PreviousPage.HasValue ? Model.View.PreviousPage.Value : 1 %><%=Model.View.UrlPart %>'>&lsaquo;</a>
				<span class="paging-input">
					<input class='cur-p' title='Current page' type='text' name='paged' value='<%= Model.View.CurPage %>' size='1' /> of <span class='total-pages'><%= Model.View.TotalPages%></span>
				</span>
				<a class='next-page <%= Model.View.NextPage.HasValue ? "" : "disabled" %>' title='Go to the next page' href='/Portfolio/?cp=<%= Model.View.NextPage.HasValue ? Model.View.NextPage.Value : Model.View.TotalPages %><%=Model.View.UrlPart %>'>&rsaquo;</a>
				<a class='last-page <%= Model.View.IsOnePage ? "disabled" : "" %>' title='Go to the last page' href='/Portfolio/?cp=<%= Model.View.TotalPages%><%=Model.View.UrlPart %>'>&raquo;</a>
			</span>
		</div>
	</div><!-- /table-head -->
	<table class="tbl fixed" cellspacing="0">
		<thead>
		<tr>
			<th scope='col' class='manage-column column-active'>Active</th>
			<th scope='col' class='manage-column column-desc'>Description</th>
			<th scope='col' class='manage-column column-applications'>Applications</th>
			<th scope='col' class='manage-column column-visits'>Visits</th>
		</tr>
		</thead>
		<tfoot>
		<tr>
			<th scope='col' class='manage-column column-active'>Active</th>
			<th scope='col' class='manage-column column-desc'>Description</th>
			<th scope='col' class='manage-column column-applications'>Applications</th>
			<th scope='col' class='manage-column column-visits'>Visits</th>
		</tr>
		</tfoot>
		<tbody id="the-list" class='list:user'>
        <%if (Model.View.Portfolios.Any()) {
              foreach (var item in Model.View.Portfolios) { %>	
		<tr class="<%= item.IsAlternative ? "" : "alternate" %>">
			<td class="column-email">
				<div class="<%: !item.IsActive ? "status-alert" : "status-ok"%>" title="<%: item.IsActive ? "" : "One of the portfolio's application has not been active for past 3 days." %>"></div>
			</td>
			<td class="column-desc">
                <span><a href="/Application/<%= item.Id %>"><%= item.Description%></a></span>
				<br />
				<div class="row-actions">
					<span class='edit'><a href="/Portfolio/Edit/<%= item.Id %>">Edit</a> | </span>
					<span class='delete'><a class='submitdelete' href='/Portfolio/Remove/<%= item.Id %>' onclick="javascript:return confirm('Are you realy want to remove <%= item.Description %> portfolio? The operation is not recoverable!');">Delete</a></span>
				</div>          
            </td>
			<td class="column-applications">
                <span><%= item.ApplicationsCount %></span>
                <br />
				<div class="row-actions">
					<span class='add-application'><a href="/Application/New/<%= item.Id %>">Add Application</a></span>
				</div>
            </td>
			<td class="role column-visits"><%= item.Visits %></td>
		</tr>
        <%  } 
          } else {%>
          <tr class="no-items"><td colspan="4" class="colspanchange">No matching portfolios were found.</td></tr>
        <%} %>
		</tbody>
	</table>
	<div class="t-nav bottom">
		<div class='t-nav-pages <%= Model.View.IsOnePage ? "one-page" : "" %>'>
			<span class="d-num"><%= Model.View.Count %> items</span>
			<span class='pagination-links'>
				<a class='first-page <%= Model.View.IsOnePage ? "disabled" : "" %>' title='Go to the first page' href='/Portfolio/?cp=1<%=Model.View.UrlPart %>'>&laquo;</a>
				<a class='prev-page <%= Model.View.PreviousPage.HasValue ? "" : "disabled" %>' title='Go to the previous page' href='/Portfolio/?cp=<%= Model.View.PreviousPage.HasValue ? Model.View.PreviousPage.Value : 1 %><%=Model.View.UrlPart %>'>&lsaquo;</a>
				<span class="paging-input"><%= Model.View.CurPage %> of <span class='total-pages'><%= Model.View.TotalPages%></span></span>
				<a class='next-page <%= Model.View.NextPage.HasValue ? "" : "disabled" %>' title='Go to the next page' href='/Portfolio/?cp=<%= Model.View.NextPage.HasValue ? Model.View.NextPage.Value : Model.View.TotalPages %><%=Model.View.UrlPart %>'>&rsaquo;</a>
				<a class='last-page <%= Model.View.IsOnePage ? "disabled" : "" %>' title='Go to the last page' href='/Portfolio/?cp=<%= Model.View.TotalPages%><%=Model.View.UrlPart %>'>&raquo;</a>
			</span>
		</div>
		<br class="clear" />
	</div>
</div><!-- /wrap -->

<br class="clear" />
</div>
</asp:Content>