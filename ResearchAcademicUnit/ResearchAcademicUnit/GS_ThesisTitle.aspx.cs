using Oracle.ManagedDataAccess.Client;
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
    public partial class GS_ThesisTitle : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);
        static string OracleConnString = System.Configuration.ConfigurationManager.ConnectionStrings["orcleConStr"].ConnectionString;
        OracleConnection conn1 = new OracleConnection(OracleConnString);
        DataTable dtSupervisor = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uid"] == null)
                Response.Redirect("Login.aspx");
            if (!IsPostBack)
            {
                if (Session["RequestedReport"] == null && Convert.ToInt16(Session["userrole"])!=10)
                    Response.Redirect("Admin_RequestsFollowUp.aspx");
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                if (Convert.ToInt16(Session["userrole"].ToString()) == 10)
                {
                    string sql = @"select * From meu_new.all_students_karz where student_id=" + Session["userid"].ToString();
                    OracleCommand cmdOrc = new OracleCommand(sql, conn1);
                    OracleDataAdapter da = new OracleDataAdapter(cmdOrc);
                    DataTable dtTest = new DataTable();
                    da.Fill(dtTest);
                    if (dtTest.Rows[0][28].ToString().Trim() == "شامل")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('هذا النموذج خاص بطلاب الرسائل فقط','error');", true);
                        Timer3.Enabled = true;
                    }
                    else if (dtTest.Rows[0][23].ToString() != "" && Convert.ToDouble(dtTest.Rows[0][23].ToString()) < 3)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('معدلك التراكمي أقل من 3، لايمكنك التقدم لهذا الطلب','error');", true); ;
                        Timer3.Enabled = true;
                    }
                    //else if(dtTest.Rows[0][31].ToString() != "" || dtTest.Rows[0][32].ToString() != "" || dtTest.Rows[0][33].ToString() != "")
                    //{
                    //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('تم تعيين مشرف مسبقا، لايمكنك التقدم لهذا الطلب','error');", true); ;
                    //    Timer3.Enabled = true;

                    //}
                }


                dtSupervisor.Columns.Add("JobId",typeof(int));
                dtSupervisor.Columns.Add("InstName");
                dtSupervisor.Columns.Add("Status");
                Session["dtSupervisor"] = dtSupervisor;

                SqlCommand cmdWF = new SqlCommand(@"SELECT [AutoId],[FormId],[StepNo], a.[PrivId], b.PrivTo
                                                   FROM WorkFlow a, Com_Priviliges b
                                                   where a.PrivId = b.PrivId and FormId=3", conn);
                DataTable dtWF = new DataTable();
                dtWF.Load(cmdWF.ExecuteReader());
                if (dtWF.Rows.Count != 0)
                {
                    Session["dtWF"] = dtWF;
                    var priv = dtWF.Select("privto=" + Session["uid"]);
                    if (priv.Length == 0)
                    {
                        btnShowDec.Visible = false;
                        Session["StepNo"] = null;
                        getStudInfo(Session["userid"].ToString(), false, 0);
                    }
                    else if (Session["dtData"] != null)
                    {
                        DataTable dtData = (DataTable)Session["dtData"];
                        Session["StepNo"] = priv[0][2];
                        if (Session["StepNo"].ToString() != "1")
                            decisionInfoDiv.Visible = false;
                        getStudInfo(dtData.Rows[0][1].ToString(), true, Convert.ToInt16(Session["RequestedReport"]));
                    }
                }
                else
                {
                    btnShowDec.Visible = false;
                    
                }
                //if (dtWF.Rows.Count != 0)
                //{
                //    Session["dtWF"] = dtWF;
                //    var priv = dtWF.Select("privto=" + Session["uid"]);
                //    if (priv.Length == 0)
                //    {
                //        btnShowDec.Visible = false;
                //    }
                //    else if (Session["dtData"] != null)
                //    {
                //        DataTable dtData = (DataTable)Session["dtData"];
                //        getStudInfo(dtData.Rows[0][1].ToString(), true, Convert.ToInt16(Session["RequestedReport"]));
                //    }
                //    if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                //        conn.Open();
                //    SqlCommand cmdR = new SqlCommand("Select * From ResearcherInfo where AcdId=" + Session["userid"], conn);
                //    SqlDataReader dr = cmdR.ExecuteReader();
                //    dr.Read();
                //    lblSupName.Text = dr[2].ToString();
                //    lblSupDegree.Text = dr[5].ToString();
                //    lblSupMajor.Text = dr[9].ToString();
                //    lblFaculty.Text = dr[3].ToString();
                //    lblSupDept.Text = dr[4].ToString();
                //}
                //else
                //{
                //    btnSend.Visible = false;
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('لم يتم تحديد مسار لهذا النموذج أرجو مراجعة مدير النظام','error');", true);
                //}
                SqlCommand cmd = new SqlCommand("Select * from Com_Faculty where FacId<100", conn);
                ddlFaculty.DataSource = cmd.ExecuteReader();
                ddlFaculty.DataTextField = "FacNameA";
                ddlFaculty.DataValueField = "FacId";
                ddlFaculty.DataBind();
                ddlFaculty.Items.Insert(0, "اختيار الكلية");
                ddlFaculty.Items[0].Value = "";
                conn.Close();
            }
        }

        protected void txtStudName_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnGetData_Click(object sender, EventArgs e)
        {
            if (txtStudId.Text != "")
            {
                getStudInfo(txtStudId.Text, false, 0);
            }
            else
            {
                lblStudName.Text =
                lblStudAvg.Text =
                lblStudFaculty.Text =
                lblStudDept.Text =
                lblStudMajor.Text = "";
            }

        }

        protected void getStudInfo(string studid, bool source, int index)
        {
            string sql = @"select * From meu_new.all_students_karz where student_id=" + studid;
            OracleCommand cmdOrc = new OracleCommand(sql, conn1);
            OracleDataAdapter da = new OracleDataAdapter(cmdOrc);
            DataTable dtTest = new DataTable();
            da.Fill(dtTest);
            if (dtTest.Rows.Count != 0)
            {
                txtStudId.Text = dtTest.Rows[0][0].ToString();
                lblStudName.Text = dtTest.Rows[0][5].ToString();
                lblStudAvg.Text = dtTest.Rows[0][23].ToString();
                lblStudFaculty.Text = dtTest.Rows[0][8].ToString();
                lblStudFacultyNo.Text = dtTest.Rows[0][18].ToString();
                lblStudDept.Text = dtTest.Rows[0][15].ToString();
                lblStudDeptNo.Text = dtTest.Rows[0][14].ToString();
                lblStudMajor.Text = dtTest.Rows[0][9].ToString();
                lblStudStatus.Text = dtTest.Rows[0][21].ToString();

                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("select * from GS_StudThesis where StudId=" + txtStudId.Text, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    if (!source)
                    {
                        if (Convert.ToInt16(dr["status"]) == 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('تم ارسال طلبك وهو قيد الدراسة','info');", true);
                            source = true;
                            btnSend.Visible = false;
                        }
                        else if (Convert.ToInt16(dr["status"]) == 1)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('تم ارسال طلبك وهو معتمد','info');", true);
                            source = true;
                            btnSend.Visible = false;
                        }
                        else if (Convert.ToInt16(dr["status"]) == 2)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('الطلب يحتاج بعض التعديل','info');", true);

                            //else if (Convert.ToInt16(dr["status"]) == 3)
                            //{
                            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('الطلب يحتاج بعض التعديلات','info');", true);
                            btnSend.Visible = true;
                            source = false;
                        }
                    }
                    else
                    {
                        source = true;
                        btnSend.Visible = false;
                    }
                    txtThesisTitleA.Text = dr["ThesisTitleA"].ToString();
                    txtThesisTitleE.Text = dr["ThesisTitleE"].ToString();
                    txtThesisAbstract.Text = dr["ThesisAbstract"].ToString();
                    rdLangType.SelectedValue = dr["LangType"].ToString();

                    txtStudId.ReadOnly = source;
                    txtThesisTitleA.ReadOnly = source;
                    txtThesisTitleE.ReadOnly = source;
                    txtThesisAbstract.ReadOnly = source;
                    cmd = new SqlCommand("Select JobId,InstName,Status From GS_StudThesisSup where SGThId=" + dr["AutoId"], conn);
                    dtSupervisor.Load(cmd.ExecuteReader());
                    Session["dtSupervisor"] = dtSupervisor;
                    GridView1.DataSource = dtSupervisor;
                    GridView1.DataBind();
                    if (source)
                    {
                        GridView1.Columns[2].Visible = false;
                        AddSupDiv.Visible = false;
                    }
                    if(Session["StepNo"]!=null && Convert.ToInt16(Session["StepNo"])==1)
                    {
                        GridView1.Columns[3].Visible = true;
                    }
                    else
                    {
                        GridView1.Columns[3].Visible = false;
                    }
                    Session["SGThId"] = dr["AutoId"];
                }
                else
                    Session["SGThId"] = null;
            }
        }

        protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFaculty.SelectedValue != "")
            {
                if (conn1.State == ConnectionState.Broken || conn1.State == ConnectionState.Closed)
                    conn1.Open();

                DataTable dt = new DataTable();
                OracleCommand cmd = new OracleCommand();
                OracleDataAdapter da = new OracleDataAdapter();
                string sql = "";

                sql = "select distinct dept_no,dept_name from meu_new.faculties_depts_view where dean_id<>0 and faculty_no<>14 and faculty_no=" + ddlFaculty.SelectedValue + " order by faculty_no";
                cmd = new OracleCommand(sql, conn1);
                da = new OracleDataAdapter(cmd);
                da.Fill(dt);
                ddlDept.DataSource = dt;
                ddlDept.DataTextField = "dept_name";
                ddlDept.DataValueField = "dept_no";
                ddlDept.DataBind();
                ddlDept.Items.Insert(0, "اختيار القسم");
                ddlDept.Items[0].Value = "";

                conn1.Close();
            }

        }

        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlDept.SelectedValue!="")
            {
                
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmdAdopted=new SqlCommand(@"SELECT stuff((SELECT ', ' + cast(JobId as varchar(max)) FROM Adopted_Supervisor where status=2 FOR XML PATH('')), 1, 2, '')", conn);
                DataTable dtAdopted = new DataTable();
                dtAdopted.Load(cmdAdopted.ExecuteReader());

                conn.Close();

                if (conn1.State == ConnectionState.Broken || conn1.State == ConnectionState.Closed)
                    conn1.Open();
                DataTable dt = new DataTable();
                OracleCommand cmd = new OracleCommand();
                OracleDataAdapter da = new OracleDataAdapter();
                string sql = "";
                sql = "select distinct instructor_id,instructor_name from meu_new.inst_timetable_view where  faculty_no=" + ddlFaculty.SelectedValue + " and dept_no=" + ddlDept.SelectedValue + " and instructor_id in(" + dtAdopted.Rows[0][0] + ") order by instructor_id";
                cmd = new OracleCommand(sql, conn1);
                da = new OracleDataAdapter(cmd);
                da.Fill(dt);
                ddlSupervisor.DataSource = dt;
                ddlSupervisor.DataTextField = "instructor_name";
                ddlSupervisor.DataValueField = "instructor_id";
                ddlSupervisor.DataBind();
                ddlSupervisor.Items.Insert(0, "اختيار المشرف");
                ddlSupervisor.Items[0].Value = "";
            }
        }

        protected void lnkDeleteSupervisor_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;

            dtSupervisor = (DataTable)Session["dtSupervisor"];
            dtSupervisor.Rows.RemoveAt(row.RowIndex);
            Session["dtSupervisor"] = dtSupervisor;
            GridView1.DataSource = dtSupervisor;
            GridView1.DataBind();
        }

        protected void btnAdSup_Click(object sender, EventArgs e)
        {
            if (GridView1.Rows.Count < 3)
            {
                GridView1.Columns[3].Visible = false;
                if (ddlSupervisor.SelectedValue != "")
                {
                    dtSupervisor = (DataTable)Session["dtSupervisor"];
                    DataRow[] row_found = dtSupervisor.Select("Jobid=" + ddlSupervisor.SelectedValue);
                    if (row_found.Length == 0)
                    {
                        DataRow row = dtSupervisor.NewRow();
                        row[0] = ddlSupervisor.SelectedValue;
                        row[1] = ddlSupervisor.SelectedItem.Text;
                        row[2] = 0;
                        dtSupervisor.Rows.Add(row);
                        Session["dtSupervisor"] = dtSupervisor;
                        GridView1.DataSource = dtSupervisor;
                        GridView1.DataBind();
                        ddlFaculty.SelectedValue = "";
                        ddlDept.Items.Clear();
                        ddlSupervisor.Items.Clear();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('تم اختيار المشرف من قبل','error');", true);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('لا يمكن اختيار اكثر من 3 مشرفين','error');", true);
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (txtThesisTitleA.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('يجب تحديد عنوان الرسالة بالعربي','error');", true);
            }
            else if (txtThesisTitleE.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('يجب تحديد عنوان الرسالة بالانجليزي','error');", true);
            }
            else if (txtThesisAbstract.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('يجب تحديد ملخص الرسالة','error');", true);
            }
            else if (GridView1.Rows.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('يجب تحديد مشرف واحد على الاقل','error');", true);
            }
            else
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                string sql = @"SELECT *
                    FROM WorkFlow a, Com_Priviliges b,Com_JobTitle c
                    where a.PrivId = b.PrivId and a.PrivId = c.AutoId and FormId = 3 and stepno=1 and FacultyNo=" + lblStudFacultyNo.Text;// + " and DeptId=" + lblStudDept.Text;

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
                    SqlCommand cmd = new SqlCommand();
                    string SGThId = "";
                    if (Session["SGThId"] == null)
                    {
                        cmd = new SqlCommand("Insert into GS_StudThesis output inserted.Autoid values(@studid,@titleA,@titleE,@abstract,@date,@userid,0,3,'','','',@SupName,@LangType)", conn);
                        cmd.Parameters.AddWithValue("@studid", txtStudId.Text);
                        cmd.Parameters.AddWithValue("@titleA", txtThesisTitleA.Text);
                        cmd.Parameters.AddWithValue("@titleE", txtThesisTitleE.Text);
                        cmd.Parameters.AddWithValue("@abstract", txtThesisAbstract.Text);
                        cmd.Parameters.AddWithValue("@date", DateTime.Now.Date);
                        cmd.Parameters.AddWithValue("@userid", Session["userid"]);
                        cmd.Parameters.AddWithValue("@SupName", Session["userName"]);
                        cmd.Parameters.AddWithValue("@LangType", rdLangType.SelectedValue);
                        SGThId = cmd.ExecuteScalar().ToString();
                        Session["SGThId"] = SGThId;
                        for (int i = 0; i < GridView1.Rows.Count; i++)
                        {
                            cmd = new SqlCommand("insert into GS_StudThesisSup values(@SGThId,@JobId,@InstName,0)", conn);
                            cmd.Parameters.AddWithValue("@SGThId", SGThId);
                            cmd.Parameters.AddWithValue("@JobId", GridView1.Rows[i].Cells[0].Text);
                            cmd.Parameters.AddWithValue("@InstName", GridView1.Rows[i].Cells[1].Text);
                            cmd.ExecuteNonQuery();
                        }
                        //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('تم ارسال طلبك بنجاح وهو قيد الدراسة','info');", true);
                    }
                    else
                    {
                        cmd = new SqlCommand("Update GS_StudThesis set ThesisTitleA=@ThesisTitleA,ThesisTitleE=@ThesisTitleE,ThesisAbstract=@ThesisAbstract where AutoId=" + Session["SGThId"], conn);
                        cmd.Parameters.AddWithValue("@ThesisTitleA", txtThesisTitleA.Text);
                        cmd.Parameters.AddWithValue("@ThesisTitleE", txtThesisTitleE.Text);
                        cmd.Parameters.AddWithValue("@ThesisAbstract", txtThesisAbstract.Text);
                        cmd.Parameters.AddWithValue("@LangType", rdLangType.SelectedValue);
                        cmd.ExecuteNonQuery(); 
                        cmd = new SqlCommand("Delete From GS_StudThesisSup where SGThId=" + Session["SGThId"], conn);
                        cmd.ExecuteNonQuery();
                        SGThId = Session["SGThId"].ToString();
                        for (int i = 0; i < GridView1.Rows.Count; i++)
                        {
                            cmd = new SqlCommand("insert into GS_StudThesisSup values(@SGThId,@JobId,@InstName,0)", conn);
                            cmd.Parameters.AddWithValue("@SGThId", SGThId);
                            cmd.Parameters.AddWithValue("@JobId", GridView1.Rows[i].Cells[0].Text);
                            cmd.Parameters.AddWithValue("@InstName", GridView1.Rows[i].Cells[1].Text);
                            cmd.ExecuteNonQuery();
                        }
                        Session["SGThId"] = SGThId;

                        SqlCommand cmdUpdate = new SqlCommand("Update RequestsFollowUp Set ReqStatus=N'انجزت',Decision=1 where AutoId=(select max(autoid) from RequestsFollowUp where RequestId=" + Session["RequestedReport"] + " and type=3)", conn);
                        cmdUpdate.ExecuteNonQuery();

                    }

                    cmd = new SqlCommand("insert into RequestsFollowUp values(@RequestId,@ReqFromId,@ReqFromName,@ReqToId,@RquToName,@RequestDate,N'قيد الانتظار',@ActualId,@Notes,0,3)", conn);
                    cmd.Parameters.AddWithValue("@RequestId", SGThId);
                    cmd.Parameters.AddWithValue("@ReqFromId", Session["userid"]);
                    cmd.Parameters.AddWithValue("@ReqFromName", lblStudName.Text);
                    cmd.Parameters.AddWithValue("@ReqToId", dr[0][4]);
                    cmd.Parameters.AddWithValue("@RquToName", dtPriv.Rows[0][13] + " - " + (dtPriv.Rows[0][8].ToString() != "0" ? dr[0][9] : dr[0][7]));
                    cmd.Parameters.AddWithValue("@RequestDate", DateTime.Now.Date);
                    cmd.Parameters.AddWithValue("@ActualId", Session["userid"]);
                    cmd.Parameters.AddWithValue("@Notes", txtDirNote.Text);
                    cmd.ExecuteNonQuery();

                    /* end */
                    conn.Close();
                    Timer1.Enabled = true;
                }
                else
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('لم يتم تحديد رئيس القسم لهذا الطالب','error');", true);


                conn.Close();
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Response.Redirect("GS_ThesisTitle.aspx");
        }

        protected void btnDisAgree_Click(object sender, EventArgs e)
        {
            if (Session["RequestedReport"] != null)
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                string sql = @"SELECT *
                    FROM WorkFlow a, Com_Priviliges b,Com_JobTitle c
                    where a.PrivId = b.PrivId and a.PrivId = c.AutoId and FormId = 3 and PrivNo=(select ReqToId 
            											 From RequestsFollowUp 
            											 where RequestId=" + Session["RequestedReport"] + @" and type=3
            											 and AutoId=(select Max(autoid)
            														 From RequestsFollowUp
            														 where RequestId=" + Session["RequestedReport"] + @" and Type=3))";

                SqlCommand cmdGetSentInfo = new SqlCommand(sql, conn);
                DataTable dtPriv = new DataTable();
                dtPriv.Load(cmdGetSentInfo.ExecuteReader());
                DataRow[] drRowFrom = dtPriv.Select("FacultyNo=" + dtPriv.Rows[0][6]);
                if (drRowFrom.Length > 1)
                {
                    if (dtPriv.Rows[0][8].ToString() != "0")
                    {
                        drRowFrom = dtPriv.Select("DeptId=" + dtPriv.Rows[0][8]);
                    }
                }
                sql = @"SELECT *
                    FROM WorkFlow a, Com_Priviliges b,Com_JobTitle c
                    where a.PrivId = b.PrivId and a.PrivId = c.AutoId and FormId = 3 and stepNo=" + (Convert.ToInt16(dtPriv.Rows[0][2]) - 1);// + " and FacultyNo=" + dtPriv.Rows[0][6]; // lblStudFacultyNo.Text;// + " and DeptId=" + lblStudDept.Text;
                cmdGetSentInfo = new SqlCommand(sql, conn);
                DataTable dtPrivTo = new DataTable();
                dtPrivTo.Load(cmdGetSentInfo.ExecuteReader());
                DataRow[] drRow;
                if (dtPrivTo.Rows.Count > 1)
                {
                    drRow = dtPrivTo.Select("FacultyNo=" + drRowFrom[0][6]);
                    if (drRow[0][8].ToString() != "0")
                    {
                        drRow = dtPrivTo.Select("DeptId=" + dtPrivTo.Rows[0][8]);
                    }
                }
                else
                {
                    drRow = dtPrivTo.Select();
                }
                /*Update current record*/

                //if (Session["StepNo"] != null && Convert.ToInt16(Session["StepNo"]) == 2)
                //{
                //    SqlCommand cmdSup = new SqlCommand("Update GS_StudThesisSup set status=0 where SGThId=" + Session["RequestedReport"], conn);
                //    cmdSup.ExecuteNonQuery();

                //    cmdSup = new SqlCommand("Update GS_StudThesisSup set status=1 where SGThId=" + Session["RequestedReport"] + " and JobId=" + Session["SupJobId"], conn);
                //    cmdSup.ExecuteNonQuery();
                //}

                string x = Session["UpdatedRecord"].ToString();
                SqlCommand cmdUpdate = new SqlCommand("Update RequestsFollowUp Set ReqStatus=N'انجزت',Decision=2 where AutoId=(select max(autoid) from RequestsFollowUp where RequestId=" + Session["RequestedReport"] + " and type=3)", conn);
                cmdUpdate.ExecuteNonQuery();

                
                //if(drPrev.HasRows)
                if (drRow.Length != 0)
                {
                    cmdUpdate = new SqlCommand("Select * from RequestsFollowUp where AutoId=(select max(autoid) from RequestsFollowUp where RequestId=" + Session["RequestedReport"] + " and type=3)", conn);
                    SqlDataReader drPrev = cmdUpdate.ExecuteReader();
                    /* insert followup information*/
                    drPrev.Read();
                    SqlCommand cmd = new SqlCommand("insert into RequestsFollowUp values(@RequestId,@ReqFromId,@ReqFromName,@ReqToId,@RquToName,@RequestDate,N'قيد الانتظار',@ActualId,@Notes,0,3)", conn);
                    cmd.Parameters.AddWithValue("@RequestId", Session["RequestedReport"]);
                    cmd.Parameters.AddWithValue("@ReqFromId", drRowFrom[0][4]);
                    cmd.Parameters.AddWithValue("@ReqFromName", drRowFrom[0][13] + " - " + (drRowFrom[0][8].ToString() != "0" ? drRowFrom[0][9] : drRowFrom[0][7]));
                    cmd.Parameters.AddWithValue("@ReqToId", drPrev[2]);
                    cmd.Parameters.AddWithValue("@RquToName", drPrev[3]);
                    //cmd.Parameters.AddWithValue("@ReqToId", drRow[0][4]);
                    //cmd.Parameters.AddWithValue("@RquToName", drRow[0][13] + " - " + (drRow[0][8].ToString() != "0" ? drRow[0][9] : drRow[0][7]));
                    cmd.Parameters.AddWithValue("@RequestDate", DateTime.Now.Date);
                    cmd.Parameters.AddWithValue("@ActualId", Session["userid"]);
                    cmd.Parameters.AddWithValue("@Notes", txtDirNote.Text);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("insert into RequestsFollowUp values(@RequestId,@ReqFromId,@ReqFromName,@ReqToId,@RquToName,@RequestDate,N'قيد الانتظار',@ActualId,@Notes,0,3)", conn);
                    cmd.Parameters.AddWithValue("@RequestId", Session["RequestedReport"]);
                    cmd.Parameters.AddWithValue("@ReqFromId", drRowFrom[0][4]);
                    cmd.Parameters.AddWithValue("@ReqFromName", drRowFrom[0][13] + " - " + (drRowFrom[0][8].ToString() != "0" ? drRowFrom[0][9] : drRowFrom[0][7]));
                    cmd.Parameters.AddWithValue("@ReqToId", Session["OriginalSenderId"]);
                    cmd.Parameters.AddWithValue("@RquToName", Session["OriginalSenderName"]);
                    cmd.Parameters.AddWithValue("@RequestDate", DateTime.Now.Date);
                    cmd.Parameters.AddWithValue("@ActualId", Session["userid"]);
                    cmd.Parameters.AddWithValue("@Notes", txtDirNote.Text);
                    cmd.ExecuteNonQuery();

                    SqlCommand cmdUpdate1 = new SqlCommand("Update GS_StudThesis Set Status=2 where AutoId=" + Session["RequestedReport"], conn);
                    cmdUpdate1.ExecuteNonQuery();
                }
                conn.Close();
                Session["RequestedReport"] = null;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('تم ارسال الطلب','info');", true);
                Timer2.Enabled = true;

                //if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                //    conn.Open();

                //string sql = @"SELECT *
                //    FROM WorkFlow a, Com_Priviliges b,Com_JobTitle c
                //    where a.PrivId = b.PrivId and a.PrivId = c.AutoId and FormId = 3 and PrivNo=(select ReqToId 
                //							 From RequestsFollowUp 
                //							 where RequestId=" + Session["RequestedReport"] + @" and type=3
                //							 and AutoId=(select Max(autoid)
                //										 From RequestsFollowUp
                //										 where RequestId=" + Session["RequestedReport"] + @" and Type=3))";


                //SqlCommand cmdGetSentInfo = new SqlCommand(sql, conn);
                //DataTable dtPriv = new DataTable();
                //dtPriv.Load(cmdGetSentInfo.ExecuteReader());
                //DataRow[] drRowFrom = dtPriv.Select("FacultyNo=" + dtPriv.Rows[0][6]);
                //if (drRowFrom.Length > 1)
                //{
                //    if (dtPriv.Rows[0][8].ToString() != "0")
                //    {
                //        drRowFrom = dtPriv.Select("DeptId=" + dtPriv.Rows[0][8]);
                //    }
                //}
                ///*Update current record*/

                //string x = Session["UpdatedRecord"].ToString();
                //SqlCommand cmdUpdate = new SqlCommand("Update RequestsFollowUp Set ReqStatus=N'انجزت',Decision=2 where AutoId=(select max(autoid) from RequestsFollowUp where RequestId=" + Session["RequestedReport"] + " and type=3)", conn);
                //cmdUpdate.ExecuteNonQuery();

                //    SqlCommand cmd = new SqlCommand("insert into RequestsFollowUp values(@RequestId,@ReqFromId,@ReqFromName,@ReqToId,@RquToName,@RequestDate,N'مرفوض',@ActualId,@Notes,0,3)", conn);
                //    cmd.Parameters.AddWithValue("@RequestId", Session["RequestedReport"]);
                //    cmd.Parameters.AddWithValue("@ReqFromId", drRowFrom[0][4]);
                //    cmd.Parameters.AddWithValue("@ReqFromName", drRowFrom[0][13] + " - " + (drRowFrom[0][8].ToString() != "0" ? drRowFrom[0][9] : drRowFrom[0][7]));
                //    cmd.Parameters.AddWithValue("@ReqToId", Session["OriginalSenderId"]);
                //    cmd.Parameters.AddWithValue("@RquToName", Session["OriginalSenderName"]);
                //    cmd.Parameters.AddWithValue("@RequestDate", DateTime.Now.Date);
                //    cmd.Parameters.AddWithValue("@ActualId", Session["userid"]);
                //    cmd.Parameters.AddWithValue("@Notes", txtDirNote.Text);
                //    cmd.ExecuteNonQuery();

                //    SqlCommand cmdUpdate1 = new SqlCommand("Update GS_StudThesis Set Status=2 where AutoId=" + Session["RequestedReport"], conn);
                //    cmdUpdate1.ExecuteNonQuery();
                //conn.Close();
                //Session["RequestedReport"] = null;
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('تم ارسال الطلب','info');", true);
                //Timer2.Enabled = true;
            }
        }

        protected void btnDirAgree_Click(object sender, EventArgs e)
        {
            //if (Session["RequestedReport"] != null)
            //{
            //bool SupervisorSelected = false;
            //for (int i = 0; i < GridView1.Rows.Count; i++)
            //{
            //    RadioButton rd = GridView1.Rows[i].FindControl("rdAdoptedSup") as RadioButton;
            //    if (rd.Checked == true)
            //    {
            //        SupervisorSelected = true;
            //        Session["SupJobId"] = GridView1.Rows[i].Cells[0].Text;
            //        break;
            //    }
            //}
            //if (Session["StepNo"] != null && Convert.ToInt16(Session["StepNo"]) == 2 && !SupervisorSelected)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('يجب تحديد المشرف','error');", true);
            //}
            //else
            //{
            //if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
            //    conn.Open();

            //string sql = @"SELECT *
            //FROM WorkFlow a, Com_Priviliges b,Com_JobTitle c
            //where a.PrivId = b.PrivId and a.PrivId = c.AutoId and FormId = 3 and PrivNo=(select ReqToId 
            //			 From RequestsFollowUp 
            //			 where RequestId=" + Session["RequestedReport"] + @" and type=3
            //			 and AutoId=(select Max(autoid)
            //						 From RequestsFollowUp
            //						 where RequestId=" + Session["RequestedReport"] + @" and Type=3))";

            //SqlCommand cmdGetSentInfo = new SqlCommand(sql, conn);
            //DataTable dtPriv = new DataTable();
            //dtPriv.Load(cmdGetSentInfo.ExecuteReader());
            //DataRow[] drRowFrom = dtPriv.Select("FacultyNo=" + dtPriv.Rows[0][6]);
            //if (drRowFrom.Length > 1)
            //{
            //    if (dtPriv.Rows[0][8].ToString() != "0")
            //    {
            //        drRowFrom = dtPriv.Select("DeptId=" + dtPriv.Rows[0][8]);
            //    }
            //}
            //sql = @"SELECT *
            //FROM WorkFlow a, Com_Priviliges b,Com_JobTitle c
            //where a.PrivId = b.PrivId and a.PrivId = c.AutoId and FormId = 3 and stepNo=" + (Convert.ToInt16(dtPriv.Rows[0][2]) - 1);// + " and FacultyNo=" + dtPriv.Rows[0][6]; // lblStudFacultyNo.Text;// + " and DeptId=" + lblStudDept.Text;
            //cmdGetSentInfo = new SqlCommand(sql, conn);
            //DataTable dtPrivTo = new DataTable();
            //dtPrivTo.Load(cmdGetSentInfo.ExecuteReader());
            //DataRow[] drRow;
            //if (dtPrivTo.Rows.Count > 1)
            //{
            //    drRow = dtPrivTo.Select("FacultyNo=" + drRowFrom[0][6]);
            //    if (drRow[0][8].ToString() != "0")
            //    {
            //        drRow = dtPrivTo.Select("DeptId=" + dtPrivTo.Rows[0][8]);
            //    }
            //}
            //else
            //{
            //    drRow = dtPrivTo.Select();
            //}
            ///*Update current record*/

            ////if (Session["StepNo"] != null && Convert.ToInt16(Session["StepNo"]) == 2)
            ////{
            ////    SqlCommand cmdSup = new SqlCommand("Update GS_StudThesisSup set status=0 where SGThId=" + Session["RequestedReport"], conn);
            ////    cmdSup.ExecuteNonQuery();

            ////    cmdSup = new SqlCommand("Update GS_StudThesisSup set status=1 where SGThId=" + Session["RequestedReport"] + " and JobId=" + Session["SupJobId"], conn);
            ////    cmdSup.ExecuteNonQuery();
            ////}

            //string x = Session["UpdatedRecord"].ToString();
            //SqlCommand cmdUpdate = new SqlCommand("Update RequestsFollowUp Set ReqStatus=N'انجزت',Decision=1 where AutoId=(select max(autoid) from RequestsFollowUp where RequestId=" + Session["RequestedReport"] + " and type=3)", conn);
            //cmdUpdate.ExecuteNonQuery();
            //if (drRow.Length != 0)
            //{
            //    /* insert followup information*/
            //    SqlCommand cmd = new SqlCommand("insert into RequestsFollowUp values(@RequestId,@ReqFromId,@ReqFromName,@ReqToId,@RquToName,@RequestDate,N'قيد الانتظار',@ActualId,@Notes,0,3)", conn);
            //    cmd.Parameters.AddWithValue("@RequestId", Session["RequestedReport"]);
            //    cmd.Parameters.AddWithValue("@ReqFromId", drRowFrom[0][4]);
            //    cmd.Parameters.AddWithValue("@ReqFromName", drRowFrom[0][13] + " - " + (drRowFrom[0][8].ToString() != "0" ? drRowFrom[0][9] : drRowFrom[0][7]));
            //    cmd.Parameters.AddWithValue("@ReqToId", drRow[0][4]);
            //    cmd.Parameters.AddWithValue("@RquToName", drRow[0][13] + " - " + (drRow[0][8].ToString() != "0" ? drRow[0][9] : drRow[0][7]));
            //    cmd.Parameters.AddWithValue("@RequestDate", DateTime.Now.Date);
            //    cmd.Parameters.AddWithValue("@ActualId", Session["userid"]);
            //    cmd.Parameters.AddWithValue("@Notes", txtDirNote.Text);
            //    cmd.ExecuteNonQuery();
            //}
            //else
            //{
            //    SqlCommand cmd = new SqlCommand("insert into RequestsFollowUp values(@RequestId,@ReqFromId,@ReqFromName,@ReqToId,@RquToName,@RequestDate,N'مكتمل',@ActualId,@Notes,0,3)", conn);
            //    cmd.Parameters.AddWithValue("@RequestId", Session["RequestedReport"]);
            //    cmd.Parameters.AddWithValue("@ReqFromId", drRowFrom[0][4]);
            //    cmd.Parameters.AddWithValue("@ReqFromName", drRowFrom[0][13] + " - " + (drRowFrom[0][8].ToString() != "0" ? drRowFrom[0][9] : drRowFrom[0][7]));
            //    cmd.Parameters.AddWithValue("@ReqToId", Session["OriginalSenderId"]);
            //    cmd.Parameters.AddWithValue("@RquToName", Session["OriginalSenderName"]);
            //    cmd.Parameters.AddWithValue("@RequestDate", DateTime.Now.Date);
            //    cmd.Parameters.AddWithValue("@ActualId", Session["userid"]);
            //    cmd.Parameters.AddWithValue("@Notes", txtDirNote.Text);
            //    cmd.ExecuteNonQuery();

            //    SqlCommand cmdUpdate1 = new SqlCommand("Update GS_StudThesis Set Status=2 where AutoId=" + Session["RequestedReport"], conn);
            //    cmdUpdate1.ExecuteNonQuery();
            //}
            //conn.Close();
            //Session["RequestedReport"] = null;
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('تم ارسال الطلب','info');", true);
            //Timer2.Enabled = true;
            //}
            //}

            if (Session["RequestedReport"] != null)
            {
                bool SupervisorSelected = false;
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    RadioButton rd = GridView1.Rows[i].FindControl("rdAdoptedSup") as RadioButton;
                    if (rd.Checked == true)
                    {
                        SupervisorSelected = true;
                        Session["SupJobId"] = GridView1.Rows[i].Cells[0].Text;
                        break;
                    }
                }
                if (Session["StepNo"] != null && Convert.ToInt16(Session["StepNo"]) == 1 && !SupervisorSelected)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('يجب تحديد المشرف','error');", true);
                }
                else
                {
                    if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                        conn.Open();

                    string sql = @"SELECT *
                    FROM WorkFlow a, Com_Priviliges b,Com_JobTitle c
                    where a.PrivId = b.PrivId and a.PrivId = c.AutoId and FormId = 3 and PrivNo=(select ReqToId 
            											 From RequestsFollowUp 
            											 where RequestId=" + Session["RequestedReport"] + @" and type=3
            											 and AutoId=(select Max(autoid)
            														 From RequestsFollowUp
            														 where RequestId=" + Session["RequestedReport"] + @" and Type=3))";

                    SqlCommand cmdGetSentInfo = new SqlCommand(sql, conn);
                    DataTable dtPriv = new DataTable();
                    dtPriv.Load(cmdGetSentInfo.ExecuteReader());
                    DataRow[] drRowFrom = dtPriv.Select("FacultyNo=" + dtPriv.Rows[0][6]);
                    if (drRowFrom.Length > 1)
                    {
                        if (dtPriv.Rows[0][8].ToString() != "0")
                        {
                            drRowFrom = dtPriv.Select("DeptId=" + dtPriv.Rows[0][8]);
                        }
                    }
                    sql = @"SELECT *
                    FROM WorkFlow a, Com_Priviliges b,Com_JobTitle c
                    where a.PrivId = b.PrivId and a.PrivId = c.AutoId and FormId = 3 and stepNo=" + (Convert.ToInt16(dtPriv.Rows[0][2]) + 1);// + " and FacultyNo=" + dtPriv.Rows[0][6]; // lblStudFacultyNo.Text;// + " and DeptId=" + lblStudDept.Text;
                    cmdGetSentInfo = new SqlCommand(sql, conn);
                    DataTable dtPrivTo = new DataTable();
                    dtPrivTo.Load(cmdGetSentInfo.ExecuteReader());
                    DataRow[] drRow;
                    if (dtPrivTo.Rows.Count > 1)
                    {
                        drRow = dtPrivTo.Select("FacultyNo=" + drRowFrom[0][6]);
                        if (drRow[0][8].ToString() != "0")
                        {
                            drRow = dtPrivTo.Select("DeptId=" + dtPrivTo.Rows[0][8]);
                        }
                    }
                    else
                    {
                        drRow = dtPrivTo.Select();
                    }
                    /*Update current record*/

                    if (Session["StepNo"] != null && Convert.ToInt16(Session["StepNo"]) == 1)
                    {

                        SqlCommand cmdSup = new SqlCommand("Update GS_StudThesisSup set status=0 where SGThId=" + Session["RequestedReport"], conn);
                        cmdSup.ExecuteNonQuery();



                        cmdSup = new SqlCommand("Update GS_StudThesisSup set status=1 where SGThId=" + Session["RequestedReport"] + " and JobId=" + Session["SupJobId"], conn);
                        cmdSup.ExecuteNonQuery();

                        SqlCommand cmdUpdate1 = new SqlCommand("Update GS_StudThesis Set DecNo=@dn,DecMeeting=@dm,DecDate=@dd where AutoId=" + Session["RequestedReport"], conn);
                        cmdUpdate1.Parameters.AddWithValue("@dn",txtDecNo.Text);
                        cmdUpdate1.Parameters.AddWithValue("@dm",txtDecMeetNo.Text);
                        cmdUpdate1.Parameters.AddWithValue("@dd", txtDecMeetDate.Text);
                        cmdUpdate1.ExecuteNonQuery();
                    }

                    string x = Session["UpdatedRecord"].ToString();
                    SqlCommand cmdUpdate = new SqlCommand("Update RequestsFollowUp Set ReqStatus=N'انجزت',Decision=1 where AutoId=(select max(autoid) from RequestsFollowUp where RequestId=" + Session["RequestedReport"] + " and type=3)", conn);
                    cmdUpdate.ExecuteNonQuery();
                    if (drRow.Length != 0)
                    {
                        /* insert followup information*/
                        SqlCommand cmd = new SqlCommand("insert into RequestsFollowUp values(@RequestId,@ReqFromId,@ReqFromName,@ReqToId,@RquToName,@RequestDate,N'قيد الانتظار',@ActualId,@Notes,0,3)", conn);
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

                        SqlCommand cmdUpdate1 = new SqlCommand("Update GS_StudThesis Set Status=1 where AutoId=" + Session["RequestedReport"], conn);
                        cmdUpdate1.ExecuteNonQuery();
                    }
                    conn.Close();
                    Session["RequestedReport"] = null;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('تم ارسال الطلب','info');", true);
                    Timer2.Enabled = true;
                }
            }
        }

        protected void Timer2_Tick(object sender, EventArgs e)
        {
            Response.Redirect("Admin_RequestsFollowUp.aspx");
        }

        protected void Timer3_Tick(object sender, EventArgs e)
        {
            Response.Redirect("HomePage.aspx");
        }

        protected void rdAdoptedSup_CheckedChanged(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                RadioButton rd = GridView1.Rows[i].FindControl("rdAdoptedSup") as RadioButton;
                if (i == row.RowIndex)
                    rd.Checked = true;
                else
                    rd.Checked = false;
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType==DataControlRowType.DataRow)
            {
                if (e.Row.Cells[4].Text == "1")
                {
                    e.Row.Cells[4].Text = "معتمد";
                    e.Row.Attributes.Add("class", "bg-success");
                }
                else if (e.Row.Cells[4].Text == "0")
                    e.Row.Cells[4].Text = "غير معتمد";
            }
        }
    }
}