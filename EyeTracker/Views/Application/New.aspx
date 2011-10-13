<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TopMenu.Master" Inherits="System.Web.Mvc.ViewPage<EyeTracker.Model.ApplicationModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">New Application</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="../../Content/ApplicationNew.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/ApplicationNew.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Title" runat="server">New Application</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="TopMenu" runat="server">
    <%:Html.ActionLink("All Application", "") %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="app-config" class="step">
    <h3>1. Application Configuration</h3>
    <div><%: Html.LabelFor(m => m.Description)%> <%: Html.TextBoxFor(m => m.Description)%></div>
    <div><%: Html.ValidationMessageFor(m => m.Description)%></div>
    <p><a id="create_lnk">Create</a></p>
</div>
<div id="sample_code" class="step">
    <h3>2. Download package and insert into your code</h3>
    <div>Package: <a href="<%: ViewBag.PackageLink %>">Android Package 1.0.1</a></div>
    <div>Property ID: <strong><%: ViewBag.PropertyId %></strong></div>
    <textarea><%: ViewBag.CodeSample %></textarea>
    <p><a id="save_lnk">Save</a></p>
</div>
<div id="overlay" class="overlay"></div>
</asp:Content>


