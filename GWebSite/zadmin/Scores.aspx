<%@ Page Language="C#" MasterPageFile="~/GWS.master" AutoEventWireup="true" CodeFile="Scores.aspx.cs"
    Inherits="Default2" Title="Scores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Large"
            Text="Administração de Resultados"></asp:Label>
        <br />
        <br />
    </div>
    <div>
        <asp:Label ID="Label2" runat="server" Text="Introdução de novos Resultados"></asp:Label>
        <br />
        <br />
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="http://phpdev.dei.isep.ipp.pt/i070997/ARQSI86/SetScore.html"
            Target="_blank">Introduzir Pontuação (Client PHP - PHPDEV)</asp:HyperLink>
        <br />
        <br />
        <br />
        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="http://dot.dei.isep.ipp.pt/070997/dir2/GWebService.asmx"
            Target="_blank">DOT - GWebService</asp:HyperLink>
    </div>
</asp:Content>
