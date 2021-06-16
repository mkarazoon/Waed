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
    public partial class WorkShopSeminarForm : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);
        DataTable dtRInfo = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.IsNewSession || Session["uid"] == null)
                Response.Redirect("Login.aspx");

            if (!IsPostBack)
            {
                //hfResearchId.Value = Session["ResearchId"].ToString();
                dtRInfo.Columns.Add("Serial");
                dtRInfo.Columns.Add("ReName");
                Session["dtRInfo"] = dtRInfo;

                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmdd = new SqlCommand("select * from ResearcherInfo where acdid=" + Session["ResearchId"], conn);
                DataTable dtt = new DataTable();
                dtt.Load(cmdd.ExecuteReader());
                if (dtt.Rows.Count != 0)
                {
                    
                    
                    
                    lblFaculty.Text = dtt.Rows[0]["College"].ToString();
                    lblDept.Text = dtt.Rows[0]["Dept"].ToString();
                    lblDegree.Text = dtt.Rows[0]["RLevel"].ToString();
                }
            }
        }
    }
}