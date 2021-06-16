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
    public partial class ResearchInterests : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["MEUCV"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uid"] == null)
                Response.Redirect("Login.aspx");

            Session["backurl"] = "Default.aspx";
            ContentPlaceHolder cp = this.Master.Master.FindControl("ContentPlaceHolder1") as ContentPlaceHolder;
            HtmlGenericControl list = (HtmlGenericControl)cp.FindControl("menu3");//.FindControl("menu1");
            list.Attributes.Add("class", "ca-menu activeLi");

            //Label lbl = (Label)Master.FindControl("lblWelcome");
            //lbl.Text = "الاهتمامات البحثية";
            if (!IsPostBack)
                getData();
        }

        protected void getData()
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand cmdTrain = new SqlCommand("select *,ROW_NUMBER() OVER(ORDER BY autoid ASC) AS serial from ResearchInterest where jobid=" + Session["uid"], conn);
            GridView1.DataSource = cmdTrain.ExecuteReader();
            GridView1.DataBind();


            cmdTrain = new SqlCommand("select *,ROW_NUMBER() OVER(ORDER BY autoid ASC) AS serial from Needed where jobid=" + Session["uid"], conn);
            GridView2.DataSource = cmdTrain.ExecuteReader();
            GridView2.DataBind();

            cmdTrain = new SqlCommand("select *,ROW_NUMBER() OVER(ORDER BY autoid ASC) AS serial from Thesis where jobid=" + Session["uid"], conn);
            GridView3.DataSource = cmdTrain.ExecuteReader();
            GridView3.DataBind();

            conn.Close();

        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(Session["saved"]))
            {
                lblMsg.Visible = false;
                Timer1.Enabled = false;
                //Response.Redirect("ResearchInterests.aspx");
            }
        }

        protected void btnSaveCert_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            //SqlCommand cmdGet = new SqlCommand("select * from Needed where jobid=" + Session["uid"], conn);

            if (GridView1.Rows.Count >= 1 && GridView2.Rows.Count >= 1 && GridView3.Rows.Count >= 1)
            {
                try
                {
                    if (Convert.ToBoolean(Session["saved"]))
                    {
                        SqlCommand cmdGet = new SqlCommand("Insert Into SectionsInserted values(3," + Session["uid"] + ")", conn);
                        cmdGet.ExecuteNonQuery();
                    }
                }
                catch { }
                lblInterestErr.Visible = false;
                Response.Redirect("Qualifications.aspx");
            }
            else
            {
                lblInterestErr.Visible = true;
                txtRInter.Focus();
                ddlRSector.Focus();
            }
        }

        protected void ddlRSector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRSector.SelectedValue != "0")
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand cmd = new SqlCommand("Select * From RFields where RSector=" + ddlRSector.SelectedValue, conn);
                ddlField.DataSource = cmd.ExecuteReader();
                ddlField.DataValueField = "id";
                ddlField.DataTextField = "RField";
                ddlField.DataBind();
                ddlField.Items.Insert(0, "حدد المجال البحثي");
                ddlField.Items[0].Value = "0";
                conn.Close();
            }
            else
            {
                ddlField.Items.Clear();
            }
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand cmd = new SqlCommand("Delete From Needed where Autoid=" + row.Cells[0].Text, conn);
            cmd.ExecuteNonQuery();

            cmd = new SqlCommand("Insert Into SectionsUpdated values(2," + Session["uid"] + ",@du)", conn);
            cmd.Parameters.AddWithValue("@du", Convert.ToDateTime(DateTime.Now.Date));
            cmd.ExecuteNonQuery();

            conn.Close();
            Response.Redirect("ResearchInterests.aspx");
        }

        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            ddlField1.SelectedValue = row.Cells[2].Text;
            txtNeed1.Text = row.Cells[4].Text;
            //txtRInter.Text = row.Cells[6].Text;
            //ddlRSector.SelectedValue = row.Cells[2].Text;

            //if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
            //    conn.Open();
            //SqlCommand cmd = new SqlCommand("Select * From Needed where RSector=" + ddlRSector.SelectedValue, conn);
            //ddlField.DataSource = cmd.ExecuteReader();
            //ddlField.DataValueField = "id";
            //ddlField.DataTextField = "RField";
            //ddlField.DataBind();
            //ddlField.Items.Insert(0, "حدد المجال البحثي");
            //ddlField.Items[0].Value = "0";

            //ddlField.SelectedValue = row.Cells[4].Text;
            lblUpdate.Text= row.Cells[0].Text;
            LinkButton2.Text = "تعديل";

        }

        protected void btnTrainSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmdGet = new SqlCommand("select * from ResearchInterest where jobid=" + Session["uid"] + " and AutoId=" + lblUpdate.Text, conn);
                if (cmdGet.ExecuteReader().HasRows)
                {
                    cmdGet = new SqlCommand("delete from ResearchInterest where jobid=" + Session["uid"] + " and AutoId=" + lblUpdate.Text, conn);
                    cmdGet.ExecuteNonQuery();

                    cmdGet = new SqlCommand("Insert Into SectionsUpdated values(2," + Session["uid"] + ",@du)", conn);
                    cmdGet.Parameters.AddWithValue("@du", Convert.ToDateTime(DateTime.Now.Date));
                    cmdGet.ExecuteNonQuery();
                    Session["saved"] = false;

                }
                string[] interest = txtRInter.Text.Split(';');
                for (int i = 0; i < interest.Length; i++)
                {
                    if (interest[i] != "")
                    {
                        SqlCommand cmd = new SqlCommand("insert into ResearchInterest values(" +
                            "@instjob,@ri,@rs,@rsn,@rf,@rfn)", conn);

                        cmd.Parameters.AddWithValue("@instjob", Session["uid"]);
                        cmd.Parameters.AddWithValue("@ri", interest[i]);
                        cmd.Parameters.AddWithValue("@rs", ddlRSector.SelectedValue);
                        cmd.Parameters.AddWithValue("@rsn", ddlRSector.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@rf", ddlField.SelectedValue);
                        cmd.Parameters.AddWithValue("@rfn", ddlField.SelectedItem.Text);
                        cmd.ExecuteNonQuery();
                    }
                }

                SqlCommand cmdTrain = new SqlCommand("select *,ROW_NUMBER() OVER(ORDER BY autoid ASC) AS serial from ResearchInterest where jobid=" + Session["uid"], conn);
                GridView1.DataSource = cmdTrain.ExecuteReader();
                GridView1.DataBind();

                conn.Close();
                Session["saved"] = true;
                //lblMsg.Visible = true;
                //lblMsg.Text = "تمت الاضافة بنجاح";
                //Timer1.Enabled = true;
                lblInterestErr.Visible = false;


                txtRInter.Text = "";
                ddlRSector.SelectedValue = "0";
                lblUpdate.Text = "0";
                ddlField.Items.Clear();
            }
            catch
            {
                lblMsg.Visible = true;
                lblMsg.Text = "حصل خطأ أثناء التخزين";
                Timer1.Enabled = true;
                Session["saved"] = false;
            }

        }

        //protected void btnSaveNeeded_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
        //            conn.Open();

        //        SqlCommand cmdGet = new SqlCommand("select * from Needed where jobid=" + Session["uid"], conn);
        //        if (cmdGet.ExecuteReader().HasRows)
        //        {
        //            cmdGet = new SqlCommand("delete from Needed where jobid=" + Session["uid"], conn);
        //            cmdGet.ExecuteNonQuery();

        //            cmdGet = new SqlCommand("Insert Into SectionsUpdated values(3," + Session["uid"] + ",@du)", conn);
        //            cmdGet.Parameters.AddWithValue("@du", Convert.ToDateTime(DateTime.Now.Date));
        //            cmdGet.ExecuteNonQuery();
        //            Session["saved"] = false;

        //        }

        //        SqlCommand cmd = new SqlCommand("insert into Needed values(" +
        //            "@instjob,1,@rs,@f)", conn);

        //        cmd.Parameters.AddWithValue("@instjob", Session["uid"]);
        //        cmd.Parameters.AddWithValue("@rs", txtNeed1.Text);
        //        cmd.Parameters.AddWithValue("@f", ddlField1.SelectedValue);
        //        cmd.ExecuteNonQuery();

        //        if(txtNeed2.Text!="")
        //        {
        //            SqlCommand cmd2 = new SqlCommand("insert into Needed values(" +
        //                "@instjob,2,@rs,@f)", conn);

        //            cmd2.Parameters.AddWithValue("@instjob", Session["uid"]);
        //            cmd2.Parameters.AddWithValue("@rs", txtNeed2.Text);
        //            cmd2.Parameters.AddWithValue("@f", ddlField2.SelectedValue);
        //            cmd2.ExecuteNonQuery();
        //        }

        //        if (txtNeed3.Text != "")
        //        {
        //            SqlCommand cmd3 = new SqlCommand("insert into Needed values(" +
        //                "@instjob,3,@rs,@f)", conn);

        //            cmd3.Parameters.AddWithValue("@instjob", Session["uid"]);
        //            cmd3.Parameters.AddWithValue("@rs", txtNeed3.Text);
        //            cmd3.Parameters.AddWithValue("@f", ddlField3.SelectedValue);
        //            cmd3.ExecuteNonQuery();
        //        }
        //        conn.Close();
        //        Session["saved"] = true;
        //        lblMsg.Visible = true;
        //        lblMsg.Text = "تم التخزين بنجاح";
        //        Timer1.Enabled = true;


        //        txtRInter.Text = "";
        //        ddlRSector.SelectedValue = "0";
        //        lblUpdate.Text = "0";
        //        ddlField.Items.Clear();
        //    }
        //    catch
        //    {
        //        lblMsg.Visible = true;
        //        lblMsg.Text = "حصل خطأ أثناء التخزين";
        //        Timer1.Enabled = true;
        //        Session["saved"] = false;
        //    }
        //}

        protected void btnAddNeeded_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmdGet = new SqlCommand("select * from Needed where jobid=" + Session["uid"] + " and AutoId=" + lblUpdateNeed.Text, conn);
                if (cmdGet.ExecuteReader().HasRows)
                {
                    cmdGet = new SqlCommand("delete from Needed where jobid=" + Session["uid"] + " and AutoId=" + lblUpdateNeed.Text, conn);
                    cmdGet.ExecuteNonQuery();

                    cmdGet = new SqlCommand("Insert Into SectionsUpdated values(3," + Session["uid"] + ",@du)", conn);
                    cmdGet.Parameters.AddWithValue("@du", Convert.ToDateTime(DateTime.Now.Date));
                    cmdGet.ExecuteNonQuery();
                    Session["saved"] = false;

                }

                SqlCommand cmd = new SqlCommand("insert into Needed values(" +
                    "@instjob,1,@rs,@f,@fs)", conn);

                cmd.Parameters.AddWithValue("@instjob", Session["uid"]);
                cmd.Parameters.AddWithValue("@rs", txtNeed1.Text);
                cmd.Parameters.AddWithValue("@f", ddlField1.SelectedValue);
                cmd.Parameters.AddWithValue("@fs", ddlField1.SelectedItem.Text);
                cmd.ExecuteNonQuery();

                SqlCommand cmdTrain = new SqlCommand("select *,ROW_NUMBER() OVER(ORDER BY autoid ASC) AS serial from Needed where jobid=" + Session["uid"], conn);
                GridView2.DataSource = cmdTrain.ExecuteReader();
                GridView2.DataBind();

                conn.Close();
                Session["saved"] = true;
                lblMsg.Visible = true;
                lblMsg.Text = "تمت الاضافة بنجاح";
                Timer1.Enabled = true;


                txtNeed1.Text = "";
                ddlField1.SelectedValue = "0";
                lblUpdateNeed.Text = "0";
            }
            catch
            {
                lblMsg.Visible = true;
                lblMsg.Text = "حصل خطأ أثناء التخزين";
                Timer1.Enabled = true;
                Session["saved"] = false;
            }
        }

        protected void btnAddthesis_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmdGet = new SqlCommand("select * from Thesis where jobid=" + Session["uid"] + " and AutoId=" + lblUpdateThesis.Text, conn);
                if (cmdGet.ExecuteReader().HasRows)
                {
                    cmdGet = new SqlCommand("delete from Thesis where jobid=" + Session["uid"] + " and AutoId=" + lblUpdateThesis.Text, conn);
                    cmdGet.ExecuteNonQuery();

                    cmdGet = new SqlCommand("Insert Into SectionsUpdated values(3," + Session["uid"] + ",@du)", conn);
                    cmdGet.Parameters.AddWithValue("@du", Convert.ToDateTime(DateTime.Now.Date));
                    cmdGet.ExecuteNonQuery();
                    Session["saved"] = false;

                }

                SqlCommand cmd = new SqlCommand("insert into Thesis values(" +
                    "@instjob,@rs)", conn);

                cmd.Parameters.AddWithValue("@instjob", Session["uid"]);
                cmd.Parameters.AddWithValue("@rs", txtThesisTitle.Text);
                cmd.ExecuteNonQuery();

                SqlCommand cmdTrain = new SqlCommand("select *,ROW_NUMBER() OVER(ORDER BY autoid ASC) AS serial from Thesis where jobid=" + Session["uid"], conn);
                GridView3.DataSource = cmdTrain.ExecuteReader();
                GridView3.DataBind();

                conn.Close();
                Session["saved"] = true;
                lblMsg.Visible = true;
                lblMsg.Text = "تمت الاضافة بنجاح";
                Timer1.Enabled = true;


                txtThesisTitle.Text = "";
                lblUpdateThesis.Text = "0";
            }
            catch
            {
                lblMsg.Visible = true;
                lblMsg.Text = "حصل خطأ أثناء التخزين";
                Timer1.Enabled = true;
                Session["saved"] = false;
            }
        }

        protected void lnkDeleteTh_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand cmd = new SqlCommand("Delete From Thesis where Autoid=" + row.Cells[0].Text, conn);
            cmd.ExecuteNonQuery();

            cmd = new SqlCommand("Insert Into SectionsUpdated values(3," + Session["uid"] + ",@du)", conn);
            cmd.Parameters.AddWithValue("@du", Convert.ToDateTime(DateTime.Now.Date));
            cmd.ExecuteNonQuery();

            conn.Close();
            Response.Redirect("ResearchInterests.aspx");
        }

        protected void lnkUpdateTh_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            txtThesisTitle.Text = row.Cells[2].Text;
            lblUpdateThesis.Text = row.Cells[0].Text;
        }

        protected void lnkUpdateNeed_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            ddlField1.SelectedValue= row.Cells[2].Text;
            txtNeed1.Text = row.Cells[4].Text;
            lblUpdateNeed.Text = row.Cells[0].Text;
        }

        protected void lnkDeleteNeed_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand cmd = new SqlCommand("Delete From Needed where Autoid=" + row.Cells[0].Text, conn);
            cmd.ExecuteNonQuery();

            cmd = new SqlCommand("Insert Into SectionsUpdated values(3," + Session["uid"] + ",@du)", conn);
            cmd.Parameters.AddWithValue("@du", Convert.ToDateTime(DateTime.Now.Date));
            cmd.ExecuteNonQuery();

            conn.Close();
            Response.Redirect("ResearchInterests.aspx");
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            Response.Redirect("Qualifications.aspx");
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            Response.Redirect("PersonalInfo.aspx");
        }
    }
}