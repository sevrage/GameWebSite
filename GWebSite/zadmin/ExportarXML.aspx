<%@ Page Title="Exportar XML" Language="C#" MasterPageFile="~/GWS.master" AutoEventWireup="true"
    CodeFile="ExportarXML.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Large"
        Text="Criar Ficheiro XML"></asp:Label>
    <p>
        <asp:Button ID="Button1" runat="server" Text="Criar" onclick="Button1_Click" />
    </p>
    <p>
        <a href="TopMap.xml" target=”_blank”>Abrir XML</a>
</asp:Content>
