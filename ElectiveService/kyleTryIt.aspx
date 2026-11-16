<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="kyleTryIt.aspx.cs" Inherits="ElectiveService.kyleTryIt" %>
<%@ Register Src="~/UserControl/CaptchaControl.ascx" TagPrefix="uc" TagName="Captcha" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Kyle's TryIt page</title>
    <style>
        .out { display: block; margin-top: 8px; padding: 6px; background: #eee; white-space: pre-wrap; max-height: 300px; overflow: auto; }
        .out:empty { display: none; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Kyle's TryIt page</h2>

        <section id="hash">
            <h3>PasswordHasher (HashLib.dll)</h3>
            Password:
            <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
            <asp:Button ID="btnHash" runat="server" Text="Hash" OnClick="btnHash_Click" />
            <asp:Label ID="lblHash" runat="server" CssClass="out"></asp:Label>
            <br />
            Stored hash:
            <asp:TextBox ID="storedHash" runat="server"></asp:TextBox>
            Password to check:
            <asp:TextBox ID="txtCheck" runat="server"></asp:TextBox>
            <asp:Button ID="btnVerify" runat="server" Text="Verify" OnClick="btnVerify_Click" />
            <asp:Label ID="lblMatch" runat="server" CssClass="out"></asp:Label>
        </section>
        <hr />

        <section id="captcha">
            <h3>CaptchaControl (user control)</h3>
            <uc:Captcha ID="captcha1" runat="server" />
            <asp:Button ID="btnCheck" runat="server" Text="Check" OnClick="btnCheck_Click" />
            <asp:Label ID="lblCaptcha" runat="server" CssClass="out"></asp:Label>
        </section>
        <hr />

        <section id="download">
            <h3>WebDownload (WCF service)</h3>
            URL:
            <asp:TextBox ID="txtUrl" runat="server" Width="350px"></asp:TextBox>
            <asp:Button ID="btnDownload" runat="server" Text="Invoke" OnClick="btnDownload_Click" />
            <asp:Label ID="downloadOut" runat="server" CssClass="out"></asp:Label>
        </section>
        <hr />

        <section id="top10">
            <h3>Top10ContentWords (REST service)</h3>
            URL:
            <asp:TextBox ID="txtTopUrl" runat="server" Width="350px"></asp:TextBox>
            <asp:Button ID="btnTop10" runat="server" Text="Invoke" OnClick="btnTop10_Click" />
            <asp:Label ID="lblTop10" runat="server" CssClass="out"></asp:Label>
        </section>
    </form>
</body>
</html>
