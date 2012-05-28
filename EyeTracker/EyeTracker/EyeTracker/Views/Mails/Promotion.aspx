<%@ Page Title="" Language="C#" 
MasterPageFile="~/Views/Shared/Mail.Master" 
Inherits="System.Web.Mvc.ViewPage<EyeTracker.Model.Mails.PromotionEmailModel>" %>

<asp:Content ID="PageTitleContent" ContentPlaceHolderID="PageTitleContent" runat="server">
<%:Model.Subject %>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<div marginwidth="0" marginheight="0" style="margin: 0; padding: 0; background-color: #ededed; width: 100%!important">
    <center>
        <%if (Model.IsEmailProcess)
          { %>
        <p style="color:#878787;font-family:Arial;font-size:11px;line-height:157%;padding:15px 0 5px 0;margin:0">
            The email is not displayed properly? <a href="<%:Model.ThisEmailUrl %>" style="color:#878787;font-weight:normal;text-decoration:underline" target="_blank">View the online version</a>.
            Annoyed? <a href="<%:Model.UnsubscribeUrl %>" style="color:#878787;font-weight:normal;text-decoration:underline" target="_blank">Unsubscribe</a> at any time.
        </p>
        <%} %>
        <table border="0" cellpadding="0" cellspacing="20" height="100%" width="100%" style="margin:0;padding:0;background-color:#ededed;min-height:100%!important;width:100%!important">
            <tbody><tr>
                <td align="middle" valign="top" style="border-collapse:collapse">                
                    <table border="0" cellpadding="0" cellspacing="0" width="550">
                        <tbody><tr>
                            <td valign="middle" style="border-collapse:collapse">
                                <table border="0" cellpadding="40" cellspacing="0" width="100%">
                                    <tbody><tr>
                                        <td align="left" style="border-collapse:collapse">
                                            <div style="color:#505050;font-family:Arial;font-size:10px;line-height:100%;text-align:middle">
                                            <a href="<%:Model.SiteRootUrl %>" style="color:#336699;font-weight:normal;text-decoration:underline" target="_blank"><img src="<%:Model.SiteRootUrl %>/Content/images/logo_60.png" width="392" height="60" align="" border="0" title="Fingerprint" alt="Fingerprint" style="border:0;min-height:auto;line-height:100%;outline:none;text-decoration:none;display:inline;margin-bottom:0px"></a>
                                            </div>
                                        </td>
                                    </tr>
                                </tbody></table>
                            </td>
                        </tr>
                    </tbody></table>
                    <table border="0" cellpadding="0" cellspacing="0" width="550">
                        <tbody><tr>
                            <td align="center" valign="top" style="border-collapse:collapse">
                                    
                                <table border="0" cellpadding="0" cellspacing="0" width="500">
                                    <tbody><tr>
                                        <td valign="top" style="border-collapse:collapse;background-color:#ffffff">

                                                
                                            <table border="0" cellpadding="20" cellspacing="0" width="100%">
                                                <tbody><tr>
                                                    <td valign="top" style="border-collapse:collapse">
                                                        <div style="color:#505050;font-family:Arial;font-size:14px;line-height:157%;text-align:left">
<%=Model.Body %>
                                                        </div>
                                                                                                        </td>
                                                </tr>
                                            </tbody></table>
                                                

                                        </td>
                                    </tr>
                                </tbody></table>
                                    
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="top" style="border-collapse:collapse">
                                    
                                <table border="0" cellpadding="10" cellspacing="0" width="520">
                                    <tbody><tr>
                                        <td valign="top" style="border-collapse:collapse">

                                                
                                            <table border="0" cellpadding="10" cellspacing="0" width="100%">
                                                <tbody><tr>
                                                    <td colspan="2" valign="middle" style="border-collapse:collapse;background-color:#ffffff;border:0">
                                                        <div style="color:#707070;font-family:Arial;font-size:11px;line-height:150%;text-align:left">

                                                            <p>Join our fantastic community: <a href="http://twitter.com/mobillify" style="color:#707070;font-weight:normal;text-decoration:underline" target="_blank">follow us on Twitter</a> and <a href="http://www.facebook.com/mobillify" style="color:#707070;font-weight:normal;text-decoration:underline" target="_blank">join us on Facebook</a></p>

<p>Contact us at: <%:Model.ContactUsEmail %>.</p>
        <%if (Model.IsEmailProcess)
          { %>
    <p><a href="<%:Model.UnsubscribeUrl %>" style="color:#707070;font-weight:normal;text-decoration:underline" target="_blank">unsubscribe</a> | <a href="<%:Model.ThisEmailUrl %>" style="color:#707070;font-weight:normal;text-decoration:underline" target="_blank">view it in your browser</a></p>
<%} %>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </tbody></table>
                                                

                                        </td>
                                    </tr>
                                </tbody></table>
                                    
                            </td>
                        </tr>
                    </tbody></table>
                    <br>
                </td>
            </tr>
        </tbody></table>
    </center>
    <%if (Model.IsEmailProcess)
    { %>
    <!-- Traking image -->
    <img src="" height="1" width="1">
    <%} %>
</div>
</asp:Content>