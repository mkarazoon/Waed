using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchAcademicUnit
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["backurl"] = Session["webformpathurl"];

            if (!IsPostBack)
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();
                string id = Request.QueryString["id"];

                SqlCommand cmd = new SqlCommand("Select OutLine From CourseInfo where CourseId=" + id, conn);
                DataTable dt = new DataTable();

                dt.Load(cmd.ExecuteReader());
                if (dt.Rows[0][0].ToString() != "")
                {
                    OutLineDiv.InnerHtml = dt.Rows[0][0].ToString();
                }
                else
                    OutLineDiv.InnerText = "غير متاحة حاليا";
            }


        }
    }
}