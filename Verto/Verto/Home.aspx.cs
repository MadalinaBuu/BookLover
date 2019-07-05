using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Verto
{
    public partial class Home : Page
    {
        string constr = ConfigurationManager.ConnectionStrings["VertoContext"].ConnectionString;

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
                query += "SELECT top 3 * FROM " + table + " WHERE Offer <> ''";
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
        private void BindRepeater(string table, Repeater rpt)
        {
            string query = "SELECT * FROM " + table + " ORDER BY Id DESC";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(query, con))
                {
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        rpt.DataSource = dt;
                        rpt.DataBind();
                    }
                }
            }
        }
        private void BindProducts()
        {
            string query = "SELECT p.Id, p.Name, p.Offer, p.Source, c.Name AS Category FROM products p INNER JOIN categories c ON p.CategoryId = c.Id";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(query, con))
                {
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        gvProducts.DataSource = dt;
                        gvProducts.DataBind();
                    }
                }
            }
        }
        [WebMethod]
        public static string SaveCategory(Category category)
        {
            string result = string.Empty;
            if (string.IsNullOrEmpty(category.Name)) result += "Add category name ! ";
            if (string.IsNullOrEmpty(category.Source)) result += "Add category image ! ";
            if (UtilsClass.RecordExists(category.Name)) result += "This category already exists ! ";
            if (!string.IsNullOrEmpty(result)) return result;

            string constr = ConfigurationManager.ConnectionStrings["VertoContext"].ConnectionString;
            SqlConnection conn = new SqlConnection(constr);
            string query = "INSERT INTO categories VALUES(@Name, @Source)";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", category.Name);
                cmd.Parameters.AddWithValue("@Source", category.Source);
                cmd.CommandType = CommandType.Text;
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    UtilsClass.SaveImage(category.Source, category.Name);
                }
                return "succes";
            }
            catch (SqlException )
            {
                return "An error has occured!";
            }
            finally
            {
                conn.Close();
            }
        }

        protected void btnShowProducts_Click(object sender, EventArgs e)
        {
            divProducts.Visible = true;
            BindProducts();
        }

        protected void gvProducts_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["VertoContext"].ConnectionString);
            GridViewRow row = gvProducts.Rows[e.RowIndex];
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM products where Id='" + Convert.ToInt32(gvProducts.DataKeys[e.RowIndex].Value.ToString()) + "'", conn);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException){}
            finally
            {
                conn.Close();
            }
            BindProducts();
        }

        protected void gvProducts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lbDelete = (LinkButton)e.Row.FindControl("lbDelete");
                lbDelete.Attributes.Add("onclick", "return confirm('Are you sure to delete this record?');");

                #region ddlCategory
                DropDownList ddlCategory = (e.Row.FindControl("ddlCategory") as DropDownList);
                ddlCategory.Enabled = e.Row.RowIndex == gvProducts.EditIndex;
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["VertoContext"].ConnectionString);
                try
                {
                    conn.Open();
                    string cmd = "Select * from categories";
                    SqlDataAdapter adpt = new SqlDataAdapter(cmd, conn);
                    DataTable dt = new DataTable();
                    adpt.Fill(dt);
                    ddlCategory.DataSource = dt;
                    ddlCategory.DataBind();
                    ddlCategory.DataTextField = "Name";
                    ddlCategory.DataValueField = "ID";
                    ddlCategory.DataBind();
                    string selectedCategory = DataBinder.Eval(e.Row.DataItem, "Category").ToString();
                    ddlCategory.Items.FindByText(selectedCategory).Selected = true;
                }
                catch (Exception) { }
                finally
                {
                    conn.Close();
                }
                #endregion
            }
        }

        protected void gvProducts_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvProducts.EditIndex = e.NewEditIndex;
            BindProducts();
        }

        protected void gvProducts_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["VertoContext"].ConnectionString);
            int id = Convert.ToInt32(gvProducts.DataKeys[e.RowIndex].Value.ToString());
            GridViewRow row = (GridViewRow)gvProducts.Rows[e.RowIndex];
            TextBox txtName = (TextBox)row.Cells[0].Controls[0];
            TextBox txtOffer = (TextBox)row.Cells[1].Controls[0];
            TextBox txtSource = (TextBox)row.Cells[2].Controls[0];
            DropDownList ddlCategory = (DropDownList)row.Cells[3].FindControl("ddlCategory");
            gvProducts.EditIndex = -1;
            conn.Open();
            SqlCommand cmd = new SqlCommand("UPDATE products SET Name='" + txtName.Text + "',Offer='" + txtOffer.Text + "',Source='" + txtSource.Text + "',CategoryId='" + ddlCategory.SelectedValue + "'WHERE Id='" + id + "'", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            BindProducts();
            BindList("products", lvProducts, true);
        }
    }
}