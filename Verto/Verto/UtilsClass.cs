﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace Verto
{
    public static class UtilsClass
    {
        public static void SaveImage(string url, string filename)
        {
            byte[] data;
            string ext = string.Empty;
            using (WebClient client = new WebClient())
            {
                data = client.DownloadData(url);
                ext = Path.GetExtension(url);
            }
            File.WriteAllBytes(HttpContext.Current.Server.MapPath("img/categories/") + RemoveSpecialCharacters(filename).ToLower() + ext, data);
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
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["VertoContext"].ConnectionString);
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
    }
}