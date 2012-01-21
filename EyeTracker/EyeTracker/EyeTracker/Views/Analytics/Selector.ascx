<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<EyeTracker.Model.SelectorModel>" %>
<div class="selector">
    <a class="link"><%: Model.Title %></a>
    <div class="pop-up">
        <ul>
            <%foreach (var item in Model.Items)
              { %>
            <li><label itemindex="<%: item.Index %>"><input type="checkbox" value="<%: item.Id %>" /> <%: item.Text %></label></li>
            <%} %>
        </ul>
        <div class="action"><a class="cancel-btn">Cancel</a><a class="apply-btn">Apply</a></div>
    </div>
    <ul class="selected">
        <%foreach (var item in Model.SelectedItems)
          { %>
            <li><a itemid="<%: item.Id %>" itemindex="<%: item.Index %>" class="remove-btn">&nbsp;</a> <%: item.Text %></li>
        <%} %>
    </ul>
</div>
