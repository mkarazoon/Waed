using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchAcademicUnit
{
    public partial class FollowUpFormThesisView : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);
        static string constrOracleStu = "User Id=karazoon;Password=karazoon;data source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=10.0.101.121)(PORT=1521))(CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = meu)));";
        OracleConnection conn1 = new OracleConnection(constrOracleStu);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                System.Web.UI.HtmlControls.HtmlGenericControl divh = (System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("prinOut");
                divh.Visible = false;

                System.Web.UI.HtmlControls.HtmlGenericControl divf = (System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("printfooter");
                divf.Visible = false;

                //if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                //    conn.Open();
                string studid = "";
                string autoid = "";
                if (Request.QueryString["ReportId"] != null && Request.QueryString["ReportId"] != string.Empty)
                    studid = Request.QueryString["StudId"];

                if (Request.QueryString["StudId"] != null && Request.QueryString["StudId"] != string.Empty)
                    autoid = Request.QueryString["ReportId"];
                btnGetData_Click(studid, autoid);


                //SqlCommand cmdR = new SqlCommand("Select * From ResearcherInfo where AcdId=" + Session["userid"], conn);
                //SqlDataReader dr = cmdR.ExecuteReader();
                //dr.Read();
                //lblSupName.Text = dr[2].ToString();
                //lblSupDegree.Text = dr[5].ToString();
                //lblSupMajor.Text = dr[9].ToString();
                //lblFaculty.Text = dr[3].ToString();
                //lblSupDept.Text = dr[4].ToString();
                //SqlCommand cmd = new SqlCommand("Select AutoId,CollegeName from Faculty", conn);
                //DataTable dt = new DataTable();
                //dt.Load(cmd.ExecuteReader());
                //ddlFaculty.DataSource = dt;
                //ddlFaculty.DataTextField = "CollegeName";
                //ddlFaculty.DataValueField = "AutoId";
                //ddlFaculty.DataBind();
                //ddlFaculty.Items.Insert(0, "اختيار");
                //ddlFaculty.Items[0].Value = "";

                //conn.Close();
            }
        }

        //protected void btnSend_Click(object sender, EventArgs e)
        //{
        //    if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
        //        conn.Open();
        //    SqlCommand cmd = new SqlCommand("Insert into FollowUpThesisTable output inserted.AutoId values(@StudId,@StudName,@MsText,@StudFaculty,@StudDept,@SupName,@Degree,@CoSupName,@SupMajor," +
        //        "@SupFaculty,@SupDept,@Supdate,@ArabicThesis,@EngThesis,@MeetingMonth,@MeetingCount,@StudAch,@SupOpinion,@InsertedDate,@UserId)", conn);
        //    cmd.Parameters.AddWithValue("@StudId",txtStudId.Text);
        //    cmd.Parameters.AddWithValue("@StudName",txtStudName.Text);
        //    cmd.Parameters.AddWithValue("@MsText",txtMs.Text);
        //    cmd.Parameters.AddWithValue("@StudFaculty",ddlFaculty.SelectedValue);
        //    cmd.Parameters.AddWithValue("@StudDept",ddlDept.SelectedValue);
        //    cmd.Parameters.AddWithValue("@SupName",lblSupName.Text);
        //    cmd.Parameters.AddWithValue("@Degree",lblSupDegree.Text);
        //    cmd.Parameters.AddWithValue("@CoSupName",txtCoSupName.Text);
        //    cmd.Parameters.AddWithValue("@SupMajor",lblSupMajor.Text);
        //    cmd.Parameters.AddWithValue("@SupFaculty",lblFaculty.Text);
        //    cmd.Parameters.AddWithValue("@SupDept",lblSupDept.Text);
        //    cmd.Parameters.AddWithValue("@Supdate",txtSupDate.Text);
        //    cmd.Parameters.AddWithValue("@ArabicThesis", txtArabicThesis.Text);
        //    cmd.Parameters.AddWithValue("@EngThesis", txtEngThesis.Text);
        //    cmd.Parameters.AddWithValue("@MeetingMonth", ddlMeetingMonth.SelectedValue);
        //    cmd.Parameters.AddWithValue("@MeetingCount",txtMeetingCount.Text);
        //    cmd.Parameters.AddWithValue("@StudAch", txtStudAch.Text);
        //    cmd.Parameters.AddWithValue("@SupOpinion", txtSupOpinion.Text);
        //    cmd.Parameters.AddWithValue("@InsertedDate",DateTime.Now);
        //    cmd.Parameters.AddWithValue("@UserId",Session["userid"]);
        //    string AutoId = cmd.ExecuteScalar().ToString();
        //    conn.Close();
        //    sendEmail(AutoId);
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('تم الحفظ بنجاح','success');", true);
        //}

        protected void btnGetData_Click(string studid, string autoid)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand("Select *,CollegeName,DeptName From FollowUpThesisTable a,Faculty b,Department c where a.StudFaculty=b.AutoId and a.StudDept=c.AutoId and StudId=" + studid + " and a.Autoid="+autoid, conn);
            DataTable dtData = new DataTable();
            dtData.Load(cmd.ExecuteReader());
            if(dtData.Rows.Count!=0)
            {
                lblStudId.Text = dtData.Rows[0]["Studid"].ToString();
                lblStudName.Text = lblStudSign.Text= dtData.Rows[0]["StudName"].ToString();
                lblMs.Text = dtData.Rows[0]["MsText"].ToString();
                lblStudFaculty.Text = dtData.Rows[0]["CollegeName"].ToString();
                lblStudDept.Text = dtData.Rows[0]["DeptName"].ToString();
                lblSupName.Text= lblSuperSign.Text = dtData.Rows[0]["SupName"].ToString();
                lblSupDegree.Text = dtData.Rows[0]["Degree"].ToString();
                lblCoSupName.Text = (dtData.Rows[0]["CoSupName"].ToString()==""?"لا يوجد": dtData.Rows[0]["CoSupName"].ToString());
                lblSupMajor.Text = dtData.Rows[0]["SupMajor"].ToString();
                lblFaculty.Text = dtData.Rows[0]["SupFaculty"].ToString();
                lblSupDept.Text = dtData.Rows[0]["SupDept"].ToString();
                lblSupDate.Text = Convert.ToDateTime(dtData.Rows[0]["Supdate"]).ToString("yyyy-MM-dd");
                ArabicThesisDiv.InnerText = dtData.Rows[0]["ArabicThesis"].ToString();
                EngThesisDiv.InnerText = dtData.Rows[0]["EngThesis"].ToString();
                lblMeetingMonth.Text = dtData.Rows[0]["MeetingMonth"].ToString();
                lblMeetingCount.Text = dtData.Rows[0]["MeetingCount"].ToString();
                StudAchDiv.InnerText = dtData.Rows[0]["StudAch"].ToString();
                SupOpinionDiv.InnerText = dtData.Rows[0]["SupOpinion"].ToString();
                try
                {
                    getDeptFaculty(dtData.Rows[0]["UserId"].ToString());
                }
                catch { }
            }

            conn.Close();
        }

        protected void getDeptFaculty(string uid)
        {
            if (conn1.State == ConnectionState.Closed || conn1.State == ConnectionState.Broken)
                conn1.Open();

            DataTable dt = new DataTable();
            string sql = @"select distinct instructor_name from meu_new.inst_timetable_view a,meu_new.faculties_depts_view b where a.instructor_id=b.dean_id and b.faculty_no=(select distinct faculty_no from meu_new.inst_timetable_view where instructor_id=" + uid+")";
            OracleCommand cmd = new OracleCommand(sql, conn1);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.Fill(dt);
            Label1.Text = dt.Rows[0][0].ToString();

            dt = new DataTable();
            sql = @"select distinct instructor_name from meu_new.inst_timetable_view a,meu_new.faculties_depts_view b where a.instructor_id=b.head_id and b.faculty_no=(select distinct faculty_no from meu_new.inst_timetable_view where instructor_id=" + uid + ")";
            cmd = new OracleCommand(sql, conn1);
            da = new OracleDataAdapter(cmd);
            da.Fill(dt);
            Label2.Text = dt.Rows[0][0].ToString();


        }
    }
}