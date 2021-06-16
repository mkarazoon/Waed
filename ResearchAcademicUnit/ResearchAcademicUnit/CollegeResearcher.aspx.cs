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
    public partial class CollegeResearcher : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["logSession"] == null || Session["userid"] == null || Session["userrole"].ToString() == "6")
            {
                Response.Redirect("login.aspx");
            }
            Session["backurl"] = "Index.aspx";
            Label lblUserVal = (Label)Page.Master.FindControl("lblPageName");
            lblUserVal.Text = "البطاقة البحثية للكلية";
            Label lblinfo = (Label)Page.Master.FindControl("lblinfo");
            lblinfo.Text = "البطاقة البحثية للكلية";

            Label lblUser = (Label)Page.Master.FindControl("lblUserName");
            lblUser.Text = (Session["userrole"].ToString() == "5" ? "أهلا بكم في كلية " + Session["userCollege"] : "");

            if (!IsPostBack)
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();
                string userRole = "";
                if (Session["userrole"].ToString() == "5")
                {
                    userRole = " where acdid=" + Session["userid"];
                    ddlResearcher.Visible = true;
                    ddlDept.Visible = true;
                }
                else if (Session["userrole"].ToString() == "7")
                {
                    ddlResearcher.Visible = false;
                    ddlDept.Visible = false;
                    getDataCD(" where college=N'" + Session["userCollege"] + "' and Dept=N'" + Session["userDept"] + "'");
                    Label4.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    lblRCollege.Text = Session["userCollege"].ToString();
                    lblRDept.Text = Session["userDept"].ToString();
                    lblinfo.Text = "البطاقة البحثية كلية " + lblRCollege.Text + " - " + lblRDept.Text;
                    collegeDiv.Visible = false;
                }
                if (ddlResearcher.Visible)
                {
                    SqlCommand cmd1 = new SqlCommand("Select distinct College From ResearcherInfo" + userRole, conn);
                    ddlResearcher.DataSource = cmd1.ExecuteReader();
                    ddlResearcher.DataTextField = "College";
                    ddlResearcher.DataValueField = "College";
                    ddlResearcher.DataBind();
                    ddlResearcher.Items.Insert(0, "اختيار الكلية");
                    ddlResearcher.Items[0].Value = "0";
                    Label4.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                }
                conn.Close();
            }
        }

        protected void ddlResearcher_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            ////try
            ////{
            SqlCommand cmd111 = new SqlCommand("Select distinct Dept From ResearcherInfo where College=N'" + ddlResearcher.SelectedValue + "'", conn);
            ddlDept.DataSource = cmd111.ExecuteReader();
            ddlDept.DataValueField = "Dept";
            ddlDept.DataTextField = "Dept";
            ddlDept.DataBind();
            ddlDept.Items.Insert(0, "اختيار القسم");
            ddlDept.Items[0].Value = "0";
            conn.Close();
            lblRCollege.Text = ddlResearcher.SelectedItem.Text;
            lblRDept.Text = "جميع الأقسام";
            Label lblinfo = (Label)Page.Master.FindControl("lblinfo");
            lblinfo.Text = "البطاقة البحثية كلية "+ lblRCollege.Text + " - " + lblRDept.Text;

            getData("");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("IndexCollege.aspx");
        }

        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDept.SelectedItem.Text != "" && ddlDept.SelectedValue!="0")
            {
                lblRDept.Text = ddlDept.SelectedItem.Text;
                getData(" and Dept=N'" + ddlDept.SelectedValue + "'");
            }
            else
            {
                lblRDept.Text = "جميع الأقسام";
                getData("");
            }
            Label lblinfo = (Label)Page.Master.FindControl("lblinfo");
            lblinfo.Text = "البطاقة البحثية كلية " + lblRCollege.Text + " - " + lblRDept.Text;

        }

        protected void getData(string addCond)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd;
            //cmd = new SqlCommand("Select * From ResearcherInfo where RID='" + ddlResearcher.SelectedValue + "'", conn);
            //DataTable dt = new DataTable();
            //dt.Load(cmd.ExecuteReader());

            //lblRName.Text = dt.Rows[0]["REName"].ToString();
            //lblRNo.Text = dt.Rows[0]["Rid"].ToString();
            //lblRDegree.Text = dt.Rows[0]["Rlevel"].ToString();
            //lblRHireDate.Text = (dt.Rows[0]["HDate"].ToString() != "" ? DateTime.Parse(dt.Rows[0]["HDate"].ToString()).ToString("dd-MM-yyyy") : "");
            //lblRCollege.Text = dt.Rows[0]["College"].ToString();
            //lblRDept.Text = dt.Rows[0]["Dept"].ToString();
            //lblProductivity.Text = dt.Rows[0]["ProductivityPointer"].ToString();
            //lblEAvg.Text= dt.Rows[0]["RCitationAvg"].ToString();
            int HYear = 2007;// (dt.Rows[0]["HDate"].ToString() != "" ? DateTime.Parse(dt.Rows[0]["HDate"].ToString()).Year : DateTime.Now.Year);
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo where College=N'" + ddlResearcher.SelectedValue + "'" + addCond + ") and ReLevel = N'بحث علمي'", conn);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            Label1.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo where College=N'" + ddlResearcher.SelectedValue + "'" + addCond + ") and ReLevel = N'بحث تاريخي'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label5.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo where College=N'" + ddlResearcher.SelectedValue + "'" + addCond + ") and ReLevel = N'افتتاحية العدد'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label9.Text = dr[0].ToString();

            Label13.Text = (Convert.ToInt16(Label1.Text) +
                Convert.ToInt16(Label5.Text) +
                Convert.ToInt16(Label9.Text)).ToString();

            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo where College=N'" + ddlResearcher.SelectedValue + "') and ReYear=" + DateTime.Now.Year, conn);
            dr = cmd.ExecuteReader();
            dr.Read();

            lblCurrentR.Text = lblCurrentRCollege.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and ReYear=" + DateTime.Now.Year, conn);
            dr = cmd.ExecuteReader();
            dr.Read();

            lblPublishAvgCollege.Text = (Convert.ToDouble(lblCurrentR.Text) / Convert.ToDouble(dr[0].ToString())).ToString("P");

            if (ddlDept.SelectedValue != "0")
            {
                colDiv.Visible = false;
                coldeptDiv.Visible = true;
                deptDiv.Visible = true;
                cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo where College=N'" + ddlResearcher.SelectedValue + "'" + addCond + ") and ReYear=" + DateTime.Now.Year, conn);
                dr = cmd.ExecuteReader();
                dr.Read();
                lblCurrentRDept.Text = dr[0].ToString();

                //cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo where College=N'" + ddlResearcher.SelectedValue + "'" + addCond + ") and ReYear=" + DateTime.Now.Year, conn);
                //dr = cmd.ExecuteReader();
                //dr.Read();
                lblPublishAvgDept.Text = (Convert.ToDouble(lblCurrentRDept.Text) / Convert.ToDouble(lblCurrentR.Text)).ToString("P");
            }
            else
            {
                lblPublishAvgDept.Text = "N/A";
                colDiv.Visible = true;
                coldeptDiv.Visible = false;
                deptDiv.Visible = false;

            }

            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo where College=N'" + ddlResearcher.SelectedValue + "'" + addCond + ") and ReLevel = N'بحث علمي' and ReParticipate=N'منفرد'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label2.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo where College=N'" + ddlResearcher.SelectedValue + "'" + addCond + ") and ReLevel = N'بحث تاريخي' and ReParticipate=N'منفرد'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label6.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo where College=N'" + ddlResearcher.SelectedValue + "'" + addCond + ") and ReLevel = N'افتتاحية العدد' and ReParticipate=N'منفرد'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label10.Text = dr[0].ToString();

            Label14.Text = (Convert.ToInt16(Label2.Text) +
                            Convert.ToInt16(Label6.Text) +
                            Convert.ToInt16(Label10.Text)).ToString();

            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo where College=N'" + ddlResearcher.SelectedValue + "'" + addCond + ") and ReLevel = N'بحث علمي' and ReParticipate=N'مشترك'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label3.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo where College=N'" + ddlResearcher.SelectedValue + "'" + addCond + ") and ReLevel = N'بحث تاريخي' and ReParticipate=N'مشترك'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label7.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo where College=N'" + ddlResearcher.SelectedValue + "'" + addCond + ") and ReLevel = N'افتتاحية العدد' and ReParticipate=N'مشترك'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label11.Text = dr[0].ToString();

            Label15.Text = (Convert.ToInt16(Label3.Text) +
                            Convert.ToInt16(Label7.Text) +
                            Convert.ToInt16(Label11.Text)).ToString();

            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo where College=N'" + ddlResearcher.SelectedValue + "'" + addCond + ") and ReLevel = N'مشاركة في مؤتمر'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label17.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo where College=N'" + ddlResearcher.SelectedValue + "'" + addCond + ") and ReLevel = N'مشاركة في مؤتمر' and ReParticipate=N'منفرد'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label18.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo where College=N'" + ddlResearcher.SelectedValue + "'" + addCond + ") and ReLevel = N'مشاركة في مؤتمر' and ReParticipate=N'مشترك'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label19.Text = dr[0].ToString();

            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo where College=N'" + ddlResearcher.SelectedValue + "'" + addCond + ") and ReLevel = N'فصل في كتاب' and ReParticipate=N'منفرد'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label22.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo where College=N'" + ddlResearcher.SelectedValue + "'" + addCond + ") and ReLevel = N'كتاب' and ReParticipate=N'منفرد'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label26.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo where College=N'" + ddlResearcher.SelectedValue + "'" + addCond + ") and ReLevel = N'ترجمة' and ReParticipate=N'منفرد'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label30.Text = dr[0].ToString();

            Label34.Text = (Convert.ToInt16(Label22.Text) +
                            Convert.ToInt16(Label26.Text) +
                            Convert.ToInt16(Label30.Text)).ToString();

            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo where College=N'" + ddlResearcher.SelectedValue + "'" + addCond + ") and ReLevel = N'فصل في كتاب' and ReParticipate=N'مشترك'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label23.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo where College=N'" + ddlResearcher.SelectedValue + "'" + addCond + ") and ReLevel = N'كتاب' and ReParticipate=N'مشترك'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label27.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo where College=N'" + ddlResearcher.SelectedValue + "'" + addCond + ") and ReLevel = N'ترجمة' and ReParticipate=N'مشترك'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label31.Text = dr[0].ToString();

            Label35.Text = (Convert.ToInt16(Label23.Text) +
                            Convert.ToInt16(Label27.Text) +
                            Convert.ToInt16(Label31.Text)).ToString();

            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo where College=N'" + ddlResearcher.SelectedValue + "'" + addCond + ") and ReLevel = N'فصل في كتاب'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label21.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo where College=N'" + ddlResearcher.SelectedValue + "'" + addCond + ") and ReLevel = N'كتاب'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label25.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo where College=N'" + ddlResearcher.SelectedValue + "'" + addCond + ") and ReLevel = N'ترجمة'", conn);
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

            cmd = new SqlCommand("SELECT isnull(count(distinct [RId]),0) FROM Reward_Support where RId in (select rid from ResearcherInfo where College=N'" + ddlResearcher.SelectedValue + "'" + addCond + ")", conn);
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
                cmd = new SqlCommand("SELECT isnull(count(distinct ri.reid),0) cnt FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo where College=N'" + ddlResearcher.SelectedValue + "'" + addCond + ") and reyear=" + j, conn);
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

            strScript.Append("var options = {legend: 'none', vAxis: { gridlines: { count: 0 }}, backgroundColor: 'white' ,colors:['#AB1802'],height:'250px',chartArea: {'width': '95%', 'height': '80%'}};");
            strScript.Append(" var chart = new google.visualization.AreaChart(document.getElementById('chart_div'));  chart.draw(data, options); } google.setOnLoadCallback(drawVisualization);");
            strScript.Append(" </script>");
            ltScripts.Text += strScript.ToString();

            //}
            //catch { }

            cmd = new SqlCommand("SELECT isnull(sum(recitation),0) FROM [ResearchsInfo] RI  where reid in (select distinct reid From researcherinfo r1,Research_Researcher r2 where r1.rid=r2.rid and college=N'" + ddlResearcher.SelectedValue + "'" + (ddlDept.SelectedValue != "0" ? " and Dept=N'" + ddlDept.SelectedValue + "')" : ")"), conn);
            dr = cmd.ExecuteReader();
            dr.Read();

            lblEAvg.Text = dr[0].ToString();


            conn.Close();

            calCitation();
            if (ddlDept.SelectedValue == "0")
                getRCountPerYear();
            else
                getRCountPerYear(ddlDept.SelectedItem.Text);
        }

        protected void getDataCD(string addCond)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd;
            int HYear = 2007;
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo " + addCond + ") and ReLevel = N'بحث علمي'", conn);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            Label1.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo " + addCond + ") and ReLevel = N'بحث تاريخي'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label5.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo " +  addCond + ") and ReLevel = N'افتتاحية العدد'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label9.Text = dr[0].ToString();

            Label13.Text = (Convert.ToInt16(Label1.Text) +
                Convert.ToInt16(Label5.Text) +
                Convert.ToInt16(Label9.Text)).ToString();

            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo where College=N'" + Session["userCollege"] + "') and ReYear=" + DateTime.Now.Year, conn);
            dr = cmd.ExecuteReader();
            dr.Read();

            lblCurrentR.Text = lblCurrentRCollege.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and ReYear=" + DateTime.Now.Year, conn);
            dr = cmd.ExecuteReader();
            dr.Read();

            lblPublishAvgCollege.Text = (Convert.ToDouble(lblCurrentR.Text) / Convert.ToDouble(dr[0].ToString())).ToString("P");

            if (ddlDept.SelectedValue != "0")
            {
                colDiv.Visible = false;
                coldeptDiv.Visible = true;
                deptDiv.Visible = true;
                cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo " + addCond + ") and ReYear=" + DateTime.Now.Year, conn);
                dr = cmd.ExecuteReader();
                dr.Read();
                lblCurrentRDept.Text = dr[0].ToString();

                lblPublishAvgDept.Text = (Convert.ToDouble(lblCurrentRDept.Text) / Convert.ToDouble(lblCurrentR.Text)).ToString("P");
            }
            else
            {
                lblPublishAvgDept.Text = "N/A";
                colDiv.Visible = true;
                coldeptDiv.Visible = false;
                deptDiv.Visible = false;

            }

            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo " + addCond + ") and ReLevel = N'بحث علمي' and ReParticipate=N'منفرد'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label2.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo " +  addCond + ") and ReLevel = N'بحث تاريخي' and ReParticipate=N'منفرد'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label6.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo " +  addCond + ") and ReLevel = N'افتتاحية العدد' and ReParticipate=N'منفرد'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label10.Text = dr[0].ToString();

            Label14.Text = (Convert.ToInt16(Label2.Text) +
                            Convert.ToInt16(Label6.Text) +
                            Convert.ToInt16(Label10.Text)).ToString();

            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo " + addCond + ") and ReLevel = N'بحث علمي' and ReParticipate=N'مشترك'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label3.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo " + addCond + ") and ReLevel = N'بحث تاريخي' and ReParticipate=N'مشترك'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label7.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo " + addCond + ") and ReLevel = N'افتتاحية العدد' and ReParticipate=N'مشترك'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label11.Text = dr[0].ToString();

            Label15.Text = (Convert.ToInt16(Label3.Text) +
                            Convert.ToInt16(Label7.Text) +
                            Convert.ToInt16(Label11.Text)).ToString();

            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo " + addCond + ") and ReLevel = N'مشاركة في مؤتمر'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label17.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo " + addCond + ") and ReLevel = N'مشاركة في مؤتمر' and ReParticipate=N'منفرد'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label18.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo " + addCond + ") and ReLevel = N'مشاركة في مؤتمر' and ReParticipate=N'مشترك'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label19.Text = dr[0].ToString();

            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo " + addCond + ") and ReLevel = N'فصل في كتاب' and ReParticipate=N'منفرد'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label22.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo " + addCond + ") and ReLevel = N'كتاب' and ReParticipate=N'منفرد'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label26.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo " + addCond + ") and ReLevel = N'ترجمة' and ReParticipate=N'منفرد'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label30.Text = dr[0].ToString();

            Label34.Text = (Convert.ToInt16(Label22.Text) +
                            Convert.ToInt16(Label26.Text) +
                            Convert.ToInt16(Label30.Text)).ToString();

            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo " +  addCond + ") and ReLevel = N'فصل في كتاب' and ReParticipate=N'مشترك'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label23.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo " +  addCond + ") and ReLevel = N'كتاب' and ReParticipate=N'مشترك'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label27.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo " +  addCond + ") and ReLevel = N'ترجمة' and ReParticipate=N'مشترك'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label31.Text = dr[0].ToString();

            Label35.Text = (Convert.ToInt16(Label23.Text) +
                            Convert.ToInt16(Label27.Text) +
                            Convert.ToInt16(Label31.Text)).ToString();

            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo " + addCond + ") and ReLevel = N'فصل في كتاب'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label21.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo " +  addCond + ") and ReLevel = N'كتاب'", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            Label25.Text = dr[0].ToString();
            cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo " +  addCond + ") and ReLevel = N'ترجمة'", conn);
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

            cmd = new SqlCommand("SELECT isnull(count(distinct [RId]),0) FROM Reward_Support where RId in (select rid from ResearcherInfo " + addCond + ")", conn);
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
                Chart2.Series["serie"].Points.InsertY(1, C);
                Chart2.Series["serie"].Points[1].Color = Color.LightGray;
                Chart2.Series["serie"].Points[1].AxisLabel = "النشاطات التأليفية";
                Chart2.Series["serie"].Points.InsertY(2, P);
                Chart2.Series["serie"].Points[2].Color = Color.LightSeaGreen;
                Chart2.Series["serie"].Points[2].AxisLabel = "مشاركات المؤتمرات";
            }
            double RR = (R / 70 > 1 ? 1 : R / 70);
            double RC = (C / 2 > 1 ? 1 : C / 20);
            double RP = (P / 10 > 1 ? 1 : P / 10);
            lblComprehensiveRPer.Text = ((RR + RP + RC) / 3).ToString("P");

            DataTable newD = new DataTable();
            newD.Columns.Add("Value");
            Chart1.Series["Series1"].Points.Clear();
            for (int j = HYear; j <= DateTime.Now.Year; j++)
            {
                cmd = new SqlCommand("SELECT isnull(count(distinct ri.reid),0) cnt FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo " + addCond + ") and reyear=" + j, conn);
                dr = cmd.ExecuteReader();
                dr.Read();
                //Chart1.Series["Series1"].Points.AddXY(j, dr[0]);
                Chart1.Series["Series1"].Points.InsertY(j - HYear, dr[0]);
                Chart1.Series["Series1"].Points[j - HYear].AxisLabel = j.ToString();
                Chart1.Series["Series1"].Points[j - HYear].Color = Color.PaleVioletRed;
                DataRow row = newD.NewRow();
                row[0] = dr[0];
                newD.Rows.Add(row);
            }
            //}
            //catch { }

            cmd = new SqlCommand("SELECT isnull(sum(recitation),0) FROM [ResearchsInfo] RI  where reid in (select distinct reid From researcherinfo r1,Research_Researcher r2 where r1.rid=r2.rid and college=N'" + Session["userCollege"] + "' and Dept=N'" + Session["userDept"] + "')", conn);
            dr = cmd.ExecuteReader();
            dr.Read();

            lblEAvg.Text = dr[0].ToString();


            conn.Close();

            calCitation();
            //if (ddlDept.SelectedValue == "0")
            //    getRCountPerYear();
            //else
                getRCountPerYear(Session["userCollege"].ToString(), Session["userDept"].ToString());
        }
        protected void calCitation()
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand("select ROW_NUMBER() OVER(ORDER BY ReCitation Desc) AS row_num,ReCitation from ResearchsInfo where reid in (select distinct reid From researcherinfo r1,Research_Researcher r2  where  r1.rid=r2.rid and college=N'" + ddlResearcher.SelectedValue + "'" + (ddlDept.SelectedValue != "0" ? " and Dept=N'" + ddlDept.SelectedValue + "')" : ")"), conn);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            int h_index = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
                if (Convert.ToInt16(dt.Rows[i][0].ToString()) <= Convert.ToInt16(dt.Rows[i][1].ToString()))
                {
                    h_index = i + 1;
                }
                else
                    break;
            lblProductivity.Text = h_index.ToString();
            conn.Close();
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
                SqlCommand cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo where College=N'" + ddlResearcher.SelectedValue + "') and ReYear=" + y1 + " and ReMonth=" + i , conn);
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
                SqlCommand cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo where College=N'" + ddlResearcher.SelectedValue + "') and ReYear=" + y2 + " and ReMonth=" + i, conn);
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

            string sql = "";
            sql = "select Distinct count(RId) EmpCount From ResearcherInfo where RStatus=N'IN' and College=N'"+ddlResearcher.SelectedValue+"'";
            SqlCommand cmdCD = new SqlCommand(sql, conn);
            DataTable dtCD = new DataTable();
            dtCD.Load(cmdCD.ExecuteReader());

            lblCurrentR.Text = RTotal.ToString();
            lblCurrentRCollege.Text= RTotal.ToString();
            if (lblCurrentR.Text != "")
                if (Convert.ToInt16(lblCurrentR.Text) >=Convert.ToInt16(dtCD.Rows[0]["EmpCount"]))
                    currentDiv.Style.Add("background-color", "#6d8f28");
                else
                    currentDiv.Style.Add("background-color", "#e61b23");

            if (lblCurrentRCollege.Text != "")
                if (Convert.ToInt16(lblCurrentRCollege.Text) >= Convert.ToInt16(dtCD.Rows[0]["EmpCount"]))
                    currentDiv1.Style.Add("background-color", "#6d8f28");
                else
                    currentDiv1.Style.Add("background-color", "#e61b23");

            sql = "select Distinct count(RId) EmpCount From ResearcherInfo where RStatus=N'IN' and College=N'" + ddlResearcher.SelectedValue + "' and dept=N'" + ddlDept.SelectedValue + "'";
            SqlCommand cmdCD1 = new SqlCommand(sql, conn);
            DataTable dtCD1 = new DataTable();
            dtCD1.Load(cmdCD1.ExecuteReader());

            if (lblCurrentRDept.Text != "")
                if (Convert.ToInt16(lblCurrentRDept.Text) >= Convert.ToInt16(dtCD1.Rows[0]["EmpCount"]))
                    currentDiv2.Style.Add("background-color", "#6d8f28");
                else
                    currentDiv2.Style.Add("background-color", "#e61b23");

            if (Convert.ToInt16(lblCurrentR.Text) >= Convert.ToInt16(dtCD.Rows[0]["EmpCount"]))
            {
                string per = Math.Round(100.0 / divIndex, 0).ToString() + "%";
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
                    if (i >= 4 && i <= 6)
                        Div.Style.Add(HtmlTextWriterStyle.Width, per1);
                    else
                        Div.Style.Add(HtmlTextWriterStyle.Width, per);
                }
            }



            conn.Close();
        }

        protected void getRCountPerYear(string dept)
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

            int m1 = DateTime.Now.Month;
            int m2 = 0;

            int divIndex = 0;
            int RTotal = 0, RDTotal = 0;
            if (DateTime.Now.Month < mf)
            {
                //y = y - 1;
                m1 = 12;
                m2 = DateTime.Now.Month;
            }

            for (int i = mf; i <= m1; i++)
            {
                SqlCommand cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo where College=N'" + ddlResearcher.SelectedValue + "') and ReYear=" + y1 + " and ReMonth=" + i, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                RTotal += Convert.ToInt16(dr[0].ToString());

                cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo where College=N'" + ddlResearcher.SelectedValue + "' and Dept=N'" + dept + "') and ReYear=" + y1 + " and ReMonth=" + i, conn);
                SqlDataReader dr1 = cmd.ExecuteReader();
                dr1.Read();
                RDTotal += Convert.ToInt16(dr1[0].ToString());

                divIndex++;

                HtmlGenericControl createDiv = new HtmlGenericControl("DIV");
                createDiv.ID = "createDiv_" + divIndex;

                createDiv.Style.Add(HtmlTextWriterStyle.Color, "black");
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

                if (Convert.ToInt16(dr1[0].ToString()) == 0)
                {
                    createDiv.Style.Add(HtmlTextWriterStyle.BackgroundColor, "red");
                    createDivB.InnerHtml = "-";
                }
                else
                {
                    createDivB.InnerHtml = dr1[0].ToString();
                    createDiv.Style.Add(HtmlTextWriterStyle.BackgroundColor, "green");
                    curr = true;
                    //RDTotal += Convert.ToInt16(dr[0].ToString());
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
                SqlCommand cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo where College=N'" + ddlResearcher.SelectedValue + "') and ReYear=" + y2 + " and ReMonth=" + i, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                RTotal += Convert.ToInt16(dr[0].ToString());

                cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo where College=N'" + ddlResearcher.SelectedValue + "' and Dept=N'" + dept + "') and ReYear=" + y2 + " and ReMonth=" + i, conn);
                SqlDataReader dr1 = cmd.ExecuteReader();
                dr1.Read();
                RDTotal += Convert.ToInt16(dr1[0].ToString());

                divIndex++;
                HtmlGenericControl createDiv = new HtmlGenericControl("DIV");
                createDiv.ID = "createDiv_" + divIndex;

                createDiv.Style.Add(HtmlTextWriterStyle.Color, "black");
                createDiv.Style.Add(HtmlTextWriterStyle.Width, "10%");
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

                if (Convert.ToInt16(dr1[0].ToString()) == 0)
                {
                    createDiv.Style.Add(HtmlTextWriterStyle.BackgroundColor, "red");
                    createDivB.InnerHtml = "-";// dr[0].ToString();
                }
                else
                {
                    createDivB.InnerHtml = dr[0].ToString();
                    createDiv.Style.Add(HtmlTextWriterStyle.BackgroundColor, "green");
                    curr = true;
                    //RTotal += Convert.ToInt16(dr[0].ToString());
                }
                createDiv.Style.Add("font-size", "small");
                createDiv.Style.Add("word-wrap", "break-word");
                createDiv.Style.Add("box-sizing", "border-box");
                createDiv.Style.Add("border", "1px solid");
                createDiv.Controls.Add(createDivU);
                createDiv.Controls.Add(createDivB);
                curDiv.Controls.Add(createDiv);
            }

            lblCurrentRCollege.Text = RTotal.ToString();
            lblCurrentRDept.Text = RDTotal.ToString();
            string sql = "";
            sql = "select Distinct count(RId) EmpCount From ResearcherInfo where RStatus=N'IN' and College=N'" + ddlResearcher.SelectedValue + "'";
            SqlCommand cmdCD = new SqlCommand(sql, conn);
            DataTable dtCD = new DataTable();
            dtCD.Load(cmdCD.ExecuteReader());

            if (Convert.ToInt16(lblCurrentRCollege.Text) >= Convert.ToInt16(dtCD.Rows[0]["EmpCount"]))
                currentDiv1.Style.Add("background-color", "green");
            else
                currentDiv1.Style.Add("background-color", "red");

            sql = "select Distinct count(RId) EmpCount From ResearcherInfo where RStatus=N'IN' and College=N'" + ddlResearcher.SelectedValue + "' and dept=N'" + ddlDept.SelectedValue + "'";
            SqlCommand cmdCD1 = new SqlCommand(sql, conn);
            DataTable dtCD1 = new DataTable();
            dtCD1.Load(cmdCD1.ExecuteReader());

            if (Convert.ToInt16(lblCurrentRDept.Text) >= Convert.ToInt16(dtCD1.Rows[0]["EmpCount"]))
                currentDiv2.Style.Add("background-color", "green");
            else
                currentDiv2.Style.Add("background-color", "red");


            if (Convert.ToInt16(lblCurrentRDept.Text) >= Convert.ToInt16(dtCD1.Rows[0]["EmpCount"]))
            {
                string per = Math.Round(100.0 / divIndex, 0).ToString() + "%";
                string per1 = Math.Ceiling(100.0 / divIndex).ToString() + "%";
                for (int i = 1; i <= divIndex; i++)
                {
                    HtmlGenericControl Div = curDiv.FindControl("createDiv_" + i.ToString()) as HtmlGenericControl;
                    Div.Style.Add(HtmlTextWriterStyle.BackgroundColor, "green");
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
                    Div.Style.Add(HtmlTextWriterStyle.BackgroundColor, "red");
                    if (i >= 4 && i <= 6)
                        Div.Style.Add(HtmlTextWriterStyle.Width, per1);
                    else
                        Div.Style.Add(HtmlTextWriterStyle.Width, per);
                }
            }


            //lblCurrentRCollege.Text = RTotal.ToString();
            //lblCurrentRDept.Text = RDTotal.ToString();
            //string sql = "";
            //sql = "select Distinct count(RId) EmpCount From ResearcherInfo where RStatus=N'IN' and College=N'" + ddlResearcher.SelectedValue + "'";
            //SqlCommand cmdCD = new SqlCommand(sql, conn);
            //DataTable dtCD = new DataTable();
            //dtCD.Load(cmdCD.ExecuteReader());

            //if (Convert.ToInt16(lblCurrentRCollege.Text) >= Convert.ToInt16(dtCD.Rows[0]["EmpCount"]))
            //    currentDiv1.Style.Add("background-color", "green");
            //else
            //    currentDiv1.Style.Add("background-color", "red");

            //sql = "select Distinct count(RId) EmpCount From ResearcherInfo where RStatus=N'IN' and College=N'" + ddlResearcher.SelectedValue + "' and dept=N'" + ddlDept.SelectedValue + "'";
            //SqlCommand cmdCD1 = new SqlCommand(sql, conn);
            //DataTable dtCD1 = new DataTable();
            //dtCD1.Load(cmdCD1.ExecuteReader());

            //if (Convert.ToInt16(lblCurrentRDept.Text) >= Convert.ToInt16(dtCD1.Rows[0]["EmpCount"]))
            //    currentDiv2.Style.Add("background-color", "green");
            //else
            //    currentDiv2.Style.Add("background-color", "red");


            conn.Close();
        }

        protected void getRCountPerYear(string college, string dept)
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

            int m1 = DateTime.Now.Month;
            int m2 = 0;

            int divIndex = 0;
            int RTotal = 0, RDTotal = 0;
            if (DateTime.Now.Month < mf)
            {
                //y = y - 1;
                m1 = 12;
                m2 = DateTime.Now.Month;
            }

            for (int i = mf; i <= m1; i++)
            {
                SqlCommand cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo where College=N'" + college + "') and ReYear=" + y1 + " and ReMonth=" + i, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                RTotal += Convert.ToInt16(dr[0].ToString());

                cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo where College=N'" + college + "' and Dept=N'" + dept + "') and ReYear=" + y1 + " and ReMonth=" + i, conn);
                SqlDataReader dr1 = cmd.ExecuteReader();
                dr1.Read();
                RDTotal += Convert.ToInt16(dr1[0].ToString());

                divIndex++;

                HtmlGenericControl createDiv = new HtmlGenericControl("DIV");
                createDiv.ID = "createDiv_" + divIndex;

                createDiv.Style.Add(HtmlTextWriterStyle.Color, "black");
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

                if (Convert.ToInt16(dr1[0].ToString()) == 0)
                {
                    createDiv.Style.Add(HtmlTextWriterStyle.BackgroundColor, "red");
                    createDivB.InnerHtml = "-";
                }
                else
                {
                    createDivB.InnerHtml = dr1[0].ToString();
                    createDiv.Style.Add(HtmlTextWriterStyle.BackgroundColor, "green");
                    curr = true;
                    //RDTotal += Convert.ToInt16(dr[0].ToString());
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
                SqlCommand cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo where College=N'" + college + "') and ReYear=" + y2 + " and ReMonth=" + i, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                RTotal += Convert.ToInt16(dr[0].ToString());

                cmd = new SqlCommand("SELECT isnull(count(distinct ri.[ReId]),0) FROM [ResearchsInfo] RI, Research_Researcher RR where RI.ReId = RR.ReId and rr.RId in (select rid from ResearcherInfo where College=N'" + college + "' and Dept=N'" + dept + "') and ReYear=" + y2 + " and ReMonth=" + i, conn);
                SqlDataReader dr1 = cmd.ExecuteReader();
                dr1.Read();
                RDTotal += Convert.ToInt16(dr1[0].ToString());

                divIndex++;
                HtmlGenericControl createDiv = new HtmlGenericControl("DIV");
                createDiv.ID = "createDiv_" + divIndex;

                createDiv.Style.Add(HtmlTextWriterStyle.Color, "black");
                createDiv.Style.Add(HtmlTextWriterStyle.Width, "10%");
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

                if (Convert.ToInt16(dr1[0].ToString()) == 0)
                {
                    createDiv.Style.Add(HtmlTextWriterStyle.BackgroundColor, "red");
                    createDivB.InnerHtml = "-";// dr[0].ToString();
                }
                else
                {
                    createDivB.InnerHtml = dr[0].ToString();
                    createDiv.Style.Add(HtmlTextWriterStyle.BackgroundColor, "green");
                    curr = true;
                    //RTotal += Convert.ToInt16(dr[0].ToString());
                }
                createDiv.Style.Add("font-size", "small");
                createDiv.Style.Add("word-wrap", "break-word");
                createDiv.Style.Add("box-sizing", "border-box");
                createDiv.Style.Add("border", "1px solid");
                createDiv.Controls.Add(createDivU);
                createDiv.Controls.Add(createDivB);
                curDiv.Controls.Add(createDiv);
            }

            lblCurrentRCollege.Text = RTotal.ToString();
            lblCurrentRDept.Text = RDTotal.ToString();
            string sql = "";
            sql = "select Distinct count(RId) EmpCount From ResearcherInfo where RStatus=N'IN' and College=N'" + college + "'";
            SqlCommand cmdCD = new SqlCommand(sql, conn);
            DataTable dtCD = new DataTable();
            dtCD.Load(cmdCD.ExecuteReader());

            if (Convert.ToInt16(lblCurrentRCollege.Text) >= Convert.ToInt16(dtCD.Rows[0]["EmpCount"]))
                currentDiv1.Style.Add("background-color", "green");
            else
                currentDiv1.Style.Add("background-color", "red");

            sql = "select Distinct count(RId) EmpCount From ResearcherInfo where RStatus=N'IN' and College=N'" + college + "' and dept=N'" + dept + "'";
            SqlCommand cmdCD1 = new SqlCommand(sql, conn);
            DataTable dtCD1 = new DataTable();
            dtCD1.Load(cmdCD1.ExecuteReader());

            if (Convert.ToInt16(lblCurrentRDept.Text) >= Convert.ToInt16(dtCD1.Rows[0]["EmpCount"]))
                currentDiv2.Style.Add("background-color", "green");
            else
                currentDiv2.Style.Add("background-color", "red");


            if (Convert.ToInt16(lblCurrentRDept.Text) >= Convert.ToInt16(dtCD1.Rows[0]["EmpCount"]))
            {
                string per = Math.Round(100.0 / divIndex, 0).ToString() + "%";
                string per1 = Math.Ceiling(100.0 / divIndex).ToString() + "%";
                for (int i = 1; i <= divIndex; i++)
                {
                    HtmlGenericControl Div = curDiv.FindControl("createDiv_" + i.ToString()) as HtmlGenericControl;
                    Div.Style.Add(HtmlTextWriterStyle.BackgroundColor, "green");
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
                    Div.Style.Add(HtmlTextWriterStyle.BackgroundColor, "red");
                    if (i >= 4 && i <= 6)
                        Div.Style.Add(HtmlTextWriterStyle.Width, per1);
                    else
                        Div.Style.Add(HtmlTextWriterStyle.Width, per);
                }
            }


            conn.Close();
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session["logSession"] = "logout";
            Session["userid"] = null;
            Session["userrole"] = null;
            Response.Redirect("IndexCollege.aspx");
        }
    }
}