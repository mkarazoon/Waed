using Oracle.ManagedDataAccess.Client;
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
    public partial class HomePage : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);
        static string OracleConnString = System.Configuration.ConfigurationManager.ConnectionStrings["orcleConStr"].ConnectionString;
        OracleConnection conn1 = new OracleConnection(OracleConnString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userid"] == null || Session.IsNewSession)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                // cmd = new SqlCommand("Select isnull(count(FormId),1)+1 From FormsInfo", conn);
                //SqlDataReader dr = cmd.ExecuteReader();
                //dr.Read();
                //lblFormId.Text = dr[0].ToString();


                SqlCommand cmd = new SqlCommand("select * from FormsInfo where FromHeadEnter=0 and ClosedAt>=@d", conn);
                cmd.Parameters.AddWithValue("@d", DateTime.Now.Date);
                grdFormInfo.DataSource = cmd.ExecuteReader();
                grdFormInfo.DataBind();

                string Newsql = "";
                string sqlTables = "Select * From FormsInfo where OriginalTable is not null";
                DataTable dtTables = new DataTable();
                SqlCommand cmdTables = new SqlCommand(sqlTables, conn);
                dtTables.Load(cmdTables.ExecuteReader());
                for (int i = 0; i < dtTables.Rows.Count; i++)
                {
                    Newsql += @"SELECT distinct b.FormId,b.FormAName,c.AutoId,c.UserId,InsertedDate SentDate,c.Status,FormLink,OriginalTable,a.ReqToId,a.ReqStatus
                                  FROM RequestsFollowUp a, FormsInfo b," + dtTables.Rows[i]["OriginalTable"] + @" c
                                  where a.type=b.FormId and a.RequestId=c.AutoId and b.FormId=c.FormId";
                    Newsql += " union all ";
                }
                if (Newsql.Length != 0)
                {
                    Newsql = Newsql.Substring(0, Newsql.Length - 11);
                    Newsql = "select * from (" + Newsql + ") AllData where (AllData.ReqStatus = N'قيد الانتظار' or AllData.ReqStatus = N'مكتمل') and AllData.UserId = " + Session["uid"];
                }


                cmd = new SqlCommand(Newsql, conn);
                GridView1.DataSource = cmd.ExecuteReader();
                GridView1.DataBind();


                //string sql = @"select * From meu_new.all_students_karz where ""اخر حالة للطالب""='منتظم' and ""الدرجة""='الماجستير'";
                //OracleCommand cmdOrc = new OracleCommand(sql, conn1);
                //OracleDataAdapter da = new OracleDataAdapter(cmdOrc);
                //DataTable dtTest = new DataTable();
                //da.Fill(dtTest);
                //GridView3.DataSource = dtTest;
                //GridView3.DataBind();

                conn.Close();
            }

        }

        protected void lnkNew_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            Session["backurl"] = HttpContext.Current.Request.Url.AbsoluteUri;

            string sql = @"select * From meu_new.all_students_karz where student_id=" + Session["userid"].ToString();
            OracleCommand cmdOrc = new OracleCommand(sql, conn1);
            OracleDataAdapter da = new OracleDataAdapter(cmdOrc);
            DataTable dtTest = new DataTable();
            da.Fill(dtTest);
            //if (dtTest.Rows[0][28].ToString().Trim() == "شامل")
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('هذا النموذج خاص بطلاب الرسائل فقط','error');", true);
            //else
            //if (dtTest.Rows[0][23].ToString()!="" &&  Convert.ToDouble(dtTest.Rows[0][23].ToString()) < 3)
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('معدلك التراكمي أقل من 3، لايمكنك التقدم لهذا الطلب','error');", true);
            ////else if (dtTest.Rows[0][31].ToString().Trim() != "" || dtTest.Rows[0][32].ToString().Trim() != "" || dtTest.Rows[0][33].ToString().Trim() != "")
            ////    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('تم تحديد مشرف مسبقا','error');", true);
            //else
                Response.Redirect(row.Cells[8].Text);
        }

        protected void lnkViewInst_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            exampleModalLabel.InnerText = "تعليمات نموذج - " + row.Cells[2].Text;
            txtInstructionView.Text = row.Cells[7].Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() { LoginFail(); });", true);

        }

        protected void grdFormInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                switch (e.Row.Cells[1].Text)
                {
                    case "0":
                        e.Row.Cells[1].Text = "البحث العلمي";
                        break;
                    case "1":
                        e.Row.Cells[1].Text = "الدراسات العليا";
                        break;
                }
                switch (e.Row.Cells[6].Text)
                {
                    case "0":
                        e.Row.Cells[6].Text = "طالب";
                        break;
                    case "1":
                        e.Row.Cells[6].Text = "رئيس قسم";
                        break;
                }
            }

        }

        protected void lnkTab1_Click(object sender, EventArgs e)
        {
            tab1.Visible = true;
            tab2.Visible = false;
            lnkTab1.Attributes.Add("class", "nav-link active");
            lnkTab2.Attributes.Add("class", "nav-link");
        }

        protected void lnkTab2_Click(object sender, EventArgs e)
        {
            tab1.Visible = false;
            tab2.Visible = true;
            lnkTab1.Attributes.Add("class", "nav-link");
            lnkTab2.Attributes.Add("class", "nav-link active");
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType==DataControlRowType.DataRow)
            {
                switch (e.Row.Cells[3].Text)
                {
                    case "0":
                        e.Row.Cells[3].Text = "قيد الانتظار";
                        break;
                    case "1":
                        e.Row.Cells[3].Text = "معتمد";
                        break;
                    case "2":
                        e.Row.Cells[3].Text = "يحتاج تعديل";
                        ((LinkButton)(e.Row.FindControl("lnkUpdate"))).Visible = true;
                        break;
                }
            }
        }

        protected void lnkViewFollowup_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            FollowUpLabel.InnerText = "تتبع المعاملة - " + row.Cells[2].Text;

            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand("Select * From  RequestsFollowUp WHERE Type = "+row.Cells[0].Text+" AND RequestId = "+row.Cells[1].Text, conn);
            DataTable dtFollowUp = new DataTable();
            dtFollowUp.Load(cmd.ExecuteReader());
            GridView2.DataSource = dtFollowUp;
            GridView2.DataBind();

            conn.Close();


            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() { LoginFail1(); });", true);
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType==DataControlRowType.DataRow)
            {
                //switch(e.Row.Cells[3].Text)
                //{
                //    case "0":
                //        e.Row.Cells[3].Text = "قيد الانتظار";
                //        break;
                //    case "1":
                //        e.Row.Cells[3].Text = "معتمد";
                //        break;
                //    case "2":
                //        e.Row.Cells[3].Text = "يحتاج تعديل";
                        
                //        break;
                //}
            }
        }

        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            Session["backurl"] = HttpContext.Current.Request.Url.AbsoluteUri;

            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand("Select * From " + row.Cells[5].Text + " where AutoId=" + row.Cells[1].Text, conn);
            DataTable dtData = new DataTable();
            dtData.Load(cmd.ExecuteReader());
            Session["dtData"] = dtData;
            Session["OriginalSenderId"] = Session["userid"];
            Session["OriginalSenderName"] = Session["userName"];
            Session["UpdatedRecord"] = row.Cells[1].Text;
            Session["RequestedReport"] = row.Cells[1].Text;
            conn.Close();


            //switch (row.Cells[0].Text)
            //{
            //    case "":
            //        break;
            //}
            Response.Redirect(row.Cells[4].Text);

        }
    }
}