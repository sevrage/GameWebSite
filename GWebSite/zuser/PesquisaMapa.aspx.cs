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
            DropDownList1.DataTextField = "Descr";
            DropDownList1.DataValueField = "MapID";
            DropDownList1.DataSource = ds;
            DropDownList1.DataBind();
            Label2.Visible = false;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string user = (string)Session["userName"];
        string pass = (string)Session["passWord"];

        int id = int.Parse(DropDownList1.SelectedValue);

        GWSiteClassLibrary.IScore sc = GWSiteClassLibrary.Factory.CreateScoreService();
        DataSet ds1 = sc.GetMapScoreRank(user, pass, id, 10, "DESC");

        GridView1.DataSource = ds1;
        GridView1.DataBind();

        Label2.Visible = true;
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].Visible = false;
    }
}
