<%@ Page Language="C#" MasterPageFile="~/GWS.master" AutoEventWireup="true" CodeFile="PesquisaPais.aspx.cs" Inherits="Default2" Title="Pesquisa por país" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div>
        <asp:Label ID="Label1" runat="server" Text="Escolha o país a pesquisar:"></asp:Label>
        <br />
        <br />
        <asp:DropDownList ID="DropDownList1" runat="server" DataTextField="Country" 
            DataValueField="Country" 
            onselectedindexchanged="DropDownList1_SelectedIndexChanged">
        </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button1" runat="server" Text="Pesquisar" 
            onclick="Button1_Click" />
        <br />
        <br />
        <br />
        <asp:GridView ID="GridView1" runat="server" 
            onrowdatabound="GridView1_RowDataBound" 
            onselectedindexchanged="GridView1_SelectedIndexChanged">
        </asp:GridView>
        <br />
        <br />
        <asp:GridView ID="GridView2" runat="server" 
            onselectedindexchanged="GridView2_SelectedIndexChanged">
        </asp:GridView>
        <br />
    </div>
</asp:Content>

