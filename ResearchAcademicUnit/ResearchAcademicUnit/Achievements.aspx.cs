using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ResearchAcademicUnit
{
    public partial class Achievements : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["MEUCV"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uid"] == null)
                Response.Redirect("Login.aspx");
            Session["backurl"] = "Default.aspx";
            ContentPlaceHolder cp = this.Master.Master.FindControl("ContentPlaceHolder1") as ContentPlaceHolder;
            HtmlGenericControl list = (HtmlGenericControl)cp.FindControl("menu10");//.FindControl("menu1");
            list.Attributes.Add("class", "ca-menu activeLi");

            //Label lbl = (Label)Master.FindControl("lblWelcome");
            //lbl.Text = "الانجازات الابتكارية والريادية";
            if (!IsPostBack)
                getData();
        }

        protected void getData()
        {

            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand cmdTrain = new SqlCommand("select *,ROW_NUMBER() OVER(ORDER BY autoid ASC) AS serial from Achievement where jobid=" + Session["uid"], conn);
            GridView1.DataSource = cmdTrain.ExecuteReader();
            GridView1.DataBind();
            conn.Close();
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            //if (Convert.ToBoolean(Session["saved"]))
           // {
                lblMsg.Visible = false;
                Timer1.Enabled = false;
                Response.Redirect("Achievements.aspx");
            //}
        }

        protected void btnSaveCert_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            try
            {
                SqlCommand cmdGet = new SqlCommand("Insert Into SectionsInserted values(12," + Session["uid"] + ")", conn);
                cmdGet.ExecuteNonQuery();
            }
            catch { }

            Response.Redirect("RCommittees.aspx");
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand cmd = new SqlCommand("Delete From Achievement where Autoid=" + row.Cells[0].Text, conn);
            cmd.ExecuteNonQuery();

            cmd = new SqlCommand("Insert Into SectionsUpdated values(11," + Session["uid"] + ",@du)", conn);
            cmd.Parameters.AddWithValue("@du", Convert.ToDateTime(DateTime.Now.Date));
            cmd.ExecuteNonQuery();

            conn.Close();
            Response.Redirect("Achievements.aspx");
        }

        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            lblUpdate.Text = row.Cells[0].Text;
            ddlActType.SelectedValue = row.Cells[2].Text;
            txtAName.Text = row.Cells[4].Text;
            txtEName.Text = row.Cells[5].Text;
            txtAAbstract.Text = row.Cells[6].Text;
            txtEAbstract.Text = row.Cells[7].Text;
            LinkButton1.Text = "تعديل";
        }

        protected void btnTrainSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(ddlActType.SelectedValue == "0" &&
                    txtAName.Text.Trim() == "" &&
                    txtAAbstract.Text.Trim() == "" &&
                    txtEName.Text.Trim() == "" &&
                    txtEAbstract.Text.Trim() == ""))
                {
                    if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                        conn.Open();
                    SqlCommand cmdGet = new SqlCommand("select * from Achievement where jobid=" + Session["uid"] + " and AutoId=" + lblUpdate.Text, conn);
                    if (cmdGet.ExecuteReader().HasRows)
                    {
                        cmdGet = new SqlCommand("delete from Achievement where jobid=" + Session["uid"] + " and AutoId=" + lblUpdate.Text, conn);
                        cmdGet.ExecuteNonQuery();

                        cmdGet = new SqlCommand("Insert Into SectionsUpdated values(11," + Session["uid"] + ",@du)", conn);
                        cmdGet.Parameters.AddWithValue("@du", Convert.ToDateTime(DateTime.Now.Date));
                        cmdGet.ExecuteNonQuery();
                        Session["saved"] = false;

                    }

                    SqlCommand cmd = new SqlCommand("insert into Achievement values(" +
                        "@instjob,@ActTypeI,@ActTypeS,@AName,@EName,@AAbstarct,@EAbstarct)", conn);

                    cmd.Parameters.AddWithValue("@instjob", Session["uid"]);
                    cmd.Parameters.AddWithValue("@ActTypeI", ddlActType.SelectedValue);
                    cmd.Parameters.AddWithValue("@ActTypeS", ddlActType.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@AName", txtAName.Text);
                    cmd.Parameters.AddWithValue("@EName", txtEName.Text);
                    cmd.Parameters.AddWithValue("@AAbstarct", txtAAbstract.Text);
                    cmd.Parameters.AddWithValue("@EAbstarct", txtEAbstract.Text);
                    cmd.ExecuteNonQuery();

                    SqlCommand cmdTrain = new SqlCommand("select *,ROW_NUMBER() OVER(ORDER BY autoid ASC) AS serial from Achievement where jobid=" + Session["uid"], conn);
                    GridView1.DataSource = cmdTrain.ExecuteReader();
                    GridView1.DataBind();

                    conn.Close();
                    Session["saved"] = true;
                    lblMsg.Visible = true;
                    lblMsg.Text = "تم التخزين بنجاح";
                    Timer1.Enabled = true;
                    //LinkButton1.Text = "إضافة";
                    Response.Redirect("Achievements.aspx");
                }
                else
                {
                    Session["saved"] = false;
                    lblMsg.Visible = true;
                    lblMsg.Text = "لا يوجد ما يتم تخزينه";
                    Timer1.Enabled = true;

                }
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
            Response.Redirect("WorkShops.aspx");
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            Response.Redirect("RCommittees.aspx");
        }
    }
}