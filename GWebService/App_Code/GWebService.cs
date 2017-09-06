using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for GWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class GWebService : System.Web.Services.WebService
{
    private static string ConnectionString;
    public GWebService()
    {
        ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConn"].ConnectionString;
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    private static string CONN
    {
        get { return ConnectionString; }
    }

    [WebMethod(Description = "Online Status")]
    public string Alive()
    {
        return "I´m Alive";
    }

    [WebMethod(Description = "Registo de resultados obtidos num jogo.", EnableSession = false)]
    public string SetScore(string UserName, string Pass, int PlayerID, int MapID, int Score, string Date)
    {
        string status = "OK";


        if (PlayerID.ToString() == null || PlayerID.ToString().Trim().Length == 0)
        {
            status = "INVALID PlayerID";
            return status;
        }
        else if (MapID.ToString() == null || MapID.ToString().Trim().Length == 0)
        {
            status = "INVALID MapID";
            return status;
        }
        else if (Score.ToString() == null || Score.ToString().Trim().Length == 0)
        {
            status = "INVALID Score";
            return status;
        }
        else if (Date == null || Date.Trim().Length == 0)
        {
            status = "INVALID Date";
            return status;
        }

        SqlConnection conn = null;

        try
        {
            //
            // criar objecto de conexão à base de dados e abrir a conexão
            //
            conn = new SqlConnection(CONN);
            conn.Open();

            //
            // validar o utilizador
            //
            status = ValidateUser(conn, null, UserName, Pass);
            if (status != "OK")
                return status;

            //
            // criar comando SQL a executar
            //

            string sqlCmd = "INSERT INTO Scores (PlayerID, MapID, Score, Date) " +
                            "VALUES (@PlayerID, @MapID, @Score, @Date)";
            SqlCommand cmd = new SqlCommand(sqlCmd, conn);
            cmd.Parameters.AddWithValue("PlayerID", PlayerID);
            cmd.Parameters.AddWithValue("MapID", MapID);
            cmd.Parameters.AddWithValue("Score", Score);
            cmd.Parameters.AddWithValue("Date", Date);

            //
            // executar o comando 
            //
            int insRows = cmd.ExecuteNonQuery();

            //
            // verificar o resultado
            //
            if (insRows == 0)
                // não inseriu registo
                status = "NOT_OK";
            else
            {
                status = "OK";
            }
        }
        catch (SqlException ex)
        {
            //
            // tratar a excepção!!!!
            //
            System.Console.WriteLine("EXCEPTION : " + ex.Message);
            status = "ERROR";
        }
        finally
        {
            //
            // fechar a conexão
            //
            if (conn != null && conn.State == ConnectionState.Open)
                conn.Close();
        }

        return status;
    }

    /*
        private static string ValidateUser(string username, string pass)
        {
            string status = "OK";

            SqlConnection conn = null;

            try
            {
                //
                // criar objecto de conexão à base de dados e abrir a conexão
                //
                conn = new SqlConnection(CONN);
                conn.Open();

                //
                // invocar método auxiliar
                //
                status = ValidateUser(conn, null, username, pass);
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                //
                System.Console.WriteLine("EXCEPTION in 'User' Validation: " + ex);
                status = "ERROR";
            }
            finally
            {
                //
                // fechar a conexão
                //
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return status;
        }
    */
    private static string ValidateUser(SqlConnection conn, SqlTransaction tx, string username, string pass)
    {
        string status = "OK";
        SqlCommand cmd = null;

        try
        {
            //
            // criar comando SQL a executar
            //
            cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE UserName='" + username + "' AND Pass='" + pass + "'", conn);
            cmd.Transaction = tx;

            //
            // count(*) e ExecuteScalar()
            //
            int count = (int)cmd.ExecuteScalar();

            if (count == 1)
                status = "OK";
            else
                status = "INVALID_LOGIN";
        }
        catch (SqlException ex)
        {
            //
            // tratar a excepção!!!!
            //
            System.Console.WriteLine("EXCEPTION in 'User' Validation: " + ex);
            status = "ERROR";
        }

        return status;
    }

}

