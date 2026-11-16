<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CaptchaControl.ascx.cs" Inherits="ElectiveService.UserControl.CaptchaControl" %>
<div>
    <asp:Image ID="imgCaptcha" runat="server" ImageUrl="~/CaptchaImage.ashx" />
    <asp:TextBox ID="txtCaptcha" runat="server"></asp:TextBox>
</div>
