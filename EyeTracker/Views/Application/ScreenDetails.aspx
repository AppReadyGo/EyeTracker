<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<EyeTracker.Model.ScreenDetailsModel>" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>ScreenDetails</title>
</head>
<body>
    <form method="post" action="/Application/ScreenDetails/" enctype="multipart/form-data">
    <fieldset>
        <legend>Screen details</legend>
        <div><%: Html.LabelFor(m => m.Width)%> <%: Html.TextBoxFor(m => m.Width)%></div>
        <div><%: Html.LabelFor(m => m.Height)%> <%: Html.TextBoxFor(m => m.Height)%></div>
        <div>File: <input type="file" /></div>
        <a>add</a>
    </fieldset>
    </form>
</body>
</html>
