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
    public partial class EvaluationExp : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["MEUCV"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uid"] == null)
                Response.Redirect("Login.aspx");
            ContentPlaceHolder cp = this.Master.Master.FindControl("ContentPlaceHolder1") as ContentPlaceHolder;
            HtmlGenericControl list = (HtmlGenericControl)cp.FindControl("menu5");//.FindControl("menu1");
            list.Attributes.Add("class", "ca-menu activeLi");
            Session["backurl"] = "Default.aspx";
            //Label lbl = (Label)Master.FindControl("lblWelcome");
            //lbl.Text = "خبرات التحكيم والتقييم";
            if (!IsPostBack)
                getData();
        }

        protected void getData()
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("Year");
            for (int i = 2014; i <= DateTime.Now.Date.Year; i++)
            {
                DataRow row = dt1.NewRow();
                row[0] = i;
                dt1.Rows.Add(row);
            }

            ddlFYear.DataSource = dt1;
            ddlFYear.DataTextField = "Year";
            ddlFYear.DataValueField = "Year";
            ddlFYear.DataBind();
            ddlFYear.Items.Insert(0, "حدد السنة");
            ddlFYear.Items[0].Value = "0";

            ddlTYear.DataSource = dt1;
            ddlTYear.DataTextField = "Year";
            ddlTYear.DataValueField = "Year";
            ddlTYear.DataBind();
            ddlTYear.Items.Insert(0, "حدد السنة");
            ddlTYear.Items[0].Value = "0";

            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand("Select * From Country", conn);
            ddlCountry.DataSource = cmd.ExecuteReader();
            ddlCountry.DataTextField = "Name";
            ddlCountry.DataValueField = "Code"; ;
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, "حدد الدولة");
            ddlCountry.Items[0].Value = "0";

            SqlCommand cmdTrain = new SqlCommand("select *,ROW_NUMBER() OVER(ORDER BY autoid ASC) AS serial from EvaluationExps where jobid=" + Session["uid"], conn);
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
                Response.Redirect("EvaluationExp.aspx");
            }
        }

        protected void btnSaveCert_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            try
            {
                    SqlCommand cmdGet = new SqlCommand("Insert Into SectionsInserted values(7," + Session["uid"] + ")", conn);
                    cmdGet.ExecuteNonQuery();
            }
            catch { }

            Response.Redirect("PublishedR.aspx");
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand cmd = new SqlCommand("Delete From EvaluationExps where Autoid=" + row.Cells[0].Text, conn);
            cmd.ExecuteNonQuery();

            cmd = new SqlCommand("Insert Into SectionsUpdated values(5," + Session["uid"] + ",@du)", conn);
            cmd.Parameters.AddWithValue("@du", Convert.ToDateTime(DateTime.Now.Date));
            cmd.ExecuteNonQuery();

            conn.Close();
            Response.Redirect("EvaluationExp.aspx");
        }

        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            ddlRole.SelectedValue = row.Cells[2].Text;
            oRoleDiv.Visible = (ddlRole.SelectedValue == "4" ? true : false);
            Label lbl =(Label) row.FindControl("lblRName");
            txtORole.Text = row.Cells[12].Text;
            //txtCom.Text= row.Cells[4].Text;
            txtMgz.Text = row.Cells[5].Text;
            ddlDBType.SelectedValue = row.Cells[6].Text;
            ddlCountry.SelectedValue = row.Cells[8].Text;
            ddlFYear.SelectedValue = row.Cells[10].Text;
            ddlTYear.SelectedValue = row.Cells[11].Text;
            if(ddlDBType.SelectedValue=="1")
            {
                dbDiv.Visible = true;
                txtDBmgz.Text= row.Cells[13].Text;
            }
            else
            {
                ddlDB.SelectedValue= row.Cells[14].Text;
                ddlDB.Visible = true;
            }
            LinkButton1.Text = "تعديل";
        }

        protected void btnTrainSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand cmdGet = new SqlCommand("select * from EvaluationExps where jobid=" + Session["uid"] + " and AutoId=" + lblUpdate.Text, conn);
                if (cmdGet.ExecuteReader().HasRows)
                {
                    cmdGet = new SqlCommand("delete from EvaluationExps where jobid=" + Session["uid"] + " and AutoId=" + lblUpdate.Text, conn);
                    cmdGet.ExecuteNonQuery();

                    cmdGet = new SqlCommand("Insert Into SectionsUpdated values(5," + Session["uid"] + ",@du)", conn);
                    cmdGet.Parameters.AddWithValue("@du", Convert.ToDateTime(DateTime.Now.Date));
                    cmdGet.ExecuteNonQuery();
                    Session["saved"] = false;

                }

                SqlCommand cmd = new SqlCommand("insert into EvaluationExps values(" +
                    "@instjob,@ri,@rn,@com,@mgz,@dbi,@dbn,@cni,@cnn,@fy,@ty,@or,@dbtext,@dblisti,@dblists)", conn);

                cmd.Parameters.AddWithValue("@instjob", Session["uid"]);
                cmd.Parameters.AddWithValue("@ri", ddlRole.SelectedValue);
                cmd.Parameters.AddWithValue("@rn", ddlRole.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@com", "");
                cmd.Parameters.AddWithValue("@mgz", txtMgz.Text);
                cmd.Parameters.AddWithValue("@dbi", ddlDBType.SelectedValue);
                cmd.Parameters.AddWithValue("@dbn", ddlDBType.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@cni", ddlCountry.SelectedValue);
                cmd.Parameters.AddWithValue("@cnn", ddlCountry.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@fy", ddlFYear.SelectedValue);
                cmd.Parameters.AddWithValue("@ty", ddlTYear.SelectedValue);
                cmd.Parameters.AddWithValue("@or", txtORole.Text);
                cmd.Parameters.AddWithValue("@dbtext", txtDBmgz.Text);
                cmd.Parameters.AddWithValue("@dblisti", ddlDB.SelectedValue);
                cmd.Parameters.AddWithValue("@dblists", ddlDB.SelectedItem.Text);
                cmd.ExecuteNonQuery();

                SqlCommand cmdTrain = new SqlCommand("select *,ROW_NUMBER() OVER(ORDER BY autoid ASC) AS serial from EvaluationExps where jobid=" + Session["uid"], conn);
                GridView1.DataSource = cmdTrain.ExecuteReader();
                GridView1.DataBind();

                conn.Close();
                Session["saved"] = true;
                lblMsg.Visible = true;
                lblMsg.Text = "تم التخزين بنجاح";
                Timer1.Enabled = true;

                Response.Redirect("EvaluationExp.aspx");
                //txtLink.Text = "";
                //ddlRSector.SelectedValue = "0";
                //lblUpdate.Text = "0";
            }
            catch
            {
                lblMsg.Visible = true;
                lblMsg.Text = "حصل خطأ أثناء التخزين";
                Timer1.Enabled = true;
                Session["saved"] = false;
            }

        }

        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRole.SelectedValue != "0")
                if (ddlRole.SelectedValue == "4")
                    oRoleDiv.Visible = true;
                else
                    oRoleDiv.Visible = false;
            else
                oRoleDiv.Visible = false;
        }

        protected void ddlDBType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlDBType.SelectedValue=="1")
            {
                ddlDB.Visible = false;
                dbDiv.Visible = true;
            }
            else if (ddlDBType.SelectedValue == "2")
            {
                ddlDB.Visible = true;
                dbDiv.Visible = false;
            }
            else
            {
                ddlDB.Visible = false;
                dbDiv.Visible = false;
            }
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            Response.Redirect("LinksDB.aspx");
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            Response.Redirect("PublishedR.aspx");
        }
    }
}