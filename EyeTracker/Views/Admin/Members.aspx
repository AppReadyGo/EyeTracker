<%@ Import Namespace="EyeTracker.Model.Pages.Admin" %>
<%@ Page Title="" Language="C#" 
MasterPageFile="~/Views/Admin/Admin.Master" 
Inherits="ViewPage<ViewModelWrapper<AfterLoginMasterModel, AdminMasterModel, EyeTracker.Model.MembersPagingModel>>" %>

<asp:Content ContentPlaceHolderID="PageTitleContent" runat="server">Members</asp:Content>

<asp:Content ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="<%: Url.Content("~/Content/shared/table.css") %>" rel="stylesheet" type="text/css" />
    <script src="<%: Url.Content("~/Scripts/table.js")%>" type="text/javascript"></script>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="wrap">
	<h2>Members</h2>
	<div class="t-nav">
        <div class="a-l actions">
            <a href="/Admin/NewMember" class="add-new-h2">+ Add New</a>
        </div>
		<div class="a-l search-box">
			<input type="search" id="user-search-input" name="s" value="<%=Model.View.SearchStr %>" />
			<input type="button" name="" id="search-submit" class="button" value="Search" onclick="javascript: document.location.href = '/Admin/Members?cp=1&srch=' + escape($('#user-search-input').val());" />
		</div>
		<div class='t-nav-pages <%= Model.View.IsOnePage ? "one-page" : "" %>'>
			<span class="d-num"><%= Model.View.Count %> items</span>
			<span class='pagination-links'>
				<a class='first-page <%= Model.View.IsOnePage ? "disabled" : "" %>' title='Go to the first page' href='/Admin/Members?cp=1<%= Model.View.UrlPart %>'>&laquo;</a>
				<a class='prev-page <%= Model.View.PreviousPage.HasValue ? "" : "disabled" %>' title='Go to the previous page' href='/Admin/Members?cp=<%= Model.View.PreviousPage.HasValue ? Model.View.PreviousPage.Value : 1 %><%=Model.View.UrlPart %>'>&lsaquo;</a>
				<span class="paging-input">
					<input class='cur-p' title='Current page' type='text' name='paged' value='<%= Model.View.CurPage %>' size='1' /> of <span class='total-pages'><%= Model.View.TotalPages%></span>
				</span>
				<a class='next-page <%= Model.View.NextPage.HasValue ? "" : "disabled" %>' title='Go to the next page' href='/Admin/Members?cp=<%= Model.View.NextPage.HasValue ? Model.View.NextPage.Value : Model.View.TotalPages %><%=Model.View.UrlPart %>'>&rsaquo;</a>
				<a class='last-page <%= Model.View.IsOnePage ? "disabled" : "" %>' title='Go to the last page' href='/Admin/Members?cp=<%= Model.View.TotalPages%><%=Model.View.UrlPart %>'>&raquo;</a>
			</span>
		</div>
	</div><!-- /table-head -->
	<table class="tbl fixed" cellspacing="0">
		<thead>
		<tr>
			<th scope='col' id='username' class='manage-column column-username sortable'><a href="/Admin/Members?cp=1&orderby=email&order=<%=Model.View.EmailOrder + Model.View.SearchStrUrlPart %>"><span>E-mail</span><span class="sorting-indicator"></span></a></th>
			<th scope='col' id='name' class='manage-column column-name sortable'><a href="/Admin/Members?cp=1&orderby=name&order=<%=Model.View.NameOrder + Model.View.SearchStrUrlPart %>"><span>Name</span><span class="sorting-indicator"></span></a></th>
			<th scope='col' id='activated' class='manage-column column-activated'>Activated</th>
			<th scope='col' id='special_access' class='manage-column column-special-access'>Special Access</th>
			<th scope='col' id='last_access_date' class='manage-column column-last-access num'>Last Access Date</th>	
			<th scope='col' class='manage-column column-registred sortable'><a href="/Admin/Members?cp=1&orderby=createdate&order=<%=Model.View.CreateDateOrder + Model.View.SearchStrUrlPart %>"><span>Registred Date</span><span class="sorting-indicator"></span></a></th>	
		</tr>
		</thead>
		<tfoot>
		<tr>
			<th scope='col' class='manage-column column-email sortable'><a href="/Admin/Members?cp=1&orderby=email&order=<%=Model.View.EmailOrder + Model.View.SearchStrUrlPart  %>"><span>E-mail</span><span class="sorting-indicator"></span></a></th>
			<th scope='col' class='manage-column column-name sortable'><a href="/Admin/Members?cp=1&orderby=name&order=<%=Model.View.NameOrder + Model.View.SearchStrUrlPart  %>"><span>Name</span><span class="sorting-indicator"></span></a></th>
			<th scope='col' class='manage-column column-activated'>Activated</th>
			<th scope='col' class='manage-column column-special-access'>Special Access</th>
			<th scope='col' class='manage-column column-last-access'>Last Access Date</th>	
			<th scope='col' class='manage-column column-registred sortable'><a href="/Admin/Members?cp=1&orderby=createdate&order=<%=Model.View.CreateDateOrder + Model.View.SearchStrUrlPart %>"><span>Registred Date</span><span class="sorting-indicator"></span></a></th>	
		</tr>
		</tfoot>
		<tbody id="the-list" class='list:user'>
        <%if (Model.View.Users.Any())
          {
              foreach (var user in Model.View.Users)
              { %>	
		<tr id='user-<%= user.Id %>' class="<%= user.IsAlternative ? "" : "alternate" %>">
			<td class="email column-email">
				<strong><a href='mailto:<%= user.Email %>' title='E-mail: <%= user.Email %>'><%= user.Email%></a></strong>
				<br />
				<div class="row-actions">
					<span class='edit'><a href="/Admin/EditMember/<%= user.Id %>">Edit</a> | </span>
					<span class='delete'><a class='submitdelete' href='/Admin/DeleteMember/<%= user.Id %>' onclick="javascript:return confirm('Are you realy want to remove <%= user.Email %> user? The operation is not recoverable!');">Delete</a></span>
				</div>
			</td>
			<td class="name column-name"><%= user.Name%></td>
			<td class="role column-activated">
                <span><%= user.Activated ? "Yes" : "No"%></span>
                <%if (!user.Activated)
                  {%>
                <br />
				<div class="row-actions">
					<span class='activate'><a href="/Admin/Activate/<%= user.Id %>">Activate</a> | </span>
					<span class='resend-email'><a class='submitdelete' href='/Admin/ResendEmail/<%= user.Id %>'>Resend Email</a></span>
				</div>
                <%} %>
            </td>
			<td class="role column-special-access">
                <span><%= user.SpecialAccess ? "Yes" : "No"%></span>
                <%if (!user.SpecialAccess)
                  {%>
                <br />
				<div class="row-actions">
					<span class='special-access'><a href="/Admin/SpecialAccess/<%= user.Id %>">Grand</a></span>
				</div>
                <%} %>
            </td>
			<td class="role column-last-access"><%= user.LastAccess%></td>
			<td class="role column-registred"><%= user.Registred%></td>
		</tr>
        <%} 
          }
          else
          {%>
          <tr class="no-items"><td colspan="5" class="colspanchange">No matching members were found.</td></tr>
        <%} %>
		</tbody>
	</table>
	<div class="t-nav bottom">
		<div class='t-nav-pages <%= Model.View.IsOnePage ? "one-page" : "" %>'>
			<span class="d-num"><%= Model.View.Count %> items</span>
			<span class='pagination-links'>
				<a class='first-page <%= Model.View.IsOnePage ? "disabled" : "" %>' title='Go to the first page' href='/Admin/Members?cp=1<%=Model.View.UrlPart %>'>&laquo;</a>
				<a class='prev-page <%= Model.View.PreviousPage.HasValue ? "" : "disabled" %>' title='Go to the previous page' href='/Admin/Members?cp=<%= Model.View.PreviousPage.HasValue ? Model.View.PreviousPage.Value : 1 %><%=Model.View.UrlPart %>'>&lsaquo;</a>
				<span class="paging-input"><%= Model.View.CurPage %> of <span class='total-pages'><%= Model.View.TotalPages%></span></span>
				<a class='next-page <%= Model.View.NextPage.HasValue ? "" : "disabled" %>' title='Go to the next page' href='/Admin/Members?cp=<%= Model.View.NextPage.HasValue ? Model.View.NextPage.Value : Model.View.TotalPages %><%=Model.View.UrlPart %>'>&rsaquo;</a>
				<a class='last-page <%= Model.View.IsOnePage ? "disabled" : "" %>' title='Go to the last page' href='/Admin/Members?cp=<%= Model.View.TotalPages%><%=Model.View.UrlPart %>'>&raquo;</a>
			</span>
		</div>
		<br class="clear" />
	</div>
</div><!-- /wrap -->
<br class="clear" />
</asp:Content>



