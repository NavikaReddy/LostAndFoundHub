using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Net.Mail;
using System.Net;

namespace LostAndFound
{
    public partial class SearchFound : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                GetData();
            }
        }
        public void GetData()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select distinct fname from found", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ddlprod.Items.Clear();
                ddlprod.Items.Add("--Select--");
                ddlprod.DataTextField = "fname";
                ddlprod.DataValueField = "fname";
                ddlprod.DataSource = dt;
                ddlprod.DataBind();
                da = new SqlDataAdapter("select distinct ffoundAt from found", con);
                dt = new DataTable();
                da.Fill(dt);
                ddlplace.Items.Clear();
                ddlplace.Items.Add("--Select--");
                ddlplace.DataTextField = "ffoundAt";
                ddlplace.DataValueField = "ffoundAt";
                ddlplace.DataSource = dt;
                ddlplace.DataBind(); 
                da = new SqlDataAdapter("select distinct fdate from found", con);
                dt = new DataTable();
                da.Fill(dt);
                ddldate.Items.Clear();
                ddldate.Items.Add("--Select--");
                ddldate.DataTextField = "fdate";
                ddldate.DataValueField = "fdate";
                ddldate.DataSource = dt;
                ddldate.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            userRepeater.DataSource = null;
            userRepeater.DataBind();
            noContainer.Visible = false;

            string selectedProduct = ddlprod.SelectedValue;
            string selectedPlace = ddlplace.SelectedValue;
            string selectedDate = ddldate.SelectedValue;

            // Validate and parse the date
            DateTime selectedDateTime;
            string selectedDateFormatted = null;
            if (!string.IsNullOrEmpty(selectedDate) && DateTime.TryParse(selectedDate, out selectedDateTime))
            {
                // Use the parsed date in the desired format
                selectedDateFormatted = selectedDateTime.ToString("yyyy-MM-dd");
            }

            string query = "SELECT * FROM found WHERE 1 = 1";

            // Exclude "--Select--" values from the conditions
            if (!string.IsNullOrEmpty(selectedProduct) && selectedProduct != "--Select--")
                query += " AND fname = @fname";

            if (!string.IsNullOrWhiteSpace(selectedPlace) && selectedPlace != "--Select--")
                query += " AND ffoundAt = @ffoundAt";

            if (!string.IsNullOrEmpty(selectedDateFormatted) && selectedDate != "--Select--")
                query += " AND fdate = @fdate";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            try
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(query, con);

                // Exclude parameters for "--Select--" values
                if (!string.IsNullOrEmpty(selectedProduct) && selectedProduct != "--Select--")
                    da.SelectCommand.Parameters.AddWithValue("@fname", selectedProduct);

                if (!string.IsNullOrWhiteSpace(selectedPlace) && selectedPlace != "--Select--")
                    da.SelectCommand.Parameters.AddWithValue("@ffoundAt", selectedPlace);

                if (!string.IsNullOrEmpty(selectedDateFormatted) && selectedDate != "--Select--")
                    da.SelectCommand.Parameters.AddWithValue("@fdate", selectedDateFormatted);

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
            finally
            {
                con.Close();
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
                if (reader.Read())
                {
                    string databaseName = reader["name"].ToString().Trim();
                    string sessionName = Session["name"].ToString().Trim();

                    if (!String.Equals(databaseName, sessionName, StringComparison.OrdinalIgnoreCase))
                    {
                        Response.Redirect("Chat.aspx?user=" + HttpUtility.UrlEncode(databaseName));
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('You posted this item');", true);
                    }
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