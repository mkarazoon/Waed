using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ResearchAcademicUnit
{
    public partial class FullReport : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uid"] == null)
                Response.Redirect("Login.aspx");
            Session["backurl"] = "index.aspx";
            HtmlGenericControl divh = (HtmlGenericControl)Page.Master.FindControl("prinOut");
            divh.Visible = false;

            HtmlGenericControl divf = (HtmlGenericControl)Page.Master.FindControl("printfooter");
            divf.Visible = false;

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

        protected void btnApply_Click(object sender, EventArgs e)
        {
            int mf = Convert.ToInt16(ddlFromMonth.SelectedValue);
            int y1 = Convert.ToInt16(ddlFromYear.SelectedValue);
            int mt = Convert.ToInt16(ddlToMonth.SelectedValue);
            int y2 = Convert.ToInt16(ddlToYear.SelectedValue);
            lblPeriod.Text = "01/" + mf.ToString("00") + "/" + y1 + " - "+ DateTime.DaysInMonth(y2, mt) +"/ " + mt.ToString("00") + "/" + y2;
            int y = DateTime.Now.Year;
            int m = DateTime.Now.Month;

            string cond = "";
            if (y1 == y2)
                cond = " and reyear=" + y1 + " and remonth between " + mf + " and " + (mt);
            else
            {
                cond = " and ((reyear=" + (y1) + " and remonth between " + mf + " and 12) or (reyear=" + y2 + " and remonth between 1 and " + (mt) + ")) ";
            }
            
            //get University Research Card
            getData(cond);

            //get current year publication
            getCurrentYearPub(cond);

            //get top 10 research 
            createTop10(cond, "YES", 1);
            createTop10(cond, "NO", 1);
            createTop10(cond, "NO", 2);
            createTop10(cond, "NO", 3);
            createTop10(cond, "NO", 4);
            createTop10(cond, "NO", 5);
            createTop10(cond, "NO", 6);
        }

        protected void getData(string addCond)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd;
            int HYear = 2007;
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and ri.reid in (select reid from [ResearcherInfo] RI, Research_Researcher RR where RI.RId = RR.RId ) and restatus=N'منشور' and ReLevel = N'بحث علمي'" + addCond, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            Label1.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and ri.reid in (select reid from [ResearcherInfo] RI, Research_Researcher RR where RI.RId = RR.RId ) and restatus=N'منشور' and ReLevel = N'بحث تاريخي'" + addCond, conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label5.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and ri.reid in (select reid from [ResearcherInfo] RI, Research_Researcher RR where RI.RId = RR.RId ) and restatus=N'منشور' and ReLevel = N'افتتاحية العدد'" + addCond, conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label9.Text = dr[0].ToString();

            Label13.Text = (Convert.ToInt16(Label1.Text) +
                Convert.ToInt16(Label5.Text) +
                Convert.ToInt16(Label9.Text)).ToString();




            //cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and ri.reid in (select reid from [ResearcherInfo] RI, Research_Researcher RR where RI.RId = RR.RId and rstatus=N'IN') and restatus=N'منشور' " + addCond, conn);
            //dr = cmd.ExecuteReader();
            //dr.Read();

            //lblCurrentR.Text = dr[0].ToString();
            //cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and ri.reid in (select reid from [ResearcherInfo] RI, Research_Researcher RR where RI.RId = RR.RId and rstatus=N'IN') and restatus=N'منشور' " + addCond, conn);
            //dr = cmd.ExecuteReader();
            //dr.Read();

            //lblPublishAvg.Text = (Convert.ToDouble(lblCurrentR.Text) / Convert.ToDouble(dr[0].ToString())).ToString("P");

            //cmd = new SqlCommand("SELECT isnull(sum(recitation),0) FROM [ResearchsInfo] RI", conn);
            //dr = cmd.ExecuteReader();
            //dr.Read();

            //lblEAvg.Text = dr[0].ToString();

            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and ri.reid in (select reid from [ResearcherInfo] RI, Research_Researcher RR where RI.RId = RR.RId ) and restatus=N'منشور' and ReLevel = N'بحث علمي' and ReParticipate=N'منفرد'" + addCond, conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label2.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and ri.reid in (select reid from [ResearcherInfo] RI, Research_Researcher RR where RI.RId = RR.RId ) and restatus=N'منشور' and ReLevel = N'بحث تاريخي' and ReParticipate=N'منفرد'" + addCond, conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label6.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and ri.reid in (select reid from [ResearcherInfo] RI, Research_Researcher RR where RI.RId = RR.RId ) and restatus=N'منشور' and ReLevel = N'افتتاحية العدد' and ReParticipate=N'منفرد'" + addCond, conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label10.Text = dr[0].ToString();

            Label14.Text = (Convert.ToInt16(Label2.Text) +
                            Convert.ToInt16(Label6.Text) +
                            Convert.ToInt16(Label10.Text)).ToString();

            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and ri.reid in (select reid from [ResearcherInfo] RI, Research_Researcher RR where RI.RId = RR.RId ) and restatus=N'منشور' and ReLevel = N'بحث علمي' and ReParticipate=N'مشترك'" + addCond, conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label3.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and ri.reid in (select reid from [ResearcherInfo] RI, Research_Researcher RR where RI.RId = RR.RId ) and restatus=N'منشور' and ReLevel = N'بحث تاريخي' and ReParticipate=N'مشترك'" + addCond, conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label7.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and ri.reid in (select reid from [ResearcherInfo] RI, Research_Researcher RR where RI.RId = RR.RId ) and restatus=N'منشور' and ReLevel = N'افتتاحية العدد' and ReParticipate=N'مشترك'" + addCond, conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label11.Text = dr[0].ToString();

            Label15.Text = (Convert.ToInt16(Label3.Text) +
                            Convert.ToInt16(Label7.Text) +
                            Convert.ToInt16(Label11.Text)).ToString();

            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and ri.reid in (select reid from [ResearcherInfo] RI, Research_Researcher RR where RI.RId = RR.RId ) and restatus=N'منشور' and ReLevel = N'مشاركة في مؤتمر'" + addCond, conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label17.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and ri.reid in (select reid from [ResearcherInfo] RI, Research_Researcher RR where RI.RId = RR.RId ) and restatus=N'منشور' and ReLevel = N'مشاركة في مؤتمر' and ReParticipate=N'منفرد'" + addCond, conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label18.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and ri.reid in (select reid from [ResearcherInfo] RI, Research_Researcher RR where RI.RId = RR.RId ) and restatus=N'منشور' and ReLevel = N'مشاركة في مؤتمر' and ReParticipate=N'مشترك'" + addCond, conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label19.Text = dr[0].ToString();

            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and ri.reid in (select reid from [ResearcherInfo] RI, Research_Researcher RR where RI.RId = RR.RId ) and restatus=N'منشور' and ReLevel = N'فصل في كتاب' and ReParticipate=N'منفرد'" + addCond, conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label22.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and ri.reid in (select reid from [ResearcherInfo] RI, Research_Researcher RR where RI.RId = RR.RId ) and restatus=N'منشور' and ReLevel = N'كتاب' and ReParticipate=N'منفرد'" + addCond, conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label26.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and ri.reid in (select reid from [ResearcherInfo] RI, Research_Researcher RR where RI.RId = RR.RId ) and restatus=N'منشور' and ReLevel = N'ترجمة' and ReParticipate=N'منفرد'" + addCond, conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label30.Text = dr[0].ToString();

            Label34.Text = (Convert.ToInt16(Label22.Text) +
                            Convert.ToInt16(Label26.Text) +
                            Convert.ToInt16(Label30.Text)).ToString();

            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and ri.reid in (select reid from [ResearcherInfo] RI, Research_Researcher RR where RI.RId = RR.RId ) and restatus=N'منشور' and ReLevel = N'فصل في كتاب' and ReParticipate=N'مشترك'" + addCond, conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label23.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and ri.reid in (select reid from [ResearcherInfo] RI, Research_Researcher RR where RI.RId = RR.RId ) and restatus=N'منشور' and ReLevel = N'كتاب' and ReParticipate=N'مشترك'" + addCond, conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label27.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and ri.reid in (select reid from [ResearcherInfo] RI, Research_Researcher RR where RI.RId = RR.RId ) and restatus=N'منشور' and ReLevel = N'ترجمة' and ReParticipate=N'مشترك'" + addCond, conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label31.Text = dr[0].ToString();

            Label35.Text = (Convert.ToInt16(Label23.Text) +
                            Convert.ToInt16(Label27.Text) +
                            Convert.ToInt16(Label31.Text)).ToString();

            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and ri.reid in (select reid from [ResearcherInfo] RI, Research_Researcher RR where RI.RId = RR.RId ) and restatus=N'منشور' and ReLevel = N'فصل في كتاب'" + addCond, conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label21.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and ri.reid in (select reid from [ResearcherInfo] RI, Research_Researcher RR where RI.RId = RR.RId ) and restatus=N'منشور' and ReLevel = N'كتاب'" + addCond, conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label25.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and ri.reid in (select reid from [ResearcherInfo] RI, Research_Researcher RR where RI.RId = RR.RId ) and restatus=N'منشور' and ReLevel = N'ترجمة'" + addCond, conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label29.Text = dr[0].ToString();

            Label33.Text = (Convert.ToInt16(Label21.Text) +
                            Convert.ToInt16(Label25.Text) +
                            Convert.ToInt16(Label29.Text)).ToString();

            Label37.Text = (Convert.ToInt16(Label13.Text) +
            Convert.ToInt16(Label17.Text) +
            Convert.ToInt16(Label33.Text)).ToString();

            Label38.Text = (Convert.ToInt16(Label14.Text) +
                            Convert.ToInt16(Label18.Text) +
                            Convert.ToInt16(Label34.Text)).ToString();

            Label39.Text = (Convert.ToInt16(Label15.Text) +
                            Convert.ToInt16(Label19.Text) +
                            Convert.ToInt16(Label35.Text)).ToString();

            //lblParticipatPer.Text = (Convert.ToDouble(Label39.Text) / Convert.ToDouble(Label37.Text)).ToString("P");

            //cmd = new SqlCommand("SELECT isnull(count(distinct [RId]),0) FROM Reward_Support ", conn);
            //dr = cmd.ExecuteReader();
            //dr.Read();

            //lblSupportPer.Text = ((Convert.ToDouble(dr[0].ToString()) / Convert.ToDouble(Label37.Text)) / 2).ToString("P");


            int varicount = 0;
            if (Label13.Text != "0")
                varicount++;
            if (Label17.Text != "0")
                varicount++;
            if (Label33.Text != "0")
                varicount++;

            //switch (varicount)
            //{
            //    case 0:
            //    case 1: lblVariantRPer.Text = "لا يوجد تنوع"; break;
            //    case 2: lblVariantRPer.Text = (Label13.Text != "0" ? "تنوع" : "لا يوجد تنوع"); break;
            //    case 3: lblVariantRPer.Text = "تنوع تام"; break;
            //}
            double R = 0, C = 0, P = 0;
            if (Label37.Text != "0")
            {
                R = Math.Round(Convert.ToDouble(Label13.Text) / Convert.ToDouble(Label37.Text), 3);
                C = Math.Round(Convert.ToDouble(Label33.Text) / Convert.ToDouble(Label37.Text), 3);
                P = Math.Round(Convert.ToDouble(Label17.Text) / Convert.ToDouble(Label37.Text), 3);

                StringBuilder strScript1 = new StringBuilder();
                strScript1.Append(@"<script type='text/javascript'>  
                               google.load('visualization', '1', {packages: ['corechart']});</script>  
                               <script type='text/javascript'> 
                               google.setOnLoadCallback(drawVisualization);
                               function drawVisualization() {         
                               var data = google.visualization.arrayToDataTable([
                               ['Status', 'Per'],");

                strScript1.Append("['Scientific Research'," + R + "],");
                strScript1.Append("['Books'," + C + "],");
                strScript1.Append("['Conference'," + P + "]");
                strScript1.Append("]);");



                strScript1.Append("var options = { backgroundColor: 'white',chartArea:{left:20,top:20,width:'100%',height:'100%'}," +
                    "colors:['Olive','LightGray','LightSeaGreen'],titleTextStyle:{fontSize:'20',bold:'true'}};");
                strScript1.Append(" var chart1 = new google.visualization.PieChart(document.getElementById('piechartDiversity'));  chart1.draw(data, options);  }");
                strScript1.Append(" </script>");
                ltScripts.Text = strScript1.ToString();
            }

            //double RR = (R / 70 > 1 ? 1 : R / 70);
            //double RC = (C / 2 > 1 ? 1 : C / 20);
            //double RP = (P / 10 > 1 ? 1 : P / 10);
            //lblComprehensiveRPer.Text = ((RR + RP + RC) / 3).ToString("P");

            DataTable d = new DataTable();
            d.Columns.Add("Year");
            d.Columns.Add("Value");
            Session["d"] = d;
            getRCountPerYear();
            d = (DataTable)Session["d"];
            //Chart1.Series["Series1"].Points.Clear();
            //for (int j =Convert.ToInt16(ddlFromYear.SelectedValue); j <= Convert.ToInt16(ddlToYear.SelectedValue); j++)
            //{
            //    cmd = new SqlCommand("SELECT isnull(count(distinct ri.reid),0) cnt FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and ri.reid in (select reid from [ResearcherInfo] RI, Research_Researcher RR where RI.RId = RR.RId and rstatus=N'IN') and restatus=N'منشور' and reyear=" + j, conn);
            //    dr = cmd.ExecuteReader();
            //    dr.Read();
            //    DataRow drow = d.NewRow();

            //    drow[0] = j;
            //    drow[1] = dr[0];
            //    d.Rows.Add(drow);
            //}

            //new chart
            StringBuilder strScript = new StringBuilder();
            strScript.Append(@"<script type='text/javascript'>  
                    google.load('visualization', '1', {packages: ['corechart']});</script>  
                    <script type='text/javascript'>  
                    function drawVisualization() {         
                    var data = google.visualization.arrayToDataTable([
                ['Year', 'Count', {type: 'number', role: 'annotation'}],");
            foreach (DataRow row in d.Rows)
            {
                strScript.Append("['" + row["Year"] + "'," + row["Value"] + "," + row["Value"] + "],");
            }
            strScript.Remove(strScript.Length - 1, 1);
            strScript.Append("]);");

            strScript.Append("var options = {legend: 'none', vAxis: { gridlines: { count: 0 }}, backgroundColor: 'white' ,colors:['#AB1802'],height:'250px',width:'100%',chartArea: {'width': '95%', 'height': '80%'}};");
            strScript.Append(" var chart = new google.visualization.AreaChart(document.getElementById('chart_div'));  chart.draw(data, options); } google.setOnLoadCallback(drawVisualization);");
            strScript.Append(" </script>");
            ltScripts.Text += strScript.ToString();

            //end
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();


            cmd = new SqlCommand(@"SELECT isnull(count([ReId]),0) cnt, 
                case 
                    when MClass=N'الربع الأول' then 'Q1'
when MClass=N'الربع الثاني' then 'Q2'
when MClass=N'الربع الثالث' then 'Q3'
when MClass=N'الربع الرابع' then 'Q4'
when MClass=N'Non Qs' then 'Non Qs'
end MClass,
                MClassInt FROM ResearchsInfo where MClass<>N'غير متاح' and ReType=N'بحوث علمية' and reid in (select reid from [ResearcherInfo] RI, Research_Researcher RR where RI.RId = RR.RId and rstatus=N'IN') and ReStatus=N'منشور' " + addCond +" group by MClass,MClassInt  order by MClassInt", conn);
            dr = cmd.ExecuteReader();
            //dr.Read();
            DataTable ddd = new DataTable();
            ddd.Load(dr);

            StringBuilder strScript3 = new StringBuilder();
            strScript3.Append(@"<script type='text/javascript'>  
                               google.load('visualization', '1', {packages: ['corechart']});</script>  
                               <script type='text/javascript'> 
                               google.setOnLoadCallback(drawVisualization);
                               function drawVisualization() {         
                               var data = google.visualization.arrayToDataTable([
                               ['Status', 'Per'],");

            for (int i = 0; i < ddd.Rows.Count; i++)
            {
                strScript3.Append("['" + ddd.Rows[i][1] + "'," + ddd.Rows[i][0] + "],");
            }
            strScript3.Remove(strScript3.Length - 1, 1);
            strScript3.Append("]);");



            strScript3.Append("var options = { backgroundColor: 'white',chartArea:{left:20,top:20,width:'100%',height:'95%'}," +
                "titleTextStyle:{fontSize:'20',bold:'true'}};");
            strScript3.Append(" var chart1 = new google.visualization.PieChart(document.getElementById('piechartRTypes'));  chart1.draw(data, options);  }");
            strScript3.Append(" </script>");
            ltScripts.Text += strScript3.ToString();


            //cmd = new SqlCommand("SELECT isnull(count([ReId]),0) cnt FROM ResearchsInfo where MClass<>N'غير متاح' and ReType=N'بحوث علمية' and reid in (select reid from [ResearcherInfo] RI, Research_Researcher RR where RI.RId = RR.RId and rstatus=N'IN')" + addCond, conn);
            //dr = cmd.ExecuteReader();
            //dr.Read();
            //int ctn = Convert.ToInt16(dr[0]);

            //cmd = new SqlCommand("SELECT isnull(count([ReId]),0) cnt FROM ResearchsInfo where MClass<>N'غير متاح' and ReType=N'بحوث علمية' and MClass<>N'خارج التصنيف' and reid in (select reid from [ResearcherInfo] RI, Research_Researcher RR where RI.RId = RR.RId and rstatus=N'IN')" + addCond, conn);
            //dr = cmd.ExecuteReader();
            //dr.Read();
            //double cntw = Convert.ToInt16(dr[0]);

            //Label16.Text = (cntw / ctn).ToString("P");

            cmd = new SqlCommand(@"SELECT convert(decimal(18,3), isnull(count([ReId]),0))/("+ lblCurrentR.Text + ") cnt," +
                "case" +
                " when ReSector=N'العلوم  الحياتية' then 'Life Sciences'" +
                " when ReSector=N'العلوم الاجتماعية والإنسانية' then 'Social Sciences'" +
                " when ReSector=N'العلوم الصحية' then 'Medical Sciences'" +
                " when ReSector=N'الهندسة والعلوم الفيزيائية' then 'Engineering and Physical Sciences'" +
                " when ReSector=N'تخصصات متعددة' then 'Multidisciplinary'" +
                " end ReSector" +
                " FROM ResearchsInfo where reid in (select reid from [ResearcherInfo] RI, Research_Researcher RR where RI.RId = RR.RId and rstatus=N'IN') and ReStatus=N'منشور' " + addCond+" group by ReSector", conn);
            //dr = cmd.ExecuteReader();
            //dr.Read();
            DataTable CompDt = new DataTable();
            CompDt.Load(cmd.ExecuteReader());
            //Chart3.DataSource = CompDt;
            //Chart3.Series["serie"].XValueMember = "ReSector";
            //Chart3.Series["serie"].YValueMembers = "cnt";
            //Chart3.DataBind();

            StringBuilder strScript2 = new StringBuilder();
            strScript2.Append(@"<script type='text/javascript'>  
                               google.load('visualization', '1', {packages: ['corechart']});</script>  
                               <script type='text/javascript'> 
                               google.setOnLoadCallback(drawVisualization);
                               function drawVisualization() {         
                               var data = google.visualization.arrayToDataTable([
                               ['Status', 'Per'],");

            for (int i = 0; i < CompDt.Rows.Count; i++)
            {
                strScript2.Append("['" + CompDt.Rows[i][1] + "'," + CompDt.Rows[i][0] + "],");
            }
            strScript2.Remove(strScript2.Length - 1, 1);
            strScript2.Append("]);");



            strScript2.Append("var options = { backgroundColor: 'white',chartArea:{left:20,top:20,width:'100%',height:'100%'}," +
                "titleTextStyle:{fontSize:'20',bold:'true'}};");
            strScript2.Append(" var chart1 = new google.visualization.PieChart(document.getElementById('piechartComp'));  chart1.draw(data, options);  }");
            strScript2.Append(" </script>");
            ltScripts.Text += strScript2.ToString();

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
                          addCond + @"
                          
                          group by College";
            cmd = new SqlCommand(sql, conn);
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
            
            string[] col = { "red", "blue", "green", "silver", "Bisque", "gray", "#fcb131", "#921A1D", "AliceBlue" };
            strScript2 = new StringBuilder();
            strScript2.Append(@"<script type='text/javascript'>  
                               google.load('visualization', '1', {packages: ['corechart']});</script>  
                               <script type='text/javascript'> 
                               google.setOnLoadCallback(drawVisualization);
                               function drawVisualization() {         
                               var data = google.visualization.arrayToDataTable([
                               ['Faculty', 'Count', { role: 'style' }, { role: 'annotation' }],");

            for (int i = 0; i < dtBestCollege.Rows.Count; i++)
            {
                strScript2.Append("['" + dtBestCollege.Rows[i][1] + "'," + dtBestCollege.Rows[i][2] + ",'"+ col[i] + "','"+ dtBestCollege.Rows[i][2]+"|"+ dtBestCollege.Rows[i][1] + "'],");
            }
            strScript2.Remove(strScript2.Length - 1, 1);
            strScript2.Append("]);");

        
            strScript2.Append("var options = {vAxis:{textPosition: 'center'},hAxis: { textPosition: 'none' ,gridlines:{color: 'transparent'}},backgroundColor: 'white' ,height:'250px',width:'100%',chartArea: {'width': '95%', 'height': '80%'},seriesType: 'bars'};");
            //strScript2.Append("var options = { backgroundColor: 'white',chartArea:{left:20,top:20,width:'100%',height:'100%'}," +
            //    "titleTextStyle:{fontSize:'20',bold:'true'}};");
            strScript2.Append(" var chart1 = new google.visualization.BarChart(document.getElementById('piechartDiversity1'));  chart1.draw(data, options);  }");
            strScript2.Append(" </script>");
            ltScripts.Text += strScript2.ToString();


            //cmd = new SqlCommand("SELECT isnull(count([ReId]),0) cnt FROM ResearchsInfo where MClass<>N'غير متاح' and ReType=N'بحوث علمية'", conn);
            //dr = cmd.ExecuteReader();
            //dr.Read();
            //ctn = Convert.ToInt16(dr[0]);

            //cmd = new SqlCommand("SELECT isnull(count([ReId]),0) cnt FROM ResearchsInfo where MClass<>N'غير متاح' and ReType=N'بحوث علمية' and MClass<>N'خارج التصنيف'", conn);
            //dr = cmd.ExecuteReader();
            //dr.Read();
            //cntw = Convert.ToInt16(dr[0]);

            //Label8.Text = (cntw / ctn).ToString("P");

            //}
            //catch { }

            conn.Close();

            //calCitation();

            //getRCountPerYear();
        }

        protected void getRCountPerYear()
        {
            DataTable d = (DataTable)Session["d"];
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            //SqlCommand cmd1 = new SqlCommand("select * from YearSetting", conn);
            //DataTable dtYear = new DataTable();
            //dtYear.Load(cmd1.ExecuteReader());
            bool curr = false;
            int mf = Convert.ToInt16(ddlFromMonth.SelectedValue);
            int y1 = Convert.ToInt16(ddlFromYear.SelectedValue);
            int mt = Convert.ToInt16(ddlToMonth.SelectedValue);
            int y2 = Convert.ToInt16(ddlToYear.SelectedValue);

            //int y = DateTime.Now.Year;
            int m1 = mt;
            int m2 = 0;
            int divIndex = 0;
            int RTotal = 0;
            if (m1 < mf)
            {
                //y = y - 1;
                m1 = 12;
                m2 = mt;
            }

            for (int i = mf; i <= m1; i++)
            {
                SqlCommand cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and ri.reid in (select reid from [ResearcherInfo] RI, Research_Researcher RR where RI.RId = RR.RId ) and restatus=N'منشور' and ReYear=" + y1 + " and ReMonth=" + i, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                divIndex++;
                //HtmlGenericControl createDiv = new HtmlGenericControl("DIV");
                //createDiv.ID = "createDiv_" + divIndex;

                //createDiv.Style.Add(HtmlTextWriterStyle.Color, "white");
                ////createDiv.Style.Add(HtmlTextWriterStyle.Height, "30px");
                //createDiv.Style.Add(HtmlTextWriterStyle.Width, "10%");
                //createDiv.Style.Add("float", "right");

                //HtmlGenericControl createDivU = new HtmlGenericControl("DIV");
                //createDivU.ID = "createDivU_" + divIndex;
                //createDivU.Style.Add("text-align", "center");
                //createDivU.Style.Add("word-wrap", "break-word");
                //createDivU.Style.Add("box-sizing", "border-box");
                //createDivU.Style.Add("padding", "1px");

                //createDivU.InnerHtml = y1.ToString() + "-" + i.ToString();

                DataRow drow = d.NewRow();
                drow[0] = y1.ToString() + "-" + i.ToString();

                //HtmlGenericControl createDivB = new HtmlGenericControl("DIV");
                //createDivB.ID = "createDivB_" + divIndex;
                //createDivB.Style.Add("text-align", "center");
                //createDivB.Style.Add("word-wrap", "break-word");
                //createDivB.Style.Add("box-sizing", "border-box");

                if (Convert.ToInt16(dr[0].ToString()) == 0)
                {
                    //createDiv.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#e61b23");
                    //createDivB.InnerHtml = "-";// dr[0].ToString();
                    drow[1] = 0;
                }
                else
                {
                    //createDivB.InnerHtml = dr[0].ToString();
                    //createDiv.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#6d8f28");
                    curr = true;
                    RTotal += Convert.ToInt16(dr[0].ToString());
                    drow[1] = dr[0].ToString();
                }
                //createDiv.Style.Add("font-size", "small");
                //createDiv.Style.Add("word-wrap", "break-word");
                //createDiv.Style.Add("box-sizing", "border-box");
                //createDiv.Style.Add("border", "1px solid");
                //createDiv.Controls.Add(createDivU);
                //createDiv.Controls.Add(createDivB);
                //curDiv.Controls.Add(createDiv);
                d.Rows.Add(drow);
            }

            for (int i = 1; i <= m2; i++)
            {
                SqlCommand cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and ri.reid in (select reid from [ResearcherInfo] RI, Research_Researcher RR where RI.RId = RR.RId ) and restatus=N'منشور' and ReYear=" + y2 + " and ReMonth=" + i, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                divIndex++;
                //HtmlGenericControl createDiv = new HtmlGenericControl("DIV");
                //createDiv.ID = "createDiv_" + divIndex;

                //createDiv.Style.Add(HtmlTextWriterStyle.Color, "white");
                //createDiv.Style.Add(HtmlTextWriterStyle.Width, "10%");
                //createDiv.Style.Add("float", "right");

                //HtmlGenericControl createDivU = new HtmlGenericControl("DIV");
                //createDivU.ID = "createDivU_" + divIndex;
                //createDivU.Style.Add("text-align", "center");
                //createDivU.Style.Add("word-wrap", "break-word");
                //createDivU.Style.Add("box-sizing", "border-box");
                //createDivU.Style.Add("padding", "1px");
                //createDivU.InnerHtml = y2.ToString() + "-" + i.ToString();

                DataRow drow = d.NewRow();
                drow[0] = y2.ToString() + "-" + i.ToString();


                //HtmlGenericControl createDivB = new HtmlGenericControl("DIV");
                //createDivB.ID = "createDivB_" + divIndex;

                //createDivB.Style.Add("text-align", "center");
                //createDivB.Style.Add("word-wrap", "break-word");
                //createDivB.Style.Add("box-sizing", "border-box");

                if (Convert.ToInt16(dr[0].ToString()) == 0)
                {
                    //createDiv.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#e61b23");
                    //createDivB.InnerHtml = "-";// dr[0].ToString();
                    drow[1] = 0;
                }
                else
                {
                    //createDivB.InnerHtml = dr[0].ToString();
                    //createDiv.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#6d8f28");
                    curr = true;
                    RTotal += Convert.ToInt16(dr[0].ToString());
                    drow[1] = dr[0].ToString();
                }
                //createDiv.Style.Add("font-size", "small");
                //createDiv.Style.Add("word-wrap", "break-word");
                //createDiv.Style.Add("box-sizing", "border-box");
                //createDiv.Style.Add("border", "1px solid");
                //createDiv.Controls.Add(createDivU);
                //createDiv.Controls.Add(createDivB);
                //curDiv.Controls.Add(createDiv);
                d.Rows.Add(drow);
            }
            Session["d"] = d;
            lblCurrentR.Text = RTotal.ToString();
            //string sql = "";
            //sql = "select Distinct count(RId) EmpCount From ResearcherInfo where RStatus=N'IN'";
            //SqlCommand cmdCD = new SqlCommand(sql, conn);
            //DataTable dtCD = new DataTable();
            //dtCD.Load(cmdCD.ExecuteReader());

            //if (Convert.ToInt16(lblCurrentR.Text) >= Convert.ToInt16(dtCD.Rows[0]["EmpCount"]))
            //{
            //    string per = Math.Round(100.0 / divIndex, 0).ToString() + "%";
            //    string per1 = Math.Ceiling(100.0 / divIndex).ToString() + "%";
            //    for (int i = 1; i <= divIndex; i++)
            //    {
            //        HtmlGenericControl Div = curDiv.FindControl("createDiv_" + i.ToString()) as HtmlGenericControl;
            //        Div.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#6d8f28");
            //        if (i >= 4 && i <= 6)
            //            Div.Style.Add(HtmlTextWriterStyle.Width, per1);
            //        else
            //            Div.Style.Add(HtmlTextWriterStyle.Width, per);
            //    }
            //}
            //else
            //{
            //    string per = Math.Round(100.0 / divIndex, 0).ToString() + "%";
            //    string per1 = Math.Ceiling(100.0 / divIndex).ToString() + "%";
            //    for (int i = 1; i <= divIndex; i++)
            //    {
            //        HtmlGenericControl Div = curDiv.FindControl("createDiv_" + i.ToString()) as HtmlGenericControl;
            //        Div.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#e61b23");
            //        if (i >= 4 && i <= 6)
            //            Div.Style.Add(HtmlTextWriterStyle.Width, per1);
            //        else
            //            Div.Style.Add(HtmlTextWriterStyle.Width, per);
            //    }
            //}


            //lblCurrentR.Text = RTotal.ToString();
            //if (Convert.ToInt16(lblCurrentR.Text) >= Convert.ToInt16(dtCD.Rows[0]["EmpCount"]))
            //    currentDiv.Style.Add("background-color", "#6d8f28");
            //else
            //    currentDiv.Style.Add("background-color", "#e61b23");

            conn.Close();
        }

        //protected void calCitation()
        //{
        //    if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
        //        conn.Open();

        //    SqlCommand cmd = new SqlCommand("select ROW_NUMBER() OVER(ORDER BY ReCitation Desc) AS row_num,ReCitation from ResearchsInfo where ReStatus=N'منشور' and reid in (select reid from [ResearcherInfo] RI, Research_Researcher RR where RI.RId = RR.RId and rstatus=N'IN')", conn);
        //    DataTable dt = new DataTable();
        //    dt.Load(cmd.ExecuteReader());
        //    int h_index = 0;
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //        if (Convert.ToInt16(dt.Rows[i][0].ToString()) <= Convert.ToInt16(dt.Rows[i][1].ToString()))
        //        {
        //            h_index = i + 1;
        //        }
        //        else
        //            break;
        //    lblProductivity.Text = h_index.ToString();
        //    conn.Close();
        //}

        protected void getCurrentYearPub(string addCond)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            string sql = "";
            sql = @"select *, ROW_NUMBER() OVER(ORDER BY ReYear DESC, ReMonth DESC,MClassInt ASC) AS row_num 
                    FROM  
                    (SELECT distinct RE.ReId, ReTitle,
                    case 
                    when ReType=N'بحوث علمية' then 'Research Paper' 
                    when ReType=N'مؤتمر علمي' then 'Conference' 
                    when ReType=N'نشاط تأليفي' then 'Book'
                    end ReType,

                    case
                    when ReLevel=N'افتتاحية العدد' then 'Editorial'
                    when ReLevel=N'بحث تاريخي' then 'Review Research'
                    when ReLevel=N'بحث علمي' then 'Research Paper'
                    when ReLevel=N'مشاركة في مؤتمر' then 'Conference'
                    when ReLevel=N'فصل في كتاب' then 'Book Chapter'
                    when ReLevel=N'كتاب' then 'Book'
                    when ReLevel=N'ترجمة' then 'Translation'
                    end ReLevel,

                    ReYear,ReMonth, ReCitation,
                    case
                    when ReParticipate=N'مشترك' then 'Co-Author'
                    when ReParticipate=N'منفرد' then 'Single Author'
                    end ReParticipate,

                    Magazine, 

                    case 
                    when SourceType=N'مجلة' then 'Journal'
                    when SourceType=N'مؤتمر علمي' then 'Conference'
                    when SourceType=N'دار نشر' then 'Publisher'
                    end SourceType,

                    case 
                    when MClass=N'الربع الأول' then 'Q1'
                    when MClass=N'الربع الثاني' then 'Q2'
                    when MClass=N'الربع الثالث' then 'Q3'
                    when MClass=N'الربع الرابع' then 'Q4'
                    when MClass=N'غير متاح' then 'N/A'
                    when MClass=N'Non Qs' then 'Non Qs'
                    end MClass, 
                    case 
                    when CitationAvg=N'غير متاح' then 'N/A' else cast(CitationAvg  as nvarchar(50))
                    end CitationAvg, 
                    case 
                    when InSupport=N'لا' then 'NO' else 'YES'
                    end InSupport,
                    case 
                    when Reward=N'لا' then 'NO' else 'YES'
                    end Reward,

                    outSupport,ReStatus,MClassInt,
                    case 
                    when TopMag =N'لا' then 'NO' else 'YES'
                    end TopMag
                     FROM ResearchsInfo RE where " + addCond.Substring(5) + " and Restatus=N'منشور' and reid in (select reid from [ResearcherInfo] RI, Research_Researcher RR where RI.RId = RR.RId )) final";
            //" where ReId in (select ReId from Research_Researcher rr where rr.RId in (select ri.RId from ResearcherInfo ri where College = N'" + ddlResearcher.SelectedValue + "'))) final";
            SqlCommand cmd = new SqlCommand(sql, conn);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            if (dt.Rows.Count != 0)
            {
                Session["data"] = dt;

                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();
                string sql = "";
                sql = "Select distinct REngName,College,Dept From ResearcherInfo ri, Research_Researcher rr where ri.rid=rr.rid ";
                sql += " and rr.reid='" + e.Row.Cells[8].Text + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                HtmlGenericControl Rdiv = e.Row.FindControl("ReInfoDiv") as HtmlGenericControl;
                HtmlGenericControl Cdiv = e.Row.FindControl("CollegeDiv") as HtmlGenericControl;
                HtmlGenericControl Ddiv = e.Row.FindControl("DeptDiv") as HtmlGenericControl;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //string aname = dt.Rows[i][0].ToString();
                    //string ename = dt.Rows[i][0].ToString();
                    Rdiv.InnerText += dt.Rows[i][0].ToString() + " - ";// dt.Rows[i][0].ToString()
                    //Cdiv.InnerText = dt.Rows[i][1].ToString();
                    //Ddiv.InnerText = dt.Rows[i][2].ToString();
                }
                Rdiv.InnerText = Rdiv.InnerText.Substring(0, Rdiv.InnerText.Length - 3);
                //conn.Close();
            }
        }

        protected void createTop10(string addCond,string type,int quarter)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            //string sql = @"SELECT ReId,ReTitle,Magazine,CitationAvg,MClassInt,TopMag,ReAbstract,TotalR,Aff_Auther
            //              FROM ResearchsInfo
            //              where ReStatus=N'منشور' and TopMag=N'" + type+"' and MClassInt="+quarter+" " + addCond + @"
            //              order by cast(CitationAvg as float) desc";



            //SqlCommand cmd = new SqlCommand(sql, conn);
            DataTable dtTop = (DataTable)Session["data"];
            DataRow[] result = dtTop.Select("TopMag='"+type+ "' AND MClassInt = "+quarter);
            //dtTop.Load(cmd.ExecuteReader());

            //conn.Close();
            foreach (DataRow row in result)
            {
                if(row[0].ToString()=="AR395")
                {
                    string d = "";
                }
                HtmlGenericControl headerDiv = new HtmlGenericControl();
                //header content

                StringBuilder sb = new StringBuilder();
                //sb.Append();
                string sqlRID = @"SELECT (REngName) author,cnamee college,deptnamee dept,case 
when RLevel=N'استاذ' then 'Professor'
when RLevel=N'استاذ مشارك' then 'Associate Professor'
when RLevel=N'استاذ مساعد' then 'Assistant Professor'
when RLevel=N'محاضر' then 'Master'
end RLevel,ReAbstract,Aff_Auther,TotalR,rstatus
                          FROM ResearchsInfo re,ResearcherInfo ri,Research_Researcher rr,faculty f,department d
                          where re.ReId=rr.ReId and ri.RId=rr.RId and ri.college=f.collegename and ri.dept=d.deptname and re.reid='" + row[0] +@"'" + addCond + @"
                          order by rr.AutoId";
                SqlCommand cmd = new SqlCommand(sqlRID, conn);
                DataTable dtResearcher = new DataTable();
                dtResearcher.Load(cmd.ExecuteReader());

                string s = @"<div style='direction: ltr;font-size:14px; width: 100%;page-break-after:always'>
                            <div style='margin-top:1%; margin-bottom:1%'>
                                <div style='float:left; width: 15%'>
                                    <img src ='images/Middle_East_University_logo.png' />
                                </div>
                                <div style='float:left;width:84%'>
                                    <table border='1' style='border:1px solid black;border-collapse:collapse;width:100%;text-align:center;font-size:24px;'>
                                    <tr>
                                        <td style='width:25%;background-color:#7f7f7f;color:white'>Author</td>
                                        <td style='width:25%' colspan='3'>" + dtResearcher.Rows[0]["author"].ToString() + @"</td>
                                    </tr>
                                    <tr>
                                        <td style='width:25%;background-color:#7f7f7f;color:white'>College</td>
                                        <td style='width:25%'>" + dtResearcher.Rows[0]["college"].ToString() + @"</td>
                                        <td style='width:25%;background-color:#7f7f7f;color:white'>Department</td>
                                        <td style='width:25%'>" + dtResearcher.Rows[0]["dept"].ToString() + @"</td>
                                    </tr>
                                    </table>
                                </div>
                                <div style='clear:both'></div>
                            </div>
                            <div style='clear:both'></div>
                            <div style='margin-top:1%; margin-bottom:1%;'>
                                <table style = 'border-collapse:collapse;width:100%;border:1px solid black;text-align:center;font-size:24px;' border='1'>
                                    <tr>
                                        <td style = 'width:12.5%;background-color:#7f7f7f;color:white' >Title</td>
                                        <td style = 'width:87.5%;text-align:left;padding-left:10px' colspan = '7' >" + row[1].ToString() + @"</td>
                                    </tr>
                                    <tr>
                                        <td style = 'width:12.5%;background-color:#7f7f7f;color:white' > Jornal </td>
                                        <td style = 'width:12.5%' >" + row[8] + @"</td>
                                        <td style = 'width:12.5%;background-color:#7f7f7f;color:white' > Cite Score </td>
                                        <td style = 'width:12.5%' >" + row[11] + @"</td>
                                        <td style = 'width:12.5%;background-color:#7f7f7f;color:white' > Quarter </td>
                                        <td style = 'width:12.5%' >" + (quarter<5? quarter.ToString(): quarter == 5 ? "Non Qs":"Conferences") + @"</td>
                                        <td style = 'width:12.5%;background-color:#7f7f7f;color:white' > TOP 10 </td>
                                        <td style = 'width:12.5%' >" + type + @"</td>
                                    </tr>
                                </table>
                            </div>
                            <div style='clear:both'></div>
                            <div style='margin-top:1%; margin-bottom:1%; '>
                                <table style = 'border-collapse:collapse;width:100%;border:1px solid black;text-align:center;font-size:24px;' border='1'>
                                    <tr>
                                        <td style='background-color:#7f7f7f;'><div style='color:white;transform:translate(75px, 0px) rotate(270deg);height:180px'>Abstract</div></td>
                                        <td style = 'width:95%;text-align:left;padding:10px;vertical-align:top' colspan = '7'>" + dtResearcher.Rows[0]["ReAbstract"].ToString() + @"</td>
                                    </tr>
                                </table>
                            </div>
                            <div style='margin-top:1%; margin-bottom:1%; '>
                                <table style = 'border-collapse:collapse;width:100%;border:1px solid black;text-align:center;font-size:24px;' border='1'>
                                    <tr>
                                        
                                        <td style='background-color:#7f7f7f;color:white'>Order of Author ( Overall = "+ dtResearcher.Rows[0]["TotalR"].ToString() + @" )</td>
                                    </tr>
                                        Quarter5PageContent                                        <td style = 'width:50%;text-align:left;padding:10px;vertical-align:top'>";
                string c = "";
                string newOrder = string.Join(";", dtResearcher.Rows[0]["Aff_Auther"].ToString().Split(';').Reverse());
                string[] ord = newOrder.Split(';');
                int ord_index = 0;
                int start = 1;
                int end = Convert.ToInt16(dtResearcher.Rows[0]["TotalR"].ToString());
                s += "<TABLE style='width:100%;border:1px solid black'><tr>";
                for (int j =0 ; j < dtResearcher.Rows.Count; j++)
                {
                    if (dtResearcher.Rows[j]["rstatus"].ToString().ToUpper() == "IN")
                    {
                                s += "<td>"+ ord[ord_index] + "|" + dtResearcher.Rows[j][0].ToString()+ "</td>";
                                ord_index++;
                    }
                }
                s += "</tr></TABLE>";
                s+=@"</td>
                                    </tr>
                                </table>
                            </div>
                            <div style='clear:both'></div>
                       </div>
                             ";


                headerDiv.InnerHtml = s;
                if (type == "YES")
                    Top10PageContent.Controls.Add(headerDiv);
                else
                {
                    switch (quarter)
                    {
                        case 1:
                            Quarter1PageContent.Controls.Add(headerDiv);
                            break;
                        case 2:
                            Quarter2PageContent.Controls.Add(headerDiv);
                            break;
                        case 3:
                            Quarter3PageContent.Controls.Add(headerDiv);
                            break;
                        case 4:
                            Quarter4PageContent.Controls.Add(headerDiv);
                            break;
                        case 5:
                            Quarter5PageContent.Controls.Add(headerDiv);
                            break;
                        case 6:
                            Quarter6PageContent.Controls.Add(headerDiv);
                            break;
                    }
                }
            }


        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /*Verifies that the control is rendered */
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            GridView1.UseAccessibleHeader = true;
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            GridView1.FooterRow.TableSection = TableRowSection.TableFooter;
            GridView1.Attributes["style"] = "border-collapse:separate";
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowIndex % 10 == 0 && row.RowIndex != 0)
                {
                    row.Attributes["style"] = "page-break-after:always;";
                }
            }
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.RenderControl(hw);
            string gridHTML = sw.ToString().Replace("\"", "'").Replace(System.Environment.NewLine, "");
            StringBuilder sb = new StringBuilder();

            //function test(divName, flag)
            //{
            //    var printContents = document.getElementById(divName).innerHTML;
            //    var originalContents = document.body.innerHTML;
            //    document.body.innerHTML = printContents;
            //    window.print();
            //    document.body.innerHTML = originalContents;
            //    document.location.href = document.URL;// "PrintStudSeatsFinal.aspx";
            //    if (flag == 1)
            //    {
            //        __doPostBack("btn");
            //    }
            //}



            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload = new function(){");
            sb.Append("var printWin = window.open('', '', 'left=0");
            sb.Append(",top=0,width=1000,height=600,status=0');");
            sb.Append("printWin.document.write(\"");
            string style = "<style type = 'text/css'>thead {display:table-header-group;} tfoot{display:table-footer-group;}</style>";
            sb.Append(style + gridHTML);
            sb.Append("\");");
            sb.Append("printWin.document.close();");
            sb.Append("printWin.focus();");
            sb.Append("printWin.print();");
            sb.Append("printWin.close();");
            sb.Append("};");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
            GridView1.DataBind();
        }
    }
}