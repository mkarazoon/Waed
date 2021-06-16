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
    public partial class Admin_WorkFlow : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);
        DataTable dtWorkFlow = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userid"] == null || Session.IsNewSession)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("select * from FormsInfo", conn);
                ddlFormsInfo.DataSource = cmd.ExecuteReader();
                ddlFormsInfo.DataTextField = "FormAName";
                ddlFormsInfo.DataValueField = "FormId";
                ddlFormsInfo.DataBind();
                ddlFormsInfo.Items.Insert(0, "اختيار اسم النموذج");
                ddlFormsInfo.Items[0].Value = "";

                cmd = new SqlCommand("select * from Com_JobTitle", conn);
                ddlJobtitle.DataSource = cmd.ExecuteReader();
                ddlJobtitle.DataTextField = "JobTitleA";
                ddlJobtitle.DataValueField = "AutoId";
                ddlJobtitle.DataBind();
                ddlJobtitle.Items.Insert(0, "اختيار");
                ddlJobtitle.Items[0].Value = "";

                dtWorkFlow.Columns.Add("FormId");
                dtWorkFlow.Columns.Add("StepNo");
                dtWorkFlow.Columns.Add("PrivId");
                dtWorkFlow.Columns.Add("PrivName");
                Session["dtWorkFlow"] = dtWorkFlow;


                conn.Close();
            }

        }

        protected void btnAddStep_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
                SqlCommand cmd = new SqlCommand("Select * From Com_Priviliges where PrivId=" + ddlJobtitle.SelectedValue, conn);
                if (cmd.ExecuteReader().HasRows || ddlJobtitle.SelectedValue=="3")
                {
                    dtWorkFlow = (DataTable)Session["dtWorkFlow"];
                    var r = dtWorkFlow.Select("PrivId=" + ddlJobtitle.SelectedValue);
                    if (r.Length == 0)
                    {
                        DataRow row = dtWorkFlow.NewRow();
                        row[0] = ddlFormsInfo.SelectedValue;
                        row[1] = dtWorkFlow.Rows.Count + 1;
                        row[2] = ddlJobtitle.SelectedValue;
                        row[3] = ddlJobtitle.SelectedItem.Text;
                        dtWorkFlow.Rows.Add(row);
                        Session["dtWorkFlow"] = dtWorkFlow;
                        GridView1.DataSource = dtWorkFlow;
                        GridView1.DataBind();
                    }
                    else
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('تم إضافة هذا المسمى سابقا','error','" + Session["popUpImg"] + "');", true);
                    ddlJobtitle.SelectedValue = "";

                    if (GridView1.Rows.Count != 0)
                        btnSave.Visible = true;
                    else
                        btnSave.Visible = false;
                }
                else
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('لم يتم ربط اي شخص بهذا المسمى الوظيفي','error','" + Session["popUpImg"] + "');", true);

        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            dtWorkFlow = (DataTable)Session["dtWorkFlow"];
            dtWorkFlow.Rows.RemoveAt(row.RowIndex);
            for (int i = 0; i < dtWorkFlow.Rows.Count; i++)
                dtWorkFlow.Rows[i][1] = (i + 1).ToString();
            GridView1.DataSource = dtWorkFlow;
            GridView1.DataBind();

            if (GridView1.Rows.Count != 0)
                btnSave.Visible = true;
            else
                btnSave.Visible = false;

        }

        protected void lnkDown_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            dtWorkFlow = (DataTable)Session["dtWorkFlow"];
            DataRow firstSelectedRow = dtWorkFlow.Rows[row.RowIndex];
            DataRow firstNewRow = dtWorkFlow.NewRow();
            firstNewRow.ItemArray = firstSelectedRow.ItemArray; // copy data
            dtWorkFlow.Rows.Remove(firstSelectedRow);
            dtWorkFlow.Rows.InsertAt(firstNewRow, row.RowIndex + 1);
            for (int i = 0; i < dtWorkFlow.Rows.Count; i++)
                dtWorkFlow.Rows[i][1] = (i + 1).ToString();
            GridView1.DataSource = dtWorkFlow;
            GridView1.DataBind();
        }

        protected void lnkUp_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            dtWorkFlow = (DataTable)Session["dtWorkFlow"];
            DataRow firstSelectedRow = dtWorkFlow.Rows[row.RowIndex];
            DataRow firstNewRow = dtWorkFlow.NewRow();
            firstNewRow.ItemArray = firstSelectedRow.ItemArray; // copy data
            dtWorkFlow.Rows.Remove(firstSelectedRow);
            dtWorkFlow.Rows.InsertAt(firstNewRow, row.RowIndex - 1);
            for (int i = 0; i < dtWorkFlow.Rows.Count; i++)
                dtWorkFlow.Rows[i][1] = (i + 1).ToString();
            GridView1.DataSource = dtWorkFlow;
            GridView1.DataBind();


        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            if (GridView1.Rows.Count == 1)
            {
                ((LinkButton)GridView1.Rows[0].FindControl("lnkUp")).Visible = false;
                ((LinkButton)GridView1.Rows[0].FindControl("lnkDown")).Visible = false;

            }
            else if (GridView1.Rows.Count == 2)
            {
                ((LinkButton)GridView1.Rows[0].FindControl("lnkUp")).Visible = false;
                ((LinkButton)GridView1.Rows[1].FindControl("lnkDown")).Visible = false;

            }
            else
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    if (i == 0)
                        ((LinkButton)GridView1.Rows[i].FindControl("lnkUp")).Visible = false;
                    else if ((i + 1) == GridView1.Rows.Count)
                        ((LinkButton)GridView1.Rows[i].FindControl("lnkDown")).Visible = false;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (GridView1.Rows.Count != 0)
            {
                if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
                    conn.Open();

                SqlCommand cmdCheck = new SqlCommand("Select * From RequestsFollowUp where type=" + ddlFormsInfo.SelectedValue, conn);
                if (!cmdCheck.ExecuteReader().HasRows)
                {
                    SqlCommand cmd = new SqlCommand("delete from WorkFlow where FormId=" + ddlFormsInfo.SelectedValue, conn);
                    cmd.ExecuteNonQuery();

                    SqlBulkCopy data = new SqlBulkCopy(conn);

                    dtWorkFlow = (DataTable)Session["dtWorkFlow"];
                    data.ColumnMappings.Add("FormId", "FormId");
                    data.ColumnMappings.Add("StepNo", "StepNo");
                    data.ColumnMappings.Add("PrivId", "PrivId");
                    data.DestinationTableName = "WorkFlow";
                    data.WriteToServer(dtWorkFlow);
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('تم الحفظ بنجاح','success');", true);
                    conn.Close();
                }
                else
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('يوجد معاملات مرتبطة بهذا النموذج لا يمكن التعديل على مسار العمل','error');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('يجب أن يتم ادخال معلومات مسار النموذج','error');", true);
            }
        }

        protected void ddlFormsInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
                conn.Open();

            SqlCommand cmd = new SqlCommand(@"SELECT a.AutoId
        ,[StepNo]
      ,[PrivId], b.JobTitleA PrivName
  FROM WorkFlow a, Com_JobTitle b
  where a.PrivId = b.AutoId and FormId =" + ddlFormsInfo.SelectedValue+"order by FormId, StepNo", conn);
            dtWorkFlow = (DataTable)Session["dtWorkFlow"];
            dtWorkFlow.Load(cmd.ExecuteReader());
            Session["dtWorkFlow"] = dtWorkFlow;
            GridView1.DataSource = dtWorkFlow;// cmd.ExecuteReader();
            GridView1.DataBind();


            if (GridView1.Rows.Count != 0)
                btnSave.Visible = true;
            else
                btnSave.Visible = false;
            conn.Close();
        }
    }
}