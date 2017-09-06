<%@ Page Title="" Language="C#" MasterPageFile="~/GWS.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


            <asp:Login ID="Login1" runat="server" BackColor="#EFF3FB" BorderColor="#B5C7DE" 
                BorderPadding="4" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" 
                Font-Size="0.8em" ForeColor="#333333" 
                onauthenticate="Login1_Authenticate1">
                <TextBoxStyle Font-Size="0.8em" />
                <LoginButtonStyle BackColor="White" BorderColor="#507CD1" BorderStyle="Solid" 
                    BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" 
                    ForeColor="#284E98" />
                <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
                <TitleTextStyle BackColor="#507CD1" Font-Bold="True" Font-Size="0.9em" 
                    ForeColor="White" />
            </asp:Login>
        

            <a href="zaccount/Registo.aspx">Ainda não estou registado</a>
        

</asp:Content>

