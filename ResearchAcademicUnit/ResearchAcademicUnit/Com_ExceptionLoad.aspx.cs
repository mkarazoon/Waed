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
    public partial class Com_ExceptionLoad : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userid"] == null || Session.IsNewSession)
                Response.Redirect("Login.aspx");

            if (!IsPostBack)
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmdInst = new SqlCommand("Select * From ResearcherInfo where Rstatus='IN'", conn);
                ddlSupName.DataSource = cmdInst.ExecuteReader();
                ddlSupName.DataTextField = "RaName";
                ddlSupName.DataValueField = "AcdId";
                ddlSupName.DataBind();
                ddlSupName.Items.Insert(0, "اختيار");
                ddlSupName.Items[0].Value = "";

                SqlCommand cmdStud = new SqlCommand("Select * From ResearcherInfo where Rstatus='IN'", conn);
                ddlStudName.DataSource = cmdStud.ExecuteReader();
                ddlStudName.DataTextField = "RaName";
                ddlStudName.DataValueField = "AcdId";
                ddlStudName.DataBind();
                ddlStudName.Items.Insert(0, "اختيار");
                ddlStudName.Items[0].Value = "";

                SqlCommand cmd = new SqlCommand("Select * from Com_Exception", conn);
                GridView1.DataSource = cmd.ExecuteReader();
                GridView1.DataBind();

                conn.Close();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand cmd = new SqlCommand("insert into Com_Exception values(@InstId,@InstName,@StudId,@StudName,@Reason,@ExceptionDate,1)", conn);
                cmd.Parameters.AddWithValue("@InstId", ddlSupName.SelectedValue);
                cmd.Parameters.AddWithValue("@InstName", ddlSupName.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@StudId", ddlStudName.SelectedValue);
                cmd.Parameters.AddWithValue("@StudName", ddlStudName.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Reason", txtReason.Text);
                cmd.Parameters.AddWithValue("@ExceptionDate", DateTime.Now);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("Select * from Com_Exception", conn);
                GridView1.DataSource = cmd.ExecuteReader();
                GridView1.DataBind();

                conn.Close();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('تم الحفظ بنجاح','success');", true);
                ddlSupName.SelectedValue = "";
                ddlStudName.SelectedValue = "";
                txtReason.Text = "";

                

            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('حصل خطأ اثناء التخزين','error');", true);
            }
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmdDel = new SqlCommand("Update Com_Exception Set Status=2 where AutoId="+row.Cells[0].Text, conn);
            cmdDel.ExecuteNonQuery();

            SqlCommand cmd = new SqlCommand("Select * from Com_Exception", conn);
            GridView1.DataSource = cmd.ExecuteReader();
            GridView1.DataBind();

            conn.Close();

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType==DataControlRowType.DataRow)
            {
                switch(e.Row.Cells[6].Text)
                {
                    case "1":
                        e.Row.Cells[5].Text = "مقبول";
                        break;
                    case "2":
                        e.Row.Cells[5].Text = "محذوف";
                        ((LinkButton)(e.Row.FindControl("lnkDelete"))).Visible = false;
                        break;
                }
            }
        }
    }
}