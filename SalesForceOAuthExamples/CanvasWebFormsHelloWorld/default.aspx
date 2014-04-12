<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="CanvasWebFormsHelloWorld._default" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Index</title>
</head>
<body>
    <h2>Index</h2>
    <ul>
        <li><%=Greeting%></li>
        <li>Signed Request Status:
            <div>
            <%=SignedRequestStatus%>
            </div>
        </li>
    </ul>
</body>
</html>

