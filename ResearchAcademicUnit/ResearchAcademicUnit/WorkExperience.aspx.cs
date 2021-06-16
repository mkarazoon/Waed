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
    public partial class WorkExperience : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["MEUCV"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uid"] == null)
                Response.Redirect("Login.aspx");

            Session["backurl"] = "Default.aspx";
            ContentPlaceHolder cp = this.Master.Master.FindControl("ContentPlaceHolder1") as ContentPlaceHolder;
            HtmlGenericControl list = (HtmlGenericControl)cp.FindControl("menu15");//.FindControl("menu1");
            list.Attributes.Add("class", "ca-menu activeLi");
            if (!IsPostBack)
                getData();
        }

        protected void getData()
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand cmdTrain = new SqlCommand("select *,ROW_NUMBER() OVER(ORDER BY autoid ASC) AS serial from workexperience where jobid=" + Session["uid"], conn);
            GridView1.DataSource = cmdTrain.ExecuteReader();
            GridView1.DataBind();

            SqlCommand cmd1 = new SqlCommand("Select * From Country", conn);
            DataTable dtCountry = new DataTable();
            dtCountry.Load(cmd1.ExecuteReader());
            ddlCountry.DataSource = dtCountry;
            ddlCountry.DataTextField = "Name";
            ddlCountry.DataValueField = "Code"; ;
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, "حدد بلد العمل");
            ddlCountry.Items[0].Value = "0";

            conn.Close();
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(Session["saved"]))
            {
                lblMsg.Visible = false;
                Timer1.Enabled = false;
                Response.Redirect("WorkExperince.aspx");
            }
        }

        protected void btnSaveCert_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            try
            {
                SqlCommand cmdGet = new SqlCommand("Insert Into SectionsInserted values(15," + Session["uid"] + ")", conn);
                cmdGet.ExecuteNonQuery();
            }
            catch { }

            Response.Redirect("LinksDB.aspx");
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand cmd = new SqlCommand("Delete From workexperience where Autoid=" + row.Cells[0].Text, conn);
            cmd.ExecuteNonQuery();

            cmd = new SqlCommand("Insert Into SectionsUpdated values(15," + Session["uid"] + ",@du)", conn);
            cmd.Parameters.AddWithValue("@du", Convert.ToDateTime(DateTime.Now.Date));
            cmd.ExecuteNonQuery();

            conn.Close();
            Response.Redirect("WorkExperience.aspx");
        }

        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            lblUpdate.Text = row.Cells[0].Text;
            txtWorkPlace.Text=row.Cells[2].Text;
            ddlCountry.SelectedValue = row.Cells[3].Text;
            txtWorkDesc.Text = row.Cells[5].Text;
            
            txtFromDate.Text = row.Cells[6].Text;
            txtToDate.Text = row.Cells[7].Text;
            ddlCType.SelectedValue = row.Cells[8].Text;
            LinkButton1.Text = "تعديل";
        }

        protected void btnTrainSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand cmdGet = new SqlCommand("select * from workexperience where jobid=" + Session["uid"] + " and AutoId=" + lblUpdate.Text, conn);
                if (cmdGet.ExecuteReader().HasRows)
                {
                    cmdGet = new SqlCommand("delete from workexperience where jobid=" + Session["uid"] + " and AutoId=" + lblUpdate.Text, conn);
                    cmdGet.ExecuteNonQuery();

                    cmdGet = new SqlCommand("Insert Into SectionsUpdated values(15," + Session["uid"] + ",@du)", conn);
                    cmdGet.Parameters.AddWithValue("@du", Convert.ToDateTime(DateTime.Now.Date));
                    cmdGet.ExecuteNonQuery();
                    Session["saved"] = false;

                }

                SqlCommand cmd = new SqlCommand("insert into workexperience values(" +
                    "@instjob,@JobPlace,@JobCountryId,@JobCountry,@JobDesc,@FromDate,@ToDate,@LeaveReasonInt,@LeaveReason)", conn);

                cmd.Parameters.AddWithValue("@instjob", Session["uid"]);
                cmd.Parameters.AddWithValue("@JobPlace", txtWorkPlace.Text);
                cmd.Parameters.AddWithValue("@JobCountryId", ddlCountry.SelectedValue);
                cmd.Parameters.AddWithValue("@JobCountry", ddlCountry.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@JobDesc", txtWorkDesc.Text);
                cmd.Parameters.AddWithValue("@FromDate", txtFromDate.Text);
                cmd.Parameters.AddWithValue("@ToDate", txtToDate.Text);
                cmd.Parameters.AddWithValue("@LeaveReasonInt",ddlCType.SelectedValue );
                cmd.Parameters.AddWithValue("@LeaveReason", ddlCType.SelectedItem.Text);
                cmd.ExecuteNonQuery();

                SqlCommand cmdTrain = new SqlCommand("select *,ROW_NUMBER() OVER(ORDER BY autoid ASC) AS serial from workexperience where jobid=" + Session["uid"], conn);
                GridView1.DataSource = cmdTrain.ExecuteReader();
                GridView1.DataBind();

                conn.Close();
                Session["saved"] = true;

                Response.Redirect("WorkExperience.aspx");
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
            Response.Redirect("Qualifications.aspx");
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            Response.Redirect("LinksDB.aspx");
        }

        //protected void GridView1_DataBound(object sender, EventArgs e)
        //{
        //    string[] DBName = { "Science Citation Index -SCI", "Science Citation Index Extended - SCIE",
        //                "Thomson Reuters", "Scopus", "ERA", "EBSCO Abstract", "EconLit" };


        //    for (int j = 0; j < GridView1.Rows.Count; j++)
        //    {
        //        if (GridView1.Rows[j].Cells[9].Text == "2")
        //        {

        //            Label lbl = GridView1.Rows[j].Cells[12].FindControl("lblRName") as Label;
        //            string[] newData = lbl.Text.Split(',');
        //            lbl.Text = "";
        //            for (int i = 0; i < newData.Length; i++)
        //            {
        //                switch (newData[i])
        //                {
        //                    case "1":
        //                        lbl.Text += DBName[0] + ",";
        //                        break;
        //                    case "2":
        //                        lbl.Text += DBName[1] + ",";
        //                        break;
        //                    case "3":
        //                        lbl.Text += DBName[2] + ",";
        //                        break;
        //                    case "4":
        //                        lbl.Text += DBName[3] + ",";
        //                        break;
        //                    case "5":
        //                        lbl.Text += DBName[4] + ",";
        //                        break;
        //                    case "6":
        //                        lbl.Text += DBName[5] + ",";
        //                        break;
        //                    case "7":
        //                        lbl.Text += DBName[6] + ",";
        //                        break;
        //                }
        //            }
        //            lbl.Text = lbl.Text.Substring(0, lbl.Text.Length - 1);
        //        }
        //    }
        //}

    }
}