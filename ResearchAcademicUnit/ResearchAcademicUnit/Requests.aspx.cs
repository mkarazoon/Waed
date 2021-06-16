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
    public partial class Requests : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["ResearchId"] = Session["uid"];
            Session["RequestComeFrom"] = null;
            Session["ViewRequestFrom"] = null;
            Session["FinalStatus"] = null;
            if (Session.IsNewSession || Session["uid"] == null)
                Response.Redirect("Login.aspx");
            Session["backurl"] = "Default.aspx";
            if (!IsPostBack)
            {

                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                //SqlCommand cmd = new SqlCommand("Select AutoId ReqId,ReqDate,RequestType From ResearchFeeInfo where JobId=" + Session["uid"], conn);

                string sql = @"SELECT distinct r.[AutoId] ReqId
                          ,[JobId]
                          ,[ReqDate],RequestType,RequestFinalStatus
	                      ,(select ReqStatus from RequestsFollowUp rf where r.AutoId=rf.RequestId and type=0 and rf.AutoId =(select MAX(autoid) from RequestsFollowUp rr where rr.RequestId=r.AutoId and type=0)) rs
                      FROM ResearchFeeInfo r where   JobId=" + Session["uid"];
                sql += "union all ";
                sql += @"SELECT distinct r.[AutoId] ReqId
                          ,[JobId]
                          ,[ReqDate],RequestType,RequestFinalStatus
	                      ,(select ReqStatus from RequestsFollowUp rf where r.AutoId=rf.RequestId and type=1 and rf.AutoId =(select MAX(autoid) from RequestsFollowUp rr where rr.RequestId=r.AutoId and type=1)) rs
                      FROM ResearchRewardForm r where  JobId=" + Session["uid"];

                SqlCommand cmd = new SqlCommand(sql, conn);
                GridView1.DataSource = cmd.ExecuteReader();
                GridView1.DataBind();
                
                cmd = new SqlCommand("Select * From Priviliges where PrivType=2 and PrivDeptId<>0 and PrivTo=" + Session["uid"], conn);
                DataTable priv = new DataTable();
                priv.Load(cmd.ExecuteReader());
                if(priv.Rows.Count!=0)
                {
                    string output="";
                    for (int i = 0; i < priv.Rows.Count; i++)
                    {
                        output = output + priv.Rows[i]["AutoId"].ToString();
                        output += (i < priv.Rows.Count-1) ? "," : string.Empty;
                    }

                    DirectorDiv.Visible = true;
                    sql = @"SELECT rfu.RequestId,ReqFromName,ReqDate,RequestDate SentDate,
                                  (case 
	                              when rfi.RequestType='S' then N'طلب رسوم نشر' 
                                  end) type,ReqStatus,rfu.AutoId
                                  FROM RequestsFollowUp rfu,ResearchFeeInfo rfi
                                  where rfu.RequestId=rfi.AutoId and rfu.type=0 and reqtoid in (" + output + ") and rfu.ReqStatus=N'قيد التنفيذ'";
                    sql += " union all ";
                    sql += @"SELECT rfu.RequestId,ReqFromName,ReqDate,RequestDate SentDate,
                                  (case 
	                              when rfi.RequestType='T' then N'طلب مكافأة نشر' 
                                  end) type,ReqStatus,rfu.AutoId
                                  FROM RequestsFollowUp rfu,ResearchRewardForm rfi
                                  where rfu.RequestId=rfi.AutoId and rfu.type=1 and reqtoid in (" + output + ") and rfu.ReqStatus=N'قيد التنفيذ'";


                    cmd = new SqlCommand(sql, conn);
                    GridView3.DataSource = cmd.ExecuteReader();
                    GridView3.DataBind();

                }

                cmd = new SqlCommand("Select * From Priviliges where PrivType=1 and (PrivFacultyId<>10 or PrivFacultyId<>14) and PrivTo=" + Session["uid"], conn);
                DataTable privD = new DataTable();
                privD.Load(cmd.ExecuteReader());
                if (privD.Rows.Count != 0)
                {
                    string output = "";
                    for (int i = 0; i < privD.Rows.Count; i++)
                    {
                        output = output + privD.Rows[i]["AutoId"].ToString();
                        output += (i < privD.Rows.Count - 1) ? "," : string.Empty;
                    }

                    DeanDiv.Visible = true;
                    sql = @"SELECT rfu.RequestId,(select ReqFromName from RequestsFollowUp rf1 where  autoid =(select min(autoid) from RequestsFollowUp rf2 where  rf2.RequestId=rf1.RequestId and rfu.RequestId=rf1.RequestId and rf2.type=0) ) [ReqFromName],ReqDate,RequestDate SentDate,
                                  (case 
	                              when rfi.RequestType='S' then N'طلب رسوم نشر' 
                                  end) type,ReqStatus,rfu.AutoId
                                  FROM RequestsFollowUp rfu,ResearchFeeInfo rfi
                                  where rfu.RequestId=rfi.AutoId and rfu.type=0 and reqtoid in (" + output + ") and rfu.ReqStatus=N'قيد التنفيذ'";
                    sql += " union all ";
                    sql += @"SELECT rfu.RequestId,(select ReqFromName 
						from RequestsFollowUp rf1 
						where  autoid =(select min(autoid) 
										from RequestsFollowUp rf2 
										where  rf2.RequestId=rf1.RequestId and rfu.RequestId=rf1.RequestId and rf2.Type=1)) [ReqFromName],ReqDate,RequestDate SentDate,
                                  (case 
	                              when rfi.RequestType='T' then N'طلب مكافأة نشر' 
                                  end) type,ReqStatus,rfu.AutoId
                                  FROM RequestsFollowUp rfu,ResearchRewardForm rfi
                                  where rfu.RequestId=rfi.AutoId and rfu.type=1 and reqtoid in (" + output + ") and rfu.ReqStatus=N'قيد التنفيذ'";
                    cmd = new SqlCommand(sql, conn);
                    GridView4.DataSource = cmd.ExecuteReader();
                    GridView4.DataBind();

                }

                cmd = new SqlCommand("Select * From Priviliges where PrivFacultyId=11 and PrivTo=" + Session["uid"], conn);
                DataTable privRDept = new DataTable();
                privRDept.Load(cmd.ExecuteReader());

                if (privRDept.Rows.Count != 0)
                {
                    RDeptDiv.Visible = true;
                    sql = @"SELECT rfu.RequestId,(select ReqFromName from RequestsFollowUp rf1 where rf1.type=0 and autoid =(select min(autoid) from RequestsFollowUp rf2 where  rf2.RequestId=rfi.AutoId and rf2.type=0) ) [ReqFromName],ReqDate,RequestDate SentDate,
                                  (case 
	                              when rfi.RequestType='S' then N'طلب رسوم نشر' 
                                  end) type,ReqStatus,rfu.AutoId
                                  FROM RequestsFollowUp rfu,ResearchFeeInfo rfi
                                  where rfu.RequestId=rfi.AutoId and rfu.type=0 and reqtoid=11 and (rfu.ReqStatus=N'قيد التنفيذ') and GSInNo<>'' and GSOutNo=''";
                    sql += " union all ";
                    sql += @"SELECT rfu.RequestId,(select ReqFromName from RequestsFollowUp rf1 where rf1.type=1 and autoid =(select min(autoid) from RequestsFollowUp rf2 where  rf2.RequestId=rfi.AutoId and rf2.type=1) ) [ReqFromName],ReqDate,RequestDate SentDate,
                                  (case 
	                              when rfi.RequestType='T' then N'طلب مكافأة نشر' 
                                  end) type,ReqStatus,rfu.AutoId
                                  FROM RequestsFollowUp rfu,ResearchRewardForm rfi
                                  where rfu.RequestId=rfi.AutoId and rfu.type=1 and reqtoid=11 and (rfu.ReqStatus=N'قيد التنفيذ') and GSInNo<>'' and GSOutNo=''";

                    cmd = new SqlCommand(sql, conn);
                    GridView6.DataSource = cmd.ExecuteReader();
                    GridView6.DataBind();

                }

                cmd = new SqlCommand("Select * From Priviliges where (PrivFacultyId=10 or PrivFacultyId=14) and PrivType=1 and PrivTo=" + Session["uid"], conn);
                DataTable privRDean = new DataTable();
                privRDean.Load(cmd.ExecuteReader());

                if (privRDean.Rows.Count != 0)
                {

                    RDeanDiv.Visible = true;
                    sql = @"SELECT rfu.RequestId,(select ReqFromName from RequestsFollowUp rf1 where rf1.type=0 and  autoid =(select min(autoid) from RequestsFollowUp rf2 where  rf2.RequestId=rfi.AutoId and rf2.type=0) ) ReqFromName,ReqDate,RequestDate SentDate,
                                  (case 
	                              when rfi.RequestType='S' then N'طلب رسوم نشر' 
                                  end) type,ReqStatus,rfu.AutoId
                                  FROM RequestsFollowUp rfu,ResearchFeeInfo rfi
                                  where rfu.RequestId=rfi.AutoId and rfu.type=0 and (reqtoid=10 or reqtoid=14) and rfu.ReqStatus=N'قيد التنفيذ'";
                    sql += " union all ";
                    sql += @"SELECT rfu.RequestId,(select ReqFromName from RequestsFollowUp rf1 where rf1.type=1 and  autoid =(select min(autoid) from RequestsFollowUp rf2 where  rf2.RequestId=rfi.AutoId and rf2.type=1) ) ReqFromName,ReqDate,RequestDate SentDate,
                                  (case 
	                              when rfi.RequestType='T' then N'طلب مكافأة نشر' 
                                  end) type,ReqStatus,rfu.AutoId
                                  FROM RequestsFollowUp rfu,ResearchRewardForm rfi
                                  where rfu.RequestId=rfi.AutoId and rfu.type=1 and (reqtoid=10 or reqtoid=14) and rfu.ReqStatus=N'قيد التنفيذ'";

                    cmd = new SqlCommand(sql, conn);
                    GridView5.DataSource = cmd.ExecuteReader();
                    GridView5.DataBind();

                }
                conn.Close();
            }
        }

        protected void lnkFollowUp_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            string gridView = (((GridViewRow)(sender as Control).Parent.Parent).ClientID.Split('_'))[1];
            int reqid = Convert.ToInt32(row.Cells[0].Text);
            int type;
            if (gridView=="GridView1")
                type = ((Label)(row.Cells[2].FindControl("Label1"))).Text.Contains("رسوم") ? 0 : 1;
            else
                type = row.Cells[4].Text.Contains("رسوم") ? 0 : 1;

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
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand cmd = new SqlCommand("Select JobId From " + (((Label)(row.Cells[2].FindControl("Label1"))).Text.Contains("دعم") ? "ResearchFeeInfo" : "ResearchRewardForm") + " where AutoId=" + row.Cells[0].Text, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            Session["ResearchId"] = dr[0].ToString();
            string c= dr[0].ToString();
            Session["ViewRequestFrom"] = row.Cells[0].Text;
            Session["RequestComeFrom"] = row.Cells[0].Text;
            Session["FinalStatus"] = row.Cells[3].Text;
            Session["NotDefault"] = "Requests.aspx";
            if (((Label)(row.Cells[2].FindControl("Label1"))).Text.Contains("دعم"))
                Response.Redirect("ResearchFeeForm.aspx");
            else if(((Label)(row.Cells[2].FindControl("Label1"))).Text.Contains("مكافأة"))
                Response.Redirect("ResearchRewardForm.aspx");
        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            //for(int i=0;i<GridView1.Rows.Count;i++)
            //    if(((Label)(GridView1.Rows[i].Cells[2].FindControl("Label1"))).Text.Contains("غير"))
            //    {
            //        GridView1.Rows[i].Cells[4].Enabled = false;
            //    }
        }

        protected void lnkView_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            Session["ViewRequestFrom"] = row.Cells[0].Text;
            Session["AutoIdUpdated"] = row.Cells[6].Text;
            Session["Dir_Dean_Priv"] = "Dir";
            Session["NotDefault"] = "Requests.aspx";
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand("Select JobId From "+(row.Cells[4].Text.Contains("رسوم")? "ResearchFeeInfo":"ResearchRewardForm") +" where AutoId=" + row.Cells[0].Text, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            Session["ResearchId"] = dr[0];
            conn.Close();

            if (row.Cells[4].Text.Contains("رسوم"))
                Response.Redirect("ResearchFeeForm.aspx");
            else if(row.Cells[4].Text.Contains("مكافأة"))
                Response.Redirect("ResearchRewardForm.aspx");
        }

        protected void lnkViewDean_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            Session["ViewRequestFrom"] = row.Cells[0].Text;
            Session["AutoIdUpdated"] = row.Cells[6].Text;
            Session["Dir_Dean_Priv"] = "Dean";
            Session["NotDefault"] = "Requests.aspx";
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand("Select JobId From " + (row.Cells[4].Text.Contains("رسوم") ? "ResearchFeeInfo" : "ResearchRewardForm") + " where AutoId=" + row.Cells[0].Text, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            Session["ResearchId"] = dr[0];
            conn.Close();


            if (row.Cells[4].Text.Contains("رسوم"))
                Response.Redirect("ResearchFeeForm.aspx");
            else if (row.Cells[4].Text.Contains("مكافأة"))
                Response.Redirect("ResearchRewardForm.aspx");
        }

        protected void lnkViewReDir_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            Session["ViewRequestFrom"] = row.Cells[0].Text;
            Session["AutoIdUpdated"] = row.Cells[7].Text;
            Session["Dir_Dean_Priv"] = "ReDir";
            Session["justView"] = null;
            Session["NotDefault"] = "Requests.aspx";
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand("Select JobId From " + (row.Cells[4].Text.Contains("رسوم") ? "ResearchFeeInfo" : "ResearchRewardForm") + " where AutoId=" + row.Cells[0].Text, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            Session["ResearchId"] = dr[0];
            conn.Close();


            if (row.Cells[4].Text.Contains("رسوم"))
                Response.Redirect("ResearchFeeForm.aspx");
            else if (row.Cells[4].Text.Contains("مكافأة"))
                Response.Redirect("ResearchRewardForm.aspx");
        }

        protected void lnkViewReDean_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            Session["ViewRequestFrom"] = row.Cells[0].Text;
            Session["AutoIdUpdated"] = row.Cells[6].Text;
            Session["Dir_Dean_Priv"] = "ReDean";
            Session["NotDefault"] = "Requests.aspx";
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand("Select JobId From " + (row.Cells[4].Text.Contains("رسوم") ? "ResearchFeeInfo" : "ResearchRewardForm") + "  where AutoId=" + row.Cells[0].Text, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            Session["ResearchId"] = dr[0];
            string c= dr[0].ToString();
            conn.Close();


            if (row.Cells[4].Text.Contains("رسوم"))
                Response.Redirect("ResearchFeeForm.aspx");
            else if (row.Cells[4].Text.Contains("مكافأة"))
                Response.Redirect("ResearchRewardForm.aspx");
        }

        protected void lnkClose_Click(object sender, EventArgs e)
        {
            FollowUpDiv.Visible = false;
        }

        protected void lnkDeleteRequest_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;

            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand sqlCommandDel = new SqlCommand("Select * from RequestsFollowUp where ReqToId=11 and RequestId="+row.Cells[0].Text + " and type="+ (((Label)(row.Cells[2].FindControl("Label1"))).Text.Contains("دعم") ? "0" : "1"), conn);
            DataTable dataTable = new DataTable();
            dataTable.Load(sqlCommandDel.ExecuteReader());
            if (dataTable.Rows.Count == 0)
            {
                SqlCommand cmd = new SqlCommand("Delete From " + (((Label)(row.Cells[2].FindControl("Label1"))).Text.Contains("دعم") ? "ResearchFeeInfo" : "ResearchRewardForm") + " where AutoId=" + row.Cells[0].Text, conn);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("Delete From RequestsFollowUp where RequestId=" + row.Cells[0].Text + " and Type=" + (((Label)(row.Cells[2].FindControl("Label1"))).Text.Contains("دعم") ? "0" : "1"), conn);
                cmd.ExecuteNonQuery();

                string sql = @"SELECT distinct r.[AutoId] ReqId
                          ,[JobId]
                          ,[ReqDate],RequestType,RequestFinalStatus
	                      ,(select ReqStatus from RequestsFollowUp rf where r.AutoId=rf.RequestId and type=0 and rf.AutoId =(select MAX(autoid) from RequestsFollowUp rr where rr.RequestId=r.AutoId and type=0)) rs
                      FROM ResearchFeeInfo r where   JobId=" + Session["uid"];
                sql += "union all ";
                sql += @"SELECT distinct r.[AutoId] ReqId
                          ,[JobId]
                          ,[ReqDate],RequestType,RequestFinalStatus
	                      ,(select ReqStatus from RequestsFollowUp rf where r.AutoId=rf.RequestId and type=1 and rf.AutoId =(select MAX(autoid) from RequestsFollowUp rr where rr.RequestId=r.AutoId and type=1)) rs
                      FROM ResearchRewardForm r where  JobId=" + Session["uid"];

                cmd = new SqlCommand(sql, conn);
                GridView1.DataSource = cmd.ExecuteReader();
                GridView1.DataBind();
            }
            else
                Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('لا يمكن حذف الطلب حاليا');", true);

            conn.Close();
        }
    }
}