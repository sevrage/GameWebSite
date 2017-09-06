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

            GWSiteClassLibrary.IMap m = GWSiteClassLibrary.Factory.CreateMapService();
            DataSet ds = m.GetAll(user, pass);
            ddlMapas.DataSource = ds;
            ddlMapas.DataTextField = "Descr";
            ddlMapas.DataValueField = "MapID";
            ddlMapas.DataSource = ds;
            ddlMapas.DataBind();
            HyperLink1.Visible = false;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string user = (string)Session["userName"];
        string pass = (string)Session["passWord"];

        int idmap = int.Parse(ddlMapas.SelectedValue);
        GWSiteClassLibrary.IMap m = GWSiteClassLibrary.Factory.CreateMapService();
        DataSet dsmap = m.GetByID(user, pass, idmap);
        GridView1.DataSource = dsmap;
        GridView1.DataBind();
        //Label2.Text = idmap.ToString();
        HyperLink1.Visible = true;
        HyperLink1.NavigateUrl = "~/jogo" + idmap + ".rar";
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].Visible = false;
    }
}
