using System;
using System.Data.SqlClient;
using System.Data;

namespace GWSiteClassLibrary
{
    public interface IScore
    {
        DataSet GetAll(string UserName, string Pass);
        DataSet GetByID(string UserName, string Pass, int ScoreID);
        DataSet FindByName(string UserName, string Pass, string NamePattern);
        DataSet GetPlayerRank(string UserName, string Pass, int MapID, int PlayerID, int retLines, string scoreOrder);
        //DataSet GetPlayerRank(string UserName, string Pass, int MapID, int retLines, string scoreOrder);
        DataSet GetMapScoreRank(string UserName, string Pass, int MapID, int retLines, string scoreOrder);

        DataSet GetPlayerHistory(string UserName, string Pass, int PlayerID, string orderField, string orderType);

        int AddScore(string UserName, string Pass,
            int? PlayerID, int? MapID, int? Score, string Date,
            out GWSiteStatusEnum status);
  
        //TODO
        GWSiteStatusEnum DeleteScore(string UserName, string Pass,
            int? ScoreID, int? MapID, int? Score, string Date);

        //TODO
        GWSiteStatusEnum UpdateScore(string UserName, string Pass,
            int? ScoreID, int? Score, string Date);
    }

    public class Score : IScore
    {
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
                ds = DataBase.GetAllByTable(conn, "Scores");
                return ds;
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                System.Console.WriteLine("EXCEPTION Method<GetAll> Class'Score': " + ex.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }
        public DataSet GetByID(string UserName, string Pass, int ScoreID)
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
                ds = DataBase.GetByID(conn, null, "Scores", "ScoreID", ScoreID);
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                //
                System.Console.WriteLine("EXCEPTION Method<GetByID> Class'Score': " + ex.Message);
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
        public DataSet FindByName(string UserName, string Pass, string Date)
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
                ds = DataBase.FindByStringField("Scores", "Date", Date);
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                //
                System.Console.WriteLine("EXCEPTION Method<FindByName> Class'Score': " + ex.Message);
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


        public int AddScore(string UserName, string Pass,
                       int? PlayerID, int? MapID, int? Score, string Date,
                       out GWSiteStatusEnum status)
        {
            int ScoreID = -1;
            SqlConnection conn = null;

            //
            // validar dados de entrada
            //
            if (PlayerID.HasValue == false)
            {
                status = GWSiteStatusEnum.INVALID_ARGUMENT;
                return -1;
            }
            else if (MapID.HasValue == false)
            {
                status = GWSiteStatusEnum.INVALID_ARGUMENT;
                return -1;
            }
            else if (Score.HasValue == false)
            {
                status = GWSiteStatusEnum.INVALID_ARGUMENT;
                return -1;
            }
            else if (Date.Length == 0)
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
                    status = GWSiteStatusEnum.NOT_OK;
                else
                {
                    //
                    // inseriu registo
                    // obter novo código de id gerado pela BD
                    // veja "referência Rápida ADO.net" no moodle
                    SqlCommand idCmd = new SqlCommand("SELECT @@IDENTITY", conn);
                    ScoreID = (int)idCmd.ExecuteScalar();
                    status = GWSiteStatusEnum.OK;
                }
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                //
                System.Console.WriteLine("EXCEPTION Method<AddScore> Class'Score': " + ex.Message);
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

            return ScoreID;
        }

        public GWSiteStatusEnum UpdateScore(string UserName, string Pass, int? ScoreID, int? Score, string Date)
        {
            GWSiteStatusEnum status;
            SqlConnection conn = null;

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
                    return status;

                //
                // criar comando SQL a executar
                //
                string sqlCmd = "UPDATE Scores SET Score=@Score, Date=@Date WHERE ScoreID=@ScoreID;";
                SqlCommand cmd = new SqlCommand(sqlCmd, conn);
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
                    status = GWSiteStatusEnum.NOT_OK;
                else
                {
                    status = GWSiteStatusEnum.OK;
                }
                return status;
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                //
                System.Console.WriteLine("EXCEPTION Method<UpdateScore> Class'Score': " + ex.Message);
                status = GWSiteStatusEnum.ERROR;
                return status;
            }
            finally
            {
                //
                // fechar a conexão
                //
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public GWSiteStatusEnum DeleteScore(string UserName, string Pass,
            int? ScoreID, int? MapID, int? Score, string Date)
        {
            return GWSiteStatusEnum.OK;
        }

        public DataSet GetPlayerRank(string UserName, string Pass, int MapID, int PlayerID, int retLines, string scoreOrder)
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

                ds = DataBase.GetPlayerRank("Scores", MapID, PlayerID, retLines, scoreOrder);


                return ds;

            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                System.Console.WriteLine("EXCEPTION Method<GetPlayerRank> Class'Score': " + ex.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public DataSet GetMapScoreRank(string UserName, string Pass, int MapID, int retLines, string scoreOrder)
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

                ds = DataBase.GetRank("Scores", MapID, retLines, scoreOrder);
                return ds;
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                System.Console.WriteLine("EXCEPTION Method<GetMapScoreRank> Class'Score': " + ex.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public DataSet GetPlayerHistory(string UserName, string Pass, int PlayerID, string orderField, string orderType)
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

                ds = DataBase.GetAllHistory("Scores", PlayerID, orderField, orderType);
                return ds;
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                System.Console.WriteLine("EXCEPTION Method<GetPlayerHistory> Class'Score': " + ex.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

    }
}
