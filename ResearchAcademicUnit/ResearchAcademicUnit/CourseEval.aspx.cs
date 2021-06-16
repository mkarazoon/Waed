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
    public partial class CourseEval : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);
        static string connstring1 = System.Configuration.ConfigurationManager.ConnectionStrings["MEUCV"].ConnectionString;
        SqlConnection conn1 = new SqlConnection(connstring1);

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
                if (conn1.State == ConnectionState.Broken || conn1.State == ConnectionState.Closed)
                    conn1.Open();

                SqlCommand cmd = new SqlCommand("select * from CourseEval where jobid=" + Session["userid"] + " and courseid=" + Session["CID"], conn);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count != 0)
                {
                    rd1.SelectedValue=dt.Rows[0][4].ToString();
                    rd2.SelectedValue = dt.Rows[0][5].ToString();
                    rd3.SelectedValue = dt.Rows[0][6].ToString();
                    rd4.SelectedValue = dt.Rows[0][7].ToString();
                    rd5.SelectedValue = dt.Rows[0][8].ToString();
                    rd6.SelectedValue = dt.Rows[0][9].ToString();
                    rd7.SelectedValue = dt.Rows[0][10].ToString();
                    rd8.SelectedValue = dt.Rows[0][11].ToString();
                    rd9.SelectedValue = dt.Rows[0][12].ToString();
                    rd10.SelectedValue = dt.Rows[0][13].ToString();
                    rd11.SelectedValue = dt.Rows[0][14].ToString();
                    rd12.SelectedValue = dt.Rows[0][15].ToString();
                    rd13.SelectedValue = dt.Rows[0][16].ToString();
                    rd14.SelectedValue = dt.Rows[0][17].ToString();
                    rd15.SelectedValue = dt.Rows[0][18].ToString();
                    rd16.SelectedValue = dt.Rows[0][19].ToString();
                    rd17.SelectedValue = dt.Rows[0][20].ToString();
                    rd18.SelectedValue = dt.Rows[0][21].ToString();
                    rd19.SelectedValue = dt.Rows[0][22].ToString();
                    rd20.SelectedValue = dt.Rows[0][23].ToString();
                    txtQ21.Text= lblQ21.Text=dt.Rows[0][24].ToString();
                    txtQ22.Text = lblQ22.Text= dt.Rows[0][25].ToString();
                    
                    int sum = 0;
                    sum += Convert.ToInt16(rd1.SelectedValue);
                    sum += Convert.ToInt16(rd2.SelectedValue);
                    sum += Convert.ToInt16(rd3.SelectedValue);
                    sum += Convert.ToInt16(rd4.SelectedValue);
                    sum += Convert.ToInt16(rd5.SelectedValue);
                    sum += Convert.ToInt16(rd6.SelectedValue);
                    sum += Convert.ToInt16(rd7.SelectedValue);
                    sum += Convert.ToInt16(rd8.SelectedValue);
                    sum += Convert.ToInt16(rd9.SelectedValue);
                    sum += Convert.ToInt16(rd10.SelectedValue);
                    sum += Convert.ToInt16(rd11.SelectedValue);
                    sum += Convert.ToInt16(rd12.SelectedValue);
                    sum += Convert.ToInt16(rd13.SelectedValue);
                    sum += Convert.ToInt16(rd14.SelectedValue);
                    sum += Convert.ToInt16(rd15.SelectedValue);
                    sum += Convert.ToInt16(rd16.SelectedValue);
                    sum += Convert.ToInt16(rd17.SelectedValue);
                    sum += Convert.ToInt16(rd18.SelectedValue);
                    sum += Convert.ToInt16(rd19.SelectedValue);
                    sum += Convert.ToInt16(rd20.SelectedValue);

                    lblEvalC.Text = "تقييم الدورة هو    " + sum;
                    Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('تم تقييم الدورة من قبل وكان تقييمك لها " + sum + "');", true);
                    Button1.Visible = false;


                }
                //else
                //{
                    SqlCommand cmdd = new SqlCommand("select * from ResearcherInfo where acdid=" + Session["userid"], conn);
                    DataTable dtt = new DataTable();
                    dtt.Load(cmdd.ExecuteReader());
                    if (dtt.Rows.Count != 0)
                    {
                        lblName.Text = Session["userName"].ToString();
                        
                        lblCollege.Text = dtt.Rows[0]["College"].ToString();
                        lblDept.Text = dtt.Rows[0]["Dept"].ToString();
                        lblDegree.Text = dtt.Rows[0]["RLevel"].ToString();
                        lblMinor.Text = dtt.Rows[0]["Major"].ToString();
                        lblHDate.Text =Convert.ToDateTime(dtt.Rows[0]["HDate"]).ToString("dd-MM-yyyy");
                        lblCourseTitle.Text = Session["CTitle"].ToString();
                        lblTrainerName.Text = Session["TName"].ToString();

                        cmdd = new SqlCommand("select * from InstInfo where InstJobId=" + Session["userid"], conn1);
                        dtt = new DataTable();
                        dtt.Load(cmdd.ExecuteReader());
                        lblSex.Text = dtt.Rows[0]["Sex"].ToString() == "M" ? "ذكر" : "أنثى";
                    }
                //}
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd1 = new SqlCommand("select * from CourseEval where jobid=" + Session["userid"] + " and courseid=" + Session["CID"], conn);
            DataTable dt = new DataTable();
            dt.Load(cmd1.ExecuteReader());
            if (dt.Rows.Count != 0)
            {
                string url = "AvailableCourse.aspx";
                string script = "window.onload = function(){ alert('";
                script += "تم تقييم الدورة من قبل";
                script += "');";
                script += "window.location = '";
                script += url;
                script += "'; }";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
            }
            else
            {

                string sql = "Insert into CourseEval values(";
                sql += Session["userid"] + ",@edate,";
                sql += Session["CID"] + ",";
                sql += rd1.SelectedValue + ",";
                sql += rd2.SelectedValue + ",";
                sql += rd3.SelectedValue + ",";
                sql += rd4.SelectedValue + ",";
                sql += rd5.SelectedValue + ",";
                sql += rd6.SelectedValue + ",";
                sql += rd7.SelectedValue + ",";
                sql += rd8.SelectedValue + ",";
                sql += rd9.SelectedValue + ",";
                sql += rd10.SelectedValue + ",";
                sql += rd11.SelectedValue + ",";
                sql += rd12.SelectedValue + ",";
                sql += rd13.SelectedValue + ",";
                sql += rd14.SelectedValue + ",";
                sql += rd15.SelectedValue + ",";
                sql += rd16.SelectedValue + ",";
                sql += rd17.SelectedValue + ",";
                sql += rd18.SelectedValue + ",";
                sql += rd19.SelectedValue + ",";
                sql += rd20.SelectedValue + ",@q21,@q22)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@edate", DateTime.Now.Date);
                cmd.Parameters.AddWithValue("@q21", txtQ21.Text);
                cmd.Parameters.AddWithValue("@q22", txtQ22.Text);
                cmd.ExecuteNonQuery();

                int sum = 0;
                sum += Convert.ToInt16(rd1.SelectedValue);
                sum += Convert.ToInt16(rd2.SelectedValue);
                sum += Convert.ToInt16(rd3.SelectedValue);
                sum += Convert.ToInt16(rd4.SelectedValue);
                sum += Convert.ToInt16(rd5.SelectedValue);
                sum += Convert.ToInt16(rd6.SelectedValue);
                sum += Convert.ToInt16(rd7.SelectedValue);
                sum += Convert.ToInt16(rd8.SelectedValue);
                sum += Convert.ToInt16(rd9.SelectedValue);
                sum += Convert.ToInt16(rd10.SelectedValue);
                sum += Convert.ToInt16(rd11.SelectedValue);
                sum += Convert.ToInt16(rd12.SelectedValue);
                sum += Convert.ToInt16(rd13.SelectedValue);
                sum += Convert.ToInt16(rd14.SelectedValue);
                sum += Convert.ToInt16(rd15.SelectedValue);
                sum += Convert.ToInt16(rd16.SelectedValue);
                sum += Convert.ToInt16(rd17.SelectedValue);
                sum += Convert.ToInt16(rd18.SelectedValue);
                sum += Convert.ToInt16(rd19.SelectedValue);
                sum += Convert.ToInt16(rd20.SelectedValue);


                lblEvalC.Text = "تقييم الدورة هو    " + sum;
                Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('تم تقييم الدورة بنجاج بمعدل  " + sum + "');", true);


            }
            conn.Close();
        }
    }
}