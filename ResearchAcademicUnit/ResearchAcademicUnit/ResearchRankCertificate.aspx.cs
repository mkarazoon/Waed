using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchAcademicUnit
{
    public partial class ResearchRankCertificate : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["logSession"] == null || Session["userid"] == null || Session["userrole"].ToString() == "6")
                Response.Redirect("login.aspx");

            if (!IsPostBack)
            {
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

            //for(int i=Convert.ToInt16(dtSetting.Rows[0][2])-20;i<= Convert.ToInt16(dtSetting.Rows[0][2]) + 20;i++)

            DataTable dt1 = new DataTable();
            dt1.Columns.Add("Year");
            for (int i = 2005; i <= Convert.ToInt16(DateTime.Now.Date.Year) + 1; i++)
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

            cmd = new SqlCommand("select count(reid) from ResearchsInfo", conn);
            DataTable newdt = new DataTable();
            newdt.Load(cmd.ExecuteReader());

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

            conn.Close();
        }

        protected void getBestResearch()
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            

            DataTable dtBestResearch = new DataTable();
            string sql = @"SELECT Dense_rank() OVER(ORDER BY MClassInt,CitationAvg DESC,Reyear,remonth) AS rank,* ,
							(select Top 1 (RIR.REngName)
						  From ResearchsInfo ReR,Research_Researcher RRR,ResearcherInfo RIR
                             where 
                             ReR.ReId=rrR.ReId and rrR.RId=riR.RId and base.ReId=rer.ReId)  'Author',
							 (select Top 1 
							 (case
								  when College=N'الاداب والعلوم' then 'Arts and Sciences'
								  when College=N'الحقوق' then 'Law'
								  when College=N'الاعمال' then 'Business'
								  when College=N'تكنولوجيا المعلومات' then 'Information Technology'
								  when College=N'العلوم التربوية' then 'Educational Sciences'
								  when College=N'الهندسة' then 'Engineering'
								  when College=N'الاعلام' then 'Media'
								  when College=N'العمارة والتصميم' then 'Architecture and Design'
								  when College=N'الصيدلة' then 'Pharmacy'
								  end) 
								From ResearchsInfo ReR,Research_Researcher RRR,ResearcherInfo RIR
                             where 
                             ReR.ReId=rrR.ReId and rrR.RId=riR.RId and base.ReId=rer.ReId) college
                          
from
( 
                          select distinct top 10000000 [ReTitle]
						  ,
						  (case 
						  when [MClass]=N'الربع الأول' then 'Q1'
						  when [MClass]=N'الربع الثاني' then 'Q2'
						  when [MClass]=N'الربع الثالث' then 'Q3'
						  when [MClass]=N'الربع الرابع' then 'Q4'
						  end) MClass
                          ,cast([CitationAvg] as float) CitationAvg
                          ,MClassInt
                          ,ReYear,ReMonth,Re.ReId
                          FROM ResearchsInfo Re,Research_Researcher RR,ResearcherInfo RI
                          where MClassInt between 1 and 2 and CitationAvg<>N'غير متاح' and
                          Re.ReId=rr.ReId and rr.RId=ri.RId
                          and ReStatus=N'منشور'" +
                          Session["cond"]+ @"
                          and ri.RStatus='IN'
                          order by MClassInt,cast(CitationAvg as float) DESC,Reyear,remonth) base";
            SqlCommand cmd = new SqlCommand(sql, conn);
            dtBestResearch.Load(cmd.ExecuteReader());

            GridView2.DataSource = dtBestResearch;
            GridView2.DataBind();


            conn.Close();
        }

        protected void getBestAuthors()
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            DataTable dtBestAuthor = new DataTable();
            dtBestAuthor.Columns.Add("Rank");
            dtBestAuthor.Columns.Add("Author");
            dtBestAuthor.Columns.Add("College");
            dtBestAuthor.Columns.Add("PaperCount",typeof(int));
            dtBestAuthor.Columns.Add("RID");
            dtBestAuthor.Columns.Add("Q1", typeof(int));
            dtBestAuthor.Columns.Add("Q2", typeof(int));
            dtBestAuthor.Columns.Add("Q3", typeof(int));
            dtBestAuthor.Columns.Add("Q4", typeof(int));
            dtBestAuthor.Columns.Add("Q5", typeof(int));
            dtBestAuthor.Columns.Add("CAvg");

            string sql = @"SELECT Dense_rank() OVER(ORDER BY count(rr.rid) DESC,(SELECT isnull(count(MClass),0) q
                          FROM ResearchsInfo Rer,Research_Researcher RRr,ResearcherInfo RIi
                          where Rer.ReId=rrr.ReId and rrr.RId=rii.RId and ReStatus=N'منشور'  " +
                          Session["cond"] + @"
                          and rii.RStatus='IN' and MClassInt=1 and rii.rid=Rr.RId) desc

                          ,(SELECT isnull(count(MClass),0) q
                          FROM ResearchsInfo Rer,Research_Researcher RRr,ResearcherInfo RIi
                          where Rer.ReId=rrr.ReId and rrr.RId=rii.RId and ReStatus=N'منشور'  " +
                          Session["cond"] + @"
                          and rii.RStatus='IN' and MClassInt=2 and rii.rid=Rr.RId) desc

						  ,(SELECT isnull(count(MClass),0) q
                          FROM ResearchsInfo Rer,Research_Researcher RRr,ResearcherInfo RIi
                          where Rer.ReId=rrr.ReId and rrr.RId=rii.RId and ReStatus=N'منشور'  " +
                          Session["cond"] + @"
                          and rii.RStatus='IN' and MClassInt=3 and rii.rid=Rr.RId) desc

						  ,(SELECT isnull(count(MClass),0) q
                          FROM ResearchsInfo Rer,Research_Researcher RRr,ResearcherInfo RIi
                          where Rer.ReId=rrr.ReId and rrr.RId=rii.RId and ReStatus=N'منشور'  " +
                          Session["cond"] + @"
                          and rii.RStatus='IN' and MClassInt=4 and rii.rid=Rr.RId) desc

						  ,(SELECT isnull(count(MClass),0) q
                          FROM ResearchsInfo Rer,Research_Researcher RRr,ResearcherInfo RIi
                          where Rer.ReId=rrr.ReId and rrr.RId=rii.RId and ReStatus=N'منشور'  " +
                          Session["cond"] + @"
                          and rii.RStatus='IN' and MClassInt between 5 and 6 and rii.rid=Rr.RId) desc

						  ,cast(case when (SELECT isnull(max(CitationAvg),0) q
                          FROM ResearchsInfo Rer,Research_Researcher RRr,ResearcherInfo RIi
                          where Rer.ReId=rrr.ReId and rrr.RId=rii.RId and ReStatus=N'منشور'  " +
                          Session["cond"] + @"
                          and rii.RStatus='IN' and rii.rid=Rr.RId)=N'غير متاح' then '-1' else (SELECT isnull(max(CitationAvg),0) q
                          FROM ResearchsInfo Rer,Research_Researcher RRr,ResearcherInfo RIi
                          where Rer.ReId=rrr.ReId and rrr.RId=rii.RId and ReStatus=N'منشور'  " +
                          Session["cond"] + @"
                          and rii.RStatus='IN' and rii.rid=Rr.RId) end as decimal) desc) AS rank

                          , rr.[RId],(ri.REngName) Author
                          ,(case
						  when College=N'الاداب والعلوم' then 'Arts and Sciences'
						  when College=N'الحقوق' then 'Law'
						  when College=N'الاعمال' then 'Business'
						  when College=N'تكنولوجيا المعلومات' then 'Information Technology'
						  when College=N'العلوم التربوية' then 'Educational Sciences'
						  when College=N'الهندسة' then 'Engineering'
						  when College=N'الاعلام' then 'Media'
						  when College=N'العمارة والتصميم' then 'Architecture and Design'
						  when College=N'الصيدلة' then 'Pharmacy'
						  end) College
                          ,count(rr.rid) PaperCount



						  ,(SELECT isnull(count(MClass),0) q
                          FROM ResearchsInfo Rer,Research_Researcher RRr,ResearcherInfo RIi
                          where Rer.ReId=rrr.ReId and rrr.RId=rii.RId and ReStatus=N'منشور'  " +
                          Session["cond"] + @"
                          and rii.RStatus='IN' and MClassInt=1 and rii.rid=Rr.RId) Q1

						  ,(SELECT isnull(count(MClass),0) q
                          FROM ResearchsInfo Rer,Research_Researcher RRr,ResearcherInfo RIi
                          where Rer.ReId=rrr.ReId and rrr.RId=rii.RId and ReStatus=N'منشور'  " +
                          Session["cond"] + @"
                          and rii.RStatus='IN' and MClassInt=2 and rii.rid=Rr.RId) Q2

						  						  ,(SELECT isnull(count(MClass),0) q
                          FROM ResearchsInfo Rer,Research_Researcher RRr,ResearcherInfo RIi
                          where Rer.ReId=rrr.ReId and rrr.RId=rii.RId and ReStatus=N'منشور' " +
                          Session["cond"] + @"
                          and rii.RStatus='IN' and MClassInt=3 and rii.rid=Rr.RId) Q3

						  						  ,(SELECT isnull(count(MClass),0) q
                          FROM ResearchsInfo Rer,Research_Researcher RRr,ResearcherInfo RIi
                          where Rer.ReId=rrr.ReId and rrr.RId=rii.RId and ReStatus=N'منشور'  " +
                          Session["cond"] + @"
                          and rii.RStatus='IN' and MClassInt=4 and rii.rid=Rr.RId) Q4

						  						  ,(SELECT isnull(count(MClass),0) q
                          FROM ResearchsInfo Rer,Research_Researcher RRr,ResearcherInfo RIi
                          where Rer.ReId=rrr.ReId and rrr.RId=rii.RId and ReStatus=N'منشور'  " +
                          Session["cond"] + @"
                          and rii.RStatus='IN' and MClassInt between 5 and 6 and rii.rid=Rr.RId) Q5

						  ,case when (SELECT isnull(max(CitationAvg),0) q
                          FROM ResearchsInfo Rer,Research_Researcher RRr,ResearcherInfo RIi
                          where Rer.ReId=rrr.ReId and rrr.RId=rii.RId and ReStatus=N'منشور'  " +
                          Session["cond"] + @"
                          and rii.RStatus='IN' and rii.rid=Rr.RId)=N'غير متاح' then '-1' else (SELECT isnull(max(CitationAvg),0) q
                          FROM ResearchsInfo Rer,Research_Researcher RRr,ResearcherInfo RIi
                          where Rer.ReId=rrr.ReId and rrr.RId=rii.RId and ReStatus=N'منشور'  " +
                          Session["cond"] + @"
                          and rii.RStatus='IN' and rii.rid=Rr.RId) end CAvg

                          FROM ResearchsInfo Re,Research_Researcher RR,ResearcherInfo RI
                          where Re.ReId=rr.ReId and rr.RId=ri.RId and ReStatus=N'منشور' " +
                          Session["cond"] + @"
                          and ri.RStatus='IN'
                          group by 
						  rr.RId,(ri.REngName)
                          ,College";

            SqlCommand cmd = new SqlCommand(sql, conn);
            dtBestAuthor.Load(cmd.ExecuteReader());

//            for (int i = 0; i < dtBestAuthor.Rows.Count; i++)
//            {
//                for (int j = 1; j <= 6; j++)
//                {
//                    sql = @"SELECT isnull(count(MClass),0) q
//                          FROM ResearchsInfo Re,Research_Researcher RR,ResearcherInfo RI
//                          where 
//--MClassInt between 1 and 4 and 
//                          Re.ReId=rr.ReId and rr.RId=ri.RId and ReStatus=N'منشور' " +
//                                  Session["cond"] + @"
//                          and ri.RStatus='IN' and MClassInt=" + j + " and ri.rid='" + dtBestAuthor.Rows[i][4] + "'";

//                    SqlCommand cmd1 = new SqlCommand(sql, conn);
//                    SqlDataReader dr = cmd1.ExecuteReader();
//                    dr.Read();
//                    dtBestAuthor.Rows[i][4 + j] = dr[0];
//                }
//            }
//            var dtBestAuthor1 = dtBestAuthor.AsEnumerable()
//                   .OrderByDescending(r => r.Field<int>("PaperCount"))
//                   .ThenByDescending(r => r.Field<int>("Q1"))
//                   .ThenByDescending(r => r.Field<int>("Q2"))
//                   .ThenByDescending(r => r.Field<int>("Q3"))
//                   .ThenByDescending(r => r.Field<int>("Q4"))
//                   .ThenByDescending(r => r.Field<int>("Q5"))
//                   .ThenByDescending(r => r.Field<int>("Q6"))
//                   .Select(r => r).Take(Convert.ToInt16(ddlTop.SelectedValue))
//                   .CopyToDataTable();

            grdBestAuth.DataSource = dtBestAuthor;//.Rows.Cast<System.Data.DataRow>().Take(Convert.ToInt16(ddlTop.SelectedValue));
            grdBestAuth.DataBind();

            //btnFinalList.Visible = true;
            grdBestAuth.Visible = true;
            //GridView1.Visible = false;


            //for (int i = 0; i < dtBestAuthor1.Rows.Count; i++)
            //{
            //    if(grdBestAuth.Rows[i].Cells[0].Text)
            //}
            //    grdBestAuth.Rows[i].Cells[0].Text = (i + 1).ToString();


            conn.Close();
        }

        protected void getCollegeRank()
        {
            try
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                DataTable dtBestCollege = new DataTable();
                string sql = @"SELECT Dense_rank() OVER(ORDER BY count(distinct rr.reid) DESC) AS rank
                          ,(case
						  when College=N'الاداب والعلوم' then 'Arts and Sciences'
						  when College=N'الحقوق' then 'Law'
						  when College=N'الاعمال' then 'Business'
						  when College=N'تكنولوجيا المعلومات' then 'Information Technology'
						  when College=N'العلوم التربوية' then 'Educational Sciences'
						  when College=N'الهندسة' then 'Engineering'
						  when College=N'الاعلام' then 'Media'
						  when College=N'العمارة والتصميم' then 'Architecture and Design'
						  when College=N'الصيدلة' then 'Pharmacy'
						  end) College
                          ,count(distinct rr.reid) PaperCount
                          FROM ResearchsInfo Re,Research_Researcher RR,ResearcherInfo RI
                          where 
                          ReStatus=N'منشور' and 
                          Re.ReId=rr.ReId and rr.RId=ri.RId " +
                              Session["cond"] + @"
                          and ri.RStatus='IN'
                          group by College";
                SqlCommand cmd = new SqlCommand(sql, conn);
                dtBestCollege.Load(cmd.ExecuteReader());
                int cnt = Convert.ToInt16(dtBestCollege.Rows[dtBestCollege.Rows.Count - 1][0]) + 1;

                string[] c = { "Arts and Sciences", "Media", "Business", "Law", "Pharmacy", "Educational Sciences", "Architecture and Design", "Engineering", "Information Technology" };
                for (int i = 0; i < c.Length; i++)
                {
                    bool found = false;
                    for (int j = 0; j < dtBestCollege.Rows.Count; j++)
                    {

                        if (dtBestCollege.Rows[j][1].ToString() == c[i].ToString())
                        {
                            found = true;
                            break;
                        }

                    }
                    if (!found)
                    {
                        DataRow r = dtBestCollege.NewRow();
                        r[0] = cnt;
                        r[1] = c[i];
                        r[2] = 0;
                        dtBestCollege.Rows.Add(r);
                    }

                }
                grdCollegeRank.DataSource = dtBestCollege;
                grdCollegeRank.DataBind();


                conn.Close();
            }
            catch { }
        }

        protected void btnApply_Click(object sender, EventArgs e)
        {
            
            int mf = Convert.ToInt16(ddlFromMonth.SelectedValue);
            int y1 = Convert.ToInt16(ddlFromYear.SelectedValue);
            int mt = Convert.ToInt16(ddlToMonth.SelectedValue);
            int y2 = Convert.ToInt16(ddlToYear.SelectedValue);

            Label1.Text = DateTime.DaysInMonth(y2, mt) + "/" + mt.ToString("00") + "/" + y2 + " - " + "01/" + mf.ToString("00") + "/" + y1;
            int y = DateTime.Now.Year;
            int m = DateTime.Now.Month;

            string cond = " and (";
            if (y1 == y2)
                cond = " and reyear=" + y1 + " and remonth between " + mf + " and " + (mt);
            else
            {
                for (int i = y1; i < y2; i++)
                {
                    cond += " (reyear=" + (i) + " and remonth between " + mf + " and 12) or (reyear=" + (i+1) + " and remonth between 1 and " + (mt) + ") or ";
                }
                cond = cond.Substring(0, cond.Length - 4) + ")";
            }


            Session["cond"] = cond;
            getBestResearch();
            getBestAuthors();
            getCollegeRank();
        }

        protected void ddlFromYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("Year");
            for (int i = Convert.ToInt16(ddlFromYear.SelectedValue); i <= DateTime.Now.Date.Year  + 1; i++)
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

        protected void ddlFromMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("Month");
            if (ddlFromMonth.SelectedValue != "0")
            {
                if (ddlFromYear.SelectedValue == ddlToYear.SelectedValue)
                {
                    for (int i = Convert.ToInt16(ddlFromMonth.SelectedValue); i <= 12; i++)
                    {
                        DataRow row = dt1.NewRow();
                        row[0] = i;
                        dt1.Rows.Add(row);
                    }
                }
                else
                {
                    for (int i = 1; i <= Convert.ToInt16(ddlFromMonth.SelectedValue) - 1; i++)
                    {
                        DataRow row = dt1.NewRow();
                        row[0] = i;
                        dt1.Rows.Add(row);
                    }
                }
                ddlToMonth.DataSource = dt1;
                ddlToMonth.DataTextField = "Month";
                ddlToMonth.DataValueField = "Month";
                ddlToMonth.DataBind();
                ddlToMonth.Items.Insert(0, "To Month");
                ddlToMonth.Items[0].Value = "0";
            }
        }

        protected void ddlToYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("Month");
            if (ddlFromMonth.SelectedValue != "0")
            {
                if (ddlFromYear.SelectedValue == ddlToYear.SelectedValue)
                {
                    for (int i = Convert.ToInt16(ddlFromMonth.SelectedValue); i <= 12; i++)
                    {
                        DataRow row = dt1.NewRow();
                        row[0] = i;
                        dt1.Rows.Add(row);
                    }
                }
                else
                {
                    for (int i = 1; i <= Convert.ToInt16(ddlFromMonth.SelectedValue) - 1; i++)
                    {
                        DataRow row = dt1.NewRow();
                        row[0] = i;
                        dt1.Rows.Add(row);
                    }
                }
                ddlToMonth.DataSource = dt1;
                ddlToMonth.DataTextField = "Month";
                ddlToMonth.DataValueField = "Month";
                ddlToMonth.DataBind();
                ddlToMonth.Items.Insert(0, "To Month");
                ddlToMonth.Items[0].Value = "0";
            }

        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            GridViewRow row =(GridViewRow)(sender as Control).Parent.Parent;
            StringBuilder s = new StringBuilder();
            s.Append("<p style='font-family:Algerian;font-size:26px'>Deanship of Graduate Studies and Scientific Research</p><p style='font-family:Algerian;'>Presents to</p>");
            SqlCommand cmd = new SqlCommand("SELECT REngName,r.Magazine,ISSN,CitationAvg FROM Research_Researcher rr, ResearcherInfo ri,ResearchsInfo r where rr.RId=ri.RId and rr.ReId=r.ReId and rr.ReId='" + row.Cells[8].Text+"'", conn);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            string a = "";
            string juornal = "";
            for (int i=0;i<dt.Rows.Count;i++)
            {
                a += "<div style='font-weight:bold'>" + dt.Rows[i][0].ToString() + "</div>";
                juornal = dt.Rows[i][1].ToString();
            }
            s.Append(a);
            s.Append("<p style='font-size:30px'>Best Research Paper Award 2019/2020</p><p>For the Paper</p><p style='font-weight:bold;font-size:20px'>" + row.Cells[1].Text + "</p>");
            s.Append("<p>Published in</p><p style='font-weight:bold;font-size:20px'>" + juornal + "</p>");
            s.Append("<p>ISSN : " + dt.Rows[0][2] + " | Cite Score : " + dt.Rows[0][3] + "</p>");
            Session["certText"] = s;
            Response.Redirect("ReCertificate.aspx");
            conn.Close();
        }

        protected void lnkPrintBestAuther_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            StringBuilder s = new StringBuilder();
            s.Append("<p style='font-family:Algerian;font-size:26px'>Deanship of Graduate Studies and Scientific Research</p><p style='font-family:Algerian;'>Presents to</p>");
            s.Append("<p style='font-weight:bold;font-size:18px;margin-top:150px'>" + row.Cells[1].Text + "</p>");
            //s.Append("<p>The Dean of " + row.Cells[1].Text + " Faculty</p>");
            s.Append("<p>Number of Papers ( " + row.Cells[3].Text + " )</p><p style='font-family:Algerian;font-size:36px'>Best Author Award 2019/2020</p><p style='font-weight:bold'>" + (((LinkButton)(sender as Control)).ID== "lnkPrintBestAutherU" ? (txtDeanName.Text==""? "Middle East University":txtDeanName.Text):"Faculty")+" Level</p>"+ (((LinkButton)(sender as Control)).ID == "lnkPrintBestAutherU" ? "" : "<p>"+row.Cells[2].Text+"</p>"));
            Session["certText"] = s;
            Response.Redirect("ReCertificate.aspx");
        }

        protected void lnkPrintBestCollege_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            StringBuilder s = new StringBuilder();
            s.Append("<p style='font-family:Algerian;font-size:26px'>Deanship of Graduate Studies and Scientific Research</p><p style='font-family:Algerian;margin-bottom:150px'>Presents to</p>");
            s.Append("<p>"+txtDeanName.Text+"</p>");
            s.Append("<p style='font-weight:bold'>The Dean of " + row.Cells[1].Text + " Faculty</p>");
            s.Append("<p style='font-family:Algerian;font-size:36px'>Best Faculty Award 2019/2020</p><p style='font-weight:bold'>Number of Published Papers ( " + row.Cells[2].Text+" )</p>");
            Session["certText"] = s;
            Response.Redirect("ReCertificate.aspx");
        }
    }
}