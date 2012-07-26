<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<EyeTracker.Model.Pages.Application.ApplicationModel>" %>
<form id="edit_form" action="/Application/Edit/" method="post">
    <%:Html.HiddenFor(m => m.Id) %>
    <%:Html.HiddenFor(m => m.PortfolioId) %>
    <table>
        <caption>1. Application Configuration</caption>
        <tbody id="tbody">
            <tr><td class="label"><%: Html.LabelFor(m => m.Description)%></td><td><%: Html.TextBoxFor(m => m.Description, new { MaxLength = 50, @class = "name" })%><br /><%: Html.ValidationMessageFor(m => m.Description)%></td></tr>
            <tr><td class="label"><%: Html.LabelFor(m => m.Type)%></td><td><%: Html.DropDownListFor(m => m.Type, Model.ViewData.TypesList)%></td></tr>
        </tbody>
    </table>
    <p id="action_pnl"><a href="#" id="create_lnk" class="link2"><span><span>Create</span></span></a>&nbsp;<a class="link4" href="/Analytics"><span><span>Cancel</span></span></a></p>
    <div id="sample_code">
        <div id="sample_web_code" class="step">
            <strong>3. Paste this code on your site</strong>
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
        <div id="sample_android_code" class="step">
            <strong>3. Download package and insert into your code</strong>
            <div>Package: <a href="/Packages/Android_Package_1.0.1.jar">Android Package 1.0.1 <strong>Download</strong></a></div>
            <div>Property ID: <strong class="property-id"><%= Model.ViewData.PropertyId%></strong></div>
            <textarea readonly="readonly" id="android_code">
        import com.mobillify.fingerprint.FingerPrint;
        public class TestActivity extends Activity {

            //Activity and View Details
            private static final String appID = "<%= Model.ViewData.PropertyId%>";
            private static final String viewPath = "View Unique Name";

            .
            .
            .
         }
            </textarea>
        </div>
        <p><a class="link2" onClick="$(this).closest('form').submit();"><span><span>Save</span></span></a>&nbsp;<a class="link4" href="/Analytics"><span><span>Cancel</span></span></a></p>
    </div>
</form>
<div id="overlay" class="overlay"></div>