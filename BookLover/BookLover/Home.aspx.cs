using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookLover
{
    public partial class Home : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindTabs();
            BindLVProducts();
            BindCategories();
        }
        private void BindTabs()
        {
            string query = "SELECT * FROM tabs";
            DataTable tabs = UtilsClass.Select(query);
            lvTabs.DataSource = tabs;
            lvTabs.DataBind();
        }
        private void BindLVProducts()
        {
            string query = "SELECT top 3 * FROM products WHERE Offer<> ''";
            DataTable products = UtilsClass.Select(query);
            lvProducts.DataSource = products;
            lvProducts.DataBind();
        }
        private void BindGVProducts()
        {
            string query = "SELECT p.Id, p.Name, p.Offer, p.Source, c.Name AS Category FROM products p INNER JOIN categories c ON p.CategoryId = c.Id";
            DataTable products = UtilsClass.Select(query);
            gvProducts.DataSource = products;
            gvProducts.DataBind();
        }
        private void BindCategories()
        {
            string query = "SELECT * FROM categories ORDER BY Id DESC";
            DataTable categories = UtilsClass.Select(query);
            rptCategories.DataSource = categories;
            rptCategories.DataBind();
        }
        [WebMethod]
        public static string SaveCategory(Category category)
        {
            string result = string.Empty;
            if (string.IsNullOrEmpty(category.Name)) result += "Add category name ! ";
            if (string.IsNullOrEmpty(category.Source)) result += "Add category image ! ";
            if (UtilsClass.RecordExists(category.Name)) result += "This category already exists ! ";
            if (!string.IsNullOrEmpty(result)) return result;

            string constr = ConfigurationManager.ConnectionStrings["BookLoverContext"].ConnectionString;
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
                    UtilsClass.SaveImage(category.Source, "img/categories/", category.Name);
                }
                return "succes";
            }
            catch (SqlException)
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
            if (divProducts.Visible)
            {
                divProducts.Visible = false;
                btnShowProducts.Text = "View All Products";
            }
            else
            {
                divProducts.Visible = true;
                btnShowProducts.Text = "Hide All Products";
            }
            BindGVProducts();
        }

        protected void gvProducts_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            UtilsClass.Delete("products", gvProducts.DataKeys[e.RowIndex].Value.ToString());
            BindGVProducts();
        }

        protected void gvProducts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lbDelete = (LinkButton)e.Row.FindControl("lbDelete");
                lbDelete.Attributes.Add("onclick", "return confirm('Are you sure to delete this record?');");

                DropDownList ddlCategory = (e.Row.FindControl("ddlCategory") as DropDownList);
                ddlCategory.Enabled = e.Row.RowIndex == gvProducts.EditIndex;
                string query = "SELECT * from categories";
                DataTable categories = UtilsClass.Select(query);
                ddlCategory.DataSource = categories;
                ddlCategory.DataBind();
                ddlCategory.DataTextField = "Name";
                ddlCategory.DataValueField = "ID";
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("Select", "-1"));

                try
                {
                    string selectedCategory = DataBinder.Eval(e.Row.DataItem, "Category").ToString();
                    ddlCategory.Items.FindByText(selectedCategory).Selected = true;
                }
                catch (Exception) { }
            }
        }

        protected void gvProducts_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvProducts.EditIndex = e.NewEditIndex;
            BindGVProducts();
        }

        protected void gvProducts_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BookLoverContext"].ConnectionString);
            int id = Convert.ToInt32(gvProducts.DataKeys[e.RowIndex].Value.ToString());
            GridViewRow row = gvProducts.Rows[e.RowIndex];
            TextBox txtName = (TextBox)row.Cells[0].Controls[0];
            TextBox txtOffer = (TextBox)row.Cells[1].Controls[0];
            TextBox txtSource = (TextBox)row.Cells[2].Controls[0];
            DropDownList ddlCategory = (DropDownList)row.Cells[3].FindControl("ddlCategory");
            gvProducts.EditIndex = -1;
            try
            {
                #region Check File
                string query = "SELECT top 1 Name, Source FROM products WHERE Id = " + id + ";";
                DataTable dtProducts = UtilsClass.Select(query);

                if (dtProducts.Rows.Count > 0)
                {
                    if ((dtProducts.Rows[0]["Source"].ToString().ToLower() != txtSource.Text.ToString().ToLower()))
                    {
                        string fileName = dtProducts.Rows[0]["Name"].ToString();
                        UtilsClass.DeleteImage(fileName);
                        UtilsClass.SaveImage(txtSource.Text, "img/products/", txtName.Text);
                    }
                }
                #endregion

                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE products SET Name='" + txtName.Text + "',Offer='" + txtOffer.Text + "',Source='" + txtSource.Text + "',CategoryId='" + ddlCategory.SelectedValue + "'WHERE Id='" + id + "'", conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception) { }

            BindGVProducts();
            BindLVProducts();
        }

        protected void gvProducts_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvProducts.EditIndex = -1;
            BindGVProducts();
        }
    }
}