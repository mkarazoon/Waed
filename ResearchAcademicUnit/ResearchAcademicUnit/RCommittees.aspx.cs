using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ResearchAcademicUnit
{
    public partial class RCommittees : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["MEUCV"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uid"] == null)
                Response.Redirect("Login.aspx");
            Session["backurl"] = "Default.aspx";
            ContentPlaceHolder cp = this.Master.Master.FindControl("ContentPlaceHolder1") as ContentPlaceHolder;
            HtmlGenericControl list = (HtmlGenericControl)cp.FindControl("menu11");//.FindControl("menu1");
            list.Attributes.Add("class", "ca-menu activeLi");

            //Label lbl = (Label)Master.FindControl("lblWelcome");
            //lbl.Text = "العضوية في اللجان البحثية";
            if (!IsPostBack)
                getData();
        }

        protected void getData()
        {

            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand("Select * From Country", conn);
            ddlCountry.DataSource = cmd.ExecuteReader();
            ddlCountry.DataTextField = "Name";
            ddlCountry.DataValueField = "Code"; ;
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, "حدد الدولة");
            ddlCountry.Items[0].Value = "0";

            SqlCommand cmdTrain = new SqlCommand("select *,ROW_NUMBER() OVER(ORDER BY autoid ASC) AS serial from Committee where jobid=" + Session["uid"], conn);
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
                Response.Redirect("RCommittees.aspx");
            }
        }

        protected void btnSaveCert_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            try
            {
                SqlCommand cmdGet = new SqlCommand("Insert Into SectionsInserted values(13," + Session["uid"] + ")", conn);
                cmdGet.ExecuteNonQuery();
            }
            catch { }

            Response.Redirect("RCertificate.aspx");
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand cmd = new SqlCommand("Delete From Committee where Autoid=" + row.Cells[0].Text, conn);
            cmd.ExecuteNonQuery();

            cmd = new SqlCommand("Insert Into SectionsUpdated values(12," + Session["uid"] + ",@du)", conn);
            cmd.Parameters.AddWithValue("@du", Convert.ToDateTime(DateTime.Now.Date));
            cmd.ExecuteNonQuery();

            conn.Close();
            Response.Redirect("RCommittees.aspx");
        }

        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            lblUpdate.Text = row.Cells[0].Text;
            txtName.Text = row.Cells[2].Text;
            ddlActType.SelectedValue = row.Cells[3].Text;
            txtDate.Text = row.Cells[5].Text;
            ddlCountry.SelectedValue= row.Cells[6].Text;
            txtNotes.Text = row.Cells[8].Text;
            LinkButton1.Text = "تعديل";
        }

        protected void btnTrainSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand cmdGet = new SqlCommand("select * from Committee where jobid=" + Session["uid"] + " and AutoId=" + lblUpdate.Text, conn);
                if (cmdGet.ExecuteReader().HasRows)
                {
                    cmdGet = new SqlCommand("delete from Committee where jobid=" + Session["uid"] + " and AutoId=" + lblUpdate.Text, conn);
                    cmdGet.ExecuteNonQuery();

                    cmdGet = new SqlCommand("Insert Into SectionsUpdated values(12," + Session["uid"] + ",@du)", conn);
                    cmdGet.Parameters.AddWithValue("@du", Convert.ToDateTime(DateTime.Now.Date));
                    cmdGet.ExecuteNonQuery();
                    Session["saved"] = false;

                }

                SqlCommand cmd = new SqlCommand("insert into Committee values(" +
                    "@instjob,@CommName,@TypeI,@TypeS,@CDate,@CI,@CN,@Notes)", conn);

                cmd.Parameters.AddWithValue("@instjob", Session["uid"]);
                cmd.Parameters.AddWithValue("@CommName", txtName.Text);
                cmd.Parameters.AddWithValue("@TypeI", ddlActType.SelectedValue);
                cmd.Parameters.AddWithValue("@TypeS", ddlActType.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@CDate", Convert.ToDateTime(txtDate.Text));
                cmd.Parameters.AddWithValue("@CI", ddlCountry.SelectedValue);
                cmd.Parameters.AddWithValue("@CN", ddlCountry.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Notes", txtNotes.Text);
                cmd.ExecuteNonQuery();

                SqlCommand cmdTrain = new SqlCommand("select *,ROW_NUMBER() OVER(ORDER BY autoid ASC) AS serial from Committee where jobid=" + Session["uid"], conn);
                GridView1.DataSource = cmdTrain.ExecuteReader();
                GridView1.DataBind();

                conn.Close();
                Session["saved"] = true;
                //lblMsg.Visible = true;
                //lblMsg.Text = "تم التخزين بنجاح";
                //Timer1.Enabled = true;

                Response.Redirect("RCommittees.aspx");
            }
            catch
            {
                lblMsg.Visible = true;
                lblMsg.Text = "حصل خطأ أثناء التخزين";
                Timer1.Enabled = true;
                Session["saved"] = false;
            }
        }

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            DateTime insertedDate;
            if (DateTime.TryParse(txtDate.Text, out insertedDate))
            {
                var parameterDate = DateTime.ParseExact("01/01/2014", "MM/dd/yyyy", CultureInfo.InvariantCulture);
                if (insertedDate < parameterDate)
                {
                    txtDate.Text = "";
                    lblFDateError.Visible = true;
                }
                else
                    lblFDateError.Visible = false;
            }
            else
                txtDate.Text = "";
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            Response.Redirect("Achievements.aspx");
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            Response.Redirect("RCertificate.aspx");
        }
    }
}