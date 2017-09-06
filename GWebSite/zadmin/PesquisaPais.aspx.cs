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
            GWSiteClassLibrary.IPlayer p = GWSiteClassLibrary.Factory.CreatePlayerService();
            DataSet ds = p.GetDistinct(user, pass, "Country");
            DropDownList1.DataTextField = "Country";
            DropDownList1.DataValueField = "Country";
            DropDownList1.DataSource = ds;

            DropDownList1.DataBind();
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string user = (string)Session["userName"];
        string pass = (string)Session["passWord"];

        string pais = DropDownList1.SelectedValue;



        GWSiteClassLibrary.IPlayer p = GWSiteClassLibrary.Factory.CreatePlayerService();
        DataSet ds1 = p.FindByName(user, pass, "Country", pais);

        GridView1.DataSource = ds1;
        GridView1.DataBind();

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //Esconder colunas que nao interessam
        e.Row.Cells[0].Visible = false;//id
        e.Row.Cells[1].Visible = false;//id

    }
}
