<%@ Page Language="C#" MasterPageFile="~/GWS.master" AutoEventWireup="true" CodeFile="Download.aspx.cs" Inherits="Default2" Title="Download" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div>
    
        <table class="style1">
            <tr>
                <td>
                    <asp:DropDownList ID="ddlMapas" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                        Text="Pesquisar" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server" onrowcreated="GridView1_RowCreated">
                    </asp:GridView>
                </td>
                <td>
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/jogo1.rar">Download</asp:HyperLink>
                </td>
            </tr>
        </table>
    
    </div>
</asp:Content>

