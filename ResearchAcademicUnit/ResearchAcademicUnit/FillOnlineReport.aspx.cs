using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace HelpDeskIT
{
    public partial class FillOnlineReport : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["OnlineLectures"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);
        static string constrOracleStu = "User Id=karazoon;Password=karazoon;data source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=10.0.101.10)(PORT=1521))(CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = meudb)));";
        OracleConnection conn1 = new OracleConnection(constrOracleStu);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.IsNewSession || Session["userid"] == null)
                Response.Redirect("Login.aspx");

            if (!IsPostBack)
            {
                //if (txtInstId.Text != "")
                //{
                    if (conn1.State == ConnectionState.Closed || conn1.State == ConnectionState.Broken)
                        conn1.Open();
                txtInstId.Text = Session["userid"].ToString();
                txtInstId.ReadOnly = true;
                    DataTable dt = new DataTable();
                    string sql = "select distinct * from meu_new.inst_timetable_view where instructor_id=" + Session["userid"];
                    OracleCommand cmd = new OracleCommand(sql, conn1);
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    da.Fill(dt);
                    if (dt.Rows.Count != 0)
                    {
                        lblInstName.Text = dt.Rows[0]["INSTRUCTOR_NAME"].ToString();
                        lblFaculty.Text = dt.Rows[0]["faculty"].ToString();
                        lblDept.Text = dt.Rows[0]["dept"].ToString();
                        instNameDiv.Visible = true;
                        instFacultyDiv.Visible = true;
                        instDeptDiv.Visible = true;
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        conn1.Close();

                        if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
                            conn.Open();

                        SqlCommand cmd1 = new SqlCommand("Select * from OnlineReport where InstId=" + (txtInstId.Text != "" ? txtInstId.Text : "0") + " order by LectureDate desc", conn);
                        GridView2.DataSource = cmd1.ExecuteReader();
                        GridView2.DataBind();
                        mytabs.Visible = true;
                        tab1.Visible = true;

                        conn.Close();
                    }
                    else
                    {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('لا يوجد جدول دراسي حاليا','error');", true);
                    mytabs.Visible = false;
                        instNameDiv.Visible = false;
                        instFacultyDiv.Visible = false;
                        instDeptDiv.Visible = false;
                        tab1.Visible = false;
                        tab2.Visible = false;
                    }

                //}
                //else
                //{
                //    mytabs.Visible = false;
                //    instNameDiv.Visible = false;
                //    instFacultyDiv.Visible = false;
                //    instDeptDiv.Visible = false;
                //    tab1.Visible = false;
                //    tab2.Visible = false;
                //}

            }
        }

        protected void txtInstId_TextChanged(object sender, EventArgs e)
        {
            if (txtInstId.Text != "")
            {
                if (conn1.State == ConnectionState.Closed || conn1.State == ConnectionState.Broken)
                    conn1.Open();

                DataTable dt = new DataTable();
                string sql = "select distinct * from meu_new.inst_timetable_view where instructor_id=" + txtInstId.Text;
                OracleCommand cmd = new OracleCommand(sql, conn1);
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count != 0)
                {
                    lblInstName.Text = dt.Rows[0]["INSTRUCTOR_NAME"].ToString();
                    lblFaculty.Text = dt.Rows[0]["faculty"].ToString();
                    lblDept.Text = dt.Rows[0]["dept"].ToString();
                    instNameDiv.Visible = true;
                    instFacultyDiv.Visible = true;
                    instDeptDiv.Visible = true;
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    conn1.Close();

                    if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
                        conn.Open();

                    SqlCommand cmd1 = new SqlCommand("Select * from OnlineReport where InstId=" + (txtInstId.Text != "" ? txtInstId.Text : "0") + " order by LectureDate desc", conn);
                    GridView2.DataSource = cmd1.ExecuteReader();
                    GridView2.DataBind();
                    mytabs.Visible = true;
                    tab1.Visible = true;

                    conn.Close();
                }
                else
                {
                    mytabs.Visible = false;
                    instNameDiv.Visible = false;
                    instFacultyDiv.Visible = false;
                    instDeptDiv.Visible = false;
                    tab1.Visible = false;
                    tab2.Visible = false;
                }

            }
            else
            {
                mytabs.Visible = false;
                instNameDiv.Visible = false;
                instFacultyDiv.Visible = false;
                instDeptDiv.Visible = false;
                tab1.Visible = false;
                tab2.Visible = false;
            }
        }

        protected void lnkView_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < GridView1.Rows.Count; i++)
            //{
            //    CheckBox chkLec = GridView1.Rows[i].FindControl("chkLec") as CheckBox;
            //    chkLec.Checked = false;
            //}
            //GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            //CheckBox chkLecActual = row.FindControl("chkLec") as CheckBox;
            //Session["rowIndex"] = row.RowIndex;
            //chkLecActual.Checked = true;
            //detailsDiv.Visible = true;
        }

        protected void txtPresentStud_TextChanged(object sender, EventArgs e)
        {

            //if (Convert.ToInt16(txtPresentStud.Text) <= Convert.ToInt16(GridView1.Rows[Convert.ToInt16(Session["rowIndex"])].Cells[4].Text))
            //    lblAbsent.Text = (Convert.ToInt16(GridView1.Rows[Convert.ToInt16(Session["rowIndex"])].Cells[5].Text) - Convert.ToInt16(txtPresentStud.Text)).ToString();
            //else
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('عدد الطلاب الكلي أقل من عدد الحضور','error');", true);
            //    txtPresentStud.Text = "";
            //}
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent.Parent;
            Label lblCourseNo = row.FindControl("lblCourseNo") as Label;
            Label lblCourseName = row.FindControl("lblCourseName") as Label;
            Label lblSection = row.FindControl("lblSection") as Label;
            Label lblRegStud = row.FindControl("lblRegStud") as Label;
            RadioButtonList rdDaysNew = row.FindControl("rdDaysNew") as RadioButtonList;
            RadioButtonList rdtimesNew = row.FindControl("rdtimesNew") as RadioButtonList;
            //Label txtlecDate = row.FindControl("txtlecDate") as Label;
            HtmlInputGenericControl txtlecDate = (HtmlInputGenericControl)row.FindControl("txtlecDate");
            TextBox txtPresentStud = row.FindControl("txtPresentStud") as TextBox;
            CheckBoxList chkApps = row.FindControl("chkApps") as CheckBoxList;
            TextBox txtNotes = row.FindControl("txtNotes") as TextBox;
            //try
            //{
            //DateTime d= Convert.ToDateTime(txtlecDate.Value);
                if (rdDaysNew.SelectedIndex == -1)
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('يجب تحديد اليوم','error');", true);
                else if (rdtimesNew.SelectedIndex == -1)
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('يجب تحديد الوقت','error');", true);
                else if (txtlecDate.Value == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('يجب تحديد التاريخ/هناك خطأ في التاريخ','error');", true);
                    txtlecDate.Focus();
                }
                else if (txtPresentStud.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('يجب تحديد عدد الحضور','error');", true);
                    txtPresentStud.Focus();
                }
                else if (Convert.ToInt16(txtPresentStud.Text) > Convert.ToInt16(lblRegStud.Text))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('عدد الحضور أكبر من عدد الطلبة المسجلين','error');", true);
                    txtPresentStud.Text = "";
                }
                else
                {
                    string apps = "";
                    for (int i = 0; i < chkApps.Items.Count; i++)
                        if (chkApps.Items[i].Selected)
                            apps += chkApps.Items[i].Value + ",";
                    if (apps == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('يجب تحديد تطبيق واحد على الأقل','error');", true);
                    }
                    else
                    {
                        apps = apps.Substring(0, apps.Length - 1);
                        if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
                            conn.Open();

                    string rowIndex;

                        SqlCommand cmd = new SqlCommand("insert into OnlineReport output inserted.autoid values(@InstId,@InstName,@faculty,@dept," +
                            "@CourseId,@CourseName,@SectionId,@Reg_Student,@lecday,@LectureDate" +
                            ",@lecTime,@PresentCount,@ApplicationType,@InstNotes,@EntryDate)", conn);
                        cmd.Parameters.AddWithValue("@InstId", txtInstId.Text);
                        cmd.Parameters.AddWithValue("@InstName", lblInstName.Text.Trim());
                        cmd.Parameters.AddWithValue("@faculty", lblFaculty.Text.Trim());
                        cmd.Parameters.AddWithValue("@dept", lblDept.Text.Trim());
                        cmd.Parameters.AddWithValue("@CourseId", lblCourseNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@Coursename", lblCourseName.Text.Trim());
                        cmd.Parameters.AddWithValue("@SectionId", lblSection.Text.Trim());
                        cmd.Parameters.AddWithValue("@Reg_Student", lblRegStud.Text.Trim());
                        cmd.Parameters.AddWithValue("@lecday", rdDaysNew.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@LectureDate", Convert.ToDateTime(txtlecDate.Value));
                        cmd.Parameters.AddWithValue("@lecTime", rdtimesNew.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@PresentCount", txtPresentStud.Text);
                        cmd.Parameters.AddWithValue("@ApplicationType", apps);
                        cmd.Parameters.AddWithValue("@InstNotes", txtNotes.Text);
                        cmd.Parameters.AddWithValue("@EntryDate", DateTime.Now);
                    rowIndex = cmd.ExecuteScalar().ToString();
                    Session["rowIndex"] = rowIndex;
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('تم التخزين بنجاح','success');", true);



                    SqlCommand cmd1 = new SqlCommand("Select * from OnlineReport where InstId=" + (txtInstId.Text != "" ? txtInstId.Text : "0") + " order by LectureDate desc", conn);
                    GridView2.DataSource = cmd1.ExecuteReader();
                    GridView2.DataBind();

                    tab1.Visible = false;
                    tab2.Visible = true;
                    lnkTab2.Attributes.Add("class", "nav-link active");
                    lnkTab1.Attributes.Add("class", "nav-link");

                    
                    conn.Close();
                    }
                }

            //}
            //catch
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('حصل خطأ أثناء التخزين','error');", true);
            //}
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {

        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            for(int j=0;j<GridView1.Rows.Count;j++)
            {
                string str1= String.Join("", GridView1.Rows[j].Cells[0].Text.Split(',', '(', ')'));
       //         string str = new string((from c in GridView1.Rows[j].Cells[3].Text
       //                                  where char.IsWhiteSpace(c) || char.IsLetterOrDigit(c)
       //                                  select c
       //).ToArray());
                string[] days = str1.Split(' ').Distinct().ToArray();
                List<string> termsList = new List<string>();
                List<string> timeList = new List<string>();
                for (int i = 0; i < days.Length; i++)
                {
                    
                    if (days[i].Contains("س"))
                    {
                        termsList.Add("س");
                    }
                    else if (days[i].Contains("ح"))
                    {
                        termsList.Add("ح");
                    }
                    else if (days[i].Contains("ن"))
                    {
                        termsList.Add("ن");
                    }
                    else if (days[i].Contains("ث"))
                    {
                        termsList.Add("ث");
                    }
                    else if (days[i].Contains("ر"))
                    {
                        termsList.Add("ر");
                    }
                    else if (days[i].Contains("خ"))
                    {
                        termsList.Add("خ");
                    }
                    if (days[i].Contains("_"))
                        timeList.Add(days[i]);
                }
                //RadioButtonList rdtime = GridView1.Rows[j].FindControl("rdtimes") as RadioButtonList;
                //rdtime.DataSource = timeList;
                //rdtime.DataBind();
                RadioButtonList rdtimeNew = GridView1.Rows[j].FindControl("rdtimesNew") as RadioButtonList;
                rdtimeNew.DataSource = timeList;
                rdtimeNew.DataBind();


                //RadioButtonList rd = GridView1.Rows[j].FindControl("rdDays") as RadioButtonList;
                //for (int x = rd.Items.Count - 1; x >= 0; x--)
                //{
                //    bool found = false;
                //    for (int y = 0; y < termsList.Count; y++)
                //        if (termsList[y] == rd.Items[x].Value)
                //            found=true;
                //    if (!found)
                //        rd.Items.RemoveAt(x);

                //}
                RadioButtonList rdNew = GridView1.Rows[j].FindControl("rdDaysNew") as RadioButtonList;
                for (int x = rdNew.Items.Count - 1; x >= 0; x--)
                {
                    bool found = false;
                    for (int y = 0; y < termsList.Count; y++)
                        if (termsList[y] == rdNew.Items[x].Value)
                            found = true;
                    if (!found)
                        rdNew.Items.RemoveAt(x);

                }

            }
        }

        protected void lnkTab1_Click(object sender, EventArgs e)
        {
            tab1.Visible = true;
            tab2.Visible = false;
            lnkTab1.Attributes.Add("class", "nav-link active");
            lnkTab2.Attributes.Add("class", "nav-link");
            Session["rowIndex"] = null;
        }

        protected void lnkTab2_Click(object sender, EventArgs e)
        {
            tab2.Visible = true;
            tab1.Visible = false;
            lnkTab2.Attributes.Add("class", "nav-link active");
            lnkTab1.Attributes.Add("class", "nav-link");
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(Session["rowIndex"] !=null)
            if (e.Row.Cells[0].Text == Session["rowIndex"].ToString())
            {
                e.Row.BackColor = Color.Black;
                e.Row.ForeColor = Color.White;
            }
        }
    }
}