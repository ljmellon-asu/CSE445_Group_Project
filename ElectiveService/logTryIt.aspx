<%@ Page Language="C#" AutoEventWireup="true" Async="true" CodeBehind="logTryIt.aspx.cs" Inherits="ElectiveService.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                <h2> Welcome to the Movie Log Service Try it Page!</h2>
                <p>
                    This service will take user input about what movie they watched, and the users rating and store it in JSON format.
                </p>
            </div>
            <hr />
            <div>
                 URL : <a href ="https://localhost:44344/Help/Api/POST-api-Log"> https://localhost:44344/Help/Api/POST-api-Log </a></div>
            <hr />
            <div>
                Method Name: LogMovie<br />
                Parameter Types: String (title) String(year) decimal (Rating)<br />
                Return Type: Task</div>
            <hr />
        </div>
        <p>
            Movie Title:<asp:TextBox ID="TitleTbox" runat="server"></asp:TextBox>
            Release Year:
            <asp:TextBox ID="YearTbox" runat="server"></asp:TextBox>&nbsp;User Rating:
            <asp:TextBox ID="RatingTbox" runat="server"></asp:TextBox>&nbsp;<asp:Button ID="logButton" runat="server" OnClick="logButton_Click" Text="Log Movie!" />
        </p>
        <p>
            &nbsp;</p>
        <hr />
        <div>
        </div>
        <asp:Button ID="showLogButton" runat="server" OnClick="showLogButton_Click" Text="Show My Log" />

        <asp:Label ID="EmptyLogMessage" runat="server" Text="Your log is empty." Visible="false" />
        <asp:Label ID="StatusLabel" runat="server" Text="no current status." Visible="false" />

        <asp:Button ID="ClearButton" runat="server" OnClick="ClearButton_Click" Text="Clear Log" />

        <asp:Repeater ID="LogRepeater" runat="server">
            <ItemTemplate>
                <div>
                    <h3><%#: Eval("Title") %></h3>
                    <p>
                        Release date: <%#: Eval("ReleaseDate") %><br />
                        Runtime: <%#: Eval("Runtime") %><br />
                        Your rating: <%#: Eval("UserRating") %>
                    </p>
                    <asp:Image runat="server"
                        ImageUrl='<%# Eval("PosterUrl") %>'
                        AlternateText='<%# Eval("Title") %>'
                        Visible='<%# !String.IsNullOrWhiteSpace(Convert.ToString(Eval("PosterUrl"))) %>'
                        Height="200" />
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </form>
</body>
</html>
