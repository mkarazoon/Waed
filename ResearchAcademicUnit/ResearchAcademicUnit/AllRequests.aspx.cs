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
    public partial class AllRequests : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["ResearchId"] = Session["uid"];
            if (Session["userid"] == null || Session.IsNewSession)
            {
                Response.Redirect("Login.aspx");
            }
            Session["backurl"] = "Default.aspx";
            if (!IsPostBack)
            {
                Session["SelectedCollege"] = 0;
                Session["SelectedType"] = 0;
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                string sql = @"select * from (SELECT distinct r.[AutoId] ReqId
                          ,[JobId]
                          ,(select RaName from ResearcherInfo where acdid=jobid) RaName
                          ,(select College from ResearcherInfo where acdid=jobid) College
                          ,[ReqDate],RequestType,RequestFinalStatus
	                      ,(select ReqStatus from RequestsFollowUp rf where r.AutoId=rf.RequestId and type=0 and rf.AutoId =(select MAX(autoid) from RequestsFollowUp rr where rr.RequestId=r.AutoId and type=0)) rs
                      FROM ResearchFeeInfo r where RequestType='S'";
                sql += " union all ";
                sql += @"SELECT distinct r.[AutoId] ReqId
                          ,[JobId]
                          ,(select RaName from ResearcherInfo where acdid=jobid) RaName
                          ,(select College from ResearcherInfo where acdid=jobid) College
                          ,[ReqDate],RequestType,RequestFinalStatus
	                      ,(select ReqStatus from RequestsFollowUp rf where r.AutoId=rf.RequestId and type=1 and rf.AutoId =(select MAX(autoid) from RequestsFollowUp rr where rr.RequestId=r.AutoId and type=1)) rs
                      FROM ResearchRewardForm r  where RequestType='T') h
                        order by ReqDate Desc";

                SqlCommand cmd = new SqlCommand(sql, conn);
                GridView1.DataSource = cmd.ExecuteReader();
                GridView1.DataBind();

                conn.Close();
            }

        }

        protected void lnkFollowUp_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            int reqid = Convert.ToInt32(row.Cells[0].Text);
            int type = ((Label)row.FindControl("Label1")).Text.Contains("رسوم") ? 0 : 1;
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            FollowUpDiv.Visible = true;
            SqlCommand cmd = new SqlCommand("select * from RequestsFollowUp where requestid=" + reqid + " and type=" + type + " order by Autoid", conn);
            GridView2.DataSource = cmd.ExecuteReader();
            GridView2.DataBind();

            conn.Close();
        }

        protected void lnkShow_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            Session["ViewRequestFrom"] = row.Cells[0].Text;
            Session["Dir_Dean_Priv"] = null;
            Session["justView"] = "ok";
            Session["NotDefault"] = "AllRequests.aspx";
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand("Select JobId From " + (((Label)row.FindControl("Label1")).Text.Contains("رسوم") ? "ResearchFeeInfo" : "ResearchRewardForm") + " where AutoId=" + row.Cells[0].Text, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            Session["ResearchId"] = dr[0];
            conn.Close();

            if (((Label)row.FindControl("Label1")).Text.Contains("رسوم"))
                Response.Redirect("ResearchFeeForm.aspx");
            else if (((Label)row.FindControl("Label1")).Text.Contains("مكافأة"))
                Response.Redirect("ResearchRewardForm.aspx");
        }

        protected void lnkClose_Click(object sender, EventArgs e)
        {
            FollowUpDiv.Visible = false;
        }

        protected void searchBox_TextChanged(object sender, EventArgs e)
        {

            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            string sql = @"select * from (SELECT distinct r.[AutoId] ReqId
                          ,[JobId]
                          ,(select RaName from ResearcherInfo where acdid=jobid) RaName
                          ,(select College from ResearcherInfo where acdid=jobid) College
                          ,[ReqDate],RequestType,RequestFinalStatus
	                      ,(select ReqStatus from RequestsFollowUp rf where r.AutoId=rf.RequestId and type=0 and rf.AutoId =(select MAX(autoid) from RequestsFollowUp rr where rr.RequestId=r.AutoId and type=0)) rs
                      FROM ResearchFeeInfo r";
            sql += " union all ";
            sql += @"SELECT distinct r.[AutoId] ReqId
                          ,[JobId]
                          ,(select RaName from ResearcherInfo where acdid=jobid) RaName
                          ,(select College from ResearcherInfo where acdid=jobid) College
                          ,[ReqDate],RequestType,RequestFinalStatus
	                      ,(select ReqStatus from RequestsFollowUp rf where r.AutoId=rf.RequestId and type=1 and rf.AutoId =(select MAX(autoid) from RequestsFollowUp rr where rr.RequestId=r.AutoId and type=1)) rs
                      FROM ResearchRewardForm r) h
                        where raname like N'%"+searchBox.Text+ @"%'
                        order by ReqDate Desc";

            SqlCommand cmd = new SqlCommand(sql, conn);
            GridView1.DataSource = cmd.ExecuteReader();
            GridView1.DataBind();

            conn.Close();

        }


        protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DropDownList ddl = (DropDownList)GridView1.HeaderRow.FindControl("ddlFaculty");

            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            string sql = @"select * from (SELECT distinct r.[AutoId] ReqId
                          ,[JobId]
                          ,(select RaName from ResearcherInfo where acdid=jobid) RaName
                          ,(select College from ResearcherInfo where acdid=jobid) College
                          ,[ReqDate],RequestType,RequestFinalStatus
	                      ,(select ReqStatus from RequestsFollowUp rf where r.AutoId=rf.RequestId and type=0 and rf.AutoId =(select MAX(autoid) from RequestsFollowUp rr where rr.RequestId=r.AutoId and type=0)) rs
                      FROM ResearchFeeInfo r";
            sql += " union all ";
            sql += @"SELECT distinct r.[AutoId] ReqId
                          ,[JobId]
                          ,(select RaName from ResearcherInfo where acdid=jobid) RaName
                          ,(select College from ResearcherInfo where acdid=jobid) College
                          ,[ReqDate],RequestType,RequestFinalStatus
	                      ,(select ReqStatus from RequestsFollowUp rf where r.AutoId=rf.RequestId and type=1 and rf.AutoId =(select MAX(autoid) from RequestsFollowUp rr where rr.RequestId=r.AutoId and type=1)) rs
                      FROM ResearchRewardForm r) h
                         " + cond() + " order by ReqDate Desc";

            SqlCommand cmd = new SqlCommand(sql, conn);
            GridView1.DataSource = cmd.ExecuteReader();
            GridView1.DataBind();
            conn.Close();
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DropDownList ddl = (DropDownList)GridView1.HeaderRow.FindControl("ddlType");

            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            string sql = @"select * from (SELECT distinct r.[AutoId] ReqId
                          ,[JobId]
                          ,(select RaName from ResearcherInfo where acdid=jobid) RaName
                          ,(select College from ResearcherInfo where acdid=jobid) College
                          ,[ReqDate],RequestType,RequestFinalStatus
	                      ,(select ReqStatus from RequestsFollowUp rf where r.AutoId=rf.RequestId and type=0 and rf.AutoId =(select MAX(autoid) from RequestsFollowUp rr where rr.RequestId=r.AutoId and type=0)) rs
                      FROM ResearchFeeInfo r";
            sql += " union all ";
            sql += @"SELECT distinct r.[AutoId] ReqId
                          ,[JobId]
                          ,(select RaName from ResearcherInfo where acdid=jobid) RaName
                          ,(select College from ResearcherInfo where acdid=jobid) College
                          ,[ReqDate],RequestType,RequestFinalStatus
	                      ,(select ReqStatus from RequestsFollowUp rf where r.AutoId=rf.RequestId and type=1 and rf.AutoId =(select MAX(autoid) from RequestsFollowUp rr where rr.RequestId=r.AutoId and type=1)) rs
                      FROM ResearchRewardForm r) h
                         " + cond() + " order by ReqDate Desc";

            SqlCommand cmd = new SqlCommand(sql, conn);
            GridView1.DataSource = cmd.ExecuteReader();
            GridView1.DataBind();

            conn.Close();
        }

        protected string cond()
        {
            string c = "";
            if (ddlFaculty.SelectedValue == "1" && ddlType.SelectedValue == "1")
                c= "";
            else if (ddlFaculty.SelectedValue != "1" && ddlType.SelectedValue != "1")
                c= " where college like N'%" + ddlFaculty.SelectedValue + "%' and " + (ddlType.SelectedValue == "دعم" ? " (RequestType ='S' or RequestType ='NS') " : " (RequestType ='T' or RequestType ='NT') ");
            else if (ddlFaculty.SelectedValue != "1" && ddlType.SelectedValue == "1")
                c = " where college like N'%" + ddlFaculty.SelectedValue + "%' ";
            else if (ddlFaculty.SelectedValue == "1" && ddlType.SelectedValue != "1")
                c = " where " + (ddlType.SelectedValue == "دعم" ? " (RequestType ='S' or RequestType ='NS') " : " (RequestType ='T' or RequestType ='NT') ");

            return c;
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            //string sql = @"select * from (SELECT distinct r.[AutoId] ReqId
            //              ,[JobId]
            //              ,(select RaName from ResearcherInfo where acdid=jobid) RaName
            //              ,(select College from ResearcherInfo where acdid=jobid) College
            //              ,[ReqDate],RequestType,RequestFinalStatus
            //           ,(select ReqStatus from RequestsFollowUp rf where r.AutoId=rf.RequestId and type=0 and rf.AutoId =(select MAX(autoid) from RequestsFollowUp rr where rr.RequestId=r.AutoId and type=0)) rs
            //          FROM ResearchFeeInfo r";
            //sql += " union all ";
            //sql += @"SELECT distinct r.[AutoId] ReqId
            //              ,[JobId]
            //              ,(select RaName from ResearcherInfo where acdid=jobid) RaName
            //              ,(select College from ResearcherInfo where acdid=jobid) College
            //              ,[ReqDate],RequestType,RequestFinalStatus
            //           ,(select ReqStatus from RequestsFollowUp rf where r.AutoId=rf.RequestId and type=1 and rf.AutoId =(select MAX(autoid) from RequestsFollowUp rr where rr.RequestId=r.AutoId and type=1)) rs
            //          FROM ResearchRewardForm r) h "+(ddlStatus.SelectedValue=="1"?"": "where RequestFinalStatus like N'%" + ddlStatus.SelectedItem.Text + @"%'")+@" order by ReqDate Desc";

            string sql = @"select * from (SELECT distinct r.[AutoId] ReqId
                          ,[JobId]
                          ,(select RaName from ResearcherInfo where acdid=jobid) RaName
                          ,(select College from ResearcherInfo where acdid=jobid) College
                          ,[ReqDate],RequestType,RequestFinalStatus
	                      ,(select ReqStatus from RequestsFollowUp rf where r.AutoId=rf.RequestId and type=0 and rf.AutoId =(select MAX(autoid) from RequestsFollowUp rr where rr.RequestId=r.AutoId and type=0)) rs
                      FROM ResearchFeeInfo r where RequestType='S'";
            sql += " union all ";
            sql += @"SELECT distinct r.[AutoId] ReqId
                          ,[JobId]
                          ,(select RaName from ResearcherInfo where acdid=jobid) RaName
                          ,(select College from ResearcherInfo where acdid=jobid) College
                          ,[ReqDate],RequestType,RequestFinalStatus
	                      ,(select ReqStatus from RequestsFollowUp rf where r.AutoId=rf.RequestId and type=1 and rf.AutoId =(select MAX(autoid) from RequestsFollowUp rr where rr.RequestId=r.AutoId and type=1)) rs
                      FROM ResearchRewardForm r  where RequestType='T') h "+(ddlStatus.SelectedValue=="1"?"": "where RequestFinalStatus like N'%" + ddlStatus.SelectedItem.Text + @"%'")+@" order by ReqDate Desc";
                        //order by ReqDate Desc";


            SqlCommand cmd = new SqlCommand(sql, conn);
            GridView1.DataSource = cmd.ExecuteReader();
            GridView1.DataBind();

            conn.Close();
        }
    }
}