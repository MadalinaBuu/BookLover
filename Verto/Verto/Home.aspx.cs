using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Verto
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindList("tabs", lvTabs, false);
            BindList("products", lvProducts, true);
            BindRepeater("categories", rptCategories);
        }

        private void BindList(string table, ListView list, bool products)
        {
            string constr = ConfigurationManager.ConnectionStrings["VertoContext"].ConnectionString;
            string query = string.Empty;
            if (products)
                query += "SELECT top 3 * FROM " + table + " WHERE Offer IS NOT NULL";
            else
                query = "SELECT * FROM " + table;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(query, con))
                {
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        list.DataSource = dt;
                        list.DataBind();
                    }
                }
            }
        }
        private void BindRepeater(string table, Repeater list)
        {
            string constr = ConfigurationManager.ConnectionStrings["VertoContext"].ConnectionString;
            string query = query = "SELECT * FROM " + table;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(query, con))
                {
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        list.DataSource = dt;
                        list.DataBind();
                    }
                }
            }
        }
    }
}