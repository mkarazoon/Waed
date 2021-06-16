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
    public partial class CourseRegistration : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);
        static string connstring1 = System.Configuration.ConfigurationManager.ConnectionStrings["MEUCV"].ConnectionString;
        SqlConnection conn1 = new SqlConnection(connstring1);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uid"] == null)
                Response.Redirect("Login.aspx");

            if(Session["uid"].ToString()!="2422")
                Response.Redirect("Default.aspx");

            Session["backurl"] = "Default.aspx";
            if (!IsPostBack)
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmdCInfo = new SqlCommand(@"select distinct CI.CourseId,(CI.CourseName+' | '+
                                                    (case 
                                                    when CI.CourseLevel=1 then N'مبتدئ'
                                                    when CI.CourseLevel=2 then N'متوسط'
                                                    when CI.CourseLevel=3 then N'متقدم'
                                                    when CI.CourseLevel=4 then N'عام'
                                                    end)) CourseName_Level
                                                FROM CourseInfo CI
                                                order by CI.CourseId", conn);
                DataTable dtt = new DataTable();
                dtt.Load(cmdCInfo.ExecuteReader());

                ddlCourseName.DataSource = dtt;
                ddlCourseName.DataValueField = "CourseId";
                ddlCourseName.DataTextField = "CourseName_Level";
                ddlCourseName.DataBind();
                ddlCourseName.Items.Insert(0, "حدد النشاط البحثي");
                ddlCourseName.Items[0].Value = "0";

                ddlCName.DataSource = dtt;
                ddlCName.DataValueField = "CourseId";
                ddlCName.DataTextField = "CourseName_Level";
                ddlCName.DataBind();
                ddlCName.Items.Insert(0, "حدد النشاط البحثي");
                ddlCName.Items[0].Value = "0";


                SqlCommand cmd = new SqlCommand(@"select distinct CI.CourseId,JobId,RaName,RI.College 'RCollege'
                                                  ,Status,CI.College 'TargetCollege', concat(CourseDate,' / ',CourseTime) Details,
                                                    RegisterDate,concat(CI.CourseName,' | ',
                                                    (case 
                                                    when CI.CourseLevel=1 then N'مبتدئ'
                                                    when CI.CourseLevel=2 then N'متوسط'
                                                    when CI.CourseLevel=3 then N'متقدم'
                                                    when CI.CourseLevel=4 then N'عام'
                                                    end)) CourseName_Level
                                                FROM [InstructorCourses] IC, ResearcherInfo RI, CourseDates CD, CourseInfo CI
                                                where IC.JobId = RI.AcdId and IC.CourseId = CD.CourseId and IC.SlotId =CD.AutoId and CI.CourseId = ic.CourseId
                                                and status=N'قيد التنفيذ'
                                                order by CI.CourseId,RI.College", conn);
                GridView1.DataSource = cmd.ExecuteReader();
                GridView1.DataBind();

                if (GridView1.Rows.Count == 0)
                    CurrentDiv.Visible = false;
                else
                    CurrentDiv.Visible = true;

                cmd = new SqlCommand(@"select distinct CI.CourseId,CI.CourseName,JobId,RaName,RI.College 'RCollege',Status,CI.College 'TargetCollege', concat(CourseDate,' / ',CourseTime) Details,
                                                    RegisterDate,concat(CI.CourseName,' | ',
                                                    (case 
                                                    when CI.CourseLevel=1 then N'مبتدئ'
                                                    when CI.CourseLevel=2 then N'متوسط'
                                                    when CI.CourseLevel=3 then N'متقدم'
                                                    when CI.CourseLevel=4 then N'عام'
                                                    end)) CourseName_Level
                                                FROM [InstructorCourses] IC, ResearcherInfo RI, CourseDates CD, CourseInfo CI
                                                where IC.JobId = RI.AcdId and IC.CourseId = CD.CourseId and IC.SlotId =CD.AutoId and CI.CourseId = ic.CourseId
                                                and status=N'تمت الموافقة'
                                                order by CI.CourseId,RI.College", conn);
                GridView2.DataSource = cmd.ExecuteReader();
                GridView2.DataBind();
                if (GridView2.Rows.Count == 0)
                    AgreeDiv.Visible = false;
                else
                    AgreeDiv.Visible = true;
                cmd = new SqlCommand(@"select distinct CI.CourseId,CI.CourseName,JobId,RaName,RI.College 'RCollege',Status,CI.College 'TargetCollege', concat(CourseDate,' / ',CourseTime) Details,
                                                    RegisterDate,concat(CI.CourseName,' | ',
                                                    (case 
                                                    when CI.CourseLevel=1 then N'مبتدئ'
                                                    when CI.CourseLevel=2 then N'متوسط'
                                                    when CI.CourseLevel=3 then N'متقدم'
                                                    when CI.CourseLevel=4 then N'عام'
                                                    end)) CourseName_Level
                                                FROM [InstructorCourses] IC, ResearcherInfo RI, CourseDates CD, CourseInfo CI
                                                where IC.JobId = RI.AcdId and IC.CourseId = CD.CourseId and IC.SlotId =CD.AutoId and CI.CourseId = ic.CourseId
                                                and status=N'تعليق المشاركة'
                                                order by CI.CourseId,RI.College", conn);
                GridView3.DataSource = cmd.ExecuteReader();
                GridView3.DataBind();
                if (GridView3.Rows.Count == 0)
                    disAgreeDiv.Visible = false;
                else
                    disAgreeDiv.Visible = true;

                cmd = new SqlCommand(@"select distinct CI.CourseId,CI.CourseName,JobId,RaName,RI.College 'RCollege',Status,CI.College 'TargetCollege', concat(CourseDate,' / ',CourseTime) Details,
                                                    RegisterDate,concat(CI.CourseName,' | ',
                                                    (case 
                                                    when CI.CourseLevel=1 then N'مبتدئ'
                                                    when CI.CourseLevel=2 then N'متوسط'
                                                    when CI.CourseLevel=3 then N'متقدم'
                                                    when CI.CourseLevel=4 then N'عام'
                                                    end)) CourseName_Level
                                                FROM [InstructorCourses] IC, ResearcherInfo RI, CourseDates CD, CourseInfo CI
                                                where IC.JobId = RI.AcdId and IC.CourseId = CD.CourseId and IC.SlotId =CD.AutoId and CI.CourseId = ic.CourseId
                                                and (status=N'غياب بعذر' or status=N'غياب بدون عذر')
                                                order by CI.CourseId,RI.College", conn);
                GridView4.DataSource = cmd.ExecuteReader();
                GridView4.DataBind();
                if (GridView4.Rows.Count == 0)
                    AbsDiv.Visible = false;
                else
                    AbsDiv.Visible = true;


                conn.Close();
            }
        }

        protected void lnkAgree_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;


            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand("Update InstructorCourses set status=N'تمت الموافقة' where jobid=" + row.Cells[1].Text + " and courseid=" + row.Cells[0].Text, conn);
            cmd.ExecuteNonQuery();

            Response.Redirect("CourseRegistration.aspx");
            //cmd = new SqlCommand(@"select distinct CI.CourseId,CI.CourseName,JobId,RaName,RI.College 'RCollege',Status,CI.College 'TargetCollege', concat(CourseDate,' / ',CourseTime) Details
            //                                    FROM [InstructorCourses] IC, ResearcherInfo RI, CourseDates CD, CourseInfo CI
            //                                    where IC.JobId = RI.AcdId and IC.CourseId = CD.CourseId and IC.SlotId =CD.AutoId and CI.CourseId = ic.CourseId
            //                                    and status=N'تمت الموافقة'
            //                                    order by CI.CourseId", conn);
            //GridView2.DataSource = cmd.ExecuteReader();
            //GridView2.DataBind();

            //cmd = new SqlCommand(@"select distinct CI.CourseId,CI.CourseName,JobId,RaName,RI.College 'RCollege',Status,CI.College 'TargetCollege', concat(CourseDate,' / ',CourseTime) Details
            //                                    FROM [InstructorCourses] IC, ResearcherInfo RI, CourseDates CD, CourseInfo CI
            //                                    where IC.JobId = RI.AcdId and IC.CourseId = CD.CourseId and IC.SlotId =CD.AutoId and CI.CourseId = ic.CourseId
            //                                    and status=N'قيد التنفيذ'
            //                                    order by CI.CourseId", conn);
            //GridView1.DataSource = cmd.ExecuteReader();
            //GridView1.DataBind();

            //send email

            //var smtp = new SmtpClient
            //{
            //    Host = "smtp.gmail.com",
            //    Port = 587,
            //    EnableSsl = true,
            //    DeliveryMethod = SmtpDeliveryMethod.Network,
            //    UseDefaultCredentials = false,
            //    Credentials = new NetworkCredential("mkarazoon@gmail.com", "malo@lian1")
            //};
            //using (var message = new MailMessage("mkarazoon@gmail.com", "mkarazoon@gmail.com")
            //{
            //    Subject = "دورة " + row.Cells[4].Text,
            //    Body = "تمت الموافقة على تسجيلك بالدورة"
                
            //})
            //{
            //    message.Attachments.Add(new Attachment(""));
            //    smtp.Send(message);
            //}




            conn.Close();
        }

        protected void lnkDisAgree_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;


            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand("Update InstructorCourses set status=N'تعليق المشاركة' where jobid=" + row.Cells[1].Text + " and courseid=" + row.Cells[0].Text, conn);
            cmd.ExecuteNonQuery();
            Response.Redirect("CourseRegistration.aspx");
            //cmd = new SqlCommand(@"select distinct CI.CourseId,CI.CourseName,JobId,RaName,RI.College 'RCollege',Status,CI.College 'TargetCollege', concat(CourseDate,' / ',CourseTime) Details
            //                                    FROM [InstructorCourses] IC, ResearcherInfo RI, CourseDates CD, CourseInfo CI
            //                                    where IC.JobId = RI.AcdId and IC.CourseId = CD.CourseId and IC.SlotId =CD.AutoId and CI.CourseId = ic.CourseId
            //                                    and status=N'عدم الموافقة'
            //                                    order by CI.CourseId", conn);
            //GridView3.DataSource = cmd.ExecuteReader();
            //GridView3.DataBind();

            //cmd = new SqlCommand(@"select distinct CI.CourseId,CI.CourseName,JobId,RaName,RI.College 'RCollege',Status,CI.College 'TargetCollege', concat(CourseDate,' / ',CourseTime) Details
            //                                    FROM [InstructorCourses] IC, ResearcherInfo RI, CourseDates CD, CourseInfo CI
            //                                    where IC.JobId = RI.AcdId and IC.CourseId = CD.CourseId and IC.SlotId =CD.AutoId and CI.CourseId = ic.CourseId
            //                                    and status=N'قيد التنفيذ'
            //                                    order by CI.CourseId", conn);
            //GridView1.DataSource = cmd.ExecuteReader();
            //GridView1.DataBind();

            conn.Close();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (conn1.State == ConnectionState.Broken || conn1.State == ConnectionState.Closed)
                    conn1.Open();
                string[] cname = e.Row.Cells[6].Text.Split(',');

                SqlCommand cmd = new SqlCommand("Select CollegeName From College where AutoId in (" + e.Row.Cells[6].Text + ")", conn1);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                string newCollege = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    newCollege += dt.Rows[i][0].ToString() + ",";
                }
                if (e.Row.Cells[6].Text.Contains("10"))
                    newCollege += "باقي الكليات,";

                if (e.Row.Cells[6].Text.Contains("11"))
                    newCollege += "كل الكليات,";

                HtmlGenericControl div = (HtmlGenericControl)e.Row.Cells[7].FindControl("colNameDiv");
                div.InnerText = newCollege.Substring(0, newCollege.Length - 1);
                conn1.Close();
            }
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            SqlCommand cmd = new SqlCommand("Update InstructorCourses set status=N'قيد التنفيذ' where jobid=" + row.Cells[1].Text + " and courseid=" + row.Cells[0].Text, conn);
            cmd.ExecuteNonQuery();
            Response.Redirect("CourseRegistration.aspx");
            //cmd = new SqlCommand(@"select distinct CI.CourseId,CI.CourseName,JobId,RaName,RI.College 'RCollege',Status,CI.College 'TargetCollege', concat(CourseDate,' / ',CourseTime) Details
            //                                    FROM [InstructorCourses] IC, ResearcherInfo RI, CourseDates CD, CourseInfo CI
            //                                    where IC.JobId = RI.AcdId and IC.CourseId = CD.CourseId and IC.SlotId =CD.AutoId and CI.CourseId = ic.CourseId
            //                                    and status=N'قيد التنفيذ'
            //                                    order by CI.CourseId", conn);
            //GridView1.DataSource = cmd.ExecuteReader();
            //GridView1.DataBind();

            //if (btn.NamingContainer.ClientID.ToString().ToLower().Contains("gridview2"))
            //{
            //    cmd = new SqlCommand(@"select distinct CI.CourseId,CI.CourseName,JobId,RaName,RI.College 'RCollege',Status,CI.College 'TargetCollege', concat(CourseDate,' / ',CourseTime) Details
            //                                    FROM [InstructorCourses] IC, ResearcherInfo RI, CourseDates CD, CourseInfo CI
            //                                    where IC.JobId = RI.AcdId and IC.CourseId = CD.CourseId and IC.SlotId =CD.AutoId and CI.CourseId = ic.CourseId
            //                                    and status=N'تمت الموافقة'
            //                                    order by CI.CourseId", conn);
            //    GridView2.DataSource = cmd.ExecuteReader();
            //    GridView2.DataBind();
            //}
            //else
            //{

            //    cmd = new SqlCommand(@"select distinct CI.CourseId,CI.CourseName,JobId,RaName,RI.College 'RCollege',Status,CI.College 'TargetCollege', concat(CourseDate,' / ',CourseTime) Details
            //                                    FROM [InstructorCourses] IC, ResearcherInfo RI, CourseDates CD, CourseInfo CI
            //                                    where IC.JobId = RI.AcdId and IC.CourseId = CD.CourseId and IC.SlotId =CD.AutoId and CI.CourseId = ic.CourseId
            //                                    and status=N'عدم الموافقة'
            //                                    order by CI.CourseId", conn);
            //    GridView3.DataSource = cmd.ExecuteReader();
            //    GridView3.DataBind();
            //}

            conn.Close();

        }

        protected void lnkAbsWE_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            SqlCommand cmd = new SqlCommand("Update InstructorCourses set status=N'غياب بعذر' where jobid=" + row.Cells[1].Text + " and courseid=" + row.Cells[0].Text, conn);
            cmd.ExecuteNonQuery();
            Response.Redirect("CourseRegistration.aspx");
        }

        protected void lnkAbsWOE_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            SqlCommand cmd = new SqlCommand("Update InstructorCourses set status=N'غياب بدون عذر' where jobid=" + row.Cells[1].Text + " and courseid=" + row.Cells[0].Text, conn);
            cmd.ExecuteNonQuery();
            Response.Redirect("CourseRegistration.aspx");
        }

        protected void ddlCourseName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand(@"select distinct CI.CourseId,JobId,RaName,RI.College 'RCollege'
                                                  ,Status,CI.College 'TargetCollege', concat(CourseDate,' / ',CourseTime) Details,
                                                    RegisterDate,concat(CI.CourseName,' | ',
                                                    (case 
                                                    when CI.CourseLevel=1 then N'مبتدئ'
                                                    when CI.CourseLevel=2 then N'متوسط'
                                                    when CI.CourseLevel=3 then N'متقدم'
                                                    when CI.CourseLevel=4 then N'عام'
                                                    end)) CourseName_Level
                                                FROM [InstructorCourses] IC, ResearcherInfo RI, CourseDates CD, CourseInfo CI
                                                where IC.JobId = RI.AcdId and IC.CourseId = CD.CourseId and IC.SlotId =CD.AutoId and CI.CourseId = ic.CourseId
                                                and status=N'تمت الموافقة' and CI.CourseId=" + ddlCourseName.SelectedValue + @" 
                                                order by CI.CourseId,RI.College", conn);
            GridView5.DataSource = cmd.ExecuteReader();
            GridView5.DataBind();

            btnExport.Visible = true;
            //Response.Clear();
            //Response.Buffer = true;
            //Response.ClearContent();
            //Response.ClearHeaders();
            //Response.Charset = "";
            //string FileName = ddlCourseName.SelectedItem.Text + "_" + DateTime.Now + ".xls";
            //StringWriter strwritter = new StringWriter();
            //HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.ContentType = "application/vnd.ms-excel";
            //Response.ContentEncoding = System.Text.Encoding.Unicode;
            //Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            //Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            //GridView5.GridLines = GridLines.Both;
            //GridView5.HeaderStyle.Font.Bold = true;
            //GridView5.RenderControl(htmltextwrtter);
            //Response.Write(strwritter.ToString());
            //Response.End();

            //Response.Redirect("CourseRegistration.aspx");


            //conn.Close();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //required to avoid the runtime error "  
            //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {

            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = ddlCourseName.SelectedItem.Text + "_" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            GridView5.GridLines = GridLines.Both;
            GridView5.HeaderStyle.Font.Bold = true;
            GridView5.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();

            //Response.Redirect("CourseRegistration.aspx");


            conn.Close();
        }

        protected void ddlCName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand(@"SELECT [JobId],raname
                                          ,[EvalDate]
                                          ,[CourseId]
                                          ,([Q1]+[Q2]+[Q3]+[Q4]+[Q5]+[Q6]+[Q7]+[Q8]+[Q9]+[Q10]+[Q11]+[Q12]+[Q13]+[Q14]+[Q15]+[Q16]+[Q17]+[Q18]+[Q19]+[Q20]) 'EvalR'
                                      FROM CourseEval,ResearcherInfo
                                      where JobId=AcdId and CourseId=" + ddlCName.SelectedValue, conn);
            GridView6.DataSource = cmd.ExecuteReader();
            GridView6.DataBind();
            Button1.Visible = true;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = ddlCName.SelectedItem.Text + "_" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            GridView6.GridLines = GridLines.Both;
            GridView6.HeaderStyle.Font.Bold = true;
            GridView6.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
        }
    }
}