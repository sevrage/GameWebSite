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
    protected void Button1_Click(object sender, EventArgs e)
    {
        string user = (string)Session["userName"];
        string pass = (string)Session["passWord"];

        if (RadioButtonList1.SelectedItem.Value.Equals("Altutil"))
        {
            GridView1.Visible = true;
            GridView2.Visible = false;
            Label2.Visible = false;
            Label3.Visible = false;
            DetailsView1.Visible = false;
            GWSiteClassLibrary.IUser ualt = GWSiteClassLibrary.Factory.CreateUserService();
            DataSet dsalt = ualt.GetAll(user, pass);
            GridView1.DataSource = dsalt;
            GridView1.DataBind();
        }
        else
            if (RadioButtonList1.SelectedItem.Value.Equals("Rutl"))
            {
                GridView1.Visible = false;
                GridView2.Visible = true;
                Label2.Visible = false;
                Label3.Visible = false;
                DetailsView1.Visible = false;
                GWSiteClassLibrary.IUser udel = GWSiteClassLibrary.Factory.CreateUserService();
                DataSet dsdel = udel.GetAll(user, pass);
                GridView2.DataSource = dsdel;
                GridView2.DataBind();
            }
    }
    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {
        string user = (string)Session["userName"];
        string pass = (string)Session["passWord"];

        int iduser = int.Parse(GridView2.SelectedDataKey.Value.ToString());
        
        GWSiteClassLibrary.IUser deluser = GWSiteClassLibrary.Factory.CreateUserService();
        GWSiteClassLibrary.GWSiteStatusEnum status = deluser.DeleteUser(user, pass, iduser);
        Label2.Visible = true;
        Label2.Text = status.ToString();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        int idutl = int.Parse(GridView1.SelectedDataKey.Value.ToString());
        bindDetalhesUsers(idutl);
    }
    
    private void bindusers()
    {
        string user = (string)Session["userName"];
        string pass = (string)Session["passWord"];

        GridView1.Visible = true;
        GWSiteClassLibrary.IUser bu = GWSiteClassLibrary.Factory.CreateUserService();
        DataSet dsbu = bu.GetAll(user, pass);
        GridView1.DataSource = dsbu;
        GridView1.DataBind();
    }

    private void bindDetalhesUsers(int id)
    {
        string user = (string)Session["userName"];
        string pass = (string)Session["passWord"];

        DetailsView1.Visible = true;
        GWSiteClassLibrary.IUser bdu = GWSiteClassLibrary.Factory.CreateUserService();
        DetailsView1.DataSource = bdu.GetByID(user, pass, id);
        DetailsView1.DataBind();
    }
    protected void DetailsView1_ModeChanging(object sender, DetailsViewModeEventArgs e)
    {
        if (DetailsView1.CurrentMode == DetailsViewMode.ReadOnly)
        {
            DetailsView1.ChangeMode(DetailsViewMode.Edit);
            string idstr = GridView1.SelectedDataKey.Value.ToString();
            int id = int.Parse(idstr);
            bindDetalhesUsers(id);
        }
        else if (DetailsView1.CurrentMode == DetailsViewMode.Edit)
        {
            DetailsView1.ChangeMode(DetailsViewMode.ReadOnly);
            string idstr = GridView1.SelectedDataKey.Value.ToString();
            int id = int.Parse(idstr);
            bindDetalhesUsers(id);
        }
    }
    protected void DetailsView1_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
    {
        string user = (string)Session["userName"];
        string pass = (string)Session["passWord"];

        
        GWSiteClassLibrary.IUser dvuser = GWSiteClassLibrary.Factory.CreateUserService();
        int id = int.Parse(DetailsView1.Rows[0].Cells[1].Text);
        string passnew = ((TextBox)DetailsView1.Rows[2].Cells[1].Controls[0]).Text;
        GWSiteClassLibrary.GWSiteStatusEnum status = dvuser.UpdateUserPass(user, pass, id, passnew);
        Label3.Visible = true;
        Label3.Text = status.ToString();
        DetailsView1.Visible = false;
        DataSet ds = dvuser.GetAll(user, pass);
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false; //esconde ID
    }
}
