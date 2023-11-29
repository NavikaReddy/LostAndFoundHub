using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using Microsoft.Ajax.Utilities;
using System.Web.UI.HtmlControls;

namespace LostAndFound
{
    public partial class FoundItems : System.Web.UI.Page
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

                SqlDataAdapter da = new SqlDataAdapter("sp_getAllData", con);
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
                    no.Src = "https://elements-cover-images-0.imgix.net/41ce1856-ce64-47eb-9cc9-d50c75ba936b?auto=compress%2Cformat&w=900&fit=max&s=ba27396ca2b150afd778262eed2ec8af";
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        protected void claim_func(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand da = new SqlCommand("select postBy from found where fid= @fid", con);
                da.Parameters.AddWithValue("@fid", int.Parse(((Button)(sender)).CommandArgument.ToString()));
                SqlDataReader dr = da.ExecuteReader();
                if (dr.Read())
                {
                    if (dr["postBy"].ToString() != Session["email"].ToString())
                    { 
                    string fromMail = "lostandfoundhub3@gmail.com";
                    string fromPassword = "pmjcbaqjfotyrpng";
                    MailMessage msg = new MailMessage();
                    msg.From = new MailAddress(fromMail);
                    msg.Subject = "Claim_LostAndFoundHub";
                    msg.To.Add(new MailAddress(dr["postBy"].ToString()));
                    msg.Body = Session["email"].ToString() + " wants to claim the item you found. If the user contacted you and claimed the item please make sure to login to LostAndFoundHub to delete your post.";
                    var smtpClient = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential(fromMail, fromPassword),
                        EnableSsl = true,
                    };
                    smtpClient.Send(msg);
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Your details have been sent via an e-mail to the user of the found item');", true);
                }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('You can\\'t claim the item you have posted');", true);

                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            
        }
        protected void contact_func(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select name from users where email=(select postBy from found where fid=@fid)", con);
                cmd.Parameters.AddWithValue("@fid", int.Parse(((Button)(sender)).CommandArgument.ToString()));
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.Read()) 
                {
                    string inner = "Go to profile->chat and contact the user " + reader["name"];
                    string str = "alert('" + inner + " ');";
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert",str , true);
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