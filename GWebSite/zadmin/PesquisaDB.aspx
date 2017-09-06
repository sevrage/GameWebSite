<%@ Page Title="" Language="C#" MasterPageFile="~/GWS.master" AutoEventWireup="true"
    CodeFile="PesquisaDB.aspx.cs" Inherits="zadmin_PesquisaDB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Large"
        Text="Pesquisa Por Palavra"></asp:Label>
    <br />
    <a>Palavra a Pesquisar: </a>
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <asp:Button ID="Button1" runat="server" Text="Pesquisar" OnClick="Button1_Click" />
    <asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="False" 
        AllowSorting="True" AllowPaging="True" 
        onpageindexchanging="GridView1_PageIndexChanging" 
        onsorting="GridView1_Sorting" DataKeyNames="ColumnName,ColumnValue" 
        onselectedindexchanged="GridView1_SelectedIndexChanged">
        <Columns>
            <asp:ButtonField CommandName="Select" Text="Seleccionar" />
            <asp:BoundField DataField="ColumnName" HeaderText="Tabela" ReadOnly="True" SortExpression="ColumnName"/>
            <asp:BoundField DataField="ColumnValue" HeaderText="Valor" ReadOnly="True" SortExpression="ColumnValue"/>
        </Columns>
    </asp:GridView>
    <asp:GridView ID="GridView2" runat="server">
    </asp:GridView>    
    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>    
    
</asp:Content>
