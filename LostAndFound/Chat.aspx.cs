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

namespace LostAndFound
{
    public partial class Chat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LoadFriends();
                ChatControls.Visible = false;
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
            LoadChatbox();
            
        }

        public void LoadChatbox()
        {
            string name = Session["name"].ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            con.Open();
            string str = "select * from chat where Sender ='" + name + "' and Receiver ='" + Label1.Text + "' or Sender ='" + Label1.Text + "' and Receiver ='" + name + "'";
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
            LoadChatbox();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string d1 = DateTime.Now.ToString("MM-dd-yyyy");
            string name = Session["name"].ToString();
            string query = "insert into chat values('" + name + "','" + Label1.Text + "','" + TextBox1.Text + "','" + d1 + "')";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            int i = cmd.ExecuteNonQuery();
            if (i == 1)
            {
                TextBox1.Text = "";
            }
        }
    }
}