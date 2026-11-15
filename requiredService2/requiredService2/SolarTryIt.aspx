<%@ Page Language="C#" AutoEventWireup="true" Async="true" CodeBehind="SolarTryIt.aspx.cs" Inherits="requiredService2.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                <h2> Welcome to the Solar Intensity Finder Try it Page!</h2>
                <p>  
                   The Following service will take latitude and longitude coordinates and call the 
                   Solar Intensity API to return the solar intensity for that location.
                </p>
                <hr />
                <div>
                  &nbsp;URL : <a href ="https://localhost:44356/Help/Api/GET-api-Solar_latitude_longitude"> https://localhost:44356/Help/Api/GET-api-Solar_latitude_longitude </a>
                </div>
                <hr />
                <div>
                    Method Name: SolarIntensity<br />
                    Parameter Types: decimal (latitude) &amp; decimal (longitude)<br />
                    Return Type: decimal (annual solar intensity avg)
                    <br />
                    <hr />
                </div>
            </div>
            Latitude:
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            Longitude:<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
&nbsp;<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Find Solar Intensity" />
        </div>
        <div>
            Solar Intensity: <asp:TextBox ID="TextBox3" runat="server" ReadOnly="True"></asp:TextBox>
        </div>
    </form>
</body>
</html>
