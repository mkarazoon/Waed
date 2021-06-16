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
    public partial class ThesisSup : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["MEUCV"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uid"] == null)
                Response.Redirect("Login.aspx");

            //Label lbl = (Label)Master.FindControl("lblWelcome");
            //lbl.Text = "خبرات الاشراف على الرسائل داخل الجامعة";
            if (!IsPostBack)
                getData();
        }

        protected void getData()
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmdTrain = new SqlCommand("select *,ROW_NUMBER() OVER(ORDER BY autoid ASC) AS serial from ThesisInfo where jobid=" + Session["uid"], conn);
            GridView1.DataSource = cmdTrain.ExecuteReader();
            GridView1.DataBind();
            conn.Close();
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(Session["saved"]))
            {
                lblMsg.Visible = false;
                Timer1.Enabled = false;
                Response.Redirect("ThesisSup.aspx");
            }
        }

        protected void btnSaveCert_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            try
            {
                    SqlCommand cmdGet = new SqlCommand("Insert Into SectionsInserted values(16," + Session["uid"] + ")", conn);
                    cmdGet.ExecuteNonQuery();
            }
            catch { }

            Response.Redirect("ThesisSup.aspx");
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand cmd = new SqlCommand("Delete From ThesisInfo where Autoid=" + row.Cells[0].Text, conn);
            cmd.ExecuteNonQuery();

            cmd = new SqlCommand("Insert Into SectionsUpdated values(16," + Session["uid"] + ",@du)", conn);
            cmd.Parameters.AddWithValue("@du", Convert.ToDateTime(DateTime.Now.Date));
            cmd.ExecuteNonQuery();

            conn.Close();
            Response.Redirect("ThesisSup.aspx");
        }

        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            lblUpdate.Text = row.Cells[0].Text;
            txtThTitleA.Text = row.Cells[2].Text;
            txtThTitleE.Text = row.Cells[3].Text;
            ddlLang.SelectedValue= row.Cells[4].Text;
            txtUni.Text = row.Cells[6].Text;
            txtAcdYear.Text = row.Cells[7].Text;
            ddlPubStatus.SelectedValue = row.Cells[8].Text;
            txtStudName.Text = row.Cells[10].Text;
            LinkButton1.Text = "تعديل";
        }

        protected void btnTrainSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand cmdGet = new SqlCommand("select * from ThesisInfo where jobid=" + Session["uid"] + " and AutoId=" + lblUpdate.Text, conn);
                if (cmdGet.ExecuteReader().HasRows)
                {
                    cmdGet = new SqlCommand("delete from ThesisInfo where jobid=" + Session["uid"] + " and AutoId=" + lblUpdate.Text, conn);
                    cmdGet.ExecuteNonQuery();

                    cmdGet = new SqlCommand("Insert Into SectionsUpdated values(16," + Session["uid"] + ",@du)", conn);
                    cmdGet.Parameters.AddWithValue("@du", Convert.ToDateTime(DateTime.Now.Date));
                    cmdGet.ExecuteNonQuery();
                    Session["saved"] = false;

                }

                SqlCommand cmd = new SqlCommand("insert into ThesisInfo values(" +
                    "@instjob,@ThesisTitleA,@ThesisTitleE,@ThesisLangInt,@ThesisLang,@University,@AcdYear,@typeint,@typeName,@StudName)", conn);

                cmd.Parameters.AddWithValue("@instjob", Session["uid"]);
                cmd.Parameters.AddWithValue("@ThesisTitleA", txtThTitleA.Text);
                cmd.Parameters.AddWithValue("@ThesisTitleE", txtThTitleE.Text);
                cmd.Parameters.AddWithValue("@ThesisLangInt", ddlLang.SelectedValue);
                cmd.Parameters.AddWithValue("@ThesisLang", ddlLang.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@University", txtUni.Text);
                cmd.Parameters.AddWithValue("@AcdYear", txtAcdYear.Text);
                cmd.Parameters.AddWithValue("@typeint", ddlPubStatus.SelectedValue);
                cmd.Parameters.AddWithValue("@typeName", ddlPubStatus.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@StudName", txtStudName.Text);
                cmd.ExecuteNonQuery();

                SqlCommand cmdTrain = new SqlCommand("select *,ROW_NUMBER() OVER(ORDER BY autoid ASC) AS serial from ThesisInfo where jobid=" + Session["uid"], conn);
                GridView1.DataSource = cmdTrain.ExecuteReader();
                GridView1.DataBind();

                conn.Close();
                Session["saved"] = true;
                lblMsg.Visible = true;
                lblMsg.Text = "تم التخزين بنجاح";
                Timer1.Enabled = true;

                Response.Redirect("ThesisSup.aspx");
            }
            catch
            {
                lblMsg.Visible = true;
                lblMsg.Text = "حصل خطأ أثناء التخزين";
                Timer1.Enabled = true;
                Session["saved"] = false;
            }

        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            Response.Redirect("RCommittees.aspx");
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReCertificate.aspx");
        }
    }
}