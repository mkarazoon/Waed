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
    public partial class SupportAwardRep : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                //getData();
                fillSetting();
            }
        }

        protected void fillSetting()
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();


            SqlCommand cmd = new SqlCommand("select * from YearSetting", conn);
            DataTable dtSetting = new DataTable();
            dtSetting.Load(cmd.ExecuteReader());

            DataTable dt1 = new DataTable();
            dt1.Columns.Add("Year");
            for (int i = 2007; i <= Convert.ToInt16(DateTime.Now.Date.Year) + 1; i++)
            {
                DataRow row = dt1.NewRow();
                row[0] = i;
                dt1.Rows.Add(row);
            }

            ddlFromYear.DataSource = dt1;
            ddlFromYear.DataTextField = "Year";
            ddlFromYear.DataValueField = "Year";
            ddlFromYear.DataBind();
            ddlFromYear.Items.Insert(0, "From Year");
            ddlFromYear.Items[0].Value = "0";

            DataTable dt2 = new DataTable();
            dt2.Columns.Add("Month");
            for (int i = 1; i <= 12; i++)
            {
                DataRow row = dt2.NewRow();
                row[0] = i;
                dt2.Rows.Add(row);
            }

            ddlFromMonth.DataSource = dt2;
            ddlFromMonth.DataTextField = "Month";
            ddlFromMonth.DataValueField = "Month";
            ddlFromMonth.DataBind();
            ddlFromMonth.Items.Insert(0, "From Month");
            ddlFromMonth.Items[0].Value = "0";

            ddlToMonth.DataSource = dt2;
            ddlToMonth.DataTextField = "Month";
            ddlToMonth.DataValueField = "Month";
            ddlToMonth.DataBind();
            ddlToMonth.Items.Insert(0, "To Month");
            ddlToMonth.Items[0].Value = "0";


            conn.Close();
        }

        protected void ddlFromYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("Year");
            for (int i = Convert.ToInt16(ddlFromYear.SelectedValue); i <= Convert.ToInt16(DateTime.Now.Year) + 1; i++)
            {
                DataRow row = dt1.NewRow();
                row[0] = i;
                dt1.Rows.Add(row);
            }

            ddlToYear.DataSource = dt1;
            ddlToYear.DataTextField = "Year";
            ddlToYear.DataValueField = "Year";
            ddlToYear.DataBind();
            ddlToYear.Items.Insert(0, "To Year");
            ddlToYear.Items[0].Value = "0";

        }

        protected void getData()
        {
            string sql = @"SELECT      distinct ri.ReId, ReTitle, Magazine, MClass, ReYear, ReMonth, ReStatus, ReParticipate, TeamType, Aff_Auther,
(select 
STUFF((
select '; ' + rii.RaName
from ResearcherInfo rii,Research_Researcher rrr
where rii.RId=rrr.RId and rrr.ReId=rs.reid
order by AutoId
FOR XML PATH('')), 1, 1, '') researchers
from ResearchsInfo rs where rs.ReId=ri.ReId) 'RNames', TotalR, TotalRIn,
InSupport,isnull((select distinct RaName from ResearcherInfo r1,Reward_Support rs where r1.RId=rs.rid and rs.reid=ri.ReId and rtype=1),'-') 'SupRName',
isnull((SELECT rinfo.[FeeValue] FROM ReDirectorInfo rinfo,ResearchFeeInfo reinfo where rinfo.RequestId=reinfo.AutoId and reinfo.ReTitle=ri.ReTitle and type=0 and reinfo.RequestFinalStatus=N'مكتمل'),'-') 'SupAmount',
Reward,isnull((select distinct RaName from ResearcherInfo r1,Reward_Support rs where r1.RId=rs.rid and rs.reid=ri.ReId and rtype=2 ),'-') 'AwardRName',
isnull((SELECT rinfo.[FeeValue] FROM ReDirectorInfo rinfo,ResearchRewardForm reinfo where rinfo.RequestId=reinfo.AutoId and reinfo.ReTitle=ri.ReTitle and type=1 and reinfo.RequestFinalStatus=N'مكتمل'),'-') 'AwardAmount'

FROM            ResearchsInfo ri,ResearcherInfo r,Research_Researcher rr
WHERE   ri.reid=rr.reid and rr.rid=r.RId  
and ((ReYear = 2019 AND ReMonth BETWEEN 9 AND 12)
OR
(ReYear = 2020 AND ReMonth BETWEEN 1 AND 8))
--and (insupport=N'نعم' or reward=N'نعم') and ReStatus=N'منشور'
ORDER BY ReTitle";
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand(sql, conn);
            DataTable dtInfo = new DataTable();
            dtInfo.Load(cmd.ExecuteReader());
            GridView1.DataSource = dtInfo;
            GridView1.DataBind();
            conn.Close();
        }

        protected void btnApply_Click(object sender, EventArgs e)
        {
            int mf = Convert.ToInt16(ddlFromMonth.SelectedValue);
            int y1 = Convert.ToInt16(ddlFromYear.SelectedValue);
            int mt = Convert.ToInt16(ddlToMonth.SelectedValue);
            int y2 = Convert.ToInt16(ddlToYear.SelectedValue);
            int y = DateTime.Now.Year;
            int m = DateTime.Now.Month;

            string cond = "";
            if (y1 == y2)
                cond = " and reyear=" + y1 + " and remonth between " + mf + " and " + (mt);
            else
            {
                cond = " and ((reyear=" + (y1) + " and remonth between " + mf + " and 12) or (reyear=" + y2 + " and remonth between 1 and " + (mt) + ")) ";
            }

            string sql = "";
            if (ddlType.SelectedValue=="1")
            {
                sql = @"SELECT      distinct ri.ReId, ri.ReTitle, ReYear, ReMonth,InSupport,isnull((select distinct RaName from ResearcherInfo r1,Reward_Support rs where r1.RId=rs.rid and rs.reid=ri.ReId and rtype=1),'-') 'SupRName',
isnull((SELECT rinfo.[FeeValue] FROM ReDirectorInfo rinfo,ResearchFeeInfo reinfo where rinfo.RequestId=reinfo.AutoId and reinfo.ReTitle=ri.ReTitle and type=0 and reinfo.RequestFinalStatus=N'مكتمل'),'-') 'SupAmount'
,'' Magazine,'' MClass,'' ReStatus,'' ReParticipate,'' TeamType,'' Aff_Auther,'' RNames,'' TotalR,'' TotalRIn,'' Reward,'' AwardRName,'' AwardAmount
,isnull((select distinct AcdId from ResearcherInfo r1,Reward_Support rs where r1.RId=rs.rid and rs.reid=ri.ReId and rtype=1),'-') AcdId
FROM            ResearchsInfo ri,ResearcherInfo r,Research_Researcher rr
WHERE   ri.reid=rr.reid and rr.rid=r.RId  " + cond + " ORDER BY ReTitle";
            }
            else
            {
                sql = @"SELECT    distinct   ri.ReId, ri.ReTitle, ReYear, ReMonth, Reward,c.RaName AwardRName,c.NewAmount AwardAmount,
'' Magazine,'' MClass,'' ReStatus,'' ReParticipate,'' TeamType,'' Aff_Auther,'' RNames,'' TotalR,'' TotalRIn,'' InSupport,'' SupRName,'' SupAmount,c.AcdId
FROM            ResearchsInfo ri,ResearcherInfo r,Research_Researcher rr,ResearchRewardForm rrf,NewAmountAward c
WHERE   ri.reid=rr.reid and rr.rid=r.RId and ri.ReTitle=rrf.ReTitle and rrf.AutoId=c.ReqId " + cond + @"
union all
SELECT c.ReId, b.ReTitle,ReYear,ReMonth,c.Reward,e.RaName AwardRName,FeeValue AwardAmount,
'' Magazine,'' MClass,'' ReStatus,'' ReParticipate,'' TeamType,'' Aff_Auther,'' RNames,'' TotalR,'' TotalRIn,'' InSupport,'' SupRName,'' SupAmount,e.AcdId
  FROM ReDirectorInfo a,ResearchRewardForm b,ResearchsInfo c,Reward_Support d,ResearcherInfo e
  where a.RequestId=b.AutoId and b.ReTitle=c.ReTitle and c.ReId=d.ReId and d.RId=e.RId and d.RType=2 and type=1 and FeeValue<>''" + cond;
            }
//            string sql = @"SELECT      distinct ri.ReId, ReTitle, Magazine, MClass, ReYear, ReMonth, ReStatus, ReParticipate, TeamType, Aff_Auther,
//(select 
//STUFF((
//select '; ' + rii.RaName
//from ResearcherInfo rii,Research_Researcher rrr
//where rii.RId=rrr.RId and rrr.ReId=rs.reid
//order by AutoId
//FOR XML PATH('')), 1, 1, '') researchers
//from ResearchsInfo rs where rs.ReId=ri.ReId) 'RNames', TotalR, TotalRIn,
//InSupport,isnull((select distinct RaName from ResearcherInfo r1,Reward_Support rs where r1.RId=rs.rid and rs.reid=ri.ReId and rtype=1),'-') 'SupRName',
//isnull((SELECT rinfo.[FeeValue] FROM ReDirectorInfo rinfo,ResearchFeeInfo reinfo where rinfo.RequestId=reinfo.AutoId and reinfo.ReTitle=ri.ReTitle and type=0 and reinfo.RequestFinalStatus=N'مكتمل'),'-') 'SupAmount',
//Reward,isnull((select distinct RaName from ResearcherInfo r1,Reward_Support rs where r1.RId=rs.rid and rs.reid=ri.ReId and rtype=2 ),'-') 'AwardRName',
//(case when isnull((SELECT rinfo.[FeeValue] FROM ReDirectorInfo rinfo,ResearchRewardForm reinfo where rinfo.RequestId=reinfo.AutoId and reinfo.ReTitle=ri.ReTitle and type=1 and reinfo.RequestFinalStatus=N'مكتمل'),'-')<>N'' 
//then isnull((SELECT rinfo.[FeeValue] FROM ReDirectorInfo rinfo,ResearchRewardForm reinfo where rinfo.RequestId=reinfo.AutoId and reinfo.ReTitle=ri.ReTitle and type=1 and reinfo.RequestFinalStatus=N'مكتمل'),'-')
//else (select NewAmount From NewAmountAward na,ResearchRewardForm rf where na.ReqId=rf.AutoId and na.AcdId=rf.JobId and rf.ReTitle=ri.ReTitle) end) 'AwardAmount'

//FROM            ResearchsInfo ri,ResearcherInfo r,Research_Researcher rr
//WHERE   ri.reid=rr.reid and rr.rid=r.RId  " + cond + "ORDER BY ReTitle";
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand(sql, conn);
            DataTable dtInfo = new DataTable();
            dtInfo.Load(cmd.ExecuteReader());
            GridView1.DataSource = dtInfo;
            GridView1.DataBind();

            //int rpcount = dtInfo.AsEnumerable().Where(r => r.Field<string>(7) == "مشترك").ToList().Count();
            //int rscount = dtInfo.AsEnumerable().Where(r => r.Field<string>(7) == "منفرد").ToList().Count();

            //DataTable dtType = new DataTable();
            //dtType.Columns.Add("RType");
            //dtType.Columns.Add("RCount");

            //DataRow row = dtType.NewRow();
            //row[0] = "مشترك";
            //row[1] = rpcount;
            //dtType.Rows.Add(row);

            //row = dtType.NewRow();
            //row[0] = "منفرد";
            //row[1] = rscount;
            //dtType.Rows.Add(row);

            //GridView2.DataSource = dtType;
            //GridView2.DataBind();


            //DataTable dtSA = new DataTable();
            //dtSA.Columns.Add("RType");
            //dtSA.Columns.Add("RSupport");
            //dtSA.Columns.Add("RAward");


            //int rsupcount = dtInfo.AsEnumerable().Where(r => r.Field<string>(13) == "نعم").ToList().Count();
            //int racount = dtInfo.AsEnumerable().Where(r => r.Field<string>(16) == "نعم").ToList().Count();

            //DataRow row1 = dtSA.NewRow();
            //row1[0] = "العدد";
            //row1[1] = rsupcount;
            //row1[2] = racount;
            //dtSA.Rows.Add(row1);

            //double jd = 0;
            //double us = 0;
            //double ur = 0;

            //for (int i = 0; i < GridView1.Rows.Count; i++)
            //{
            //    string[] data = GridView1.Rows[i].Cells[14].Text.Split(' ');
            //    if (data.Length != 0)
            //    {
            //        if (GridView1.Rows[i].Cells[14].Text.Contains("دينار"))
            //            jd += Convert.ToDouble(data[0]);
            //        else if (GridView1.Rows[i].Cells[14].Text.Contains("دولار"))
            //            us += Convert.ToDouble(data[0]);
            //        else if (GridView1.Rows[i].Cells[14].Text.Contains("يورو"))
            //            ur += Convert.ToDouble(data[0]);
            //    }
            //}
            ////double supSum = dtInfo.AsEnumerable().Where(r => r.Field<string>(15) == "نعم").ToList().Count();
            //string samount = "دينار أردني: " + jd + "<br>دولار أمريكي: " + us + "<br>يورو: " + ur;

            //jd = 0;
            //us = 0;
            //ur = 0;

            //for (int i = 0; i < GridView1.Rows.Count; i++)
            //{
            //    string[] data = GridView1.Rows[i].Cells[17].Text.Split(' ');
            //    if (data.Length != 0)
            //    {
            //        if (GridView1.Rows[i].Cells[17].Text.Contains("دينار"))
            //            jd += Convert.ToDouble(data[0]);
            //        else if (GridView1.Rows[i].Cells[17].Text.Contains("دولار"))
            //            us += Convert.ToDouble(data[0]);
            //        else if (GridView1.Rows[i].Cells[17].Text.Contains("يورو"))
            //            ur += Convert.ToDouble(data[0]);
            //    }
            //}

            //string aamount = "دينار أردني: " + jd + "<br>دولار أمريكي: " + us + "<br>يورو: " + ur;

            //row1 = dtSA.NewRow();
            //row1[0] = "القيمة";
            //row1[1] = HttpUtility.HtmlDecode(samount);
            //row1[2] = HttpUtility.HtmlDecode(aamount);
            //dtSA.Rows.Add(row1);

            //GridView3.DataSource = dtSA;
            //GridView3.DataBind();

            //int rqr1 = dtInfo.AsEnumerable().Where(r => r.Field<string>(3) == "الربع الأول").ToList().Count();
            //int rqr2 = dtInfo.AsEnumerable().Where(r => r.Field<string>(3) == "الربع الثاني").ToList().Count();
            //int rqr3 = dtInfo.AsEnumerable().Where(r => r.Field<string>(3) == "الربع الثالث").ToList().Count();
            //int rqr4 = dtInfo.AsEnumerable().Where(r => r.Field<string>(3) == "الربع الرابع").ToList().Count();
            //int rqr5 = dtInfo.AsEnumerable().Where(r => r.Field<string>(3) == "Non Q's").ToList().Count();
            //int rqr6 = dtInfo.AsEnumerable().Where(r => r.Field<string>(3) == null).ToList().Count();


            //string[] amount = { "الربع الأول", "الربع الثاني", "الربع الثالث", "الربع الرابع", "Non Q's", "" };

            //for (int x = 0; x <= 5; x++)
            //{
            //    jd = 0;
            //    us = 0;
            //    ur = 0;
            //    for (int i = 0; i < GridView1.Rows.Count; i++)
            //    {
            //        //if(i==19)
            //        //{
            //        //    string xxx = "";
            //        //}
            //        string dd = HttpUtility.HtmlDecode(GridView1.Rows[i].Cells[2].Text).Trim();
            //        if (dd == amount[x])
            //        {
            //            string[] data = GridView1.Rows[i].Cells[14].Text.Split(' ');
            //            if (data.Length != 0)
            //            {
            //                if (GridView1.Rows[i].Cells[14].Text.Contains("دينار"))
            //                    jd += Convert.ToDouble(data[0]);
            //                else if (GridView1.Rows[i].Cells[14].Text.Contains("دولار"))
            //                    us += Convert.ToDouble(data[0]);
            //                else if (GridView1.Rows[i].Cells[14].Text.Contains("يورو"))
            //                    ur += Convert.ToDouble(data[0]);
            //            }
            //        }
            //    }
            //    amount[x] = "دينار أردني: " + jd + "<br>دولار أمريكي: " + us + "<br>يورو: " + ur;
            //}


            //string[] amounta = { "الربع الأول", "الربع الثاني", "الربع الثالث", "الربع الرابع", "Non Q's", "" };

            //for (int x = 0; x <= 5; x++)
            //{
            //    jd = 0;
            //    us = 0;
            //    ur = 0;
            //    for (int i = 0; i < GridView1.Rows.Count; i++)
            //    {
            //        if (HttpUtility.HtmlDecode(GridView1.Rows[i].Cells[2].Text).Trim() == amounta[x])
            //        {
            //            string[] data = GridView1.Rows[i].Cells[17].Text.Split(' ');
            //            if (data.Length != 0)
            //            {
            //                if (GridView1.Rows[i].Cells[17].Text.Contains("دينار"))
            //                    jd += Convert.ToDouble(data[0]);
            //                else if (GridView1.Rows[i].Cells[17].Text.Contains("دولار"))
            //                    us += Convert.ToDouble(data[0]);
            //                else if (GridView1.Rows[i].Cells[17].Text.Contains("يورو"))
            //                    ur += Convert.ToDouble(data[0]);
            //            }
            //        }
            //    }
            //    amounta[x] = "دينار أردني: " + jd + "<br>دولار أمريكي: " + us + "<br>يورو: " + ur;
            //}

            //DataTable d = new DataTable();
            //d.Columns.Add("qrt");
            //d.Columns.Add("RCount");
            //d.Columns.Add("SupportAmount");
            //d.Columns.Add("AwardAmount");

            //DataRow drow = d.NewRow();
            //drow[0] = "الربع الأول";
            //drow[1] = rqr1;
            //drow[2] = amount[0];
            //drow[3] = amounta[0];
            //d.Rows.Add(drow);

            //drow = d.NewRow();
            //drow[0] = "الربع الثاني";
            //drow[1] = rqr2;
            //drow[2] = amount[1];
            //drow[3] = amounta[1];
            //d.Rows.Add(drow);

            //drow = d.NewRow();
            //drow[0] = "الربع الثالث";
            //drow[1] = rqr3;
            //drow[2] = amount[2];
            //drow[3] = amounta[2];
            //d.Rows.Add(drow);

            //drow = d.NewRow();
            //drow[0] = "الربع الرابع";
            //drow[1] = rqr4;
            //drow[2] = amount[3];
            //drow[3] = amounta[3];
            //d.Rows.Add(drow);

            //drow = d.NewRow();
            //drow[0] = "Non Q's";
            //drow[1] = rqr5;
            //drow[2] = amount[4];
            //drow[3] = amounta[4];
            //d.Rows.Add(drow);

            //drow = d.NewRow();
            //drow[0] = "غير متاح";
            //drow[1] = rqr6;
            //drow[2] = amount[5];
            //drow[3] = amounta[5];
            //d.Rows.Add(drow);

            //GridView4.DataSource = d;
            //GridView4.DataBind();


            //jd = 0;
            //us = 0;
            //ur = 0;
            //int[] order = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            //for (int i = 0; i < GridView1.Rows.Count; i++)
            //{
            //    if (HttpUtility.HtmlDecode(GridView1.Rows[i].Cells[12].Text).Trim() == "نعم")
            //    {
            //        char[] arr = GridView1.Rows[i].Cells[8].Text.ToCharArray();
            //        Array.Reverse(arr);
            //        string dd= new string(arr);
            //        string[] data = dd.Split(';');
            //        string[] data1 = GridView1.Rows[i].Cells[9].Text.Split(';');
            //        //string[] dd = data.ToArray().Reverse();
            //        for (int x=0;x<data.Length;x++)
            //        {
            //            if (data1[x].Trim() == GridView1.Rows[i].Cells[13].Text)
            //                order[Convert.ToInt16(data[x]) - 1]++;
            //        }
            //    }
            //}

            //int[] order1 = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            //for (int i = 0; i < GridView1.Rows.Count; i++)
            //{
            //    //if(i==134)
            //    //{
            //    //    string c = "";
            //    //}
            //    if (HttpUtility.HtmlDecode(GridView1.Rows[i].Cells[15].Text).Trim() == "نعم")
            //    {
            //        char[] arr = GridView1.Rows[i].Cells[8].Text.ToCharArray();
            //        Array.Reverse(arr);
            //        string dd = new string(arr);
            //        string[] data = dd.Split(';');
            //        string[] data1 = GridView1.Rows[i].Cells[9].Text.Split(';');
            //        //string[] dd = data.ToArray().Reverse();
            //        for (int x = 0; x < data.Length; x++)
            //        {
            //            if (data1[x].Trim() == GridView1.Rows[i].Cells[16].Text)
            //                order1[Convert.ToInt16(data[x]) - 1]++;
            //        }
            //    }
            //}

            //DataTable ddd = new DataTable();
            //ddd.Columns.Add("status");
            //ddd.Columns.Add("RCount");
            //ddd.Columns.Add("order");

            //for(int i=0;i<9;i++)
            //{
            //    DataRow r = ddd.NewRow();
            //    r[0] = "دعم";
            //    r[1] = (i + 1);
            //    r[2] = order[i];
            //    ddd.Rows.Add(r);
            //}

            //for (int i = 0; i < 9; i++)
            //{
            //    DataRow r = ddd.NewRow();
            //    r[0] = "مكافأة";
            //    r[1] = (i + 1);
            //    r[2] = order1[i];
            //    ddd.Rows.Add(r);
            //}

            //for (int i = ddd.Rows.Count - 1; i >= 0; i--)
            //{
            //    if (ddd.Rows[i][2].ToString() == "0")
            //        ddd.Rows.RemoveAt(i);
            //}
            //GridView5.DataSource = ddd;
            //GridView5.DataBind();

            conn.Close();
        }

        protected void lnk_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand("Delete From ChequeInfo where ReId='" + row.Cells[0].Text+"' and AcdId="+ row.Cells[22].Text, conn);
            cmd.ExecuteNonQuery();

            TextBox txtSNo = (TextBox)row.FindControl("SChequeNo");
            TextBox txtANo = (TextBox)row.FindControl("AChequeNo");
            HtmlInputGenericControl sdate =(HtmlInputGenericControl)row.FindControl("SChequeD");
            HtmlInputGenericControl adate = (HtmlInputGenericControl)row.FindControl("AChequeD");
            try
            {
                if (txtSNo.Visible && txtANo.Visible && txtSNo.Text!="" && txtANo.Text!="" && sdate.Value!="" && adate.Value!="")
                {
                    cmd = new SqlCommand("Insert into ChequeInfo values('" + row.Cells[0].Text + "'," + txtSNo.Text + ",@sd," + txtANo.Text + ",@ad,"+ row.Cells[22].Text+")", conn);
                    cmd.Parameters.AddWithValue("@sd", Convert.ToDateTime(sdate.Value));
                    cmd.Parameters.AddWithValue("@ad", Convert.ToDateTime(adate.Value));
                    cmd.ExecuteNonQuery();
                }
                else if (txtSNo.Visible && txtSNo.Text != "" && sdate.Value != "")
                {
                    cmd = new SqlCommand("Insert into ChequeInfo (ReId,SChequeNo,SChequeDate,acdid) values('" + row.Cells[0].Text + "'," + txtSNo.Text + ",@sd," + row.Cells[22].Text + ")", conn);
                    cmd.Parameters.AddWithValue("@sd", Convert.ToDateTime(sdate.Value));
                    cmd.ExecuteNonQuery();
                }
                else if (txtANo.Visible && txtANo.Text != "" && adate.Value != "")
                {
                    cmd = new SqlCommand("Insert into ChequeInfo (ReId,AChequeNo,AChequeDate,acdid) values('" + row.Cells[0].Text + "'," + txtANo.Text + ",@ad," + row.Cells[22].Text + ")", conn);
                    cmd.Parameters.AddWithValue("@ad", Convert.ToDateTime(adate.Value));
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    msgDiv.Visible = true;
                    lblMsg.Text = "يجب ادخال جميع المعلومات";

                }
            }
            catch(Exception err)
            {
                msgDiv.Visible = true;
                lblMsg.Text = "يجب ادخال جميع المعلومات";
            }
            conn.Close();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //13,17
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtSNo = (TextBox)e.Row.FindControl("SChequeNo");
                TextBox txtANo = (TextBox)e.Row.FindControl("AChequeNo");
                HtmlInputGenericControl sdate = (HtmlInputGenericControl)e.Row.FindControl("SChequeD");
                HtmlInputGenericControl adate = (HtmlInputGenericControl)e.Row.FindControl("AChequeD");

                if (e.Row.Cells[13].Text == "لا" || e.Row.Cells[13].Text == "&nbsp;")
                {


                    txtSNo.Visible = false;
                    sdate.Visible = false;
                }
                //else
                //{
                //}

                if (e.Row.Cells[17].Text == "لا" || e.Row.Cells[17].Text == "&nbsp;")
                {


                    txtANo.Visible = false;
                    adate.Visible = false;
                }
                //else
                //{
                //    if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                //        conn.Open();

                //    conn.Close();
                //}
                if (e.Row.Cells[13].Text == "نعم" || e.Row.Cells[17].Text == "نعم")
                {
                    if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                        conn.Open();

                    SqlCommand sqlCommand = new SqlCommand("Select * From ChequeInfo where reid='" + e.Row.Cells[0].Text + "' and AcdId=" + e.Row.Cells[22].Text, conn);
                    DataTable dt = new DataTable();
                    dt.Load(sqlCommand.ExecuteReader());
                    if (dt.Rows.Count != 0)
                    {
                        if(dt.Rows[0][2].ToString()!="")
                        {
                            txtSNo.Text = dt.Rows[0][2].ToString();
                            string d = Convert.ToDateTime(dt.Rows[0][3].ToString()).ToString("yyyy-MM-dd");
                            sdate.Value = d;// dr["BOD"].ToString();
                        }
                        if (dt.Rows[0][4].ToString() != "")
                        {
                            txtANo.Text = dt.Rows[0][4].ToString();
                            string d = Convert.ToDateTime(dt.Rows[0][5].ToString()).ToString("yyyy-MM-dd");
                            adate.Value = d;// dr["BOD"].ToString();
                        }

                    }
                }

                conn.Close();

            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            msgDiv.Visible = false;
        }
    }
}