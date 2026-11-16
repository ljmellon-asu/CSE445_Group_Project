
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cookieStemTryIt.aspx.cs" Inherits="ElectiveService.cookieStemTryIt" %>

<%@ Register Src="~/UserControl/MoviePreferences.ascx" TagPrefix="uc" TagName="MoviePreferences" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Movie Logging Application</title>
</head>
<body>
    <form id="form1" runat="server">

    <h1>Movie Logging Application</h1>

<p>This application allows users to log movies, save movie preferences, and analyze movie information.</p>

 <p>To test the application, enter a username, choose a movie genre from the dropdown and click save profile, then read cookie, refresh the page and click read cookie again to ensure cookie profile is saved .</p>

        <!-- Nav link -->
        <p><a href="<%= ResolveUrl("~/Default.aspx") %>">Back to the Default page</a></p>

        <hr />
        <!-- User Control and Cookies -->
        <h2 id="preferences">Movie Profile</h2>
        <uc:MoviePreferences ID="MoviePreferences1" runat="server" />
        <hr />

        <!-- REST Stemming Service -->
        <h2 id="stemming">Stemming REST Service</h2>

        <p> Enter text from a movie review to convert the words into their root forms. </p>

        <p>Input Text:</p>

        <asp:TextBox ID="txtStemInput" runat="server" TextMode="MultiLine" Rows="4" Width="500px" />

        <br /><br />

        <asp:Button ID="btnStem" runat="server" Text="Stem Inputted Words" OnClick="btnStem_Click" />

        <p>Stemmed Text:</p>

        <asp:TextBox ID="txtStemOutput" runat="server" TextMode="MultiLine" Rows="4" Width="500px" ReadOnly="true" />

        <br /><br />

        <asp:Label ID="lblStatus" runat="server" />

        <hr />
    </form>
</body>
</html>
