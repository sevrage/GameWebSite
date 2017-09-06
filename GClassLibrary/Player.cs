using System;
using System.Data.SqlClient;
using System.Data;

namespace GWSiteClassLibrary
{
    public interface IPlayer
    {

        DataSet GetAll(string UserName, string Pass);
        DataSet GetByID(string UserName, string Pass, int PlayerID);
        DataSet FindByName(string UserName, string Pass, string Column, string NamePattern);

        int AddPlayer(string UserName, string Pass,
                int? UserID, string Name, int? Age, char? Gender,
                string Email, string Address, string Country, out GWSiteStatusEnum status);

        DataSet GetDistinct(string UserName, string Pass, string Column);

        GWSiteStatusEnum UpdatePlayer(string UserName, string Pass,
            int? PlayerID, string Name, int? Age, char? Gender,
            string Email, string Address, string Country);

        GWSiteStatusEnum UpdatePlayerX(string UserName, string Pass,
            int? PlayerID, string[] X);
        
    }

    public class Player : IPlayer
    {
        public Player()
        {
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
                ds = DataBase.GetAllByTable(conn, "Players");
                return ds;
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                System.Console.WriteLine("EXCEPTION Method<GetAll> Class'Player': " + ex.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }
        public DataSet GetByID(string UserName, string Pass, int PlayerID)
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
                ds = DataBase.GetByID(conn, null, "Players", "PlayerID", PlayerID);
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                //
                System.Console.WriteLine("EXCEPTION Method<GetByID> Class'Player': " + ex.Message);
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

        
        public DataSet GetDistinct(string UserName, string Pass, string Column)
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
                ds = DataBase.GetDistinct("Players", Column);
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                //
                System.Console.WriteLine("EXCEPTION Method<FindByName> Class'Player': " + ex.Message);
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
                ds = DataBase.FindByStringField("Players", Column , NamePattern);
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                //
                System.Console.WriteLine("EXCEPTION Method<FindByName> Class'Player': " + ex.Message);
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

        public int AddPlayer(string UserName, string Pass,
                       int? UserID, string Name, int? Age, char? Gender, 
                       string Email, string Address, string Country,
                       out GWSiteStatusEnum status)
        {
            int PlayerID = -1;
            SqlConnection conn = null;

            //
            // validar dados de entrada
            //
            if (UserID.HasValue == false)
            {
                status = GWSiteStatusEnum.INVALID_ARGUMENT;
                return -1;
            }
            else if (Name == null || Name.Trim().Length == 0)
            {
                status = GWSiteStatusEnum.INVALID_ARGUMENT;
                return -1;
            }
            else if (Email == null || Email.Trim().Length == 0)
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
                status = DataBase.ValidateUser(conn, null, UserName, Pass);
                if (status != GWSiteStatusEnum.OK)
                    return -1;

                //
                // criar comando SQL a executar
                //

                string sqlCmd = "INSERT INTO Players (UserID, Name, Age, Gender, Email, Address, Country) " +
                                "VALUES (@USerID, @Name, @Age, @Gender, @Email, @Address, @Country)";
                SqlCommand cmd = new SqlCommand(sqlCmd, conn);
                cmd.Parameters.AddWithValue("UserID", UserID);
                cmd.Parameters.AddWithValue("Name", Name);
                if (Age.HasValue == false)
                    cmd.Parameters.AddWithValue("Age", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("Age", Age);

                if (Gender.HasValue == false)
                    cmd.Parameters.AddWithValue("Gender", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("Gender", Gender);
                
                    cmd.Parameters.AddWithValue("Email", Email);

                if (Address.Length == 0)
                    cmd.Parameters.AddWithValue("Address", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("Address", Address);

                if (Country.Length == 0)
                    cmd.Parameters.AddWithValue("Country", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("Country", Country);

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
                    // veja "referência Rápida ADO.net" no moodle
                    SqlCommand idCmd = new SqlCommand("SELECT @@IDENTITY", conn);
                    PlayerID = (int)idCmd.ExecuteScalar();
                    status = GWSiteStatusEnum.OK;
                }
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                //
                System.Console.WriteLine("EXCEPTION Method<AddPlayer> Class'Player': " + ex.Message);
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

            return PlayerID;
        }
        public GWSiteStatusEnum UpdatePlayerX(string UserName, string Pass,
            int? PlayerID, string[] X) 
        {

            GWSiteStatusEnum status = GWSiteStatusEnum.OK;
            SqlConnection conn = null;

            if (PlayerID.HasValue == false)
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
                string[] column = { "Name", "Age", "Gender", "Email", "Address", "Country" };
                //int? PlayerID, string Name, int? Age, char? Gender,string Email, string Address, string Country)
                //string[] value = { Name, Age.ToString(), Gender.ToString(), Email, Address, Country };
                status = DataBase.UpdateByID("Players", PlayerID, "PlayerID", column, X);

            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                System.Console.WriteLine("EXCEPTION Method'UpdatePlayer' Class'Players': " + ex.Message);
                return status;
            }
            finally
            {
                conn.Close();
            }
            return status;       
        
        }

        public GWSiteStatusEnum UpdatePlayer(string UserName, string Pass,
                                 int? PlayerID, string Name, int? Age, char? Gender, 
                                 string Email, string Address, string Country)
        {
            GWSiteStatusEnum status = GWSiteStatusEnum.OK;
            SqlConnection conn = null;

            if (PlayerID.HasValue == false)
            {
                status = GWSiteStatusEnum.INVALID_ARGUMENT;
                return status;
            }
            else if (Name == null || Name.Trim().Length == 0)
            {
                status = GWSiteStatusEnum.INVALID_ARGUMENT;
                return status;
            }
            else if (Email == null || Email.Trim().Length == 0)
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
                string[] column = {"Name","Age","Gender","Email","Address", "Country"};
                //int? PlayerID, string Name, int? Age, char? Gender,string Email, string Address, string Country)
                string[] value = { Name, Age.ToString(), Gender.ToString(), Email, Address, Country };
                status = DataBase.UpdateByID("Players", PlayerID, "PlayerID", column, value);
                
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                System.Console.WriteLine("EXCEPTION Method'UpdatePlayer' Class'Players': " + ex.Message);
                return status;
            }
            finally
            {
                conn.Close();
            }
            return status;
        }



    }


}
