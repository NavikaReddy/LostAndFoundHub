using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tensorflow.Keras.Layers;
using Jint.Parser.Ast;
using static System.Net.WebRequestMethods;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;

namespace LostAndFound
{
    public partial class Chat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if(!IsPostBack)
            {
                if (Request.QueryString["user"] != null)
                {
                    string selectedUser = HttpUtility.UrlDecode(Request.QueryString["user"]);
                    // Load chat messages for the selected user
                    LoadFriends();
                    Label1.Text = selectedUser;
                    ChatControls.Visible = true;
                    LoadChatbox(selectedUser);
                }
                else
                {
                    // Handle when there is no selected user parameter
                    // You might want to display a message or take some other action
                    LoadFriends();
                    ChatControls.Visible = false;
                }

            }
        }
        public void LoadFriends()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            string current = Session["name"].ToString();
            con.Open();
            SqlCommand cmd1 = new SqlCommand("select name,uid from users where name!=@name", con);
            cmd1.Parameters.AddWithValue("@name", current);
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataList1.DataSource = ds;
            DataList1.DataBind();
            con.Close();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            LinkButton lBtn = sender as LinkButton;
            string id = ((LinkButton)sender).CommandArgument.ToString();
            Label1.Text = lBtn.Text;
            ChatControls.Visible = true;
            LoadChatbox(Label1.Text);
            
        }

        public void LoadChatbox(string s)
        {
            string name = Session["name"].ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            con.Open();
            string str = "select * from chat where Sender ='" + name + "' and Receiver ='" + s + "' or Sender ='" + Label1.Text + "' and Receiver ='" + name + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataList2.DataSource = ds;
            DataList2.DataBind();
            con.Close();
        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            LoadChatbox(Label1.Text);
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string d1 = DateTime.Now.ToString("MM-dd-yyyy");
            string name = Session["name"].ToString();
            string query = "insert into chat(Sender,Receiver,Message,Date) values('" + name + "','" + Label1.Text + "','" + TextBox1.Text + "','" + d1 + "')";

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    int i = cmd.ExecuteNonQuery();
                    if (i == 1)
                    {
                        TextBox1.Text = "";
                    }
                }
            }
        }

        //        using (SqlCommand da = new SqlCommand("select email from users where name=@name", con))
        //        {
        //            da.Parameters.AddWithValue("@name", Label1.Text);
        //            SqlDataReader dr = da.ExecuteReader();

        //            if (dr.Read())
        //            {
        //                string fromMail = "lostandfoundhub3@gmail.com";
        //                string fromPassword = "pmjcbaqjfotyrpng";
        //                MailMessage msg = new MailMessage();
        //                msg.From = new MailAddress(fromMail);
        //                msg.Subject = "Messages_LostAndFound Hub";
        //                MailAddress ma = new MailAddress(dr["email"].ToString());
        //                msg.To.Add(ma);
        //                msg.Body = "You have received a new message from " + Session["name"].ToString();

        //                // Offload asynchronous task to a different context
        //                Task.Run(async () => await SendEmailAsync(msg, fromMail, fromPassword)).Wait();
        //            }
        //        }
        //    }
        //}

        //private async Task SendEmailAsync(MailMessage msg, string fromMail, string fromPassword)
        //{
        //    var smtpClient = new SmtpClient("smtp.gmail.com")
        //    {
        //        Port = 587,
        //        Credentials = new NetworkCredential(fromMail, fromPassword),
        //        EnableSsl = true,
        //    };

        //    // Send email asynchronously
        //    await smtpClient.SendMailAsync(msg);
        //}


    }
}