using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ResearchAcademicUnit
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        protected void Page_Load(object sender, EventArgs e)
        {
            string path = HttpContext.Current.Request.Url.AbsoluteUri;
            string s = Session["webformpathurl"].ToString();
            Session["backurl"] = Session["webformpathurl"];
            if (!IsPostBack)
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();
                string[] id = Request.QueryString["id"].Split('_');

                SqlCommand cmdCheckCourseType = new SqlCommand("Select Target From CourseDates where Autoid=" + id[0] +" and courseid="+id[1], conn);
                SqlDataReader dr = cmdCheckCourseType.ExecuteReader();
                if (dr.HasRows)
                {
                    string message = "";
                    dr.Read();
                    if (Convert.ToInt16(dr[0]) == 1)
                    {
                        SqlCommand cmdCheckCourseRegister = new SqlCommand("Select * From InstructorCourses where JobId="+ Session["uid"]+" and CourseId=" + id[1], conn);
                        SqlDataReader drCourse = cmdCheckCourseRegister.ExecuteReader();
                        if (!drCourse.HasRows)
                        {
                            SqlCommand cmd = new SqlCommand("insert into InstructorCourses values(" + id[1]+"," + id[0] + "," + Session["uid"]+",@RDate,N'قيد التنفيذ','')", conn);
                            cmd.Parameters.AddWithValue("@RDate", DateTime.Now.Date);
                            cmd.ExecuteNonQuery();
                            message = "تم التسجيل بالدورة بنجاح";
                        }
                        else
                        {
                            message = "تم التسجيل بالدورة من قبل";
                        }
                    }
                    else
                    {
                        message = "هذه الدورة خاصة بالطلاب";
                    }
                    
                    string url = Session["webformpathurl"].ToString();
                    string script = "window.onload = function(){ alert('";
                    script += message;
                    script += "');";
                    script += "window.location = '";
                    script += url;
                    script += "'; }";
                    ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                }
                conn.Close();
            }
        }
    }
}