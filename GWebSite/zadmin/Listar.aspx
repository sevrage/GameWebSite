<%@ Page Language="C#" MasterPageFile="~/GWS.master" AutoEventWireup="true" CodeFile="Listar.aspx.cs" Inherits="Default2" Title="Listar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div>
    
    <table class="style1">
        <tr>
            <td class="style4">
                <asp:GridView ID="GridView2" runat="server">
                </asp:GridView>
                <br />
                <br />
            </td>
            <td class="style3">
                <asp:GridView ID="GridView3" runat="server">
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="style4">
                <asp:GridView ID="GridView4" runat="server">
                </asp:GridView>
            </td>
            <td class="style3">
                <asp:GridView ID="GridView5" runat="server" AllowPaging="True" AllowSorting="True" 
                    onpageindexchanging="GridView5_PageIndexChanging1" 
                    onsorting="GridView5_Sorting" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="ScoreID" HeaderText="Score ID" ReadOnly="True" SortExpression="ScoreID"/>
                        <asp:BoundField DataField="PlayerID" HeaderText="Player ID" ReadOnly="True" SortExpression="PlayerID"/>
                        <asp:BoundField DataField="MapID" HeaderText="Map ID" ReadOnly="True" SortExpression="MapID"/>
                        <asp:BoundField DataField="Score" HeaderText=" Score" ReadOnly="True" SortExpression="Score"/>
                        <asp:BoundField DataField="Date" HeaderText="Data" ReadOnly="True" SortExpression="Date"/>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
   </div>
    
</asp:Content>

