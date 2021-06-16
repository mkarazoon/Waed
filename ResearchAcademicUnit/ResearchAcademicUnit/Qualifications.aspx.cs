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
    public partial class Qualifications : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["MEUCV"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uid"] == null)
                Response.Redirect("Login.aspx");
            Session["backurl"] = "Default.aspx";
            ContentPlaceHolder cp = this.Master.Master.FindControl("ContentPlaceHolder1") as ContentPlaceHolder;
            HtmlGenericControl list = (HtmlGenericControl)cp.FindControl("menu2");//.FindControl("menu1");
            list.Attributes.Add("class", "ca-menu activeLi");

            //Label lbl = (Label)Master.FindControl("lblWelcome");
            //lbl.Text = "المؤهلات العلمية";

            if (!IsPostBack)
            {
                getData();
            }
        }

        protected void getData()
        {
            try
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                DataTable dt1 = new DataTable();
                dt1.Columns.Add("Year");
                for (int i = 1920; i <= 2050; i++)
                {
                    DataRow row = dt1.NewRow();
                    row[0] = i;
                    dt1.Rows.Add(row);
                }

                ddlBYear.DataSource = dt1;
                ddlBYear.DataTextField = "Year";
                ddlBYear.DataValueField = "Year";
                ddlBYear.DataBind();
                ddlBYear.Items.Insert(0, "حدد السنة");
                ddlBYear.Items[0].Value = "0";

                ddlMYear.DataSource = dt1;
                ddlMYear.DataTextField = "Year";
                ddlMYear.DataValueField = "Year";
                ddlMYear.DataBind();
                ddlMYear.Items.Insert(0, "حدد السنة");
                ddlMYear.Items[0].Value = "0";

                ddlPYear.DataSource = dt1;
                ddlPYear.DataTextField = "Year";
                ddlPYear.DataValueField = "Year";
                ddlPYear.DataBind();
                ddlPYear.Items.Insert(0, "حدد السنة");
                ddlPYear.Items[0].Value = "0";

                SqlCommand cmd1 = new SqlCommand("Select * From Country", conn);
                DataTable dt = new DataTable();
                dt.Load(cmd1.ExecuteReader());
                ddlBCountry.DataSource = dt;
                ddlBCountry.DataTextField = "Name";
                ddlBCountry.DataValueField = "Code"; ;
                ddlBCountry.DataBind();
                ddlBCountry.Items.Insert(0, "حدد بلد التخرج");
                ddlBCountry.Items[0].Value = "0";

                ddlMCountry.DataSource = dt;
                ddlMCountry.DataTextField = "Name";
                ddlMCountry.DataValueField = "Code"; ;
                ddlMCountry.DataBind();
                ddlMCountry.Items.Insert(0, "حدد بلد التخرج");
                ddlMCountry.Items[0].Value = "0";

                ddlPCountry.DataSource = dt;
                ddlPCountry.DataTextField = "Name";
                ddlPCountry.DataValueField = "Code"; ;
                ddlPCountry.DataBind();
                ddlPCountry.Items.Insert(0, "حدد بلد التخرج");
                ddlPCountry.Items[0].Value = "0";


                SqlCommand cmdQual = new SqlCommand("select * from instinfo where instjobid=" + Session["uid"], conn);
                SqlDataReader drQual = cmdQual.ExecuteReader();
                if (drQual.HasRows)
                {
                    drQual.Read();
                    Session["deg"] = Convert.ToInt16(drQual["Qual"]);
                }
                SqlCommand cmdCQual = new SqlCommand("select * from Qualification where instjobid=" + Session["uid"], conn);
                SqlDataReader drCQual = cmdCQual.ExecuteReader();
                int deg = Convert.ToInt16(drQual["Qual"]);
                if (Convert.ToInt16(Session["deg"]) == 3)
                {
                    masterDetDiv.Visible = false;
                    PhdDetDiv.Visible = false;
                    if (drCQual.HasRows)
                    {
                        drCQual.Read();
                        txtBUni.Text = drCQual[3].ToString();
                        txtBCollege.Text = drCQual[4].ToString();
                        ddlBYear.SelectedValue = drCQual[5].ToString();
                        txtBMajor.Text = drCQual[6].ToString();
                        txtBMinor.Text = drCQual[7].ToString();
                        ddlBCountry.SelectedValue = drCQual[8].ToString();
                        ddlBGrade.SelectedValue = drCQual[9].ToString();
                    }
                }
                else if (Convert.ToInt16(Session["deg"]) == 2)
                {
                    PhdDetDiv.Visible = false;
                    if (drCQual.HasRows)
                    {
                        drCQual.Read();
                        txtBUni.Text = drCQual[3].ToString();
                        txtBCollege.Text = drCQual[4].ToString();
                        ddlBYear.SelectedValue = drCQual[5].ToString();
                        txtBMajor.Text = drCQual[6].ToString();
                        txtBMinor.Text = drCQual[7].ToString();
                        ddlBCountry.SelectedValue = drCQual[8].ToString();
                        ddlBGrade.SelectedValue = drCQual[9].ToString();

                        drCQual.Read();
                        txtMUni.Text = drCQual[3].ToString();
                        txtMCollege.Text = drCQual[4].ToString();
                        ddlMYear.SelectedValue = drCQual[5].ToString();
                        txtMMajor.Text = drCQual[6].ToString();
                        txtMMinor.Text = drCQual[7].ToString();
                        ddlMCountry.SelectedValue = drCQual[8].ToString();
                        ddlMGrade.SelectedValue = drCQual[9].ToString();
                    }
                }
                else
                {
                    if (drCQual.HasRows)
                    {
                        drCQual.Read();
                        txtBUni.Text = drCQual[3].ToString();
                        txtBCollege.Text = drCQual[4].ToString();
                        ddlBYear.SelectedValue = drCQual[5].ToString();
                        txtBMajor.Text = drCQual[6].ToString();
                        txtBMinor.Text = drCQual[7].ToString();
                        ddlBCountry.SelectedValue = drCQual[8].ToString();
                        ddlBGrade.SelectedValue = drCQual[9].ToString();

                        drCQual.Read();
                        txtMUni.Text = drCQual[3].ToString();
                        txtMCollege.Text = drCQual[4].ToString();
                        ddlMYear.SelectedValue = drCQual[5].ToString();
                        txtMMajor.Text = drCQual[6].ToString();
                        txtMMinor.Text = drCQual[7].ToString();
                        ddlMCountry.SelectedValue = drCQual[8].ToString();
                        ddlMGrade.SelectedValue = drCQual[9].ToString();

                        drCQual.Read();
                        txtPUni.Text = drCQual[3].ToString();
                        txtPCollege.Text = drCQual[4].ToString();
                        ddlPYear.SelectedValue = drCQual[5].ToString();
                        txtPMajor.Text = drCQual[6].ToString();
                        txtPMinor.Text = drCQual[7].ToString();
                        ddlPCountry.SelectedValue = drCQual[8].ToString();
                        ddlPGrade.SelectedValue = drCQual[9].ToString();
                    }
                }
                conn.Close();
            }
            catch { }
        }

        protected void btnSaveCert_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmdGet = new SqlCommand("select * from Qualification where instjobid=" + Session["uid"], conn);
                if (cmdGet.ExecuteReader().HasRows)
                {
                    cmdGet = new SqlCommand("delete from Qualification where instjobid=" + Session["uid"], conn);
                    cmdGet.ExecuteNonQuery();

                    cmdGet = new SqlCommand("Insert Into SectionsUpdated values(2," + Session["uid"] + ",@du)", conn);
                    cmdGet.Parameters.AddWithValue("@du", Convert.ToDateTime(DateTime.Now.Date));
                    cmdGet.ExecuteNonQuery();
                    Session["saved"] = false;

                }

                if (bscDetDiv.Visible)
                {
                    SqlCommand cmd = new SqlCommand("insert into Qualification values(" +
                    "@instjob,@QualType,@QualUniName,@QualColName,@GradYear," +
                    "@Major,@Minor,@Country,@Grade)", conn);

                    cmd.Parameters.AddWithValue("@instjob", Session["uid"]);
                    cmd.Parameters.AddWithValue("@QualType", lblBsc.Text);
                    cmd.Parameters.AddWithValue("@QualUniName", txtBUni.Text);
                    cmd.Parameters.AddWithValue("@QualColName", txtBCollege.Text);
                    cmd.Parameters.AddWithValue("@GradYear", ddlBYear.SelectedValue);
                    cmd.Parameters.AddWithValue("@Major", txtBMajor.Text);
                    cmd.Parameters.AddWithValue("@Minor", txtBMinor.Text);
                    cmd.Parameters.AddWithValue("@Country", ddlBCountry.SelectedValue);
                    cmd.Parameters.AddWithValue("@Grade", ddlBGrade.SelectedValue);
                    cmd.ExecuteNonQuery();
                    Session["saved"] = true;
                }
                if (masterDetDiv.Visible)
                {
                    SqlCommand cmd = new SqlCommand("insert into Qualification values(" +
                    "@instjob,@QualType,@QualUniName,@QualColName,@GradYear," +
                    "@Major,@Minor,@Country,@Grade)", conn);

                    cmd.Parameters.AddWithValue("@instjob", Session["uid"]);
                    cmd.Parameters.AddWithValue("@QualType", lblMaster.Text);
                    cmd.Parameters.AddWithValue("@QualUniName", txtMUni.Text);
                    cmd.Parameters.AddWithValue("@QualColName", txtMCollege.Text);
                    cmd.Parameters.AddWithValue("@GradYear", ddlMYear.SelectedValue);
                    cmd.Parameters.AddWithValue("@Major", txtMMajor.Text);
                    cmd.Parameters.AddWithValue("@Minor", txtMMinor.Text);
                    cmd.Parameters.AddWithValue("@Country", ddlMCountry.SelectedValue);
                    cmd.Parameters.AddWithValue("@Grade", ddlMGrade.SelectedValue);
                    cmd.ExecuteNonQuery();
                    Session["saved"] = true;
                }

                if (PhdDetDiv.Visible)
                {
                    SqlCommand cmd = new SqlCommand("insert into Qualification values(" +
                    "@instjob,@QualType,@QualUniName,@QualColName,@GradYear," +
                    "@Major,@Minor,@Country,@Grade)", conn);

                    cmd.Parameters.AddWithValue("@instjob", Session["uid"]);
                    cmd.Parameters.AddWithValue("@QualType", lblPhd.Text);
                    cmd.Parameters.AddWithValue("@QualUniName", txtPUni.Text);
                    cmd.Parameters.AddWithValue("@QualColName", txtPCollege.Text);
                    cmd.Parameters.AddWithValue("@GradYear", ddlPYear.SelectedValue);
                    cmd.Parameters.AddWithValue("@Major", txtPMajor.Text);
                    cmd.Parameters.AddWithValue("@Minor", txtPMinor.Text);
                    cmd.Parameters.AddWithValue("@Country", ddlPCountry.SelectedValue);
                    cmd.Parameters.AddWithValue("@Grade", ddlPGrade.SelectedValue);
                    cmd.ExecuteNonQuery();
                    Session["saved"] = true;
                }
            try
            {


                if (Convert.ToBoolean(Session["saved"]))
                {
                    cmdGet = new SqlCommand("Insert Into SectionsInserted values(3," + Session["uid"] + ")", conn);
                    cmdGet.ExecuteNonQuery();
                }
            }
            catch { }
                conn.Close();
                Session["saved"] = true;
                lblMsg.Visible = true;
                //lblMsg.Text = "تم التخزين بنجاح";
                //Timer1.Enabled = true;
                //Response.Redirect("LinksDB.aspx");
        }
            catch
            {
                lblMsg.Visible = true;
                lblMsg.Text = "حصل خطأ أثناء التخزين";
                Timer1.Enabled = true;
                Session["saved"] = false;
            }
}

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(Session["saved"]))
            {
                lblMsg.Visible = false;
                Timer1.Enabled = false;
                Response.Redirect("ResearchInterests.aspx");
            }
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            Response.Redirect("ResearchInterests.aspx");
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            Response.Redirect("LinksDB.aspx");
        }
    }
}