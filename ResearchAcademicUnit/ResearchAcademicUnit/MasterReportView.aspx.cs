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
    public partial class MasterReportView : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl divh = (HtmlGenericControl)Page.Master.FindControl("prinOut");
            divh.Visible = false;

            HtmlGenericControl divf = (HtmlGenericControl)Page.Master.FindControl("printfooter");
            divf.Visible = false;

            if (!IsPostBack)
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                if (Request.QueryString["ReportId"] != null && Request.QueryString["ReportId"] != string.Empty)
                    lblReportId.Text = Request.QueryString["ReportId"];

                if (Request.QueryString["StudId"] != null && Request.QueryString["StudId"] != string.Empty)
                    lblStudId.Text = Request.QueryString["StudId"];
                SqlCommand cmd = new SqlCommand("select * from MeetingReport where Studid=" + lblStudId.Text + " and ReportId=" + lblReportId.Text, conn);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count != 0)
                {
                    lblDate.Text = Convert.ToDateTime(dt.Rows[0]["ReportDate"]).ToString("dd-MM-yyyy");
                    lblStudName.Text = dt.Rows[0]["StudName"].ToString();
                    cmd = new SqlCommand("Select CollegeName From Faculty where AutoId=" + dt.Rows[0]["StudFaculty"].ToString(), conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    dr.Read();
                    lblStudFaculty.Text = dt.Rows[0]["StudFaculty"].ToString() == "0" ? "" : dr[0].ToString();


                    lblStudMajor.Text = dt.Rows[0]["StudMajor"].ToString();
                    lblSupName.Text = dt.Rows[0]["SupervisorName"].ToString();

                    cmd = new SqlCommand("Select CollegeName From Faculty where AutoId=" + dt.Rows[0]["SuperFaculty"].ToString(), conn);
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    lblSupFaculty.Text = dt.Rows[0]["SuperFaculty"].ToString()=="0"? "": dr[0].ToString();

                    cmd = new SqlCommand("Select DeptName From Department where AutoId=" + dt.Rows[0]["SuperDept"].ToString(), conn);
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    lblSupDpt.Text = dt.Rows[0]["SuperDept"].ToString()=="0"?"": dr[0].ToString();

                    thesisTitleDiv.InnerHtml = dt.Rows[0]["ThsisTitle"].ToString();
                    studReqDiv.InnerHtml = dt.Rows[0]["StudAch"].ToString();
                    NextWeekDiv.InnerHtml = dt.Rows[0]["StudReqNextWeek"].ToString();
                    if (dt.Rows[0]["MeetingMthod"].ToString() != "")
                    {
                        string[] mm = dt.Rows[0]["MeetingMthod"].ToString().Split(',');
                        for (int i = 0; i <= 6; i++)
                            for (int j = 0; j < mm.Length; j++)
                                if (chkMethod.Items[i].Value == mm[j])
                                    chkMethod.Items[i].Selected = true;
                    }
                    lblStudSign.Text = lblStudName.Text;
                    lblSuperSign.Text = lblSupName.Text;
                    //lblStudName.Text = dt.Rows[0]["MeetingMthod"].ToString();
                }
                conn.Close(); 
            }
        }
    }
}