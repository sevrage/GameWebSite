using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Registar_Click(object sender, EventArgs e)
    {
        try
        {
            GWSiteClassLibrary.GWSiteStatusEnum status;
            GWSiteClassLibrary.GWSiteStatusEnum st;
            string nome = TextBox1.Text;
            int idade = int.Parse(TextBox2.Text);
            string morada = TextBox4.Text;
            string pais = TextBox5.Text;
            string email = TextBox6.Text;
            string username = TextBox7.Text;
            string password = TextBox8.Text;
            char genero = char.Parse(RadioButtonList1.SelectedItem.Value);

            GWSiteClassLibrary.IUser us = GWSiteClassLibrary.Factory.CreateUserService();
            int usAdd = us.AddUser(username, password, out status);
            ConfirmaUser.Text = usAdd.ToString();

            GWSiteClassLibrary.IPlayer player = GWSiteClassLibrary.Factory.CreatePlayerService();
            int idAdd = player.AddPlayer(username, password, usAdd, nome, idade, genero, email, morada, pais, out st);

            Label16.Text = status.ToString();
            if (st == GWSiteClassLibrary.GWSiteStatusEnum.OK)
                ConfirmaUser.Visible = true;
        }

        catch (SystemException) { }
    }
}
