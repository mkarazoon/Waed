using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchAcademicUnit
{
    public partial class Com_JournalDataBase : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);
        static string constrOracleStu = "User Id=karazoon;Password=karazoon;data source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=10.0.101.121)(PORT=1521))(CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = meu)));";
        OracleConnection conn1 = new OracleConnection(constrOracleStu);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userid"] == null || Session.IsNewSession)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("Select * from Adopted_JournalList", conn);
                GridView1.DataSource = cmd.ExecuteReader();
                GridView1.DataBind();


                cmd = new SqlCommand("Select * from Com_JobTitle", conn);
                DataTable dtJob = new DataTable();
                dtJob.Load(cmd.ExecuteReader());
                GridView2.DataSource = dtJob;
                GridView2.DataBind();

                ddlJobTitle.DataSource = cmd.ExecuteReader();
                ddlJobTitle.DataTextField = "JobTitleA";
                ddlJobTitle.DataValueField = "AutoId";
                ddlJobTitle.DataBind();
                ddlJobTitle.Items.Insert(0, "اختيار");
                ddlJobTitle.Items[0].Value = "";

                cmd = new SqlCommand(@"SELECT * FROM Com_Priviliges a, Com_JobTitle b where a.PrivId=b.AutoId", conn);
                GridView3.DataSource = cmd.ExecuteReader();
                GridView3.DataBind();

                conn.Close();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand("Insert into Adopted_JournalList values(@Aname,1,@d)", conn);
            cmd.Parameters.AddWithValue("@Aname", txtDBName.Text);
            cmd.Parameters.AddWithValue("@d", DateTime.Now);
            cmd.ExecuteNonQuery();
                
                
            cmd = new SqlCommand("Select * from Adopted_JournalList", conn);
            GridView1.DataSource = cmd.ExecuteReader();
            GridView1.DataBind();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('تم الحفظ بنجاح','success');", true);
            conn.Close();
            txtDBName.Text = "";
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;

            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            int status = 1;
            string msg = "تم اعتماد قاعدة البيانات";
            if ((sender as Control).ID == "lnkDelete")
            {
                status = 0;
                msg = "تم إلغاء اعتماد قاعدة البيانات";
            }
            SqlCommand cmd = new SqlCommand("Update Adopted_JournalList Set Status=" + status + ",StatusDate=@d where Autoid=" + row.Cells[1].Text, conn);
            cmd.Parameters.AddWithValue("@d", DateTime.Now);
            cmd.ExecuteNonQuery();


            cmd = new SqlCommand("Select * from Adopted_JournalList", conn);
            GridView1.DataSource = cmd.ExecuteReader();
            GridView1.DataBind();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('" + msg + "','info');", true);
            conn.Close();
        }

        protected void lnkTab1_Click(object sender, EventArgs e)
        {
            tab1.Visible = true;
            tab2.Visible = false;
            tab3.Visible = false;
            lnkTab1.Attributes.Add("class", "nav-link active");
            lnkTab2.Attributes.Add("class", "nav-link");
            lnkTab3.Attributes.Add("class", "nav-link");
        }

        protected void lnkTab2_Click(object sender, EventArgs e)
        {
            tab1.Visible = false;
            tab2.Visible = true;
            tab3.Visible = false;
            lnkTab1.Attributes.Add("class", "nav-link");
            lnkTab2.Attributes.Add("class", "nav-link active");
            lnkTab3.Attributes.Add("class", "nav-link");
        }

        protected void lnkTab3_Click(object sender, EventArgs e)
        {
            tab1.Visible = false;
            tab2.Visible = false;
            tab3.Visible = true;
            lnkTab1.Attributes.Add("class", "nav-link");
            lnkTab2.Attributes.Add("class", "nav-link");
            lnkTab3.Attributes.Add("class", "nav-link active");
            DataTable dt = new DataTable();
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da = new OracleDataAdapter();
            string sql = "";

            //sql = "select distinct faculty_no,faculty_name from meu_new.faculties_depts_view where dean_id<>0 and faculty_no<>14 order by faculty_no";
            sql = "select * From Com_Faculty";
            //cmd = new OracleCommand(sql, conn1);
            //da = new OracleDataAdapter(cmd);
            //da.Fill(dt);


            dt.Clear();
            sql = @"select * From meu_new.HRS_JOB_VIEW a order by display_name";
            cmd = new OracleCommand(sql, conn1);
            da = new OracleDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            ddlPrivToName.DataSource = dt;
            ddlPrivToName.DataTextField = "display_name";
            ddlPrivToName.DataValueField = "Staff_id";
            ddlPrivToName.DataBind();
            ddlPrivToName.Items.Insert(0, "اختيار");
            ddlPrivToName.Items[0].Value = "";



            //sql = @"select ""رقم الطالب"",""اسم الطالب"",""الدرجة"",""اخر حالة للطالب"" From meu_new.all_students_karz";
            //cmd = new OracleCommand(sql, conn1);
            //da = new OracleDataAdapter(cmd);
            //dt = new DataTable();
            //da.Fill(dt);
            //GridView5.DataSource = dt;
            //GridView5.DataBind();

            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd1 = new SqlCommand("Select * from Com_JobTitle", conn);
            DataTable dtJob = new DataTable();
            dtJob.Load(cmd1.ExecuteReader());

            ddlJobTitle.DataSource = dtJob;
            ddlJobTitle.DataTextField = "JobTitleA";
            ddlJobTitle.DataValueField = "AutoId";
            ddlJobTitle.DataBind();
            ddlJobTitle.Items.Insert(0, "اختيار");
            ddlJobTitle.Items[0].Value = "";

            cmd1 = new SqlCommand("Select * from Com_Faculty", conn);
            ddlMainDept.DataSource = cmd1.ExecuteReader();
            ddlMainDept.DataTextField = "FacNameA";
            ddlMainDept.DataValueField = "FacId";
            ddlMainDept.DataBind();
            ddlMainDept.Items.Insert(0, "اختيار الكلية");
            ddlMainDept.Items[0].Value = "";
            //ddlMainDept.Items.Insert(ddlMainDept.Items.Count, "عمادة الدراسات العليا");
            //ddlMainDept.Items[ddlMainDept.Items.Count - 1].Value = "100";
            //ddlMainDept.Items.Insert(ddlMainDept.Items.Count, "الرئاسة");
            //ddlMainDept.Items[ddlMainDept.Items.Count - 1].Value = "101";

            conn.Close();

        }

        protected void btnSaveJobTitle_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand("Insert into Com_JobTitle values(@Aname,@Ename)", conn);
            cmd.Parameters.AddWithValue("@Aname", txtAName.Text);
            cmd.Parameters.AddWithValue("@Ename", txtEName.Text);
            cmd.ExecuteNonQuery();

            cmd = new SqlCommand("Select * from Com_JobTitle", conn);
            GridView2.DataSource = cmd.ExecuteReader();
            GridView2.DataBind();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('تم الحفظ بنجاح','success');", true);
            conn.Close();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;

            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand cmd = new SqlCommand("Select * From Adopted_JournalList", conn);
            GridView1.DataSource = cmd.ExecuteReader();
            GridView1.DataBind();

            conn.Close();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand cmd = new SqlCommand("Select * From Adopted_JournalList", conn);
            GridView1.DataSource = cmd.ExecuteReader();
            GridView1.DataBind();

            conn.Close();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            string customerId = (row.Cells[1].Controls[0] as TextBox).Text;
            string amount = (row.Cells[2].Controls[0] as TextBox).Text;
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand cmd = new SqlCommand("Update Adopted_JournalList Set DBName=N'" + amount + "' where autoid=" + customerId ,conn);
            cmd.ExecuteNonQuery();
            GridView1.EditIndex = -1;
            cmd = new SqlCommand("Select * From Adopted_JournalList", conn);
            GridView1.DataSource = cmd.ExecuteReader();
            GridView1.DataBind();

            conn.Close();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType==DataControlRowType.DataRow)
            {
                LinkButton lnk1 = (LinkButton)e.Row.FindControl("lnkDelete");
                LinkButton lnk2 = (LinkButton)e.Row.FindControl("lnkAdopted");
                if (e.Row.Cells[3].Text=="0")
                {
                    lnk2.Visible = true;
                }
                else if (e.Row.Cells[3].Text == "1")
                {
                    lnk1.Visible = true;
                }
            }
        }

        protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView2.EditIndex = e.NewEditIndex;
            if(e.NewEditIndex==0 || e.NewEditIndex==1)
                GridView2.EditIndex = -1;
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand cmd = new SqlCommand("Select * From Com_JobTitle", conn);
            GridView2.DataSource = cmd.ExecuteReader();
            GridView2.DataBind();

            conn.Close();

        }

        protected void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView2.EditIndex = -1;
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand cmd = new SqlCommand("Select * From Com_JobTitle", conn);
            GridView2.DataSource = cmd.ExecuteReader();
            GridView2.DataBind();

            conn.Close();
        }

        protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView2.Rows[e.RowIndex];
            string id = (row.Cells[1].Controls[0] as TextBox).Text;
            string aname = (row.Cells[2].Controls[0] as TextBox).Text;
            string ename = (row.Cells[3].Controls[0] as TextBox).Text;
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand cmd = new SqlCommand("Update Com_JobTitle Set JobTitleA=@an,JobTitleE=@en where autoid=" + id, conn);
            cmd.Parameters.AddWithValue("@an", aname);
            cmd.Parameters.AddWithValue("@en", ename);
            cmd.ExecuteNonQuery();

            GridView2.EditIndex = -1;
            cmd = new SqlCommand("Select * From Com_JobTitle", conn);
            GridView2.DataSource = cmd.ExecuteReader();
            GridView2.DataBind();

            conn.Close();

        }

        protected void btnSavePriv_Click(object sender, EventArgs e)
        {
            if (ddlMainDept.SelectedValue != "" && ddlPrivToName.SelectedValue != "" && ddlJobTitle.SelectedValue != "")
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand cmd;

                cmd = new SqlCommand("Select * From Com_Priviliges where PrivId=" + ddlJobTitle.SelectedValue + " and FacultyNo=" + ddlMainDept.SelectedValue + (ddlSupDept.SelectedValue!=""? " and DeptId=" + ddlSupDept.SelectedValue:""), conn);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count == 0)
                {
                    if (ddlSupDept.SelectedValue == "")
                    {
                        cmd = new SqlCommand("insert into Com_Priviliges values(" + ddlJobTitle.SelectedValue + "," + ddlMainDept.SelectedValue + ",N'" + ddlMainDept.SelectedItem.Text + "',0,N''," + ddlPrivToName.SelectedValue + ",N'" + ddlPrivToName.SelectedItem.Text + "')", conn);
                    }
                    else
                    {
                        cmd = new SqlCommand("insert into Com_Priviliges values(" + ddlJobTitle.SelectedValue + "," + ddlMainDept.SelectedValue + ",N'" + ddlMainDept.SelectedItem.Text + "'," + ddlSupDept.SelectedValue + ",N'" + ddlSupDept.SelectedItem.Text + "'," + "," + ddlPrivToName.SelectedValue + ",N'" + ddlPrivToName.SelectedItem.Text + "')", conn);
                    }
                    cmd.ExecuteNonQuery();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('تم الحفظ بنجاح','success');", true);
                    ddlPrivToName.SelectedValue = "";
                    ddlJobTitle.SelectedValue = "";
                    ddlMainDept.SelectedValue = "";
                    ddlSupDept.SelectedValue = "";
                    cmd = new SqlCommand(@"SELECT * FROM Com_Priviliges a, Com_JobTitle b where a.PrivId=b.AutoId", conn);
                    GridView3.DataSource = cmd.ExecuteReader();
                    GridView3.DataBind();
                }
                else
                {
                    Session["PrivNo"] = dt.Rows[0][0];
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() { LoginFail(); });", true);

                }

                conn.Close();
            }
        }

        protected void ddlMainDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMainDept.SelectedValue != "")
            {
                if (conn1.State == ConnectionState.Broken || conn1.State == ConnectionState.Closed)
                    conn1.Open();

                DataTable dt = new DataTable();
                OracleCommand cmd = new OracleCommand();
                OracleDataAdapter da = new OracleDataAdapter();
                string sql = "";

                sql = "select distinct dept_no,dept_name from meu_new.faculties_depts_view where dean_id<>0 and faculty_no<>14 and faculty_no=" + ddlMainDept.SelectedValue + " order by faculty_no";
                cmd = new OracleCommand(sql, conn1);
                da = new OracleDataAdapter(cmd);
                da.Fill(dt);
                ddlSupDept.DataSource = dt;
                ddlSupDept.DataTextField = "dept_name";
                ddlSupDept.DataValueField = "dept_no";
                ddlSupDept.DataBind();
                ddlSupDept.Items.Insert(0, "اختيار القسم");
                ddlSupDept.Items[0].Value = "";

                conn1.Close();
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Response.Redirect("Com_JournalDataBase.aspx");
        }

        protected void btnGetData_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand cmdCheck = new SqlCommand("Select * From Com_Priviliges where PrivId=1 or PrivId=2", conn);
            DataTable dtCheck = new DataTable();
            dtCheck.Load(cmdCheck.ExecuteReader());
            //if(dtCheck.Rows.Count)

            DataTable dt = new DataTable();
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da = new OracleDataAdapter();
            string sql = "";

            sql = @"select distinct faculty_no,faculty_name,
                            dean_id,(select distinct instructor_name From meu_new.inst_timetable_view b where a.dean_id=b.instructor_id) as dean_name
                            --,dept_no,dept_name,
                            --dept_head,(select distinct instructor_name From meu_new.inst_timetable_view b where a.dept_head=b.instructor_id) as dir_name 
                            from meu_new.faculties_depts_view a 
                            where dean_id<>0 and faculty_no<>14 order by faculty_no";
            cmd = new OracleCommand(sql, conn1);
            da = new OracleDataAdapter(cmd);
            da.Fill(dt);
            string sql1 = "";
            for(int i=0;i<dt.Rows.Count;i++)
            {
                DataRow[] row = dtCheck.Select("PrivId=1 and FacultyNo=" + dt.Rows[i][0]);
                if (row.Length != 0)
                {
                    if (Convert.ToInt16(dt.Rows[i][2]) != Convert.ToInt16(row[0][6]))
                        sql1 += "update Com_Priviliges Set PrivTo=" + dt.Rows[i][2] + ",PrivToName=N'" + dt.Rows[i][3] + "' where PrivNo=" + row[0][0] + ";";
                }
                else
                    sql1 += "insert into Com_Priviliges values(1," + dt.Rows[i][0] + ",N'" + dt.Rows[i][1] + "',0,''," + dt.Rows[i][2] + ",N'" + dt.Rows[i][3] + "');";
                
            }

            sql = @"select distinct faculty_no,faculty_name,
                            dean_id,(select distinct instructor_name From meu_new.inst_timetable_view b where a.dean_id=b.instructor_id) as dean_name
                            ,dept_no,dept_name,
                            dept_head,(select distinct instructor_name From meu_new.inst_timetable_view b where a.dept_head=b.instructor_id) as dir_name 
                            from meu_new.faculties_depts_view a 
                            where dean_id<>0 and faculty_no<>14 order by faculty_no";
            cmd = new OracleCommand(sql, conn1);
            da = new OracleDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if(dt.Rows[i][6].ToString()!="")
                {
                    DataRow[] row = dtCheck.Select("PrivId=2 and FacultyNo=" + dt.Rows[i][0]+" and DeptId="+ dt.Rows[i][4]);
                    if (row.Length != 0)
                    {
                        if (Convert.ToInt16(dt.Rows[i][6]) != Convert.ToInt16(row[0][6]))
                            sql1 += "update Com_Priviliges Set PrivTo=" + dt.Rows[i][6] + ",PrivToName=N'" + dt.Rows[i][7] + "' where PrivNo=" + row[0][0] + ";";
                    }
                    else
                        sql1 += "insert into Com_Priviliges values(2," + dt.Rows[i][0] + ",N'" + dt.Rows[i][1] + "',"+ dt.Rows[i][4] + ",N'" + dt.Rows[i][5] + "',"+ dt.Rows[i][6] + ",N'" + dt.Rows[i][7] + "');";
                }
            }

            if (sql1 != "")
            {
                SqlCommand cmdInsert = new SqlCommand(sql1, conn);
                cmdInsert.ExecuteNonQuery();

                SqlCommand cmd1 = new SqlCommand(@"SELECT * FROM Com_Priviliges a, Com_JobTitle b where a.PrivId=b.AutoId", conn);
                GridView3.DataSource = cmd1.ExecuteReader();
                GridView3.DataBind();

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('تم تخزين المعلومات بنجاح','success');", true);
            }
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('لا يوجد تعديل على البيانات المدخلة','info');", true);


            

            conn.Close();
        }

        protected void GridView3_DataBound(object sender, EventArgs e)
        {
            for(int i=0;i<GridView3.Rows.Count;i++)
            {

            }
        }

        protected void btnUpdatePriv_Click(object sender, EventArgs e)
        {
            if (ddlMainDept.SelectedValue != "" && ddlPrivToName.SelectedValue != "" && ddlJobTitle.SelectedValue != "")
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand cmd;

                cmd = new SqlCommand("Update Com_Priviliges Set PrivTo=" + ddlPrivToName.SelectedValue + ",PrivToName=N'" + ddlPrivToName.SelectedItem.Text + "' where PrivNo=" + Session["PrivNo"], conn);
                cmd.ExecuteNonQuery();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('تم التعديل بنجاح','success');", true);
                ddlPrivToName.SelectedValue = "";
                ddlJobTitle.SelectedValue = "";
                ddlMainDept.SelectedValue = "";
                ddlSupDept.SelectedValue = "";
                Session["PrivNo"] = null;

                cmd = new SqlCommand(@"SELECT * FROM Com_Priviliges a, Com_JobTitle b where a.PrivId=b.AutoId", conn);
                GridView3.DataSource = cmd.ExecuteReader();
                GridView3.DataBind();

                conn.Close();
            }

        }

        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    Response.Clear();
        //    Response.Buffer = true;
        //    Response.ClearContent();
        //    Response.ClearHeaders();
        //    Response.Charset = "";
        //    string FileName = "SCOPUS_" + DateTime.Now + ".xls";
        //    StringWriter strwritter = new StringWriter();
        //    HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    Response.ContentType = "application/vnd.ms-excel";
        //    Response.ContentEncoding = System.Text.Encoding.Unicode;
        //    Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
        //    Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        //    GridView5.GridLines = GridLines.Both;
        //    GridView5.HeaderStyle.Font.Bold = true;
        //    GridView5.RenderControl(htmltextwrtter);
        //    Response.Write(strwritter.ToString());
        //}


        public override void VerifyRenderingInServerForm(Control control)
        {
        }

    }
}