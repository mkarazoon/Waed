using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ResearchAcademicUnit
{
    public partial class MasterReport : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl divh = (HtmlGenericControl)Page.Master.FindControl("prinOut");
            divh.Visible = false;

            HtmlGenericControl divf = (HtmlGenericControl)Page.Master.FindControl("printfooter");
            divf.Visible = false;

            if(!IsPostBack)
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmdd = new SqlCommand("select * from Faculty where CollegeName<>N'الصيدلة' and CollegeName<>N'الهندسة'", conn);
                DataTable dtFaculty = new DataTable();
                dtFaculty.Load(cmdd.ExecuteReader());

                ddlStudFaculty.DataSource = dtFaculty;
                ddlStudFaculty.DataTextField = "CollegeName";
                ddlStudFaculty.DataValueField = "AutoId";
                ddlStudFaculty.DataBind();
                ddlStudFaculty.Items.Insert(0, "حدد الكلية");
                ddlStudFaculty.Items[0].Value = "0";

                ddlSuperFaculty.DataSource = dtFaculty;
                ddlSuperFaculty.DataTextField = "CollegeName";
                ddlSuperFaculty.DataValueField = "AutoId";
                ddlSuperFaculty.DataBind();
                ddlSuperFaculty.Items.Insert(0, "حدد الكلية");
                ddlSuperFaculty.Items[0].Value = "0";

                conn.Close();
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string methodMeeting = "";
            for(int i=0;i<=6;i++)
            {
                if (chkMethod.Items[i].Selected)
                    methodMeeting += chkMethod.Items[i].Value + ",";
            }
            methodMeeting = methodMeeting.Length != 0 ? methodMeeting.Substring(0, methodMeeting.Length - 1) : "";
            //if(methodMeeting.Length!=0)
            //{
                //lblMethodErr.Visible = false;
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("select * from MeetingReport where Studid=" + txtStudId.Text + " and ReportId=" + ddlReportNo.SelectedValue, conn);
                if (!cmd.ExecuteReader().HasRows)
                {
                    string sql = "insert into MeetingReport values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14)";
                    cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@p1", ddlReportNo.SelectedValue);
                    cmd.Parameters.AddWithValue("@p2", txtStudId.Text);
                    cmd.Parameters.AddWithValue("@p3", txtStudName.Text);
                    cmd.Parameters.AddWithValue("@p4", ddlStudFaculty.SelectedValue);
                    cmd.Parameters.AddWithValue("@p5", txtStudMajor.Text);
                    cmd.Parameters.AddWithValue("@p6", txtSupervisorName.Text);
                    cmd.Parameters.AddWithValue("@p7", ddlSuperFaculty.SelectedValue);
                    cmd.Parameters.AddWithValue("@p8", ddlDept.SelectedValue);
                    cmd.Parameters.AddWithValue("@p9", txtThesisTitle.Text);
                    cmd.Parameters.AddWithValue("@p10", txtStudAchievment.Text);
                    cmd.Parameters.AddWithValue("@p11", txtStudRequired.Text);
                    cmd.Parameters.AddWithValue("@p12", methodMeeting);
                    cmd.Parameters.AddWithValue("@p13", ddlOption.SelectedValue);
                    cmd.Parameters.AddWithValue("@p14", Convert.ToDateTime(txtReportDate.Text));
                    cmd.ExecuteNonQuery();

                    sendEmail();

                    System.Web.UI.ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('تم ارسال التقرير بنجاح');", true);
                }
                else
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('تم ارسال التقرير من قبل');", true);
                }
                conn.Close();
            //}
            //var smtp = new SmtpClient
            //{
            //    Host = "smtp.office365.com",
            //    Port = 587,
            //    EnableSsl = true,
            //    DeliveryMethod = SmtpDeliveryMethod.Network,
            //    UseDefaultCredentials = false,
            //    Credentials = new NetworkCredential("mkarazoon@meu.edu.jo", "malo@lian1")
            //};
            //using (var message = new MailMessage("mkarazoon@meu.edu.jo", "mkarazoon@meu.edu.jo")
            //{
            //    Subject = "دورة ",
            //    Body = "تمت الموافقة على تسجيلك بالدورة"

            //})
            //{
            //    message.Attachments.Add(new Attachment(Server.MapPath("images/cert.png")));
            //    smtp.Send(message);
            //}

        }

        protected void sendEmail()
        {
            var smtp = new SmtpClient
            {
                Host = "smtp.office365.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("mkarazoon@meu.edu.jo", "malo@lian1")
            };
            using (var message = new MailMessage("mkarazoon@meu.edu.jo", "Graduate-dept@meu.edu.jo")
            {
                Subject = "تقرير رقم  " + ddlReportNo.SelectedValue + " للطالب رقم " + txtStudId.Text,
                Body ="<body dir=rtl>"+ "تم ارسال معلومات التقرير الخاص بالطالب "+ txtStudName.Text +"<br> والمشرف "+ txtSupervisorName.Text
                + "<br> لكلية " + ddlStudFaculty.SelectedValue=="0"?"غير محددة": ddlStudFaculty.SelectedItem.Text 
                + "<br> لطباعة التقرير " 
                + "<a href='http://meusr-ra.meu.edu.jo/MasterReportView.aspx?StudId="+txtStudId.Text +"&ReportId="+ddlReportNo.SelectedValue
                +"'>اضغط هنا</a></body>",
                IsBodyHtml=true
            })
            {
                //message.Attachments.Add(new Attachment(Server.MapPath("images/cert.png")));
                smtp.Send(message);
            }

        }

        protected void ddlSuperFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlSuperFaculty.SelectedValue!="0")
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmdd = new SqlCommand("select * from Department where CAutoid="+ddlSuperFaculty.SelectedValue, conn);
                DataTable dtDept = new DataTable();
                dtDept.Load(cmdd.ExecuteReader());

                ddlDept.DataSource = dtDept;
                ddlDept.DataTextField = "DeptName";
                ddlDept.DataValueField = "AutoId";
                ddlDept.DataBind();
                ddlDept.Items.Insert(0, "حدد القسم");
                ddlDept.Items[0].Value = "0";
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.ContentType = "application/pdf";

            Response.AddHeader("content-disposition", "attachment;filename=dd.pdf");

            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            StringWriter stringWriter = new StringWriter();

            HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);

            divprint.RenderControl(htmlTextWriter);

            StringReader stringReader = new StringReader(stringWriter.ToString());

            Document Doc = new Document(PageSize.A4, 10f, 10f, 40f, 0f);

            HTMLWorker htmlparser = new HTMLWorker(Doc);

            PdfWriter.GetInstance(Doc, Response.OutputStream);

            Doc.Open();

            htmlparser.Parse(stringReader);

            Doc.Close();

            Response.Write(Doc);

            Response.End();

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /*Verifies that the control is rendered */
        }

        protected void rdOption_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            whoInsertedDiv.Visible = false;
            divprint.Visible = true;
        }
    }
}