using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace GWSiteClassLibrary
{

    internal class DataBase
    {
        private static readonly string ConnectionString;
        
        static DataBase()
        {
            ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConn"].ConnectionString;
        }

        internal static string CONN
        {
            get { return ConnectionString; }
        }

        public static DataSet SearchAllTables(string SearchStr)
        {
            SqlConnection conn = new SqlConnection(CONN);
            DataSet ds = null;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SearchAllTables", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SearchStr", SearchStr);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds, "SearchAllTables");
            }
            catch (SqlException ex)
            {
                System.Console.WriteLine("EXCEPTION Method<SearchAllTables> Class'DataBase': " + ex);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return ds;
        }

        #region ValidateUser
        /// <summary>
        /// Valida um login/password no sistema
        /// </summary>
        /// <param name="User">username de login na aplicação</param>
        /// <param name="Pass">password de login na aplicação</param>
        /// <returns></returns>
        /*
        public override bool ValidateUser(string username, string password)
        {
            return true;
        } 
        */
        public static GWSiteStatusEnum ValidateUser(string username, string pass)
        {
            GWSiteStatusEnum status = GWSiteStatusEnum.OK;

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
                status = GWSiteStatusEnum.ERROR;
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
        public static GWSiteStatusEnum ValidateUser(SqlConnection conn, SqlTransaction tx, string username, string pass)
        {
            GWSiteStatusEnum status = GWSiteStatusEnum.OK;
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
                    status = GWSiteStatusEnum.OK;
                else
                    status = GWSiteStatusEnum.INVALID_LOGIN;
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                //
                System.Console.WriteLine("EXCEPTION in 'User' Validation: " + ex);
                status = GWSiteStatusEnum.ERROR;
            }

            return status;
        }

        #endregion
        /*
        //-------------------------
        public static string AddJogador(string nome, int idade, string morada)
        {
            SqlConnection con = new SqlConnection(CONN);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("addJogador", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nome", nome);
                cmd.Parameters.AddWithValue("@idade", idade);
                cmd.Parameters.AddWithValue("@morada", morada);
                cmd.ExecuteNonQuery();
                return null; // success 
            }
            catch (Exception ex)
            {
                return ex.Message;  // return error message
            }
            finally
            {
                con.Close();
            }
        }
        public static string DeleteUser(int key)
        {
            SqlConnection con = new SqlConnection(CONN);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("deleteJogador", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@jid", jid);
                cmd.ExecuteNonQuery();
                return null; // success 
            }
            catch (Exception ex)
            {
                return ex.Message;  // return error message
            }
            finally
            {
                con.Close();
            }
        }
        public static string UpdateJogador(int jid, string nome, int idade, string morada)
        {
            SqlConnection con = new SqlConnection(CONN);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("updateJogador", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@jid", jid);
                cmd.Parameters.AddWithValue("@nome", nome);
                cmd.Parameters.AddWithValue("@idade", idade);
                cmd.Parameters.AddWithValue("@morada", morada);
                cmd.ExecuteNonQuery();
                return null; // success 
            }
            catch (Exception ex)
            {
                return ex.Message;  // return error message
            }
            finally
            {
                con.Close();
            }
        }


        //------------------------------------------------------
        */
        #region NoReturn

        public static GWSiteStatusEnum UpdateByID(string table, int key, string keyName, string column, string value)
        {

            GWSiteStatusEnum status = GWSiteStatusEnum.OK;
            SqlConnection conn = null;

            try
            {
                //
                // criar objecto de conexão à base de dados e abrir a conexão
                //
                conn = new SqlConnection(DataBase.CONN);
                conn.Open();

                //
                // criar comando SQL a executar
                //
                                // update Users Set Pass='jakinas' WHERE UserID=30
                                //UpdateByID(table, key, keyName, column, value)
                                //UpdateByID("Maps", MapID, "MapID", "Descr", Descr);
                string sSqlCmd = "UPDATE " + table + " SET "+column+"="+"'"+value+"'"+ " WHERE " + keyName + " = " + key.ToString();
                SqlCommand cmd = new SqlCommand(sSqlCmd, conn);
                //
                // executar o comando 
                //
                int insRows = cmd.ExecuteNonQuery();
                

                //
                // verificar o resultado
                //
                if (insRows == 0)
                    // não inseriu registo
                    status = GWSiteStatusEnum.NOT_OK;
                else
                {
                    //
                    // inseriu registo
                    // 
                    status = GWSiteStatusEnum.OK;
                }
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                //
                System.Console.WriteLine("EXCEPTION Method<UpdateByID 1> Class'DataBase': " + ex.Message);
                status = GWSiteStatusEnum.ERROR;
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
        public static GWSiteStatusEnum UpdateByID(string table, int? key, string keyName, string[] column , string[] value)
        {

            GWSiteStatusEnum status = GWSiteStatusEnum.OK;
            SqlConnection conn = null;

            try
            {
                //
                // criar objecto de conexão à base de dados e abrir a conexão
                //
                conn = new SqlConnection(DataBase.CONN);
                conn.Open();

                //
                // criar comando SQL a executar
                //
                System.Text.StringBuilder sb = new System.Text.StringBuilder("UPDATE " + table + " SET ");
                for (int i = 0; i < value.Length - 1; i++)
                {
                    sb.Append(column[i].ToString());
                    sb.Append("='");
                    sb.Append(value[i].ToString());
                    sb.Append("',");
                }
                sb.Length = sb.Length - 1;
                sb.Append(" WHERE " + keyName + "='");
                sb.Append(key);
                sb.Append("'");
                string sSqlCmd = sb.ToString();
                SqlCommand cmd = new SqlCommand(sSqlCmd, conn);
                System.Console.WriteLine("STRING: "+sSqlCmd);
                //
                // executar o comando 
                //
                int insRows = cmd.ExecuteNonQuery();

                //
                // verificar o resultado
                //
                if (insRows == 0)
                    // não inseriu registo
                    status = GWSiteStatusEnum.NOT_OK;
                else
                {
                    //
                    // inseriu registo
                    // 
                    status = GWSiteStatusEnum.OK;
                }
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                //
                System.Console.WriteLine("EXCEPTION Method<UpdateByID 2> Class'DataBase': " + ex.Message);
                status = GWSiteStatusEnum.ERROR;
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

        #endregion

        public static int GetUserID(string table, string keyName)
        {
            int ret = -1;
            SqlConnection conn = null;

            try
            {

                //
                // criar objecto de conexão à base de dados e abrir a conexão
                //
                conn = new SqlConnection(CONN);
                conn.Open();

                //
                // criar um DataAdapter para executar o comando e devolver o dataset
                //select Role from Roles where UserID=3
                string sSqlCmd = "SELECT  UserID FROM " + table + " WHERE  UserName LIKE '"+ keyName +"'";
                SqlCommand cmd = new SqlCommand(sSqlCmd, conn);
                //
                // executar o comando 
                //
                ret = Int32.Parse(cmd.ExecuteScalar().ToString());
                //
                // receber o rolo
                //
                if (ret.ToString() == null || ret.ToString().Trim().Length == 0)
                    ret = -1;

            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                //
                System.Console.WriteLine("EXCEPTION Method<GetRoleByID> Class'DataBase': " + ex.Message);
                ret = -1;
            }
            finally
            {
                //
                // fechar a conexão
                //
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return ret;
        }
        public static int GetPlayerID(string table, string keyName)
        {
            int ret = -1;
            SqlConnection conn = null;

            try
            {

                //
                // criar objecto de conexão à base de dados e abrir a conexão
                //
                conn = new SqlConnection(CONN);
                conn.Open();

                //
                // criar um DataAdapter para executar o comando e devolver o dataset
                //select Role from Roles where UserID=3
                string sSqlCmd = "SELECT  PlayerID FROM " + table + " WHERE  UserName LIKE '" + keyName + "'";
                SqlCommand cmd = new SqlCommand(sSqlCmd, conn);
                //
                // executar o comando 
                //
                ret = Int32.Parse(cmd.ExecuteScalar().ToString());
                //
                // receber o rolo
                //
                if (ret.ToString() == null || ret.ToString().Trim().Length == 0)
                    ret = -1;

            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                //
                System.Console.WriteLine("EXCEPTION Method<GetRoleByID> Class'DataBase': " + ex.Message);
                ret = -1;
            }
            finally
            {
                //
                // fechar a conexão
                //
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return ret;
        }
        public static string GetRoleByID(string table, int key)
        {
            string ret = "User";
            SqlConnection conn = null;

            try
            {

                //
                // criar objecto de conexão à base de dados e abrir a conexão
                //
                conn = new SqlConnection(CONN);
                conn.Open();

                //
                // criar um DataAdapter para executar o comando e devolver o dataset
                //select Role from Roles where UserID=3
                string sSqlCmd = "SELECT  Role FROM " + table + " WHERE  UserID  = " + key.ToString();
                SqlCommand cmd = new SqlCommand(sSqlCmd, conn);
                //
                // executar o comando e verificar o Rolo
                //
                int insRows = cmd.ExecuteNonQuery();

                if (insRows == 0)
                    ret = "User";
                else
                {
                    ret = (string)cmd.ExecuteScalar();
                }
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                //
                System.Console.WriteLine("EXCEPTION Method<GetRoleByID> Class'DataBase': " + ex.Message);
                ret = "User";
            }
            finally
            {
                //
                // fechar a conexão
                //
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return ret;
        }

        #region GetByID
        /// <summary>
        /// Obtem um dataset resultante de uma pesquisa por chave
        /// </summary>
        /// <param name="table">tabela a pesquisar</param>
        /// <param name="field">campo chave a usar</param>
        /// <param name="key">valor da chave a pesquisar</param>
        /// <returns>um dataset do tipo pedido</returns>
        public static DataSet GetByID(string table, string keyName, int key)
        {
            DataSet ds = null;
            SqlConnection conn = null;

            try
            {
                //
                // criar objecto DataSet
                //
                ds = new DataSet();

                //
                // criar objecto de conexão à base de dados e abrir a conexão
                //
                conn = new SqlConnection(CONN);
                conn.Open();

                //
                // criar um DataAdapter para executar o comando e devolver o dataset
                //
                string sSqlCmd = "SELECT * FROM " + table + " WHERE " + keyName + " = " + key.ToString();
                SqlDataAdapter da = new SqlDataAdapter(sSqlCmd, conn);

                //
                // executar o comando e preencher um DataSet
                //
                da.Fill(ds, table);
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                //
                System.Console.WriteLine("EXCEPTION Method<GetByID 1> Class'DataBase': " + ex.Message);
                ds = null;
            }
            finally
            {
                //
                // fechar a conexão
                //
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return ds;
        }

        //select distinct country from  players 
        public static DataSet GetDistinct(string table, string keyName)
        {
            DataSet ds = null;
            SqlConnection conn = null;

            try
            {
                //
                // criar objecto DataSet
                //
                ds = new DataSet();

                //
                // criar objecto de conexão à base de dados e abrir a conexão
                //
                conn = new SqlConnection(CONN);
                conn.Open();

                //
                // criar um DataAdapter para executar o comando e devolver o dataset
                //
                string sSqlCmd = "SELECT DISTINCT "+keyName+" FROM " + table;
                SqlDataAdapter da = new SqlDataAdapter(sSqlCmd, conn);

                //
                // executar o comando e preencher um DataSet
                //
                da.Fill(ds, table);
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                //
                System.Console.WriteLine("EXCEPTION Method<GetDistinct> Class'DataBase': " + ex.Message);
                ds = null;
            }
            finally
            {
                //
                // fechar a conexão
                //
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return ds;
        }

        public static GWSiteStatusEnum DeleteByID(string table, int key)
        {
            GWSiteStatusEnum status = GWSiteStatusEnum.OK;
            SqlConnection conn = null;

            try
            {
                //
                // criar objecto de conexão à base de dados e abrir a conexão
                //
                conn = new SqlConnection(DataBase.CONN);
                conn.Open();
                string sSqlCmd = "";
                //
                // criar comando SQL a executar
                //
                if (table == "Maps")
                {
                    sSqlCmd = "DELETE FROM Scores WHERE MapID=" + key.ToString();
                    SqlCommand cmd = new SqlCommand(sSqlCmd, conn);
                    int insRows = cmd.ExecuteNonQuery();
                    if (insRows == 0)
                        status = GWSiteStatusEnum.NOT_OK;
                    sSqlCmd = "DELETE FROM Maps WHERE MapID=" + key.ToString();
                    cmd = new SqlCommand(sSqlCmd, conn);
                    insRows = cmd.ExecuteNonQuery();
                    if (insRows == 0)
                        status = GWSiteStatusEnum.NOT_OK;

                    else
                    {
                        status = GWSiteStatusEnum.OK;
                    }                
                }
                else if (table == "Users")
                {
                    //DELETE FROM Scores WHERE PlayerID=(select PlayerID FROM Players WHERE UserID=30)
                    sSqlCmd = "DELETE FROM Scores WHERE PlayerID=(SELECT PlayerID FROM Players WHERE UserID=" + key.ToString()+")";
                    SqlCommand cmd = new SqlCommand(sSqlCmd, conn);
                    int insRows = cmd.ExecuteNonQuery();
                    if (insRows == 0)
                        status = GWSiteStatusEnum.NOT_OK;

                    sSqlCmd = "DELETE FROM Players WHERE UserID=" + key.ToString();
                    cmd = new SqlCommand(sSqlCmd, conn);
                    insRows = cmd.ExecuteNonQuery();
                    if (insRows == 0)
                        status = GWSiteStatusEnum.NOT_OK;

                    sSqlCmd = "DELETE FROM Users WHERE UserID=" + key.ToString();
                    cmd = new SqlCommand(sSqlCmd, conn);
                    insRows = cmd.ExecuteNonQuery();
                    if (insRows == 0)
                        status = GWSiteStatusEnum.NOT_OK;
                    else
                    {
                        status = GWSiteStatusEnum.OK;
                    }  

                }

            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                //
                System.Console.WriteLine("EXCEPTION Method<AddNew 1> Class'DataBase': " + ex.Message);
                status = GWSiteStatusEnum.ERROR;
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
        public static GWSiteStatusEnum AddNew(string table, string keyName)
        {
            GWSiteStatusEnum status = GWSiteStatusEnum.OK;
            SqlConnection conn = null;
            SqlTransaction tn = null;

            try
            {
                //
                // criar objecto de conexão à base de dados e abrir a conexão
                //
                conn = new SqlConnection(DataBase.CONN);
                conn.Open();
                tn = conn.BeginTransaction();

                //
                // criar comando SQL a executar
                //

                string sSqlCmd = "INSERT INTO " + table + " VALUES " + "('" + keyName + "')";
                SqlCommand cmd = new SqlCommand(sSqlCmd, conn);
                //
                // executar o comando 
                //
                cmd.Transaction = tn;
                int insRows = cmd.ExecuteNonQuery();

                //
                // verificar o resultado
                //
                if (insRows == 0)
                    // não inseriu registo
                    status = GWSiteStatusEnum.NOT_OK;
                else
                {
                    //
                    // inseriu registo
                    // 
                    status = GWSiteStatusEnum.OK;

                }
                tn.Commit();
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                //
                if (tn != null)
                    tn.Rollback();
                System.Console.WriteLine("EXCEPTION Method<AddNew 1> Class'DataBase': " + ex.Message);
                status = GWSiteStatusEnum.ERROR;
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
        public static GWSiteStatusEnum AddNew(string table, string keyName)
        {
                GWSiteStatusEnum status = GWSiteStatusEnum.OK;
                SqlConnection conn = null;

                try
                {
                    //
                    // criar objecto de conexão à base de dados e abrir a conexão
                    //
                    conn = new SqlConnection(DataBase.CONN);
                    conn.Open();

                    //
                    // criar comando SQL a executar
                    //
                    //INSERT INTO table_name
                    //VALUES (value1, value2, value3,...)

                    string sSqlCmd = "INSERT INTO " + table + " VALUES " +"('"+keyName+"')";
                    SqlCommand cmd = new SqlCommand(sSqlCmd, conn);
                    //
                    // executar o comando 
                    //
                    int insRows = cmd.ExecuteNonQuery();

                    //
                    // verificar o resultado
                    //
                    if (insRows == 0)
                        // não inseriu registo
                        status = GWSiteStatusEnum.NOT_OK;
                    else
                    {
                        //
                        // inseriu registo
                        // 
                        status = GWSiteStatusEnum.OK;
                    }
                }
                catch (SqlException ex)
                {
                    //
                    // tratar a excepção!!!!
                    //
                    System.Console.WriteLine("EXCEPTION Method<AddNew 1> Class'DataBase': " + ex.Message);
                    status = GWSiteStatusEnum.ERROR;
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
        */
        public static GWSiteStatusEnum AddNew(string table, string[] value)
        {
            GWSiteStatusEnum status = GWSiteStatusEnum.OK;
            SqlConnection conn = null;

            try
            {
                //
                // criar objecto de conexão à base de dados e abrir a conexão
                //
                conn = new SqlConnection(DataBase.CONN);
                conn.Open();

                //
                // criar comando SQL a executar
                //
                //INSERT INTO table_name
                //VALUES (value1, value2, value3,...)

                System.Text.StringBuilder sb = new System.Text.StringBuilder("INSERT INTO " + table + " VALUES (");
                foreach (string k in value)
                {
                    sb.Append("'"+k.ToString()+"'");
                    sb.Append(",");
                }
                sb.Length = sb.Length - 1;
                sb.Append(")");
                string sSqlCmd = sb.ToString();
                SqlCommand cmd = new SqlCommand(sSqlCmd, conn);
                //
                // executar o comando 
                //
                int insRows = cmd.ExecuteNonQuery();

                //
                // verificar o resultado
                //
                if (insRows == 0)
                    // não inseriu registo
                    status = GWSiteStatusEnum.NOT_OK;
                else
                {
                    //
                    // inseriu registo
                    // 
                    status = GWSiteStatusEnum.OK;
                }
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                //
                System.Console.WriteLine("EXCEPTION Method<AddNew 2> Class'DataBase': " + ex.Message);
                status = GWSiteStatusEnum.ERROR;
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


        /// <summary>
        /// Obtem um dataset resultante de uma pesquisa por chave
        /// </summary>
        /// <param name="conn">a conexão a utilizar</param>
        /// <param name="table">tabela a pesquisar</param>
        /// <param name="keyName">campo chave a usar</param>
        /// <param name="key">valor da chave a pesquisar</param>
        /// <returns>um dataset do tipo pedido</returns>
        public static DataSet GetByID(SqlConnection conn, SqlTransaction tx, string table, string keyName, int key)
        {
            DataSet ds = null;

            try
            {
                //
                // criar objecto DataSet
                //
                ds = new DataSet();

                //
                // criar um Datadapter para executar o comando e devolver o dataset
                //
                string sSqlCmd = "SELECT * FROM " + table + " WHERE " + keyName + " = " + key.ToString();
                SqlDataAdapter da = new SqlDataAdapter(sSqlCmd, conn);
                da.SelectCommand.Transaction = tx;

                //
                // executar o comando e preencher um Dataset
                //
                da.Fill(ds, table);
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                //
                System.Console.WriteLine("EXCEPTION Method<GetByID 2> Class'DataBase': " + ex.Message);
                ds = null;
            }

            return ds;
        }
        /// <summary>
        /// Obtem um dataset resultante de uma pesquisa por chave
        /// </summary>
        /// <param name="table">tabela a pesquisar</param>
        /// <param name="field">campo chave a usar</param>
        /// <param name="key">valor da chave a pesquisar</param>
        /// <returns>um dataset do tipo pedido</returns>
        public static DataSet GetByID(SqlConnection conn, SqlTransaction tx, string table, string keyName, int[] keys)
        {
            DataSet ds = null;

            try
            {
                //
                // criar objecto DataSet
                //
                ds = new DataSet();

                //
                // criar um Datadapter para executar o comando e devolver o dataset
                //
                System.Text.StringBuilder sb = new System.Text.StringBuilder("SELECT * FROM " + table + " WHERE " + keyName + " IN (");
                foreach (int k in keys)
                {
                    sb.Append(k.ToString());
                    sb.Append(",");
                }
                sb.Length = sb.Length - 1;
                sb.Append(")");
                string sSqlCmd = sb.ToString();

                SqlDataAdapter da = new SqlDataAdapter(sSqlCmd, conn);
                da.SelectCommand.Transaction = tx;

                //
                // executar o comando e preencher um Dataset
                //
                da.Fill(ds, table);
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                //
                System.Console.WriteLine("EXCEPTION Method<GetByID 3> Class'DataBase': " + ex.Message);
                ds = null;
            }

            return ds;
        }
        public static DataSet GetAllFROMTable(SqlConnection conn, SqlTransaction tx, string table)
        {

            DataSet ds = null;

            try
            {
                //
                // criar objecto DataSet
                //
                ds = new DataSet();

                //
                // criar um DataAdapter para executar o comando e devolver o dataset
                //
                string sSqlCmd = "SELECT * FROM " + table;
                SqlDataAdapter da = new SqlDataAdapter(sSqlCmd, conn);
                da.SelectCommand.Transaction = tx;

                //
                // executar o comando e preencher um Dataset
                //
                da.Fill(ds, table);
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                //
                System.Console.WriteLine("EXCEPTION Method<GetAllFROMTable> Class'DataBase': " + ex.Message);
                ds = null;
            }

            return ds;
        }
        #endregion

        #region FindByStringField
        /// <summary>
        /// Obtem um dataset resultante de uma pesquisa por chave
        /// </summary>
        /// <param name="table">tabela a pesquisar</param>
        /// <param name="field">campo chave a usar</param>
        /// <param name="key">valor da chave a pesquisar</param>
        /// <returns>um dataset do tipo pedido</returns>
        public static DataSet FindByStringField(string table, string field, string key)
        {
            DataSet ds = null;
            SqlConnection conn = null;

            try
            {

                ds = new DataSet();

                conn = new SqlConnection(CONN);

                //
                // invocar método auxiliar
                //
                ds = FindByStringField(conn, table, field, key);
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                //
                System.Console.WriteLine("EXCEPTION Method<FindByStringField 1> Class'DataBase': " + ex);
            }
            finally
            {
                //
                // fechar a conexão
                //
                if (conn != null && conn.State != ConnectionState.Closed)
                    conn.Close();
            }

            //
            // retornar valor
            //
            return ds;
        }
        /// <summary>
        /// Obtem um dataset resultante de uma pesquisa por chave
        /// </summary>
        /// <param name="table">tabela a pesquisar</param>
        /// <param name="field">campo chave a usar</param>
        /// <param name="key">valor da chave a pesquisar</param>
        /// <returns>um dataset do tipo pedido</returns>
        public static DataSet FindByStringField(SqlConnection conn, string table, string field, string key)
        {

            DataSet ds = null;

            try
            {
                ds = new DataSet();
                //
                // criar um Datadapter para executar o comando e devolver o dataset
                // SELECT * FROM Players WHERE Country LIKE 'Portugal'
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM " + table + " WHERE " + field + " LIKE '" + key + "'", conn);

                //
                // executar o comando e preencher um Dataset
                //
                da.Fill(ds, table);
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                //
                System.Console.WriteLine("EXCEPTION Method<FindByStringField 2> Class'DataBase': " + ex);
            }

            //
            // retornar valor
            //
            return ds;
        }
        #endregion

        #region GetAllByTable
        public static DataSet GetAllByTable(SqlConnection conn, string table)
        {
            DataSet ds = null;

            try
            {
                ds = new DataSet();
                //
                // criar um Datadapter para executar o comando e devolver o dataset
                //
                string sSqlCmd = "SELECT * FROM " + table;
                SqlDataAdapter da = new SqlDataAdapter(sSqlCmd, conn);

                //
                // executar o comando e preencher um Dataset
                //
                da.Fill(ds, table);
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                //
                System.Console.WriteLine("EXCEPTION Method<GetAllByTable> Class'DataBase': " + ex);
            }

            //
            // retornar valor
            //
            return ds;
        }
        #endregion

        /// <summary>
        /// Obtem o Rank
        /// </summary>
        /// <param name="table">tabela a pesquisar</param>
        /// <param name="key">valor da chave a pesquisar</param>
        /// <param name="total">total de resultados a devolver</param>
        /// <param name="order">por ordem: DESC ou ASC</param>
        /// <returns>um dataset do tipo pedido</returns>
        #region GetRank 
        public static DataSet GetRank(string table, int key, int retLines, string orderType)
        {
            DataSet ds = null;
            SqlConnection conn = null;

            try
            {
                //
                // criar objecto DataSet
                //
                ds = new DataSet();

                //
                // criar objecto de conexão à base de dados e abrir a conexão
                //
                conn = new SqlConnection(CONN);
                conn.Open();

                //
                // criar um DataAdapter para executar o comando e devolver o dataset
                // 
                /*
                               SELECT TOP 3 Players.PlayerID, Users.UserName, Scores.Score
               FROM Scores, Users, Players
               WHERE Scores.PlayerID=Players.PlayerID AND Players.UserID=Users.UserID AND Scores.MapID=2
               ORDER BY Score DESC
                   */
                string sSqlCmd = "SELECT TOP " + retLines.ToString() + " Maps.Descr, Users.UserName, Scores.Score, Scores.Date" +
                                 " FROM Scores, Users, Players, Maps" +
                                 " WHERE Scores.MapID=Maps.MapID AND Scores.PlayerID=Players.PlayerID AND Players.UserID=Users.UserID AND Scores.MapID=" + key.ToString() + 
                                 " ORDER BY Score " + orderType ;

                SqlDataAdapter da = new SqlDataAdapter(sSqlCmd, conn);

                //
                // executar o comando e preencher um DataSet
                //
                da.Fill(ds, table);
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                //
                System.Console.WriteLine("EXCEPTION Method<GetRank> Class'DataBase': " + ex.Message);
                ds = null;
            }
            finally
            {
                //
                // fechar a conexão
                //
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return ds;

        }
        public static DataSet GetPlayerRank(string table, int key, int player, int retLines, string orderType)
        {
            DataSet ds = null;
            SqlConnection conn = null;

            try
            {
                //
                // criar objecto DataSet
                //
                ds = new DataSet();

                //
                // criar objecto de conexão à base de dados e abrir a conexão
                //
                conn = new SqlConnection(CONN);
                conn.Open();

                //
                // criar um DataAdapter para executar o comando e devolver o dataset
                // 

                /*
                                    SELECT TOP 2 Maps.Descr, Users.UserName, Scores.Score, Scores.Date
                                    FROM Scores, Users, Players, Maps
                                    WHERE Scores.MapID=Maps.MapID AND Scores.PlayerID=Players.PlayerID AND Players.UserID=Users.UserID AND Scores.MapID=1
                                    AND Players.PlayerID=3
                                    ORDER BY Score DESC; 
                 * */

                string sSqlCmd = "SELECT TOP " + retLines.ToString() + "Maps.Descr, Users.UserName, Scores.Score, Scores.Date" +
                                 " FROM Scores, Users, Players, Maps" +
                                 " WHERE Scores.MapID=Maps.MapID AND Scores.PlayerID=Players.PlayerID AND Players.UserID=Users.UserID AND Scores.MapID=" + key.ToString() +
                                 " AND Players.PlayerID=" + player +
                                 " ORDER BY Score " + orderType;

                SqlDataAdapter da = new SqlDataAdapter(sSqlCmd, conn);

                //
                // executar o comando e preencher um DataSet
                //
                da.Fill(ds, table);
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                //
                System.Console.WriteLine("EXCEPTION Method<GetPlayerRank> Class'DataBase': " + ex.Message);
                ds = null;
            }
            finally
            {
                //
                // fechar a conexão
                //
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return ds;
        }
        #endregion

        public static DataSet GetAllHistory(string table, int key, string orderField, string orderType)
        {
            DataSet ds = null;
            SqlConnection conn = null;

            try
            {
                //
                // criar objecto DataSet
                //
                ds = new DataSet();

                //
                // criar objecto de conexão à base de dados e abrir a conexão
                //
                conn = new SqlConnection(CONN);
                conn.Open();

                //
                // criar um DataAdapter para executar o comando e devolver o dataset
                // 

                /*
                                    SELECT TOP 2 Maps.Descr, Users.UserName, Scores.Score, Scores.Date
                                    FROM Scores, Users, Players, Maps
                                    WHERE Scores.MapID=Maps.MapID AND Scores.PlayerID=Players.PlayerID AND Players.UserID=Users.UserID AND Scores.MapID=1
                                    AND Players.PlayerID=3
                                    ORDER BY Score DESC; 
                 * */

                string sSqlCmd = "SELECT Maps.Descr, Users.UserName, Scores.Score, Scores.Date" +
                                 " FROM Scores, Users, Players, Maps" +
                                 " WHERE Scores.MapID=Maps.MapID AND Scores.PlayerID=Players.PlayerID AND Players.UserID=Users.UserID" +
                                 " AND Players.PlayerID=" + key +
                                 " ORDER BY " + orderField + " " + orderType;

                SqlDataAdapter da = new SqlDataAdapter(sSqlCmd, conn);

                //
                // executar o comando e preencher um DataSet
                //
                da.Fill(ds, table);
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                //
                System.Console.WriteLine("EXCEPTION Method<GetPlayerRank> Class'DataBase': " + ex.Message);
                ds = null;
            }
            finally
            {
                //
                // fechar a conexão
                //
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return ds;
        }
    }
}
