using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Services.Description;
using Microsoft.Ajax.Utilities;

namespace LostAndFound
{
    public partial class Register : System.Web.UI.Page
    {
        public int otp;
        public bool verified = false;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void register_Click(object sender, EventArgs e)
        {
            verified = Convert.ToBoolean(Session["verify"]);
            if (verified == true)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
                try
                {
                    con.Open();
                    SqlCommand da = new SqlCommand("select email from users", con);
                    SqlDataReader dr = da.ExecuteReader();
                    bool notExist = true;
                    while (dr.Read())
                    {
                        if (dr["email"].ToString() == txtemail.Text)
                        {
                            lbl.Text = "User already exists!! Login with your credentials";
                            notExist = false;
                            break;
                        }
                    }
                    con.Close();
                    if (notExist == true)
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("sp_register", con);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@name", txtname.Text);
                        cmd.Parameters.AddWithValue("@email", txtemail.Text);
                        cmd.Parameters.AddWithValue("@pass", txtpass.Text);
                        cmd.Parameters.AddWithValue("@mobile", txtmobile.Text);
                        int x = cmd.ExecuteNonQuery();
                        if (x > 0)
                        {
                            Response.Redirect("Login.aspx");
                        }
                        else
                        {
                            Response.Write("Something went wrong");
                        }
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
            
            else
            {
                lbl.Text = "Email verification failed";
            }
        }


        protected void send_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            otp = rnd.Next(1000, 9999);
            Session["otpTxt"] = otp;
            string fromMail = "lostandfoundhub3@gmail.com";
            string fromPassword = "pmjcbaqjfotyrpng";
            MailMessage msg= new MailMessage();
            msg.From = new MailAddress(fromMail);
            msg.Subject = "OTP_LostAndFound Hub";
            MailAddress ma = new MailAddress(txtemail.Text);
            msg.To.Add(ma);

                msg.To.Add(ma);
                msg.Body = "Your otp is " + otp.ToString();

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromMail, fromPassword),
                    EnableSsl = true,
                };

                smtpClient.Send(msg);
        }

        protected void verify_Click(object sender, EventArgs e)
        {

            otp = Convert.ToInt32(Session["otpTxt"]);
            if (otp.ToString() == txtotp.Text)
            {
                verified = true;
                Session["verify"] = verified;
                // Set green tick icon
                verifyIcon.Attributes["class"] = "fa-solid fa-check";
                verifyIcon.Attributes["style"] = "color: #0cd41a;"; // Green background color
            }
            else
            {
                verified = false;
                Session["verify"] = verified;

                // Set red cross icon
                verifyIcon.Attributes["class"] = "fa-solid fa-xmark";
                verifyIcon.Attributes["style"] = "color: #ff0000;"; // Red background color
            }
        }
    }
}