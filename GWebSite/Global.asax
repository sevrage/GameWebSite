<%@ Application Language="C#" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Security.Principal" %>
<%@ Import Namespace="System.Diagnostics" %>
<%@ Import Namespace="System.Web.SessionState" %>

<script runat="server">
    
    void Application_PreRequestHandlerExecute(object src, EventArgs e)
    {
        Page p = this.Context.Handler as Page;
        if (p != null)
        {
            p.PreInit += new EventHandler(page_PreInit);
        }
    }

    void page_PreInit(object sender, EventArgs e)
    {
        Page p = this.Context.Handler as Page;
        if (p != null)
        {
            if (Session["Theme"] == "Default" || Session["Theme"] == "Inverted")
                p.Theme = Session["Theme"].ToString();
            else
                p.Theme = "Default";
        }
    }

    protected void Application_AuthenticateRequest(Object sender, EventArgs e)
    {
        String roles = null;
        FormsIdentity identity = null;

        if (Context.Request.IsAuthenticated)
        {
            if (Context.User.Identity.AuthenticationType == "Forms")
            {
                // get the comma delimited list of roles from the user data
                // in the authentication ticket
                identity = (FormsIdentity)(Context.User.Identity);
                roles = identity.Ticket.UserData;

                // create a new user principal object with the current user identity
                // and the roles assigned to the user
                Context.User = new GenericPrincipal(identity, roles.Split(','));
            }
            else
            {
                // application is improperly configured so throw an exception
                throw new ApplicationException("A Aplicação deve ser configurada para Forms Authentication");
            }  // if (Context.User.Identity.AuthenticationType = "Forms")
        }  // if (Context.Request.IsAuthenticated)
    }  // Application_AuthenticateRequest
    
    
    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started
        //Session["Theme"] = "Bright";

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

        FormsAuthentication.SignOut();
        Session.Abandon();
        Response.Redirect("Default.aspx", true);

    }
       
</script>
