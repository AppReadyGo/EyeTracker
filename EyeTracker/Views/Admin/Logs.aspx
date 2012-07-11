<%@ Import Namespace="EyeTracker.Model.Pages.Admin" %>
<%@ Page Title="" Language="C#" 
MasterPageFile="~/Views/Admin/Admin.Master" 
Inherits="ViewPage<ViewModelWrapper<AfterLoginMasterModel, AdminMasterModel, LogsModel>>" %>

<asp:Content ContentPlaceHolderID="PageTitleContent" runat="server">Logs</asp:Content>

<asp:Content ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="<%: Url.Content("~/Content/admin.logs.css") %>" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<% Html.RenderPartial("Forms/LogsFilter", Model.View); %>
<textarea runat="server" ID="output" enableviewstate="false" wrap="off" readonly style="width:100%;height:600px;">
<%= ViewBag.Logs%>
</textarea>
</asp:Content>



