using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Text.RegularExpressions;

public partial class zadmin_PesquisaDB : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        GridView1.DataSource = getDataSet();
        GridView1.DataBind();
    }

    protected DataSet getDataSet()
    {
        string user = (string)Session["userName"];
        string pass = (string)Session["passWord"];
        GWSiteClassLibrary.IUser u = GWSiteClassLibrary.Factory.CreateUserService();
        DataSet ds = u.SearchWord(user, pass, TextBox1.Text);
        GridView1.DataSource = ds;
        return ds;
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataSet ds = new DataSet();
        ds = getDataSet();
        GridView1.DataSource = ds;
        GridView1.PageIndex = e.NewPageIndex;
        this.GridView1.DataBind();
    }

    protected string SortField
    {
        get{return (string)ViewState["_sortField"];}
        set{ViewState["_sortField"] = value;}
    }
    protected string SortDir
    {
        get{return (string)ViewState["_sortDir"];}
        set{ViewState["_sortDir"] = value;}
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
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

        GridView1.DataSource = dt.DefaultView;
        GridView1.DataBind();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string user = (string)Session["userName"];
        string pass = (string)Session["passWord"];

        String x = GridView1.SelectedRow.Cells[1].Text;
        string y = GridView1.SelectedRow.Cells[2].Text;

        string v = x.Replace("[", "");
        v = v.Replace("]", "");
        v = v.Replace(".", " ");

        //Label2.Text = v;

        string[] words = v.Split(' ');

        GWSiteClassLibrary.IUser f = GWSiteClassLibrary.Factory.CreateUserService();
        DataSet ds = f.FindInDB(user, pass, words[1], words[2], y);
        GridView2.DataSource = ds;
        GridView2.DataBind();

    }
}
