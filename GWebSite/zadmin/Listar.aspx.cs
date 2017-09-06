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

            ////////////////Listagens Scores//////////////
            GridView5.DataSource = getDataSet();
            GridView5.DataBind();

            /*
            GWSiteClassLibrary.IScore sc = GWSiteClassLibrary.Factory.CreateScoreService();
            DataSet ds = null;
            ds = sc.GetAll(user, pass);
            GridView5.DataSource = ds;
            GridView5.DataBind();
             * */
            /*GWSiteClassLibrary.IUser ustest = GWSiteClassLibrary.Factory.CreateUserService();
            //DataSet ds = null;
            DataSet ds = ustest.FindByName(user, pass, username);
            GridView5.DataSource = ds;
            GridView5.DataBind();*/


            ////////////////Listagens Mapas//////////////
            GWSiteClassLibrary.IMap m = GWSiteClassLibrary.Factory.CreateMapService();
            DataSet dsP1 = null;
            dsP1 = m.GetAll(user, pass);
            GridView3.DataSource = dsP1;
            // GridView1.DataTextField = "Name";
            // GridView1.DataValueField = "PlayerID";
            GridView3.DataBind();

            ////////////////Listagens Users//////////////
            GWSiteClassLibrary.IUser u = GWSiteClassLibrary.Factory.CreateUserService();
            DataSet dsP2 = null;
            dsP2 = u.GetAll(user, pass);
            GridView2.DataSource = dsP2;
            // GridView1.DataTextField = "Name";
            // GridView1.DataValueField = "PlayerID";
            GridView2.DataBind();

            ////////////////Listagens Players//////////////
            GWSiteClassLibrary.IPlayer p = GWSiteClassLibrary.Factory.CreatePlayerService();
            DataSet dsP3 = null;
            dsP3 = p.GetAll(user, pass);
            GridView4.DataSource = dsP3;
            GridView4.DataBind();
        }

    }

    protected DataSet getDataSet()
    {
        string user = (string)Session["userName"];
        string pass = (string)Session["passWord"];
        GWSiteClassLibrary.IScore sc = GWSiteClassLibrary.Factory.CreateScoreService();
        DataSet ds = null;
        ds = sc.GetAll(user, pass);
        GridView5.DataSource = ds;
        return ds;
    }

    protected void GridView5_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        DataSet ds = new DataSet();
        ds = getDataSet();
        GridView5.DataSource = ds;
        GridView5.PageIndex = e.NewPageIndex;
        this.GridView5.DataBind();
    }

    protected string SortField
    {
        get { return (string)ViewState["_sortField"]; }
        set { ViewState["_sortField"] = value; }
    }
    protected string SortDir
    {
        get { return (string)ViewState["_sortDir"]; }
        set { ViewState["_sortDir"] = value; }
    }
    protected void GridView5_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (e.SortExpression == SortField && SortDir != "desc")
        {
            SortDir = "desc";
        }
        else
        {
            SortDir = "asc";
        }
        SortField = e.SortExpression;
        MakeBind();
    }

    protected void MakeBind()
    {
        DataSet ds = new DataSet();
        ds = getDataSet();
        DataTable dt = ds.Tables[0];
        dt.DefaultView.Sort = SortField + " " + SortDir;

        GridView5.DataSource = dt.DefaultView;
        GridView5.DataBind();
    }

}
