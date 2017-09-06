using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Login1_Authenticate1(object sender, AuthenticateEventArgs e)
    {
        // Parametro querystring contendo o return URL
        const String QS_RETURN_URL = "ReturnURL";
        FormsAuthenticationTicket ticket = null;
        HttpCookie cookie = null;
        String encryptedStr = null;
        String nextPage = null;
        

        //Login l = (Login)LoginView1.Controls[0].Controls[1].FindControl("Login1");

        GWSiteClassLibrary.IUser us = GWSiteClassLibrary.Factory.CreateUserService();

        GWSiteClassLibrary.GWSiteStatusEnum tipo = us.Validate(Login1.UserName, Login1.Password);


        if (tipo.ToString() == "OK")
        {
            //variavel de sessao e na pagina admin vai ver s ta la o user senao reencaminha paki outra vez
            Session["userName"] = Login1.UserName;
            Session["passWord"] = Login1.Password;
            Session["UserID"] = us.GetUserID(Login1.UserName, Login1.Password);
            //GET PAGE THEME
            Session["Theme"] = "Bright";


            //GET ROLE
            //string urole = "User";
            string urole = us.GetUserRole(Login1.UserName, Login1.Password, Int32.Parse(Session["UserID"].ToString()));

            //Label1.Text = Session["UserID"].ToString();
            //Label2.Text = urole;
            //Label3.Text = Session["Theme"].ToString();
            ticket = new FormsAuthenticationTicket(1,
                                                   (String)(Login1.UserName),
                                                   DateTime.Now,
                                                   DateTime.Now.AddMinutes(30),
                                                   Login1.RememberMeSet,
                                                   urole);
            encryptedStr = FormsAuthentication.Encrypt(ticket);

            cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedStr);
            if (Login1.RememberMeSet)
            {
                cookie.Expires = ticket.IssueDate.AddYears(10);
            }
            Response.Cookies.Add(cookie);
            if (Request.QueryString[QS_RETURN_URL] != null)
            {
                // user attempted to access a page without logging in so redirect
                // them to their originally requested page
                nextPage = Request.QueryString[QS_RETURN_URL];
            }
            else
            {
                // user came straight to the login page so just send them to the
                // home page
                if (urole == "User")
                    nextPage = "~/zuser/User.aspx";
                else if (urole == "Admin")
                    nextPage = "~/zadmin/Admin.aspx";
                else
                    nextPage = "~/Default.aspx";
            }
            Response.Redirect(nextPage, true);
        }
        else
        {
            // user credentials do not exist in the database so output error
            // message indicating the problem
            Login1.FailureText = "Erro: Por favor verifique o UserName e PassWord.";
        }
    }
}
