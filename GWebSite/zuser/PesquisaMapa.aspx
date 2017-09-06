<%@ Page Language="C#" MasterPageFile="~/GWS.master" AutoEventWireup="true" CodeFile="PesquisaMapa.aspx.cs" Inherits="Default2" Title="Pesquisa de Mapas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <asp:Label ID="Label1" runat="server" Text="Escolha o mapa a pesquisar:" 
            Font-Bold="True" Font-Size="Larger"></asp:Label>
        <br />
        <br />
        <asp:DropDownList ID="DropDownList1" runat="server" 
            onselectedindexchanged="DropDownList1_SelectedIndexChanged">
        </asp:DropDownList>
&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button1" runat="server" Text="Pesquisar" 
            onclick="Button1_Click" />
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Top 10"></asp:Label>
        <br />
        <br />
        <asp:GridView ID="GridView1" runat="server" 
            onselectedindexchanged="GridView1_SelectedIndexChanged" 
            onrowcreated="GridView1_RowCreated">
        </asp:GridView>
        <br />
    </div>
</asp:Content>

