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
    public partial class CollegeAbstract : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["logSession"] == null || Session["userid"] == null || Session["userrole"].ToString() == "6")
            {
                Response.Redirect("Index.aspx");
            }
            Session["backurl"] = "Index.aspx";
            Label lblUserVal = (Label)Page.Master.FindControl("lblPageName");
            lblUserVal.Text = "ملخص الأبحاث للكلية";

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
                    deptDirector();
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
                }
                conn.Close();
            }
        }

        protected void ddlResearcher_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlResearcher.SelectedValue != "0")
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();
                //try
                //{
                string sql = "";
                sql = "SELECT distinct ri.[RId] row_num,REName,";
                sql += "(select count(rr1.[ReId]) from[Research_Researcher] rr1,ResearchsInfo r where rr1.RId = ri.RId and r.ReId = rr1.ReId and r.ReType = N'بحوث علمية') RCount,";
                sql += "(select count(rr1.[ReId]) from[Research_Researcher] rr1,ResearchsInfo r where rr1.RId = ri.RId and r.ReId = rr1.ReId and r.ReType = N'مؤتمر علمي') CCount,";
                sql += "(select count(rr1.[ReId]) from[Research_Researcher] rr1,ResearchsInfo r where rr1.RId = ri.RId and r.ReId = rr1.ReId and r.ReType = N'نشاط تأليفي') PCount,";
                sql += "(select count(rr1.[ReId]) from[Research_Researcher] rr1,ResearchsInfo r where rr1.RId = ri.RId and r.ReId = rr1.ReId ) total";
                sql += " FROM [Research_Researcher] rr,ResearcherInfo ri";
                sql += " where rr.RId = ri.RId and College = N'"+ddlResearcher.SelectedValue+"'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                lblCollegeInfo.Text =ddlResearcher.SelectedItem.Text +  " - جميع الأقسام";
                Label lblinfo = (Label)Page.Master.FindControl("lblinfo");
                lblinfo.Text = "البطاقة البحثية كلية " + lblCollegeInfo.Text;

                if (dt.Rows.Count != 0)
                {
                    Session["data"] = dt;

                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    msgDiv.Visible = false;
                    dataDiv.Visible = true;
                }
                else
                {
                    msgDiv.Visible = true;
                    dataDiv.Visible = false;
                }

                cmd = new SqlCommand("Select distinct Dept From ResearcherInfo where College=N'" + ddlResearcher.SelectedValue + "'", conn);
                ddlDept.DataSource = cmd.ExecuteReader();
                ddlDept.DataValueField = "Dept";
                ddlDept.DataTextField = "Dept";
                ddlDept.DataBind();
                ddlDept.Items.Insert(0, "اختيار القسم");
                ddlDept.Items[0].Value = "0";
                msgDiv.Visible = false;
                conn.Close();
            }
            else
            {
                ddlDept.Items.Clear();
                msgDiv.Visible = true;
                dataDiv.Visible = false;
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("IndexCollege.aspx");
        }

        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDept.SelectedValue != "0")
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();
                //try
                //{
                string sql = "";
                sql = "SELECT distinct ri.[RId] row_num,REName,";
                sql += "(select count(rr1.[ReId]) from[Research_Researcher] rr1,ResearchsInfo r where rr1.RId = ri.RId and r.ReId = rr1.ReId and r.ReType = N'بحوث علمية') RCount,";
                sql += "(select count(rr1.[ReId]) from[Research_Researcher] rr1,ResearchsInfo r where rr1.RId = ri.RId and r.ReId = rr1.ReId and r.ReType = N'مؤتمر علمي') CCount,";
                sql += "(select count(rr1.[ReId]) from[Research_Researcher] rr1,ResearchsInfo r where rr1.RId = ri.RId and r.ReId = rr1.ReId and r.ReType = N'نشاط تأليفي') PCount,";
                sql += "(select count(rr1.[ReId]) from[Research_Researcher] rr1,ResearchsInfo r where rr1.RId = ri.RId and r.ReId = rr1.ReId ) total";
                sql += " FROM [Research_Researcher] rr,ResearcherInfo ri";
                sql += " where rr.RId = ri.RId and College = N'" + ddlResearcher.SelectedValue + "'";
                sql += " and Dept=N'" + ddlDept.SelectedValue + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                lblCollegeInfo.Text = ddlResearcher.SelectedItem.Text + " - " + ddlDept.SelectedItem.Text;
                Label lblinfo = (Label)Page.Master.FindControl("lblinfo");
                lblinfo.Text = "البطاقة البحثية كلية " + lblCollegeInfo.Text;

                if (dt.Rows.Count != 0)
                {
                    Session["data"] = dt;

                    msgDiv.Visible = false;
                    dataDiv.Visible = true;

                }
                else
                {
                    Session["data"] = null;
                    msgDiv.Visible = true;
                    dataDiv.Visible = false;

                }

                GridView1.DataSource = Session["data"];
                GridView1.DataBind();

                conn.Close();
            }
            else
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();
                //try
                //{
                string sql = "";
                sql = "SELECT distinct ri.[RId] row_num,REName,";
                sql += "(select count(rr1.[ReId]) from[Research_Researcher] rr1,ResearchsInfo r where rr1.RId = ri.RId and r.ReId = rr1.ReId and r.ReType = N'بحوث علمية') RCount,";
                sql += "(select count(rr1.[ReId]) from[Research_Researcher] rr1,ResearchsInfo r where rr1.RId = ri.RId and r.ReId = rr1.ReId and r.ReType = N'مؤتمر علمي') CCount,";
                sql += "(select count(rr1.[ReId]) from[Research_Researcher] rr1,ResearchsInfo r where rr1.RId = ri.RId and r.ReId = rr1.ReId and r.ReType = N'نشاط تأليفي') PCount,";
                sql += "(select count(rr1.[ReId]) from[Research_Researcher] rr1,ResearchsInfo r where rr1.RId = ri.RId and r.ReId = rr1.ReId ) total";
                sql += " FROM [Research_Researcher] rr,ResearcherInfo ri";
                sql += " where rr.RId = ri.RId and College = N'" + ddlResearcher.SelectedValue + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count != 0)
                {
                    Session["data"] = dt;

                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    msgDiv.Visible = false;
                    dataDiv.Visible = true;

                }
                else
                {
                    Session["data"] = null;
                    msgDiv.Visible = true;
                    dataDiv.Visible = false;
                }

            }
        }

        protected void deptDirector()
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            //try
            //{
            string sql = "";
            sql = "SELECT distinct ri.[RId] row_num,REName,";
            sql += "(select count(rr1.[ReId]) from[Research_Researcher] rr1,ResearchsInfo r where rr1.RId = ri.RId and r.ReId = rr1.ReId and r.ReType = N'بحوث علمية') RCount,";
            sql += "(select count(rr1.[ReId]) from[Research_Researcher] rr1,ResearchsInfo r where rr1.RId = ri.RId and r.ReId = rr1.ReId and r.ReType = N'مؤتمر علمي') CCount,";
            sql += "(select count(rr1.[ReId]) from[Research_Researcher] rr1,ResearchsInfo r where rr1.RId = ri.RId and r.ReId = rr1.ReId and r.ReType = N'نشاط تأليفي') PCount,";
            sql += "(select count(rr1.[ReId]) from[Research_Researcher] rr1,ResearchsInfo r where rr1.RId = ri.RId and r.ReId = rr1.ReId ) total";
            sql += " FROM [Research_Researcher] rr,ResearcherInfo ri";
            sql += " where rr.RId = ri.RId and College = N'" + Session["userCollege"] + "' and Dept=N'"+ Session["userDept"] +"'";
            //sql += " and Dept=N'" + ddlDept.SelectedValue + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            lblCollegeInfo.Text = Session["userCollege"] + " - " + Session["userDept"];
            Label lblinfo = (Label)Page.Master.FindControl("lblinfo");
            lblinfo.Text = "البطاقة البحثية كلية " + lblCollegeInfo.Text;

            if (dt.Rows.Count != 0)
            {
                Session["data"] = dt;

                msgDiv.Visible = false;
                dataDiv.Visible = true;

            }
            else
            {
                Session["data"] = null;
                msgDiv.Visible = true;
                dataDiv.Visible = false;

            }

            GridView1.DataSource = Session["data"];
            GridView1.DataBind();

            conn.Close();
        }

    }
}