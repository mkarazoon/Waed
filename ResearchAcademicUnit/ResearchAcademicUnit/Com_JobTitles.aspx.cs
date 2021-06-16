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
    public partial class Com_JobTitles : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userid"] == null || Session.IsNewSession)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("Select * from Com_JobTitle", conn);
                GridView1.DataSource = cmd.ExecuteReader();
                GridView1.DataBind();

                conn.Close();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand("Insert into Com_JobTitle values(@Aname,@Ename)", conn);
            cmd.Parameters.AddWithValue("@Aname", txtAName.Text);
            cmd.Parameters.AddWithValue("@Ename", txtEName.Text);
            cmd.ExecuteNonQuery();
                
                
            cmd = new SqlCommand("Select * from Com_JobTitle", conn);
            GridView1.DataSource = cmd.ExecuteReader();
            GridView1.DataBind();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('تم الحفظ بنجاح','success');", true);
            conn.Close();
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;

            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand("Update Com_JobTitle where Autoid=" + row.Cells[0].Text, conn);
            cmd.ExecuteNonQuery();


            cmd = new SqlCommand("Select * from Com_JobTitle", conn);
            GridView1.DataSource = cmd.ExecuteReader();
            GridView1.DataBind();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('تم حذف قاعدة البيانات','info');", true);
            conn.Close();
        }
    }
}