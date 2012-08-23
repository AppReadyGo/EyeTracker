<%@ Import Namespace="EyeTracker.Model.Pages.Portfolio" %>
<%@ Page Title="" Language="C#" 
MasterPageFile="~/Views/Shared/AfterLogin.Master" 
Inherits="ViewPage<ViewModelWrapper<AfterLoginMasterModel, ApplicationIndexModel>>" %>

<asp:Content ID="PageTitleContent" ContentPlaceHolderID="PageTitleContent" runat="server">Applications</asp:Content>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="<%: Url.Content("~/Content/after.login.master.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%: Url.Content("~/Content/shared/table.css") %>" rel="stylesheet" type="text/css" />
    <script src="<%: Url.Content("~/Scripts/table.js")%>" type="text/javascript"></script>
    <link href="<%: Url.Content("~/Content/application.index.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%: Url.Content("~/Content/shared/bredcrumbs.css")%>" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<div class="crumb">
    <div class="border-right">
		<div class="border-left">
			<div class="inner">
                <ul>
		           <li><a href="/Portfolio"><h4>Portfolios</h4></a></li>
		        </ul>
			</div>
		</div>
    </div>
</div>
<div style="clear:both;"></div>
<div class="content-wrapper">
<h2><%: Model.View.PortfolioDescription%> - Applications</h2>
<div class="wrap">  
	<div class="t-nav">
        <div class="a-l actions">
            <a href="/Application/New/<%: Model.View.PortfolioId %>" class="link2"><span><span>+ Add Application</span></span></a>
        </div>
		<div class="a-l search-box">
			<input type="search" id="search-input" name="s" value="<%=Model.View.SearchStr %>" />
            <a class="link2" onclick="javascript: document.location.href = '/Application/<%: Model.View.PortfolioId %>?cp=1&srch=' + escape($('#search-input').val());"><span><span>Search</span></span></a>
		</div>
		<div class='t-nav-pages <%= Model.View.IsOnePage ? "one-page" : "" %>'>
			<span class="d-num"><%= Model.View.Count %> items</span>
			<span class='pagination-links'>
				<a class='first-page <%= Model.View.IsOnePage ? "disabled" : "" %>' title='Go to the first page' href='/Application/<%: Model.View.PortfolioId %>?cp=1<%= Model.View.UrlPart %>'>&laquo;</a>
				<a class='prev-page <%= Model.View.PreviousPage.HasValue ? "" : "disabled" %>' title='Go to the previous page' href='/Application/<%: Model.View.PortfolioId %>?cp=<%= Model.View.PreviousPage.HasValue ? Model.View.PreviousPage.Value : 1 %><%=Model.View.UrlPart %>'>&lsaquo;</a>
				<span class="paging-input">
					<input class='cur-p' title='Current page' type='text' name='paged' value='<%= Model.View.CurPage %>' size='1' /> of <span class='total-pages'><%= Model.View.TotalPages%></span>
				</span>
				<a class='next-page <%= Model.View.NextPage.HasValue ? "" : "disabled" %>' title='Go to the next page' href='/Application/<%: Model.View.PortfolioId %>?cp=<%= Model.View.NextPage.HasValue ? Model.View.NextPage.Value : Model.View.TotalPages %><%=Model.View.UrlPart %>'>&rsaquo;</a>
				<a class='last-page <%= Model.View.IsOnePage ? "disabled" : "" %>' title='Go to the last page' href='/Application/<%: Model.View.PortfolioId %>?cp=<%= Model.View.TotalPages%><%=Model.View.UrlPart %>'>&raquo;</a>
			</span>
		</div>
	</div><!-- /table-head -->
	<table class="tbl fixed" cellspacing="0">
		<thead>
		<tr>
			<th scope='col' class='manage-column column-active'>Active</th>
			<th scope='col' class='manage-column column-desc'>Description</th>
			<th scope='col' class='manage-column column-key'>Key</th>
			<th scope='col' class='manage-column column-visits'>Visits</th>
		</tr>
		</thead>
		<tfoot>
		<tr>
			<th scope='col' class='manage-column column-active'>Active</th>
			<th scope='col' class='manage-column column-desc'>Description</th>
			<th scope='col' class='manage-column column-key'>Key</th>
			<th scope='col' class='manage-column column-visits'>Visits</th>
		</tr>
		</tfoot>
		<tbody id="the-list" class='list:user'>
        <%if (Model.View.Applications.Any())
          {
              foreach (var item in Model.View.Applications)
              { %>	
		<tr class="<%= item.IsAlternative ? "" : "alternate" %>">
			<td class="column-email">
				<div class="<%: !item.IsActive ? "status-alert" : "status-ok"%>" title="<%: item.IsActive ? "" : "The application has not been active for past 3 days." %>"></div>
			</td>
			<td class="column-desc">
                <span><a href="/Analytics/Dashboard/?pid=<%= Model.View.PortfolioId %>&aid=<%= item.Id %>"><%= item.Description%></a></span>
				<br />
				<div class="row-actions">
					<span class='screens'><a href="/Application/Screens/<%= item.Id %>">Screens</a> | </span>
                    <span class='edit'><a href="/Application/Edit/<%= item.Id %>">Edit</a> | </span>
					<span class='delete'><a class='submitdelete' href='/Application/Remove/<%= item.Id %>' onclick="javascript:return confirm('Are you realy want to remove <%= item.Description %> application? The operation is not recoverable!');">Delete</a></span>
				</div>          
            </td>
			<td class="role column-visits"><%= item.Key %></td>
			<td class="role column-visits"><%= item.Visits %></td>
		</tr>
        <%} 
          }
          else
          {%>
          <tr class="no-items"><td colspan="4" class="colspanchange">No matching applications were found.</td></tr>
        <%} %>
		</tbody>
	</table>
	<div class="t-nav bottom">
		<div class='t-nav-pages <%= Model.View.IsOnePage ? "one-page" : "" %>'>
			<span class="d-num"><%= Model.View.Count %> items</span>
			<span class='pagination-links'>
				<a class='first-page <%= Model.View.IsOnePage ? "disabled" : "" %>' title='Go to the first page' href='/Application/<%: Model.View.PortfolioId %>?cp=1<%=Model.View.UrlPart %>'>&laquo;</a>
				<a class='prev-page <%= Model.View.PreviousPage.HasValue ? "" : "disabled" %>' title='Go to the previous page' href='/Application/<%: Model.View.PortfolioId %>?cp=<%= Model.View.PreviousPage.HasValue ? Model.View.PreviousPage.Value : 1 %><%=Model.View.UrlPart %>'>&lsaquo;</a>
				<span class="paging-input"><%= Model.View.CurPage %> of <span class='total-pages'><%= Model.View.TotalPages%></span></span>
				<a class='next-page <%= Model.View.NextPage.HasValue ? "" : "disabled" %>' title='Go to the next page' href='/Application/<%: Model.View.PortfolioId %>?cp=<%= Model.View.NextPage.HasValue ? Model.View.NextPage.Value : Model.View.TotalPages %><%=Model.View.UrlPart %>'>&rsaquo;</a>
				<a class='last-page <%= Model.View.IsOnePage ? "disabled" : "" %>' title='Go to the last page' href='/Application/<%: Model.View.PortfolioId %>?cp=<%= Model.View.TotalPages%><%=Model.View.UrlPart %>'>&raquo;</a>
			</span>
		</div>
		<br class="clear" />
	</div>
</div><!-- /wrap -->
<br class="clear" />
</div>
</asp:Content>