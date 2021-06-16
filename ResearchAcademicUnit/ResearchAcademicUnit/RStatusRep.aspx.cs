using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ResearchAcademicUnit
{
    public partial class RStatusRep : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl divh = (HtmlGenericControl)Page.Master.FindControl("prinOut");
            divh.Visible = false;

            HtmlGenericControl divf = (HtmlGenericControl)Page.Master.FindControl("printfooter");
            divf.Visible = false;

            if (Session["uid"] == null)
                Response.Redirect("Login.aspx");
            Session["backurl"] = "index.aspx";
            if (!IsPostBack)
            {
                fillSetting();
            }
        }

        protected void fillSetting()
        {
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

        protected void btnApply_Click(object sender, EventArgs e)
        {
            Session["inst"] = 0;
            Session["instPend"] = 0;

            int mf = Convert.ToInt16(ddlFromMonth.SelectedValue);
            int y1 = Convert.ToInt16(ddlFromYear.SelectedValue);
            int mt = Convert.ToInt16(ddlToMonth.SelectedValue);
            int y2 = Convert.ToInt16(ddlToYear.SelectedValue);
            lblPeriod.Text = "01/" + mf.ToString("00") + "/" + y1 + " - " + DateTime.DaysInMonth(y2, mt) + "/ " + mt.ToString("00") + "/" + y2;
            int y = DateTime.Now.Year;
            int m = DateTime.Now.Month;

            string cond = "";
            if (y1 == y2)
                cond = " and reyear=" + y1 + " and remonth between " + mf + " and " + (mt);
            else
            {
                cond = " and ((reyear=" + (y1) + " and remonth between " + mf + " and 12) or (reyear=" + y2 + " and remonth between 1 and " + (mt) + ")) ";
            }
            Session["cond"] = cond;
            if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
                conn.Open();
            DataTable dtResearch = new DataTable();
            dtResearch.Columns.Add("RId");
            dtResearch.Columns.Add("REngName");
            dtResearch.Columns.Add("College");
            dtResearch.Columns.Add("PubFirstAuth", typeof(int));
            dtResearch.Columns.Add("PubCoAuth", typeof(int));
            dtResearch.Columns.Add("AccptedFirstAuth", typeof(int));
            dtResearch.Columns.Add("AccptedCoAuth", typeof(int));
            dtResearch.Columns.Add("AuthorStatus");

            SqlCommand cmd;
            cmd = new SqlCommand("select Rid,REngName,College From ResearcherInfo where RStatus='IN' and college=N'الاداب والعلوم' order by College", conn);
            DataTable dtResearcher = new DataTable();
            dtResearcher.Load(cmd.ExecuteReader());
            for (int i = 0; i < dtResearcher.Rows.Count; i++)
            {
                DataRow row = dtResearch.NewRow();
                row[0] = dtResearcher.Rows[i][0];
                row[1] = dtResearcher.Rows[i][1];
                row[2] = dtResearcher.Rows[i][2];
                string sql = @"Select re.ReId,Aff_Auther,rr.AutoId,re.ReStatus
                            from ResearchsInfo RE,Research_Researcher RR
                            where re.ReId=rr.ReId and
                            rr.Rid='" + row[0] + @"' "
                            + cond;
                cmd = new SqlCommand(sql, conn);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count == 0)
                {
                    row[3] = 0;
                    row[4] = 0;
                    row[5] = 0;
                    row[6] = 0;
                    row[7] = "No";
                    dtResearch.Rows.Add(row);
                }
                else
                {
                    int pf = 0;
                    int pc = 0;
                    int af = 0;
                    int ac = 0;
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        switch (dt.Rows[j][3])
                        {
                            case "منشور":
                                cmd = new SqlCommand(@"select 
                                                    ROW_NUMBER() OVER(ORDER BY autoid ASC) AS Row#,RId
                                                    from Research_Researcher where ReId = '" + dt.Rows[j][0] + @"'", conn);
                                DataTable dtR = new DataTable();
                                dtR.Load(cmd.ExecuteReader());
                                if (dtR.Rows[0][1].ToString() == dtResearcher.Rows[i][0].ToString())
                                    pf++;
                                else
                                    pc++;
                                break;

                            case "مقبول للنشر":
                                cmd = new SqlCommand(@"select 
                                                    ROW_NUMBER() OVER(ORDER BY autoid ASC) AS Row#,RId
                                                    from Research_Researcher where ReId = '" + dt.Rows[j][0] + @"'", conn);
                                dtR = new DataTable();
                                dtR.Load(cmd.ExecuteReader());
                                if (dtR.Rows[0][1].ToString() == dtResearcher.Rows[i][0].ToString())
                                    af++;
                                else
                                    ac++;
                                break;
                        }
                    }
                    row[3] = pf;
                    row[4] = pc;
                    row[5] = af;
                    row[6] = ac;
                    row[7] = (pf != 0 ? "OK" : (af != 0 ? "PENDING" : "NO"));
                    dtResearch.Rows.Add(row);
                }
            }
            dtResearch.DefaultView.Sort = "College,PubFirstAuth DESC,AccptedFirstAuth  DESC,PubCoAuth DESC,AccptedCoAuth DESC";
            grdArt.DataSource = dtResearch;
            grdArt.DataBind();

            dtResearch.Rows.Clear();

            cmd = new SqlCommand("select Rid,REngName,College From ResearcherInfo where RStatus='IN' and college=N'الاعلام' order by College", conn);
            dtResearcher = new DataTable();
            dtResearcher.Load(cmd.ExecuteReader());
            for (int i = 0; i < dtResearcher.Rows.Count; i++)
            {
                DataRow row = dtResearch.NewRow();
                row[0] = dtResearcher.Rows[i][0];
                row[1] = dtResearcher.Rows[i][1];
                row[2] = dtResearcher.Rows[i][2];
                string sql = @"Select re.ReId,Aff_Auther,rr.AutoId,re.ReStatus
                            from ResearchsInfo RE,Research_Researcher RR
                            where re.ReId=rr.ReId and
                            rr.Rid='" + row[0] + @"' "
                            + cond;
                cmd = new SqlCommand(sql, conn);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count == 0)
                {
                    row[3] = 0;
                    row[4] = 0;
                    row[5] = 0;
                    row[6] = 0;
                    row[7] = "No";
                    dtResearch.Rows.Add(row);
                }
                else
                {
                    int pf = 0;
                    int pc = 0;
                    int af = 0;
                    int ac = 0;
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        switch (dt.Rows[j][3])
                        {
                            case "منشور":
                                cmd = new SqlCommand(@"select 
                                                    ROW_NUMBER() OVER(ORDER BY autoid ASC) AS Row#,RId
                                                    from Research_Researcher where ReId = '" + dt.Rows[j][0] + @"'", conn);
                                DataTable dtR = new DataTable();
                                dtR.Load(cmd.ExecuteReader());
                                if (dtR.Rows[0][1].ToString() == dtResearcher.Rows[i][0].ToString())
                                    pf++;
                                else
                                    pc++;
                                break;

                            case "مقبول للنشر":
                                cmd = new SqlCommand(@"select 
                                                    ROW_NUMBER() OVER(ORDER BY autoid ASC) AS Row#,RId
                                                    from Research_Researcher where ReId = '" + dt.Rows[j][0] + @"'", conn);
                                dtR = new DataTable();
                                dtR.Load(cmd.ExecuteReader());
                                if (dtR.Rows[0][1].ToString() == dtResearcher.Rows[i][0].ToString())
                                    af++;
                                else
                                    ac++;
                                break;
                        }
                    }
                    row[3] = pf;
                    row[4] = pc;
                    row[5] = af;
                    row[6] = ac;
                    row[7] = (pf != 0 ? "OK" : (af != 0 ? "PENDING" : "NO"));
                    dtResearch.Rows.Add(row);
                }
            }
            dtResearch.DefaultView.Sort = "College,PubFirstAuth DESC,AccptedFirstAuth  DESC,PubCoAuth DESC,AccptedCoAuth DESC";
            grdMedia.DataSource = dtResearch;
            grdMedia.DataBind();

            dtResearch.Rows.Clear();

            cmd = new SqlCommand("select Rid,REngName,College From ResearcherInfo where RStatus='IN' and college=N'الاعمال' order by College", conn);
            dtResearcher = new DataTable();
            dtResearcher.Load(cmd.ExecuteReader());
            for (int i = 0; i < dtResearcher.Rows.Count; i++)
            {
                DataRow row = dtResearch.NewRow();
                row[0] = dtResearcher.Rows[i][0];
                row[1] = dtResearcher.Rows[i][1];
                row[2] = dtResearcher.Rows[i][2];
                string sql = @"Select re.ReId,Aff_Auther,rr.AutoId,re.ReStatus
                            from ResearchsInfo RE,Research_Researcher RR
                            where re.ReId=rr.ReId and
                            rr.Rid='" + row[0] + @"' "
                            + cond;
                cmd = new SqlCommand(sql, conn);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count == 0)
                {
                    row[3] = 0;
                    row[4] = 0;
                    row[5] = 0;
                    row[6] = 0;
                    row[7] = "No";
                    dtResearch.Rows.Add(row);
                }
                else
                {
                    int pf = 0;
                    int pc = 0;
                    int af = 0;
                    int ac = 0;
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        switch (dt.Rows[j][3])
                        {
                            case "منشور":
                                cmd = new SqlCommand(@"select 
                                                    ROW_NUMBER() OVER(ORDER BY autoid ASC) AS Row#,RId
                                                    from Research_Researcher where ReId = '" + dt.Rows[j][0] + @"'", conn);
                                DataTable dtR = new DataTable();
                                dtR.Load(cmd.ExecuteReader());
                                if (dtR.Rows[0][1].ToString() == dtResearcher.Rows[i][0].ToString())
                                    pf++;
                                else
                                    pc++;
                                break;

                            case "مقبول للنشر":
                                cmd = new SqlCommand(@"select 
                                                    ROW_NUMBER() OVER(ORDER BY autoid ASC) AS Row#,RId
                                                    from Research_Researcher where ReId = '" + dt.Rows[j][0] + @"'", conn);
                                dtR = new DataTable();
                                dtR.Load(cmd.ExecuteReader());
                                if (dtR.Rows[0][1].ToString() == dtResearcher.Rows[i][0].ToString())
                                    af++;
                                else
                                    ac++;
                                break;
                        }
                    }
                    row[3] = pf;
                    row[4] = pc;
                    row[5] = af;
                    row[6] = ac;
                    row[7] = (pf != 0 ? "OK" : (af != 0 ? "PENDING" : "NO"));
                    dtResearch.Rows.Add(row);
                }
            }
            DataView dv = dtResearch.DefaultView;
            dv.Sort = "College,PubFirstAuth DESC,AccptedFirstAuth  DESC,PubCoAuth DESC,AccptedCoAuth DESC";
            dtResearch = dv.ToTable();
            //dtResearch.DefaultView.Sort = "College,PubFirstAuth DESC,AccptedFirstAuth  DESC,PubCoAuth DESC,AccptedCoAuth DESC";
            DataTable dt1 = new DataTable();
            dt1 = dtResearch.Clone();
            DataTable dt2 = new DataTable();
            dt2 = dtResearch.Clone();
            int xx = 0;
            foreach (DataRow row in dtResearch.Rows)
            {
                if (xx<20)
                    dt1.Rows.Add(row.ItemArray);
                else
                    dt2.Rows.Add(row.ItemArray);
                xx++;
            }

            grdBusiness.DataSource = dt1;
            grdBusiness.DataBind();

            grdBusiness1.DataSource = dt2;
            grdBusiness1.DataBind();


            dtResearch.Rows.Clear();

            cmd = new SqlCommand("select Rid,REngName,College From ResearcherInfo where RStatus='IN' and college=N'الحقوق' order by College", conn);
            dtResearcher = new DataTable();
            dtResearcher.Load(cmd.ExecuteReader());
            for (int i = 0; i < dtResearcher.Rows.Count; i++)
            {
                DataRow row = dtResearch.NewRow();
                row[0] = dtResearcher.Rows[i][0];
                row[1] = dtResearcher.Rows[i][1];
                row[2] = dtResearcher.Rows[i][2];
                string sql = @"Select re.ReId,Aff_Auther,rr.AutoId,re.ReStatus
                            from ResearchsInfo RE,Research_Researcher RR
                            where re.ReId=rr.ReId and
                            rr.Rid='" + row[0] + @"' "
                            + cond;
                cmd = new SqlCommand(sql, conn);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count == 0)
                {
                    row[3] = 0;
                    row[4] = 0;
                    row[5] = 0;
                    row[6] = 0;
                    row[7] = "No";
                    dtResearch.Rows.Add(row);
                }
                else
                {
                    int pf = 0;
                    int pc = 0;
                    int af = 0;
                    int ac = 0;
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        switch (dt.Rows[j][3])
                        {
                            case "منشور":
                                cmd = new SqlCommand(@"select 
                                                    ROW_NUMBER() OVER(ORDER BY autoid ASC) AS Row#,RId
                                                    from Research_Researcher where ReId = '" + dt.Rows[j][0] + @"'", conn);
                                DataTable dtR = new DataTable();
                                dtR.Load(cmd.ExecuteReader());
                                if (dtR.Rows[0][1].ToString() == dtResearcher.Rows[i][0].ToString())
                                    pf++;
                                else
                                    pc++;
                                break;

                            case "مقبول للنشر":
                                cmd = new SqlCommand(@"select 
                                                    ROW_NUMBER() OVER(ORDER BY autoid ASC) AS Row#,RId
                                                    from Research_Researcher where ReId = '" + dt.Rows[j][0] + @"'", conn);
                                dtR = new DataTable();
                                dtR.Load(cmd.ExecuteReader());
                                if (dtR.Rows[0][1].ToString() == dtResearcher.Rows[i][0].ToString())
                                    af++;
                                else
                                    ac++;
                                break;
                        }
                    }
                    row[3] = pf;
                    row[4] = pc;
                    row[5] = af;
                    row[6] = ac;
                    row[7] = (pf != 0 ? "OK" : (af != 0 ? "PENDING" : "NO"));
                    dtResearch.Rows.Add(row);
                }
            }
            dtResearch.DefaultView.Sort = "College,PubFirstAuth DESC,AccptedFirstAuth  DESC,PubCoAuth DESC,AccptedCoAuth DESC";
            grdLaw.DataSource = dtResearch;
            grdLaw.DataBind();

            dtResearch.Rows.Clear();

            cmd = new SqlCommand("select Rid,REngName,College From ResearcherInfo where RStatus='IN' and college=N'الصيدلة' order by College", conn);
            dtResearcher = new DataTable();
            dtResearcher.Load(cmd.ExecuteReader());
            for (int i = 0; i < dtResearcher.Rows.Count; i++)
            {
                DataRow row = dtResearch.NewRow();
                row[0] = dtResearcher.Rows[i][0];
                row[1] = dtResearcher.Rows[i][1];
                row[2] = dtResearcher.Rows[i][2];
                string sql = @"Select re.ReId,Aff_Auther,rr.AutoId,re.ReStatus
                            from ResearchsInfo RE,Research_Researcher RR
                            where re.ReId=rr.ReId and
                            rr.Rid='" + row[0] + @"' "
                            + cond;
                cmd = new SqlCommand(sql, conn);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count == 0)
                {
                    row[3] = 0;
                    row[4] = 0;
                    row[5] = 0;
                    row[6] = 0;
                    row[7] = "No";
                    dtResearch.Rows.Add(row);
                }
                else
                {
                    int pf = 0;
                    int pc = 0;
                    int af = 0;
                    int ac = 0;
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        switch (dt.Rows[j][3])
                        {
                            case "منشور":
                                cmd = new SqlCommand(@"select 
                                                    ROW_NUMBER() OVER(ORDER BY autoid ASC) AS Row#,RId
                                                    from Research_Researcher where ReId = '" + dt.Rows[j][0] + @"'", conn);
                                DataTable dtR = new DataTable();
                                dtR.Load(cmd.ExecuteReader());
                                if (dtR.Rows[0][1].ToString() == dtResearcher.Rows[i][0].ToString())
                                    pf++;
                                else
                                    pc++;
                                break;

                            case "مقبول للنشر":
                                cmd = new SqlCommand(@"select 
                                                    ROW_NUMBER() OVER(ORDER BY autoid ASC) AS Row#,RId
                                                    from Research_Researcher where ReId = '" + dt.Rows[j][0] + @"'", conn);
                                dtR = new DataTable();
                                dtR.Load(cmd.ExecuteReader());
                                if (dtR.Rows[0][1].ToString() == dtResearcher.Rows[i][0].ToString())
                                    af++;
                                else
                                    ac++;
                                break;
                        }
                    }
                    row[3] = pf;
                    row[4] = pc;
                    row[5] = af;
                    row[6] = ac;
                    row[7] = (pf != 0 ? "OK" : (af != 0 ? "PENDING" : "NO"));
                    dtResearch.Rows.Add(row);
                }
            }
            dtResearch.DefaultView.Sort = "College,PubFirstAuth DESC,AccptedFirstAuth  DESC,PubCoAuth DESC,AccptedCoAuth DESC";
            grdPharmacy.DataSource = dtResearch;
            grdPharmacy.DataBind();

            dtResearch.Rows.Clear();

            cmd = new SqlCommand("select Rid,REngName,College From ResearcherInfo where RStatus='IN' and college=N'العلوم التربوية' order by College", conn);
            dtResearcher = new DataTable();
            dtResearcher.Load(cmd.ExecuteReader());
            for (int i = 0; i < dtResearcher.Rows.Count; i++)
            {
                DataRow row = dtResearch.NewRow();
                row[0] = dtResearcher.Rows[i][0];
                row[1] = dtResearcher.Rows[i][1];
                row[2] = dtResearcher.Rows[i][2];
                string sql = @"Select re.ReId,Aff_Auther,rr.AutoId,re.ReStatus
                            from ResearchsInfo RE,Research_Researcher RR
                            where re.ReId=rr.ReId and
                            rr.Rid='" + row[0] + @"' "
                            + cond;
                cmd = new SqlCommand(sql, conn);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count == 0)
                {
                    row[3] = 0;
                    row[4] = 0;
                    row[5] = 0;
                    row[6] = 0;
                    row[7] = "No";
                    dtResearch.Rows.Add(row);
                }
                else
                {
                    int pf = 0;
                    int pc = 0;
                    int af = 0;
                    int ac = 0;
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        switch (dt.Rows[j][3])
                        {
                            case "منشور":
                                cmd = new SqlCommand(@"select 
                                                    ROW_NUMBER() OVER(ORDER BY autoid ASC) AS Row#,RId
                                                    from Research_Researcher where ReId = '" + dt.Rows[j][0] + @"'", conn);
                                DataTable dtR = new DataTable();
                                dtR.Load(cmd.ExecuteReader());
                                if (dtR.Rows[0][1].ToString() == dtResearcher.Rows[i][0].ToString())
                                    pf++;
                                else
                                    pc++;
                                break;

                            case "مقبول للنشر":
                                cmd = new SqlCommand(@"select 
                                                    ROW_NUMBER() OVER(ORDER BY autoid ASC) AS Row#,RId
                                                    from Research_Researcher where ReId = '" + dt.Rows[j][0] + @"'", conn);
                                dtR = new DataTable();
                                dtR.Load(cmd.ExecuteReader());
                                if (dtR.Rows[0][1].ToString() == dtResearcher.Rows[i][0].ToString())
                                    af++;
                                else
                                    ac++;
                                break;
                        }
                    }
                    row[3] = pf;
                    row[4] = pc;
                    row[5] = af;
                    row[6] = ac;
                    row[7] = (pf != 0 ? "OK" : (af != 0 ? "PENDING" : "NO"));
                    dtResearch.Rows.Add(row);
                }
            }
            dtResearch.DefaultView.Sort = "College,PubFirstAuth DESC,AccptedFirstAuth  DESC,PubCoAuth DESC,AccptedCoAuth DESC";
            grdEduSci.DataSource = dtResearch;
            grdEduSci.DataBind();

            dtResearch.Rows.Clear();

            cmd = new SqlCommand("select Rid,REngName,College From ResearcherInfo where RStatus='IN' and college=N'العمارة والتصميم' order by College", conn);
            dtResearcher = new DataTable();
            dtResearcher.Load(cmd.ExecuteReader());
            for (int i = 0; i < dtResearcher.Rows.Count; i++)
            {
                DataRow row = dtResearch.NewRow();
                row[0] = dtResearcher.Rows[i][0];
                row[1] = dtResearcher.Rows[i][1];
                row[2] = dtResearcher.Rows[i][2];
                string sql = @"Select re.ReId,Aff_Auther,rr.AutoId,re.ReStatus
                            from ResearchsInfo RE,Research_Researcher RR
                            where re.ReId=rr.ReId and
                            rr.Rid='" + row[0] + @"' "
                            + cond;
                cmd = new SqlCommand(sql, conn);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count == 0)
                {
                    row[3] = 0;
                    row[4] = 0;
                    row[5] = 0;
                    row[6] = 0;
                    row[7] = "No";
                    dtResearch.Rows.Add(row);
                }
                else
                {
                    int pf = 0;
                    int pc = 0;
                    int af = 0;
                    int ac = 0;
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        switch (dt.Rows[j][3])
                        {
                            case "منشور":
                                cmd = new SqlCommand(@"select 
                                                    ROW_NUMBER() OVER(ORDER BY autoid ASC) AS Row#,RId
                                                    from Research_Researcher where ReId = '" + dt.Rows[j][0] + @"'", conn);
                                DataTable dtR = new DataTable();
                                dtR.Load(cmd.ExecuteReader());
                                if (dtR.Rows[0][1].ToString() == dtResearcher.Rows[i][0].ToString())
                                    pf++;
                                else
                                    pc++;
                                break;

                            case "مقبول للنشر":
                                cmd = new SqlCommand(@"select 
                                                    ROW_NUMBER() OVER(ORDER BY autoid ASC) AS Row#,RId
                                                    from Research_Researcher where ReId = '" + dt.Rows[j][0] + @"'", conn);
                                dtR = new DataTable();
                                dtR.Load(cmd.ExecuteReader());
                                if (dtR.Rows[0][1].ToString() == dtResearcher.Rows[i][0].ToString())
                                    af++;
                                else
                                    ac++;
                                break;
                        }
                    }
                    row[3] = pf;
                    row[4] = pc;
                    row[5] = af;
                    row[6] = ac;
                    row[7] = (pf != 0 ? "OK" : (af != 0 ? "PENDING" : "NO"));
                    dtResearch.Rows.Add(row);
                }
            }
            dtResearch.DefaultView.Sort = "College,PubFirstAuth DESC,AccptedFirstAuth  DESC,PubCoAuth DESC,AccptedCoAuth DESC";
            grdArchDesign.DataSource = dtResearch;
            grdArchDesign.DataBind();

            dtResearch.Rows.Clear();

            cmd = new SqlCommand("select Rid,REngName,College From ResearcherInfo where RStatus='IN' and college=N'الهندسة' order by College", conn);
            dtResearcher = new DataTable();
            dtResearcher.Load(cmd.ExecuteReader());
            for (int i = 0; i < dtResearcher.Rows.Count; i++)
            {
                DataRow row = dtResearch.NewRow();
                row[0] = dtResearcher.Rows[i][0];
                row[1] = dtResearcher.Rows[i][1];
                row[2] = dtResearcher.Rows[i][2];
                string sql = @"Select re.ReId,Aff_Auther,rr.AutoId,re.ReStatus
                            from ResearchsInfo RE,Research_Researcher RR
                            where re.ReId=rr.ReId and
                            rr.Rid='" + row[0] + @"' "
                            + cond;
                cmd = new SqlCommand(sql, conn);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count == 0)
                {
                    row[3] = 0;
                    row[4] = 0;
                    row[5] = 0;
                    row[6] = 0;
                    row[7] = "No";
                    dtResearch.Rows.Add(row);
                }
                else
                {
                    int pf = 0;
                    int pc = 0;
                    int af = 0;
                    int ac = 0;
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        switch (dt.Rows[j][3])
                        {
                            case "منشور":
                                cmd = new SqlCommand(@"select 
                                                    ROW_NUMBER() OVER(ORDER BY autoid ASC) AS Row#,RId
                                                    from Research_Researcher where ReId = '" + dt.Rows[j][0] + @"'", conn);
                                DataTable dtR = new DataTable();
                                dtR.Load(cmd.ExecuteReader());
                                if (dtR.Rows[0][1].ToString() == dtResearcher.Rows[i][0].ToString())
                                    pf++;
                                else
                                    pc++;
                                break;

                            case "مقبول للنشر":
                                cmd = new SqlCommand(@"select 
                                                    ROW_NUMBER() OVER(ORDER BY autoid ASC) AS Row#,RId
                                                    from Research_Researcher where ReId = '" + dt.Rows[j][0] + @"'", conn);
                                dtR = new DataTable();
                                dtR.Load(cmd.ExecuteReader());
                                if (dtR.Rows[0][1].ToString() == dtResearcher.Rows[i][0].ToString())
                                    af++;
                                else
                                    ac++;
                                break;
                        }
                    }
                    row[3] = pf;
                    row[4] = pc;
                    row[5] = af;
                    row[6] = ac;
                    row[7] = (pf != 0 ? "OK" : (af != 0 ? "PENDING" : "NO"));
                    dtResearch.Rows.Add(row);
                }
            }
            dtResearch.DefaultView.Sort = "College,PubFirstAuth DESC,AccptedFirstAuth  DESC,PubCoAuth DESC,AccptedCoAuth DESC";
            grdEngineering.DataSource = dtResearch;
            grdEngineering.DataBind();

            dtResearch.Rows.Clear();

            cmd = new SqlCommand("select Rid,REngName,College From ResearcherInfo where RStatus='IN' and college=N'تكنولوجيا المعلومات' order by College", conn);
            dtResearcher = new DataTable();
            dtResearcher.Load(cmd.ExecuteReader());
            for (int i = 0; i < dtResearcher.Rows.Count; i++)
            {
                DataRow row = dtResearch.NewRow();
                row[0] = dtResearcher.Rows[i][0];
                row[1] = dtResearcher.Rows[i][1];
                row[2] = dtResearcher.Rows[i][2];
                string sql = @"Select re.ReId,Aff_Auther,rr.AutoId,re.ReStatus
                            from ResearchsInfo RE,Research_Researcher RR
                            where re.ReId=rr.ReId and
                            rr.Rid='" + row[0] + @"' "
                            + cond;
                cmd = new SqlCommand(sql, conn);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count == 0)
                {
                    row[3] = 0;
                    row[4] = 0;
                    row[5] = 0;
                    row[6] = 0;
                    row[7] = "No";
                    dtResearch.Rows.Add(row);
                }
                else
                {
                    int pf = 0;
                    int pc = 0;
                    int af = 0;
                    int ac = 0;
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        switch (dt.Rows[j][3])
                        {
                            case "منشور":
                                cmd = new SqlCommand(@"select 
                                                    ROW_NUMBER() OVER(ORDER BY autoid ASC) AS Row#,RId
                                                    from Research_Researcher where ReId = '" + dt.Rows[j][0] + @"'", conn);
                                DataTable dtR = new DataTable();
                                dtR.Load(cmd.ExecuteReader());
                                if (dtR.Rows[0][1].ToString() == dtResearcher.Rows[i][0].ToString())
                                    pf++;
                                else
                                    pc++;
                                break;

                            case "مقبول للنشر":
                                cmd = new SqlCommand(@"select 
                                                    ROW_NUMBER() OVER(ORDER BY autoid ASC) AS Row#,RId
                                                    from Research_Researcher where ReId = '" + dt.Rows[j][0] + @"'", conn);
                                dtR = new DataTable();
                                dtR.Load(cmd.ExecuteReader());
                                if (dtR.Rows[0][1].ToString() == dtResearcher.Rows[i][0].ToString())
                                    af++;
                                else
                                    ac++;
                                break;
                        }
                    }
                    row[3] = pf;
                    row[4] = pc;
                    row[5] = af;
                    row[6] = ac;
                    row[7] = (pf != 0 ? "OK" : (af != 0 ? "PENDING" : "NO"));
                    dtResearch.Rows.Add(row);
                }
            }
            dtResearch.DefaultView.Sort = "College,PubFirstAuth DESC,AccptedFirstAuth  DESC,PubCoAuth DESC,AccptedCoAuth DESC";
            grdIT.DataSource = dtResearch;
            grdIT.DataBind();


            conn.Close();
            getScale();
            calFaculty();

            AllDivs.Visible = true;
        }

        protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView grd = (GridView)sender;

        }

        protected void grdArt_DataBound(object sender, EventArgs e)
        {
            GridView grd = (GridView)sender;
            GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);

            TableHeaderCell cell = new TableHeaderCell();
            cell.Text = "Name";
            cell.ColumnSpan = 1;
            cell.RowSpan = 2;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Published";
            cell.ColumnSpan = 2;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.ColumnSpan = 2;
            cell.Text = "Accepted";
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Author Status";
            cell.ColumnSpan = 1;
            cell.RowSpan = 2;
            row.Controls.Add(cell);

            row.BackColor = ColorTranslator.FromHtml("#921A1D");
            grd.HeaderRow.Parent.Controls.AddAt(0, row);
            //grd.HeaderRow.Parent.Cells[0].RowSpan = 2;
            int instTarget = 0;
            int instPending = 0;
            //double allinst = 0;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (GridView1.Rows[i].Cells[4].Text == "OK")
                {
                    GridView1.Rows[i].BackColor = Color.LightGray;
                }
            }

            for (int i=0;i<grd.Rows.Count;i++)
            {
                if (grd.Rows[i].Cells[5].Text == "OK")
                {
                    grd.Rows[i].BackColor = Color.LightGray;
                    instTarget++;
                }
                else if (grd.Rows[i].Cells[5].Text == "PENDING")
                {
                    grd.Rows[i].BackColor = Color.FromArgb(252, 177, 49);
                    instPending++;
                }
            }
            Session["inst"] = Convert.ToDouble(Session["inst"]) + Convert.ToDouble(instTarget);
            Session["instPend"] = Convert.ToDouble(Session["instPend"]) + Convert.ToDouble(instPending);

        }

        protected void getScale()
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM  OldYears", conn);

            DataTable dt = new DataTable();
            dt.Columns.Add("AcademicYear");
            dt.Columns.Add("NoRes", typeof(int));
            dt.Columns.Add("NoInst", typeof(int));
            dt.Load(cmd.ExecuteReader());
            //DataRow row = dt.NewRow();
            //row[0] = "2017/2018";
            //row[1] = 49;
            //row[2] = 173;
            //dt.Rows.Add(row);

            //row = dt.NewRow();
            //row[0] = "2018/2019";
            //row[1] = 53;
            //row[2] = 175;
            //dt.Rows.Add(row);
            cmd = new SqlCommand(@"SELECT distinct re.[ReId]
  FROM ResearchsInfo re,ResearcherInfo ri,Research_Researcher rr
  where re.ReId=rr.ReId and ri.RId=rr.RId
  and re.ReStatus=N'منشور' 
and ((ReYear="+(Convert.ToInt16(ddlFromYear.SelectedValue)-2)+ @" and ReMonth between 9 and 12) or (ReYear=" + (Convert.ToInt16(ddlFromYear.SelectedValue) - 1) + @" and ReMonth between 1 and 8))" , conn);
            DataTable dtt = new DataTable();
            dtt.Load(cmd.ExecuteReader());
            dt.Rows[0][1] = dtt.Rows.Count;

            cmd = new SqlCommand(@"SELECT distinct re.[ReId]
  FROM ResearchsInfo re,ResearcherInfo ri,Research_Researcher rr
  where re.ReId=rr.ReId and ri.RId=rr.RId
  and re.ReStatus=N'منشور' 
and ((ReYear=" + (Convert.ToInt16(ddlFromYear.SelectedValue) - 1) + @" and ReMonth between 9 and 12) or (ReYear=" + (Convert.ToInt16(ddlFromYear.SelectedValue)) + @" and ReMonth between 1 and 8))", conn);
            DataTable dtt1 = new DataTable();
            dtt1.Load(cmd.ExecuteReader());
            dt.Rows[1][1] = dtt1.Rows.Count;

            cmd = new SqlCommand(@"SELECT distinct re.[ReId]
  FROM ResearchsInfo re,ResearcherInfo ri,Research_Researcher rr
  where re.ReId=rr.ReId and ri.RId=rr.RId
  and re.ReStatus=N'منشور' " + Session["cond"], conn);
            DataTable dtt2 = new DataTable();
            dtt2.Load(cmd.ExecuteReader());
            dt.Rows[2][0] = ddlFromYear.SelectedValue + "/" + (Convert.ToInt16(ddlFromYear.SelectedValue) + 1);
            dt.Rows[2][1] = dtt2.Rows.Count;

            cmd = new SqlCommand(@"SELECT distinct re.[ReId]
  FROM ResearchsInfo re,ResearcherInfo ri,Research_Researcher rr
  where re.ReId=rr.ReId and ri.RId=rr.RId
  and re.ReStatus=N'مقبول للنشر' " + Session["cond"], conn);
            DataTable dtt3 = new DataTable();
            dtt3.Load(cmd.ExecuteReader());
            dt.Rows[2][0] = ddlFromYear.SelectedValue + "/" + (Convert.ToInt16(ddlFromYear.SelectedValue) + 1) + " (Accepted=" + dtt3.Rows.Count + ")";
            //dt.Rows[2][1] = dtt2.Rows.Count;


            // to calculate expected year
            //DataRow row = dt.NewRow();
            //row[0] = ddlFromYear.SelectedValue + "/" + (Convert.ToInt16(ddlFromYear.SelectedValue) + 1);
            //row[1] =Convert.ToInt16((Math.Ceiling(dtt2.Rows.Count/5.0))*10);
            //row[2] = dt.Rows[2][2];
            //dt.Rows.Add(row);
            //row = dt.NewRow();
            //row[0] = "2019/2020";
            //row[1] = 110; منشور / 5) *  10 ) roundup
            //row[2] = 183;
            //dt.Rows.Add(row);



            StringBuilder strScript = new StringBuilder();
            strScript.Append(@"<script type='text/javascript'>  
                    google.load('visualization', '1', {packages: ['corechart']});</script>  
                    <script type='text/javascript'>  
                    function drawVisualization() {         
                    var data = google.visualization.arrayToDataTable([
                ['Year','Research performance', {type: 'number', role: 'annotation'}, 'Number of Instructors', {type: 'number', role: 'annotation'}],");
            foreach (DataRow row1 in dt.Rows)
            {
                strScript.Append("['" + row1["AcademicYear"] + "'," + row1["NoRes"] + "," + row1["NoRes"] + "," + row1["NoInst"] + "," + row1["NoInst"] + "],");
            }
            strScript.Remove(strScript.Length - 1, 1);
            strScript.Append("]);");

            strScript.Append("var options = {chartarea:{backgroundColor: {stroke: '#4322c0',strokeWidth: 3}},backgroundColor: '#d2d5d5', legend: 'left',lineWidth: 10,pointSize: 20,targetAxisIndex: 1,vAxis: { gridlines: {count: 0}}};");
            strScript.Append(" var chart = new google.visualization.LineChart(document.getElementById('chart_div'));  chart.draw(data, options); } google.setOnLoadCallback(drawVisualization);");
            strScript.Append(" </script>");
            ltScripts.Text += strScript.ToString();


            StringBuilder strScript1 = new StringBuilder();
            strScript1.Append(@"<script type='text/javascript'>  
                    google.load('visualization', '1', {packages: ['corechart']});</script>  
                    <script type='text/javascript'>  
                    function drawVisualization() {         
                    var data = google.visualization.arrayToDataTable([
                        ['Year',
                        'Number of instructors per paper',{type: 'number', role: 'annotation'},
                        'Percentage of instructors per paper'],");
            //foreach (DataRow row1 in dt.Rows)
            //{
            strScript1.Append("['"
                            + dt.Rows[0]["AcademicYear"] + "',"
                            + Math.Round(Convert.ToDouble(dt.Rows[0]["NoInst"]) / Convert.ToDouble(dt.Rows[0]["NoRes"]), 1) + ","
                            + Math.Round(Convert.ToDouble(dt.Rows[0]["NoInst"]) / Convert.ToDouble(dt.Rows[0]["NoRes"]), 1) + ","
                            //+ Math.Round(Convert.ToDouble(dt.Rows[0]["NoRes"]) / Convert.ToDouble(dt.Rows[0]["NoInst"]), 2) + ","
                            + Math.Round(Convert.ToDouble(dt.Rows[0]["NoRes"]) / Convert.ToDouble(dt.Rows[0]["NoInst"]) , 2) + "],");

            strScript1.Append("['"
                            + dt.Rows[1]["AcademicYear"] + "',"
                            + Math.Round(Convert.ToDouble(dt.Rows[1]["NoInst"]) / Convert.ToDouble(dt.Rows[1]["NoRes"]), 1) + ","
                            + Math.Round(Convert.ToDouble(dt.Rows[1]["NoInst"]) / Convert.ToDouble(dt.Rows[1]["NoRes"]), 1) + ","
                            //+ Math.Round(Convert.ToDouble(dt.Rows[1]["NoRes"]) / Convert.ToDouble(dt.Rows[1]["NoInst"]), 2) + ","
                            + Math.Round(Convert.ToDouble(dt.Rows[1]["NoRes"]) / Convert.ToDouble(dt.Rows[1]["NoInst"]) , 2) + "],");
            //strScript1.Append("['"
            //                + dt.Rows[3]["AcademicYear"] + "',"
            //                + Math.Round(Convert.ToDouble(dt.Rows[3]["NoInst"]) / Convert.ToDouble(dt.Rows[3]["NoRes"]), 1) + ","
            //                + Math.Round(Convert.ToDouble(dt.Rows[3]["NoInst"]) / Convert.ToDouble(dt.Rows[3]["NoRes"]), 1) + ","
            //                //+ Math.Round(Convert.ToDouble(dt.Rows[3]["NoRes"]) / Convert.ToDouble(dt.Rows[3]["NoInst"]), 2) + ","
            //                + Math.Round(Convert.ToDouble(dt.Rows[3]["NoRes"]) / Convert.ToDouble(dt.Rows[3]["NoInst"]) , 2) + "],");
            //}
            strScript1.Remove(strScript1.Length - 1, 1);
            strScript1.Append("]);");
            strScript1.Append(
            @"var formatPercent = new google.visualization.NumberFormat({pattern: '0%'});
            var view = new google.visualization.DataView(data);
view.setColumns([0, 1,2,3,{
      calc: function(dt, row)
        {
            return formatPercent.formatValue(dt.getValue(row, 3));
        },
      type: 'string',
      role: 'annotation'
}
  ]);
            ");
            strScript1.Append("var options = {width:'100%',chartArea: { width: '90%'},backgroundColor: '#d2d5d5',title: 'INSTRUCTORS PERFORMANCE ACCORDING TO ACADEMIC YEAR ', legend: {maxLines: 3, position: 'top'},series: {0: { color: '#808785' },1: { color: '#a61702' }},chartArea: { width: '90%'},vAxis: { gridlines: {count: 0}}};");
            strScript1.Append(" var chart = new google.visualization.ColumnChart(document.getElementById('chart_div1'));  chart.draw(view, options); } google.setOnLoadCallback(drawVisualization);");
            strScript1.Append(" </script>");
            ltScripts.Text += strScript1.ToString();



        }

        protected void calFaculty()
        {
            if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
                conn.Open();

            DataTable dt = new DataTable();
            dt.Columns.Add("faculty");
            dt.Columns.Add("inst");
            dt.Columns.Add("PubPaper");
            dt.Columns.Add("AccPaper");
            dt.Columns.Add("FStatus");
            dt.Columns.Add("SHORTAGE");
            dt.Columns.Add("PubPer");
            dt.Columns.Add("InstAch");
            dt.Columns.Add("InstAchPer");
            
            string[,] facultyName = { { "grdArt", "Arts and Sciences" },
                { "grdMedia" , "Media"}, 
                { "grdBusiness" , "Business"}, 
                { "grdLaw" , "Law"}, 
                { "grdPharmacy" , "Pharmacy"}, 
                { "grdEduSci" , "Educational Sciences"}, 
                { "grdArchDesign" , "Architecture and Design"}, 
                { "grdEngineering" , "Engineering"}, 
                { "grdIT" , "Information Technology"} };
            for (int i = 0; i <= 8; i++)
            {
                GridView grd = (GridView)AllDivs.FindControl(facultyName[i,0]);
                DataRow row = dt.NewRow();
                row[0] = facultyName[i, 1];
                row[1] =(facultyName[i, 0]== "grdBusiness" ? (grdBusiness.Rows.Count+grdBusiness1.Rows.Count): grd.Rows.Count);

                string sql = @"SELECT 
                          count(distinct rr.reid) PaperCount,ReStatus
                          FROM ResearchsInfo Re,Research_Researcher RR,ResearcherInfo RI,Faculty f
                          where 
                           
                          Re.ReId=rr.ReId and rr.RId=ri.RId and ri.college=f.collegename " +
              Session["cond"] + @"
                          and f.CNameE='"+ facultyName[i, 1] + "'" +
                          " group by ReStatus";
                SqlCommand cmd = new SqlCommand(sql, conn);
                DataTable dtBestCollege = new DataTable();
                dtBestCollege.Load(cmd.ExecuteReader());



                int pubPaper = 0;
                int accPaper = 0;
                int InstTarget = 0;
                switch(dtBestCollege.Rows.Count)
                {
                    case 1:
                        if (dtBestCollege.Rows[0][1].ToString() == "منشور")
                            pubPaper = Convert.ToInt16(dtBestCollege.Rows[0][0]);
                        else
                            accPaper = Convert.ToInt16(dtBestCollege.Rows[0][0]);
                        break;
                    case 2:
                        accPaper = Convert.ToInt16(dtBestCollege.Rows[0][0]);
                        pubPaper = Convert.ToInt16(dtBestCollege.Rows[1][0]);
                        break;
                }
                for(int j=0;j< grd.Rows.Count;j++)
                {
                    //pubPaper += Convert.ToInt16(grd.Rows[j].Cells[1].Text) + Convert.ToInt16(grd.Rows[j].Cells[2].Text);
                    //accPaper += Convert.ToInt16(grd.Rows[j].Cells[3].Text) + Convert.ToInt16(grd.Rows[j].Cells[4].Text);
                    if (grd.Rows[j].Cells[5].Text == "OK")
                        InstTarget++;
                }

                if(facultyName[i, 1]== "Business")
                {
                    for (int j = 0; j < grdBusiness1.Rows.Count; j++)
                    {
                        //pubPaper += Convert.ToInt16(grd.Rows[j].Cells[1].Text) + Convert.ToInt16(grd.Rows[j].Cells[2].Text);
                        //accPaper += Convert.ToInt16(grd.Rows[j].Cells[3].Text) + Convert.ToInt16(grd.Rows[j].Cells[4].Text);
                        if (grdBusiness1.Rows[j].Cells[5].Text == "OK")
                            InstTarget++;
                    }
                }
                row[2] = pubPaper;
                row[3] = accPaper;
                if (Convert.ToInt16(row[1].ToString()) <= (pubPaper + accPaper))
                    row[4] = "OK";
                else
                    row[4] = "NO";

                row[5] = (Convert.ToInt16(row[1].ToString()) - (pubPaper + accPaper)>0?( Convert.ToInt16(row[1].ToString()) - (pubPaper + accPaper)).ToString(): "NO SHORTAGE");
                row[6] = (Math.Round((pubPaper + accPaper) / Convert.ToDouble(row[1].ToString()), 2) > 1 ? 100 : Math.Round((pubPaper + accPaper) / Convert.ToDouble(row[1].ToString()), 2)*100);
                row[7] = InstTarget;
                row[8] = Math.Round(InstTarget / Convert.ToDouble(row[1].ToString()), 2)*100;
                dt.Rows.Add(row);
            }
            GridView1.DataSource = dt;
            GridView1.DataBind();

            StringBuilder strScript2 = new StringBuilder();
            double c = Convert.ToDouble(Session["inst"]);
            double d = Convert.ToDouble(Session["instPend"]);
            double e =Convert.ToDouble(Session["allinst"])-(c+d);
            double ach =Math.Round( Convert.ToDouble(Session["inst"])/ Convert.ToDouble(Session["allinst"]), 2);
            double pend = Math.Round(Convert.ToDouble(Session["instPend"]) / Convert.ToDouble(Session["allinst"]), 2);
            double notach = Math.Round(e/ Convert.ToDouble(Session["allinst"]), 2);
            strScript2.Append(@"<script type='text/javascript'>  
                    google.load('visualization', '1', {packages: ['corechart']});</script>  
                    <script type='text/javascript'>  
                    function drawVisualization() {         
                    var data = google.visualization.arrayToDataTable([
                ['Performance','Count', {type: 'number', role: 'annotation'}],");
                strScript2.Append("['ACHIEVED TARGET'," + ach + "," + ach + "],");
            strScript2.Append("['PENDING'," + pend + "," + pend + "],");
            strScript2.Append("['NOT ACHIEVED'," + notach + "," + notach + "],");
            strScript2.Remove(strScript2.Length - 1, 1);
            strScript2.Append("]);");

            strScript2.Append("var options = {chartarea:{width:'100%'},backgroundColor: '#d2d5d5',title: 'INSTRUCTORS PERFORMANCE (FIRST SEMESTER 2019/2020)',legend: 'left',lineWidth: 10,pointSize: 20,targetAxisIndex: 1,vAxis: { gridlines: {count: 0}}};");
            strScript2.Append(" var chart = new google.visualization.PieChart(document.getElementById('chart_div2'));  chart.draw(data, options); } google.setOnLoadCallback(drawVisualization);");
            strScript2.Append(" </script>");
            ltScripts.Text += strScript2.ToString();

            StringBuilder strScript = new StringBuilder();
            strScript.Append(@"<script type='text/javascript'>  
                    google.load('visualization', '1', {packages: ['corechart']});</script>  
                    <script type='text/javascript'>  
                    function drawVisualization() {         
                    var data = google.visualization.arrayToDataTable([
                ['Faculty','No of Instructor',  {type: 'number' ,role:'annotation'},
                 'Number Instructors Achieved Target',  {type: 'number' ,role:'annotation'},'Perc'],");
            for(int i=0;i<GridView1.Rows.Count;i++)
            {
                string c1 =  (Convert.ToDouble(GridView1.Rows[i].Cells[8].Text.Substring(0, GridView1.Rows[i].Cells[8].Text.Length - 2)) / 100).ToString();
                strScript.Append("['" + GridView1.Rows[i].Cells[0].Text + "'," + GridView1.Rows[i].Cells[1].Text + "," + GridView1.Rows[i].Cells[1].Text + "," + GridView1.Rows[i].Cells[7].Text + "," + GridView1.Rows[i].Cells[7].Text + "," + c1 + "],");
            }
            strScript.Remove(strScript.Length - 1, 1);
            strScript.Append("]);");
            strScript.Append(
            @"var formatPercent = new google.visualization.NumberFormat({pattern: '0%'});
            var view = new google.visualization.DataView(data);
view.setColumns([0, 1, 2, 3,4,
5,{
      calc: function(dt, row)
        {
            return formatPercent.formatValue(dt.getValue(row, 5));
        },
      type: 'string',
      role: 'annotation'
}
  ]);
            ");
            strScript.Append("var options = {annotations: {textStyle: {color: '#871b47'}},hAxis: { textPosition: 'none' ,gridlines:{color: 'transparent'}},chartArea: { width: '50%'},backgroundColor: '#d2d5d5',series: {0: { color: '#808785' },1: { color: '#a61702' },2: { color: '#d2d5d5' }},title: 'PERCENTAGE OF PUBLISHING ACCORDING TO INSTRUCTOR',isStacked: 'true',legend: 'left',lineWidth: 10,pointSize: 20,targetAxisIndex: 1,vAxis: { gridlines: {count: 0}}};");
            strScript.Append(" var chart = new google.visualization.BarChart(document.getElementById('chart_div3'));  chart.draw(view, options); } google.setOnLoadCallback(drawVisualization);");
            strScript.Append(" </script>");
            ltScripts.Text += strScript.ToString();

            StringBuilder strScript3 = new StringBuilder();
            strScript3.Append(@"<script type='text/javascript'>  
                    google.load('visualization', '1', {packages: ['corechart']});</script>  
                    <script type='text/javascript'>  
                    function drawVisualization() {         
                    var data = google.visualization.arrayToDataTable([
                ['Faculty','No of Instructor',  {type: 'number' ,role:'annotation'},
                 'Number Instructors Achieved Target',  {type: 'number' ,role:'annotation'},'Perc'],");
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                int t1 = Convert.ToInt16(GridView1.Rows[i].Cells[2].Text);
                int t2 = Convert.ToInt16(GridView1.Rows[i].Cells[3].Text);
                string c1 = (Convert.ToDouble(GridView1.Rows[i].Cells[6].Text.Substring(0, GridView1.Rows[i].Cells[6].Text.Length - 2)) / 100).ToString();
                strScript3.Append("['" + GridView1.Rows[i].Cells[0].Text + "'," + GridView1.Rows[i].Cells[1].Text + "," + GridView1.Rows[i].Cells[1].Text + "," + (t1 + t2) + "," + (t1 + t2) + "," + c1 + "],");
            }
            strScript.Remove(strScript3.Length - 1, 1);
            strScript3.Append("]);");
            strScript3.Append(
            @"var formatPercent = new google.visualization.NumberFormat({pattern: '0%'});
            var view = new google.visualization.DataView(data);
view.setColumns([0, 1, 2, 3,4,
5,{
      calc: function(dt, row)
        {
            return formatPercent.formatValue(dt.getValue(row, 5));
        },
      type: 'string',
      role: 'annotation'
}
  ]);
            ");
            strScript3.Append("var options = {annotations: {textStyle: {color: '#871b47'}},hAxis: { textPosition: 'none' ,gridlines:{color: 'transparent'}},chartArea: { width: '50%'},backgroundColor: '#d2d5d5',series: {0: { color: '#808785' },1: { color: '#a61702' },2: { color: '#d2d5d5' }},title: 'PERCENTAGE OF PUBLISHING ACCORDING TO PAPER',isStacked: 'true',legend: 'left',lineWidth: 10,pointSize: 20,targetAxisIndex: 1,vAxis: { gridlines: {count: 0}}};");
            strScript3.Append(" var chart = new google.visualization.BarChart(document.getElementById('chart_div4'));  chart.draw(view, options); } google.setOnLoadCallback(drawVisualization);");
            strScript3.Append(" </script>");
            ltScripts.Text += strScript3.ToString();


            conn.Close();
        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            double allinst = 0;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (GridView1.Rows[i].Cells[4].Text == "OK")
                {
                    GridView1.Rows[i].BackColor = Color.LightGray;
                }
                allinst += Convert.ToInt16(GridView1.Rows[i].Cells[1].Text);
            }
            Session["allinst"] = allinst;
        }
    }
}