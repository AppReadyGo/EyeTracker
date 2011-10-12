<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TopMenu.Master" Inherits="System.Web.Mvc.ViewPage<EyeTracker.Model.ApplicationModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">New Application</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Title" runat="server">New Application</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="TopMenu" runat="server">
    <%:Html.ActionLink("All Application", "") %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>Application</h2>

<% using (Html.BeginForm()) {%>
    <%: Html.ValidationSummary(true, "Application creation was unsuccessful. Please correct the errors and try again.")%>
    <%: Html.LabelFor(m => m.Description)%>
    <div>
        <fieldset>
            <legend>Application Information</legend>

            <div class="editor-label">
                <%: Html.LabelFor(m => m.Description)%>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(m => m.Description)%>
                <%: Html.ValidationMessageFor(m => m.Description)%>
            </div>
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>
    </div>
<%}%>

</asp:Content>


