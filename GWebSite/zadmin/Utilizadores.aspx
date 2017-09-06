<%@ Page Language="C#" MasterPageFile="~/GWS.master" AutoEventWireup="true" CodeFile="Utilizadores.aspx.cs" Inherits="Default2" Title="Administração Utilizadores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 21px;
        }
        .style2
        {
            width: 75px;
        }
        .style3
        {
            width: 246px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div>
    
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="True" 
            Font-Size="Large" Text="Administraçao de Utilizadores"></asp:Label>
    
    </div>
    <table class="style1">
        <tr>
            <td class="style3">
                <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                    <asp:ListItem Selected="True" Value="Altutil">Alterar dados utilizador</asp:ListItem>
                    <asp:ListItem Value="Rutl">Remover utlizador</asp:ListItem>
                </asp:RadioButtonList>
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                    Text="Seleccionar" />
                <br />
            </td>
            <td>
                <asp:GridView ID="GridView2" runat="server" CellPadding="4" 
                    DataKeyNames="UserID" ForeColor="#333333" GridLines="None" 
                    onselectedindexchanged="GridView2_SelectedIndexChanged" Visible="False">
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:ButtonField CommandName="Select" Text="Eliminar" />
                    </Columns>
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </td>
            <td class="style2">
                <asp:Label ID="Label2" runat="server" Visible="False"></asp:Label>
                </td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;
                <asp:GridView ID="GridView1" runat="server" DataKeyNames="UserID" 
                    onrowcreated="GridView1_RowCreated" 
                    onselectedindexchanged="GridView1_SelectedIndexChanged" CellPadding="4" 
                    ForeColor="#333333" GridLines="None" Visible="False">
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:ButtonField CommandName="Select" Text="Seleccionar" />
                    </Columns>
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </td>
            <td>
                <asp:DetailsView ID="DetailsView1" runat="server" Height="50px" Width="125px" 
                    AutoGenerateEditButton="True" onitemupdating="DetailsView1_ItemUpdating" 
                    onmodechanging="DetailsView1_ModeChanging" AutoGenerateRows="False" 
                    BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" 
                    CellPadding="4">
                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                    <RowStyle BackColor="White" ForeColor="#330099" />
                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                    <Fields>
                        <asp:BoundField DataField="UserID" HeaderText="UserID" ReadOnly="True" />
                        <asp:BoundField DataField="UserName" HeaderText="Username" ReadOnly="True" />
                        <asp:BoundField DataField="Pass" HeaderText="Password" />
                    </Fields>
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                    <EditRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                </asp:DetailsView>
                <br />
                <asp:Label ID="Label3" runat="server" Visible="False"></asp:Label>
            </td>
            <td class="style2">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;</td>
            <td>
                <br />
            </td>
            <td class="style2">
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

