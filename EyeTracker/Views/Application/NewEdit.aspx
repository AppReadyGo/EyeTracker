<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TopMenu.Master" Inherits="System.Web.Mvc.ViewPage<EyeTracker.Model.ApplicationModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">New Application</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="<%: Url.Content("~/Content/ApplicationNewEdit.css")%>" rel="stylesheet" type="text/css" />
    <script src="<%: Url.Content("~/Scripts/ApplicationNewEdit.js")%>" type="text/javascript"></script>
    <script type="text/javascript">
        var newAppURL = '/Application/New/<%: ViewBag.PortfolioId %>/';
        var appId = <%: Model.Id %>;
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Title" runat="server">New Application</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="TopMenu" runat="server">
    <a href="/Application/<%= ViewBag.PortfolioId %>">All Application</a>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<form id="application_form" method="post" action="/Application/Edit/<%= ViewBag.PortfolioId %>">
<%: Html.HiddenFor(m => m.Id)%>
<div id="app_config" class="step">
    <h3>1. Application Configuration</h3>
    <div><%: Html.LabelFor(m => m.Description)%> <%: Html.TextBoxFor(m => m.Description)%></div>
    <div id="description_error"><%: Html.ValidationMessageFor(m => m.Description)%></div>
    <div><%: Html.LabelFor(m => m.Type)%> <%: Html.DropDownListFor(m => m.Type, (IEnumerable<SelectListItem>)ViewData["TypesList"])%></div>
    <p><a id="create_lnk">Create</a></p>
</div>
<div id="screens" class="step">
    <h3>2. Screenshots upload</h3>
    <div><img /> 500 X 600 <a>change</a></div>
    <div>New: <input /> X <input /> <input type="file" /><a >add</a></div>
</div>
<div id="sample_code" class="step">
    <h3>3. Download package and insert into your code</h3>
    <div>Package: <a href="<%: ViewBag.PackageLink %>">Android Package 1.0.1</a></div>
    <div>Property ID: <strong id="property"><%: ViewBag.PropertyId%></strong></div>
    <textarea id="code"><%: ViewBag.CodeSample%></textarea>
    <p><a id="done_lnk">Save</a></p>
</div>
</form>
<div id="overlay" class="overlay"></div>
</asp:Content>


