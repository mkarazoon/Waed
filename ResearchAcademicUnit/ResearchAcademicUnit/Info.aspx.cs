using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchAcademicUnit
{
    public partial class Info : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["logSession"] == null || Session["userid"] == null)
            {
                Response.Redirect("Index.aspx");
            }
            Session["backurl"] = "Index.aspx";
            Label lblUserVal = (Label)Page.Master.FindControl("lblPageName");
            lblUserVal.Text = "تقرير الأداء البحثي للباحث";
            Label lblinfo = (Label)Page.Master.FindControl("lblinfo");
            lblinfo.Text = "تقرير الأداء البحثي للباحث";

            Label lblUser = (Label)Page.Master.FindControl("lblUserName");
            lblUser.Text = (Session["userrole"].ToString() == "6" ? "أهلا بكم " + Session["userCollege"] : "");

            if (!IsPostBack)
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();
                string UserCond = " where  RStatus='IN'";
                SqlCommand cmdCollege = new SqlCommand("select College,RID,RAName From ResearcherInfo where Acdid=" + Session["userid"], conn);
                SqlDataReader dr = cmdCollege.ExecuteReader();
                dr.Read();

                if (Session["userrole"].ToString() == "6")
                {
                    UserCond = " where RStatus='IN' and AcdId=" + Session["userid"];
                    ddlCollege.Visible = false;
                }
                else if (Session["userrole"].ToString() == "5")
                {
                    UserCond = " where RStatus='IN' and college=N'" + dr[0].ToString() + "'";
                    ddlCollege.Visible = false;
                }
                else if (Session["userrole"].ToString() == "7")
                {
                    UserCond = " where RStatus='IN'  and college=N'" + Session["userCollege"] + "' and dept=N'" + Session["userDept"] + "'";
                    ddlCollege.Visible = false;
                }


                SqlCommand cmd1 = new SqlCommand("Select RID,(RAName + ' - '+ REName) RName,RAName From ResearcherInfo"+ UserCond, conn);
                ddlResearcher.DataSource = cmd1.ExecuteReader();
                ddlResearcher.DataTextField = "RName";
                ddlResearcher.DataValueField = "RID";
                ddlResearcher.DataBind();
                ddlResearcher.Items.Insert(0, "اختيار الباحث");
                ddlResearcher.Items[0].Value = "0";
                if (Session["userrole"].ToString() == "6")
                {
                    ddlResearcher.SelectedValue =dr[1].ToString();
                    ddlResearcher.Visible = false;
                    lblRName.Text = dr[2].ToString();
                    nameDiv.Attributes.Add("class", nameDiv.Attributes["class"].ToString().Replace("lbl", ""));
                    getRData();
                }
                else
                {
                    ddlResearcher.Visible = true;
                    lblRName.Text = "";
                }
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("select Distinct College From ResearcherInfo where RStatus='IN'", conn);
                ddlCollege.DataSource = cmd.ExecuteReader();
                ddlCollege.DataTextField = "College";
                ddlCollege.DataValueField = "College";
                ddlCollege.DataBind();
                ddlCollege.Items.Insert(0, "حدد الكلية");
                ddlCollege.Items[0].Value = "0";

                Label4.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                conn.Close();
            }
        }

        protected void ddlResearcher_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblRName.Text = ddlResearcher.SelectedItem.Text;
            getRData();
            //if (ddlResearcher.SelectedValue != "0")
            //{
            //    if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
            //        conn.Open();
            //    //try
            //    //{
            //    SqlCommand cmd = new SqlCommand("SELECT ROW_NUMBER() OVER (ORDER BY RI.RID) row_num, * FROM [ResearcherInfo] RI, ResearchsInfo RE, Research_Researcher RR " +
            //        " where Ri.RId = RR.RId and re.ReId = rr.ReId and RI.RID ='" + ddlResearcher.SelectedValue + "'", conn);
            //    DataTable dt = new DataTable();
            //    dt.Load(cmd.ExecuteReader());
            //    if (dt.Rows.Count != 0)
            //    {
            //        //lblMsg.Text = "";
            //        //lblRName.Text = dt.Rows[0]["REName"].ToString();
            //        lblRNo.Text = dt.Rows[0]["Rid"].ToString();
            //        lblRDegree.Text = dt.Rows[0]["Rlevel"].ToString();
            //        lblRHireDate.Text = (dt.Rows[0]["HDate"].ToString() != "" ? DateTime.Parse(dt.Rows[0]["HDate"].ToString()).ToString("dd-MM-yyyy") : "");
            //        lblRCollege.Text = dt.Rows[0]["College"].ToString();
            //        lblRDept.Text = dt.Rows[0]["Dept"].ToString();
            //        Label4.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");

            //        GridView1.DataSource = dt;
            //        GridView1.DataBind();
            //        msgDiv.Visible = false;
            //        infoDiv.Visible = true;
            //    }
            //    else
            //    {
            //        msgDiv.Visible = true;
            //        infoDiv.Visible = false;
            //        lblMsg.Text = "لا يوجد معلومات حاليا";
            //        lblRNo.Text = "";
            //        lblRDegree.Text = "";
            //        lblRHireDate.Text = "";
            //        lblRCollege.Text = "";
            //        lblRDept.Text = "";
            //        Label4.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");

            //    }
            //    conn.Close();
            //}
            //else
            //{
            //    msgDiv.Visible = true;
            //    infoDiv.Visible = false;
            //    lblMsg.Text = "الرجاء تحديد الباحث";
            //    lblRNo.Text = "";
            //    lblRDegree.Text = "";
            //    lblRHireDate.Text = "";
            //    lblRCollege.Text = "";
            //    lblRDept.Text = "";
            //    Label4.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");

            //}
        }

        protected void getRData()
        {
            if (ddlResearcher.SelectedValue != "0")
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();
                //try
                //{
                SqlCommand cmd = new SqlCommand("SELECT ROW_NUMBER() OVER (ORDER BY RI.RID) row_num, * FROM [ResearcherInfo] RI, ResearchsInfo RE, Research_Researcher RR " +
                    " where Ri.RId = RR.RId and re.ReId = rr.ReId and RI.RID ='" + ddlResearcher.SelectedValue + "' order by ReYear DESC,ReMonth DESC", conn);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count != 0)
                {
                    //lblMsg.Text = "";
                    //lblRName.Text = dt.Rows[0]["REName"].ToString();
                    lblRNo.Text = dt.Rows[0]["Rid"].ToString();
                    lblRDegree.Text = dt.Rows[0]["Rlevel"].ToString();
                    lblRHireDate.Text = (dt.Rows[0]["HDate"].ToString() != "" ? DateTime.Parse(dt.Rows[0]["HDate"].ToString()).ToString("dd-MM-yyyy") : "");
                    lblRCollege.Text = dt.Rows[0]["College"].ToString();
                    lblRDept.Text = dt.Rows[0]["Dept"].ToString();
                    Label4.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    Session["ResearcherInfo"] = dt;
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    msgDiv.Visible = false;
                    infoDiv.Visible = true;
                }
                else
                {
                    msgDiv.Visible = true;
                    infoDiv.Visible = false;
                    lblMsg.Text = "لا يوجد معلومات حاليا";
                    lblRNo.Text = "";
                    lblRDegree.Text = "";
                    lblRHireDate.Text = "";
                    lblRCollege.Text = "";
                    lblRDept.Text = "";
                    Label4.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");

                }
                conn.Close();
            }
            else
            {
                msgDiv.Visible = true;
                infoDiv.Visible = false;
                lblMsg.Text = "الرجاء تحديد الباحث";
                lblRNo.Text = "";
                lblRDegree.Text = "";
                lblRHireDate.Text = "";
                lblRCollege.Text = "";
                lblRDept.Text = "";
                Label4.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");

            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (GridView1.AllowPaging)
            {
                GridView1.AllowPaging = false;
                GridView1.DataSource = Session["ResearcherInfo"];
                GridView1.DataBind();
            }
            GridView1.DataSource = Session["ResearcherInfo"];
            GridView1.DataBind();
            //else
            //{
            //    GridView1.AllowPaging = true;
            //    GridView1.PageSize = 10;
            //    GridView1.DataBind();
            //}

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
                        "    قسم البحث العلمي" + "<p style='direction:rtl'>أبحاث الباحث : " + lblRName.Text+

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

        protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            if (ddlCollege.SelectedValue != "0")
            {
                SqlCommand cmd1 = new SqlCommand("Select RID,(RAName + ' - '+ REName) RName,RAName From ResearcherInfo where RSTatus='IN' and College=N'" + ddlCollege.SelectedValue+"'", conn);
                ddlResearcher.DataSource = cmd1.ExecuteReader();
                ddlResearcher.DataTextField = "RName";
                ddlResearcher.DataValueField = "RID";
                ddlResearcher.DataBind();
                ddlResearcher.Items.Insert(0, "اختيار الباحث");
                ddlResearcher.Items[0].Value = "0";
            }
            else
            {
                SqlCommand cmd1 = new SqlCommand("Select RID,(RAName + ' - '+ REName) RName,RAName From ResearcherInfo where RSTatus='IN'", conn);
                ddlResearcher.DataSource = cmd1.ExecuteReader();
                ddlResearcher.DataTextField = "RName";
                ddlResearcher.DataValueField = "RID";
                ddlResearcher.DataBind();
                ddlResearcher.Items.Insert(0, "اختيار الباحث");
                ddlResearcher.Items[0].Value = "0";
            }
            conn.Close();
        }
    }
}