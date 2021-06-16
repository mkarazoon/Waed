using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ResearchAcademicUnit
{
    public partial class SecPrintRequests : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.IsNewSession || Session["secid"] == null)
                Response.Redirect("Login.aspx");
            Session["ViewRequestFrom"] = null;
            Session["RequestComeFrom"] = null;
            Session["PrintForm"] = null;
            Session["ResearchId"] = Session["userid"];
            Session["backurl"] = "Default.aspx";

            if (!IsPostBack)
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                //SqlCommand cmd = new SqlCommand("Select * From Priviliges where PrivType=3 and PrivDeptId=0 and PrivTo=663", conn);
                SqlCommand cmd = new SqlCommand("Select * From Priviliges where PrivType=3 and PrivDeptId=0 and PrivTo="+ Session["userid"], conn);
                DataTable priv = new DataTable();
                priv.Load(cmd.ExecuteReader());
                if (priv.Rows.Count != 0)
                {
                    for (int privId = 0; privId < priv.Rows.Count; privId++)
                    {
                        // secretary of faculty
                        if (Convert.ToInt16(priv.Rows[privId][2].ToString()) < 10)
                        {
                            SecPrint.Visible = true;

                            string output = "";
                            for (int i = 0; i < priv.Rows.Count; i++)
                            {
                                output = output + priv.Rows[i][2].ToString();
                                output += (i < priv.Rows.Count - 1) ? "," : string.Empty;
                            }

                            string s = @"SELECT *,(case 
	                              when r.RequestType='S' then N'طلب رسوم نشر' 
	                              when r.RequestType='T' then N'طلب مكافأة' 
                                  end) type,r.autoid RequestId,FacultyReqNo
                                  FROM ResearchFeeInfo r, Faculty f,ResearcherInfo ri
                                  where r.JobId = ri.AcdId and ri.College = f.CollegeName
                                  and f.autoid in (" + output + ") and RequestFinalStatus<>N''";

                            SecPrint.Visible = true;
                            cmd = new SqlCommand(s, conn);
                            GridView6.DataSource = cmd.ExecuteReader();
                            GridView6.DataBind();
                        }
                        // secretary of GS
                        if (priv.Rows[privId][2].ToString() == "10" || priv.Rows[privId][2].ToString() == "14")
                        {
                            GSSec.Visible = true;
                            string s = @"select * from (SELECT RaName,ReqDate,(case 
	                              when r.RequestType='S' then N'طلب رسوم نشر' 
                                  end) type,r.autoid RequestId,FacultyReqNo,GSInNo,GSOutNo,RequestFinalStatus
                                  FROM ResearchFeeInfo r, Faculty f,ResearcherInfo ri
                                  where r.JobId = ri.AcdId and ri.College = f.CollegeName
                                  and r.FacultyReqNo<>'' and RequestFinalStatus=N'مكتمل'";
                            s += " union all ";
                            s += @"SELECT RaName,ReqDate,(case 
	                              when r.RequestType='T' then N'طلب مكافأة نشر' 
                                  end) type,r.autoid RequestId,FacultyReqNo,GSInNo,GSOutNo,RequestFinalStatus
                                  FROM ResearchRewardForm r, Faculty f,ResearcherInfo ri
                                  where r.JobId = ri.AcdId and ri.College = f.CollegeName
                                  and r.FacultyReqNo<>'' and RequestFinalStatus=N'مكتمل') h order by GSOutNo,ReqDate";

                            SecPrint.Visible = true;
                            cmd = new SqlCommand(s, conn);
                            GridView1.DataSource = cmd.ExecuteReader();
                            GridView1.DataBind();
                        }
                        // secretary of vice president
                        if (priv.Rows[privId][2].ToString() == "13")
                        {
                            RectorOfficeDiv.Visible = true;
                            string s = @"select * from (SELECT RaName,ReqDate,(case 
	                              when r.RequestType='S' then N'طلب رسوم نشر' 
                                  end) type,r.autoid RequestId,FacultyReqNo,GSInNo,GSOutNo,RequestFinalStatus
                                  FROM ResearchFeeInfo r, Faculty f,ResearcherInfo ri
                                  where r.JobId = ri.AcdId and ri.College = f.CollegeName
                                  and r.GSOutNo<>'' and RequestFinalStatus<>N'' and done=0
                                   ";
                            s += " union all ";
                            s += @"SELECT RaName,ReqDate,(case 
	                              when r.RequestType='T' then N'طلب مكافأة نشر' 
                                  end) type,r.autoid RequestId,FacultyReqNo,GSInNo,GSOutNo,RequestFinalStatus
                                  FROM ResearchRewardForm r, Faculty f,ResearcherInfo ri
                                  where r.JobId = ri.AcdId and ri.College = f.CollegeName
                                  and r.GSOutNo<>'' and RequestFinalStatus<>N'' and done=0) h order by ReqDate desc
                                   ";

                            SecPrint.Visible = true;
                            cmd = new SqlCommand(s, conn);
                            GridView2.DataSource = cmd.ExecuteReader();
                            GridView2.DataBind();

                            s = @"select * from (SELECT RaName,ReqDate,(case 
	                              when r.RequestType='S' then N'طلب رسوم نشر' 
                                  end) type,r.autoid RequestId,FacultyReqNo,GSInNo,GSOutNo,RequestFinalStatus
                                  FROM ResearchFeeInfo r, Faculty f,ResearcherInfo ri
                                  where r.JobId = ri.AcdId and ri.College = f.CollegeName
                                  and r.GSOutNo<>'' and RequestFinalStatus<>N'' and done=1
                                   ";
                            s += " union all ";
                            s += @"SELECT RaName,ReqDate,(case 
	                              when r.RequestType='T' then N'طلب مكافأة نشر' 
                                  end) type,r.autoid RequestId,FacultyReqNo,GSInNo,GSOutNo,RequestFinalStatus
                                  FROM ResearchRewardForm r, Faculty f,ResearcherInfo ri
                                  where r.JobId = ri.AcdId and ri.College = f.CollegeName
                                  and r.GSOutNo<>'' and RequestFinalStatus<>N'' and done=1) h order by ReqDate desc
                                   ";

                            cmd = new SqlCommand(s, conn);
                            GridView3.DataSource = cmd.ExecuteReader();
                            GridView3.DataBind();


                            s = @"select * from (SELECT RaName,ReqDate,(case 
	                              when r.RequestType='S' then N'طلب رسوم نشر' 
                                  end) type,r.autoid RequestId,FacultyReqNo,GSInNo,GSOutNo,RequestFinalStatus
                                  FROM ResearchFeeInfo r, Faculty f,ResearcherInfo ri
                                  where r.JobId = ri.AcdId and ri.College = f.CollegeName
                                  and r.GSOutNo<>'' and RequestFinalStatus<>N''
                                   ";
                            s += " union all ";
                            s += @"SELECT RaName,ReqDate,(case 
	                              when r.RequestType='T' then N'طلب مكافأة نشر' 
                                  end) type,r.autoid RequestId,FacultyReqNo,GSInNo,GSOutNo,RequestFinalStatus
                                  FROM ResearchRewardForm r, Faculty f,ResearcherInfo ri
                                  where r.JobId = ri.AcdId and ri.College = f.CollegeName
                                  and r.GSOutNo<>'' and RequestFinalStatus<>N'') h order by ReqDate desc
                                   ";

                            cmd = new SqlCommand(s, conn);
                            DataTable d = new DataTable();
                            d.Load(cmd.ExecuteReader());
                            int sp = 0;
                            int rew = 0;
                            for(int i=0;i<d.Rows.Count;i++)
                            {
                                if (d.Rows[i][2].ToString() == "طلب رسوم نشر")
                                    sp++;
                                else
                                    rew++;
                            }
                            lblCountSupport.Text = sp.ToString();
                            lblCountReward.Text = rew.ToString();

                        }
                        // secretary of president
                        if (priv.Rows[privId][2].ToString() == "14")
                        {
                            RectorOfficeDiv.Visible = true;
                            string s = @"SELECT *,(case 
	                              when r.RequestType='S' then N'طلب رسوم نشر' 
	                              when r.RequestType='T' then N'طلب مكافأة' 
                                  end) type,r.autoid RequestId,FacultyReqNo
                                  FROM ResearchFeeInfo r, Faculty f,ResearcherInfo ri
                                  where r.JobId = ri.AcdId and ri.College = f.CollegeName
                                  and r.GSOutNo<>'' and RequestFinalStatus<>N''";

                            SecPrint.Visible = true;
                            cmd = new SqlCommand(s, conn);
                            GridView2.DataSource = cmd.ExecuteReader();
                            GridView2.DataBind();
                        }
                    }
                }
                conn.Close();
            }
        }

        protected void lnkViewReDir_Click(object sender, EventArgs e)
        {
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            Session["ViewRequestFrom"] = row.Cells[0].Text;
            Session["PrintForm"] = "P";
            //Session["Dir_Dean_Priv"] = "Sec";
            Session["backurl"] = "SecPrintRequests.aspx";
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand("Select JobId From "+(row.Cells[3].Text.Contains("رسوم")? "ResearchFeeInfo":"ResearchRewardForm")+ " where AutoId=" + row.Cells[0].Text, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            Session["ResearchId"] = dr[0];
            conn.Close();

            if (row.Cells[3].Text.Contains("رسوم"))
                Response.Redirect("ResearchFeeForm.aspx");
            else if(row.Cells[3].Text.Contains("مكافأة"))
                Response.Redirect("ResearchRewardForm.aspx");
        }

        protected void lnkPrintCover_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            Session["backurl"] = "SecPrintRequests.aspx";
            Session["ViewRequestFrom"] = row.Cells[0].Text;
            Session["SecType"] = "Faculty";
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand("Select JobId From ResearchFeeInfo where AutoId=" + row.Cells[0].Text, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            Session["ResearchId"] = dr[0];

            cmd = new SqlCommand("Update ResearchFeeInfo Set FacultyReqNo=N'" + (row.Cells[3].FindControl("txtFacultyReqNo") as TextBox).Text + "' where AutoId=" + row.Cells[0].Text, conn);
            cmd.ExecuteNonQuery();

            cmd = new SqlCommand("Select * From Priviliges where PrivType=3 and PrivFacultyId=10", conn);
            SqlDataReader drSecGS = cmd.ExecuteReader();
            drSecGS.Read();

            System.Text.StringBuilder msg = new System.Text.StringBuilder();
            msg.Clear();
            msg.Append("<body dir='rtl'><b>المساعد الاداري/ عمادة الدراسات العليا والبحث العلمي المحترم،</b>");
            msg.Append("<br>الرجاء السير بإجراءات طلب دعم رسوم نشر بحث علمي لطلب الباحث " + row.Cells[1].Text);
            msg.Append(" <a href='http://meusr-ra.meu.edu.jo/' target='_blank'>بالدخول إلى موقع البحث العلمي </a> ");
            cmd = new SqlCommand("SELECT * FROM Priviliges p,faculty f where p.PrivFacultyId=f.AutoId and p.PrivTo=" + Session["secid"], conn);
            SqlDataReader dr1 = cmd.ExecuteReader();
            dr1.Read();
            msg.Append("<br><br><b>كلية " + dr1["CollegeName"] + "</b>");
            sendEmail(drSecGS[5].ToString(), msg.ToString());


            Response.Redirect("CoverPage.aspx");
        }

        protected void sendEmail(string email, string msg)
        {
            //send email

            var smtp = new SmtpClient
            {
                Host = "smtp.office365.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("Researchoffice@meu.edu.jo", "Qad11204")
            };
            using (var message = new MailMessage("Researchoffice@meu.edu.jo", email)
            {
                IsBodyHtml = true,
                Subject = "تجميع ملفات الباحثين",
                Body = msg// "تم استقبال طلبك بنجاح  " + "<a href=http://meusr-ra.meu.edu.jo/'>اضغط هنا لمتابعة الطلب</a>"

            })
            {
                //message.Attachments.Add(new Attachment(""));
                smtp.Send(message);
            }

        }

        protected void lnkPrintCoverRe_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            Session["backurl"] = "SecPrintRequests.aspx";
            Session["ViewRequestFrom"] = row.Cells[0].Text;
            Session["SecType"] = "GS";
            Session["FormTypePrint"] = row.Cells[3].Text;
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand("Select JobId From " + (row.Cells[3].Text.Contains("رسوم") ? "ResearchFeeInfo" : "ResearchRewardForm") + "  where AutoId=" + row.Cells[0].Text, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            Session["ResearchId"] = dr[0];

            //cmd = new SqlCommand("Update ResearchFeeInfo Set GSInNo=N'" + (row.Cells[3].FindControl("txtGSOutNo") as TextBox).Text + "' where AutoId=" + row.Cells[0].Text, conn);
            //cmd.ExecuteNonQuery();

            //cmd = new SqlCommand("Select * From Priviliges where PrivType=2 and PrivFacultyId=11", conn);
            //SqlDataReader drSecGS = cmd.ExecuteReader();
            //drSecGS.Read();


            //System.Text.StringBuilder msg = new System.Text.StringBuilder();
            //msg.Clear();
            //msg.Append("<body dir='rtl'><b>رئيس قسم البحث العلمي المحترم،</b>");
            //msg.Append("<br>الرجاء دراسة طلب دعم رسوم نشر بحث علمي لطلب الباحث " + row.Cells[1].Text);
            //msg.Append(" <a href='http://meusr-ra.meu.edu.jo/' target='_blank'>بالدخول إلى موقع البحث العلمي </a> ");
            //msg.Append("<br><br><b>المساعد الإداري-عمادة الدراسات العليا والبحث العلمي</b>");
            //sendEmail(drSecGS[5].ToString(), msg.ToString());


            Response.Redirect("CoverPage.aspx");
        }

        protected void lnkGenerateForm_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent.Parent;
            Session["ViewRequestFrom"] = row.Cells[0].Text;
            Session["NotDefault"] = "SecPrintRequests.aspx";
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand("Select JobId From " + (row.Cells[3].Text.Contains("رسوم") ? "ResearchFeeInfo" : "ResearchRewardForm") + " where AutoId=" + row.Cells[0].Text, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            Session["ResearchId"] = dr[0];
            Session["FormType"] = row.Cells[3].Text;
            cmd = new SqlCommand("Update " + (row.Cells[3].Text.Contains("رسوم") ? "ResearchFeeInfo" : "ResearchRewardForm") + " Set GSOutNo=N'" + (row.Cells[3].FindControl("txtGSInNo") as TextBox).Text + "' where AutoId=" + row.Cells[0].Text, conn);
            cmd.ExecuteNonQuery();

            conn.Close();

            System.Text.StringBuilder msg = new System.Text.StringBuilder();

            msg.Clear();
            msg.Append("<body dir='rtl'><b>رئيس قسم البحث العلمي المحترم،</b>");

            msg.Append("<br>الرجاء تجميع ملفات " + (row.Cells[3].Text) + " بحث علمي لطلب الباحث " + row.Cells[1].Text + " رقم " + row.Cells[0].Text);
            msg.Append(" <a href='http://meusr-ra.meu.edu.jo/' target='_blank'>بالدخول إلى موقع البحث العلمي </a> ");
            msg.Append("<br><br><b>عميد الدراسات العليا والبحث العلمي</b>");
            sendEmail("Atarawneh@meu.edu.jo", msg.ToString());

            //Response.Redirect("CoverPageReDept.aspx");
            Response.Redirect("SecPrintRequests.aspx");
        }

        protected void lnkGenerateDecision_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            Session["ViewRequestFrom"] = row.Cells[0].Text;
            Session["backurl"] = "SecPrintRequests.aspx";
            Session["FormTypePrint"] = row.Cells[3].Text;
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand("Select JobId From " + (row.Cells[3].Text.Contains("رسوم") ? "ResearchFeeInfo" : "ResearchRewardForm") + "  where AutoId=" + row.Cells[0].Text, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            Session["ResearchId"] = dr[0];
            conn.Close();


            Response.Redirect("CoverPageRectorOffice.aspx");
        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (GridView1.Rows[i].Cells[5].Text.Contains("مكتمل"))
                {
                    HtmlGenericControl divh = (HtmlGenericControl)GridView1.Rows[i].Cells[5].FindControl("gsSaderDiv");
                    divh.Visible = true;

                    if ((GridView1.Rows[i].FindControl("txtGSInNo") as TextBox).Text != "")
                        (GridView1.Rows[i].FindControl("txtGSInNo") as TextBox).ReadOnly = true;
                }
            }
        }

        protected void lnkGenerateFormRector_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            Session["ViewRequestFrom"] = row.Cells[0].Text;
            Session["backurl"] = "SecPrintRequests.aspx";
            Session["FormTypePrint"] = row.Cells[3].Text;
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand("Select JobId From " + (row.Cells[3].Text.Contains("رسوم") ? "ResearchFeeInfo" : "ResearchRewardForm") + "  where AutoId=" + row.Cells[0].Text, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            Session["ResearchId"] = dr[0];

            conn.Close();


            Response.Redirect("CoverPageReDept.aspx");
        }

        protected void btnDone_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand cmd;
            for (int i=0;i<GridView2.Rows.Count;i++)
            {
                CheckBox chk = (CheckBox)GridView2.Rows[i].FindControl("chkDone");
                if(chk.Checked)
                {
                    if (GridView2.Rows[i].Cells[3].Text == "طلب رسوم نشر")
                        cmd = new SqlCommand("Update ResearchFeeInfo set Done=1 where AutoId="+GridView2.Rows[i].Cells[0].Text, conn);
                    else
                        cmd = new SqlCommand("Update ResearchRewardForm set Done=1 where AutoId=" + GridView2.Rows[i].Cells[0].Text, conn);

                    cmd.ExecuteNonQuery();
                }

            }

            string s = @"select * from (SELECT RaName,ReqDate,(case 
	                              when r.RequestType='S' then N'طلب رسوم نشر' 
                                  end) type,r.autoid RequestId,FacultyReqNo,GSInNo,GSOutNo,RequestFinalStatus
                                  FROM ResearchFeeInfo r, Faculty f,ResearcherInfo ri
                                  where r.JobId = ri.AcdId and ri.College = f.CollegeName
                                  and r.GSOutNo<>'' and RequestFinalStatus<>N'' and done=0
                                   ";
            s += " union all ";
            s += @"SELECT RaName,ReqDate,(case 
	                              when r.RequestType='T' then N'طلب مكافأة نشر' 
                                  end) type,r.autoid RequestId,FacultyReqNo,GSInNo,GSOutNo,RequestFinalStatus
                                  FROM ResearchRewardForm r, Faculty f,ResearcherInfo ri
                                  where r.JobId = ri.AcdId and ri.College = f.CollegeName
                                  and r.GSOutNo<>'' and RequestFinalStatus<>N'' and done=0)
                                   h order by ReqDate desc";


            cmd = new SqlCommand(s, conn);
            GridView2.DataSource = cmd.ExecuteReader();
            GridView2.DataBind();

            s = @"select * from (SELECT RaName,ReqDate,(case 
	                              when r.RequestType='S' then N'طلب رسوم نشر' 
                                  end) type,r.autoid RequestId,FacultyReqNo,GSInNo,GSOutNo,RequestFinalStatus
                                  FROM ResearchFeeInfo r, Faculty f,ResearcherInfo ri
                                  where r.JobId = ri.AcdId and ri.College = f.CollegeName
                                  and r.GSOutNo<>'' and RequestFinalStatus<>N'' and done=1
                                   ";
            s += " union all ";
            s += @"SELECT RaName,ReqDate,(case 
	                              when r.RequestType='T' then N'طلب مكافأة نشر' 
                                  end) type,r.autoid RequestId,FacultyReqNo,GSInNo,GSOutNo,RequestFinalStatus
                                  FROM ResearchRewardForm r, Faculty f,ResearcherInfo ri
                                  where r.JobId = ri.AcdId and ri.College = f.CollegeName
                                  and r.GSOutNo<>'' and RequestFinalStatus<>N'' and done=1) h order by ReqDate desc
                                   ";

            cmd = new SqlCommand(s, conn);
            GridView3.DataSource = cmd.ExecuteReader();
            GridView3.DataBind();


            conn.Close();
        }

        protected void lnkSaveAll_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            string c = "<table><tr><td>رقم الطلب</td><td>نوع الطلب</td><td>اسم الباحث</td></tr>";
            for (int i = 0; i < GridView1.Rows.Count; i++)
                if (((CheckBox)(GridView1.Rows[i].FindControl("chkOk"))).Checked)
                {
                    SqlCommand cmd = new SqlCommand("Update " + (GridView1.Rows[i].Cells[3].Text.Contains("رسوم") ? "ResearchFeeInfo" : "ResearchRewardForm") + " Set GSOutNo=N'ع د ع / د / 8 / " + (GridView1.Rows[i].Cells[3].FindControl("txtGSInNo") as TextBox).Text + "' where AutoId=" + GridView1.Rows[i].Cells[0].Text, conn);
                    cmd.ExecuteNonQuery();
                    c +="<tr><td>"+ GridView1.Rows[i].Cells[0].Text + "</td><td>"+ GridView1.Rows[i].Cells[3].Text+ "</td><td>" + GridView1.Rows[i].Cells[1].Text + "</td></tr>";
                }
            c += "</table>";

            conn.Close();
            if (c != "<table><tr><td>رقم الطلب</td><td>نوع الطلب</td><td>اسم الباحث</td></tr></table>")
            {
                System.Text.StringBuilder msg = new System.Text.StringBuilder();

                msg.Clear();
                msg.Append("<body dir='rtl'><b>رئيس قسم البحث العلمي المحترم،</b>");

                msg.Append("<br>الرجاء تجميع ملفات الباحثين<br>" + (c));
                msg.Append(" <a href='http://meusr-ra.meu.edu.jo/' target='_blank'>بالدخول إلى موقع البحث العلمي </a> ");
                msg.Append("<br><br><b>عميد الدراسات العليا والبحث العلمي</b>");
                sendEmail("Atarawneh@meu.edu.jo", msg.ToString());

                //Response.Redirect("CoverPageReDept.aspx");
                Response.Redirect("SecPrintRequests.aspx");
            }
        }

        protected void lnkView_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent.Parent;
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand cmd = new SqlCommand("Select JobId,RequestFinalStatus From " + (row.Cells[3].Text.Contains("رسوم") ? "ResearchFeeInfo" : "ResearchRewardForm") + " where AutoId=" + row.Cells[0].Text, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            Session["ResearchId"] = dr[0].ToString();
            string c = dr[0].ToString();
            Session["ViewRequestFrom"] = row.Cells[0].Text;
            Session["RequestComeFrom"] = row.Cells[0].Text;
            Session["FinalStatus"] = dr[1].ToString();
            Session["NotDefault"] = "SecPrintRequests.aspx";
            if (row.Cells[3].Text.Contains("رسوم"))
                Response.Redirect("ResearchFeeForm.aspx");
            else if (row.Cells[3].Text.Contains("مكافأة"))
                Response.Redirect("ResearchRewardForm.aspx");

        }
    }
}