using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class SQLDataAccess
    {
        #region Member Variable

        private SqlConnection conn;
        private DataSet ds;
        private SqlCommand cmd;
        private SqlDataAdapter da;
        private DataTable dt;

        private SqlDataReader dr;
        #endregion

        #region Constructor

        public SQLDataAccess()
        {

            conn = new SqlConnection(Connection.SqlConnectionString);

        }
        #endregion

        #region Member Functions
        public void ExecuteStoreProcedure(string strSp_Name)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            cmd = new SqlCommand(strSp_Name, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            conn.Close();

        }

        /// <summary>
        /// Execute Store Procedure and Get the Data in Dataset
        /// </summary>
        /// <param name="strSP_Name">Store Procedure Name</param>
        /// <returns>Result in Dataset</returns>
        public DataSet GetDatasetExecuteStoreProcedure(string strSP_Name)
        {
            cmd = new SqlCommand(strSP_Name, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);


            return ds;

        }
        /// <summary>
        /// Gets the datatable execute store procedure.
        /// </summary>
        /// <param name="strSP_Name">Name of the STR S p_.</param>
        /// <returns></returns>
        public DataTable GetDatatableExecuteStoreProcedure(string strSP_Name)
        {
            cmd = new SqlCommand(strSP_Name, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);

            return dt;

        }

        public DataTable GetDatatableExecuteStoreProcedure(string strSP_Name, Hashtable hInputParameters)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            cmd = new SqlCommand(strSP_Name, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (hInputParameters.Count > 0)
            {
                IDictionaryEnumerator IEnum = hInputParameters.GetEnumerator();
                while (IEnum.MoveNext())
                {
                    cmd.Parameters.AddWithValue(IEnum.Key.ToString(), IEnum.Value);
                }
            }
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);

            return dt;

        }


        /// <summary>
        /// Excute Store Procedure Having Input Parameters
        /// </summary>
        /// <param name="strSP_Name">Store Procedure Name</param>
        /// <param name="hInputParameters">Input Parameters Key=Parameter Name value= Value of Parameter</param>
        public string ExecuteStoreProcedure(string strSP_Name, Hashtable hInputParameters)
        {
            try
            {

                if (conn.State == ConnectionState.Open)
                    conn.Close();
                conn.Open();
                cmd = new SqlCommand(strSP_Name, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (hInputParameters.Count > 0)
                {
                    IDictionaryEnumerator IEnum = hInputParameters.GetEnumerator();
                    while (IEnum.MoveNext())
                    {
                        cmd.Parameters.AddWithValue(IEnum.Key.ToString(), IEnum.Value);
                    }
                }
                // cmd.ExecuteNonQuery();

                //  conn.Close();

                DataTable dt = new DataTable();


                //  cmd.ExecuteNonQuery();
                dr = cmd.ExecuteReader();
                //  da = new SqlDataAdapter(cmd);

                dt = new DataTable();
                dt.Load(dr);
                conn.Close();

                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0][0].ToString();
                }

                return string.Empty;

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        #endregion
    }
}
