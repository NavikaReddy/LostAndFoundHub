using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using static System.Net.WebRequestMethods;
using System.Net.Mail;
using System.Net;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace LostAndFound
{
    public partial class LostItems : System.Web.UI.Page
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

                SqlDataAdapter da = new SqlDataAdapter("sp_getAllDataLost", con);
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
        
    }
}