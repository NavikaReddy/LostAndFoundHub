using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace LostAndFound
{
    public partial class FoundPosts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetData();
            }
        }
        public void GetData()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_getUserFoundData", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@postBy", Session["email"].ToString());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    userRepeater.DataSource = dt;
                    userRepeater.DataBind();
                }
                else
                {
                    // No data, show the "no" image
                    noContainer.Visible = true;
                    no.Src = "https://cdn.dribbble.com/users/1231865/screenshots/11157048/media/bc9427646c632ded563ee076fdc5dfda.jpg?resize=1200x900&vertical=center";
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        protected void btndel_Click(object sender, EventArgs e)
        {
            int id = int.Parse(((Button)(sender)).CommandArgument.ToString());
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("delete from found where fid=@fid", con);
                cmd.Parameters.AddWithValue("@fid", id);
                int x = cmd.ExecuteNonQuery();
                if (x > 0)
                {
                    Response.Redirect("FoundPosts.aspx");
                }
                else
                {
                    Response.Write("Something went wrong");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally { con.Close(); }
        }
    }
}