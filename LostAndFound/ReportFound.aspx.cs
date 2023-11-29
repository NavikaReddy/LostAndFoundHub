using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.WebRequestMethods;
using System.Net.Mail;
using System.Net;

namespace LostAndFound
{
    public partial class ReportFound : System.Web.UI.Page
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
                SqlCommand cmd = new SqlCommand("sp_found", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fname", txtname.Text);
                cmd.Parameters.AddWithValue("fdesc", txtFDesc.Text);
                cmd.Parameters.AddWithValue("@ffoundAt", txtFFoundAt.Text);
                cmd.Parameters.AddWithValue("@fdate", Convert.ToDateTime(txtfdate.Text));
                cmd.Parameters.Add("@f_img", System.Data.SqlDbType.VarBinary, -1).Value = imageData;
                cmd.Parameters.AddWithValue("@postBy", Session["email"].ToString());
                int x = cmd.ExecuteNonQuery();
                if (x > 0)
                {
                    SqlCommand da = new SqlCommand("select email from users where lost>=1", con);
                    SqlDataReader dr = da.ExecuteReader();
                    while (dr.Read())
                    {
                        string to = dr["email"].ToString();
                        string fromMail = "lostandfoundhub3@gmail.com";
                        string fromPassword = "pmjcbaqjfotyrpng";
                        MailMessage msg = new MailMessage();
                        msg.From = new MailAddress(fromMail);
                        msg.Subject = "Found_LostAndFound Hub";
                        msg.To.Add(new MailAddress(to));
                        msg.Body = "New item found :) \n\nDetails are as below: \n" + "\nProduct Name : " + txtname.Text +"\nProduct Description : " + txtFDesc.Text + "\nFound At : "  + txtFFoundAt.Text + "\n If it is yours contact the user who posted by logging in to out website LostAndFoundHub";
                        var smtpClient = new SmtpClient("smtp.gmail.com")
                        {
                            Port = 587,
                            Credentials = new NetworkCredential(fromMail, fromPassword),
                            EnableSsl = true,
                        };
                        smtpClient.Send(msg);
                    }
                    Response.Redirect("FoundItems.aspx");
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
            finally
            {
                con.Close();
            }
        }
       
    }
}