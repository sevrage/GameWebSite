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
        string user = (string)Session["userName"];
        string pass = (string)Session["passWord"];

        if (!Page.IsPostBack)
        {
            GWSiteClassLibrary.IMap map = GWSiteClassLibrary.Factory.CreateMapService();
            DataSet ds = map.GetAll(user, pass);
            gvmaps.DataSource = ds;
            gvmaps.DataBind();
        }
    }
    protected void gvmaps_SelectedIndexChanged(object sender, EventArgs e)
    {
        string user = (string)Session["userName"];
        string pass = (string)Session["passWord"];
        int uid = int.Parse(Session["UserID"].ToString());

        int idmap = int.Parse(gvmaps.SelectedDataKey.Value.ToString());
        GWSiteClassLibrary.IPlayer pl = GWSiteClassLibrary.Factory.CreatePlayerService();
        DataSet ds = pl.FindByName(user, pass, "UserID", uid.ToString());
        GridView1.DataSource = ds;
        GridView1.DataBind();
        int plid = int.Parse(GridView1.Rows[0].Cells[0].Text);

        GWSiteClassLibrary.IScore score = GWSiteClassLibrary.Factory.CreateScoreService();
        DataSet ds3 = score.GetPlayerRank(user, pass, idmap, plid, 1000, "DESC");// .GetAll(user, pass);
        GridView2.DataSource = ds3;
        GridView2.DataBind();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void gvmaps_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false; //esconde ID
    }
    protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[2].Text = "Pontuação";
        e.Row.Cells[3].Text = "Data";
        e.Row.Cells[0].Visible = false;
        e.Row.Cells[1].Visible = false;

    }
}
