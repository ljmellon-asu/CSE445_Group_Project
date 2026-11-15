<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WordCountTryIt.aspx.cs" Inherits="requiredService1.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Try it! Page</title>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
        <div>
            <h2> Welcome to the WordCount Service Try it Page!</h2>
            <p>This service takes a .txt file and counts the occurances of every
               word in the .txt file, returning a string of json data of the words
               and their counts in key value pairs.
            </p>
            <hr />
            <div>
                Method Name: WordCount()<br />
                Parameter Types: Byte[]<br />
                Return Type: String (of json formatted data)</div>
            <hr />
            <div>
                  URL : <a href ="https://localhost:44308/WebService1.asmx?op=WordCount"> https://localhost:44308/WebService1.asmx?op=WordCount </a>
            </div>
            <hr />
            Upload .txt file here: <input id="File1" type="file" runat="server" />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Count Words!" Width="268px" />
            <hr />
        </div>
        <div>
        result: <asp:TextBox ID="TextBox2" runat="server" Height="368px" OnTextChanged="TextBox2_TextChanged" ReadOnly="True" TextMode="MultiLine" Width="666px"></asp:TextBox>
    </div>
    </form>
</body>
</html>
