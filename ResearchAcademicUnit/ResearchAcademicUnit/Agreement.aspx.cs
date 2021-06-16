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
    public partial class Agreement : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["MEUCV"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uid"] == null)
                Response.Redirect("Login.aspx");
            Session["backurl"] = "Default.aspx";
            //Label lbl = (Label)Master.FindControl("lblWelcome");
            ////lbl.Text = "إقرار بصحة المعلومات";

            if (!IsPostBack)
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("select * From SectionsInserted where Jobid=" + Session["uid"] + " and Sectionid=16", conn);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                if(dt.Rows.Count!=0)
                {
                    //Response.Redirect("PersonalInfo.aspx");
                    chkAgree.Visible = false;
                    btnTrainSave.Enabled = true;
                    AgreeDiv.Visible = true;
                    //chkAgree.Checked = true;
                    //chkAgree.Enabled = false;
                    btnTrainSave.Visible = true;
                    //uploadDiv.Visible = true;
                }

                conn.Close();
            }

        }

        protected void btnSaveCert_Click(object sender, EventArgs e)
        {

            Response.Redirect("UploadFiles.aspx");
        }

        protected void btnTrainSave_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            try
            {
                if (chkAgree.Checked || chkAgree.Visible==false)
                {
                    //uploadDiv.Visible = true;

                    //msgDiv.Visible = true;
                    //lblMsg.Text = "تم ارسال البيانات البحثية بنجاح. يمكنك التعديل على البيانات حتى تاريخ ";
                    try
                    {
                        SqlCommand cmdGet = new SqlCommand("Insert Into SectionsInserted values(1," + Session["uid"] + ")", conn);
                        cmdGet.ExecuteNonQuery();
                    }
                    catch { }

                    try
                    {
                        SqlCommand cmdGet = new SqlCommand("Insert Into SectionsInserted values(16," + Session["uid"] + ")", conn);
                        cmdGet.ExecuteNonQuery();
                    }
                    catch { }

                    Response.Redirect("PersonalInfo.aspx");
                }
            }
            catch { }
        }

        protected void chkAgree_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAgree.Checked)
            {
                btnTrainSave.Enabled = true;
                AgreeDiv.Visible = true;
            }
            else
            {
                btnTrainSave.Enabled = false;
                AgreeDiv.Visible = false;
            }
        }
    }
}