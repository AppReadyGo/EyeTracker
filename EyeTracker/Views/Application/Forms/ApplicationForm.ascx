<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<EyeTracker.Model.Pages.Application.ApplicationModel>" %>
<%:Html.HiddenFor(m => m.PortfolioId) %>
<%:Html.HiddenFor(m => m.Id) %>
<table>
    <caption>1. Application Configuration</caption>
    <tbody id="tbody">
        <tr><td class="label"><%: Html.LabelFor(m => m.Description)%></td><td><%: Html.TextBoxFor(m => m.Description, new { MaxLength = 100, @class = "name" })%><br /><%: Html.ValidationMessageFor(m => m.Description)%></td></tr>
        <tr>
            <td class="label">
                <%: Html.LabelFor(m => m.Type)%>
            </td>
            <td>
                <%if (ViewBag.Edit){ %>
                    <%: Model.ViewData.TypesList.Single(s => s.Value == ((int)Model.Type).ToString()).Text %>
                    <%: Html.HiddenFor(m => m.Type)%>
                <%} else { %>
                    <%: Html.DropDownListFor(m => m.Type, Model.ViewData.TypesList)%>
                <%} %>
            </td>
        </tr>
    </tbody>
</table>
<%if (!ViewBag.Edit){ %>
<p id="action_pnl"><a href="#" id="create_lnk" class="link2"><span><span>Create</span></span></a>&nbsp;<a class="link4" href="/Application/<%: Model.PortfolioId %>"><span><span>Cancel</span></span></a></p>
<%} %>
<div id="sample_code">
    <%if (!ViewBag.Edit || Model.Type == (int)EyeTracker.Common.Entities.ApplicationType.Web){ %>
    <div id="sample_web_code" class="step">
        <strong><%: ViewBag.Edit ? "2" : "3" %>. Paste this code on your site</strong>
        <div>Copy the following code, then paste it onto every page you want to track immediately before the closing &lt;/head&gt; tag.</div>
        <textarea readonly="readonly" id="web_code">
            <script type="text/javascript">
                var _mfyaq = _mfyaq || {};
                _mfyaq.key = '<%: Model.ViewData.PropertyId%>';

                (function () {
                    var mfyaq = document.createElement('script'); mfyaq.type = 'text/javascript'; mfyaq.async = true;
                    mfyaq.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www')
                    mfyaq.src += 'fingerprint.mobillify.com/analytics.js';
                    var scr = document.getElementsByTagName('script')[0]; scr.parentNode.insertBefore(mfyaq, scr);
                })();

            </script>
        </textarea>
    </div>
    <%} %>
    <%if (!ViewBag.Edit || Model.Type == (int)EyeTracker.Common.Entities.ApplicationType.Android) { %>
    <div id="sample_android_code" class="step">
        <strong><%: ViewBag.Edit ? "2" : "3" %>. Download package and insert into your code</strong>
        <div>Package: <a href="/Packages/fingerprint-<%: ViewBag.Version %>.jar">Android Package <%: ViewBag.Version %> <strong>Download</strong></a></div>
        <div>Properties: <a id="properties_lnk" href="/Properties/<%:(int)EyeTracker.Common.Entities.ApplicationType.Android %>/<%: Model.PortfolioId %>/<%: ViewBag.Edit ? Model.Id.ToString() : "{appId}" %>/fingerprint.properties">Android Properties <%: ViewBag.Version %> <strong>Download</strong></a></div>
        <div>Property ID: <strong class="property-id"><%= Model.ViewData.PropertyId%></strong></div>
    </div>
    <%} %>
    <p><a class="link2" onClick="$(this).closest('form').submit();"><span><span>Save</span></span></a>&nbsp;<a class="link4" href="/Application/<%: Model.PortfolioId %>"><span><span>Cancel</span></span></a></p>
</div>
<div id="overlay" class="overlay"></div>