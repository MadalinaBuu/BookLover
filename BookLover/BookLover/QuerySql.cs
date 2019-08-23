using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Verto
{
    public class QuerySql
    {
        public static DataTable Select(string query)
        {
            string constr = ConfigurationManager.ConnectionStrings["VertoContext"].ConnectionString;
            SqlConnection conn = new SqlConnection(constr);
            try
            {
                conn.Open();
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(query, con))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
            catch (SqlException) { return null; }
            finally
            {
                conn.Close();
            }
        }
        public static void Delete(string table, string id)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["VertoContext"].ConnectionString);
            try
            {
                conn.Open();
                string query = "DELETE FROM " + table + " where Id=" + id;
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException) { }
            finally
            {
                conn.Close();
            }
        }
    }
}