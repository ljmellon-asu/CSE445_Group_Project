<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MoviePreferences.ascx.cs" Inherits="ElectiveService.UserControl.MoviePreferences" %>
<h3>Movie Preferences</h3>

<p>Enter your username:</p>

<asp:TextBox ID="txtUsername" runat="server" />

<p>Select your favorite movie genre:</p>

<asp:DropDownList ID="ddlGenre" runat="server">
    <asp:ListItem>Action</asp:ListItem>
    <asp:ListItem>Comedy</asp:ListItem>
    <asp:ListItem>Drama</asp:ListItem>
    <asp:ListItem>Horror</asp:ListItem>
    <asp:ListItem>Sci-Fi</asp:ListItem>
</asp:DropDownList>

<br /><br />

<asp:Button ID="btnSave" runat="server" Text="Save Profile" OnClick="btnSave_Click" />

<asp:Button ID="btnRead" runat="server" Text="Read Cookie" OnClick="btnRead_Click" />

<br /><br />

<asp:Label ID="lblProfile" runat="server" />