using Oracle.ManagedDataAccess.Client;
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
    public partial class GS_NoObjection : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);
        static string connstringCV = System.Configuration.ConfigurationManager.ConnectionStrings["MEUCV"].ConnectionString;
        SqlConnection connCV = new SqlConnection(connstringCV);
        static string OracleConnString = System.Configuration.ConfigurationManager.ConnectionStrings["orcleConStr"].ConnectionString;
        OracleConnection conn1 = new OracleConnection(OracleConnString);
        DataTable dtSupervisor = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uid"] == null)
                Response.Redirect("Login.aspx");
            if (!IsPostBack)
            {
                ViewState["HasPosted"] = false;
                if (Session["RequestedReport"] == null && Convert.ToInt16(Session["userrole"]) != 10)
                    Response.Redirect("Admin_RequestsFollowUp.aspx");

                HtmlGenericControl divh = (HtmlGenericControl)Page.Master.FindControl("prinOut");
                divh.Visible = false;

                HtmlGenericControl divf = (HtmlGenericControl)Page.Master.FindControl("printfooter");
                divf.Visible = false;

                //if (Session["PrintForm"] != null && Convert.ToBoolean(Session["PrintForm"]) == true)
                //    buttonsDiv.Visible = false;

                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                if (Convert.ToInt16(Session["userrole"].ToString()) == 10)
                {
                    string sql = @"select * From meu_new.all_students_karz where student_id=" + Session["userid"].ToString();
                    OracleCommand cmdOrc = new OracleCommand(sql, conn1);
                    OracleDataAdapter da = new OracleDataAdapter(cmdOrc);
                    DataTable dtTest = new DataTable();
                    da.Fill(dtTest);
                    if (dtTest.Rows[0][21].ToString().Trim() != "منتظم")
                    {
                        btnSend.Visible = false;
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('هذا النموذج خاص بالطلاب المنتظمين فقط','error');", true);
                        Timer3.Enabled = true;
                    }
                    else if (dtTest.Rows[0][23].ToString() != "" && Convert.ToDouble(dtTest.Rows[0][23].ToString()) < 3)
                    {
                        btnSend.Visible = false;
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('معدلك التراكمي أقل من 3، لايمكنك التقدم لهذا الطلب','error');", true); ;
                        Timer3.Enabled = true;
                    }
                    else if (Convert.ToInt16(dtTest.Rows[0][24].ToString().Trim()) < 15)
                    {
                        btnSend.Visible = false;
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('يجب ان تكون عدد الساعات المقطوعة بنجاح 15 ساعة فأكثر','error');", true); ;
                        Timer3.Enabled = true;

                    }
                }


                dtSupervisor.Columns.Add("SupId");
                dtSupervisor.Columns.Add("SupName");
                dtSupervisor.Columns.Add("SupDesc");
                dtSupervisor.Columns.Add("SupWorkPlace");
                dtSupervisor.Columns.Add("SupMajor");
                dtSupervisor.Columns.Add("SupDegree");
                dtSupervisor.Columns.Add("SupDegreeT");
                dtSupervisor.Columns.Add("InsertedBy");
                dtSupervisor.Columns.Add("InsertedByDesc");
                
                Session["dtSupervisor"] = dtSupervisor;

                SqlCommand cmdWF = new SqlCommand(@"SELECT [AutoId],[FormId],[StepNo], a.[PrivId], b.PrivTo,
                                                    (select JobTitleA From Com_JobTitle where a.PrivId=Com_JobTitle.AutoId) JobTitleA
                                                   FROM WorkFlow a
                                                    FULL OUTER JOIN Com_Priviliges b on
                                                   a.PrivId = b.PrivId and FormId=8", conn);
                DataTable dtWF = new DataTable();
                dtWF.Load(cmdWF.ExecuteReader());
                if (dtWF.Rows.Count != 0)
                {
                    Session["dtWF"] = dtWF;
                    var priv = dtWF.Select("privto=" + Session["uid"]);
                    if (priv.Length == 0 && Session["uid"].ToString().Length == 9)
                    {
                        //btnShowDec.Visible = true;
                        Session["StepNo"] = null;
                        getStudInfo(Session["userid"].ToString(), false, 0);
                        
                    }
                    else if (Session["dtData"] != null)
                    {
                        if (priv.Length == 0)
                            priv = dtWF.Select("stepno=3 and Formid=8");
                        string c= priv[0][5].ToString();
                        Session["desc"]=priv[0][5];

                        buttonsDiv.Visible = false;
                        DataTable dtData = (DataTable)Session["dtData"];
                        Session["StepNo"] = priv[0][2];
                        SqlCommand cmd = new SqlCommand("Select Max(StepNo) From WorkFlow where FormId=8", conn);
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            dr.Read();
                            if (Convert.ToInt16(dr[0].ToString()) == Convert.ToInt16(Session["StepNo"]))
                                Session["GS_Adopted"] = true;
                            else
                                Session["GS_Adopted"] = null;
                        }
                        
                        switch (Convert.ToInt32(priv[0][2]))
                        {
                            case 1:
                                RegDiv.Visible = true;
                                break;
                            case 2:
                                FinDiv.Visible = true;
                                break;
                            default:
                                SupervisorDiv.Visible = true;
                                if (Convert.ToInt16(priv[0][2].ToString()) == 3)
                                {
                                    btnSendSupInfo.Visible = true;
                                    btnDirAgree.Visible = false;
                                    //btnDisAgree.Visible = false;
                                }
                                else
                                {
                                    btnSendSupInfo.Visible = false;
                                    btnDirAgree.Visible = true;
                                    //btnDisAgree.Visible = true;
                                }
                                cmd = new SqlCommand(@"Select *,
                                                      (case when SupDegree=1 then N'استاذ' when SupDegree=2 then N'استاذ مشارك' when SupDegree=3 then N'استاذ مساعد' end) SupDegreeT,
                                                      (case when InOut='IN' then N'مناقش داخلي' when InOut='OUT' then N'مناقش خارجي' when InOut='VIEW' then N'مراقب للجلسة' end) placeT 
                                                      From GS_NoObjectionCommittee where ReqId=" + Session["RequestedReport"] + " order by InOut", conn);
                                dtSupervisor = new DataTable();
                                dtSupervisor.Load(cmd.ExecuteReader());
                                Session["dtSupervisor"] = dtSupervisor;
                                GridView1.DataSource = dtSupervisor;
                                GridView1.DataBind();
                                break;
                        }
                        //if (Session["StepNo"].ToString() != "1")
                        //    decisionInfoDiv.Visible = false;
                        getStudInfo(dtData.Rows[0][1].ToString(), true, Convert.ToInt16(Session["RequestedReport"]));
                    }
                }
                else
                {
                    //btnShowDec.Visible = false;

                }
                conn.Close();
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
                txtThesisTitleOriginal.Text = dtTest.Rows[0]["Thesis_arabic"].ToString();
                txtThesisTitleOriginal.ReadOnly = true;
                txtThesisTitleTranslate.Text = dtTest.Rows[0]["Thesis_eng"].ToString();
                txtThesisTitleTranslate.ReadOnly = true;
                //currentTrack.InnerText = dtTest.Rows[0][28].ToString().Trim();
                //if (dtTest.Rows[0][28].ToString().Trim() == "شامل")
                //    newTrack.InnerText = "رسالة";
                //else
                //    newTrack.InnerText = "شامل";
                string sem = dtTest.Rows[0][16].ToString().Trim().Substring(dtTest.Rows[0][16].ToString().Trim().Length - 1, 1);
                lblJoinSemester.Text = sem == "1" ? "الفصل الاول" : sem == "2" ? "الفصل الثاني" : "الفصل الصيفي";
                lblJoinYear.Text = dtTest.Rows[0][16].ToString().Trim().Substring(0, 4) + "/" + (Convert.ToInt16(dtTest.Rows[0][16].ToString().Trim().Substring(0, 4)) + 1);
                lblHourSuccess.Text = dtTest.Rows[0][24].ToString().Trim();
                lblSupervisorName.Text = dtTest.Rows[0][33].ToString().Trim();
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("select * from GS_NoObjectionThesis where StudId=" + txtStudId.Text, conn);
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

                            btnSend.Visible = true;
                            source = false;
                        }
                    }
                    else
                    {
                        source = true;
                        btnSend.Visible = false;
                        
                        cmd = new SqlCommand("Select * From GS_NoObjectionRegistration where ReqId=" + Session["RequestedReport"], conn);
                        SqlDataReader drReg = cmd.ExecuteReader();
                        if(drReg.HasRows)
                        {
                            drReg.Read();
                            txtDiscussionDay.Text = drReg["DiscussionDay"].ToString();
                            string d = Convert.ToDateTime(drReg["DiscussionDate"].ToString()).ToString("yyyy-MM-dd");
                            txtDiscussionDate.Text = d;// dr["BOD"].ToString();
                            //txtDiscussionDate.Text = drReg["DiscussionDate"].ToString();
                            txtPlaceTime.Text = drReg["DiscussionPlace"].ToString();
                        }

                        

                    }

                    txtStudId.ReadOnly = source;
                    //Session["SGThId"] = dr["AutoId"];
                    Timer1.Enabled = true;
                }
                else
                    Session["SGNoObjId"] = null;
            }
        }

        protected void Timer3_Tick(object sender, EventArgs e)
        {
            Response.Redirect("HomePage.aspx");
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

        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            string sql = @"SELECT *
                    FROM WorkFlow a, Com_Priviliges b,Com_JobTitle c
                    where a.PrivId = b.PrivId and a.PrivId = c.AutoId and FormId = 8 and stepno=1";// and FacultyNo=" + lblStudFacultyNo.Text;// + " and DeptId=" + lblStudDept.Text;

            SqlCommand cmdGetSentInfo = new SqlCommand(sql, conn);
            DataTable dtPriv = new DataTable();
            dtPriv.Load(cmdGetSentInfo.ExecuteReader());
            //DataRow[] dr = dtPriv.Select("FacultyNo=" + lblStudFacultyNo.Text);
            //if (dtPriv.Rows[0][8].ToString() != "0")
            //{
            //    dr = dtPriv.Select("DeptId=" + lblStudDeptNo.Text);
            //}

            if (dtPriv.Rows.Count != 0)
            {
                SqlCommand cmd = new SqlCommand();
                string SGNoObjId = "";
                if (Session["SGNoObjId"] == null)
                {
                    cmd = new SqlCommand("Insert into GS_NoObjectionThesis output inserted.Autoid values(@studid,@ThesisA,@ThesisE,@date,@userid,0,8,@SupName)", conn);
                    cmd.Parameters.AddWithValue("@studid", txtStudId.Text);
                    cmd.Parameters.AddWithValue("@ThesisA", txtThesisTitleOriginal.Text);
                    cmd.Parameters.AddWithValue("@ThesisE", txtThesisTitleTranslate.Text);
                    cmd.Parameters.AddWithValue("@date", DateTime.Now.Date);
                    cmd.Parameters.AddWithValue("@userid", Session["userid"]);
                    cmd.Parameters.AddWithValue("@SupName", Session["userName"]);
                    SGNoObjId = cmd.ExecuteScalar().ToString();
                    Session["SGNoObjId"] = SGNoObjId;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('تم ارسال الطلب','info');", true);
                }
                else
                {
                    cmd = new SqlCommand("Update GS_NoObjectionThesis set status=0 where AutoId=" + Session["SGNoObjId"], conn);
                    cmd.ExecuteNonQuery();

                    SqlCommand cmdUpdate = new SqlCommand("Update RequestsFollowUp Set ReqStatus=N'انجزت',Decision=1 where AutoId=(select max(autoid) from RequestsFollowUp where RequestId=" + Session["RequestedReport"] + " and type=8)", conn);
                    cmdUpdate.ExecuteNonQuery();

                    SGNoObjId = Session["SGNoObjId"].ToString();
                    Session["SGNoObjId"] = SGNoObjId;

                }

                cmd = new SqlCommand("insert into RequestsFollowUp values(@RequestId,@ReqFromId,@ReqFromName,@ReqToId,@RquToName,@RequestDate,N'قيد الانتظار',@ActualId,@Notes,0,8)", conn);
                cmd.Parameters.AddWithValue("@RequestId", SGNoObjId);
                cmd.Parameters.AddWithValue("@ReqFromId", Session["userid"]);
                cmd.Parameters.AddWithValue("@ReqFromName", lblStudName.Text);
                cmd.Parameters.AddWithValue("@ReqToId", dtPriv.Rows[0][4]);
                cmd.Parameters.AddWithValue("@RquToName", dtPriv.Rows[0][13] + " - " + (dtPriv.Rows[0][8].ToString() != "0" ? dtPriv.Rows[0][9] : dtPriv.Rows[0][7]));
                cmd.Parameters.AddWithValue("@RequestDate", DateTime.Now.Date);
                cmd.Parameters.AddWithValue("@ActualId", Session["userid"]);
                cmd.Parameters.AddWithValue("@Notes", txtDirNote.Text);
                cmd.ExecuteNonQuery();

                /* end */
                conn.Close();
                Timer1.Enabled = true;


                conn.Close();
            }

        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Response.Redirect("HomePage.aspx");
        }

        protected void btnDirAgree_Click(object sender, EventArgs e)
        {
            if (Session["RequestedReport"] != null)
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                string sql = @"SELECT *
                    FROM WorkFlow a, Com_Priviliges b,Com_JobTitle c
                    where a.PrivId = b.PrivId and a.PrivId = c.AutoId and FormId = 8 and PrivNo=(select ReqToId 
            											 From RequestsFollowUp 
            											 where RequestId=" + Session["RequestedReport"] + @" and type=8
            											 and AutoId=(select Max(autoid)
            														 From RequestsFollowUp
            														 where RequestId=" + Session["RequestedReport"] + @" and Type=8))";

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
                    where a.PrivId = b.PrivId and a.PrivId = c.AutoId and FormId = 8 and stepNo=" + (Convert.ToInt16(dtPriv.Rows[0][2]) + 1);
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

                    SqlCommand cmdSup = new SqlCommand("Update GS_NoObjectionThesis set status=0 where AutoId=" + Session["RequestedReport"], conn);
                    cmdSup.ExecuteNonQuery();



                    //cmdSup = new SqlCommand("Update GS_StudThesisSup set status=1 where SGThId=" + Session["RequestedReport"] + " and JobId=" + Session["SupJobId"], conn);
                    //cmdSup.ExecuteNonQuery();

                    //SqlCommand cmdUpdate1 = new SqlCommand("Update GS_ChangeTrack Set DecNo=@dn,DecMeeting=@dm,DecDate=@dd where AutoId=" + Session["RequestedReport"], conn);
                    //cmdUpdate1.Parameters.AddWithValue("@dn", txtDecNo.Text);
                    //cmdUpdate1.Parameters.AddWithValue("@dm", txtDecMeetNo.Text);
                    //cmdUpdate1.Parameters.AddWithValue("@dd", txtDecMeetDate.Text);
                    //cmdUpdate1.ExecuteNonQuery();
                }

                string x = Session["UpdatedRecord"].ToString();
                
                if (drRow.Length != 0)
                {
                    /* insert followup information*/
                    SqlCommand cmdUpdate = new SqlCommand("Update RequestsFollowUp Set ReqStatus=N'انجزت',Decision=1 where AutoId=(select max(autoid) from RequestsFollowUp where RequestId=" + Session["RequestedReport"] + " and type=8)", conn);
                    cmdUpdate.ExecuteNonQuery();
                    SqlCommand cmd = new SqlCommand("insert into RequestsFollowUp values(@RequestId,@ReqFromId,@ReqFromName,@ReqToId,@RquToName,@RequestDate,N'قيد الانتظار',@ActualId,@Notes,0,8)", conn);
                    cmd.Parameters.AddWithValue("@RequestId", Session["RequestedReport"]);
                    cmd.Parameters.AddWithValue("@ReqFromId", drRowFrom[0][4]);
                    cmd.Parameters.AddWithValue("@ReqFromName", drRowFrom[0][13] + " - " + (drRowFrom[0][8].ToString() != "0" ? drRowFrom[0][9] : drRowFrom[0][7]));
                    cmd.Parameters.AddWithValue("@ReqToId", drRow[0][4]);
                    cmd.Parameters.AddWithValue("@RquToName", drRow[0][13] + " - " + (drRow[0][8].ToString() != "0" ? drRow[0][9] : drRow[0][7]));
                    cmd.Parameters.AddWithValue("@RequestDate", DateTime.Now.Date);
                    cmd.Parameters.AddWithValue("@ActualId", Session["userid"]);
                    cmd.Parameters.AddWithValue("@Notes", txtDirNote.Text);
                    cmd.ExecuteNonQuery();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('تم ارسال الطلب','info');", true);
                    Timer2.Enabled = true;
                }
                else
                {
                    int InSup = 0;
                    int OutSup = 0;
                    int ViewSup = 0;
                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        CheckBox chk = GridView1.Rows[i].FindControl("chkAdopted") as CheckBox;
                        if (chk.Checked)
                        {
                            if (GridView1.Rows[i].Cells[6].Text == "مناقش داخلي")
                                InSup++;
                            else if (GridView1.Rows[i].Cells[6].Text == "مناقش خارجي")
                                OutSup++;
                            else if (GridView1.Rows[i].Cells[6].Text == "مراقب للجلسة")
                                ViewSup++;
                            //cmd = new SqlCommand("Update GS_NoObjectionCommittee Set status=1 where reqId=" + Session["RequestedReport"] + " and SupId=" + GridView1.Rows[i].Cells[0].Text, conn);
                            //cmd.ExecuteNonQuery();
                        }
                    }
                    if (InSup == 2 && OutSup == 1 && ViewSup == 1)
                    {
                        SqlCommand cmdUpdate = new SqlCommand("Update RequestsFollowUp Set ReqStatus=N'انجزت',Decision=1 where AutoId=(select max(autoid) from RequestsFollowUp where RequestId=" + Session["RequestedReport"] + " and type=8)", conn);
                        cmdUpdate.ExecuteNonQuery();
                        SqlCommand cmd = new SqlCommand("insert into RequestsFollowUp values(@RequestId,@ReqFromId,@ReqFromName,@ReqToId,@RquToName,@RequestDate,N'مكتمل',@ActualId,@Notes,0,8)", conn);
                        cmd.Parameters.AddWithValue("@RequestId", Session["RequestedReport"]);
                        cmd.Parameters.AddWithValue("@ReqFromId", drRowFrom[0][4]);
                        cmd.Parameters.AddWithValue("@ReqFromName", drRowFrom[0][13] + " - " + (drRowFrom[0][8].ToString() != "0" ? drRowFrom[0][9] : drRowFrom[0][7]));
                        cmd.Parameters.AddWithValue("@ReqToId", Session["OriginalSenderId"]);
                        cmd.Parameters.AddWithValue("@RquToName", Session["OriginalSenderName"]);
                        cmd.Parameters.AddWithValue("@RequestDate", DateTime.Now.Date);
                        cmd.Parameters.AddWithValue("@ActualId", Session["userid"]);
                        cmd.Parameters.AddWithValue("@Notes", txtDirNote.Text);
                        cmd.ExecuteNonQuery();

                        SqlCommand cmdUpdate1 = new SqlCommand("Update GS_NoObjectionThesis Set Status=1 where AutoId=" + Session["RequestedReport"], conn);
                        cmdUpdate1.ExecuteNonQuery();
                        Session["RequestedReport"] = null;
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('تم ارسال الطلب','info');", true);
                        Timer2.Enabled = true;
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('يجب ان يتم اختيار مناقش داخلي عدد 2، مناقش خارجي عدد 2 ومراقب للجلسة','error');", true);
                    }
                }
                conn.Close();
                
                //}
            }

        }

        protected void btnDisAgree_Click(object sender, EventArgs e)
        {
            if (Session["RequestedReport"] != null)
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                string sql = @"SELECT *
                    FROM WorkFlow a, Com_Priviliges b,Com_JobTitle c
                    where a.PrivId = b.PrivId and a.PrivId = c.AutoId and FormId = 8 and PrivNo=(select ReqToId 
            											 From RequestsFollowUp 
            											 where RequestId=" + Session["RequestedReport"] + @" and type=8
            											 and AutoId=(select Max(autoid)
            														 From RequestsFollowUp
            														 where RequestId=" + Session["RequestedReport"] + @" and Type=8))";

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
                    where a.PrivId = b.PrivId and a.PrivId = c.AutoId and FormId = 8 and stepNo=" + (Convert.ToInt16(dtPriv.Rows[0][2]) - 1);
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
                SqlCommand cmdUpdate = new SqlCommand("Update RequestsFollowUp Set ReqStatus=N'انجزت',Decision=2 where AutoId=(select max(autoid) from RequestsFollowUp where RequestId=" + Session["RequestedReport"] + " and type=8)", conn);
                cmdUpdate.ExecuteNonQuery();


                //if(drPrev.HasRows)
                if (drRow.Length != 0)
                {
                    cmdUpdate = new SqlCommand("Select * from RequestsFollowUp where AutoId=(select max(autoid) from RequestsFollowUp where RequestId=" + Session["RequestedReport"] + " and type=8)", conn);
                    SqlDataReader drPrev = cmdUpdate.ExecuteReader();
                    /* insert followup information*/
                    drPrev.Read();
                    SqlCommand cmd = new SqlCommand("insert into RequestsFollowUp values(@RequestId,@ReqFromId,@ReqFromName,@ReqToId,@RquToName,@RequestDate,N'قيد الانتظار',@ActualId,@Notes,0,8)", conn);
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
                    SqlCommand cmd = new SqlCommand("insert into RequestsFollowUp values(@RequestId,@ReqFromId,@ReqFromName,@ReqToId,@RquToName,@RequestDate,N'قيد الانتظار',@ActualId,@Notes,0,8)", conn);
                    cmd.Parameters.AddWithValue("@RequestId", Session["RequestedReport"]);
                    cmd.Parameters.AddWithValue("@ReqFromId", drRowFrom[0][4]);
                    cmd.Parameters.AddWithValue("@ReqFromName", drRowFrom[0][13] + " - " + (drRowFrom[0][8].ToString() != "0" ? drRowFrom[0][9] : drRowFrom[0][7]));
                    cmd.Parameters.AddWithValue("@ReqToId", Session["OriginalSenderId"]);
                    cmd.Parameters.AddWithValue("@RquToName", Session["OriginalSenderName"]);
                    cmd.Parameters.AddWithValue("@RequestDate", DateTime.Now.Date);
                    cmd.Parameters.AddWithValue("@ActualId", Session["userid"]);
                    cmd.Parameters.AddWithValue("@Notes", txtDirNote.Text);
                    cmd.ExecuteNonQuery();

                    SqlCommand cmdUpdate1 = new SqlCommand("Update GS_NoObjectionThesis Set Status=2 where AutoId=" + Session["RequestedReport"], conn);
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

        protected void Timer2_Tick(object sender, EventArgs e)
        {
            Response.Redirect("Admin_RequestsFollowUp.aspx");
        }

        protected void btnRegSend_Click(object sender, EventArgs e)
        {
            if (Session["RequestedReport"] != null)
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                string sql = sql = @"SELECT *
                    FROM WorkFlow a, Com_Priviliges b,Com_JobTitle c
                    where a.PrivId = b.PrivId and a.PrivId = c.AutoId and FormId = 8 and PrivNo=(select ReqToId 
            											 From RequestsFollowUp 
            											 where RequestId=" + Session["RequestedReport"] + @" and type=8
            											 and AutoId=(select Max(autoid)
            														 From RequestsFollowUp
            														 where RequestId=" + Session["RequestedReport"] + @" and Type=8))";

                SqlCommand cmdGetSentInfo = new SqlCommand(sql, conn);
                DataTable dtPriv = new DataTable();
                dtPriv.Load(cmdGetSentInfo.ExecuteReader());

                sql = @"SELECT *
                    FROM WorkFlow a, Com_Priviliges b,Com_JobTitle c
                    where a.PrivId = b.PrivId and a.PrivId = c.AutoId and FormId = 8 and stepNo=2";
                cmdGetSentInfo = new SqlCommand(sql, conn);
                DataTable dtPrivTo = new DataTable();
                dtPrivTo.Load(cmdGetSentInfo.ExecuteReader());

                SqlCommand cmdUpdate = new SqlCommand("Update RequestsFollowUp Set ReqStatus=N'انجزت',Decision=1 where AutoId=(select max(autoid) from RequestsFollowUp where RequestId=" + Session["RequestedReport"] + " and type=8)", conn);
                cmdUpdate.ExecuteNonQuery();

                SqlCommand cmd = new SqlCommand("insert into RequestsFollowUp values(@RequestId,@ReqFromId,@ReqFromName,@ReqToId,@RquToName,@RequestDate,N'قيد الانتظار',@ActualId,@Notes,0,8)", conn);
                cmd.Parameters.AddWithValue("@RequestId", Session["RequestedReport"]);
                cmd.Parameters.AddWithValue("@ReqFromId", dtPriv.Rows[0][4]);
                cmd.Parameters.AddWithValue("@ReqFromName", dtPriv.Rows[0][13] + " - " + (dtPriv.Rows[0][8].ToString() != "0" ? dtPriv.Rows[0][9] : dtPriv.Rows[0][7]));
                cmd.Parameters.AddWithValue("@ReqToId", dtPrivTo.Rows[0][4]);
                cmd.Parameters.AddWithValue("@RquToName", dtPrivTo.Rows[0][13] + " - " + (dtPrivTo.Rows[0][8].ToString() != "0" ? dtPrivTo.Rows[0][9] : dtPrivTo.Rows[0][7]));
                cmd.Parameters.AddWithValue("@RequestDate", DateTime.Now.Date);
                cmd.Parameters.AddWithValue("@ActualId", Session["userid"]);
                cmd.Parameters.AddWithValue("@Notes", txtDirNote.Text);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("Insert into GS_NoObjectionRegistration values" +
                    "(@reqid,@CourseFinish,@YearFinish,@SemPostPond,@SemDiscontinue,@SupAssignDate,@SemExtend,@SemInUni,@CurrentSemRegStatus,@Notes,@DiscussionStatus,@DiscussionNotes,'','','','','')", conn);
                cmd.Parameters.AddWithValue("@reqid", Session["RequestedReport"]);
                cmd.Parameters.AddWithValue("@CourseFinish", ddlFinishCourseSem.SelectedValue);
                cmd.Parameters.AddWithValue("@YearFinish", txtFinishCourseYesr.Text);
                cmd.Parameters.AddWithValue("@SemPostPond", txtPostpondSemCount.Text);
                cmd.Parameters.AddWithValue("@SemDiscontinue", txtDisconnectSemCount.Text);
                cmd.Parameters.AddWithValue("@SupAssignDate", txtAssginSupDate.Text);
                cmd.Parameters.AddWithValue("@SemExtend", txtExtendSemYear.Text);
                cmd.Parameters.AddWithValue("@SemInUni", txtAllSems.Text);
                cmd.Parameters.AddWithValue("@CurrentSemRegStatus", chkRegCurSem.SelectedValue);
                cmd.Parameters.AddWithValue("@Notes", txtAdminRegNotes.Text);
                cmd.Parameters.AddWithValue("@DiscussionStatus", chkStudDiscussionStatus.SelectedValue);
                cmd.Parameters.AddWithValue("@DiscussionNotes", txtNotes.Text);
                cmd.ExecuteNonQuery();

                Session["RequestedReport"] = null;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('تم ارسال الطلب','info');", true);
                conn.Close();
                /* end */
                Timer4.Enabled = true;
            }
        }

        protected void Timer4_Tick(object sender, EventArgs e)
        {
            Response.Redirect("Admin_requestsFollowUp.aspx");
        }

        protected void btnFinSend_Click(object sender, EventArgs e)
        {
            if (Session["RequestedReport"] != null)
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                string sql = sql = @"SELECT *
                    FROM WorkFlow a, Com_Priviliges b,Com_JobTitle c
                    where a.PrivId = b.PrivId and a.PrivId = c.AutoId and FormId = 8 and PrivNo=(select ReqToId 
            											 From RequestsFollowUp 
            											 where RequestId=" + Session["RequestedReport"] + @" and type=8
            											 and AutoId=(select Max(autoid)
            														 From RequestsFollowUp
            														 where RequestId=" + Session["RequestedReport"] + @" and Type=8))";

                SqlCommand cmdGetSentInfo = new SqlCommand(sql, conn);
                DataTable dtPriv = new DataTable();
                dtPriv.Load(cmdGetSentInfo.ExecuteReader());

                sql = @"SELECT *
                    FROM WorkFlow a, Com_Priviliges b,Com_JobTitle c
                    where a.PrivId = b.PrivId and a.PrivId = c.AutoId and FormId = 8 and stepNo=3";
                cmdGetSentInfo = new SqlCommand(sql, conn);
                DataTable dtPrivTo = new DataTable();
                dtPrivTo.Load(cmdGetSentInfo.ExecuteReader());
                if (dtPrivTo.Rows.Count == 0)
                {
                    sql = @"select * From meu_new.all_students_karz where student_id=" + txtStudId.Text;
                    OracleCommand cmdOrc = new OracleCommand(sql, conn1);
                    OracleDataAdapter da = new OracleDataAdapter(cmdOrc);
                    DataTable dtTest = new DataTable();
                    da.Fill(dtTest);
                    if (dtTest.Rows.Count != 0 && dtTest.Rows[0]["SINGLE_SUPERVISOR"].ToString() != "")
                    {
                        string s = dtTest.Rows[0]["SINGLE_SUPERVISOR"].ToString();
                        string sup_id = dtTest.Rows[0]["SINGLE_SUPERVISOR_ID"].ToString();
                        SqlCommand cmdUpdate = new SqlCommand("Update RequestsFollowUp Set ReqStatus=N'انجزت',Decision=1 where AutoId=(select max(autoid) from RequestsFollowUp where RequestId=" + Session["RequestedReport"] + " and type=8)", conn);
                        cmdUpdate.ExecuteNonQuery();

                        SqlCommand cmd = new SqlCommand("insert into RequestsFollowUp values(@RequestId,@ReqFromId,@ReqFromName,@ReqToId,@RquToName,@RequestDate,N'قيد الانتظار',@ActualId,@Notes,0,8)", conn);
                        cmd.Parameters.AddWithValue("@RequestId", Session["RequestedReport"]);
                        cmd.Parameters.AddWithValue("@ReqFromId", dtPriv.Rows[0][4]);
                        cmd.Parameters.AddWithValue("@ReqFromName", dtPriv.Rows[0][13] + " - " + (dtPriv.Rows[0][8].ToString() != "0" ? dtPriv.Rows[0][9] : dtPriv.Rows[0][7]));
                        cmd.Parameters.AddWithValue("@ReqToId", sup_id);// dtPrivTo.Rows[0][4]);
                        cmd.Parameters.AddWithValue("@RquToName", s);// dtPrivTo.Rows[0][13] + " - " + (dtPrivTo.Rows[0][8].ToString() != "0" ? dtPrivTo.Rows[0][9] : dtPrivTo.Rows[0][7]));
                        cmd.Parameters.AddWithValue("@RequestDate", DateTime.Now.Date);
                        cmd.Parameters.AddWithValue("@ActualId", Session["userid"]);
                        cmd.Parameters.AddWithValue("@Notes", txtDirNote.Text);
                        cmd.ExecuteNonQuery();

                        cmd = new SqlCommand("Update GS_NoObjectionRegistration Set " +
                            "FinStatus=@FinStatus, FinDate=@FinDate where ReqId=@ReqId", conn);
                        cmd.Parameters.AddWithValue("@reqid", Session["RequestedReport"]);
                        cmd.Parameters.AddWithValue("@FinStatus", chkStudFinanceStatus.SelectedValue);
                        cmd.Parameters.AddWithValue("@FinDate", DateTime.Now.Date);
                        cmd.ExecuteNonQuery();

                        Session["RequestedReport"] = null;
                        Timer5.Enabled = true;
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('تم ارسال الطلب','info');", true);
                        conn.Close();
                        /* end */
                        
                    }
                }
            }
        }

        protected void btnAddCommittee_Click(object sender, EventArgs e)
        {
            //dtSupervisor.Columns.Add("SupId");
            //dtSupervisor.Columns.Add("SupName");
            //dtSupervisor.Columns.Add("SupDesc");
            //dtSupervisor.Columns.Add("SupWorkPlace");
            //dtSupervisor.Columns.Add("SupMajor");
            //dtSupervisor.Columns.Add("SupDegree");
            if (ddlPlace.SelectedValue != "0"
                && ddlName.SelectedValue != "0"
                && ddlDesc.SelectedValue != "0"
                && txtWorkPalce.Text.Trim() != ""
                && txtMinor.Text.Trim() != ""
                && ddlDegree.SelectedValue != "0")
            {

                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd1 = new SqlCommand(@"Select *,
                                                      (case when SupDegree=1 then N'استاذ' when SupDegree=2 then N'استاذ مشارك' when SupDegree=3 then N'استاذ مساعد' end) SupDegreeT,
                                                      (case when InOut='IN' then N'مناقش داخلي' when InOut='OUT' then N'مناقش خارجي' when InOut='VIEW' then N'مراقب للجلسة' end) placeT
                                                      From GS_NoObjectionCommittee where SupId=" + ddlName.SelectedValue + @" and ReqId=" + Session["RequestedReport"]+" and insertedby=N'"+Session["userName"]+"'", conn);
                if (!cmd1.ExecuteReader().HasRows)
                {
                    dtSupervisor = (DataTable)Session["dtSupervisor"];
                    SqlCommand cmd2 = new SqlCommand(@"Select *,
                                                      (case when SupDegree=1 then N'استاذ' when SupDegree=2 then N'استاذ مشارك' when SupDegree=3 then N'استاذ مساعد' end) SupDegreeT,
                                                      (case when InOut='IN' then N'مناقش داخلي' when InOut='OUT' then N'مناقش خارجي' when InOut='VIEW' then N'مراقب للجلسة' end) placeT 
                                                      From GS_NoObjectionCommittee where ReqId=" + Session["RequestedReport"] + " and insertedby=N'" + Session["userName"] + "'" + " order by InOut", conn);
                    dtSupervisor = new DataTable();
                    dtSupervisor.Load(cmd2.ExecuteReader());

                    DataRow[] InBoss = dtSupervisor.Select("SupDesc='رئيس' and InsertedBy='" + Session["userName"] + "'");
                    DataRow[] InRec = dtSupervisor.Select("InOut='IN' and InsertedBy='"+Session["userName"] +"'");
                    DataRow[] OutRec = dtSupervisor.Select("InOut='OUT' and InsertedBy='" + Session["userName"] + "'");
                    DataRow[] ViewRec = dtSupervisor.Select("InOut='VIEW' and InsertedBy='" + Session["userName"] + "'");
                    //cmd1 = new SqlCommand("Select isnull(Count(InOut),0) From GS_NoObjectionCommittee where InOut='IN' and ReqId=" + Session["RequestedReport"], conn);
                    //SqlDataReader dr = cmd1.ExecuteReader();
                    //dr.Read();
                    //if (Convert.ToInt16(dr[0].ToString()) < 3)
                    //{
                    bool insert = true;
                    string msg = "";
                    if (InBoss.Length == 1 && ddlDesc.SelectedItem.Text == "رئيس")
                    {
                        insert = false;
                        msg = "لا يمكن اضافة أكثر من رئيس";
                    }
                    else if (InRec.Length == 3 && ddlPlace.SelectedValue == "IN")
                    {
                        insert = false;
                        msg = "لا يمكن اضافة أكثر من 3 مناقشين داخليين";
                    }
                    else if (OutRec.Length == 3 && ddlPlace.SelectedValue == "OUT")
                    {
                        insert = false;
                        msg = "لا يمكن اضافة أكثر من 3 مناقشين خارجيين";
                    }
                    else if (ViewRec.Length == 1 && ddlPlace.SelectedValue == "VIEW")
                    {
                        insert = false;
                        msg = "لا يمكن اضافة أكثر من مراقب للجسلة";
                    }
                    if (insert)
                    {
                        SqlCommand cmd = new SqlCommand("Insert into GS_NoObjectionCommittee values" +
                                "(@ReqId,@SupId,@SupName,@SupDesc,@SupWorkPlace,@SupMajor,@SupDegree,@InOut,@InsertedBy,@InsertedDate,0,@InsertedByDesc)", conn);
                        cmd.Parameters.AddWithValue("@ReqId", Session["RequestedReport"]);
                        cmd.Parameters.AddWithValue("@SupId", ddlName.SelectedValue);
                        cmd.Parameters.AddWithValue("@SupName", ddlName.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@SupDesc", ddlDesc.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@SupWorkPlace", txtWorkPalce.Text);
                        cmd.Parameters.AddWithValue("@SupMajor", txtMinor.Text);
                        cmd.Parameters.AddWithValue("@SupDegree", ddlDegree.SelectedValue);
                        cmd.Parameters.AddWithValue("@InOut", ddlPlace.SelectedValue);
                        string desc = Session["userName"].ToString().Trim() == lblSupervisorName.Text.Trim() ? "المشرف" : Session["desc"].ToString();
                        cmd.Parameters.AddWithValue("@InsertedBy", Session["userName"]);
                        cmd.Parameters.AddWithValue("@InsertedDate", DateTime.Now.Date);
                        cmd.Parameters.AddWithValue("@InsertedByDesc", desc);
                        cmd.ExecuteNonQuery();

                        cmd = new SqlCommand(@"Select *,
                                                      (case when SupDegree=1 then N'استاذ' when SupDegree=2 then N'استاذ مشارك' when SupDegree=3 then N'استاذ مساعد' end) SupDegreeT,
                                                      (case when InOut='IN' then N'مناقش داخلي' when InOut='OUT' then N'مناقش خارجي' when InOut='VIEW' then N'مراقب للجلسة' end) placeT 
                                                      From GS_NoObjectionCommittee where ReqId=" + Session["RequestedReport"] + " order by InOut", conn);
                        dtSupervisor = new DataTable();
                        dtSupervisor.Load(cmd.ExecuteReader());
                        Session["dtSupervisor"] = dtSupervisor;
                        conn.Close();

                        GridView1.DataSource = dtSupervisor;
                        GridView1.DataBind();
                    }
                    else
                    {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('"+msg+"','error');", true);
                    }
                    //}

                }
                else
                {
                    SqlCommand cmd = new SqlCommand(@"Select *,
                                                      (case when SupDegree=1 then N'استاذ' when SupDegree=2 then N'استاذ مشارك' when SupDegree=3 then N'استاذ مساعد' end) SupDegreeT,
                                                      (case when InOut='IN' then N'مناقش داخلي' when InOut='OUT' then N'مناقش خارجي' when InOut='VIEW' then N'مراقب للجلسة' end) placeT 
                                                      From GS_NoObjectionCommittee where ReqId=" + Session["RequestedReport"] + " order by InOut", conn);
                    dtSupervisor = new DataTable();
                    dtSupervisor.Load(cmd.ExecuteReader());

                    GridView1.DataSource = dtSupervisor;
                    GridView1.DataBind();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('تم اضافة هذا المناقش مسبقا','error');", true);
                }
                //Response.Redirect(Request.Url.ToString(), false);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('يجب ادخال معلومات المناقش كاملة','error');", true);
            }
            ddlPlace.SelectedValue = "0";
            ddlName.SelectedValue = "0";
            ddlDesc.SelectedValue = "0";
            txtWorkPalce.Text = "";
            txtMinor.Text = "";
            ddlDegree.SelectedValue = "0";




        }

        protected void ddlPlace_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPlace.SelectedValue != "0")
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmdSup = new SqlCommand(@"SELECT distinct [jobId]
                                                      ,[Place]
	                                                  ,(case 
	                                                  when place='in' then
	                                                  (select R.RaName from ResearcherInfo r where jobId=AcdId)
	                                                  else
	                                                  (select c.RName From Com_OuterSupervisorInfo c where c.AutoId=jobId)
	                                                  end) SupName
                                                  FROM Adopted_Supervisor
                                                  where Status<>3 and place='" + (ddlPlace.SelectedValue=="VIEW"?"IN": ddlPlace.SelectedValue) + "'", conn);
                ddlName.DataSource = cmdSup.ExecuteReader();
                ddlName.DataTextField = "SupName";
                ddlName.DataValueField = "jobid";
                ddlName.DataBind();
                ddlName.Items.Insert(0, "تحديد المشرف");
                ddlName.Items[0].Value = "0";
                if (ddlPlace.SelectedValue == "IN") {
                    ddlDesc.SelectedValue = "";
                    ddlDesc.Enabled = true;
                }
                else if (ddlPlace.SelectedValue == "OUT")
                {
                    ddlDesc.SelectedValue = "3";
                    ddlDesc.Enabled = false;

                }
                else
                {
                    ddlDesc.SelectedValue = "4";
                    ddlDesc.Enabled = false;
                }
            }
            else
                ddlName.Items.Clear();
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;

            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand("Delete From GS_NoObjectionCommittee where ReqId=" + Session["RequestedReport"] + " and SupId=" + row.Cells[0].Text, conn);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand(@"Select *,
                                                      (case when SupDegree=1 then N'استاذ' when SupDegree=2 then N'استاذ مشارك' when SupDegree=3 then N'استاذ مساعد' end) SupDegreeT ,
                                                      (case when InOut='IN' then N'مناقش داخلي' when InOut='OUT' then N'مناقش خارجي' when InOut='VIEW' then N'مراقب للجلسة' end) placeT
                                                      From GS_NoObjectionCommittee where ReqId=" + Session["RequestedReport"]+" order by InOut", conn);
            dtSupervisor = new DataTable();
            dtSupervisor.Load(cmd.ExecuteReader());

            GridView1.DataSource = dtSupervisor;
            GridView1.DataBind();
            conn.Close();
        }

        protected void btnSendSupInfo_Click(object sender, EventArgs e)
        {
            dtSupervisor = (DataTable)Session["dtSupervisor"];
            DataRow[] InRec = dtSupervisor.Select("InOut='IN' and trim(InsertedBy)='" + Session["userName"].ToString().Trim() + "'");
            DataRow[] OutRec = dtSupervisor.Select("InOut='OUT' and trim(InsertedBy)='" + Session["userName"].ToString().Trim() + "'");
            DataRow[] ViewRec = dtSupervisor.Select("InOut='VIEW' and trim(InsertedBy)='" + Session["userName"].ToString().Trim() + "'");
            //cmd1 = new SqlCommand("Select isnull(Count(InOut),0) From GS_NoObjectionCommittee where InOut='IN' and ReqId=" + Session["RequestedReport"], conn);
            //SqlDataReader dr = cmd1.ExecuteReader();
            //dr.Read();
            //if (Convert.ToInt16(dr[0].ToString()) < 3)
            //{
            if (InRec.Length == 3 && OutRec.Length == 3 && ViewRec.Length == 1)
            {
                if (GridView1.Rows.Count !=0 && txtDiscussionDate.Text.Trim() != "" && txtPlaceTime.Text.Trim() != "")
                {
                    if (Session["RequestedReport"] != null)
                    {
                        if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                            conn.Open();

                        string sql = sql = @"SELECT *
                    FROM WorkFlow a, Com_Priviliges b,Com_JobTitle c
                    where a.PrivId = b.PrivId and a.PrivId = c.AutoId and FormId = 8 and PrivNo=(select ReqToId 
            											 From RequestsFollowUp 
            											 where RequestId=" + Session["RequestedReport"] + @" and type=8
            											 and AutoId=(select Max(autoid)
            														 From RequestsFollowUp
            														 where RequestId=" + Session["RequestedReport"] + @" and Type=8))";

                        SqlCommand cmdGetSentInfo = new SqlCommand(sql, conn);
                        DataTable dtPriv = new DataTable();
                        dtPriv.Load(cmdGetSentInfo.ExecuteReader());
                        //DataRow[] drRowFrom = dtPriv.Select("FacultyNo="+lblStudFacultyNo.Text);
                        //if (drRowFrom.Length > 1)
                        //{
                        //    if (dtPriv.Rows[0][8].ToString() != "0")
                        //    {
                        //        drRowFrom = dtPriv.Select("DeptId=" + dtPriv.Rows[0][8]);
                        //    }
                        //}

                        sql = @"SELECT *
                    FROM WorkFlow a, Com_Priviliges b,Com_JobTitle c
                    where a.PrivId = b.PrivId and a.PrivId = c.AutoId and FormId = 8 and stepNo=4 ";
                        cmdGetSentInfo = new SqlCommand(sql, conn);
                        DataTable dtPrivTo = new DataTable();
                        dtPrivTo.Load(cmdGetSentInfo.ExecuteReader());
                        DataRow[] drRow;
                        if (dtPrivTo.Rows.Count > 1)
                        {
                            drRow = dtPrivTo.Select("FacultyNo=" + lblStudFacultyNo.Text + " and DeptId=" + lblStudDeptNo.Text);
                            //if (drRow[0][8].ToString() != "0")
                            //{
                            //    drRow = dtPrivTo.Select("DeptId=" + lblStudDeptNo.Text);// dtPrivTo.Rows[0][8]);
                            //}
                        }
                        else
                        {
                            drRow = dtPrivTo.Select();
                        }
                        //if (dtPrivTo.Rows.Count == 0)
                        //{
                        sql = @"select * From meu_new.all_students_karz where student_id=" + txtStudId.Text;
                        OracleCommand cmdOrc = new OracleCommand(sql, conn1);
                        OracleDataAdapter da = new OracleDataAdapter(cmdOrc);
                        DataTable dtTest = new DataTable();
                        da.Fill(dtTest);
                        //if (dtTest.Rows.Count != 0 && dtTest.Rows[0]["SINGLE_SUPERVISOR"].ToString() != "")
                        //{
                        string s = dtTest.Rows[0]["SINGLE_SUPERVISOR"].ToString();
                        string sup_id = dtTest.Rows[0]["SINGLE_SUPERVISOR_ID"].ToString();
                        SqlCommand cmdUpdate = new SqlCommand("Update RequestsFollowUp Set ReqStatus=N'انجزت',Decision=1 where AutoId=(select max(autoid) from RequestsFollowUp where RequestId=" + Session["RequestedReport"] + " and type=8)", conn);
                        cmdUpdate.ExecuteNonQuery();

                        SqlCommand cmd = new SqlCommand("insert into RequestsFollowUp values(@RequestId,@ReqFromId,@ReqFromName,@ReqToId,@RquToName,@RequestDate,N'قيد الانتظار',@ActualId,@Notes,0,8)", conn);
                        cmd.Parameters.AddWithValue("@RequestId", Session["RequestedReport"]);
                        cmd.Parameters.AddWithValue("@ReqFromId", sup_id);
                        cmd.Parameters.AddWithValue("@ReqFromName", s);// dtPriv.Rows[0][13] + " - " + (dtPriv.Rows[0][8].ToString() != "0" ? dtPriv.Rows[0][9] : dtPriv.Rows[0][7]));
                        cmd.Parameters.AddWithValue("@ReqToId", drRow[0][4]);
                        cmd.Parameters.AddWithValue("@RquToName", drRow[0][13] + " - " + (drRow[0][8].ToString() != "0" ? drRow[0][9] : drRow[0][7]));
                        cmd.Parameters.AddWithValue("@RequestDate", DateTime.Now.Date);
                        cmd.Parameters.AddWithValue("@ActualId", Session["userid"]);
                        cmd.Parameters.AddWithValue("@Notes", txtDirNote.Text);
                        cmd.ExecuteNonQuery();

                        cmd = new SqlCommand("Update GS_NoObjectionRegistration Set " +
                            "DiscussionDate=@DiscussionDate,DiscussionDay=@DiscussionDay, DiscussionPlace=@DiscussionPlace where ReqId=@ReqId", conn);
                        cmd.Parameters.AddWithValue("@reqid", Session["RequestedReport"]);
                        cmd.Parameters.AddWithValue("@DiscussionDate", txtDiscussionDate.Text.Trim());
                        cmd.Parameters.AddWithValue("@DiscussionDay", txtDiscussionDay.Text.Trim());
                        cmd.Parameters.AddWithValue("@DiscussionPlace", txtPlaceTime.Text.Trim());
                        cmd.ExecuteNonQuery();

                        Session["RequestedReport"] = null;
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('تم ارسال الطلب','info');", true);
                        conn.Close();
                        /* end */
                        Timer6.Enabled = true;
                    }
                }
                else
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('يجب ادخال المعلومات كاملة','error');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('يجب ادخال معلومات المناقشين كاملة','error');", true);
            }

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string f = HttpUtility.HtmlDecode(e.Row.Cells[7].Text.Trim().Split('-')[0]);
                string c = Session["userName"].ToString().Trim();
                if(f!=c)
                {
                    LinkButton lnkDelete = e.Row.FindControl("lnkDelete") as LinkButton;
                    lnkDelete.Visible = false;
                }
                if(Session["GS_Adopted"]!=null && Convert.ToBoolean(Session["GS_Adopted"]))
                {
                    CheckBox chk = e.Row.FindControl("chkAdopted") as CheckBox;
                    chk.Visible = true;
                }
            }
        }

        protected void Timer5_Tick(object sender, EventArgs e)
        {
            Response.Redirect("Admin_requestsFollowUp.aspx");
        }

        protected void ddlName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlName.SelectedValue != "0")
            {
                
                SqlCommand cmd;
                if (ddlPlace.SelectedValue != "OUT")
                {
                    try
                    {
                        if (connCV.State == ConnectionState.Broken || connCV.State == ConnectionState.Closed)
                            connCV.Open();
                        cmd = new SqlCommand("Select CollegeName,Degree,Minor From InstInfo a,College b where a.College=b.AutoId and InstJobId=" + ddlName.SelectedValue, connCV);
                        SqlDataReader dr = cmd.ExecuteReader();
                        dr.Read();
                        txtWorkPalce.Text = dr[0].ToString();

                        //string s = dr[1].ToString().Trim();
                        //int IndexIs = ddlDegree.Items.IndexOf(((ListItem)ddlDegree.Items.FindByText(s)));
                        ddlDegree.SelectedValue = dr[1].ToString();// IndexIs.ToString();
                        txtMinor.Text = dr[2].ToString();
                        connCV.Close();
                    }
                    catch(Exception err) { }
                }
                else if (ddlPlace.SelectedValue == "OUT")
                {
                    if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                        conn.Open();
                    cmd = new SqlCommand("Select * From Com_OuterSupervisorInfo where AutoId=" + ddlName.SelectedValue, conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    dr.Read();
                    txtWorkPalce.Text = dr["RUni"].ToString();
                    ddlDegree.SelectedValue = dr["RDegree"].ToString();
                    txtMinor.Text = dr["RMajor"].ToString();
                    conn.Close();
                }
            }
        }

        protected void Timer6_Tick(object sender, EventArgs e)
        {
            Response.Redirect("Admin_requestsFollowUp.aspx");
        }
    }
}