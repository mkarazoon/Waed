using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace ResearchAcademicUnit
{

    public partial class ResearchSearch : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);
        DataTable dataTableIn = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.IsNewSession || Session["uid"] == null)
                Response.Redirect("Login.aspx");

        }











        protected void getReInfo(string cond, string search)
        {
            try
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("Select *,substring(ReAbstract,0,100) ReAbstract1 from ResearchsInfo where " + cond, conn);
                cmd.Parameters.AddWithValue("@sr1", search);
                cmd.Parameters.AddWithValue("@sr2", search);
                DataTable data = new DataTable();
                data.Load(cmd.ExecuteReader());
                //if (data.Rows.Count == 1)
                //{
                //    AllResearchDiv.Visible = false;
                //    ResearchInfoDiv.Visible = true;
                //    NotExistDataDiv.Visible = false;
                //    txtReID.Text = data.Rows[0]["reid"].ToString();
                //    txtTitle.Text = Server.HtmlDecode(data.Rows[0]["ReTitle"].ToString());
                //    txtAbstract.Text = data.Rows[0]["ReAbstract"].ToString();
                //    ddlType.SelectedValue = data.Rows[0]["retype"].ToString();

                //    if (ddlType.SelectedValue == "بحوث علمية")
                //    {
                //        ddlLevel.Items.Clear();
                //        ddlLevel.Items.Insert(0, "حدد نوع النشاط");
                //        ddlLevel.Items[0].Value = "0";
                //        ddlLevel.Items.Insert(1, "بحث علمي");
                //        ddlLevel.Items[1].Value = "بحث علمي";
                //        ddlLevel.Items.Insert(2, "بحث تاريخي");
                //        ddlLevel.Items[2].Value = "بحث تاريخي";
                //        ddlLevel.Items.Insert(3, "افتتاحية العدد");
                //        ddlLevel.Items[3].Value = "افتتاحية العدد";
                //    }
                //    else if (ddlType.SelectedValue == "مؤتمر علمي")
                //    {
                //        ddlLevel.Items.Clear();
                //        ddlLevel.Items.Insert(0, "حدد نوع النشاط");
                //        ddlLevel.Items[0].Value = "0";
                //        ddlLevel.Items.Insert(1, "مشاركة في مؤتمر");
                //        ddlLevel.Items[1].Value = "مشاركة في مؤتمر";
                //    }
                //    else if (ddlType.SelectedValue == "نشاط تأليفي")
                //    {
                //        ddlLevel.Items.Clear();
                //        ddlLevel.Items.Insert(0, "حدد نوع النشاط");
                //        ddlLevel.Items[0].Value = "0";
                //        ddlLevel.Items.Insert(1, "فصل في كتاب");
                //        ddlLevel.Items[1].Value = "فصل في كتاب";
                //        ddlLevel.Items.Insert(2, "كتاب");
                //        ddlLevel.Items[2].Value = "كتاب";
                //        ddlLevel.Items.Insert(3, "ترجمة");
                //        ddlLevel.Items[3].Value = "ترجمة";
                //    }
                //    else
                //        ddlLevel.Items.Clear();


                //    ddlLevel.SelectedValue = data.Rows[0]["relevel"].ToString();
                //    ddlYear.SelectedValue = data.Rows[0]["reyear"].ToString();
                //    ddlMonth.SelectedValue = data.Rows[0]["remonth"].ToString();
                //    ddlStatus.SelectedValue = data.Rows[0]["restatus"].ToString();
                //    txtReCitation.Text = data.Rows[0]["recitation"].ToString();
                //    ddlParticipate.SelectedValue = data.Rows[0]["ReParticipate"].ToString();
                //    ddlTeamType.SelectedValue = data.Rows[0]["TeamType"].ToString();
                //    txtAffliation.Text = data.Rows[0]["Aff_Auther"].ToString();



                //    txtJournalName.Text = data.Rows[0]["Magazine"].ToString();
                //    txtISSN.Text = data.Rows[0]["ISSN"].ToString();
                //    ddlRSourceType.SelectedValue = data.Rows[0]["SourceType"].ToString();
                //    txtPublisher.Text = data.Rows[0]["Author"].ToString();
                //    ddlClass.SelectedItem.Text = data.Rows[0]["MClass"].ToString();
                //    ddlTop10.SelectedValue = data.Rows[0]["TopMag"].ToString();
                //    txtRCitationAvg.Text = data.Rows[0]["CitationAvg"].ToString();

                //    ddlJurnalSector.SelectedValue = data.Rows[0]["MagVector"].ToString();
                //    ddlSector.SelectedValue = data.Rows[0]["ReSector"].ToString();
                //    if (ddlJurnalSector.SelectedValue != "0")
                //    {
                //        SqlCommand cmdFields = new SqlCommand("SELECT id,r.RField FROM ReFields r where r.RSector=N'" + ddlJurnalSector.SelectedValue + "'", conn);
                //        DataTable dtField = new DataTable();
                //        dtField.Load(cmdFields.ExecuteReader());
                //        ddlJurnalField.DataSource = dtField;
                //        ddlJurnalField.DataTextField = "RField";
                //        ddlJurnalField.DataValueField = "RField";
                //        ddlJurnalField.DataBind();
                //        //ddlJurnalField.Items.Insert(0, "اختيار");
                //       // ddlJurnalField.Items[0].Value = "0";

                //        ddlJurnalField.SelectedValue = data.Rows[0]["MagField"].ToString();

                //        //cmdFields = new SqlCommand("SELECT distinct r.RField FROM ReFields r where r.RSector=N'" + ddlJurnalSector.SelectedValue + "'", conn);
                //        //dtField = new DataTable();
                //        //dtField.Load(cmdFields.ExecuteReader());
                //        ddlSourceField.DataSource = dtField;
                //        ddlSourceField.DataTextField = "RField";
                //        ddlSourceField.DataValueField = "RField";
                //        ddlSourceField.DataBind();
                //        ddlSourceField.Items.Insert(0, "اختيار");
                //        ddlSourceField.Items[0].Value = "0";

                //        ddlSourceField.SelectedValue = data.Rows[0]["ReField"].ToString();
                //    }

                //    ddlSupport.SelectedValue = data.Rows[0]["InSupport"].ToString();
                //    ddlReward.SelectedValue = data.Rows[0]["Reward"].ToString();
                //    txtOutSupport.Text = data.Rows[0]["OutSupport"].ToString();
                //    ddlAllR.SelectedValue = data.Rows[0]["TotalR"].ToString();
                //    ddlINR.SelectedValue = data.Rows[0]["TotalRIn"].ToString();
                //    ddlClass.SelectedValue = data.Rows[0]["MClassInt"].ToString();
                //    txtEID.Text = data.Rows[0]["AutoidResearch"].ToString();

                //    if (ddlSupport.SelectedValue == "نعم")
                //    {
                //        cmd = new SqlCommand("Select RId from Reward_Support where RType=1 and ReId='" + txtReID.Text + "'", conn);
                //        SqlDataReader dr = cmd.ExecuteReader();
                //        dr.Read();
                //        ddlSupportRe.SelectedValue = dr[0].ToString();
                //        ddlSupportRe.Visible = true;
                //    }
                //    if (ddlReward.SelectedValue == "نعم")
                //    {
                //        cmd = new SqlCommand("Select RId from Reward_Support where RType=2 and ReId='" + txtReID.Text + "'", conn);
                //        SqlDataReader dr = cmd.ExecuteReader();
                //        dr.Read();
                //        ddlRewardRe.SelectedValue = dr[0].ToString();
                //        ddlRewardRe.Visible = true;
                //    }

                //    cmd = new SqlCommand("Select r.RId,RaName from ResearcherInfo r,Research_Researcher rr where r.rid=rr.rid and ReId='" + txtReID.Text + "'", conn);
                //    dataTableIn.Load(cmd.ExecuteReader());
                //    GridView2.DataSource = dataTableIn;// cmd.ExecuteReader();
                //    GridView2.DataBind();
                //    Session["dataTableIn"] = dataTableIn;
                //    if (GridView2.Rows.Count == 0)
                //    {
                //        ResearchNamesScopusDiv.Visible = true;
                //        cmd = new SqlCommand("Select Affiliations From RTemp where EID='" + txtEID.Text + "'", conn);
                //        DataTable dt = new DataTable();
                //        dt.Load(cmd.ExecuteReader());
                //        if (dt.Rows.Count != 0)
                //        {
                //            string[] aff = dt.Rows[0][0].ToString().Split(';');
                //            for (int i = 0; i < aff.Length; i++)
                //            {
                //                ResearchNamesScopusDiv.InnerHtml += (i + 1) + ". " + aff[i] + "<br>";
                //                if (aff[i].ToLower().Contains("middle east university"))
                //                {
                //                    txtAffliation.Text += (i + 1) + ";";
                //                }
                //            }
                //            if (txtAffliation.Text.Length != 0)
                //                txtAffliation.Text = txtAffliation.Text.Substring(0, txtAffliation.Text.Length - 1);
                //        }
                //    }


                //}
                //else if (data.Rows.Count > 1)
                //{
                AllResearchDiv.Visible = true;
                //ResearchInfoDiv.Visible = false;
                GridView1.DataSource = data;
                GridView1.DataBind();
                GridView1.Columns[2].Visible = false;
                bindResearch();
                //}
                //else
                //    Response.Redirect("NewResearch.aspx");
            }
            catch (Exception ex) { }
        }


        protected void lnkMore_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;

            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand cmd;
            LinkButton lnk = (sender as Control) as LinkButton;
            if (lnk.Text == "more")
            {
                cmd = new SqlCommand("Select ReAbstract from ResearchsInfo where ReId=N'" + row.Cells[0].Text + "'", conn);
                lnk.Text = "less";
            }
            else
            {
                cmd = new SqlCommand("Select substring(ReAbstract,0,100) ReAbstract1 from ResearchsInfo where ReId=N'" + row.Cells[0].Text + "'", conn);
                lnk.Text = "more";
            }
            DataTable data = new DataTable();
            data.Load(cmd.ExecuteReader());
            Label label = (Label)row.FindControl("lblAbstract");
            label.Text = data.Rows[0][0].ToString();
            conn.Close();
        }

        protected void bindResearch()
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand cmd;
            cmd = new SqlCommand("SELECT r.RId,r.RaName,RType,ReId FROM Reward_Support rs,ResearcherInfo r where rs.RId=r.RId", conn);
            DataTable dataTable = new DataTable();
            dataTable.Load(cmd.ExecuteReader());

            cmd = new SqlCommand("SELECT AutoId,ReId,rr.RId,case when datalength(r.RaName)=0 then REName else RaName end raname FROM Research_Researcher rr,ResearcherInfo r where rr.RId=r.RId", conn);
            DataTable dataTableAll = new DataTable();
            dataTableAll.Load(cmd.ExecuteReader());

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (GridView1.Rows[i].Cells[24].Text == "نعم")
                {
                    try
                    {
                        DataRow[] filteredRows = dataTable.Select("ReId='" + GridView1.Rows[i].Cells[0].Text + "' and RType=1");
                        GridView1.Rows[i].Cells[25].Text = filteredRows[0][1].ToString();
                    }
                    catch { }
                }

                if (GridView1.Rows[i].Cells[26].Text == "نعم")
                {
                    try
                    {
                        DataRow[] filteredRows = dataTable.Select("ReId='" + GridView1.Rows[i].Cells[0].Text + "' and RType=2");
                        GridView1.Rows[i].Cells[27].Text = filteredRows[0][1].ToString();
                    }
                    catch { }
                }
                DataTable ddd = new DataTable();
                ddd = dataTableAll.Clone();
                dataTableAll.AsEnumerable().Where(rr => rr.Field<String>("ReId").Equals(GridView1.Rows[i].Cells[0].Text)).CopyToDataTable(ddd, LoadOption.OverwriteChanges);

                GridView grid = (GridView)GridView1.Rows[i].FindControl("GridView3");
                grid.DataSource = ddd;
                grid.DataBind();
            }

            conn.Close();

        }

        //protected void btnExport_Click(object sender, EventArgs e)
        //{
        //    if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
        //        conn.Open();

        //    SqlCommand cmd = new SqlCommand("Select *,ReAbstract ReAbstract1 from ResearchsInfo", conn);
        //    DataTable data = new DataTable();
        //    data.Load(cmd.ExecuteReader());
        //    GridView1.DataSource = data;
        //    GridView1.DataBind();
        //    bindResearch();
        //    conn.Close();
        //    Button1.Visible = true;
        //}

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            GridView1.Columns[2].Visible = true;
            GridView1.Columns[3].Visible = false;
            GridView1.Columns[32].Visible = false;
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "SCOPUS_" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            GridView1.GridLines = GridLines.Both;
            GridView1.HeaderStyle.Font.Bold = true;
            GridView1.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());

            GridView1.Columns[3].Visible = true;
            GridView1.Columns[2].Visible = false;
            GridView1.Columns[32].Visible = true;
            GridView1.DataSource = null;
            GridView1.DataBind();
            Response.End();
        }





        //protected void lnkShowInfo_Click(object sender, EventArgs e)
        //{
        //    clearForm();
        //    GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
        //    if (row.Cells[0].Text.Contains("s") && !row.Cells[0].Text.Contains("&nbsp;"))
        //        //getReInfo("EID='" + HttpUtility.HtmlDecode(row.Cells[0].Text.Trim()) + "'");
        //        getReInfo("AutoidResearch=@sr1 or AutoidResearch=@sr2", HttpUtility.HtmlDecode(row.Cells[0].Text.Trim()));
        //    else
        //        //getReInfo("Retitle=N'" + HttpUtility.HtmlDecode(((Label)row.Cells[4].FindControl("lblTitle")).Text.Trim()) + "' or AutoidResearch='" + HttpUtility.HtmlDecode(((Label)row.Cells[4].FindControl("lblTitle")).Text.Trim()) + "'");
        //        getReInfo("Retitle=@sr1 or AutoidResearch=@sr2", HttpUtility.HtmlDecode(((Label)row.Cells[4].FindControl("lblTitle")).Text.Trim()));
        //    if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
        //        conn.Open();
        //    DataTable dt = new DataTable();
        //    SqlCommand cmd = new SqlCommand();
        //    if (row.Cells[2].Text.Contains("ملفات الدعم"))
        //    {
        //        cmd = new SqlCommand("Select ReTitle,'' Abstract, 0 Year,MagName Sourcetitle,MagISSN ISSN,PublisherName Publisher,N'مقبول للنشر' St,RID From ResearchFeeInfo rf,ResearcherInfo ri where rf.JobId=ri.acdid and ReTitle=@sr1", conn);
        //        cmd.Parameters.AddWithValue("@sr1", ((Label)row.Cells[4].FindControl("lblTitle")).Text);
        //        dt.Load(cmd.ExecuteReader());


        //        ddlSupport.SelectedValue = "نعم";
        //        ddlSupportRe.Visible = true;
        //        ddlSupportRe.SelectedValue = dt.Rows[0]["RID"].ToString();
        //    }

        //    if (row.Cells[2].Text.Contains("ملفات مكافأة"))
        //    {
        //        cmd = new SqlCommand("Select ReTitle,'' Abstract, 0 Year,MagName Sourcetitle,MagISSN ISSN,PublisherName Publisher,N'منشور' St,RID From ResearchRewardForm rf,ResearcherInfo ri where rf.JobId=ri.acdid and ReTitle=@rt", conn);
        //        cmd.Parameters.AddWithValue("@rt", ((Label)row.Cells[4].FindControl("lblTitle")).Text);
        //        dt.Load(cmd.ExecuteReader());

        //        ddlReward.SelectedValue = "نعم";
        //        ddlRewardRe.Visible = true;
        //        ddlRewardRe.SelectedValue = dt.Rows[0]["RID"].ToString();
        //    }

        //    conn.Close();
        //}

        //protected void grdNotExistData_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if(e.Row.RowType==DataControlRowType.DataRow)
        //    {
        //        if(e.Row.Cells[0].Text=="" || e.Row.Cells[0].Text =="&nbsp;" || e.Row.Cells[0].Text!="")
        //        {
        //            ((LinkButton)e.Row.FindControl("lnkShowInfo")).Visible = true;

        //        }
        //        else
        //        {
        //            ((LinkButton)e.Row.FindControl("lnkAdd")).Visible = true;
        //        }

        //        if (e.Row.Cells[2].Text.Contains("جديد"))
        //        {
        //            ((LinkButton)e.Row.FindControl("lnkAdd")).Visible = true;
        //            ((LinkButton)e.Row.FindControl("lnkShowInfo")).Visible = false;
        //            ((LinkButton)e.Row.FindControl("lnkUpdatetitle")).Visible = true;
        //        }
        //        else if (e.Row.Cells[2].Text.Contains("تحديث"))
        //        {
        //            ((LinkButton)e.Row.FindControl("lnkAdd")).Visible = false;
        //            ((LinkButton)e.Row.FindControl("lnkShowInfo")).Visible = true;
        //        }


        //    }
        //}

        //protected void lnkAdd_Click(object sender, EventArgs e)
        //{
        //    clearForm();
        //    GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
        //    //txtTitle.Text= HttpUtility.HtmlDecode( row.Cells[1].Text);
        //    txtEID.Text = HttpUtility.HtmlDecode(row.Cells[0].Text).Trim();
        //    if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
        //        conn.Open();

        //    SqlCommand cmd=new SqlCommand();
        //    if (!row.Cells[2].Text.Contains("ملفات"))
        //        cmd = new SqlCommand("Select *,Rtitle Retitle,N'منشور' St From RTemp where EID=N'" + row.Cells[0].Text + "'", conn);
        //    else if (row.Cells[2].Text.Contains("الدعم"))
        //    {
        //        cmd = new SqlCommand("Select ReTitle,'' Abstract, 0 Year,MagName Sourcetitle,MagISSN ISSN,PublisherName Publisher,N'مقبول للنشر' St,RID,'' CitedBy,AcceptDate From ResearchFeeInfo rf,ResearcherInfo ri where rf.JobId=ri.acdid and ReTitle=@t", conn);
        //        cmd.Parameters.AddWithValue("t", ((Label)row.Cells[4].FindControl("lblTitle")).Text);
        //    }
        //    else if (row.Cells[2].Text.Contains("مكافأة"))
        //        cmd = new SqlCommand("Select ReTitle,'' Abstract, 0 Year,MagName Sourcetitle,MagISSN ISSN,PublisherName Publisher,N'منشور' St,RID,'' CitedBy,'' AcceptDate From ResearchRewardForm rf,ResearcherInfo ri where rf.JobId=ri.acdid and ReTitle=N'" + ((Label)row.Cells[4].FindControl("lblTitle")).Text + "'", conn);

        //    DataTable dt = new DataTable();
        //    dt.Load(cmd.ExecuteReader());
        //    if(dt.Rows.Count!=0)
        //    {
        //        txtTitle.Text = HttpUtility.HtmlDecode(dt.Rows[0]["ReTitle"].ToString());
        //        txtAbstract.Text = dt.Rows[0]["Abstract"].ToString();
        //        txtISSN.Text = dt.Rows[0]["ISSN"].ToString();
        //        ddlYear.SelectedValue= dt.Rows[0]["Year"].ToString();
        //        txtJournalName.Text= dt.Rows[0]["Sourcetitle"].ToString();
        //        txtPublisher.Text = dt.Rows[0]["Publisher"].ToString();
        //        ddlStatus.SelectedValue = dt.Rows[0]["St"].ToString();
        //        txtReCitation.Text = (dt.Rows[0]["CitedBy"] == null ? "" : dt.Rows[0]["CitedBy"].ToString());
        //        txtAbstract.Text = "NA";
        //        txtAffliation.Text = "1";
        //        ddlINR.SelectedValue = "1";
        //        ddlAllR.SelectedValue = "1";
        //        ddlParticipate.SelectedValue = "منفرد";
        //        ddlTeamType.SelectedValue = "فردي";
        //        txtReCitation.Text = "0";
        //        ddlType.SelectedValue = "بحوث علمية";
        //        ddlLevel.Items.Clear();
        //        ddlLevel.Items.Insert(0, "حدد نوع النشاط");
        //        ddlLevel.Items[0].Value = "0";
        //        ddlLevel.Items.Insert(1, "بحث علمي");
        //        ddlLevel.Items[1].Value = "بحث علمي";
        //        ddlLevel.Items.Insert(2, "بحث تاريخي");
        //        ddlLevel.Items[2].Value = "بحث تاريخي";
        //        ddlLevel.Items.Insert(3, "افتتاحية العدد");
        //        ddlLevel.Items[3].Value = "افتتاحية العدد";
        //        ddlLevel.SelectedValue = "بحث علمي";
        //        ddlRSourceType.SelectedValue = "مجلة";
        //        if (row.Cells[2].Text.Contains("ملفات الدعم"))
        //        {
        //            ddlSupport.SelectedValue = "نعم";
        //            ddlSupportRe.Visible = true;
        //            ddlSupportRe.SelectedValue = dt.Rows[0]["RID"].ToString();
        //        }
        //        if (row.Cells[2].Text.Contains("ملفات مكافأة"))
        //        {
        //            ddlReward.SelectedValue = "نعم";
        //            ddlRewardRe.Visible = true;
        //            ddlRewardRe.SelectedValue = dt.Rows[0]["RID"].ToString();
        //        }
        //        dataTableIn.Columns.Add("RID");
        //        dataTableIn.Columns.Add("RaName");
        //        Session["dataTableIn"] = dataTableIn;

        //    }

        //    conn.Close();
        //    ResearchInfoDiv.Visible = true;
        //}

        //protected void lnkSupport_Click(object sender, EventArgs e)
        //{
        //    grdNotExistData.Columns[3].Visible = true;
        //    if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
        //        conn.Open();
        //    string sql = @"select ReTitle,N'ملفات الدعم جديد' AutoidResearch,N'مقبول للنشر' restatus,NULL eid,NULL RTitle ,AcceptDate AcceptedDate
        //                    from ResearchFeeInfo where RequestFinalStatus=N'مكتمل' 
        //                    and ReTitle not in (SELECT ReTitle FROM ResearchsInfo)
        //                    union
        //                    select ReTitle,N'ملفات الدعم تحديث' AutoidResearch,N'مقبول للنشر' restatus,NULL eid,NULL RTitle ,AcceptDate AcceptedDate
        //                    from ResearchFeeInfo where RequestFinalStatus=N'مكتمل' 
        //                    and ReTitle in (SELECT ReTitle FROM ResearchsInfo where InSupport=N'لا')";
        //    SqlCommand sqlCommand = new SqlCommand(sql, conn);
        //    grdNotExistData.DataSource = sqlCommand.ExecuteReader();
        //    grdNotExistData.DataBind();
        //    conn.Close();
        //    uploadDiv.Visible = false;
        //    NotExistDataDiv.Visible = true;
        //    UpdateRStatusDiv.Visible = false;
        //    ResearchInfoDiv.Visible = false;
        //    grdNotExistData.Columns[0].Visible = false;
        //    grdNotExistData.Columns[1].Visible = false;
        //    grdNotExistData.Columns[3].Visible = true;
        //}

        //protected void lnkReward_Click(object sender, EventArgs e)
        //{
        //    if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
        //        conn.Open();
        //    string sql = @"select ReTitle,N'ملفات مكافأة جديد' AutoidResearch,N'منشور' restatus,NULL eid,NULL RTitle ,'' AcceptedDate
        //                    from ResearchRewardForm where RequestFinalStatus=N'مكتمل' 
        //                    and ReTitle not in (SELECT ReTitle FROM ResearchsInfo)
        //                    union
        //                    select ReTitle,N'ملفات مكافأة تحديث' AutoidResearch,N'منشور' restatus,NULL eid,NULL RTitle ,'' AcceptedDate
        //                    from ResearchRewardForm where RequestFinalStatus=N'مكتمل' 
        //                    and ReTitle in (SELECT ReTitle FROM ResearchsInfo where Reward=N'لا')";
        //    SqlCommand sqlCommand = new SqlCommand(sql, conn);
        //    grdNotExistData.DataSource = sqlCommand.ExecuteReader();
        //    grdNotExistData.DataBind();
        //    conn.Close();
        //    uploadDiv.Visible = false;
        //    NotExistDataDiv.Visible = true;
        //    UpdateRStatusDiv.Visible = false;
        //    ResearchInfoDiv.Visible = false;
        //    AllResearchDiv.Visible = false;
        //    grdNotExistData.Columns[0].Visible = false;
        //    grdNotExistData.Columns[1].Visible = false;
        //    grdNotExistData.Columns[3].Visible = false;
        //}

        //protected void lnkUpload_Click(object sender, EventArgs e)
        //{
        //    uploadDiv.Visible = true;
        //    NotExistDataDiv.Visible = false;
        //    UpdateRStatusDiv.Visible = false;
        //    ResearchInfoDiv.Visible = false;
        //}

        //protected void btnUpload_Click(object sender, EventArgs e)
        //{
        //    ConfirmDiv.Visible = true;
        //    if (FileUpload1.HasFile)
        //    {
        //        string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
        //        string fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);
        //        string fileLocation = Server.MapPath("Docs/" + fileName);

        //        System.IO.DirectoryInfo di = new DirectoryInfo(Server.MapPath("Docs"));

        //        foreach (FileInfo file in di.GetFiles())
        //        {
        //            file.Delete();
        //        }
        //        foreach (DirectoryInfo dir in di.GetDirectories())
        //        {
        //            dir.Delete(true);
        //        }

        //        if (File.Exists(fileLocation))
        //        {
        //            Random n = new Random();
        //            fileLocation = fileLocation + n.Next();
        //        }

        //        FileUpload1.SaveAs(fileLocation);

        //        Session["Uploaded"] = fileLocation;
        //        Session["ext"] = fileExtension;
        //    }
        //    else
        //    {
        //        Session["Uploaded"] = null;
        //        Session["ext"] = null;
        //    }
        //}

        //public bool CompareResearchs()
        //{
        //    string fileLocation = Session["Uploaded"].ToString();
        //    string fileExtension = Session["ext"].ToString();
        //    if (Session["Uploaded"] != null)
        //    {

        //        string conStr = "";
        //        if (fileExtension == ".xls" || fileExtension == ".xlsb" || fileExtension == ".xlsx")
        //        {

        //                conStr = ConfigurationManager.ConnectionStrings["Excel"].ConnectionString;
        //                conStr = String.Format(conStr, fileLocation, "Yes");
        //                OleDbConnection connExcel = new OleDbConnection(conStr);
        //                OleDbCommand cmdExcel = new OleDbCommand();
        //                OleDbDataAdapter oda = new OleDbDataAdapter();
        //                DataTable dt = new DataTable();
        //                cmdExcel.Connection = connExcel;

        //            //Get the name of First Sheet
        //            try
        //            {
        //                connExcel.Close();
        //            }
        //            catch {

        //            }

        //            try
        //            {
        //                connExcel.Open();
        //            }
        //            catch (Exception err)
        //            {
        //            }
        //            DataTable dtExcelSchema;
        //            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        //            String[] excelSheetNames = new String[dtExcelSchema.Rows.Count];

        //            string SheetName = "";
        //            if (rdType.SelectedValue == "1")
        //            {
        //                SheetName = dtExcelSchema.Rows[18]["TABLE_NAME"].ToString();
        //            }
        //            else
        //            {
        //                SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
        //            }

        //            connExcel.Close();
        //            connExcel.Open();
        //            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
        //            oda.SelectCommand = cmdExcel;
        //            IDataReader excelDr = cmdExcel.ExecuteReader();
        //            int x = excelDr.FieldCount;
        //            excelDr.Read();
        //            string exMessage = "";
        //            if (rdType.SelectedValue != "1")
        //            {
        //                try
        //                {
        //                    excelDr["Title"].ToString();
        //                }
        //                catch (Exception ex)
        //                {
        //                    exMessage = "عمود عنوان البحث (Title) غير موجود";
        //                }
        //                try
        //                {
        //                    excelDr["Year"].ToString();
        //                }
        //                catch (Exception ex)
        //                {
        //                    exMessage += " - عمود السنة (Year) غير موجود";
        //                }
        //                try
        //                {
        //                    excelDr["Source title"].ToString();
        //                }
        //                catch (Exception ex)
        //                {
        //                    exMessage += " - عمود مصدر البحث (Source title) غير موجود";
        //                }
        //                try
        //                {
        //                    excelDr["Cited by"].ToString();
        //                }
        //                catch (Exception ex)
        //                {
        //                    exMessage += " - عمود الاستشهادات (Cited By) غير موجود";
        //                }
        //                try
        //                {
        //                    excelDr["Abstract"].ToString();
        //                }
        //                catch (Exception ex)
        //                {
        //                    exMessage += " - عمود ملخص البحث (Abstract) غير موجود";
        //                }
        //                try
        //                {
        //                    excelDr["Publisher"].ToString();
        //                }
        //                catch (Exception ex)
        //                {
        //                    exMessage += " - عمود الناشر (Publisher) غير موجود";
        //                }
        //                try
        //                {
        //                    excelDr["ISSN"].ToString();
        //                }
        //                catch (Exception ex)
        //                {
        //                    exMessage += " - عمود ISSN غير موجود";
        //                }
        //                try
        //                {
        //                    excelDr["EID"].ToString();
        //                }
        //                catch (Exception ex)
        //                {
        //                    exMessage += " - عمود EID غير موجود";
        //                }
        //                try
        //                {
        //                    excelDr["Authors"].ToString();
        //                }
        //                catch (Exception ex)
        //                {
        //                    exMessage += " - عمود الباحثين (Authors) غير موجود";
        //                }
        //                try
        //                {
        //                    excelDr["Authors with affiliations"].ToString();
        //                }
        //                catch (Exception ex)
        //                {
        //                    exMessage += " - عمود الباحثين والجامعات (Authors with affiliations) غير موجود";
        //                }
        //                try
        //                {
        //                    excelDr["Document Type"].ToString();
        //                }
        //                catch (Exception ex)
        //                {
        //                    exMessage += " - عمود نوع البحث (Document Type) غير موجود";
        //                }
        //                try
        //                {
        //                    excelDr["site score"].ToString();
        //                }
        //                catch (Exception ex)
        //                {
        //                    exMessage += " - عمود نوع البحث (site score) غير موجود";
        //                }
        //                //try
        //                //{
        //                //    excelDr["quarter"].ToString();
        //                //}
        //                //catch (Exception ex)
        //                //{
        //                //    exMessage += " - عمود نوع البحث (quarter) غير موجود";
        //                //}

        //            }
        //            else
        //            {
        //                try
        //                {
        //                    excelDr["Title"].ToString();
        //                }
        //                catch (Exception ex)
        //                {
        //                    exMessage = "عمود اسم المجلة (Title) غير موجود";
        //                }
        //                try
        //                {
        //                    excelDr["CiteScore"].ToString();
        //                }
        //                catch (Exception ex)
        //                {
        //                    exMessage += " - عمود الاستشهادات (CiteScore) غير موجود";
        //                }
        //                try
        //                {
        //                    excelDr["Scopus ASJC Code (Sub-subject Area)"].ToString();
        //                }
        //                catch (Exception ex)
        //                {
        //                    exMessage += " - عمود قطاع المجلة (Scopus ASJC Code (Sub-subject Area)) غير موجود";
        //                }
        //                try
        //                {
        //                    excelDr["Publisher"].ToString();
        //                }
        //                catch (Exception ex)
        //                {
        //                    exMessage += " - عمود الناشر (Publisher) غير موجود";
        //                }
        //                //try
        //                //{
        //                //    excelDr["Quartile"].ToString();
        //                //}
        //                //catch (Exception ex)
        //                //{
        //                //    exMessage += " - عمود ربع المجلة (Quartile) غير موجود";
        //                //}
        //                //try
        //                //{
        //                //    excelDr["Top 10% (CiteScore Percentile)"].ToString();
        //                //}
        //                //catch (Exception ex)
        //                //{
        //                //    exMessage += " - عمود (Top 10% (CiteScore Percentile)) غير موجود";
        //                //}
        //            }

        //            //Session["ex"] = exMessage;
        //            connExcel.Close();

        //            connExcel.Open();
        //            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
        //            oda.SelectCommand = cmdExcel;
        //            excelDr = cmdExcel.ExecuteReader();
        //            if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
        //                conn.Open();

        //            SqlBulkCopy bulkCopy = new SqlBulkCopy(conn);

        //            if (rdType.SelectedValue == "1")
        //            {
        //                SqlCommand cmdDTemp = new SqlCommand("Delete From CurrentJurnals", conn);
        //                cmdDTemp.ExecuteNonQuery();
        //                bulkCopy.DestinationTableName = "CurrentJurnals";
        //                SqlBulkCopyColumnMapping RTitle = new SqlBulkCopyColumnMapping("Title", "Title");
        //                bulkCopy.ColumnMappings.Add(RTitle);
        //                SqlBulkCopyColumnMapping Year = new SqlBulkCopyColumnMapping("CiteScore", "CiteScore");
        //                bulkCopy.ColumnMappings.Add(Year);
        //                SqlBulkCopyColumnMapping Sourcetitle = new SqlBulkCopyColumnMapping("Scopus ASJC Code (Sub-subject Area)", "ScopusASJCCode");
        //                bulkCopy.ColumnMappings.Add(Sourcetitle);
        //                SqlBulkCopyColumnMapping CitedBy = new SqlBulkCopyColumnMapping("Publisher", "Publisher");
        //                bulkCopy.ColumnMappings.Add(CitedBy);
        //                SqlBulkCopyColumnMapping Abstract = new SqlBulkCopyColumnMapping("Quartile", "Quartile");
        //                bulkCopy.ColumnMappings.Add(Abstract);
        //                SqlBulkCopyColumnMapping Publisher = new SqlBulkCopyColumnMapping("Top 10% (CiteScore Percentile)", "Top10");
        //                bulkCopy.ColumnMappings.Add(Publisher);
        //            }
        //            else
        //            {
        //                SqlCommand cmdDTemp = new SqlCommand("Delete From RTemp", conn);
        //                cmdDTemp.ExecuteNonQuery();
        //                bulkCopy.DestinationTableName = "RTemp";
        //                SqlBulkCopyColumnMapping RTitle = new SqlBulkCopyColumnMapping("Title", "RTitle");
        //                bulkCopy.ColumnMappings.Add(RTitle);
        //                SqlBulkCopyColumnMapping Year = new SqlBulkCopyColumnMapping("Year", "Year");
        //                bulkCopy.ColumnMappings.Add(Year);
        //                SqlBulkCopyColumnMapping Sourcetitle = new SqlBulkCopyColumnMapping("Source title", "Sourcetitle");
        //                bulkCopy.ColumnMappings.Add(Sourcetitle);
        //                SqlBulkCopyColumnMapping CitedBy = new SqlBulkCopyColumnMapping("Cited by", "CitedBy");
        //                bulkCopy.ColumnMappings.Add(CitedBy);
        //                SqlBulkCopyColumnMapping Abstract = new SqlBulkCopyColumnMapping("Abstract", "Abstract");
        //                bulkCopy.ColumnMappings.Add(Abstract);
        //                SqlBulkCopyColumnMapping Publisher = new SqlBulkCopyColumnMapping("Publisher", "Publisher");
        //                bulkCopy.ColumnMappings.Add(Publisher);
        //                SqlBulkCopyColumnMapping ISSN = new SqlBulkCopyColumnMapping("ISSN", "ISSN");
        //                bulkCopy.ColumnMappings.Add(ISSN);
        //                SqlBulkCopyColumnMapping EID = new SqlBulkCopyColumnMapping("EID", "EID");
        //                bulkCopy.ColumnMappings.Add(EID);
        //                SqlBulkCopyColumnMapping Authors = new SqlBulkCopyColumnMapping("Authors", "Authors");
        //                bulkCopy.ColumnMappings.Add(Authors);
        //                SqlBulkCopyColumnMapping Affiliations = new SqlBulkCopyColumnMapping("Authors with affiliations", "Affiliations");
        //                bulkCopy.ColumnMappings.Add(Affiliations);
        //                SqlBulkCopyColumnMapping DocType = new SqlBulkCopyColumnMapping("Document Type", "DocType");
        //                bulkCopy.ColumnMappings.Add(DocType);
        //                //SqlBulkCopyColumnMapping SiteScore = new SqlBulkCopyColumnMapping("site score", "SiteScore");
        //                //bulkCopy.ColumnMappings.Add(SiteScore);
        //                //SqlBulkCopyColumnMapping quarter = new SqlBulkCopyColumnMapping("quarter", "Quarter");
        //                //bulkCopy.ColumnMappings.Add(quarter);

        //            }

        //            try
        //            {
        //                ConfirmDiv.Visible = false;
        //                bulkCopy.BulkCopyTimeout = 0;
        //                bulkCopy.WriteToServer(excelDr);

        //                bulkCopy.Close();

        //                connExcel.Close();
        //                conn.Close();
        //                return true;
        //            }

        //            catch (Exception ex)
        //            {
        //                bulkCopy.Close();

        //                connExcel.Close();
        //                conn.Close();
        //                Session["ex"] = exMessage;
        //            }
        //            conn.Close();
        //        }
        //        return false;
        //    }
        //    return false;
        //}


        protected void lnkSearch_Click(object sender, EventArgs e)
        {
            searchDiv.Visible = true;

        }

        //protected void btnCancel_Click(object sender, EventArgs e)
        //{
        //    ConfirmDiv.Visible = false;
        //}

        //protected void btnConfirm_Click(object sender, EventArgs e)
        //{
        //    ConfirmDiv.Visible = false;
        //    if (CompareResearchs())
        //    {
        //        Timer1.Interval = 10000;
        //        Timer1.Enabled = true;
        //        alertMsgSuccess.Visible = true;
        //    }
        //    else
        //    {
        //        Timer1.Interval = 10000;
        //        Timer1.Enabled = true;
        //        Label2.Text = Session["ex"].ToString();
        //        alertErr.Visible = true;
        //    }

        //}

        //protected void btnOk_Click(object sender, EventArgs e)
        //{
        //    msgDiv.Visible = false;
        //}

        //protected void Timer1_Tick(object sender, EventArgs e)
        //{
        //    alertMsgSuccess.Visible = false;
        //    alertErr.Visible = false;
        //    Timer1.Enabled = false;
        //}


        protected void lnkMainSearch_Click(object sender, EventArgs e)
        {
            searchDiv.Visible = false;
            if (txtSearch.Text != "")
                //getReInfo("Retitle like N'%"+txtSearch.Text +"%'");
                getReInfo("Retitle like @sr1", "%" + txtSearch.Text + "%");
            else
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("Select *,substring(ReAbstract,1,100) ReAbstract1 from ResearchsInfo", conn);
                DataTable data = new DataTable();
                data.Load(cmd.ExecuteReader());
                GridView1.DataSource = data;
                GridView1.DataBind();
                bindResearch();
                conn.Close();
                GridView1.Columns[2].Visible = false;
                Button1.Visible = true;
                AllResearchDiv.Visible = true;

            }

        }

        protected void lnkClearSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            AllResearchDiv.Visible = false;
        }
    }
}