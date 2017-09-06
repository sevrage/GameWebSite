<%@ Page Language="C#" MasterPageFile="~/GWS.master" AutoEventWireup="true" CodeFile="Mapas.aspx.cs" Inherits="Default2" Title="Administração Mapas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div>
    
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Large" 
            Text="Administração de Mapas"></asp:Label>
        <br />
        <br />
        <br />
    
    </div>
    <div>
        <table class="style1">
            <tr>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                        <asp:ListItem Selected="True" Value="Addnmap">Adicionar novo mapa</asp:ListItem>
                        <asp:ListItem Value="Altmap">Alterar mapa</asp:ListItem>
                        <asp:ListItem Value="Elmap">Eliminar mapa</asp:ListItem>
                    </asp:RadioButtonList>
                    <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                        Text="Escolher" />
                </td>
                <td>
                    <asp:GridView ID="GridView2" runat="server" DataKeyNames="MapID" 
                        onselectedindexchanged="GridView2_SelectedIndexChanged">
                        <Columns>
                            <asp:ButtonField CommandName="Select" Text="Eliminar" />
                        </Columns>
                    </asp:GridView>
                    <br />
                    <br />
                    <asp:Label ID="Label3" runat="server" Visible="False"></asp:Label>
                    <br />
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <br />
                    <asp:Label ID="Label5" runat="server" Text="Introduza o nome do novo mapa: " 
                        Visible="False"></asp:Label>
&nbsp;<asp:TextBox ID="TextBox1" runat="server" Visible="False"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button2" runat="server" onclick="Button2_Click1" 
                        Text="Confirmar" Visible="False" />
&nbsp;
                    <asp:Label ID="Label6" runat="server" Visible="False"></asp:Label>
                    <br />
                    <br />
                    <asp:GridView ID="GridView1" runat="server" CellPadding="4" 
                        DataKeyNames="MapID" ForeColor="#333333" GridLines="None" 
                        onselectedindexchanged="GridView1_SelectedIndexChanged" Visible="False">
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
                    <asp:Label ID="Label4" runat="server"></asp:Label>
                    <br />
                    <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <br />
                </td>
                <td class="style2">
                    <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateEditButton="True" 
                        BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" 
                        CellPadding="4" Height="50px" onmodechanging="DetailsView1_ModeChanging" 
                        Width="125px" onitemupdating="DetailsView1_ItemUpdating"
                        Visible="False" AutoGenerateRows="False">
                        
                        <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                        <RowStyle BackColor="White" ForeColor="#330099" />
                        <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                        <Fields>
                            <asp:BoundField DataField="MapID" HeaderText="IDMap" ReadOnly="True" />
                            <asp:BoundField DataField="Descr" HeaderText="Descricao" />
                        </Fields>
                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                        <EditRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                    </asp:DetailsView>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </div>
</asp:Content>

