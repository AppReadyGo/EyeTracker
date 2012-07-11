<%@ Import Namespace="EyeTracker.Model.Pages.Admin" %>
<%@ Page Title="" Language="C#" 
MasterPageFile="~/Views/Admin/Admin.Master" 
Inherits="ViewPage<ViewModelWrapper<AfterLoginMasterModel, AdminMasterModel, LogsModel>>" %>

<asp:Content ContentPlaceHolderID="PageTitleContent" runat="server">Members</asp:Content>

<asp:Content ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<style>
select{padding:2px;height:2em}
br.clear { height: 2px; line-height: 2px;}
.clear{clear:both;}

.wrap{margin:4px 15px 0 0}
.a-l{float:left;}
.a-r{float:right;}
.t-nav{height:30px;margin:6px 0 4px;vertical-align:middle}
.t-nav .t-nav-pages{float:right;display:block;cursor:default;height:30px;line-height:30px;font-size:12px}
.t-nav .t-nav-pages a{ border-color: #E3E3E3; }
.t-nav .t-nav-pages a,.t-nav-pages span.current{text-decoration:none;padding:3px 6px}
.t-nav .t-nav-pages a.disabled:hover,.t-nav .t-nav-pages a.disabled:active{cursor:default}
.t-nav .t-nav-pages a,.t-nav-pages span.current{-webkit-border-radius:3px;border-radius:3px;border-width:1px;border-style:solid}
.t-nav .t-nav-pages .cur-p { text-align: center; }
.t-nav .d-num{margin-right:10px;font-size:12px;font-style:italic;font-family:Georgia,"Times New Roman","Bitstream Charter",Times,serif}
.t-nav .no-pages,.t-nav .one-page .pagination-links{display:none}

table.fixed{table-layout:fixed}

.widefat{background-color: #F9F9F9;border-color: #DFDFDF;-webkit-border-radius:3px;border-radius:3px;border-width:1px;border-style:solid;border-spacing:0;width:100%;margin:0}
.widefat *{word-wrap:break-word}
.widefat a{text-decoration:none}
.widefat thead th:first-of-type{-webkit-border-top-left-radius:3px;border-top-left-radius:3px}
.widefat thead th:last-of-type{-webkit-border-top-right-radius:3px;border-top-right-radius:3px}
.widefat tfoot th:first-of-type{-webkit-border-bottom-left-radius:3px;border-bottom-left-radius:3px}
.widefat tfoot th:last-of-type{-webkit-border-bottom-right-radius:3px;border-bottom-right-radius:3px}
.widefat td,.widefat th{border-width:1px 0;border-style:solid}.widefat tfoot th{border-bottom:0}
.widefat .no-items td{border-bottom-width:0}
.widefat td{font-size:12px;padding:4px 7px 2px;vertical-align:top}
.widefat td p,.widefat td ol,.widefat td ul{font-size:12px}
.widefat th{padding:7px 7px 8px;text-align:left;line-height:1.3em;font-size:14px}
.widefat th input{margin:0 0 0 8px;padding:0;vertical-align:text-top}
.widefat .check-column{width:2.2em;padding:11px 0 0;vertical-align:top}
.widefat tbody th.check-column{padding:9px 0 12px}
.widefat .num,.column-comments,.column-links,.column-posts{text-align:center}
.widefat th#comments{vertical-align:middle}
.widefat th,.search{font-family:Georgia,"Times New Roman","Bitstream Charter",Times,serif}
.widefat th,.widefat td{overflow:hidden}
.widefat th{font-weight:normal}
.widefat td p{margin:2px 0 .8em}
.widefat .column-comment p{margin:.6em 0}
.widefat th.sortable,.widefat th.sorted{padding:0}
.widefat td,.widefat th{border-top-color:#fff;border-bottom-color:#dfdfdf}
.widefat th{text-shadow:rgba(255,255,255,0.8) 0 1px 0}
.widefat td{color:#555}
.widefat p,.widefat ol,.widefat ul{color:#333}
.widefat thead tr th,.widefat tfoot tr th{color:#333}
.widefat thead tr th,.widefat tfoot tr th{background-color:#f1f1f1;background-image:-ms-linear-gradient(top,#f9f9f9,#ececec);background-image:-moz-linear-gradient(top,#f9f9f9,#ececec);background-image:-o-linear-gradient(top,#f9f9f9,#ececec);background-image:-webkit-gradient(linear,left top,left bottom,from(#f9f9f9),to(#ececec));background-image:-webkit-linear-gradient(top,#f9f9f9,#ececec);background-image:linear-gradient(top,#f9f9f9,#ececec)}
table.widefat span.delete a,table.widefat span.trash a,table.widefat span.spam a{color:#bc0b0b}
.row-actions{visibility:hidden;padding:2px 0 0}
tr:hover .row-actions{visibility:visible}
.fixed .column-role{width:15%}

th.sortable a,th.sorted a{display:block;overflow:hidden;padding:7px 7px 8px}
th.sortable a span,th.sorted a span{float:left;cursor:pointer}
.alternate,.alt{background-color:#fcfcfc}

textarea, input[type="text"], input[type="password"], input[type="file"], input[type="button"], input[type="submit"], input[type="reset"], input[type="email"], input[type="number"], input[type="search"], input[type="tel"], input[type="url"], select {
  background-color: #FFFFFF;
  border-color: #DFDFDF;
  color: #333333;
}
.button, .button-secondary, .submit input, input[type="button"], input[type="submit"] {
  border-color: #BBBBBB;
  color: #464646;
}
input,select{outline:0;line-height:15px;margin:1px;padding:3px;}
select{-webkit-border-radius:3px;border-radius:3px;border-width:1px;border-style:solid}
input[type="text"],input[type="password"],input[type="number"],input[type="search"],input[type="email"],input[type="url"],textarea
{-moz-box-sizing:border-box;-webkit-box-sizing:border-box;-ms-box-sizing:border-box;box-sizing:border-box}
input[type="checkbox"],input[type="radio"]{vertical-align:text-top;padding:0;margin:1px 0 0}
input[type="search"]{-webkit-appearance:textfield}
input[type="search"]::-webkit-search-decoration{display:none}

textarea,input[type="text"],input[type="password"],input[type="file"],input[type="button"],input[type="submit"],input[type="reset"],input[type="email"],input[type="number"],input[type="search"],input[type="tel"],input[type="url"],select{-webkit-border-radius:3px;border-radius:3px;border-width:1px;border-style:solid}
</style>
<div class="wrap">
	<h2>Members</h2>
	<form action="" method="get">
	<div class="t-nav">
        <div class="a-l">
            <a href="user-new.php" class="add-new-h2">+ Add New</a>
        </div>
		<div class="a-l search-box">
			<input type="search" id="user-search-input" name="s" value="" />
			<input type="submit" name="" id="search-submit" class="button" value="Search Users"  />
		</div>
		<div class='t-nav-pages one-page'>
			<span class="d-num">2 items</span>
			<span class='pagination-links'>
				<a class='first-page disabled' title='Go to the first page' href='http://www.panshin.me/wp-admin/users.php'>&laquo;</a>
				<a class='prev-page disabled' title='Go to the previous page' href='http://www.panshin.me/wp-admin/users.php?paged=1'>&lsaquo;</a>
				<span class="paging-input">
					<input class='cur-p' title='Current page' type='text' name='paged' value='1' size='1' /> of <span class='total-pages'>1</span>
				</span>
				<a class='next-page disabled' title='Go to the next page' href='http://www.panshin.me/wp-admin/users.php?paged=1'>&rsaquo;</a>
				<a class='last-page disabled' title='Go to the last page' href='http://www.panshin.me/wp-admin/users.php?paged=1'>&raquo;</a>
			</span>
		</div>
	</div><!-- /table-head -->
	<table class="widefat fixed" cellspacing="0">
		<thead>
		<tr>
			<th scope='col' id='username' class='manage-column column-username sortable desc'  style=""><a href="http://www.panshin.me/wp-admin/users.php?orderby=login&#038;order=asc"><span>E-mail</span><span class="sorting-indicator"></span></a></th>
			<th scope='col' id='name' class='manage-column column-name sortable desc'  style=""><a href="http://www.panshin.me/wp-admin/users.php?orderby=name&#038;order=asc"><span>Name</span><span class="sorting-indicator"></span></a></th>
			<th scope='col' id='role' class='manage-column column-role'  style="">Role</th>
			<th scope='col' id='activated' class='manage-column column-activated'  style="">Activated</th>
			<th scope='col' id='posts' class='manage-column column-posts num'  style="">Posts</th>	
		</tr>
		</thead>
		<tfoot>
		<tr>
			<th scope='col'  class='manage-column column-email sortable desc'  style=""><a href="http://www.panshin.me/wp-admin/users.php?orderby=login&#038;order=asc"><span>E-mail</span><span class="sorting-indicator"></span></a></th>
			<th scope='col'  class='manage-column column-name sortable desc'  style=""><a href="http://www.panshin.me/wp-admin/users.php?orderby=name&#038;order=asc"><span>Name</span><span class="sorting-indicator"></span></a></th>
			<th scope='col'  class='manage-column column-role'  style="">Role</th>
			<th scope='col' id='activated' class='manage-column column-activated'  style="">Activated</th>
			<th scope='col'  class='manage-column column-posts num'  style="">Posts</th>	
		</tr>
		</tfoot>
		<tbody id="the-list" class='list:user'>	
		<tr id='user-2' class="alternate">
			<td class="email column-email">
				<strong><a href='mailto:victoria@curlydesigner.com' title='E-mail: victoria@curlydesigner.com'>victoria@curlydesigner.com</a></strong>
				<br />
				<div class="row-actions">
					<span class='edit'><a href="user-edit.php?user_id=2&#038;wp_http_referer=%2Fwp-admin%2Fusers.php">Edit</a> | </span>
					<span class='delete'><a class='submitdelete' href='users.php?action=delete&amp;user=2&amp;_wpnonce=5bab032e7f'>Delete</a></span>
				</div>
			</td>
			<td class="name column-name">Victoria Panshin</td>
			<td class="role column-role">Editor</td>
			<td class="role column-activated">Yes</td>
			<td class="posts column-posts num"><a href='edit.php?author=2' title='View posts by this author' class='edit'>4</a></td>
		</tr>
		<tr id='user-1'>
			<td class="email column-email">
				<strong><a href='mailto:yuri@panshinspace.com' title='E-mail: yuri@panshinspace.com'>yuri@panshinspace.com</a></strong>
				<br />
				<div class="row-actions">
					<span class='edit'><a href="profile.php">Edit</a></span>
				</div>
			</td>
			<td class="name column-name">Yuri Panshin</td>
			<td class="role column-role">Administrator</td>
			<td class="role column-activated">
				<span>No</span>
				<br />
				<div class="row-actions">
					<span class='edit'><a href="profile.php">Resend Email</a></span>
				</div>
			</td>
			<td class="posts column-posts num"><a href='edit.php?author=1' title='View posts by this author' class='edit'>89</a></td>
		</tr>
		</tbody>
	</table>
	<div class="t-nav bottom">
		<div class='t-nav-pages one-page'>
			<span class="d-num">2 items</span>
			<span class='pagination-links'>
				<a class='first-page disabled' title='Go to the first page' href='http://www.panshin.me/wp-admin/users.php'>&laquo;</a>
				<a class='prev-page disabled' title='Go to the previous page' href='http://www.panshin.me/wp-admin/users.php?paged=1'>&lsaquo;</a>
				<span class="paging-input">1 of <span class='total-pages'>1</span></span>
				<a class='next-page disabled' title='Go to the next page' href='http://www.panshin.me/wp-admin/users.php?paged=1'>&rsaquo;</a>
				<a class='last-page disabled' title='Go to the last page' href='http://www.panshin.me/wp-admin/users.php?paged=1'>&raquo;</a>
			</span>
		</div>
		<br class="clear" />
	</div>
	</form>
</div><!-- /wrap -->
<br class="clear" />
</asp:Content>



