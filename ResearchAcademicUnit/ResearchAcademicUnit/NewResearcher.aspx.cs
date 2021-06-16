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
    public partial class NewResearcher : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        //[RId], [REName], [RaName], [College], [Dept], [RLevel], [RDegree], [HDate], [AcdId], [Major], [DegreeYear],
        //[PositionName], [ExpYear], [ProductivityPointer], [RCitationAvg], [RStatus], [REngName]

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uid"] == null)
                Response.Redirect("Login.aspx");
            Session["backurl"] = "Index.aspx";
            if (!IsPostBack)
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd1 = new SqlCommand("Select AutoId, CollegeName From Faculty", conn);
                ddlFacullty.DataSource = cmd1.ExecuteReader();
                ddlFacullty.DataTextField = "CollegeName";
                ddlFacullty.DataValueField = "CollegeName";
                ddlFacullty.DataBind();
                ddlFacullty.Items.Insert(0, "اختيار الكلية");
                ddlFacullty.Items[0].Value = "0";

                SqlCommand sqlCommand = new SqlCommand("SELECT isnull(max(cast(SUBSTRING(rid,4,len(rid)) as int))+1,1) FROM ResearcherInfo", conn);
                SqlDataReader dataReader = sqlCommand.ExecuteReader();
                dataReader.Read();
                txtRID.Text = "MEU0" + dataReader[0].ToString();

                SqlCommand cmdCheck = new SqlCommand("Select * from ResearcherInfo order by College,Dept,RStatus,RaName", conn);
                //if (!cmdCheck.ExecuteReader().HasRows)
                //{
                    GridView1.DataSource = cmdCheck.ExecuteReader();
                    GridView1.DataBind();
                //}

                DataTable dt1 = new DataTable();
                dt1.Columns.Add("Year");
                for (int i = 1920; i <= DateTime.Now.Date.Year; i++)
                {
                    DataRow row = dt1.NewRow();
                    row[0] = i;
                    dt1.Rows.Add(row);
                }

                ddlQualYear.DataSource = dt1;
                ddlQualYear.DataTextField = "Year";
                ddlQualYear.DataValueField = "Year";
                ddlQualYear.DataBind();
                ddlQualYear.Items.Insert(0, "حدد السنة");
                ddlQualYear.Items[0].Value = "0";

                DataTable dt = new DataTable();
                dt.Columns.Add("Year");
                for (int i = 1; i <= 100; i++)
                {
                    DataRow row = dt.NewRow();
                    row[0] = i;
                    dt.Rows.Add(row);
                }

                ddlExpYear.DataSource = dt;
                ddlExpYear.DataTextField = "Year";
                ddlExpYear.DataValueField = "Year";
                ddlExpYear.DataBind();
                ddlExpYear.Items.Insert(0, "حدد العدد");
                ddlExpYear.Items[0].Value = "0";


                conn.Close();
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("NewResearcher.aspx");
        }

        protected void ddlFacullty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFacullty.SelectedValue != "0")
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("Select d.AutoId,DeptName From Department d,Faculty f where cautoid = f.autoid and CollegeName=N'" + ddlFacullty.SelectedValue+"'" , conn);
                ddlDept.DataSource = cmd.ExecuteReader();
                ddlDept.DataValueField = "DeptName";
                ddlDept.DataTextField = "DeptName";
                ddlDept.DataBind();
                ddlDept.Items.Insert(0, "اختيار القسم");
                ddlDept.Items[0].Value = "0";
                conn.Close();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand cmdCheck = new SqlCommand("Select * from ResearcherInfo where RID=N'" + txtRID.Text+"'", conn);
                if (!cmdCheck.ExecuteReader().HasRows)
                {
                    string sql = "";
                    sql += "Insert into ResearcherInfo values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@p1", txtRID.Text);
                    cmd.Parameters.AddWithValue("@p2", txtReName.Text);
                    cmd.Parameters.AddWithValue("@p3", txtRaName.Text);
                    cmd.Parameters.AddWithValue("@p4", ddlFacullty.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@p5", ddlDept.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@p6", ddlRLevel.SelectedValue != "0" ? ddlRLevel.SelectedItem.Text : "");
                    cmd.Parameters.AddWithValue("@p7", ddlRDegree.SelectedValue != "0" ? ddlRDegree.SelectedItem.Text : "");
                    cmd.Parameters.AddWithValue("@p8", Convert.ToDateTime(txtHDate.Text));
                    cmd.Parameters.AddWithValue("@p9", txtAcdId.Text);
                    cmd.Parameters.AddWithValue("@p10", txtMajor.Text);
                    cmd.Parameters.AddWithValue("@p11", ddlQualYear.SelectedValue);
                    cmd.Parameters.AddWithValue("@p12", txtPositionName.Text);
                    cmd.Parameters.AddWithValue("@p13", ddlExpYear.SelectedValue);
                    cmd.Parameters.AddWithValue("@p14", txtProductivityPointer.Text);
                    cmd.Parameters.AddWithValue("@p15", txtRCitationAvg.Text);
                    cmd.Parameters.AddWithValue("@p16", ddlRStatus.SelectedValue);
                    cmd.Parameters.AddWithValue("@p17", txtREngName.Text);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    string sql = "";
                    sql += "Update ResearcherInfo set RID=@p1,ReName=@p2,RaName=@p3,College=@p4,Dept=@p5,RLevel=@p6,RDegree=@p7,HDate=@p8,AcdId=@p9,Major=@p10,DegreeYear=@p11,PositionName=@p12,ExpYear=@p13,ProductivityPointer=@p14,RCitationAvg=@p15,RStatus=@p16,REngName=@p17 where RID=@p1";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@p1", txtRID.Text);
                    cmd.Parameters.AddWithValue("@p2", txtReName.Text);
                    cmd.Parameters.AddWithValue("@p3", txtRaName.Text);
                    cmd.Parameters.AddWithValue("@p4", ddlFacullty.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@p5", ddlDept.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@p6", ddlRLevel.SelectedValue != "0" ? ddlRLevel.SelectedItem.Text : "");
                    cmd.Parameters.AddWithValue("@p7", ddlRDegree.SelectedValue != "0" ? ddlRDegree.SelectedItem.Text : "");
                    cmd.Parameters.AddWithValue("@p8", Convert.ToDateTime(txtHDate.Text));
                    cmd.Parameters.AddWithValue("@p9", txtAcdId.Text);
                    cmd.Parameters.AddWithValue("@p10", txtMajor.Text);
                    cmd.Parameters.AddWithValue("@p11", ddlQualYear.SelectedValue);
                    cmd.Parameters.AddWithValue("@p12", txtPositionName.Text);
                    cmd.Parameters.AddWithValue("@p13", ddlExpYear.SelectedValue);
                    cmd.Parameters.AddWithValue("@p14", txtProductivityPointer.Text);
                    cmd.Parameters.AddWithValue("@p15", txtRCitationAvg.Text);
                    cmd.Parameters.AddWithValue("@p16", ddlRStatus.SelectedValue);
                    cmd.Parameters.AddWithValue("@p17", txtREngName.Text);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
                alertMsgSuccess.Visible = true;
                Timer1.Enabled = true;
                Label1.Text = "تم التخزين بنجاح";
            }
            catch {
                alertErr.Visible = true;
                Timer1.Enabled = true;
                Label2.Text = "حصل خطأ أثناء التخزين، أرجو التأكد من المعلومات المدخلة";
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            alertMsgSuccess.Visible = false;
            alertErr.Visible = false;
            Timer1.Enabled = false;
            Response.Redirect("NewResearcher.aspx");
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;

            txtRID.Text = row.Cells[0].Text;
            txtReName.Text = row.Cells[3].Text;
            txtRaName.Text =Server.HtmlDecode(row.Cells[1].Text);
            ddlFacullty.SelectedValue = row.Cells[4].Text == "&nbsp;" ? "0" : row.Cells[4].Text;
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand("Select DeptName From Department d,Faculty f where d.CAutoid=f.autoid and CollegeName=N'" + (row.Cells[4].Text == "&nbsp;" ? "0" : row.Cells[4].Text)+"'", conn);
            ddlDept.DataSource = cmd.ExecuteReader();
            ddlDept.DataValueField = "DeptName";
            ddlDept.DataTextField = "DeptName";
            ddlDept.DataBind();
            ddlDept.Items.Insert(0, "اختيار القسم");
            ddlDept.Items[0].Value = "0";
            conn.Close();

            ddlDept.SelectedValue = row.Cells[5].Text;
            ddlRLevel.SelectedValue = row.Cells[6].Text;
            ddlRDegree.SelectedValue = row.Cells[7].Text;
            txtHDate.Text = Convert.ToDateTime(row.Cells[8].Text).ToString("dd-MM-yyyy");
            txtAcdId.Text = row.Cells[9].Text;
            txtMajor.Text = row.Cells[10].Text;
            ddlQualYear.SelectedValue = row.Cells[11].Text == "&nbsp;" ? "0" : row.Cells[11].Text;
            txtPositionName.Text = Server.HtmlDecode(row.Cells[12].Text);
            ddlExpYear.SelectedValue = row.Cells[13].Text == "&nbsp;" ? "0" : row.Cells[13].Text;
            txtProductivityPointer.Text = row.Cells[14].Text;
            txtRCitationAvg.Text = row.Cells[15].Text;
            ddlRStatus.SelectedValue = row.Cells[16].Text;
            txtREngName.Text = row.Cells[2].Text;


        }

        protected void lnkDelR_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand("delete from ResearcherInfo where RID=N'" + row.Cells[0].Text +"'", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            Label1.Text = "تم الحذف بنجاح";
            alertMsgSuccess.Visible = true;
            Timer1.Enabled = true;
        }
    }
}