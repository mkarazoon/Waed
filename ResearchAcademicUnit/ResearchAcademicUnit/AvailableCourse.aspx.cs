using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ResearchAcademicUnit
{
    public partial class AvailableCourse : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uid"] == null)
                Response.Redirect("Login.aspx");
            Session["backurl"] = "Default.aspx";

            if (Session["Linktype"] != null)
            {
                if (Session["Linktype"].ToString() == "myCourses")
                {
                    lnkMyCourses.Attributes.Add("class", "active");
                    lnkCurrent.Attributes.Add("class", "");
                    myCourses.Visible = true;
                    coursename.Visible = false;
                    getMyCourses();
                }
                if (Session["Linktype"].ToString() == "current")
                {
                    lnkMyCourses.Attributes.Add("class", "");
                    lnkCurrent.Attributes.Add("class", "active");
                    myCourses.Visible = false;
                    coursename.Visible = true;
                }
            }
            getCourseInfo();
        }

        protected void getCourseInfo()
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand("select Min(autoid),CourseField From CourseInfo group by CourseField order by Min(autoid)", conn);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());

            string sb = "<ul>";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sb += @"
                       <li class='col-xs-12 col-sm-6 col-md-4 subcat-col' style='height: 304px;'>
                           <a id='79' nohref>
                              <i class='material-icons'>book</i>
                                     <span>
                                       <strong>دورات في مجال " + dt.Rows[i][1] + @"</strong>
                                 </span>
                           </a>
                           <div class='subcat'>";
                cmd = new SqlCommand("select distinct CourseName,CourseStatus From CourseInfo where CourseField=N'" + dt.Rows[i][1] + "'", conn);
                DataTable dtCourse = new DataTable();
                dtCourse.Load(cmd.ExecuteReader());

                for (int j = 0; j < dtCourse.Rows.Count; j++)
                {

                    sb += "<a style='margin:15px' href='CourseDetails.aspx?id=" + dtCourse.Rows[j][0] + "'>"+(j+1)+" - " + dtCourse.Rows[j][0] +(Convert.ToInt16(dtCourse.Rows[j][1])==1?" |<b> التسجيل متاح حاليا</b>": " | التسجيل غير متاح حاليا") + "</a>";
                }
                sb += @"</div>
                       </li>";
            }
            sb += "</ul>";

            coursename.InnerHtml = sb.ToString();
            conn.Close();
        }

        protected void lnkCurrent_Click(object sender, EventArgs e)
        {
            Session["Linktype"] = "current";
            Response.Redirect("AvailableCourse.aspx");
        }

        protected void lnkMyCourses_Click(object sender, EventArgs e)
        {
            Session["Linktype"] = "myCourses";
            Response.Redirect("AvailableCourse.aspx");
        }

        protected void getMyCourses()
        {

            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand(@"select distinct CI.CourseId,CI.CourseName,JobId,RaName,Status,(CourseDate+' / '+CourseTime) Details,
                                            (case
                                             when DATENAME(weekday,CourseDate)='Saturday' then N'السبت' 
                                             when DATENAME(weekday,CourseDate)='Sunday' then N'الأحد' 
                                             when DATENAME(weekday,CourseDate)='Monday' then N'الاثنين' 
                                             when DATENAME(weekday,CourseDate)='Tuesday' then N'الثلاثاء' 
                                             when DATENAME(weekday,CourseDate)='Wednesday' then N'الاربعاء' 
                                             when DATENAME(weekday,CourseDate)='Thursday' then N'الخميس'
                                             end) day,
                                            (case
                                            when CI.CourseType=1 then N'ورشة تدريبية'
                                            when CI.CourseType=2 then N'دورة تدريبة'
                                            end) Type,
(case 
when CI.CourseLevel=1 then N'مبتدئ'
when CI.CourseLevel=2 then N'متوسط'
when CI.CourseLevel=3 then N'متقدم'
when CI.CourseLevel=4 then N'عام'
end) Level,CI.Place,Trainer,CourseHour,CourseNameE,EvalStatus
                                                FROM [InstructorCourses] IC, ResearcherInfo RI, CourseDates CD, CourseInfo CI
                                                where IC.JobId = RI.AcdId and IC.CourseId = CD.CourseId and IC.SlotId =CD.AutoId and CI.CourseId = ic.CourseId
                                                --and status=N'قيد التنفيذ'
                                                and IC.JobId=" + Session["uid"] + @"
                                                order by CI.CourseId", conn);
            GridView1.DataSource = cmd.ExecuteReader();
            GridView1.DataBind();


            conn.Close();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType==DataControlRowType.DataRow)
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("Select RaName From ResearcherInfo where Acdid in (" + e.Row.Cells[5].Text+")", conn);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                string newT = "";
                for(int i=0;i<dt.Rows.Count;i++)
                {
                    newT += dt.Rows[i][0].ToString() + ",";
                }
                if (e.Row.Cells[14].Text == "1")
                {
                    DateTime d = Convert.ToDateTime(e.Row.Cells[4].Text.Substring(0, 10));
                    if (e.Row.Cells[8].Text == "تمت الموافقة" && Convert.ToDateTime(e.Row.Cells[4].Text.Substring(0, 10)) <= DateTime.Now.Date)
                        e.Row.Cells[9].Enabled = true;
                    else
                        e.Row.Cells[9].Enabled = false;
                }
                else
                {
                    e.Row.Cells[9].Enabled = false;
                    e.Row.Cells[9].Text = "انتهت فترة التقييم";
                }

                HtmlGenericControl div = (HtmlGenericControl)e.Row.Cells[6].FindControl("trainerDiv");
                div.InnerText = newT.Substring(0, newT.Length - 1);

                cmd = new SqlCommand("Select * from CourseEval where JobId="+Session["uid"] + " and CourseId="+ e.Row.Cells[10].Text, conn);
                DataTable dtEval = new DataTable();
                dtEval.Load(cmd.ExecuteReader());
                if(dtEval.Rows.Count!=0)
                {
                    LinkButton lnk = (LinkButton)e.Row.Cells[10].FindControl("lnkCert");
                    lnk.Visible = true;
                }
                //conn.Close();
            }
        }

        protected void lnkEvalCourse_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            Session["CID"] = row.Cells[10].Text;
            Session["TName"] = ((HtmlGenericControl)(row.Cells[6].FindControl("trainerDiv"))).InnerText;
            Session["CTitle"] = row.Cells[0].Text;
            Response.Redirect("CourseEval.aspx");
        }

        protected void lnkCert_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            Session["CourseName"] = row.Cells[0].Text;
            Session["CourseLevel"] = row.Cells[2].Text;
            Session["CourseHour"] = row.Cells[12].Text;
            Session["CourseDate"] = row.Cells[4].Text.Substring(0, 10);
            Session["CourseNameE"] = row.Cells[13].Text;
            Response.Redirect("CourseCertificate.aspx");
        }
    }
}