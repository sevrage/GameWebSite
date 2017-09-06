using System;
using System.Data;
using System.Data.SqlClient;

namespace GWSiteClassLibrary
{
    public interface IMap
    {
        DataSet GetAll(string UserName, string Pass);
        DataSet GetByID(string UserName, string Pass, int MapID);
        DataSet FindByName(string UserName, string Pass, string Descr);

        //int GetTotalMaps(string username, string pass, int MapID, out StatusEnum status);

        GWSiteStatusEnum AddMap(string UserName, string Pass, string Descr);
 
        GWSiteStatusEnum UpdateMap(string UserName, string Pass, int MapID, string Descr);

        GWSiteStatusEnum DeleteMap(string UserName, string Pass, int MapID);

    }
    public class Map : IMap
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
                ds = DataBase.GetAllByTable(conn, "Maps");          
                return ds;
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                System.Console.WriteLine("EXCEPTION Method'GetAll' Class'Map': " + ex.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }
        public DataSet GetByID(string UserName, string Pass, int MapID)
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
                ds = DataBase.GetByID(conn, null, "Maps", "MapID", MapID);
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                //
                System.Console.WriteLine("EXCEPTION Method<GetByID> Class'Map': " + ex.Message);
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

        public DataSet FindByName(string UserName, string Pass, string Descr)
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
                ds = DataBase.FindByStringField("Maps", "Descr", Descr);
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                //
                System.Console.WriteLine("EXCEPTION Method<FindByName> Class'Map': " + ex.Message);
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

        public GWSiteStatusEnum AddMap(string UserName, string Pass, string Descr)
        {
            GWSiteStatusEnum status = GWSiteStatusEnum.OK;
            SqlConnection conn = null;

            if (Descr == null || Descr.Trim().Length == 0)
            {
                status = GWSiteStatusEnum.INVALID_ARGUMENT;
                return status;
            }

            try
            {
                conn = new SqlConnection(DataBase.CONN);
                conn.Open();

                status = DataBase.ValidateUser(conn, null, UserName, Pass);
                if (status != GWSiteStatusEnum.OK)
                    return status;

                status = DataBase.AddNew("Maps", Descr);
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                System.Console.WriteLine("EXCEPTION Method'AddMap' Class'Maps': " + ex.Message);
                return status;
            }
            finally
            {
                conn.Close();
            }
            return status;
        }

        public GWSiteStatusEnum UpdateMap(string UserName, string Pass,int MapID, string Descr)
        {
            GWSiteStatusEnum status = GWSiteStatusEnum.OK;
            SqlConnection conn = null;

            if (Descr == null || Descr.Trim().Length == 0)
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

                status = DataBase.UpdateByID("Maps", MapID, "MapID", "Descr", Descr);
            }
            catch (SqlException ex)
            {
                //
                // tratar a excepção!!!!
                System.Console.WriteLine("EXCEPTION Method<UpdateMap> Class'Users': " + ex.Message);
                return status;
            }
            finally
            {
                conn.Close();
            }
            return status;
        }
        public GWSiteStatusEnum DeleteMap(string UserName, string Pass, int MapID)
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
            else if (MapID.ToString() == null || Pass.ToString().Trim().Length == 0)
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

                status = DataBase.DeleteByID("Maps", MapID);
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
    }
}
