<%@ Page Language="C#" MasterPageFile="~/GWS.master" AutoEventWireup="true" CodeFile="Registo.aspx.cs" Inherits="Default2" Title="Registo novo Utilizador" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>       
    <table>
        <tr>
            <td>
            </td>
            <td style="margin-left: 40px">
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="True" 
                    Font-Size="Large" Text="Novo Registo"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Nome"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:CompareValidator ID="CompareValidator2" runat="server" 
                    ControlToValidate="TextBox1" ErrorMessage="Nome inválido" 
                    Operator="DataTypeCheck"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Idade"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:RangeValidator ID="RangeValidator2" runat="server" 
                    ControlToValidate="TextBox2" ErrorMessage="Idade inválida" Type="Integer" 
                    MaximumValue="100" MinimumValue="10"></asp:RangeValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Text="Sexo"></asp:Label>
            </td>
            <td style="margin-left: 200px">
                &nbsp;
                &nbsp;<asp:RadioButtonList ID="RadioButtonList1" runat="server" 
                    RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="M">Masculino</asp:ListItem>
                    <asp:ListItem Value="F">Feminino</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="Telefone"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:RangeValidator ID="RangeValidator3" runat="server" 
                    ControlToValidate="TextBox3" ErrorMessage="Telefone inválido" 
                    Type="Integer" MaximumValue="999999999" MinimumValue="100000000"></asp:RangeValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label6" runat="server" Text="Morada"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label7" runat="server" Text="País"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:CompareValidator ID="CompareValidator3" runat="server" 
                    ControlToValidate="TextBox5" ErrorMessage="País inválido" 
                    Operator="DataTypeCheck"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label8" runat="server" Text="E-mail"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ErrorMessage="E-mail incorrecto" 
                    ValidationExpression="[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[a-z]{2,4}" 
                    ControlToValidate="TextBox6"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label9" runat="server" Text="Username"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label10" runat="server" Text="Password"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox8" runat="server" TextMode="Password"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label15" runat="server" Text="Confirme password"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox9" runat="server" TextMode="Password"></asp:TextBox>
            </td>
            <td>
                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ControlToCompare="TextBox8" ControlToValidate="TextBox9" 
                    ErrorMessage="A password e a confirmação não são iguais"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Default.aspx">Ir 
                para página de Login</asp:HyperLink>
            </td>
            <td>
                <asp:Button ID="Registar" runat="server"  onclick="Registar_Click"
                    Text="Enviar" />
            </td>
            <td>
                <asp:Label ID="ConfirmaUser" runat="server" Text="Registo efectuado com sucesso" 
                    Visible="False"></asp:Label>
                <br />
                <asp:Label ID="Label16" runat="server"></asp:Label>
                <br />
            </td>
        </tr>
    </table>
    <br />
&nbsp;</div>
</asp:Content>

