using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LostAndFound
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnreg_Click(object sender, EventArgs e)
        {
            string email = Session["email"].ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update users set pass = @pass where email=@email", con);
                cmd.Parameters.AddWithValue("@pass", txtpwd.Text);
                cmd.Parameters.AddWithValue("@email", email);
                int ex = cmd.ExecuteNonQuery();
                if (ex > 0)
                {
                    confirm.Text = "Password updated successfully";
                    confirm.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    confirm.Text = "Couldn't update password!";
                    confirm.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
            finally { con.Close(); }
        }
    }
}