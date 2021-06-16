using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ResearchAcademicUnit
{
    public partial class UniversityInfo : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["logSession"] == null || Session["userid"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            Session["backurl"] = "Index.aspx";
            Label lblUserVal = (Label)Page.Master.FindControl("lblPageName");
            lblUserVal.Text = "تقرير الأداء البحثي للجامعة";
            Label lblinfo = (Label)Page.Master.FindControl("lblinfo");
            lblinfo.Text = "تقرير الأداء البحثي للجامعة";

            Label lblUser = (Label)Page.Master.FindControl("lblUserName");
            lblUser.Text = "تاريخ التقرير : " + DateTime.Now.Date.ToString("dd-MM-yyyy");

            if (!IsPostBack)
            {
                getData();
            }

        }

        protected void getData()
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();
                string sql = "";
            sql = "select *, ROW_NUMBER() OVER(ORDER BY ReYear DESC, ReMonth DESC,MClassInt ASC) AS row_num" +
                  " FROM " +
                  " (SELECT distinct RE.ReId, ReTitle, ReType, ReLevel, ReYear,ReMonth, ReCitation, ReParticipate, Magazine," +
                  " SourceType, MClass, CitationAvg, InSupport, Reward, outSupport,ReStatus,MClassInt,TopMag " +
                  " FROM ResearchsInfo RE) final";
                  //" where ReId in (select ReId from Research_Researcher rr where rr.RId in (select ri.RId from ResearcherInfo ri where College = N'" + ddlResearcher.SelectedValue + "'))) final";
            SqlCommand cmd = new SqlCommand(sql, conn);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            if (dt.Rows.Count != 0)
            {
                Session["data"] = dt;

                GridView1.DataSource = dt;
                GridView1.DataBind();
                msgDiv.Visible = false;
                //infoDiv.Visible = true;
            }
            else
            {
                msgDiv.Visible = true;
                //infoDiv.Visible = false;
                lblMsg.Text = "لا يوجد معلومات حاليا";
                //lblRNo.Text = "";
                //lblRDegree.Text = "";
                //lblRHireDate.Text = "";
                //lblRCollege.Text = "";
                //lblRDept.Text = "";
                //Label4.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");

            }

        }

        //protected void ddlResearcher_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlResearcher.SelectedValue != "0")
        //    {
        //        if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
        //            conn.Open();
        //        //try
        //        //{
        //        //string sql= "select *, ROW_NUMBER() OVER (ORDER BY RI.RID) AS row_num FROM " +
        //        //   //(SELECT DISTINCT id FROM table WHERE fid = 64) Base"
        //        //" (SELECT distinct RI.RID, ReTitle,ReType,ReLevel,ReYear,ReCitation,ReParticipate,Magazine,SourceType,MClass,CitationAvg,InSupport,Reward,outSupport " +
        //        //    " FROM [ResearcherInfo] RI, ResearchsInfo RE, Research_Researcher RR " +
        //        //    " where Ri.RId = RR.RId and re.ReId = rr.ReId and RI.College =N'" + ddlResearcher.SelectedValue + "') RI";
        //        string sql = "";
        //        sql = "select *, ROW_NUMBER() OVER(ORDER BY ReID) AS row_num" +
        //              " FROM " +
        //              " (SELECT distinct ReId, ReTitle, ReType, ReLevel, ReYear, ReCitation, ReParticipate, Magazine," +
        //              " SourceType, MClass, CitationAvg, InSupport, Reward, outSupport " +
        //              " FROM ResearchsInfo RE " +
        //              " where ReId in (select ReId from Research_Researcher rr where rr.RId in (select ri.RId from ResearcherInfo ri where College = N'"+ddlResearcher.SelectedValue+"'))) final";
        //          SqlCommand cmd = new SqlCommand(sql, conn);
        //        DataTable dt = new DataTable();
        //        dt.Load(cmd.ExecuteReader());
        //        if (dt.Rows.Count != 0)
        //        {
        //            Session["data"] = dt;
        //            //lblMsg.Text = "";
        //            //lblRName.Text = dt.Rows[0]["REName"].ToString();
        //            //lblRNo.Text = dt.Rows[0]["Rid"].ToString();
        //            //lblRDegree.Text = dt.Rows[0]["Relevel"].ToString();
        //            //lblRHireDate.Text = (dt.Rows[0]["HDate"].ToString() != "" ? DateTime.Parse(dt.Rows[0]["HDate"].ToString()).ToString("dd-MM-yyyy") : "");
        //            //lblRCollege.Text = dt.Rows[0]["College"].ToString();
        //            //lblRDept.Text = dt.Rows[0]["Dept"].ToString();
        //            //Label4.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");

        //            GridView1.DataSource = dt;
        //            GridView1.DataBind();
        //            msgDiv.Visible = false;
        //            infoDiv.Visible = true;
        //        }
        //        else
        //        {
        //            msgDiv.Visible = true;
        //            infoDiv.Visible = false;
        //            lblMsg.Text = "لا يوجد معلومات حاليا";
        //            lblRNo.Text = "";
        //            lblRDegree.Text = "";
        //            lblRHireDate.Text = "";
        //            lblRCollege.Text = "";
        //            lblRDept.Text = "";
        //            Label4.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");

        //        }

        //        cmd = new SqlCommand("Select distinct Dept From ResearcherInfo where College=N'" + ddlResearcher.SelectedValue + "'", conn);
        //        ddlDept.DataSource = cmd.ExecuteReader();
        //        ddlDept.DataValueField = "Dept";
        //        ddlDept.DataTextField = "Dept";
        //        ddlDept.DataBind();
        //        ddlDept.Items.Insert(0, "اختيار القسم");
        //        ddlDept.Items[0].Value = "0";

        //        conn.Close();
        //    }
        //    else
        //    {
        //        msgDiv.Visible = true;
        //        infoDiv.Visible = false;
        //        lblMsg.Text = "الرجاء تحديد الكلية";
        //        lblRNo.Text = "";
        //        lblRDegree.Text = "";
        //        lblRHireDate.Text = "";
        //        lblRCollege.Text = "";
        //        lblRDept.Text = "";
        //        ddlDept.Items.Clear();
        //        Label4.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");

        //    }
        //}

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("IndexUni.aspx");
        }

        //protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlDept.SelectedValue != "0")
        //    {
        //        if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
        //            conn.Open();
        //        //try
        //        //{
        //        string sql = "";
        //        sql = "select *, ROW_NUMBER() OVER(ORDER BY ReID) AS row_num" +
        //              " FROM " +
        //              " (SELECT distinct ReId, ReTitle, ReType, ReLevel, ReYear, ReCitation, ReParticipate, Magazine," +
        //              " SourceType, MClass, CitationAvg, InSupport, Reward, outSupport " +
        //              " FROM ResearchsInfo RE " +
        //              " where ReId in (select ReId from Research_Researcher rr where rr.RId in (select ri.RId from ResearcherInfo ri where College = N'" + ddlResearcher.SelectedValue + "' and Dept=N'"+ddlDept.SelectedValue+"'))) final";
        //        SqlCommand cmd = new SqlCommand(sql, conn);
        //        DataTable dt = new DataTable();
        //        dt.Load(cmd.ExecuteReader());
        //        if (dt.Rows.Count != 0)
        //        {
        //            Session["data"] = dt;
        //            //lblMsg.Text = "";
        //            //lblRName.Text = dt.Rows[0]["REName"].ToString();
        //            //lblRNo.Text = dt.Rows[0]["Rid"].ToString();
        //            //lblRDegree.Text = dt.Rows[0]["Rlevel"].ToString();
        //            //lblRHireDate.Text = (dt.Rows[0]["HDate"].ToString() != "" ? DateTime.Parse(dt.Rows[0]["HDate"].ToString()).ToString("dd-MM-yyyy") : "");
        //            //lblRCollege.Text = dt.Rows[0]["College"].ToString();
        //            //lblRDept.Text = dt.Rows[0]["Dept"].ToString();
        //            Label4.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");

        //            GridView1.DataSource = dt;
        //            GridView1.DataBind();
        //            msgDiv.Visible = false;
        //            infoDiv.Visible = true;
        //        }
        //        else
        //        {
        //            msgDiv.Visible = true;
        //            infoDiv.Visible = false;
        //            lblMsg.Text = "لا يوجد معلومات حاليا";
        //            lblRNo.Text = "";
        //            lblRDegree.Text = "";
        //            lblRHireDate.Text = "";
        //            lblRCollege.Text = "";
        //            lblRDept.Text = "";
        //            Label4.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");

        //        }
        //        conn.Close();
        //    }
        //    else
        //    {
        //        if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
        //            conn.Open();
        //        //try
        //        //{
        //        string sql = "";
        //        sql = "select *, ROW_NUMBER() OVER(ORDER BY ReID) AS row_num" +
        //              " FROM " +
        //              " (SELECT distinct ReId, ReTitle, ReType, ReLevel, ReYear, ReCitation, ReParticipate, Magazine," +
        //              " SourceType, MClass, CitationAvg, InSupport, Reward, outSupport " +
        //              " FROM ResearchsInfo RE " +
        //              " where ReId in (select ReId from Research_Researcher rr where rr.RId in (select ri.RId from ResearcherInfo ri where College = N'" + ddlResearcher.SelectedValue + "'))) final";
        //        SqlCommand cmd = new SqlCommand(sql, conn);
        //        DataTable dt = new DataTable();
        //        dt.Load(cmd.ExecuteReader());
        //        if (dt.Rows.Count != 0)
        //        {
        //            Session["data"] = dt;
        //            //lblMsg.Text = "";
        //            //lblRName.Text = dt.Rows[0]["REName"].ToString();
        //            //lblRNo.Text = dt.Rows[0]["Rid"].ToString();
        //            //lblRDegree.Text = dt.Rows[0]["Rlevel"].ToString();
        //            //lblRHireDate.Text = (dt.Rows[0]["HDate"].ToString() != "" ? DateTime.Parse(dt.Rows[0]["HDate"].ToString()).ToString("dd-MM-yyyy") : "");
        //            //lblRCollege.Text = dt.Rows[0]["College"].ToString();
        //            //lblRDept.Text = dt.Rows[0]["Dept"].ToString();
        //            Label4.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");

        //            GridView1.DataSource = dt;
        //            GridView1.DataBind();
        //            msgDiv.Visible = false;
        //            infoDiv.Visible = true;
        //        }

        //    }
        //}

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();
                string sql = "";
                sql = "Select distinct REName,College,Dept,RAName From ResearcherInfo ri, Research_Researcher rr where ri.rid=rr.rid ";
                sql += " and rr.reid='" + e.Row.Cells[15].Text + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                HtmlGenericControl Rdiv = e.Row.FindControl("ReInfoDiv") as HtmlGenericControl;
                HtmlGenericControl Cdiv = e.Row.FindControl("CollegeDiv") as HtmlGenericControl;
                HtmlGenericControl Ddiv = e.Row.FindControl("DeptDiv") as HtmlGenericControl;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string aname = dt.Rows[i][3].ToString();
                    string ename = dt.Rows[i][0].ToString();
                    Rdiv.InnerText += (aname == "" ? ename : aname) + " - ";// dt.Rows[i][0].ToString()
                    Cdiv.InnerText = dt.Rows[i][1].ToString();
                    Ddiv.InnerText = dt.Rows[i][2].ToString();
                }
                Rdiv.InnerText = Rdiv.InnerText.Substring(0, Rdiv.InnerText.Length - 3);
                //conn.Close();
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.DataSource = (DataTable)Session["data"];
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session["logSession"] = "logout";
            Session["userid"] = null;
            Session["userrole"] = null;
            Response.Redirect("IndexUni.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            string sql = "";
            sql = "select  *, ROW_NUMBER() OVER(ORDER BY ReYear DESC, ReMonth DESC,MClassInt ASC) AS row_num" +
                  " FROM " +
                  " (SELECT distinct RE.ReId, ReTitle, ReType, ReLevel, ReYear,ReMonth, ReCitation, ReParticipate, Magazine," +
                  " SourceType, MClass, CitationAvg, InSupport, Reward, outSupport,ReStatus,MClassInt,TopMag " +
                  " FROM ResearchsInfo RE) final";
            //" where ReId in (select ReId from Research_Researcher rr where rr.RId in (select ri.RId from ResearcherInfo ri where College = N'" + ddlResearcher.SelectedValue + "'))) final";
            SqlCommand cmd = new SqlCommand(sql, conn);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());

            conn.Close();
            if (GridView1.AllowPaging)
            {
                GridView1.AllowPaging = false;
                GridView1.DataSource = dt;// Session["data"];
                GridView1.DataBind();
            }
            else
            {
                GridView1.AllowPaging = true;
                GridView1.PageSize = 20;
                GridView1.DataBind();
            }

            GridView1.UseAccessibleHeader = true;
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            GridView1.FooterRow.TableSection = TableRowSection.TableFooter;
            GridView1.Attributes["style"] = "direction:rtl";
            foreach (GridViewRow row in GridView1.Rows)
            {
                //if (row.RowIndex % 30 == 0 && row.RowIndex != 0)
                //{
                //    row.Attributes["style"] = "page-break-after:always;";
                    
                //}
                row.Attributes["style"] = "color:black;text-align:center";
                //row.Attributes.Add("","");
            }
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.RenderControl(hw);
            string gridHTML ="<div style = 'float: right; width: 25%; text-align: center; padding-top: 10px' >جامعة الشرق الأوسط<br>MIDDLE EAST UNIVERSITY<br>AMMAN</div>" +
                        "<div style='float: right; width: 49%; text-align: center; padding-top: 10px;padding-bottom:10px'>" +
                        "    تقرير الاستعلام البحثي" +
                        "<br />" +
                        DateTime.Now.ToString("dd-MM-yyyy") +
                        "</div>" +
                        "<div style = 'float: left; width: 25%; text-align: center; padding-top: 10px;padding-bottom:10px; font-size: 100%' >" +
                        "    عمادة الدراسات العليا والبحث العلمي<br />" +
                        "    قسم البحث العلمي" +"<br>أبحاث الجامعة"+
                        
                        "</div>" +
                        "<div style = 'clear: both' ></ div >";

            gridHTML += sw.ToString().Replace("\"", "'").Replace(System.Environment.NewLine, "");
            gridHTML+= "<p style='direction:rtl'>محددات التقرير: هذا التقرير خاص بالنتاج البحثي المنجز تحت مظلة الجامعة ومفهرس في قاعدة بيانات سكوبس</p>";
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload = new function(){");
            sb.Append("var printWin = window.open('', '', 'left=0");
            sb.Append(",top=0,width=1000,height=600,status=0');");
            sb.Append("printWin.document.write(\"");
            string style = "<style type = 'text/css'>  thead {display:table-header-group;} tfoot{display:table-footer-group;}</style>";
            sb.Append(style + gridHTML);
            sb.Append("\");");
            sb.Append("printWin.document.close();");
            sb.Append("printWin.focus();");
            sb.Append("printWin.print();");
            sb.Append("printWin.close();");
            sb.Append("};");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
            GridView1.AllowPaging = true;
            GridView1.DataBind();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /*Verifies that the control is rendered */
        }
    }
}