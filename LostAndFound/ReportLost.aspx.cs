using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Syn.Bot.Oscova;
using Google.Protobuf.Reflection;
using System.Security.Cryptography;

namespace LostAndFound
{
    public partial class ReportLost : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            try
            {
                con.Open();
                byte[] imageData = fileUpload.FileBytes;
                SqlCommand cmd = new SqlCommand("sp_lost", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@lname", txtname.Text);
                cmd.Parameters.AddWithValue("ldesc", txtLDesc.Text);
                cmd.Parameters.AddWithValue("@llostAt", txtLLostAt.Text);
                cmd.Parameters.AddWithValue("@ldate", Convert.ToDateTime(txtldate.Text));
                cmd.Parameters.Add("@l_img", System.Data.SqlDbType.VarBinary, -1).Value = imageData;
                cmd.Parameters.AddWithValue("@postBy", Session["email"].ToString());
                int x= cmd.ExecuteNonQuery();
                if(x>0)
                {
                    try
                    {
                        SqlCommand cmd1 = new SqlCommand("update users set lost=lost+1 where email=@email",con);
                        cmd1.Parameters.AddWithValue("@email", Session["email"].ToString());
                        int ex = cmd1.ExecuteNonQuery();
                       Response.Redirect("LostItems.aspx");
                    }
                    catch(Exception ex)
                    {
                        Response.Write("Something wrong");
                    }
                }
                else
                {
                    Response.Write("Something went wrong");
                }
               
            }
                catch (Exception ex)
            {
                Response.Write("no this");

                if (ex.InnerException != null)
                {
                    Response.Write("because of this");
                }
            }

            finally
            {
                con.Close();
            }
        }
    }
}