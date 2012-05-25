<%@ Control Language="C#" Inherits="ViewUserControl<EyeTracker.Models.Account.UnsubscribeModel>" %>
<% using (Html.BeginForm()) { %>
    <h3>Unsubscribe</h3>
    <p>
        Use the form below to unsubscribe your email from all promotions. 
    </p>
                                
    <div class="editor-label">
        <%: Html.LabelFor(m => m.Email) %>
    </div>
    <div class="editor-field">
        <%: Html.TextBoxFor(m => m.Email, new { autocomplete="off" })%>
        <%: Html.ValidationMessageFor(m => m.Email) %>
    </div>
    <%: Html.ValidationSummary(true) %>
    <p>
        <input type="submit" value="Unsubscribe" />
    </p>
<% } %>