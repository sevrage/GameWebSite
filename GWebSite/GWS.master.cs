using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class GWS : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        btnLogOut.Visible = Request.IsAuthenticated;
    }

    protected void btnLogOut_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        Session.Abandon();
        Response.Redirect("~/Default.aspx", true);
        
    }
}
