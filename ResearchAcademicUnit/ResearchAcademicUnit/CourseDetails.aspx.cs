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
    public partial class CourseDetails : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        protected void Page_Load(object sender, EventArgs e)
        {
            string path = HttpContext.Current.Request.Url.AbsoluteUri;
            string id = Request.QueryString["id"];

            Session["backurl"] = "AvailableCourse.aspx";
            Session["webformpathurl"] = path;

            if (!IsPostBack)
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                string sql = @"SELECT    distinct   CourseDates.CourseId
,(case
when CourseDates.Target=1 then N'أعضاء الهيئة الأكاديمية'
when CourseDates.Target=2 then N'طلبة الماجستير'
when CourseDates.Target=3 then N'الطلبة'
end) Target
, 
CourseInfo.CourseField, 
                         CourseInfo.CourseName,
(case
when CourseInfo.CourseType=1 then N'ورشة تدريبية'
when CourseInfo.CourseType=2 then N'دورة تدريبة'
end) Type,
(case 
when CourseInfo.CourseLevel=1 then N'مبتدئ'
when CourseInfo.CourseLevel=2 then N'متوسط'
when CourseInfo.CourseLevel=3 then N'متقدم'
when CourseInfo.CourseLevel=4 then N'عام'
end) Level,
						  CourseInfo.Coursehour, CourseInfo.Place, CourseInfo.Outline,CourseInfo.CourseStatus
FROM            CourseDates INNER JOIN
                         CourseInfo ON CourseDates.CourseId = CourseInfo.CourseId 
where CourseInfo.CourseName=N'" + id+"'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                string s = "<div style ='padding:15px;background:#7f7f7f;color:#fff;margin-bottom:30px'> المجال : " + id + " </div>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    s += "<div id='div_c_'" + (i + 1) + " style='width:33%;float:right;padding:1%;font-size:20px;' class='div'>";

                    s += "<div style='padding:5px;background:#7f7f7f;color:#fff'>عنوان النشاط البحثي : " + dt.Rows[i]["courseName"] + "</div>";
                    s += "<div style='padding:5px'>نوع النشاط : " + dt.Rows[i]["Type"] + "</div>";
                    s += "<div style='padding:5px'>مستوى النشاط : " + dt.Rows[i]["Level"] + "</div>";
                    s += "<div style='padding:5px'>الفئة المستهدفة : " + dt.Rows[i]["Target"] + "</div>";
                    s += "<div style='padding:5px'>مدة الدورة : " + dt.Rows[i]["Coursehour"] + "</div>";
                    s += "<div style='padding:5px'>مكان الدورة : " + dt.Rows[i]["Place"] + "</div>";
                    cmd = new SqlCommand("select distinct Trainer From CourseDates where CourseId=" + dt.Rows[i]["CourseId"], conn);
                    DataTable dtInst = new DataTable();
                    dtInst.Load(cmd.ExecuteReader());

                    //for (int j = 0; j < dtInst.Rows.Count; j++)
                    //{
                    //    string[] inst = dtInst.Rows[j][0].ToString().Split(',');
                    //    for (int instId = 0; instId < inst.Length; instId++)
                    //    {
                    //        cmd = new SqlCommand("Select RaName From ResearcherInfo where AcdId=" + inst[instId], conn);
                    //        SqlDataReader dr = cmd.ExecuteReader();
                    //        dr.Read();
                    //        s += "<div style='padding:5px'>اسم المدرب : " + dr["RaName"] + "</div>";
                    //    }
                    //}

                    int target = 0;
                    switch(dt.Rows[i]["Target"])
                    {
                        case "أعضاء الهيئة الأكاديمية":target = 1;
                            break;
                        case "طلبة الماجستير":target = 2;
                            break;
                        case "الطلبة":target = 3;
                            break;
                    }
                    //s += "<div style='padding:5px'><a href='webform2.aspx?id="+ dt.Rows[i]["CourseId"] + "' style='color:blue;cursor:pointer'>أهداف الدورة التدريبية اضغط هنا</a></div>";
                    s += "<div style='padding:5px'><a href='" + dt.Rows[i]["Outline"] + "' style='color:blue;cursor:pointer' target='_blanck'>أهداف الدورة التدريبية اضغط هنا</a></div>";
                    cmd = new SqlCommand("select distinct Autoid,CourseId,CourseDate,CourseTime,Trainer From CourseDates where target="+target+" and CourseId=" + dt.Rows[i]["CourseId"], conn);
                    DataTable dtDates = new DataTable();
                    dtDates.Load(cmd.ExecuteReader());
                    s += "<div style='padding:5px'> الأوقات المتاحة : </div>";
                    for (int DateIndex = 0; DateIndex < dtDates.Rows.Count; DateIndex++)
                    {
                        string dName = "";
                        switch(Convert.ToDateTime(dtDates.Rows[DateIndex]["CourseDate"]).DayOfWeek)
                        {
                            case DayOfWeek.Saturday:dName = "السبت";
                                break;

                            case DayOfWeek.Sunday:
                                dName = "الأحد";
                                break;
                            case DayOfWeek.Monday:
                                dName = "الاثنين";
                                break;
                            case DayOfWeek.Tuesday:
                                dName = "الثلاثاء";
                                break;
                            case DayOfWeek.Wednesday:
                                dName = "الاربعاء";
                                break;
                            case DayOfWeek.Thursday:
                                dName = "الخميس";
                                break;
                        }

                        //for (int j = 0; j < dtInst.Rows.Count; j++)
                        //{
                            string[] inst = dtDates.Rows[DateIndex][4].ToString().Split(',');
                        string trainerStr = "";
                            for (int instId = 0; instId < inst.Length; instId++)
                            {
                                cmd = new SqlCommand("Select RaName From ResearcherInfo where AcdId=" + inst[instId], conn);
                                SqlDataReader dr = cmd.ExecuteReader();
                                dr.Read();
                            trainerStr += "<span style='padding:5px'>| " + dr["RaName"] + " </span>";
                            }
                        //}

                        s += "<div style='padding:5px'><label for='Radio_"+i+"_"+DateIndex+"'><input id='Radio_" + i + "_" + DateIndex + "' type='radio' name='cdate_value' title='" + dtDates.Rows[DateIndex]["Autoid"]+"_"+ dtDates.Rows[DateIndex]["CourseId"] + "'>" + dName + "&nbsp;"
                            + " | " + Convert.ToDateTime(dtDates.Rows[DateIndex]["CourseDate"]).ToString("dd/MM/yyyy") + "&nbsp;"
                            + " | " + dtDates.Rows[DateIndex]["CourseTime"]
                            + trainerStr
                            + "</input></label></div><hr>";
                    }

                    s += @"<script>
                        function displayRadioValue() { 
                            var ele = document.getElementsByName('cdate_value'); 
                            var a1 = false;
                            for(i = 0; i < ele.length; i++) { 
                                if(ele[i].checked){ 
                                    a1=true;
                                    window.location.href = 'WebForm1.aspx?id=' + ele[i].title
                                    }
                                } 
                            if(!a1)
                              alert('يجب تحديد الوقت والتاريخ');
                        } 
                            </script>";
                    int xx = Convert.ToInt16(dt.Rows[i]["CourseStatus"]);
                    if (Convert.ToInt16(dt.Rows[i]["CourseStatus"])==1)
                    s += "<a  nohref onclick='javascript:displayRadioValue()' style='color:blue;cursor:pointer'> للتسجيل اضغط هنا</a></div>";
                    else
                        s += "<a  nohref >التسجيل غير متاح حاليا</a></div>";
                    //s += "<a  onclick='alert('Hello World')' href='WebForm1.aspx?id=" + dt.Rows[i]["CourseId"] + "'> للتسجيل اضغط هنا</a></div>";
                    if ((i + 1) % 3 == 0)
                        s += "<div style='clear:both'></div>";


                }
                s += "";
                    courseDetails.InnerHtml = s.ToString();
                


                conn.Close();
            }
        }
    }
}