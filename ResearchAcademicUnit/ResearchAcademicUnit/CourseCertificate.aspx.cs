using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ResearchAcademicUnit
{
    public partial class CourseCertificate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["backurl"] = "AvailableCourse.aspx";
            HtmlGenericControl divh = (HtmlGenericControl)Page.Master.FindControl("prinOut");
            divh.Visible = false;

            HtmlGenericControl divf = (HtmlGenericControl)Page.Master.FindControl("printfooter");
            divf.Visible = false;

            if (Session["uid"] == null)
                Response.Redirect("Login.aspx");

            if(!IsPostBack)
            {
                AText.InnerHtml = "<p style='font-size:26px'>" + "تشهد جامعة الشرق الأوسط - عمان<br>بأن </p>";
                AText.InnerHtml += "<p style='color:#921A1D;font-size:30px'>" +  Session["userName"].ToString()+ "</p>";
                AText.InnerHtml += "<p style='font-size:26px'>" +
                    (Session["Sex"].ToString()=="M"?
                    "قد حضر برنامجا تدريبيا بعنوان": "قد حضرت برنامجا تدريبيا بعنوان") + "</p>";
                AText.InnerHtml += "<p style='color:#921A1D;font-size:28px'>" + Session["CourseName"].ToString() + " <br> مستوى " + Session["CourseLevel"].ToString() + "</p>";
                AText.InnerHtml += "<p style='font-size:26px'>" + " ولمدة " + Session["CourseHour"].ToString() +" تدريبية بتاريخ " + Session["CourseDate"].ToString() + "</p><br>";
                AText.InnerHtml += "<div style='float:right;text-align:right;margin-top:20px'> <p>رئيس الجامعة</p>";
                AText.InnerHtml += "<p>أ.د. محمد الحيلة</p></div>";


                EText.InnerHtml = "<p style='font-size:24px'>Middle East University - Amman hereby certifies that:</p>";
                EText.InnerHtml += "<p style='color:#921A1D;font-weight:bold;font-size:24px'>" + Session["userNameE"].ToString() + "</p>";
                EText.InnerHtml += "<p style='font-size:24px'>" +
                                       "Attended a training program on: " + "</p>";
                EText.InnerHtml += "<p style='font-size:24px;color:#921A1D;font-weight:bold'>" + Session["CourseNameE"].ToString() + " <br> " + 
                    
                    
                    (Session["CourseLevel"].ToString()=="مبتدئ"?"Beginner":
                    (Session["CourseLevel"].ToString() == "متوسط" ? "Intermidiate" :
                    (Session["CourseLevel"].ToString() == "متقدم" ? "Advanced" :"General")))
                    
                    
                    
                    +" level" + "</p>";
                EText.InnerHtml += "<p style='font-size:24px'>" + "held for " + Session["CourseHour"].ToString().Substring(0,1) + "  training hours" + " on " + Session["CourseDate"].ToString() + "</p>";
                EText.InnerHtml += "<div style='float:left;text-align:left;margin-top:20px'> <p>MEU President</p>";
                EText.InnerHtml += "<p>Prof. Mohammad Al Hileh</p></div>";


            }
        }
    }
}