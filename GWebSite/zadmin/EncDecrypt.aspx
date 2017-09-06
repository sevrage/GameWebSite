<%@ Page Title="Encriptar ConnectionString" Language="C#" MasterPageFile="~/GWS.master"
    AutoEventWireup="true" CodeFile="EncDecrypt.aspx.cs" Inherits="Default3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <br />
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" Text="Encrypt" 
    onclick="Button1_Click1" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button2" runat="server" Text="Decrypt" 
    onclick="Button2_Click1" />
    <br />
    <br />
    <p>
        ConnectionString:</p>
    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    <br />
    <br />
    <p>
        Estado:</p>
    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>
