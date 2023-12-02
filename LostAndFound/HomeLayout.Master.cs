using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Xml.Linq;

namespace LostAndFound
{
    public partial class HomeLayout : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            con.Open();
            SqlCommand da = new SqlCommand("select email from users where name in (select Receiver from chat where Sender=@Sender and mail=0)", con);
            da.Parameters.AddWithValue("@Sender", Session["name"].ToString());
            SqlDataReader dr = da.ExecuteReader();
            while (dr.Read())
            {
                string to = dr["email"].ToString();
                string fromMail = "lostandfoundhub3@gmail.com";
                string fromPassword = "pmjcbaqjfotyrpng";
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress(fromMail);
                msg.Subject = "Message_LostAndFound Hub";
                msg.To.Add(new MailAddress(to));
                msg.Body = "You have received a new message from " + Session["name"].ToString();
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromMail, fromPassword),
                    EnableSsl = true,
                };
                smtpClient.Send(msg);
            }
            con.Close();
            con.Open();
            SqlCommand cmd= new SqlCommand("update chat set mail=1", con);
            int i = cmd.ExecuteNonQuery();
            con.Close();
            Session["email"] = "";
            Response.Redirect("Login.aspx");
        }
    }
}