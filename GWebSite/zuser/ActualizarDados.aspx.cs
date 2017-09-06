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
        if (!Page.IsPostBack)
        {
            string user = (string)Session["userName"];
            string pass = (string)Session["passWord"];
            int uid = int.Parse(Session["UserID"].ToString());
            GWSiteClassLibrary.IPlayer pl = GWSiteClassLibrary.Factory.CreatePlayerService();
            DataSet ds = pl.FindByName(user, pass, "UserID", uid.ToString());
            GridView1.DataSource = ds;
            GridView1.DataBind();
            int plid = int.Parse(GridView1.Rows[0].Cells[0].Text);
            Label2.Text = plid.ToString();
            bindDetalhesPlayers(plid);
        }
    }


    private void bindDetalhesPlayers(int id)
    {
        string user = (string)Session["userName"];
        string pass = (string)Session["passWord"];

        GWSiteClassLibrary.IPlayer bdp = GWSiteClassLibrary.Factory.CreatePlayerService();
        DetailsView1.DataSource = bdp.GetByID(user, pass, id);
        DetailsView1.DataBind();
    }
    protected void DetailsView1_ModeChanging(object sender, DetailsViewModeEventArgs e)
    {
        string user = (string)Session["userName"];
        string pass = (string)Session["passWord"];

        GWSiteClassLibrary.IPlayer pl = GWSiteClassLibrary.Factory.CreatePlayerService();
        int plid = int.Parse(GridView1.Rows[0].Cells[0].Text);

        if (DetailsView1.CurrentMode == DetailsViewMode.ReadOnly)
        {
            DetailsView1.ChangeMode(DetailsViewMode.Edit);

            bindDetalhesPlayers(plid);
        }
        else if (DetailsView1.CurrentMode == DetailsViewMode.Edit)
        {
            DetailsView1.ChangeMode(DetailsViewMode.ReadOnly);

            bindDetalhesPlayers(plid);
        }
    }
    protected void DetailsView1_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
    {
        string user = (string)Session["userName"];
        string pass = (string)Session["passWord"];

        GWSiteClassLibrary.IPlayer dvpl = GWSiteClassLibrary.Factory.CreatePlayerService();
        int plid = int.Parse(GridView1.Rows[0].Cells[0].Text);
        string nome = ((TextBox)DetailsView1.Rows[0].Cells[1].Controls[0]).Text;
        int idade = int.Parse(((TextBox)DetailsView1.Rows[1].Cells[1].Controls[0]).Text);
        char genero = char.Parse(((TextBox)DetailsView1.Rows[2].Cells[1].Controls[0]).Text);
        string morada = ((TextBox)DetailsView1.Rows[3].Cells[1].Controls[0]).Text;
        string pais = ((TextBox)DetailsView1.Rows[4].Cells[1].Controls[0]).Text;
        string mail = ((TextBox)DetailsView1.Rows[5].Cells[1].Controls[0]).Text;

        string[] value = { nome, idade.ToString(), genero.ToString(), mail, morada, pais };

        Label2.Text = plid.ToString() + "," + nome + "," + idade.ToString() + "," + genero.ToString() + "," + mail + "," + morada + ","+pais;
        //string nome = ((TextBox)DetailsView1.Rows[0].Cells[1].Controls[0]).Text;
        GWSiteClassLibrary.GWSiteStatusEnum statusX = dvpl.UpdatePlayerX(user, pass, plid, value);
        Label2.Text = statusX.ToString();
        //GWSiteClassLibrary.GWSiteStatusEnum status = dvpl.UpdatePlayer(user, pass, plid, nome, idade, genero, mail, morada, pais);
        DetailsView1.ChangeMode(DetailsViewMode.ReadOnly);
        bindDetalhesPlayers(plid);
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
