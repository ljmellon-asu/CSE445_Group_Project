<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Login.ascx.cs" Inherits="ElectiveService.UserControl.WebUserControl1" %>
<style type="text/css">
    .auto-style1 {
        width: 202px;
    }
    .auto-style2 {
        width: 202px;
        height: 34px;
    }
    .auto-style3 {
        height: 34px;
    }
</style>

<table style="width: 100%;">
    <tr>
        <td class="auto-style2">UserName</td>
        <td class="auto-style3">
            <asp:TextBox ID="unameTxt" runat="server" OnTextChanged="unameTxt_TextChanged" ValidationGroup="loginVal"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="unameTxt" ErrorMessage="UserName is a required field." ForeColor="#FF0066" ValidationGroup="loginVal"></asp:RequiredFieldValidator>
        </td>
        <td class="auto-style3"></td>
    </tr>
    <tr>
        <td class="auto-style1">Password</td>
        <td>
            <asp:TextBox ID="pwordTxt" runat="server" ValidationGroup="loginVal"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="pwordTxt" ErrorMessage="Password is a required field" ForeColor="#FF0066" ValidationGroup="loginVal"></asp:RequiredFieldValidator>
            .</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td class="auto-style1">&nbsp;</td>
        <td>
            <asp:Button ID="loginButton" runat="server" Text="Login" ValidationGroup="loginVal" />
        </td>
        <td>&nbsp;</td>
    </tr>
</table>

