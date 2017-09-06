using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GWSiteClassLibrary
{
    public interface IUser
    {
        DataSet GetAll(string UserName, string Pass);
        DataSet GetByID(string UserName, string Pass, int UserID);
        DataSet FindByName(string UserName, string Pass, string Column, string NamePattern);
        DataSet FindInDB(string UserName, string Pass, string table, string field, string NamePattern);
        GWSiteStatusEnum Validate(string username, string pass);

        int AddUser(string UserName, string Pass,
                out GWSiteStatusEnum status);

        DataSet SearchWord(string UserName, string Pass, string word);

        //TODO
        GWSiteStatusEnum UpdateUserPass(string UserName, string Pass, int UserID, string NewPass);
        //TODO
        GWSiteStatusEnum DeleteUser(string UserName, string Pass, int UserID);

        string GetUserRole(string UserName, string Pass, int UserID);
        int GetUserID(string UserName, string Pass);

    }

    internal class User : IUser
    {
        public User()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public DataSet SearchWord(string UserName, string Pass, string word) 
        {
            DataSet ds;
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(DataBase.CONN);
                conn.Open();
                GWSiteStatusEnum status = DataBase.ValidateUser(conn, null, UserName, Pass);
                if (status != GWSiteStatusEnum.OK)
                    return null;
                ds = DataBase.SearchAllTables(word);
                return ds;
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                System.Console.WriteLine("EXCEPTION Method'SearchWord' Class'Users': " + ex.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }       
        
        }


        public int GetUserID(string UserName, string Pass)
        {
            SqlConnection conn = null;
            int id = -1;

            try
            {
                //
                // criar objecto de conexão à base de dados e abrir a conexão
                //
                conn = new SqlConnection(DataBase.CONN);
                conn.Open();

                //
                // validar user
                //
                GWSiteStatusEnum status = DataBase.ValidateUser(conn, null, UserName, Pass);
                if (status != GWSiteStatusEnum.OK)
                    return -1;

                //
                // efectuar pesquisa
                //
                id = DataBase.GetUserID("Users", UserName);
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                //
                System.Console.WriteLine("EXCEPTION Method<GetByID> Class'User': " + ex.Message);
            }
            finally
            {
                //
                // fechar a conexão
                //
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return id;          
        
        }
        public string GetUserRole(string UserName, string Pass, int UserID)
        {
            SqlConnection conn = null;
            string urole = null;

            try
            {
                //
                // criar objecto de conexão à base de dados e abrir a conexão
                //
                conn = new SqlConnection(DataBase.CONN);
                conn.Open();

                //
                // validar user
                //
                GWSiteStatusEnum status = DataBase.ValidateUser(conn, null, UserName, Pass);
                if (status != GWSiteStatusEnum.OK)
                    return null;

                //
                // efectuar pesquisa
                //
                urole = DataBase.GetRoleByID("Roles", UserID);
                if (urole == null || urole.Trim().Length == 0)
                {
                    return "User";
                }
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                //
                System.Console.WriteLine("EXCEPTION Method<GetByID> Class'User': " + ex.Message);
            }
            finally
            {
                //
                // fechar a conexão
                //
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return urole;       
        }

        public DataSet GetAll(string UserName, string Pass)
        {
            DataSet ds;
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(DataBase.CONN);
                conn.Open();
                GWSiteStatusEnum status = DataBase.ValidateUser(conn, null, UserName, Pass);
                if (status != GWSiteStatusEnum.OK)
                    return null;
                ds = DataBase.GetAllByTable(conn, "Users");
                return ds;
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                System.Console.WriteLine("EXCEPTION Method'GetAll' Class'Users': " + ex.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }
        public DataSet GetByID(string UserName, string Pass, int UserID)
        {
            SqlConnection conn = null;
            DataSet ds = null;

            try
            {
                //
                // criar objecto de conexão à base de dados e abrir a conexão
                //
                conn = new SqlConnection(DataBase.CONN);
                conn.Open();

                //
                // validar user
                //
                GWSiteStatusEnum status = DataBase.ValidateUser(conn, null, UserName, Pass);
                if (status != GWSiteStatusEnum.OK)
                    return null;

                //
                // efectuar pesquisa
                //
                ds = DataBase.GetByID(conn, null, "Users", "UserID", UserID);
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                //
                System.Console.WriteLine("EXCEPTION Method<GetByID> Class'User': " + ex.Message);
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
        public DataSet FindByName(string UserName, string Pass, string Column, string NamePattern)
        {
            SqlConnection conn = null;
            DataSet ds = null;

            try
            {
                //
                // criar objecto de conexão à base de dados e abrir a conexão
                //
                conn = new SqlConnection(DataBase.CONN);
                conn.Open();

                //
                // validar utilizador
                //
                GWSiteStatusEnum status = DataBase.ValidateUser(conn, null, UserName, Pass);
                if (status != GWSiteStatusEnum.OK)
                    return null;

                //
                // efectuar pesquisa
                //
                ds = DataBase.FindByStringField("Users", Column, NamePattern);
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                //
                System.Console.WriteLine("EXCEPTION Method<FindByName> Class'User': " + ex.Message);
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


        public DataSet FindInDB(string UserName, string Pass, string table, string Column, string NamePattern) 
        {
            SqlConnection conn = null;
            DataSet ds = null;

            try
            {
                //
                // criar objecto de conexão à base de dados e abrir a conexão
                //
                conn = new SqlConnection(DataBase.CONN);
                conn.Open();

                //
                // validar utilizador
                //
                GWSiteStatusEnum status = DataBase.ValidateUser(conn, null, UserName, Pass);
                if (status != GWSiteStatusEnum.OK)
                    return null;

                //
                // efectuar pesquisa
                //
                ds = DataBase.FindByStringField(table, Column, NamePattern);
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                //
                System.Console.WriteLine("EXCEPTION Method<FindByName> Class'User': " + ex.Message);
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

        public int AddUser(string UserName, string Pass,
                       out GWSiteStatusEnum status)
        {
            int UserID = -1;
            SqlConnection conn = null;

            //
            // validar dados de entrada
            //
            if (UserName == null || UserName.Trim().Length == 0)
            {
                status = GWSiteStatusEnum.INVALID_ARGUMENT;
                return -1;
            }
            else if (Pass== null || Pass.Trim().Length == 0)
            {
                status = GWSiteStatusEnum.INVALID_ARGUMENT;
                return -1;
            }

            try
            {
                //
                // criar objecto de conexão à base de dados e abrir a conexão
                //
                conn = new SqlConnection(DataBase.CONN);
                conn.Open();

                //
                // validar o utilizador
                //
                /*
                status = DataBase.ValidateUser(conn, null, UserName, Pass);
                if (status != GWSiteStatusEnum.OK)
                    return -1;
                */
                //
                // criar comando SQL a executar
                //

                string sSqlCmd = "INSERT INTO Users (UserName, Pass) VALUES (@UserName, @Pass) SET @UserID = SCOPE_IDENTITY()";
                SqlCommand cmd = new SqlCommand(sSqlCmd, conn);
                cmd.Parameters.AddWithValue("UserName", UserName);
                cmd.Parameters.AddWithValue("Pass", Pass);
                SqlParameter UserIDParameter = new SqlParameter("@UserID",SqlDbType.Int);
                UserIDParameter.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(UserIDParameter);
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
                    // obter novo código de id gerado pela BD
                    UserID = (int)UserIDParameter.Value;
                    status = GWSiteStatusEnum.OK;
                }
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                //
                System.Console.WriteLine("EXCEPTION Method<AddUser> Class'User': " + ex.Message);
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

            return UserID;
        }


        //TODO
        public GWSiteStatusEnum UpdateUserPass(string UserName, string Pass, int UserID, string NewPass) 
        {
            GWSiteStatusEnum status = GWSiteStatusEnum.OK;
            SqlConnection conn = null;

            if (Pass== null || Pass.Trim().Length == 0)
            {
                status = GWSiteStatusEnum.INVALID_ARGUMENT;
                return status;
            }

            try
            {
                conn = new SqlConnection(DataBase.CONN);
                conn.Open();
                //status = DataBase.Set
                status = DataBase.ValidateUser(conn, null, UserName, Pass);
                if (status != GWSiteStatusEnum.OK)
                    return status;

                status = DataBase.UpdateByID("Users", UserID, "UserID", "Pass", NewPass);
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                System.Console.WriteLine("EXCEPTION Method'UpdateUser' Class'Users': " + ex.Message);
                return status;
            }
            finally
            {
                conn.Close();
            }
            return status;
        }
        //TODO
        public GWSiteStatusEnum DeleteUser(string UserName, string Pass, int UserID)
        {

            GWSiteStatusEnum status = GWSiteStatusEnum.OK;
            SqlConnection conn = null;

            if (UserName == null || UserName.Trim().Length == 0)
            {
                return GWSiteStatusEnum.INVALID_ARGUMENT;
            }
            else if (Pass == null || Pass.Trim().Length == 0)
            {
                return GWSiteStatusEnum.INVALID_ARGUMENT;
            }

            try
            {
                conn = new SqlConnection(DataBase.CONN);
                conn.Open();
                //status = DataBase.Set
                status = DataBase.ValidateUser(conn, null, UserName, Pass);
                if (status != GWSiteStatusEnum.OK)
                    return status;

                status = DataBase.DeleteByID("Users", UserID);
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                System.Console.WriteLine("EXCEPTION Method'UpdateUser' Class'Users': " + ex.Message);
                return status;
            }
            finally
            {
                conn.Close();
            }
            return status;         
        }

        public GWSiteStatusEnum Validate(string username, string pass)
        {

            GWSiteStatusEnum status = DataBase.ValidateUser(username, pass);
            return status;
        }
    }
}
