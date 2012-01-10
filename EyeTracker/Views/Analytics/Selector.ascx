<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<EyeTracker.Model.SelectorModel>" %>
<div class="selector">
    <a class="link"><%: Model.Title %></a>
    <div class="pop-up">
        <ul>
            <%foreach (var item in Model.Items)
              { %>
            <li><label itemindex="<%: item.Key %>"><input type="checkbox" value="<%: item.Key %>" /> <%: item.Value %></label></li>
            <%} %>
        </ul>
        <div class="action"><a class="cancel-btn">Cancel</a><a class="apply-btn">Apply</a></div>
    </div>
    <ul class="selected">
        <%foreach (var item in Model.SelectedItems)
          { %>
        <li><a itemid="<%: item.Key %>" itemindex="<%: item.Key %>" class="remove-btn">&nbsp;</a> <%: item.Value %></li>
        <%} %>
    </ul>
</div>
