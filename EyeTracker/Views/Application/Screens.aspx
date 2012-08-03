<%@ Import Namespace="EyeTracker.Model.Pages.Application" %>
<%@ Page Title="" Language="C#" 
MasterPageFile="~/Views/Shared/AfterLogin.Master" 
Inherits="ViewPage<ViewModelWrapper<AfterLoginMasterModel, ScreensListModel>>" %>

<asp:Content ID="PageTitleContent" ContentPlaceHolderID="PageTitleContent" runat="server"><%: Model.View.ApplicationDescription%> - Screens</asp:Content>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="<%: Url.Content("~/Content/after.login.master.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%: Url.Content("~/Content/shared/table.css") %>" rel="stylesheet" type="text/css" />
    <script src="<%: Url.Content("~/Scripts/table.js")%>" type="text/javascript"></script>
    <link href="<%: Url.Content("~/Content/application.screens.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%: Url.Content("~/Content/shared/bredcrumbs.css")%>" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="<%: Url.Content("~/Scripts/ThridParty/fancybox/jquery.fancybox.css?v=2.0.6")%>" type="text/css" media="screen" />
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/ThridParty/fancybox/jquery.fancybox.pack.js?v=2.0.6")%>"></script>

    <!-- Optionally add helpers - button, thumbnail and/or media -->
    <link rel="stylesheet" href="<%: Url.Content("~/Scripts/ThridParty/fancybox/helpers/jquery.fancybox-buttons.css?v=1.0.2")%>" type="text/css" media="screen" />
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/ThridParty/fancybox/helpers/jquery.fancybox-buttons.js?v=1.0.2")%>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/ThridParty/fancybox/helpers/jquery.fancybox-media.js?v=1.0.0")%>"></script>

    <link rel="stylesheet" href="<%: Url.Content("~/Scripts/ThridParty/fancybox/helpers/jquery.fancybox-thumbs.css?v=2.0.6")%>" type="text/css" media="screen" />
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/ThridParty/fancybox/helpers/jquery.fancybox-thumbs.js?v=2.0.6")%>"></script>
    <script type="text/javascript">
         $(function () {
             $('#the-list .column-image a').fancybox();
         });
    </script>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="crumb">
    <div class="border-right">
		<div class="border-left">
			<div class="inner">
                <ul>
		           <li><a href="/Portfolio"><h4>Portfolios</h4></a></li>
		           <li><a href="/Application/<%: Model.View.PortfolioId %>"><h4><%: Model.View.PortfolioDescription%> - Applications</h4></a></li>
		        </ul>
			</div>
		</div>
    </div>
</div>
<div style="clear:both;"></div>
<div class="content-wrapper">
<h2><%: Model.View.ApplicationDescription%> - Screens</h2>
<div class="wrap">  
	<div class="t-nav">
        <div class="a-l search-box">
			<input type="search" id="search-input" name="s" value="<%=Model.View.SearchStr %>" />
            <a class="link2" onclick="javascript: document.location.href = '/Application/Screens/<%: Model.View.ApplicationId %>?cp=1&srch=' + escape($('#search-input').val());"><span><span>Search</span></span></a>
		</div>
        <div class="a-l actions">
            <a href="/Application/ScreenNew/<%: Model.View.ApplicationId %>" class="link2"><span><span>+ Add Screen</span></span></a>
        </div>
		<div class='t-nav-pages <%= Model.View.IsOnePage ? "one-page" : "" %>'>
			<span class="d-num"><%= Model.View.Count %> items</span>
			<span class='pagination-links'>
				<a class='first-page <%= Model.View.IsOnePage ? "disabled" : "" %>' title='Go to the first page' href='/Application/Screens/<%: Model.View.ApplicationId %>?cp=1<%= Model.View.UrlPart %>'>&laquo;</a>
				<a class='prev-page <%= Model.View.PreviousPage.HasValue ? "" : "disabled" %>' title='Go to the previous page' href='/Application/Screens/<%: Model.View.ApplicationId %>?cp=<%= Model.View.PreviousPage.HasValue ? Model.View.PreviousPage.Value : 1 %><%=Model.View.UrlPart %>'>&lsaquo;</a>
				<span class="paging-input">
					<input class='cur-p' title='Current page' type='text' name='paged' value='<%= Model.View.CurPage %>' size='1' /> of <span class='total-pages'><%= Model.View.TotalPages%></span>
				</span>
				<a class='next-page <%= Model.View.NextPage.HasValue ? "" : "disabled" %>' title='Go to the next page' href='/Application/Screens/<%: Model.View.ApplicationId %>?cp=<%= Model.View.NextPage.HasValue ? Model.View.NextPage.Value : Model.View.TotalPages %><%=Model.View.UrlPart %>'>&rsaquo;</a>
				<a class='last-page <%= Model.View.IsOnePage ? "disabled" : "" %>' title='Go to the last page' href='/Application/Screens/<%: Model.View.ApplicationId %>?cp=<%= Model.View.TotalPages%><%=Model.View.UrlPart %>'>&raquo;</a>
			</span>
		</div>
	</div><!-- /table-head -->
	<table class="tbl fixed" cellspacing="0">
		<thead>
		<tr>
			<th scope='col' class='manage-column column-image'>Image</th>
			<th scope='col' class='manage-column column-path sortable'><a href="/Application/Screens/<%: Model.View.ApplicationId %>?cp=1&orderby=path&order=<%=Model.View.PathOrder + Model.View.SearchStrUrlPart %>"><span>Path</span><span class="sorting-indicator"></span></th>
			<th scope='col' class='manage-column column-width sortable'><a href="/Application/Screens/<%: Model.View.ApplicationId %>?cp=1&orderby=width&order=<%=Model.View.WidthOrder + Model.View.SearchStrUrlPart %>"><span>Width</span><span class="sorting-indicator"></span></th>
			<th scope='col' class='manage-column column-height sortable'><a href="/Application/Screens/<%: Model.View.ApplicationId %>?cp=1&orderby=height&order=<%=Model.View.HeightOrder + Model.View.SearchStrUrlPart %>"><span>Height</span><span class="sorting-indicator"></span></th>
		</tr>
		</thead>
		<tfoot>
		<tr>
			<th scope='col' class='manage-column column-image'>Image</th>
			<th scope='col' class='manage-column column-path sortable'><a href="/Application/Screens/<%: Model.View.ApplicationId %>?cp=1&orderby=path&order=<%=Model.View.PathOrder + Model.View.SearchStrUrlPart %>"><span>Path</span><span class="sorting-indicator"></span></th>
			<th scope='col' class='manage-column column-width sortable'><a href="/Application/Screens/<%: Model.View.ApplicationId %>?cp=1&orderby=width&order=<%=Model.View.WidthOrder + Model.View.SearchStrUrlPart %>"><span>Width</span><span class="sorting-indicator"></span></th>
			<th scope='col' class='manage-column column-height sortable'><a href="/Application/Screens/<%: Model.View.ApplicationId %>?cp=1&orderby=height&order=<%=Model.View.HeightOrder + Model.View.SearchStrUrlPart %>"><span>Height</span><span class="sorting-indicator"></span></th>
		</tr>
		</tfoot>
		<tbody id="the-list" class='list:user'>
        <%if (Model.View.Screens.Any())
          {
              foreach (var item in Model.View.Screens)
              { %>	
		<tr class="<%= item.IsAlternative ? "" : "alternate" %>">
			<td class="column-image">
                <a data-fancybox-group="gallery" href="/Screens/<%= item.Id %><%= item.FileExtention %>" title="Path: <%= item.Path %>, Width: <%= item.Width %>, Height: <%= item.Height %>"><img width="60" height="60" src="/Screens/<%= item.Id %><%= item.FileExtention %>" title="Path: <%= item.Path %>, Width: <%= item.Width %>, Height: <%= item.Height %>"/></a>
			</td>
			<td class="role column-path">
                <span><%= item.Path %></span>
				<br />
				<div class="row-actions">
                    <span class='edit'><a href="/Application/ScreenEdit/<%= item.Id %>">Edit</a> | </span>
					<span class='delete'><a class='submitdelete' href='/Application/ScreenRemove/<%= item.Id %>' onclick="javascript:return confirm('Are you realy want to remove the screen? The operation is not recoverable!');">Delete</a></span>
				</div>          
            </td>
			<td class="role column-width"><%= item.Width %></td>
			<td class="role column-height"><%= item.Height %></td>
		</tr>
        <%} 
          }
          else
          {%>
          <tr class="no-items"><td colspan="4" class="colspanchange">No screens were found.</td></tr>
        <%} %>
		</tbody>
	</table>
	<div class="t-nav bottom">
		<div class='t-nav-pages <%= Model.View.IsOnePage ? "one-page" : "" %>'>
			<span class="d-num"><%= Model.View.Count %> items</span>
			<span class='pagination-links'>
				<a class='first-page <%= Model.View.IsOnePage ? "disabled" : "" %>' title='Go to the first page' href='/Application/Screens/<%: Model.View.ApplicationId %>?cp=1<%=Model.View.UrlPart %>'>&laquo;</a>
				<a class='prev-page <%= Model.View.PreviousPage.HasValue ? "" : "disabled" %>' title='Go to the previous page' href='/Application/Screens/<%: Model.View.ApplicationId %>?cp=<%= Model.View.PreviousPage.HasValue ? Model.View.PreviousPage.Value : 1 %><%=Model.View.UrlPart %>'>&lsaquo;</a>
				<span class="paging-input"><%= Model.View.CurPage %> of <span class='total-pages'><%= Model.View.TotalPages%></span></span>
				<a class='next-page <%= Model.View.NextPage.HasValue ? "" : "disabled" %>' title='Go to the next page' href='/Application/Screens/<%: Model.View.ApplicationId %>?cp=<%= Model.View.NextPage.HasValue ? Model.View.NextPage.Value : Model.View.TotalPages %><%=Model.View.UrlPart %>'>&rsaquo;</a>
				<a class='last-page <%= Model.View.IsOnePage ? "disabled" : "" %>' title='Go to the last page' href='/Application/Screens/<%: Model.View.ApplicationId %>?cp=<%= Model.View.TotalPages%><%=Model.View.UrlPart %>'>&raquo;</a>
			</span>
		</div>
		<br class="clear" />
	</div>
</div><!-- /wrap -->
<br class="clear" />
</div>
</asp:Content>