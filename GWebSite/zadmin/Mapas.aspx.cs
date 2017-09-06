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
        string user = Session["userName"].ToString();
        string pass = (string)Session["passWord"];
        if (RadioButtonList1.SelectedItem.Value.Equals("Addnmap"))
        {
            DetailsView1.Visible = false;
            Label3.Visible = false;
            Label4.Visible = false;
            Label6.Visible = false;
            GridView1.Visible = false;
            GridView2.Visible = false;
            Label5.Visible = true;
            TextBox1.Visible = true;
            Button2.Visible = true;
        }
        else
            if (RadioButtonList1.SelectedItem.Value.Equals("Altmap"))
            {
                DetailsView1.Visible = false;
                Label3.Visible = false;
                Label4.Visible = false;
                Label6.Visible = false;
                GridView1.Visible = true;
                GridView2.Visible = false;
                GWSiteClassLibrary.IMap malt = GWSiteClassLibrary.Factory.CreateMapService();
                DataSet dsalt = malt.GetAll(user, pass);
                GridView1.DataSource = dsalt;
                GridView1.DataBind();
            }
            else
                if (RadioButtonList1.SelectedItem.Value.Equals("Elmap"))
                {
                    DetailsView1.Visible = false;
                    Label3.Visible = false;
                    Label4.Visible = false;
                    Label6.Visible = false;
                    GridView2.Visible = true;
                    GridView1.Visible = false;
                    Label3.Visible = false;
                    GWSiteClassLibrary.IMap mdel = GWSiteClassLibrary.Factory.CreateMapService();
                    DataSet dsdel = mdel.GetAll(user, pass);
                    GridView2.DataSource = dsdel;
                    GridView2.DataBind();
                    
                }
    }

    protected void Button2_Click1(object sender, EventArgs e)
    {
        string user = Session["userName"].ToString();
        string pass = (string)Session["passWord"];

        Label6.Visible = true;
        string descr = TextBox1.Text;
        GWSiteClassLibrary.GWSiteStatusEnum status;
        GWSiteClassLibrary.IMap admap = GWSiteClassLibrary.Factory.CreateMapService();
        status = admap.AddMap(user, pass, descr);
        Label6.Text = status.ToString();
        Label5.Visible = false;
        TextBox1.Visible = false;
        Button2.Visible = false;
    }
    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {
        string user = Session["userName"].ToString();
        string pass = (string)Session["passWord"];

        Label3.Visible = true;
        int idmap = int.Parse(GridView2.SelectedDataKey.Value.ToString());
        GWSiteClassLibrary.IMap delmap = GWSiteClassLibrary.Factory.CreateMapService();
        GWSiteClassLibrary.GWSiteStatusEnum status = delmap.DeleteMap(user, pass, idmap);
        Label3.Text = status.ToString();
        DataSet ds = delmap.GetAll(user, pass);
        GridView2.DataSource = ds;
        GridView2.DataBind();
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        int idmap = int.Parse(GridView1.SelectedDataKey.Value.ToString());
        bindDetalhesMaps(idmap);
    }

    private void bindmaps()
    {
        string user = Session["userName"].ToString();
        string pass = (string)Session["passWord"];
        GWSiteClassLibrary.IMap bm = GWSiteClassLibrary.Factory.CreateMapService();
        DataSet dsbm = bm.GetAll(user, pass);
        GridView1.DataSource = dsbm;
        GridView1.DataBind();
    }

    private void bindDetalhesMaps(int id)
    {
        string user = Session["userName"].ToString();
        string pass = (string)Session["passWord"];

        DetailsView1.Visible = true;
        GWSiteClassLibrary.IMap bdm = GWSiteClassLibrary.Factory.CreateMapService();
        DetailsView1.DataSource = bdm.GetByID(user, pass, id);
        DetailsView1.DataBind();
    }
    protected void DetailsView1_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
    {
        string user = Session["userName"].ToString();
        string pass = (string)Session["passWord"];
        Label4.Visible = true;
        GWSiteClassLibrary.IMap dvmap = GWSiteClassLibrary.Factory.CreateMapService();
        int id = int.Parse(DetailsView1.Rows[0].Cells[1].Text);
        string descr = ((TextBox)DetailsView1.Rows[1].Cells[1].Controls[0]).Text;
        GWSiteClassLibrary.GWSiteStatusEnum status = dvmap.UpdateMap(user, pass, id, descr);
        Label4.Text = status.ToString();
        DetailsView1.Visible = false;
        DataSet ds = dvmap.GetAll(user, pass);
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }
    protected void DetailsView1_ModeChanging(object sender, DetailsViewModeEventArgs e)
    {
        if (DetailsView1.CurrentMode == DetailsViewMode.ReadOnly)
        {
            DetailsView1.ChangeMode(DetailsViewMode.Edit);
            string idstr = GridView1.SelectedDataKey.Value.ToString();
            int id = int.Parse(idstr);
            bindDetalhesMaps(id);
        }
        else if (DetailsView1.CurrentMode == DetailsViewMode.Edit)
        {
            DetailsView1.ChangeMode(DetailsViewMode.ReadOnly);
            string idstr = GridView1.SelectedDataKey.Value.ToString();
            int id = int.Parse(idstr);
            bindDetalhesMaps(id);
        }
    }
}
