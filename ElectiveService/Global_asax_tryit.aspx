<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Global_asax_tryit.aspx.cs" Inherits="ElectiveService.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Global.asax TryIt Page</h2>

             <p> The Global.asax file handles 3 main functions:  </p>
                 <ul>
                    <li> On application start the total visitor count is read from the json where it is stored and sets it to an application variable to be accessed.</li>
                     <li>On session start the json is incremented by one and the application variable is updated. </li>
                     <li>On application error the user is routed to an error page.</li>
                 </ul> 
           


            <p>Total site visits: <asp:Label ID="lblCount" runat="server" /> </p>
            <a href="<%= ResolveUrl("~/doesnotexist.aspx") %>">A link to a page that does not exist</a>
        </div>
    </form>
</body>
</html>
