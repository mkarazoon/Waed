using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JoufUniProject
{
    public partial class Default : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userid"] == null || Session.IsNewSession)
            {
                Response.Redirect("Login.aspx");
            }

            Label1.Text = Session["userrole"].ToString();
            Session["PrintForm"] = null;
            Session["ResearchId"] = Session["userid"];

            if (Convert.ToInt16(Session["userid"])==2422 || Convert.ToInt16(Session["userid"]) == 2164)
            {
                CRLi.Visible = true;
                Div1.Visible = true;
                FeeSettingRole.Visible = true;
                Li1.Visible = true;
                Li2.Visible = true;
                uni2.Visible = true;
                //trainingDiv.Visible = true;
            }

            if (Convert.ToInt16(Session["userrole"]) != 6)
                Report.Visible = true;

            if (Convert.ToInt16(Session["userid"]) == 2189)
            {
                Li1.Visible = true;
                Li2.Visible = true;
            }

            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand("Select * from Com_Priviliges where PrivTo=" + Session["userid"], conn);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            //if(cmd.ExecuteReader().HasRows)
            if (Convert.ToInt16(Session["userid"]) == 2391
                || Convert.ToInt16(Session["userid"]) == 2189
                || Convert.ToInt16(Session["userid"]) == 2130
                || Convert.ToInt16(Session["userid"]) == 908
                || Convert.ToInt16(Session["userid"]) == 2422)
            {
                GSDiv.Visible = true;
                ad1.Visible = true;
                ad2.Visible = true;
                ad3.Visible = true;
                ad4.Visible = true;
                ad5.Visible = true;
                ad6.Visible = true;
                ad7.Visible = true;
                ad9.Visible = true;
                if (Convert.ToInt16(Session["userid"]) == 2130
                || Convert.ToInt16(Session["userid"]) == 908)
                {
                    Div1.Visible = false;
                    fileDiv.Visible = false;
                    scopusDiv.Visible = false;
                }
            }
            if(dt.Rows.Count!=0)
            {
                GSDiv.Visible = true;
            }

            if(Session["userrole"].ToString()=="8")
            {
                scopusDiv.Visible = false;
                fileDiv.Visible = false;
                Div1.Visible = false;
                ad3.Visible = true;
                ad8.Visible = false;
            }


            conn.Close();
        }
    }
}