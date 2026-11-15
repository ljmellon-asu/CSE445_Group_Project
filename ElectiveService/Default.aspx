<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ElectiveService.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .buttons {
            display: flex;
            justify-content: center;
            gap: 12px;
            margin: 16px 0;
        }

        .myTable {
            width: 100%;
            border-collapse: collapse;
        }

        .myTable th,
        .myTable td {
            border: 1px solid black;
            padding: 8px;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            
           <h1 style="text-align: center">Welcome to Group 31&#39;s Default page !</h1>
            <p style="text-align: center">This Application is a movie logging service, you enter the name and year the film you wish to log, and your score (not limited to just 1-5, so you have full control) and the application will store your review in a log. You can retreive this log or delete it completly. </p> 
            <div class ="buttons">
                 <asp:Button ID="MemberButton" runat="server" Text="Members Page" />
                 <asp:Button ID="StaffButton" runat="server" Text="Staff Page" />
            &nbsp;Total Site Visits: <asp:Label ID="lblCount" runat="server" />
            </div>
        </div>
    </form>
    <div>
        <table class="myTable">
            <thead>
                <tr>
                    <th colspan="5">Application and Components Summary Table</th>
                </tr>
                <tr>
                    <th colspan="5">Percentage of Overall Contribution : Lauren Mellon: -- Sushant Vissa: -- Kyle Kim --</th>
                </tr>
                <tr>
                    <th scope="col">Provider Name</th>
                    <th scope="col">Page and component type</th>
                    <th scope="col" class>Component Description</th>
                    <th scope="col">Resources used and where component is used</th>
                    <th scope="col">TryIt</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>Lauren Mellon</td>
                    <td>default aspx page</td>
                    <td class>The public Default Page that describes the application, links to other pages, and hosts the component summary table.</td>
                    <td>GUI design and C#/ HTML code behind GUI</td>
                    <td><a href="<%= ResolveUrl("~/Default.aspx") %>">TryIt</a></td>
                </tr>
                <tr>
                    <td>Lauren Mellon</td>
                    <td>Global.asax</td>
                    <td>Global event handler, handles application startup events, session
                        <br />
                        start events, and application error events. Reroutes user to an error page when an error occurs, and also tracks the total visits to the site which is displayed on the landing page. </td>
                    <td>C# code as script in Global.asax file, Uses json to store visitor state.</td>
                    <td><a href="<%= ResolveUrl("~/Global_asax_tryit.aspx") %>">TryIt</a></td>
                </tr>
                <tr>
                    <td>Lauren Mellon</td>
                    <td>Rest Service</td>
                    <td class>
                        <p>This service will take user input about what movie they watched, and the users rating and store it in JSON format.</p>
                        <p>Parameter Types: String (title) String(year) decimal (Rating)</p>
                        <p>Return Type: Task</p>
                    </td>
                    <td>
                        Uses the <a href="https://www.omdbapi.com/">OMDB api</a>, This component will be used on the member page.
                    </td>
                    <td><a href="<%= ResolveUrl("~/logTryIt.aspx") %>">TryIt</a></td>
                </tr>
                <tr>
                    <td>Kyle Kim</td>
                    <td>HashLib.dll (class library)</td>
                    <td class>
                        <p>Hashes a PW with SHA-256 and checks a typed PW against a stored hash.</p>
                        <p>Inputs: password string + a stored hash for the check.</p>
                        <p>Output: a lowercase hex hash string, or true/false for the check.</p>
                    </td>
                    <td>PasswordHasher.Hash(string) and PasswordHasher.Verify(string, string) using System.Security.Cryptography.SHA256. Used on Default.aspx and for member/staff login.</td>
                    <td></td>
                </tr>
                <tr>
                    <td>Kyle Kim</td>
                    <td>CaptchaControl.ascx and CaptchaImage.ashx (user control and handler)</td>
                    <td class>
                        <p>Shows a 5-character CAPTCHA image and checks what the user types.</p>
                        <p>Input: the typed text.</p>
                        <p>Output: true/false, shown as “Correct” or “Incorrect”.</p>
                    </td>
                    <td>The .ashx draws a 150x50 PNG with System.Drawing and stores the code in Session[&quot;captcha&quot;]. Verify() compares the typed text to the session value. Used on the login page.</td>
                    <td></td>
                </tr>
            </tbody>
        </table>
    </div>
</body>
</html>
