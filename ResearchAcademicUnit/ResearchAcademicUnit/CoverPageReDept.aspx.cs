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
    public partial class CoverPageReDept : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.IsNewSession || Session["uid"] == null)
                Response.Redirect("Login.aspx");

            HtmlGenericControl divh = (HtmlGenericControl)Page.Master.FindControl("prinOut");
            divh.Visible = false;

            HtmlGenericControl divf = (HtmlGenericControl)Page.Master.FindControl("printfooter");
            divf.Visible = false;

            if (!IsPostBack)
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmdd = new SqlCommand("select * from ResearcherInfo where acdid=" + Session["ResearchId"], conn);
                DataTable dtt = new DataTable();
                dtt.Load(cmdd.ExecuteReader());

                cmdd = new SqlCommand("select * from " + (Session["FormTypePrint"].ToString().Contains("رسوم") ? "ResearchFeeInfo" : "ResearchRewardForm") + "  where AutoId=" + Session["ViewRequestFrom"], conn);
                DataTable dtForm = new DataTable();
                dtForm.Load(cmdd.ExecuteReader());

                cmdd = new SqlCommand("select * from ReDirectorInfo where " + (Session["FormTypePrint"].ToString().Contains("رسوم") ? "type=0 and" : "type=1 and") + "  RequestId=" + Session["ViewRequestFrom"], conn);
                SqlDataReader dr = cmdd.ExecuteReader();
                dr.Read();
                string qrt = "";
                switch (dr[4].ToString())
                {
                    case "1":
                        qrt = "<font style='background-color:yellow'> ( الربع الأول )( Q1 )</font>";
                        break;
                    case "2":
                        qrt = "<font style='background-color:yellow'> ( الربع الثاني )( Q2 )</font>";
                        break;
                    case "3":
                        qrt = "<font style='background-color:yellow'> ( الربع الثالث )( Q3 )</font>";
                        break;
                    case "4":
                        qrt = "<font style='background-color:yellow'> ( الربع الرابع )( Q4 )</font>";
                        break;
                    case "5":
                        qrt = "<font style='background-color:yellow'> ( Non Qs )</font>";
                        break;
                }
                string clarivate = "";
                if (dr[13].ToString() != "0" || dr[14].ToString() != "0" || dr[15].ToString() != "0")
                    clarivate = "مفهرسة";
                else
                    clarivate = "غير مفهرسة";
                cmdd = new SqlCommand("Select * From NewAmountAward where reqid=" + Session["ViewRequestFrom"], conn);
                DataTable dtResearcher = new DataTable();
                dtResearcher.Load(cmdd.ExecuteReader());
                reqid.InnerText = Session["ViewRequestFrom"].ToString();
                lblFacultyNo.Text ="الرقم : " + dtForm.Rows[0][19].ToString();
                lblCoverDate.Text = "التاريخ :..................";// + DateTime.Now.Date.ToString("dd-MM-yyyy");
                if (Session["FormTypePrint"].ToString().Contains("رسوم"))
                    FParagDiv.InnerHtml = @"فأرفق إليكم نموذج طلب دعم رسوم نشر بحث علمي المرسل من كلية " + dtt.Rows[0]["College"].ToString() + " على كتاب رقم ( " + dtForm.Rows[0]["FacultyReqNo"].ToString() + " )  أرجو التكرم بالعلم بأني أوصي بالموافقة على دعم رسوم نشر بحث علمي بقيمة ( " + dr[3].ToString() + ")  لـ د." + dtt.Rows[0]["RaName"] + " والمعنون بـ: ";
                else
                    FParagDiv.InnerHtml = @"فأرفق إليكم نموذج طلب مكافأة على نشر بحث علمي. أرجو التكرم بالعلم بأني أوصي بالموافقة على مكافأة نشر بحث علمي وفقا للجدولة المرفقة وذلك للبحث المعنون بـ: ";

                SParagDiv.InnerHtml = dtForm.Rows[0][6].ToString();
                if (Session["FormTypePrint"].ToString().Contains("رسوم"))
                {
                    TParagDiv.InnerHtml = dtForm.Rows[0][8].ToString() + "<br>";
                }
                else
                {
                    SqlCommand cmdGetAll = new SqlCommand("Select * From RePartInfoReward where RequestId=" + Session["ViewRequestFrom"], conn);
                    DataTable dtGet = new DataTable();
                    dtGet.Load(cmdGetAll.ExecuteReader());

                    TParagDiv.InnerHtml = "<table width='100%' style='border:1px solid black;border-spacing:0; font-family: 'Khalid Art';' border=1>";
                    TParagDiv.InnerHtml += "<thead><th>اسم المجلة</th><th>الفهرسة في SCOPUS</th><th>الفهرسة في Clarivate Analytics</th><th>عدد الباحثين الكلي</th><th>عدد الباحثين من داخل الجامعة</th></thead>";
                    TParagDiv.InnerHtml += "<tr><td>" + dtForm.Rows[0][8].ToString() + "</td><td>مفهرسة ومصنفة في "+qrt+"</td><td>"+ clarivate + "</td><td>"+dtGet.Rows.Count+"</td><td>"+dtResearcher.Rows.Count+ "</td></tr>";
                    TParagDiv.InnerHtml += "</table>";
                    TParagDiv.InnerHtml += "<div style='text-align:right;margin-top:10px;margin-bottom:10px'>راجيا التكرم بتوقيع النموذج والقرارات المرفقة : </div>";
                    TParagDiv.InnerHtml += "<div style='margin-top:10px;margin-bottom:10px; font-family: 'Khalid Art';'><table width='100%' style='border:1px solid black;border-spacing:0' border=1>";
                    TParagDiv.InnerHtml += "<thead><th>اسم الباحث</th><th>الكلية</th><th>الترتيب</th><th>قيمة المكافأة</th></thead>";
                    for(int i=0;i<dtResearcher.Rows.Count;i++)
                    {
                        TParagDiv.InnerHtml += "<tr><td>" + dtResearcher.Rows[i][3].ToString() + "</td><td>" + dtResearcher.Rows[i][4].ToString() + "</td><td>" + dtResearcher.Rows[i][5].ToString() + "</td><td>" + dtResearcher.Rows[i][6].ToString() + "</td></tr>";
                    }
                    TParagDiv.InnerHtml += "</table></div>";
                }


                if (Session["FormTypePrint"].ToString().Contains("رسوم"))
                {
                    TParagDiv.InnerHtml += "كون المجلة مفهرسة ضمن قاعدة بيانات Scopus " + qrt + "، راجيا التكرم بتوقيع النموذج والقرار المرفقين على أن تكون قيمة الدعم سلفة على الباحث لحين فهرسة البحث على قاعدة بيانات SCOPUS.";
                }
                else
                {
                    //string qrt = "";
                    //switch (dr[4].ToString())
                    //{
                    //    case "1":
                    //        qrt = "<font style='background-color:yellow'> ( الأول )( Q1 )</font>";
                    //        break;
                    //    case "2":
                    //        qrt = "<font style='background-color:yellow'> ( الثاني )( Q2 )</font>";
                    //        break;
                    //    case "3":
                    //        qrt = "<font style='background-color:yellow'> ( الثالث )( Q3 )</font>";
                    //        break;
                    //    case "4":
                    //        qrt = "<font style='background-color:yellow'> ( الرابع )( Q4 )</font>";
                    //        break;
                    //    case "5":
                    //        qrt = "<font style='background-color:yellow'> ( Non Qs )</font>";
                    //        break;
                    //}
                    //TParagDiv.InnerHtml += "كون البحث مفهرس ضمن قاعدة بيانات Scopus ومصنف بالربع " + qrt + "، راجيا التكرم بتوقيع النموذج والقرار المرفقين.";
                }

                lblType.Text = (Session["FormTypePrint"].ToString().Contains("رسوم") ? "- ملف دعم رسوم نشر بحث علمي" : "- ملف مكافأة نشر بحث علمي");

                cmdd = new SqlCommand("Select Notes,RequestDate,RaName,ReqFromId From RequestsFollowUp,researcherinfo ri where actualid=ri.AcdId and  " + (Session["FormTypePrint"].ToString().Contains("رسوم") ? "type=0 and" : "type=1 and") + " Autoid=(Select Max(AutoId) From RequestsFollowUp where  " + (Session["FormTypePrint"].ToString().Contains("رسوم") ? "type=0 and" : "type=1 and") + " RequestId=" + Session["ViewRequestFrom"] + " and ReqStatus like N'%مكتمل%')", conn);
                SqlDataReader drRDNotes = cmdd.ExecuteReader();
                drRDNotes.Read();
                lblName.Text = drRDNotes[2].ToString();
                if (drRDNotes[3].ToString() == "10")
                    lblPos.Text = "ق.أ.عميد الدراسات العليا والبحث العلمي";
                else if (drRDNotes[3].ToString() == "14")
                    lblPos.Text = "عميد الدراسات العليا والبحث العلمي";
            }
        }
    }
}