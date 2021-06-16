using Oracle.ManagedDataAccess.Client;
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
    public partial class Admin_SupervisorReport : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);
        static string OracleConnString = System.Configuration.ConfigurationManager.ConnectionStrings["orcleConStr"].ConnectionString;
        OracleConnection conn1 = new OracleConnection(OracleConnString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userid"] == null || Session.IsNewSession)
                Response.Redirect("Login.aspx");

            if(!IsPostBack)
            {
                if (conn1.State == ConnectionState.Broken || conn1.State == ConnectionState.Closed)
                    conn1.Open();

                OracleCommand cmd = new OracleCommand(@"select * From meu_new.all_students_karz where Thesis_semester<>0", conn1);
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dtSuper = new DataTable();
                da.Fill(dtSuper);


                ddlSupervisor.DataSource = dtSuper;
                ddlSupervisor.DataTextField = "Single_Supervisor";
                ddlSupervisor.DataValueField = "Single_Supervisor";
                ddlSupervisor.DataBind();
                ddlSupervisor.Items.Insert(0, "تحديد المشرف");
                ddlSupervisor.Items[0].Value = "";

                GridView1.DataSource = dtSuper;
                GridView1.DataBind();

                conn1.Close();
            }
        }

        protected void ddlSupervisor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}