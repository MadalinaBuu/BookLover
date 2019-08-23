using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace BookLover
{
    public static class UtilsClass
    {
        public static void SaveImage(string url, string pathS, string filename)
        {
            byte[] data;
            string ext = string.Empty;
            using (WebClient client = new WebClient())
            {
                data = client.DownloadData(url);
                ext = Path.GetExtension(url);
            }
            File.WriteAllBytes(HttpContext.Current.Server.MapPath(pathS) + RemoveSpecialCharacters(filename).ToLower() + ext, data);
        }
        public static void DeleteImage(string filename)
        {
            var filePath = HttpContext.Current.Server.MapPath("img/products/" + RemoveSpecialCharacters(filename).ToLower() + ".jpg");
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public static string RemoveSpecialCharacters(this string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
        public static bool RecordExists(string categoryName)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BookLoverContext"].ConnectionString);
            DataTable dtCategories = new DataTable();
            try
            {
                conn.Open();
                String sql = "SELECT * FROM categories WHERE Name = @name";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", categoryName);
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                ad.Fill(dtCategories);

                if (dtCategories.Rows.Count > 0) return true;
            }
            catch (SqlException){}
            finally
            {
                conn.Close();
            }
            return false;
        }
        public static DataTable Select(string query)
        {
            string constr = ConfigurationManager.ConnectionStrings["BookLoverContext"].ConnectionString;
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
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BookLoverContext"].ConnectionString);
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