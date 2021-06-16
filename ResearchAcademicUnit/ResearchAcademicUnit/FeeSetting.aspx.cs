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
    public partial class FeeSetting : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.IsNewSession || Session["uid"] == null)
                Response.Redirect("Login.aspx");
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand cmd = new SqlCommand();
            if (!IsPostBack)
            {
                cmd = new SqlCommand("Select * From Faculty", conn);
                ddlFaculty.DataSource = cmd.ExecuteReader();
                ddlFaculty.DataValueField = "AutoId";
                ddlFaculty.DataTextField = "CollegeName";
                ddlFaculty.DataBind();
                
                ddlFaculty.Items.Insert(ddlFaculty.Items.Count, "ق.أ. عميد الدراسات العليا والبحث العلمي");
                ddlFaculty.Items[ddlFaculty.Items.Count-1].Value = (ddlFaculty.Items.Count).ToString();

                ddlFaculty.Items.Insert(ddlFaculty.Items.Count, "رئيس قسم البحث العلمي");
                ddlFaculty.Items[ddlFaculty.Items.Count-1].Value = (ddlFaculty.Items.Count ).ToString();

                ddlFaculty.Items.Insert(ddlFaculty.Items.Count, "رئيس الجامعة");
                ddlFaculty.Items[ddlFaculty.Items.Count - 1].Value = (ddlFaculty.Items.Count).ToString();

                ddlFaculty.Items.Insert(ddlFaculty.Items.Count, "نائب رئيس الجامعة");
                ddlFaculty.Items[ddlFaculty.Items.Count - 1].Value = (ddlFaculty.Items.Count).ToString();

                ddlFaculty.Items.Insert(ddlFaculty.Items.Count, "عميد الدراسات العليا والبحث العلمي");
                ddlFaculty.Items[ddlFaculty.Items.Count - 1].Value = (ddlFaculty.Items.Count).ToString();


                ddlFaculty.Items.Insert(0, "حدد الكلية");
                ddlFaculty.Items[0].Value = "0";
                cmd = new SqlCommand("Select Acdid,RaName From ResearcherInfo where RStatus='IN'", conn);
                ddlName.DataSource = cmd.ExecuteReader();
                ddlName.DataValueField = "Acdid";
                ddlName.DataTextField = "RaName";
                ddlName.DataBind();
                ddlName.Items.Insert(0, "حدد صاحب الصلاحية");
                ddlName.Items[0].Value = "0";
            }
            cmd = new SqlCommand(@"SELECT [PrivType],
		(
		case
		when PrivType=1 then N'عميد'
		when PrivType=2 then N'رئيس قسم'
		when PrivType=3 then N'مساعد اداري'
		when PrivType=4 then N'رئيس الجامعة'
		when PrivType=5 then N'نائب رئيس الجامعة'
		end
		) privname
      ,[PrivFacultyId],
	  (case when PrivFacultyId=10 then N'ق.أ. عمادة الدراسات العليا والبحث العلمي'
	        when PrivFacultyId=11 then N'رئيس قسم البحث العلمي'
			when PrivFacultyId=12 then N'رئيس الجامعة'
			when PrivFacultyId=13 then N'نائب رئيس الجامعة'
when PrivFacultyId=14 then N'عمادة الدراسات العليا والبحث العلمي'
			else (select f.CollegeName from Faculty f where f.AutoId=p.PrivFacultyId)
			end
			) College
      ,[PrivDeptId]
	  ,(case
		when PrivDeptId=0 then '' else (select d.DeptName from Department d where d.AutoId=p.PrivDeptId)
		end
	  ) deptname
      ,[PrivTo],
	  (select raname from ResearcherInfo r where r.AcdId= p.PrivTo) Name
  FROM Priviliges p
  ", conn);
            GridView1.DataSource = cmd.ExecuteReader();
            GridView1.DataBind();
            conn.Close();
        }

        protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFaculty.SelectedValue != "0")
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("select * from Department where CAutoid=" + ddlFaculty.SelectedValue, conn);
                ddlDept.DataSource = cmd.ExecuteReader();
                ddlDept.DataValueField = "AutoId";
                ddlDept.DataTextField = "DeptName";
                ddlDept.DataBind();
                ddlDept.Items.Insert(0, "حدد القسم");
                ddlDept.Items[0].Value = "0";


                conn.Close();
            }
        }

        protected void btnSaveAuth_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            try
            {

                SqlCommand cmd;
                cmd=new SqlCommand("select * from Priviliges where PrivType="+ddlAuthLevel.SelectedValue+" and PrivFacultyId=" + ddlFaculty.SelectedValue + " and PrivDeptId=" + ddlDept.SelectedValue, conn);
                DataTable dtData = new DataTable();
                dtData.Load(cmd.ExecuteReader());
                if (dtData.Rows.Count != 0)
                {
                    cmd = new SqlCommand("update Priviliges Set PrivTo=" + ddlName.SelectedValue + ",Email='"+txtEmail.Text+"' where PrivFacultyId=" + ddlFaculty.SelectedValue + " and PrivDeptId=" + ddlDept.SelectedValue, conn);
                    cmd.ExecuteNonQuery();
                }
                else
                {

                    cmd = new SqlCommand("Insert into Priviliges values(" + ddlAuthLevel.SelectedValue + "," + ddlFaculty.SelectedValue + "," + ddlDept.SelectedValue + "," + ddlName.SelectedValue + ",'"+txtEmail.Text+"')", conn);
                    cmd.ExecuteNonQuery();
                }

                Response.Redirect("FeeSetting.aspx");
            }
            catch
            {

            }

            conn.Close();
        }

        protected void ddlAuthLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand cmd;
            if (ddlAuthLevel.SelectedValue != "0")
            {
                if (ddlAuthLevel.SelectedValue != "3")
                {
                    cmd = new SqlCommand("Select Acdid,RaName From ResearcherInfo where RStatus='IN'", conn);
                    ddlName.DataSource = cmd.ExecuteReader();
                    ddlName.DataValueField = "Acdid";
                    ddlName.DataTextField = "RaName";
                    ddlName.DataBind();
                    ddlName.Items.Insert(0, "حدد صاحب الصلاحية");
                    ddlName.Items[0].Value = "0";
                }
                else
                {
                    cmd = new SqlCommand("Select id,Name From usersinfo", conn);
                    ddlName.DataSource = cmd.ExecuteReader();
                    ddlName.DataValueField = "id";
                    ddlName.DataTextField = "Name";
                    ddlName.DataBind();
                    ddlName.Items.Insert(0, "حدد صاحب الصلاحية");
                    ddlName.Items[0].Value = "0";
                }
            }

            conn.Close();
        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                for (int i = 0; i < GridView1.Rows.Count; i++)
                    if (GridView1.Rows[i].Cells[0].Text == "&nbsp;")
                    {
                        SqlCommand cmd = new SqlCommand("select name from usersinfo where id=" + GridView1.Rows[i].Cells[4].Text, conn);
                        SqlDataReader dr = cmd.ExecuteReader();
                        dr.Read();
                        GridView1.Rows[i].Cells[0].Text = dr[0].ToString();
                    }

                conn.Close();
            }
            catch { }
        }
    }
}