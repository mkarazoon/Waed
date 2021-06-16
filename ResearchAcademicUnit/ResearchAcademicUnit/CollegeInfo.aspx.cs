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
    public partial class CollegeInfo : System.Web.UI.Page
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
            lblUserVal.Text = "تقرير الأداء البحثي للكلية";

            Label lblUser = (Label)Page.Master.FindControl("lblUserName");
            lblUser.Text ="أهلا بكم في كلية " + Session["userCollege"].ToString();

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
                else if(Session["userrole"].ToString()=="7")
                {
                    ddlResearcher.Visible = false;
                    ddlDept.Visible = false;
                    lblRCollege.Text = Session["userCollege"].ToString();
                    collegediv.Visible = false;
                    getinfoByDept(Session["userDept"].ToString());
                }
                //if (Session["userrole"].ToString() == "5")
                //{
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd1 = new SqlCommand("Select distinct College From ResearcherInfo" + userRole, conn);
                    ddlResearcher.DataSource = cmd1.ExecuteReader();
                    ddlResearcher.DataTextField = "College";
                    ddlResearcher.DataValueField = "College";
                    ddlResearcher.DataBind();
                    ddlResearcher.Items.Insert(0, "اختيار الكلية");
                    ddlResearcher.Items[0].Value = "0";
                //}
                    Label4.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                conn.Close();
            }

        }

        protected void ddlResearcher_SelectedIndexChanged(object sender, EventArgs e)
        {
            getinfoByCollege();
        }

        protected void getinfoByCollege()
        {
            if (ddlResearcher.SelectedValue != "0")
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();
                //try
                //{
                lblRCollege.Text = ddlResearcher.SelectedItem.Text;
                string sql = "";

                string order = (ddlSortBy.SelectedValue == "0" ? "" :
                      " order by " + (ddlSortBy.SelectedValue == "ReYear" ? "ReYear " + ddlSortOrder.SelectedValue + ", ReMonth " + ddlSortOrder.SelectedValue : ddlSortBy.SelectedValue + " " + ddlSortOrder.SelectedValue));


                sql = "select *, ROW_NUMBER() OVER(" + order + ") AS row_num" +
                      " FROM " +
                      " (SELECT distinct ReId, ReTitle, ReType, ReLevel, ReYear, ReCitation, ReParticipate, Magazine," +
                      " SourceType, MClass, CitationAvg, InSupport, Reward, outSupport,ReMonth,ReStatus,MClassInt " +
                      " FROM ResearchsInfo RE " +
                      " where ReId in (select ReId from Research_Researcher rr where rr.RId in (select ri.RId from ResearcherInfo ri where College = N'" + ddlResearcher.SelectedValue + "'))) final";
                      //(ddlSortBy.SelectedValue == "0" ? "" :
                      //" order by " + (ddlSortBy.SelectedValue == "ReYear" ? "ReYear " + ddlSortOrder.SelectedValue + ", ReMonth " + ddlSortOrder.SelectedValue : ddlSortBy.SelectedValue + " " + ddlSortOrder.SelectedValue));

                  SqlCommand cmd = new SqlCommand(sql, conn);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                lblRDept.Text = "جميع الأقسام";
                Label lblinfo = (Label)Page.Master.FindControl("lblinfo");
                lblinfo.Text = "البطاقة البحثية كلية " + lblRCollege.Text + " - " + lblRDept.Text;

                if (dt.Rows.Count != 0)
                {
                    Session["data"] = dt;

                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    msgDiv.Visible = false;
                    infoDiv.Visible = true;
                    Button1.Visible = true;
                }
                else
                {
                    msgDiv.Visible = true;
                    infoDiv.Visible = false;
                    lblMsg.Text = "لا يوجد معلومات حاليا";
                    Button1.Visible = false;
                    Label4.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");

                }

                cmd = new SqlCommand("Select distinct Dept From ResearcherInfo where College=N'" + ddlResearcher.SelectedValue + "'", conn);
                ddlDept.DataSource = cmd.ExecuteReader();
                ddlDept.DataValueField = "Dept";
                ddlDept.DataTextField = "Dept";
                ddlDept.DataBind();
                ddlDept.Items.Insert(0, "اختيار القسم");
                ddlDept.Items[0].Value = "0";

                conn.Close();
            }
            else
            {
                msgDiv.Visible = true;
                infoDiv.Visible = false;
                lblMsg.Text = "الرجاء تحديد الكلية";
                ddlDept.Items.Clear();
                Label4.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
            }

        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("IndexCollege.aspx");
        }

        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            getinfoByDept(ddlDept.SelectedValue);
        }

        protected void getinfoByDept(string dept)
        {
            //if (ddlDept.SelectedValue != "0")
            if (dept != "0")
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();
                string sql = "";

                string order = (ddlSortBy.SelectedValue == "0" ? "" :
                      " order by " + (ddlSortBy.SelectedValue == "ReYear" ? "ReYear " + ddlSortOrder.SelectedValue + ", ReMonth " + ddlSortOrder.SelectedValue : ddlSortBy.SelectedValue + " " + ddlSortOrder.SelectedValue));


                sql = "select *, ROW_NUMBER() OVER(" + order + ") AS row_num" +
                      " FROM " +
                      " (SELECT distinct ReId, ReTitle, ReType, ReLevel, ReYear, ReCitation, ReParticipate, Magazine," +
                      " SourceType, MClass, CitationAvg, InSupport, Reward, outSupport,ReMonth,ReStatus,MClassInt " +
                      " FROM ResearchsInfo RE " +
                      " where ReId in (select ReId from Research_Researcher rr where rr.RId in (select ri.RId from ResearcherInfo ri where College = N'" + (ddlResearcher.Visible ? ddlResearcher.SelectedValue : Session["userCollege"]) + "' and Dept=N'" + dept + "'))) final";
                      //(ddlSortBy.SelectedValue == "0" ? "" :
                      //" order by " + (ddlSortBy.SelectedValue == "ReYear" ? "ReYear " + ddlSortOrder.SelectedValue + ", ReMonth " + ddlSortOrder.SelectedValue : ddlSortBy.SelectedValue + " " + ddlSortOrder.SelectedValue));

                SqlCommand cmd = new SqlCommand(sql, conn);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                lblRDept.Text = dept;
                Label lblinfo = (Label)Page.Master.FindControl("lblinfo");
                lblinfo.Text = "البطاقة البحثية كلية " + lblRCollege.Text + " - " + lblRDept.Text;

                if (dt.Rows.Count != 0)
                {
                    Session["data"] = dt;
                    Label4.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    msgDiv.Visible = false;
                    infoDiv.Visible = true;
                    Button1.Visible = true;
                }
                else
                {
                    msgDiv.Visible = true;
                    infoDiv.Visible = false;
                    lblMsg.Text = "لا يوجد معلومات حاليا";
                    Button1.Visible = false;
                    Label4.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");

                }
                conn.Close();
            }
            else
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();
                //try
                //{
                lblRDept.Text = "";
                string sql = "";
                string order = (ddlSortBy.SelectedValue == "0" ? "" :
                " order by " + (ddlSortBy.SelectedValue == "ReYear" ? "ReYear " + ddlSortOrder.SelectedValue + ", ReMonth " + ddlSortOrder.SelectedValue : ddlSortBy.SelectedValue + " " + ddlSortOrder.SelectedValue));

                sql = "select *, ROW_NUMBER() OVER(" + order + ") AS row_num" +
                      " FROM " +
                      " (SELECT distinct ReId, ReTitle, ReType, ReLevel, ReYear, ReCitation, ReParticipate, Magazine," +
                      " SourceType, MClass, CitationAvg, InSupport, Reward, outSupport,ReMonth,ReStatus,MClassInt " +
                      " FROM ResearchsInfo RE " +
                      " where ReId in (select ReId from Research_Researcher rr where rr.RId in (select ri.RId from ResearcherInfo ri where College = N'" + ddlResearcher.SelectedValue + "'))) final";
                      //(ddlSortBy.SelectedValue == "0" ? "" :
                      //" order by " + (ddlSortBy.SelectedValue == "ReYear" ? "ReYear " + ddlSortOrder.SelectedValue + ", ReMonth " + ddlSortOrder.SelectedValue : ddlSortBy.SelectedValue + " " + ddlSortOrder.SelectedValue));

                SqlCommand cmd = new SqlCommand(sql, conn);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                lblRDept.Text = "جميع الأقسام";
                if (dt.Rows.Count != 0)
                {
                    Session["data"] = dt;
                    Label4.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");

                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    msgDiv.Visible = false;
                    infoDiv.Visible = true;
                }
                else
                {
                    msgDiv.Visible = true;
                    infoDiv.Visible = false;

                }
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType==DataControlRowType.DataRow)
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();
                string sql = "";
                sql = "Select distinct REName,RAName From ResearcherInfo ri, Research_Researcher rr where ri.rid=rr.rid ";
                sql += " and rr.reid='"+e.Row.Cells[13].Text+ "' and ri.rid in( select ri.RId from ResearcherInfo ri where College = N'" + ddlResearcher.SelectedValue + "')";
                SqlCommand cmd = new SqlCommand(sql, conn);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                HtmlGenericControl div = e.Row.FindControl("ReInfoDiv") as HtmlGenericControl;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                        string aname = dt.Rows[i][1].ToString();
                        string ename = dt.Rows[i][0].ToString();
                        div.InnerText += (aname == "" ? ename : aname) + " - ";// dt.Rows[i][0].ToString()
                    

                    //e.Row.Cells[0].Text = ((e.Row.RowIndex + 1) + (GridView1.PageIndex==0?0:10* GridView1.PageIndex)).ToString();
                    //div.InnerText += dt.Rows[i][0].ToString() +" - ";
                }
                div.InnerText = div.InnerText.Substring(0, div.InnerText.Length - 3);
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dtt= (DataTable)Session["data"];
            GridView1.DataSource = (DataTable)Session["data"];
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            string sql = "";
            string order = (ddlSortBy.SelectedValue == "0" ? "" :
      " order by " + (ddlSortBy.SelectedValue == "ReYear" ? "ReYear " + ddlSortOrder.SelectedValue + ", ReMonth " + ddlSortOrder.SelectedValue : ddlSortBy.SelectedValue + " " + ddlSortOrder.SelectedValue));

            sql = "select *, ROW_NUMBER() OVER(" + order + ") AS row_num" +
                  " FROM " +
                  " (SELECT distinct ReId, ReTitle, ReType, ReLevel, ReYear, ReCitation, ReParticipate, Magazine," +
                  " SourceType, MClass, CitationAvg, InSupport, Reward, outSupport,ReMonth,ReStatus,MClassInt " +
                  " FROM ResearchsInfo RE " +
                  " where ReId in (select ReId from Research_Researcher rr where rr.RId in (select ri.RId from ResearcherInfo ri where College = N'" + (ddlResearcher.Visible ? ddlResearcher.SelectedValue : Session["userCollege"]) + (ddlDept.SelectedValue == "0" ? "" : "' and Dept=N'" + ddlDept.SelectedValue) + "'))) final";
                  //(ddlSortBy.SelectedValue == "0" ? "" :
                  //" order by " + (ddlSortBy.SelectedValue == "ReYear" ? "ReYear " + ddlSortOrder.SelectedValue + ", ReMonth " + ddlSortOrder.SelectedValue : ddlSortBy.SelectedValue + " " + ddlSortOrder.SelectedValue));

            SqlCommand cmd = new SqlCommand(sql, conn);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Columns[17].ReadOnly = false;
                dt.Rows[i][17] = (i + 1);
            }
            conn.Close();




            if (GridView1.AllowPaging)
            {
                //DataTable dtt = (DataTable)Session["data"];
                GridView1.AllowPaging = false;
                //GridView1.DataSource = Session["data"];
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            else
            {
                GridView1.AllowPaging = true;
                GridView1.PageSize = 10;
                GridView1.DataBind();
            }

            GridView1.UseAccessibleHeader = true;
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            GridView1.FooterRow.TableSection = TableRowSection.TableFooter;
            GridView1.Attributes["style"] = "direction:rtl";
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowIndex % 10 == 0 && row.RowIndex != 0)
                {
                    row.Attributes["style"] = "page-break-after:always;";

                }
                row.Attributes["style"] = "color:black;text-align:center";
                //row.Attributes.Add("","");
            }
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.RenderControl(hw);
            string gridHTML =


                        "<div style = 'float: right; width: 25%; text-align: center; padding-top: 10px' >" +
                            "جامعة الشرق الأوسط" + "<br>MIDDLE EAST UNIVERSITY<br>AMMAN" +
                        "</div>" +
                        "<div style='float: right; width: 49%; text-align: center; padding-top: 10px;padding-bottom:10px'>" +
                        "    تقرير الاستعلام البحثي" +
                        "<br />" +
                        DateTime.Now.ToString("dd-MM-yyyy") +
                        "</div>" +
                        "<div style = 'float: left; width: 25%; text-align: center; padding-top: 10px;padding-bottom:10px; font-size: 100%' >" +
                        "    عمادة الدراسات العليا والبحث العلمي<br />" +
                        "    قسم البحث العلمي" + "<p style='direction:rtl'>أبحاث الكلية : " + lblRCollege.Text +" - "+lblRDept.Text +

                        "</p></div>" +
                        "<div style = 'clear: both' ></ div >";

            gridHTML += sw.ToString().Replace("\"", "'").Replace(System.Environment.NewLine, "");
            gridHTML += "<p style='direction:rtl'>محددات التقرير: هذا التقرير خاص بالنتاج البحثي المنجز تحت مظلة الجامعة ومفهرس في قاعدة بيانات سكوبس</p>";
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

        protected void ddlSortBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            getinfoByDept((ddlDept.Visible?ddlDept.SelectedValue:Session["userDept"].ToString()));
        }

        protected void ddlSortOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            getinfoByDept((ddlDept.Visible ? ddlDept.SelectedValue : Session["userDept"].ToString()));
        }
    }
}