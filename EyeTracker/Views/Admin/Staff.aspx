<%@ Import Namespace="EyeTracker.Model.Pages.Admin" %>
<%@ Page Title="" Language="C#" 
MasterPageFile="~/Views/Admin/Admin.Master" 
Inherits="ViewPage<ViewModelWrapper<AfterLoginMasterModel, AdminMasterModel, EyeTracker.Model.PagingModel>>" %>

<asp:Content ContentPlaceHolderID="PageTitleContent" runat="server">Staffs</asp:Content>

<asp:Content ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="<%: Url.Content("~/Content/shared/table.css") %>" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="wrap">
	<h2>Staff</h2>
	<form action="" method="get">
	<div class="t-nav">
        <div class="a-l">
            <a href="user-new.php" class="add-new-h2">+ Add New</a>
        </div>
		<div class="a-l search-box">
			<input type="search" id="user-search-input" name="s" value="" />
			<input type="submit" name="" id="search-submit" class="button" value="Search Users"  />
		</div>
		<div class='t-nav-pages <%= Model.View.IsOnePage ? "one-page" : "" %>'>
			<span class="d-num"><%= Model.View.Count %> items</span>
			<span class='pagination-links'>
				<a class='first-page <%= Model.View.IsOnePage ? "disabled" : "" %>' title='Go to the first page' href='/Admin/Staff?cp=1'>&laquo;</a>
				<a class='prev-page <%= Model.View.PreviousPage.HasValue ? "" : "disabled" %>' title='Go to the previous page' href='/Admin/Staff?cp=<%= Model.View.PreviousPage.HasValue ? Model.View.PreviousPage.Value : 1 %>'>&lsaquo;</a>
				<span class="paging-input">
					<input class='cur-p' title='Current page' type='text' name='paged' value='<%= Model.View.CurPage %>' size='1' /> of <span class='total-pages'><%= Model.View.TotalPages%></span>
				</span>
				<a class='next-page <%= Model.View.NextPage.HasValue ? "" : "disabled" %>' title='Go to the next page' href='/Admin/Staff?cp=<%= Model.View.NextPage.HasValue ? Model.View.NextPage.Value : Model.View.TotalPages %>'>&rsaquo;</a>
				<a class='last-page <%= Model.View.IsOnePage ? "disabled" : "" %>' title='Go to the last page' href='/Admin/Staff?cp=<%= Model.View.TotalPages%>'>&raquo;</a>
			</span>
		</div>
	</div><!-- /table-head -->
	<table class="tbl fixed" cellspacing="0">
		<thead>
		<tr>
			<th scope='col' id='username' class='manage-column column-username sortable desc'  style=""><a href="http://www.panshin.me/wp-admin/users.php?orderby=login&#038;order=asc"><span>E-mail</span><span class="sorting-indicator"></span></a></th>
			<th scope='col' id='name' class='manage-column column-name sortable desc'  style=""><a href="http://www.panshin.me/wp-admin/users.php?orderby=name&#038;order=asc"><span>Name</span><span class="sorting-indicator"></span></a></th>
			<th scope='col' id='role' class='manage-column column-role'  style="">Role</th>
			<th scope='col' id='activated' class='manage-column column-activated'  style="">Activated</th>
			<th scope='col' id='last_access_date' class='manage-column column-last-access num'  style="">Last Access Date</th>	
		</tr>
		</thead>
		<tfoot>
		<tr>
			<th scope='col'  class='manage-column column-email sortable desc'  style=""><a href="http://www.panshin.me/wp-admin/users.php?orderby=login&#038;order=asc"><span>E-mail</span><span class="sorting-indicator"></span></a></th>
			<th scope='col'  class='manage-column column-name sortable desc'  style=""><a href="http://www.panshin.me/wp-admin/users.php?orderby=name&#038;order=asc"><span>Name</span><span class="sorting-indicator"></span></a></th>
			<th scope='col'  class='manage-column column-role'  style="">Role</th>
			<th scope='col' id='activated' class='manage-column column-activated'  style="">Activated</th>
			<th scope='col' id='Th1' class='manage-column column-last-access num'  style="">Last Access Date</th>	
		</tr>
		</tfoot>
		<tbody id="the-list" class='list:user'>
        <%foreach (var user in Model.View.Users)
        { %>	
		<tr id='user-<%= user.Id %>' class="<%= user.IsAlternative ? "" : "alternate" %>">
			<td class="email column-email">
				<strong><a href='mailto:<%= user.Email %>' title='E-mail: <%= user.Email %>'><%= user.Email %></a></strong>
				<br />
				<div class="row-actions">
					<span class='edit'><a href="user-edit.php?user_id=2&#038;wp_http_referer=%2Fwp-admin%2Fusers.php">Edit</a> | </span>
					<span class='delete'><a class='submitdelete' href='users.php?action=delete&amp;user=2&amp;_wpnonce=5bab032e7f'>Delete</a></span>
				</div>
			</td>
			<td class="name column-name"><%= user.Name %></td>
			<td class="role column-role"><%= user.Roles %></td>
			<td class="role column-activated">
                <span><%= user.Activated ? "Yes" : "No" %></span>
                <%if (!user.Activated)
                {%>
                <br />
				<div class="row-actions">
					<span class='activate'><a href="user-edit.php?user_id=2&#038;wp_http_referer=%2Fwp-admin%2Fusers.php">Activate</a> | </span>
					<span class='resend-email'><a class='submitdelete' href='users.php?action=delete&amp;user=2&amp;_wpnonce=5bab032e7f'>Resend Email</a></span>
				</div>
                <%} %>
            </td>
			<td class="role column-last-access"><%= user.LastAccess %></td>
		</tr>
        <%} %>
		</tbody>
	</table>
	<div class="t-nav bottom">
		<div class='t-nav-pages <%= Model.View.IsOnePage ? "one-page" : "" %>'>
			<span class="d-num"><%= Model.View.Count %> items</span>
			<span class='pagination-links'>
				<a class='first-page <%= Model.View.IsOnePage ? "disabled" : "" %>' title='Go to the first page' href='/Admin/Staff?cp=1'>&laquo;</a>
				<a class='prev-page <%= Model.View.PreviousPage.HasValue ? "" : "disabled" %>' title='Go to the previous page' href='/Admin/Staff?cp=<%= Model.View.PreviousPage.HasValue ? Model.View.PreviousPage.Value : 1 %>'>&lsaquo;</a>
				<span class="paging-input"><%= Model.View.CurPage %> of <span class='total-pages'><%= Model.View.TotalPages%></span></span>
				<a class='next-page <%= Model.View.NextPage.HasValue ? "" : "disabled" %>' title='Go to the next page' href='/Admin/Staff?cp=<%= Model.View.NextPage.HasValue ? Model.View.NextPage.Value : Model.View.TotalPages %>'>&rsaquo;</a>
				<a class='last-page <%= Model.View.IsOnePage ? "disabled" : "" %>' title='Go to the last page' href='/Admin/Staff?cp=<%= Model.View.TotalPages%>'>&raquo;</a>
			</span>
		</div>
		<br class="clear" />
	</div>
	</form>
</div><!-- /wrap -->
<br class="clear" />
</asp:Content>



