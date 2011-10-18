<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">Logs</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<iframe id="the_iframe" src="<%: Url.Action("Elmah") %>" width="100%" style="border:0;" onload="calcHeight();" scrolling="no"></iframe>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<script language="JavaScript">
<!--
    function calcHeight() {
        //find the height of the internal page
        var the_height = document.getElementById('the_iframe').contentWindow.document.body.scrollHeight;

        //change the height of the iframe
        document.getElementById('the_iframe').height = the_height;
    }
//-->
</script></asp:Content>


