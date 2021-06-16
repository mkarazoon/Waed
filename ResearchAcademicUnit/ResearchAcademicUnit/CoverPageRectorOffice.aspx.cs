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
    public partial class CoverPageRectorOffice : System.Web.UI.Page
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
                //if (Session["FormTypePrint"].ToString().Contains("رسوم"))
                //reqid.InnerText = Session["ViewRequestFrom"].ToString();
                cmdd = new SqlCommand("select * from ReDirectorInfo where " + (Session["FormTypePrint"].ToString().Contains("رسوم") ? " type=0 and " : " type=1 and ") + " RequestId=" + Session["ViewRequestFrom"], conn);
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

                StringBuilder sb = new StringBuilder();
                if (Session["FormTypePrint"].ToString().Contains("رسوم"))
                {
                    sb.Append(@"<div id = 'CoverDiv' style = 'font-size:18px'>
                            <div style = 'text-align: center;margin-top:30px'>
                                <img src = 'images/MEU.png' style ='width:360px' />
                                <div style='font-family:Khalid Art;font-size:26px' >
                                    مكتب رئيس الجامعة
                                </div>
                                <div style = 'font-family: Agency FB; font-size: 20px' >
                                    President Office
                                </div>
                                <hr />
                            </div>
                            <div class='showdiv' style='text-align: center'>رقم الطلب : " + Session["ViewRequestFrom"].ToString() + @"</div>
                            <div style = 'text-align: center;font-family:Khalid Art;font-weight:bold;padding:50px 50px 20px;font-size:16px' >
                                          قرار رقم(...-2020 / 2021) <br /><br />
                                          فبناءً على الصلاحيات المخولة لنا وما تقتضيه مصلحة العمل<br /><br />
                                          <u> تقــــــرر </u>
                                      </div>
                                      <div style='text-align: right;font-family:Simplified Arabic;padding:0px 50px 20px;font-size:16px;text-indent: 50px;'>" +
                                            "الموافقة على دعم رسوم نشر بحث علمي لـ د.  " + dtt.Rows[0]["RaName"].ToString() + " / كلية "
                                            + dtt.Rows[0]["College"].ToString() + " بمبلغ مقداره (" + dr[3].ToString() + ")  للبحث والمعنون بـ: " + @"
                                           </div>
                                           <div style='text-align:center;font-family:Simplified Arabic;font-weight:bold;padding:0px 50px 20px;font-size:16px'>" +
                                               dtForm.Rows[0][6].ToString()
                                               + @"
                                                </div>
                                                <div style = 'text-align: center;font-family:Simplified Arabic;padding:0px 50px 20px;font-size:16px'>
                                                
                                                         كون المجلة مفهرسة ضمن قاعدة بيانات <span style = 'font-family:Simplified Arabic;font-size:16px' > Scopus </span> ومصنفة في " + qrt+@".

على أن تكون قيمة الدعم سلفة على الباحث لحين فهرسة البحث على قاعدة بيانات SCOPUS.
                                                      </div >
                                                      <div style = 'text-align: center;font-family:Khalid Art;font-weight:bold;padding:0px 50px 20px;font-size:16px'>
                                                           وتفضلوا بقبول فائق الإحترام...
                                                       </div>
                                                       <div style = 'text-align: left;font-family:Khalid Art;font-weight:bold;padding:0px 50px 20px;font-size:16px' >
                                                            رئيس الجامعة
                                                                       &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                <br /><br />
                                                أ.د.علاء الدين توفيق الحلحولي
                                           </div>

                                           <div style = 'position: absolute;bottom: 0px;width:100%' >
                                                <hr />
                                                <img src ='images/newfooter.png' width =250px />
                                                  </div>
                                              </div>");
                }
                else
                {
                    cmdd = new SqlCommand("select * from ReDirectorInfo where " + (Session["FormTypePrint"].ToString().Contains("رسوم") ? " type=0 and " : " type=1 and ") + " RequestId=" + Session["ViewRequestFrom"], conn);
                    SqlDataReader drr = cmdd.ExecuteReader();
                    drr.Read();
                    //string qrt = "";
                    //switch (drr[4].ToString())
                    //{
                    //    case "1":
                    //        qrt = "<font style='background-color:yellow'> ( الربع الأول )( Q1 )</font>";
                    //        break;
                    //    case "2":
                    //        qrt = "<font style='background-color:yellow'> ( الربع الثاني )( Q2 )</font>";
                    //        break;
                    //    case "3":
                    //        qrt = "<font style='background-color:yellow'> ( الربع الثالث )( Q3 )</font>";
                    //        break;
                    //    case "4":
                    //        qrt = "<font style='background-color:yellow'> ( الربع الرابع )( Q4 )</font>";
                    //        break;
                    //    case "5":
                    //        qrt = "<font style='background-color:yellow'> ( Non Qs )</font>";
                    //        break;
                    //}
                    string clarv = " وغير مفهرسة في Clarivate Analytics.";

                    if (drr[13].ToString() != "0")
                        clarv = "ومفهرسة في Clarivate Analytics - " + "Arts and Humanities Citation Index (AHCI)";
                    if (drr[14].ToString() != "0")
                        clarv = "ومفهرسة في Clarivate Analytics - " + "Science Citation Index Expanded (SCIE)";
                    if (drr[15].ToString() != "0")
                        clarv = "ومفهرسة في Clarivate Analytics - " + "Social Sciences Citation Index (SSCI)";

                    cmdd = new SqlCommand("Select * From NewAmountAward where reqid=" + Session["ViewRequestFrom"], conn);
                    DataTable dtResearcher = new DataTable();
                    dtResearcher.Load(cmdd.ExecuteReader());
                    for(int i=0;i<dtResearcher.Rows.Count;i++)
                    {
                        sb.Append(@"<div id='CoverDiv' style ='font-size:18px;page-break-after: always;page-break-inside: avoid;'>
                            <div style = 'text-align: center;margin-top:30px'>
                                <img src = 'images/MEU.png' style ='width:360px' />
                                <div style='font-family:Khalid Art;font-size:26px' >
                                    مكتب رئيس الجامعة
                                </div>
                                <div style = 'font-family: Agency FB; font-size: 20px' >
                                    President Office
                                </div>
                                <hr />
                            </div>
<div class='showdiv' style='text-align: center'>رقم الطلب : " + Session["ViewRequestFrom"].ToString() + @"</div>
                            <div style = 'text-align: center;font-family:Khalid Art;font-weight:bold;padding:50px 50px 20px;font-size:16px'>
                                          قرار رقم(...-2020 / 2021) <br /><br />
                                          فبناءً على الصلاحيات المخولة لنا وما تقتضيه مصلحة العمل<br /><br />
                                          <u> تقــــــرر </u>
                           </div>
                           <div style='text-align: right;font-family:Simplified Arabic;padding:0px 50px 20px;font-size:16px;text-indent: 50px;'>" +
                                                "الموافقة على مكافأة على نشر بحث علمي لـ د.  " + dtResearcher.Rows[i][3].ToString() + " / كلية "
                                                + dtResearcher.Rows[i][4].ToString() + " بمبلغ مقداره (" + dtResearcher.Rows[i][6].ToString() + ")  للبحث والمعنون بـ: " + @"
                                           </div>
                                           <div style='text-align:center;font-family:Simplified Arabic;font-weight:bold;padding:0px 50px 20px;font-size:16px'>" +
                                                   dtForm.Rows[0][6].ToString()
                                                   + @"
                                                </div>
                                                <div style = 'text-align: center;font-family:Simplified Arabic;padding:0px 50px 20px;font-size:16px'>
                                                
                                                         كون البحث منشور في مجلة مفهرسة ضمن قاعدة البيانات <span style = 'font-family:Simplified Arabic;font-size:16px' > Scopus </span> ومصنفة في 
                                                    "+ qrt +"<br>"+ clarv + @"
                                   
                                                      </div >
                                                      <div style = 'text-align: center;font-family:Khalid Art;font-weight:bold;padding:0px 50px 20px;font-size:16px'>
                                                           وتفضلوا بقبول فائق الإحترام...
                                                       </div>
                                                       <div style = 'text-align: left;font-family:Khalid Art;font-weight:bold;padding:0px 50px 20px;font-size:16px' >
                                                            رئيس الجامعة
                                                                      &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                <br /><br />
                                                أ.د.علاء الدين توفيق الحلحولي
                                           </div>

                                           <div style = 'position:fixed;bottom: 0px;width:100%' >
                                                <hr />
                                                <img src ='images/newfooter.png' width =250px />
                                                  </div>
                                              </div>");
                    }



                }
                newDiv.InnerHtml = sb.ToString();

                //FParagDiv.InnerHtml = ;
                //SParagDiv.InnerHtml = dtForm.Rows[0][6].ToString();
                //        SParagDiv.InnerHtml +="<div>"+ "كون البحث مفهرس ضمن قاعدة بيانات Scopus ومصنف بالربع " + qrt +"</div>";

                   // TParagDiv.InnerHtml = (Session["FormTypePrint"].ToString().Contains("رسوم") ? "على أن تكون قيمة الدعم سلفة على الباحث لحين فهرسة البحث على قاعدة بيانات SCOPUS." : "") ;
                ////if (Convert.ToDateTime(dtForm.Rows[0]["ReqDate"]) < Convert.ToDateTime("01.09.2020"))
                ////    lblPos.Text = "قائم بأعمال رئيس الجامعة";
                ////else
                //    lblPos.Text = "رئيس الجامعة";
            }
        }
    }
}