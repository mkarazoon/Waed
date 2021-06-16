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
    public partial class Researcher : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["logSession"] == null || Session["userid"] == null)
            {
                Response.Redirect("Index.aspx");
            }
            Session["backurl"] = "Index.aspx";
            Label lblUserVal = (Label)Page.Master.FindControl("lblPageName");
            lblUserVal.Text = "تقرير الأداء البحثي للباحث";
            Label lblinfo = (Label)Page.Master.FindControl("lblinfo");
            lblinfo.Text = "تقرير الأداء البحثي للباحث";

            Label lblUser = (Label)Page.Master.FindControl("lblUserName");
            lblUser.Text = "تاريخ التقرير : " + DateTime.Now.Date.ToString("dd-MM-yyyy");

            if (!IsPostBack)
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();
                string UserCond = "";
                SqlCommand cmdCollege = new SqlCommand("select College,RID,Dept From ResearcherInfo where Acdid=" + Session["userid"], conn);
                SqlDataReader dr = cmdCollege.ExecuteReader();
                dr.Read();
                Session["userCollege"] = dr[0].ToString();
                Session["userDept"] = dr[2].ToString();

                if (Session["userrole"].ToString() == "6")
                {
                    UserCond = " and AcdId=" + Session["userid"];
                    ddlCollege.Visible = false;
                }
                else if (Session["userrole"].ToString() == "5")
                {
                    UserCond = " and college=N'" + dr[0].ToString() + "'";
                    ddlCollege.Visible = false;
                }
                else if(Session["userrole"].ToString() == "7")
                {
                    UserCond = " and college=N'" + Session["userCollege"] + "' and dept=N'" + Session["userDept"]+"'";
                    ddlCollege.Visible = false;
                }

                SqlCommand cmd1 = new SqlCommand("Select RID,(RAName + ' - '+ REName) RName From ResearcherInfo where RStatus='IN'" + UserCond, conn);
                ddlResearcher.DataSource = cmd1.ExecuteReader();
                ddlResearcher.DataTextField = "RName";
                ddlResearcher.DataValueField = "RID";
                ddlResearcher.DataBind();
                ddlResearcher.Items.Insert(0, "اختيار الباحث");
                ddlResearcher.Items[0].Value = "0";
                if (Session["userrole"].ToString() == "6")
                {
                    ddlResearcher.SelectedValue = dr[1].ToString();
                    ddlResearcher.Visible = false;
                    lblRName.Text = ddlResearcher.SelectedItem.Text;
                    //lblRName.Style.Add("display", "block");

                    nameDiv.Attributes.Add("class", nameDiv.Attributes["class"].ToString().Replace("lbl", ""));

                    getRData();
                }
                else
                {
                    ddlResearcher.Visible = true;
                    lblRName.Text = "";
                }
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("select Distinct College From ResearcherInfo where RStatus='IN'", conn);
                ddlCollege.DataSource = cmd.ExecuteReader();
                ddlCollege.DataTextField = "College";
                ddlCollege.DataValueField = "College";
                ddlCollege.DataBind();
                ddlCollege.Items.Insert(0, "حدد الكلية");
                ddlCollege.Items[0].Value = "0";

                Label4.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                conn.Close();
            }
        }

        protected void ddlResearcher_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmdCollege = new SqlCommand("select College,RID,Dept,RAName From ResearcherInfo where Rid='" + ddlResearcher.SelectedValue + "'", conn);
            SqlDataReader dr = cmdCollege.ExecuteReader();
            dr.Read();
            Session["userCollege"] = dr[0].ToString();
            Session["userDept"] = dr[2].ToString();
            lblRName.Text= dr[3].ToString();
            getRData();
        }

        protected void getRData()
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            //try
            //{
            SqlCommand cmd = new SqlCommand("Select * From ResearcherInfo where RID='" + ddlResearcher.SelectedValue + "'", conn);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());

            lblRNo.Text = dt.Rows[0]["Rid"].ToString();
            lblRDegree.Text = dt.Rows[0]["Rlevel"].ToString();
            lblRHireDate.Text = (dt.Rows[0]["HDate"].ToString() != "" ? DateTime.Parse(dt.Rows[0]["HDate"].ToString()).ToString("dd-MM-yyyy") : "");
            lblRCollege.Text = dt.Rows[0]["College"].ToString();
            lblRDept.Text = dt.Rows[0]["Dept"].ToString();
            lblProductivity.Text = dt.Rows[0]["ProductivityPointer"].ToString();
            lblEAvg.Text = dt.Rows[0]["RCitationAvg"].ToString();
            int HYear = (dt.Rows[0]["HDate"].ToString() != "" ? DateTime.Parse(dt.Rows[0]["HDate"].ToString()).Year : DateTime.Now.Year);
            cmd = new SqlCommand("SELECT isnull(count(ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId = '" + ddlResearcher.SelectedValue + "' and ReLevel = N'بحث علمي'", conn);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            Label1.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId = '" + ddlResearcher.SelectedValue + "' and ReLevel = N'بحث تاريخي'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label5.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId = '" + ddlResearcher.SelectedValue + "' and ReLevel = N'افتتاحية العدد'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label9.Text = dr[0].ToString();

            Label13.Text = (Convert.ToInt16(Label1.Text) +
                Convert.ToInt16(Label5.Text) +
                Convert.ToInt16(Label9.Text)).ToString();

            cmd = new SqlCommand("SELECT isnull(count(ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId = '" + ddlResearcher.SelectedValue + "' and ReYear=" + DateTime.Now.Year, conn);
            dr = cmd.ExecuteReader();
            dr.Read();

            lblCurrentR.Text = dr[0].ToString();
            if (Convert.ToInt16(lblCurrentR.Text) >= 1)
                currentDiv.Style.Add("background-color", "green");
            else
                currentDiv.Style.Add("background-color", "red");

            cmd = new SqlCommand("SELECT isnull(count(ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and ReYear=" + DateTime.Now.Year, conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            lblPublishAvgUni.Text = (Convert.ToDouble(lblCurrentR.Text) / Convert.ToDouble(dr[0].ToString())).ToString("P");

            cmd = new SqlCommand("SELECT isnull(count(ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.rid in (select rid from researcherinfo where college=N'"+Session["userCollege"].ToString()+"') and ReYear=" + DateTime.Now.Year, conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            lblPublishAvgCol.Text = (Convert.ToDouble(lblCurrentR.Text) / Convert.ToDouble(dr[0].ToString())).ToString("P");

            cmd = new SqlCommand("SELECT isnull(count(ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.rid in (select rid from researcherinfo where college=N'" + Session["userCollege"].ToString() + "' and dept=N'" + Session["userDept"].ToString() + "') and ReYear=" + DateTime.Now.Year, conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            lblPublishAvgDept.Text = (Convert.ToDouble(lblCurrentR.Text) / Convert.ToDouble(dr[0].ToString())).ToString("P");


            cmd = new SqlCommand("SELECT isnull(count(ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId = '" + ddlResearcher.SelectedValue + "' and ReLevel = N'بحث علمي' and ReParticipate=N'منفرد'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label2.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId = '" + ddlResearcher.SelectedValue + "' and ReLevel = N'بحث تاريخي' and ReParticipate=N'منفرد'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label6.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId = '" + ddlResearcher.SelectedValue + "' and ReLevel = N'افتتاحية العدد' and ReParticipate=N'منفرد'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label10.Text = dr[0].ToString();

            Label14.Text = (Convert.ToInt16(Label2.Text) +
                            Convert.ToInt16(Label6.Text) +
                            Convert.ToInt16(Label10.Text)).ToString();

            cmd = new SqlCommand("SELECT isnull(count(ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId = '" + ddlResearcher.SelectedValue + "' and ReLevel = N'بحث علمي' and ReParticipate=N'مشترك'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label3.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId = '" + ddlResearcher.SelectedValue + "' and ReLevel = N'بحث تاريخي' and ReParticipate=N'مشترك'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label7.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId = '" + ddlResearcher.SelectedValue + "' and ReLevel = N'افتتاحية العدد' and ReParticipate=N'مشترك'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label11.Text = dr[0].ToString();

            Label15.Text = (Convert.ToInt16(Label3.Text) +
                            Convert.ToInt16(Label7.Text) +
                            Convert.ToInt16(Label11.Text)).ToString();

            cmd = new SqlCommand("SELECT isnull(count(ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId = '" + ddlResearcher.SelectedValue + "' and ReLevel = N'مشاركة في مؤتمر'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label17.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId = '" + ddlResearcher.SelectedValue + "' and ReLevel = N'مشاركة في مؤتمر' and ReParticipate=N'منفرد'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label18.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId = '" + ddlResearcher.SelectedValue + "' and ReLevel = N'مشاركة في مؤتمر' and ReParticipate=N'مشترك'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label19.Text = dr[0].ToString();

            cmd = new SqlCommand("SELECT isnull(count(ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId = '" + ddlResearcher.SelectedValue + "' and ReLevel = N'فصل في كتاب' and ReParticipate=N'منفرد'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label22.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId = '" + ddlResearcher.SelectedValue + "' and ReLevel = N'كتاب' and ReParticipate=N'منفرد'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label26.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId = '" + ddlResearcher.SelectedValue + "' and ReLevel = N'ترجمة' and ReParticipate=N'منفرد'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label30.Text = dr[0].ToString();

            Label34.Text = (Convert.ToInt16(Label22.Text) +
                            Convert.ToInt16(Label26.Text) +
                            Convert.ToInt16(Label30.Text)).ToString();

            cmd = new SqlCommand("SELECT isnull(count(ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId = '" + ddlResearcher.SelectedValue + "' and ReLevel = N'فصل في كتاب' and ReParticipate=N'مشترك'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label23.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId = '" + ddlResearcher.SelectedValue + "' and ReLevel = N'كتاب' and ReParticipate=N'مشترك'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label27.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId = '" + ddlResearcher.SelectedValue + "' and ReLevel = N'ترجمة' and ReParticipate=N'مشترك'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label31.Text = dr[0].ToString();

            Label35.Text = (Convert.ToInt16(Label23.Text) +
                            Convert.ToInt16(Label27.Text) +
                            Convert.ToInt16(Label31.Text)).ToString();

            cmd = new SqlCommand("SELECT isnull(count(ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId = '" + ddlResearcher.SelectedValue + "' and ReLevel = N'فصل في كتاب'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label21.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId = '" + ddlResearcher.SelectedValue + "' and ReLevel = N'كتاب'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label25.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId = '" + ddlResearcher.SelectedValue + "' and ReLevel = N'ترجمة'", conn);
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

            lblParticipatPer.Text = (Convert.ToDouble(Label39.Text) / Convert.ToDouble(Label37.Text)).ToString("P");

            cmd = new SqlCommand("SELECT isnull(count([RId]),0) FROM Reward_Support where RId = '" + ddlResearcher.SelectedValue + "'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();

            lblSupportPer.Text = ((Convert.ToDouble(dr[0].ToString()) / Convert.ToDouble(Label37.Text)) / 2).ToString("P");

            //cmd = new SqlCommand("SELECT isnull(count([ReId]),0) FROM ResearchsInfo", conn);
            //dr = cmd.ExecuteReader();
            //dr.Read();

            //lblPublishAvg.Text=(Math.Round( Convert.ToDouble(Label37.Text)/Convert.ToDouble(dr[0].ToString()),3)).ToString();

            int varicount = 0;
            if (Label13.Text != "0")
                varicount++;
            if (Label17.Text != "0")
                varicount++;
            if (Label33.Text != "0")
                varicount++;

            switch (varicount)
            {
                case 0:
                case 1: lblVariantRPer.Text = "لا يوجد تنوع"; break;
                case 2: lblVariantRPer.Text = (Label13.Text != "0" ? "تنوع" : "لا يوجد تنوع"); break;
                case 3: lblVariantRPer.Text = "تنوع تام"; break;
            }
            double R = 0, C = 0, P = 0;
            if (Label37.Text != "0")
            {
                R = Math.Round(Convert.ToDouble(Label13.Text) / Convert.ToDouble(Label37.Text), 2);
                C = Math.Round(Convert.ToDouble(Label33.Text) / Convert.ToDouble(Label37.Text), 2);
                P = Math.Round(Convert.ToDouble(Label17.Text) / Convert.ToDouble(Label37.Text), 2);
                Chart2.Series["serie"].Points.Clear();
                Chart2.Series["serie"].Points.InsertY(0, R);
                Chart2.Series["serie"].Points[0].Color = Color.Olive;
                Chart2.Series["serie"].Points[0].AxisLabel = "البحوث العلمية";
                //Chart2.Series[0].Label = "＃VALX ＃VALY";
                Chart2.Series["serie"].Points.InsertY(1, C);
                Chart2.Series["serie"].Points[1].Color = Color.LightGray;
                Chart2.Series["serie"].Points[1].AxisLabel = "النشاطات التأليفية";
                Chart2.Series["serie"].Points.InsertY(2, P);
                Chart2.Series["serie"].Points[2].Color = Color.LightSeaGreen;
                Chart2.Series["serie"].Points[2].AxisLabel = "مشاركات المؤتمرات";

                StringBuilder strScript1 = new StringBuilder();
                strScript1.Append(@"<script type='text/javascript'>  
                               google.load('visualization', '1', {packages: ['corechart']});</script>  
                               <script type='text/javascript'> 
                               google.setOnLoadCallback(drawVisualization);
                               function drawVisualization() {         
                               var data = google.visualization.arrayToDataTable([
                               ['Status', 'النسبة'],");

                strScript1.Append("['البحوث العلمية'," + R + "],");
                strScript1.Append("['النشاطات التأليفية'," + C + "],");
                strScript1.Append("['مشاركات المؤتمرات'," + P + "]");
                strScript1.Append("]);");



                strScript1.Append("var options = { backgroundColor: 'white',chartArea:{left:20,top:20,width:'100%',height:'100%'}," +
                    "colors:['Olive','LightGray','LightSeaGreen'],titleTextStyle:{fontSize:'20',bold:'true'}};");
                strScript1.Append(" var chart1 = new google.visualization.PieChart(document.getElementById('piechartDiversity'));  chart1.draw(data, options);  }");
                strScript1.Append(" </script>");
                ltScripts.Text = strScript1.ToString();

            }
            double RR = (R / 70 > 1 ? 1 : R / 70);
            double RC = (C / 2 > 1 ? 1 : C / 20);
            double RP = (P / 10 > 1 ? 1 : P / 10);
            lblComprehensiveRPer.Text = ((RR + RP + RC) / 3).ToString("P");

            DataTable newD = new DataTable();
            newD.Columns.Add("Value");

            DataTable d = new DataTable();
            d.Columns.Add("Year");
            d.Columns.Add("Value");

            Chart1.Series["Series1"].Points.Clear();

            for (int j = HYear; j <= DateTime.Now.Year; j++)
            {
                cmd = new SqlCommand("SELECT isnull(count(reyear),0) cnt FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId = '" + ddlResearcher.SelectedValue + "' and reyear=" + j, conn);
                dr = cmd.ExecuteReader();
                dr.Read();
                //Chart1.Series["Series1"].Points.AddXY(j, dr[0]);
                Chart1.Series["Series1"].Points.InsertY(j - HYear, dr[0]);
                Chart1.Series["Series1"].Points[j - HYear].AxisLabel = j.ToString();
                Chart1.Series["Series1"].Points[j - HYear].Color = Color.PaleVioletRed;
                DataRow row = newD.NewRow();
                row[0] = dr[0];
                newD.Rows.Add(row);
                DataRow drow = d.NewRow();
                drow[0] = j;
                drow[1] = dr[0];
                d.Rows.Add(drow);
            }

            StringBuilder strScript = new StringBuilder();
            strScript.Append(@"<script type='text/javascript'>  
                    google.load('visualization', '1', {packages: ['corechart']});</script>  
                    <script type='text/javascript'>  
                    function drawVisualization() {         
                    var data = google.visualization.arrayToDataTable([
                ['السنة', 'عدد الابحاث', {type: 'number', role: 'annotation'}],");
            foreach (DataRow row in d.Rows)
            {
                strScript.Append("['" + row["Year"] + "'," + row["Value"] + "," + row["Value"] + "],");
            }
            strScript.Remove(strScript.Length - 1, 1);
            strScript.Append("]);");

            strScript.Append("var options = {legend: 'none', vAxis: { gridlines: { count: 0 }}, backgroundColor: 'white', colors:['#AB1802'],height:'250px',chartArea: {'width': '95%', 'height': '80%'}};");
            strScript.Append(" var chart = new google.visualization.AreaChart(document.getElementById('chart_div'));  chart.draw(data, options); } google.setOnLoadCallback(drawVisualization);");
            strScript.Append(" </script>");
            ltScripts.Text += strScript.ToString();

            //    double avg = 0;
            //    if (newD.Rows.Count >= 2)
            //    {
            //        for (int i = 0; i < newD.Rows.Count - 1; i++)
            //        {
            //            avg += ((Convert.ToInt16(newD.Rows[i + 1][0]) - Convert.ToInt16(newD.Rows[i][0])) / Convert.ToInt16(newD.Rows[i][0]));
            //        }
            //        avg = avg / newD.Rows.Count;
            //    lblResearchGrow.Text = Math.Abs(avg).ToString("P");
            //    if (avg >= 0)
            //    {
            //        GrowDivStatus.Style.Add("background-color", "green");
            //        GrowDivStatus.Style.Add("color", "green");
            //    }
            //    else
            //    {
            //        GrowDivStatus.Style.Add("background-color", "red");
            //        GrowDivStatus.Style.Add("color", "red");
            //    }
            //}
            //    else
            //    {
            //        lblResearchGrow.Text = "N/A";
            //    GrowDivStatus.Style.Add("background-color", "red");
            //    GrowDivStatus.Style.Add("color", "red");
            //}
            //}
            //catch { }

            conn.Close();
            getRCountPerYear();
        }

        protected void getRCountPerYear()
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand cmd1 = new SqlCommand("select * from YearSetting", conn);
            DataTable dtYear = new DataTable();
            dtYear.Load(cmd1.ExecuteReader());
            bool curr = false;
            int mf = Convert.ToInt16(dtYear.Rows[0][1]);
            int y1 = Convert.ToInt16(dtYear.Rows[0][2]);
            int mt = Convert.ToInt16(dtYear.Rows[0][3]);
            int y2 = Convert.ToInt16(dtYear.Rows[0][4]);

            //int y = DateTime.Now.Year;
            int m1 = DateTime.Now.Month;
            int m2 = 0;
            int divIndex = 0;
            int RTotal = 0;
            if (DateTime.Now.Month < mf)
            {
                //y = y - 1;
                m1 = 12;
                m2 = DateTime.Now.Month;
            }

            for (int i = mf; i <= m1; i++)
            {
                SqlCommand cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId=N'" + ddlResearcher.SelectedValue + "' and ReYear=" + y1 + " and ReMonth=" + i, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                divIndex++;
                HtmlGenericControl createDiv = new HtmlGenericControl("DIV");
                createDiv.ID = "createDiv_" + divIndex;

                createDiv.Style.Add(HtmlTextWriterStyle.Color, "white");
                createDiv.Style.Add(HtmlTextWriterStyle.Width, "10%");
                createDiv.Style.Add("float", "right");
                
                HtmlGenericControl createDivU = new HtmlGenericControl("DIV");
                createDivU.ID = "createDivU_" + divIndex;
                createDivU.Style.Add("text-align", "center");
                createDivU.Style.Add("word-wrap", "break-word");
                createDivU.Style.Add("box-sizing", "border-box");
                createDivU.Style.Add("padding", "1px");
                
                createDivU.InnerHtml = y1.ToString() + "-" + i.ToString();

                HtmlGenericControl createDivB = new HtmlGenericControl("DIV");
                createDivB.ID = "createDivB_" + divIndex;
                createDivB.Style.Add("text-align", "center");
                createDivB.Style.Add("word-wrap", "break-word");
                createDivB.Style.Add("box-sizing", "border-box");

                if (Convert.ToInt16(dr[0].ToString()) == 0)
                {
                    createDiv.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#e61b23");
                    createDivB.InnerHtml = "-";// dr[0].ToString();
                }
                else
                {
                    createDivB.InnerHtml = dr[0].ToString();
                    createDiv.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#6d8f28");
                    curr = true;
                    RTotal += Convert.ToInt16(dr[0].ToString());
                }
                createDiv.Style.Add("font-size", "small");
                createDiv.Style.Add("word-wrap", "break-word");
                createDiv.Style.Add("box-sizing", "border-box");
                createDiv.Style.Add("border", "1px solid");
                createDiv.Controls.Add(createDivU);
                createDiv.Controls.Add(createDivB);
                curDiv.Controls.Add(createDiv);
            }

            for (int i = 1; i <= m2; i++)
            {
                SqlCommand cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId=N'" + ddlResearcher.SelectedValue + "' and ReYear=" + (y2) + " and ReMonth=" + i, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                divIndex++;
                HtmlGenericControl createDiv = new HtmlGenericControl("DIV");
                createDiv.ID = "createDiv_" + divIndex;

                createDiv.Style.Add(HtmlTextWriterStyle.Color, "white");
                createDiv.Style.Add(HtmlTextWriterStyle.Width, "8.3%");
                createDiv.Style.Add("float", "right");

                HtmlGenericControl createDivU = new HtmlGenericControl("DIV");
                createDivU.ID = "createDivU_" + divIndex;
                createDivU.Style.Add("text-align", "center");
                createDivU.Style.Add("word-wrap", "break-word");
                createDivU.Style.Add("box-sizing", "border-box");
                createDivU.Style.Add("padding", "1px");
                createDivU.InnerHtml = (y2).ToString() + "-" + i.ToString();

                HtmlGenericControl createDivB = new HtmlGenericControl("DIV");
                createDivB.ID = "createDivB_" + divIndex;

                createDivB.Style.Add("text-align", "center");
                createDivB.Style.Add("word-wrap", "break-word");
                createDivB.Style.Add("box-sizing", "border-box");

                if (Convert.ToInt16(dr[0].ToString()) == 0)
                {
                    createDiv.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#e61b23");
                    createDivB.InnerHtml = "-";// dr[0].ToString();
                }
                else
                {
                    createDivB.InnerHtml = dr[0].ToString();
                    createDiv.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#6d8f28");
                    curr = true;
                    RTotal += Convert.ToInt16(dr[0].ToString());
                }
                createDiv.Style.Add("font-size", "small");
                createDiv.Style.Add("word-wrap", "break-word");
                createDiv.Style.Add("box-sizing", "border-box");
                createDiv.Style.Add("border", "1px solid");
                createDiv.Controls.Add(createDivU);
                createDiv.Controls.Add(createDivB);
                curDiv.Controls.Add(createDiv);
            }

            if (curr)
            {
                string per = Math.Round(100.0 / divIndex,0).ToString() + "%";
                string per1 = Math.Ceiling(100.0 / divIndex).ToString() + "%";
                for (int i = 1; i <= divIndex; i++)
                {
                    HtmlGenericControl Div = curDiv.FindControl("createDiv_" + i.ToString()) as HtmlGenericControl;
                    Div.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#6d8f28");
                    if (i >= 4 && i <= 6)
                        Div.Style.Add(HtmlTextWriterStyle.Width, per1);
                    else
                        Div.Style.Add(HtmlTextWriterStyle.Width, per);
                }
            }
            else
            {
                string per = Math.Round(100.0 / divIndex, 0).ToString() + "%";
                string per1 = Math.Ceiling(100.0 / divIndex).ToString() + "%";
                for (int i = 1; i <= divIndex; i++)
                {
                    HtmlGenericControl Div = curDiv.FindControl("createDiv_" + i.ToString()) as HtmlGenericControl;
                    Div.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#e61b23");
                    if (i >= 4 && i<=6)
                        Div.Style.Add(HtmlTextWriterStyle.Width, per1);
                    else
                        Div.Style.Add(HtmlTextWriterStyle.Width, per);
                }
            }

            lblCurrentR.Text = RTotal.ToString();
            if (Convert.ToInt16(lblCurrentR.Text) >= 1)
                currentDiv.Style.Add("background-color", "#6d8f28");
            else
                currentDiv.Style.Add("background-color", "#e61b23");
            conn.Close();
        }

        protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            if (ddlCollege.SelectedValue != "0")
            {
                SqlCommand cmd1 = new SqlCommand("Select RID,(RAName + ' - '+ REName) RName,RAName From ResearcherInfo where RSTatus='IN' and College=N'" + ddlCollege.SelectedValue + "'", conn);
                ddlResearcher.DataSource = cmd1.ExecuteReader();
                ddlResearcher.DataTextField = "RName";
                ddlResearcher.DataValueField = "RID";
                ddlResearcher.DataBind();
                ddlResearcher.Items.Insert(0, "اختيار الباحث");
                ddlResearcher.Items[0].Value = "0";
            }
            else
            {
                SqlCommand cmd1 = new SqlCommand("Select RID,(RAName + ' - '+ REName) RName,RAName From ResearcherInfo where RSTatus='IN'", conn);
                ddlResearcher.DataSource = cmd1.ExecuteReader();
                ddlResearcher.DataTextField = "RName";
                ddlResearcher.DataValueField = "RID";
                ddlResearcher.DataBind();
                ddlResearcher.Items.Insert(0, "اختيار الباحث");
                ddlResearcher.Items[0].Value = "0";
            }
            conn.Close();
        }

    }
}