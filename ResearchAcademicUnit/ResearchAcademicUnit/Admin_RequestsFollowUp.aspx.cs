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
    public partial class Admin_RequestsFollowUp : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.IsNewSession || Session["uid"] == null)
                Response.Redirect("Login.aspx");
            
            if (!IsPostBack)
            {
                Session["PrintForm"] = null;
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();
                string Newsql="";
                string sqlTables = "Select * From FormsInfo where OriginalTable is not null";
                DataTable dtTables = new DataTable();
                SqlCommand cmdTables = new SqlCommand(sqlTables, conn);
                dtTables.Load(cmdTables.ExecuteReader());
                for (int i = 0; i < dtTables.Rows.Count; i++)
                {
                    Newsql += @"SELECT distinct b.FormId,b.FormAName,c.AutoId,c.UserId,InsertedDate SentDate,c.Status,FormLink,OriginalTable,a.ReqToId,a.ReqStatus,c.SupName
                                  FROM RequestsFollowUp a, FormsInfo b," + dtTables.Rows[i]["OriginalTable"] + @" c
                                  where a.type=b.FormId and a.RequestId=c.AutoId and b.FormId=c.FormId";
                    Newsql += " union all ";
                }
                if (Newsql.Length != 0)
                {
                    Newsql = Newsql.Substring(0, Newsql.Length - 11);
                    Newsql = "select * from (" + Newsql + ") AllData where AllData.ReqStatus = N'قيد الانتظار' and " +
                        "(AllData.ReqToId in (select PrivNo From Com_Priviliges where PrivTo = " + Session["uid"] + ") or AllData.ReqToId=" + Session["uid"] + ")";
                }


                SqlCommand cmd = new SqlCommand(Newsql, conn);
                grdCurrentRequest.DataSource = cmd.ExecuteReader();
                grdCurrentRequest.DataBind();

                Newsql = "";
                sqlTables = "Select * From FormsInfo where OriginalTable is not null";
                dtTables = new DataTable();
                cmdTables = new SqlCommand(sqlTables, conn);
                dtTables.Load(cmdTables.ExecuteReader());
                for (int i = 0; i < dtTables.Rows.Count; i++)
                {
                    Newsql += @"SELECT distinct b.FormId,b.FormAName,c.AutoId,c.UserId,InsertedDate SentDate,c.Status,FormLink,OriginalTable,a.ReqToId,a.ReqStatus,c.SupName
                                  FROM RequestsFollowUp a, FormsInfo b," + dtTables.Rows[i]["OriginalTable"] + @" c
                                  where a.type=b.FormId and a.RequestId=c.AutoId and b.FormId=c.FormId";
                    Newsql += " union all ";
                }
                if (Newsql.Length != 0)
                {
                    Newsql = Newsql.Substring(0, Newsql.Length - 11);
                    Newsql = "select * from (" + Newsql + ") AllData where (AllData.ReqStatus = N'قيد الانتظار' or AllData.ReqStatus = N'انجزت') and AllData.ReqToId in (select PrivNo From Com_Priviliges where PrivTo = " + Session["uid"] + ")";
                }


                cmd = new SqlCommand(Newsql, conn);
                GridView1.DataSource = cmd.ExecuteReader();
                GridView1.DataBind();


                conn.Close();
            }
        }

        protected void grdCurrentRequest_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType==DataControlRowType.DataRow)
            {
                switch(e.Row.Cells[7].Text)
                {
                    case "0":
                        e.Row.Cells[8].Text = "قيد الدراسة";
                        break;
                    case "1":
                        e.Row.Cells[8].Text = "منتهي";
                        break;
                    case "2":
                        e.Row.Cells[8].Text = "";
                        break;
                    case "3":
                        e.Row.Cells[8].Text="";
                        break;

                }
            }
        }

        protected void lnkFollow_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            string id = (sender as Control).ID;
            string type;
            if(id== "lnkViewFollowup")
            {
                type = row.Cells[1].Text;
            }
            else
                type = row.Cells[2].Text;
            FollowUpLabel.InnerText = "تتبع المعاملة - " + type;// row.Cells[2].Text;

            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand("Select * From  RequestsFollowUp WHERE Type = " + row.Cells[0].Text + " AND RequestId = " + type, conn);// + row.Cells[2].Text
            DataTable dtFollowUp = new DataTable();
            dtFollowUp.Load(cmd.ExecuteReader());
            GridView2.DataSource = dtFollowUp;
            GridView2.DataBind();

            conn.Close();



            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() { LoginFail1(); });", true);
        }

        protected void lnkViewForm_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            Session["backurl"] = HttpContext.Current.Request.Url.AbsoluteUri;

            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand("Select * From " + row.Cells[10].Text + " where AutoId=" + row.Cells[2].Text, conn);
            DataTable dtData = new DataTable();
            dtData.Load(cmd.ExecuteReader());
            Session["dtData"] = dtData;
            Session["OriginalSenderId"]= row.Cells[4].Text;
            Session["OriginalSenderName"] = row.Cells[5].Text;
            Session["UpdatedRecord"] = row.Cells[11].Text;
            Session["RequestedReport"] = row.Cells[2].Text;
            conn.Close();


            //switch (row.Cells[0].Text)
            //{
            //    case "":
            //        break;
            //}
            Response.Redirect(row.Cells[9].Text);
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
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkViewFormToPrint = e.Row.FindControl("lnkViewFormToPrint") as LinkButton;
                LinkButton lnkViewDecision = e.Row.FindControl("lnkViewDecision") as LinkButton;
                switch (e.Row.Cells[5].Text)
                {
                    case "0":
                        e.Row.Cells[5].Text = "قيد الانتظار";
                        break;
                    case "1":
                        e.Row.Cells[5].Text = "معتمد";
                        lnkViewFormToPrint.Visible = true;
                        lnkViewDecision.Visible = true;
                        break;
                    case "2":
                        e.Row.Cells[5].Text = "يحتاج تعديل";
                        break;
                }
            }
        }

        protected void lnkViewDecision_Click(object sender, EventArgs e)
        {

        }

        protected void lnkViewFormToPrint_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            Session["backurl"] = HttpContext.Current.Request.Url.AbsoluteUri;

            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand("Select * From " + row.Cells[7].Text + " where AutoId=" + row.Cells[1].Text, conn);
            DataTable dtData = new DataTable();
            dtData.Load(cmd.ExecuteReader());
            Session["dtData"] = dtData;
            Session["OriginalSenderId"] = row.Cells[3].Text;
            Session["OriginalSenderName"] = row.Cells[4].Text;
            Session["PrintForm"] =true;
            Session["RequestedReport"] = row.Cells[1].Text;
            conn.Close();
            Response.Redirect(row.Cells[6].Text);
        }
    }
}