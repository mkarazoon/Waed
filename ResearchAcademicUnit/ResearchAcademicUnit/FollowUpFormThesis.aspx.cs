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
    public partial class FollowUpFormThesis : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);
        static string OracleConnString = System.Configuration.ConfigurationManager.ConnectionStrings["orcleConStr"].ConnectionString;
        OracleConnection conn1 = new OracleConnection(OracleConnString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uid"] == null)
                Response.Redirect("Login.aspx");

            if (!IsPostBack)
            {

                //string sql = @"select * From meu_new.HRS_JOB_VIEW";
                //string sql = @"select * From meu_new.all_students_karz";
                //OracleCommand cmdOrc = new OracleCommand(sql, conn1);
                //OracleDataAdapter da = new OracleDataAdapter(cmdOrc);
                //DataTable dtTest = new DataTable();
                //da.Fill(dtTest);

                //var dataRow = dtTest.Select("[رقم الطالب]=20050002");

                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmdWF = new SqlCommand(@"SELECT [AutoId],[FormId],[StepNo], a.[PrivId], b.PrivTo
                                                   FROM WorkFlow a, Com_Priviliges b
                                                   where a.PrivId = b.PrivId and FormId=7", conn);
                DataTable dtWF = new DataTable();
                dtWF.Load(cmdWF.ExecuteReader());
                if (dtWF.Rows.Count != 0)
                {
                    Session["dtWF"] = dtWF;
                    var priv = dtWF.Select("privto=" + Session["uid"]);
                    if (priv.Length == 0)
                    {
                        btnShowDec.Visible = false;
                    }
                    else if (Session["dtData"] != null)
                    {
                        DataTable dtData = (DataTable)Session["dtData"];
                        getStudInfo(dtData.Rows[0][1].ToString(), true, Convert.ToInt16(Session["RequestedReport"]));
                    }
                    if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                        conn.Open();
                    SqlCommand cmdR = new SqlCommand("Select * From ResearcherInfo where AcdId=" + Session["userid"], conn);
                    SqlDataReader dr = cmdR.ExecuteReader();
                    dr.Read();
                    lblSupName.Text = dr[2].ToString();
                    lblSupDegree.Text = dr[5].ToString();
                    lblSupMajor.Text = dr[9].ToString();
                    lblFaculty.Text = dr[3].ToString();
                    lblSupDept.Text = dr[4].ToString();
                    //SqlCommand cmd = new SqlCommand("Select AutoId,CollegeName from Faculty", conn);
                    //DataTable dt = new DataTable();
                    //dt.Load(cmd.ExecuteReader());
                    //ddlFaculty.DataSource = dt;
                    //ddlFaculty.DataTextField = "CollegeName";
                    //ddlFaculty.DataValueField = "AutoId";
                    //ddlFaculty.DataBind();
                    //ddlFaculty.Items.Insert(0, "اختيار");
                    //ddlFaculty.Items[0].Value = "";
                    //string sql = "select distinct faculty_no,faculty_name from meu_new.faculties_depts_view where dean_id<>0 and faculty_no<>14 order by faculty_no";
                    //OracleCommand cmd = new OracleCommand(sql, conn1);
                    //OracleDataAdapter da = new OracleDataAdapter(cmd);
                    //DataTable dt = new DataTable();
                    //da.Fill(dt);
                    //ddlFaculty.DataSource = dt;
                    //ddlFaculty.DataTextField = "faculty_name";
                    //ddlFaculty.DataValueField = "faculty_no";
                    //ddlFaculty.DataBind();
                    //ddlFaculty.Items.Insert(0, "اختيار الكلية");
                    //ddlFaculty.Items[0].Value = "";
                }
                else
                {
                    btnSend.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('لم يتم تحديد مسار لهذا النموذج أرجو مراجعة مدير النظام','error');", true);
                }
                conn.Close();

            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            string sql = @"SELECT *
                    FROM WorkFlow a, Com_Priviliges b,Com_JobTitle c
                    where a.PrivId = b.PrivId and a.PrivId = c.AutoId and FormId = 7 and stepno=1 and FacultyNo=" + lblStudFacultyNo.Text;// + " and DeptId=" + lblStudDept.Text;

            SqlCommand cmdGetSentInfo = new SqlCommand(sql, conn);
            DataTable dtPriv = new DataTable();
            dtPriv.Load(cmdGetSentInfo.ExecuteReader());
            DataRow[] dr = dtPriv.Select("FacultyNo=" + lblStudFacultyNo.Text);
            if (dtPriv.Rows[0][8].ToString() != "0")
            {
                dr = dtPriv.Select("DeptId=" + lblStudDeptNo.Text);
            }

            if (dr.Length != 0)
            {
                /* insert report information*/
                SqlCommand cmd = new SqlCommand("Insert into FollowUpThesisTable output inserted.AutoId values(@StudId,@StudName,@MsText,@StudFaculty,@StudDept,@SupName,@Degree,@CoSupName,@SupMajor," +
                "@SupFaculty,@SupDept,@Supdate,@ArabicThesis,@EngThesis,@MeetingMonth,@MeetingCount,@StudAch,@SupOpinion,@InsertedDate,@UserId,@Status,7)", conn);
                cmd.Parameters.AddWithValue("@StudId", txtStudId.Text);
                cmd.Parameters.AddWithValue("@StudName", lblStudName.Text);
                cmd.Parameters.AddWithValue("@MsText", lblMs.Text);
                cmd.Parameters.AddWithValue("@StudFaculty", lblStudFacultyNo.Text);
                cmd.Parameters.AddWithValue("@StudDept", lblStudDeptNo.Text);
                cmd.Parameters.AddWithValue("@SupName", lblSupName.Text);
                cmd.Parameters.AddWithValue("@Degree", lblSupDegree.Text);
                cmd.Parameters.AddWithValue("@CoSupName", txtCoSupName.Text);
                cmd.Parameters.AddWithValue("@SupMajor", lblSupMajor.Text);
                cmd.Parameters.AddWithValue("@SupFaculty", lblFaculty.Text);
                cmd.Parameters.AddWithValue("@SupDept", lblSupDept.Text);
                cmd.Parameters.AddWithValue("@Supdate", txtSupDate.Text);
                cmd.Parameters.AddWithValue("@ArabicThesis", txtArabicThesis.Text);
                cmd.Parameters.AddWithValue("@EngThesis", txtEngThesis.Text);
                cmd.Parameters.AddWithValue("@MeetingMonth", ddlMeetingMonth.SelectedValue);
                cmd.Parameters.AddWithValue("@MeetingCount", txtMeetingCount.Text);
                cmd.Parameters.AddWithValue("@StudAch", txtStudAch.Text);
                cmd.Parameters.AddWithValue("@SupOpinion", txtSupOpinion.Text);
                cmd.Parameters.AddWithValue("@InsertedDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@UserId", Session["userid"]);
                cmd.Parameters.AddWithValue("@Status", 0);
                string AutoId = cmd.ExecuteScalar().ToString();

                // get instructor department
                //string sql = @"select * From meu_new.inst_timetable_view where instructor_id="+Session["userid"];
                ////string sql = @"select * From meu_new.all_students_karz";
                //OracleCommand cmdOrc = new OracleCommand(sql, conn1);
                //OracleDataAdapter da = new OracleDataAdapter(cmdOrc);
                //DataTable dtTest = new DataTable();
                //da.Fill(dtTest);


                /* insert followup information*/
                cmd = new SqlCommand("insert into RequestsFollowUp values(@RequestId,@ReqFromId,@ReqFromName,@ReqToId,@RquToName,@RequestDate,N'قيد الانتظار',@ActualId,@Notes,0,7)", conn);
                cmd.Parameters.AddWithValue("@RequestId", AutoId);
                cmd.Parameters.AddWithValue("@ReqFromId", Session["userid"]);
                cmd.Parameters.AddWithValue("@ReqFromName", lblSupName.Text);
                cmd.Parameters.AddWithValue("@ReqToId", dr[0][4]);
                cmd.Parameters.AddWithValue("@RquToName", dtPriv.Rows[0][13] + " - " + (dtPriv.Rows[0][8].ToString()!="0"?dr[0][9]:dr[0][7]));
                cmd.Parameters.AddWithValue("@RequestDate", DateTime.Now.Date);
                cmd.Parameters.AddWithValue("@ActualId", Session["userid"]);
                cmd.Parameters.AddWithValue("@Notes", txtDirNote.Text);
                cmd.ExecuteNonQuery();

                /* end */
                conn.Close();
                //sendEmail(AutoId);
                Timer1.Enabled = true;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('تم ارسال التقرير بنجاح','success');", true);
            }
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('لم يتم تحديد رئيس القسم لهذا الطالب','error');", true);
            
        }

        protected void btnGetData_Click(object sender, EventArgs e)
        {
            if (txtStudId.Text != "")
            {
                getStudInfo(txtStudId.Text, false,0);
            }
            else
            {
                lblStudName.Text =
                lblMs.Text =
                lblFaculty.Text=
                lblStudDept.Text=
                txtCoSupName.Text =
                //txtSupDate.Text =
                txtArabicThesis.Text =
                txtEngThesis.Text = "";
            }
        }

        protected void getStudInfo(string studid,bool source,int index)
        {

            string sql = @"select * From meu_new.all_students_karz where ""رقم الطالب""=" + studid;
            OracleCommand cmdOrc = new OracleCommand(sql, conn1);
            OracleDataAdapter da = new OracleDataAdapter(cmdOrc);
            DataTable dtTest = new DataTable();
            da.Fill(dtTest);
            if (dtTest.Rows.Count != 0)
            {
                txtStudId.Text= dtTest.Rows[0][0].ToString();
                txtStudId.ReadOnly = source;
                lblStudName.Text = dtTest.Rows[0][3].ToString();
                lblMs.Text = dtTest.Rows[0][7].ToString();
                lblStudFaculty.Text = dtTest.Rows[0][6].ToString();
                lblStudFacultyNo.Text = dtTest.Rows[0][16].ToString();
                lblStudDept.Text = dtTest.Rows[0][13].ToString();
                lblStudDeptNo.Text = dtTest.Rows[0][12].ToString();
                //var dataRow = dtTest.Select("[رقم الطالب]=20050002");

                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("Select * From FollowUpThesisTable where StudId=" + studid + (source ? " and Autoid=" + index : " and Autoid=(Select Max(Autoid) From FollowUpThesisTable where Studid=" + studid + " )"), conn);
                DataTable dtData = new DataTable();
                dtData.Load(cmd.ExecuteReader());
                if (dtData.Rows.Count != 0)
                {
                    //lblStudName.Text = dtData.Rows[0]["StudName"].ToString();
                    //txtStudId.ReadOnly = source;
                    //lblMs.Text = dtData.Rows[0]["MsText"].ToString();
                    //txtMs.ReadOnly = source;
                    //lblFaculty.Text= dtData.Rows[0]["StudFaculty"].ToString();
                    //ddlMeetingMonth.Enabled = !source;

                    //SqlCommand cmd1 = new SqlCommand("Select distinct AutoId,DeptName from Department where CAutoid=" + ddlFaculty.SelectedValue, conn);
                    //ddlDept.DataSource = cmd1.ExecuteReader();
                    //ddlDept.DataTextField = "DeptName";
                    //ddlDept.DataValueField = "AutoId";
                    //ddlDept.DataBind();
                    //ddlDept.Items.Insert(0, "اختيار");
                    //ddlDept.Items[0].Value = "";
                    //ddlDept.SelectedValue = dtData.Rows[0]["StudDept"].ToString();
                    //ddlMeetingMonth.Enabled = source;

                    lblSupName.Text = dtData.Rows[0]["SupName"].ToString();
                    lblSupDegree.Text = dtData.Rows[0]["Degree"].ToString();
                    txtCoSupName.Text = dtData.Rows[0]["CoSupName"].ToString();
                    txtCoSupName.ReadOnly = source;
                    lblSupMajor.Text = dtData.Rows[0]["SupMajor"].ToString();
                    lblFaculty.Text = dtData.Rows[0]["SupFaculty"].ToString();
                    lblSupDept.Text = dtData.Rows[0]["SupDept"].ToString();
                    txtSupDate.Text = Convert.ToDateTime(dtData.Rows[0]["Supdate"]).ToString("yyyy-MM-dd");
                    txtSupDate.ReadOnly = source;
                    txtArabicThesis.Text = dtData.Rows[0]["ArabicThesis"].ToString();
                    txtArabicThesis.ReadOnly = source;
                    txtEngThesis.Text = dtData.Rows[0]["EngThesis"].ToString();
                    txtEngThesis.ReadOnly = source;
                    if (source)
                    {
                        ddlMeetingMonth.SelectedValue = dtData.Rows[0]["MeetingMonth"].ToString();
                        ddlMeetingMonth.Enabled = !source;
                        txtMeetingCount.Text = dtData.Rows[0]["MeetingCount"].ToString();
                        txtMeetingCount.ReadOnly = source;
                        txtStudAch.ReadOnly = source;
                        txtStudAch.Text = dtData.Rows[0]["StudAch"].ToString();
                        txtSupOpinion.ReadOnly = source;
                        txtSupOpinion.Text = dtData.Rows[0]["SupOpinion"].ToString();
                        btnSend.Visible = !source;
                    }
                }
                else
                {
                    //lblStudName.Text =
                    //lblMs.Text =
                    //lblFaculty.Text=
                    //lblStudDept.Text=
                    //txtCoSupName.Text =
                    ////txtSupDate.Text =
                    //txtArabicThesis.Text =
                    //txtEngThesis.Text = "";
                }
                conn.Close();
            }
            

        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Response.Redirect("FollowUpFormThesis.aspx");
        }

        protected void sendEmail(string AutoId)
        {
            var smtp = new SmtpClient
            {
                Host = "smtp.office365.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("mkarazoon@meu.edu.jo", "malo@lian1")
            };
            //using (var message = new MailMessage("mkarazoon@meu.edu.jo", "Graduate-dept@meu.edu.jo")
            using (var message = new MailMessage("mkarazoon@meu.edu.jo", "Graduate-dept@meu.edu.jo")
            {
                Subject = "تقرير المتابعة الشهري لطلبة الدراسات العليا للطالب " + lblStudName.Text,
                Body = "<body dir=rtl>" + "تم ارسال معلومات التقرير الخاص بالطالب " + lblStudName.Text + "<br> والمشرف " + lblSupName.Text
                + "<br> لكلية " + lblFaculty.Text
                + "<br> لطباعة التقرير "
                + "<a href='http://meusr-ra.meu.edu.jo/FollowUpFormThesisView.aspx?StudId=" + txtStudId.Text + "&ReportId=" + AutoId
                + "'>اضغط هنا</a></body>",
                IsBodyHtml = true
            })
            {
                smtp.Send(message);
            }

        }

        protected void btnDir_Click(object sender, EventArgs e)
        {

        }

        protected void btnDean_Click(object sender, EventArgs e)
        {

        }

        protected void btnDirAgree_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            string sql = @"SELECT *
                    FROM WorkFlow a, Com_Priviliges b,Com_JobTitle c
                    where a.PrivId = b.PrivId and a.PrivId = c.AutoId and FormId = 7 and PrivNo=(select ReqToId 
            											 From RequestsFollowUp 
            											 where RequestId=" + Session["RequestedReport"] + @" and type=7
            											 and AutoId=(select Max(autoid)
            														 From RequestsFollowUp
            														 where RequestId=" + Session["RequestedReport"] + @" and Type=7))";


            SqlCommand cmdGetSentInfo = new SqlCommand(sql, conn);
            DataTable dtPriv = new DataTable();
            dtPriv.Load(cmdGetSentInfo.ExecuteReader());
            DataRow[] drRowFrom = dtPriv.Select("FacultyNo=" + dtPriv.Rows[0][6]);
            if (drRowFrom.Length >1)
            {
                if (dtPriv.Rows[0][8].ToString() != "0")
                {
                    drRowFrom = dtPriv.Select("DeptId=" + dtPriv.Rows[0][8]);
                }
            }
            sql = @"SELECT *
                    FROM WorkFlow a, Com_Priviliges b,Com_JobTitle c
                    where a.PrivId = b.PrivId and a.PrivId = c.AutoId and FormId = 7 and stepNo=" + (Convert.ToInt16(dtPriv.Rows[0][2]) + 1);// + " and FacultyNo=" + dtPriv.Rows[0][6]; // lblStudFacultyNo.Text;// + " and DeptId=" + lblStudDept.Text;
            cmdGetSentInfo = new SqlCommand(sql, conn);
            DataTable dtPrivTo = new DataTable();
            dtPrivTo.Load(cmdGetSentInfo.ExecuteReader());
            DataRow[] drRow;
            if (dtPrivTo.Rows.Count > 1)
            {
                drRow = dtPrivTo.Select("FacultyNo=" + drRowFrom[0][6]);// + lblStudFacultyNo.Text);
                //if (dtPrivTo.Rows.Count != 0)
                //{
                    
                    if (drRow[0][8].ToString() != "0")
                    {
                        drRow = dtPrivTo.Select("DeptId=" + dtPrivTo.Rows[0][8]);
                    }
                //}
            }
            else //if (dtPrivTo.Rows.Count != 0)
            {
                drRow = dtPrivTo.Select();
            }
            //else
            //{
                //drRow.Length = 0;
            //}
                /*Update current record*/

                string x = Session["UpdatedRecord"].ToString();
                SqlCommand cmdUpdate = new SqlCommand("Update RequestsFollowUp Set ReqStatus=N'انجزت',Decision=1 where AutoId=(select max(autoid) from RequestsFollowUp where RequestId="+Session["RequestedReport"] +" and type=7)", conn);
                cmdUpdate.ExecuteNonQuery();
            if(drRow.Length!=0)
            { 
                /* insert followup information*/
                SqlCommand cmd = new SqlCommand("insert into RequestsFollowUp values(@RequestId,@ReqFromId,@ReqFromName,@ReqToId,@RquToName,@RequestDate,N'قيد الانتظار',@ActualId,@Notes,0,7)", conn);
                cmd.Parameters.AddWithValue("@RequestId", Session["RequestedReport"]);
                cmd.Parameters.AddWithValue("@ReqFromId", drRowFrom[0][4]);
                cmd.Parameters.AddWithValue("@ReqFromName", drRowFrom[0][13] + " - " + (drRowFrom[0][8].ToString() != "0" ? drRowFrom[0][9] : drRowFrom[0][7]));
                cmd.Parameters.AddWithValue("@ReqToId", drRow[0][4]);
                cmd.Parameters.AddWithValue("@RquToName", drRow[0][13] + " - " + (drRow[0][8].ToString() != "0" ? drRow[0][9] : drRow[0][7]));
                cmd.Parameters.AddWithValue("@RequestDate", DateTime.Now.Date);
                cmd.Parameters.AddWithValue("@ActualId", Session["userid"]);
                cmd.Parameters.AddWithValue("@Notes", txtDirNote.Text);
                cmd.ExecuteNonQuery();
            }
            else
            {
                //SqlCommand cmDOriginalSender=
                SqlCommand cmd = new SqlCommand("insert into RequestsFollowUp values(@RequestId,@ReqFromId,@ReqFromName,@ReqToId,@RquToName,@RequestDate,N'مكتمل',@ActualId,@Notes,0,3)", conn);
                cmd.Parameters.AddWithValue("@RequestId", Session["RequestedReport"]);
                cmd.Parameters.AddWithValue("@ReqFromId", drRowFrom[0][4]);
                cmd.Parameters.AddWithValue("@ReqFromName", drRowFrom[0][13] + " - " + (drRowFrom[0][8].ToString() != "0" ? drRowFrom[0][9] : drRowFrom[0][7]));
                cmd.Parameters.AddWithValue("@ReqToId", Session["OriginalSenderId"]);
                cmd.Parameters.AddWithValue("@RquToName", Session["OriginalSenderName"]);
                cmd.Parameters.AddWithValue("@RequestDate", DateTime.Now.Date);
                cmd.Parameters.AddWithValue("@ActualId", Session["userid"]);
                cmd.Parameters.AddWithValue("@Notes", txtDirNote.Text);
                cmd.ExecuteNonQuery();

                SqlCommand cmdUpdate1 = new SqlCommand("Update FollowUpThesisTable Set Status=1 where AutoId=" + Session["RequestedReport"], conn);
                cmdUpdate1.ExecuteNonQuery();
                //SqlCommand cmdUpdate1 = new SqlCommand("Update RequestsFollowUp Set ReqStatus=N'مكتمل',Decision=1 where AutoId=(select max(autoid) from RequestsFollowUp where RequestId=" + Session["RequestedReport"] + " and type=7)", conn);
                //cmdUpdate1.ExecuteNonQuery();
            }
            conn.Close();
        }

        protected void btnDirDisAgree_Click(object sender, EventArgs e)
        {

        }
    }
}