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
    public partial class Admin_FormInfo : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

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

                SqlCommand cmd = new SqlCommand("Select isnull(max(FormId),1)+1 From FormsInfo", conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                lblFormId.Text = dr[0].ToString();


                cmd = new SqlCommand("select * from FormsInfo", conn);
                grdFormInfo.DataSource = cmd.ExecuteReader();
                grdFormInfo.DataBind();

                conn.Close();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand cmd=new SqlCommand();
            if (Session["update"] == null)
            {
                cmd = new SqlCommand("insert into FormsInfo values(@formid,@type,@aname,@ename,@opend,@closed,@instruction,@dir,'','')", conn);
                cmd.Parameters.AddWithValue("@formid", lblFormId.Text);
                cmd.Parameters.AddWithValue("@type", ddlFormType.SelectedValue);
                cmd.Parameters.AddWithValue("@aname", txtAName.Text);
                cmd.Parameters.AddWithValue("@ename", txtEName.Text);
                cmd.Parameters.AddWithValue("@opend", txtOpenDate.Value);
                cmd.Parameters.AddWithValue("@closed", txtCloseDate.Value);
                cmd.Parameters.AddWithValue("@instruction", txtInstruction.Text);
                cmd.Parameters.AddWithValue("@dir", chkDirector.Checked ? 1 : 0);
                cmd.ExecuteNonQuery();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('تم الحفظ','success','" + Session["popUpImg"] + "');", true);
            }
            else
            {
                cmd = new SqlCommand("update FormsInfo set Type=@type,FormAName=@aname,FormEName=@ename,OpenFrom=@opend,ClosedAt=@closed,FormInstruction=@instruction,FromHeadEnter=@dir where FormId=@formid", conn);
                cmd.Parameters.AddWithValue("@formid", lblFormId.Text);
                cmd.Parameters.AddWithValue("@type", ddlFormType.SelectedValue);
                cmd.Parameters.AddWithValue("@aname", txtAName.Text);
                cmd.Parameters.AddWithValue("@ename", txtEName.Text);
                cmd.Parameters.AddWithValue("@opend", txtOpenDate.Value);
                cmd.Parameters.AddWithValue("@closed", txtCloseDate.Value);
                cmd.Parameters.AddWithValue("@instruction", txtInstruction.Text);
                cmd.Parameters.AddWithValue("@dir", chkDirector.Checked ? 1 : 0);
                cmd.ExecuteNonQuery();
                Session["update"] = null;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('تم التعديل','success','" + Session["popUpImg"] + "');", true);
            }
            cmd = new SqlCommand("select * from FormsInfo", conn);
            grdFormInfo.DataSource = cmd.ExecuteReader();
            grdFormInfo.DataBind();
            Timer1.Enabled = true;
            conn.Close();

        }

        protected void lnkViewInst_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            txtInstructionView.Text = row.Cells[7].Text;
            //viewInstDiv.InnerHtml= row.Cells[7].Text;
            instructionDiv.Visible = true;
            currentDiv.Attributes.Add("class", "col-sm-12 col-md-8 mb-2");
        }

        protected void grdFormInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType==DataControlRowType.DataRow)
            {
                switch(e.Row.Cells[1].Text)
                {
                    case "0":
                        e.Row.Cells[1].Text = "البحث العلمي";
                        break;
                    case "1":
                        e.Row.Cells[1].Text = "الدراسات العليا";
                        break;
                }
                switch (e.Row.Cells[6].Text)
                {
                    case "0":
                        e.Row.Cells[6].Text = "طالب";
                        break;
                    case "1":
                        e.Row.Cells[6].Text = "رئيس قسم";
                        break;
                }
            }
        }

        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand("select * from FormsInfo where FormId="+row.Cells[0].Text, conn);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            if(dt.Rows.Count!=0)
            {
                lblFormId.Text=dt.Rows[0]["FormId"].ToString();
                ddlFormType.SelectedValue = dt.Rows[0]["Type"].ToString();
                txtAName.Text = dt.Rows[0]["FormAName"].ToString();
                txtEName.Text = dt.Rows[0]["FormEName"].ToString();
                string d=Convert.ToDateTime(dt.Rows[0]["OpenFrom"]).ToString("yyyy-MM-dd");
                txtOpenDate.Value = d;
                d = Convert.ToDateTime(dt.Rows[0]["ClosedAt"]).ToString("yyyy-MM-dd");
                txtCloseDate.Value = d;
                txtInstruction.Text = dt.Rows[0]["FormInstruction"].ToString();
                chkDirector.Checked = (dt.Rows[0]["FromHeadEnter"].ToString() == "0" ? false : true);
                Session["update"] = "update";
            }

        }

        protected void imgClose_Click(object sender, ImageClickEventArgs e)
        {
            instructionDiv.Visible = false;
            currentDiv.Attributes.Add("class", "col-sm-12 mb-2");
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("Admin_FormInfo.aspx");
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Response.Redirect("Admin_FormInfo.aspx");
        }
    }
}