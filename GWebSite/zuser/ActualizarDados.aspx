<%@ Page Language="C#" MasterPageFile="~/GWS.master" AutoEventWireup="true" CodeFile="ActualizarDados.aspx.cs" Inherits="Default2" Title="Actualizar Dados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            height: 134px;
        }
        .style2
        {
            height: 119px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
    
        <table class="style1">
            <tr>
                <td>
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="True" 
                    Font-Size="Large" Text="Actualizar Dados"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label2" runat="server" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style2">
                
                <asp:DetailsView ID="DetailsView1" runat="server" Height="50px" Width="125px" 
                        AutoGenerateEditButton="True" AutoGenerateRows="False" 
                        onitemupdating="DetailsView1_ItemUpdating" 
                        onmodechanging="DetailsView1_ModeChanging">
                    <Fields>
                        <asp:BoundField DataField="Name" HeaderText="Nome" />
                        <asp:BoundField DataField="Age" HeaderText="Idade" />
                        <asp:BoundField DataField="Gender" HeaderText="Sexo" />
                        <asp:BoundField DataField="Address" HeaderText="Morada" />
                        <asp:BoundField DataField="Country" HeaderText="País" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                    </Fields>
                </asp:DetailsView>
                
                </td>
                <td class="style2">
                    <asp:GridView ID="GridView1" runat="server" 
                        onselectedindexchanged="GridView1_SelectedIndexChanged" Visible="False">
                    </asp:GridView>
                </td>
            </tr>
        </table>
    
    </div>
</asp:Content>

