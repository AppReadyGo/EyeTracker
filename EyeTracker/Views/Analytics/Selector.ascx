<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<EyeTracker.Model.SelectorModel>" %>
<div class="">
    <a class="select" id="app_lnk"><%: Model.Title %></a>
    <ul id="app_list" class="pop_up">
        <%foreach (var item in Model.Items)
          { %>
        <li><label><input type="checkbox" value="<%: item.Key %>" /> <%: item.Value %></label></li>
        <%} %>
    </ul>
    <ul id="sel_app_list">
        <%foreach (var item in Model.SelectedItems)
          { %>
        <li><a itemid="<%: item.Key %>">X</a> <%: item.Value %></li>
        <%} %>
    </ul>
</div>
