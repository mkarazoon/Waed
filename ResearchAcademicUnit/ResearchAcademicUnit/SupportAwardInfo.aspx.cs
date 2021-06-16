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
    public partial class SupportAwardInfo : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userid"] == null || Session.IsNewSession)
                Response.Redirect("Login.aspx");

            Session["backurl"] = "Default.aspx";

            Label lblUserVal = (Label)Page.Master.FindControl("lblPageName");
            lblUserVal.Text = "تقرير الدعم والمكافأة";
            Label lblinfo = (Label)Page.Master.FindControl("lblinfo");
            lblinfo.Text = "تقرير الدعم والمكافأة";

            HtmlGenericControl divf = (HtmlGenericControl)Page.Master.FindControl("printfooter");
            divf.Visible = false;


            Label lblUser = (Label)Page.Master.FindControl("lblUserName");
            lblUser.Text = "تاريخ التقرير : " + DateTime.Now.Date.ToString("dd-MM-yyyy");

            if (!IsPostBack)
            {
                fillSetting();
                //getData();
            }

        }

        protected void fillSetting()
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("Year");
            for (int i = 2007; i <= Convert.ToInt16(DateTime.Now.Date.Year) + 1; i++)
            {
                DataRow row = dt1.NewRow();
                row[0] = i;
                dt1.Rows.Add(row);
            }

            ddlFromYear.DataSource = dt1;
            ddlFromYear.DataTextField = "Year";
            ddlFromYear.DataValueField = "Year";
            ddlFromYear.DataBind();
            ddlFromYear.Items.Insert(0, "From Year");
            ddlFromYear.Items[0].Value = "0";

            DataTable dt2 = new DataTable();
            dt2.Columns.Add("Month");
            for (int i = 1; i <= 12; i++)
            {
                DataRow row = dt2.NewRow();
                row[0] = i;
                dt2.Rows.Add(row);
            }

            ddlFromMonth.DataSource = dt2;
            ddlFromMonth.DataTextField = "Month";
            ddlFromMonth.DataValueField = "Month";
            ddlFromMonth.DataBind();
            ddlFromMonth.Items.Insert(0, "From Month");
            ddlFromMonth.Items[0].Value = "0";

            ddlToMonth.DataSource = dt2;
            ddlToMonth.DataTextField = "Month";
            ddlToMonth.DataValueField = "Month";
            ddlToMonth.DataBind();
            ddlToMonth.Items.Insert(0, "To Month");
            ddlToMonth.Items[0].Value = "0";
        }

        protected void getData()
        {

            int mf = Convert.ToInt16(ddlFromMonth.SelectedValue);
            int y1 = Convert.ToInt16(ddlFromYear.SelectedValue);
            int mt = Convert.ToInt16(ddlToMonth.SelectedValue);
            int y2 = Convert.ToInt16(ddlToYear.SelectedValue);
            int y = DateTime.Now.Year;
            int m = DateTime.Now.Month;

            string cond = "";
            if (y1 == y2)
                cond = " and year(rfi.ReqDate)=" + y1 + " and Month(rfi.ReqDate) between " + mf + " and " + (mt);
            else
            {
                cond = " and ((year(rfi.ReqDate)=" + (y1) + " and Month(rfi.ReqDate) between " + mf + " and 12) or (year(rfi.ReqDate)=" + y2 + " and Month(rfi.ReqDate) between 1 and " + (mt) + ")) ";
            }
            Session["cond"] = cond;

            lblType.Text = ddlType.SelectedItem.Text;
            lblPeriod.Text = ddlFromMonth.SelectedValue + "-" + ddlFromYear.SelectedValue + " | " + ddlToMonth.SelectedValue + "-" + ddlToYear.SelectedValue;
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            string sql = "";
            sql = @"select ReTitle,(case when isnull((SELECT rinfo.[FeeValue] 
					FROM ReDirectorInfo rinfo," + (ddlType.SelectedValue == "0" ? "ResearchFeeInfo" : "ResearchRewardForm")+ @" reinfo 
					where rinfo.RequestId=reinfo.AutoId and reinfo.ReTitle=rfi.ReTitle and type=" + ddlType.SelectedValue + @" and reinfo.RequestFinalStatus=N'مكتمل'),'-')<>N'' 
	                then isnull((SELECT rinfo.[FeeValue] 
				    FROM ReDirectorInfo rinfo," + (ddlType.SelectedValue == "0" ? "ResearchFeeInfo" : "ResearchRewardForm") + @" reinfo 
				    where rinfo.RequestId=reinfo.AutoId and reinfo.ReTitle=rfi.ReTitle and type=" + ddlType.SelectedValue + @" and reinfo.RequestFinalStatus=N'مكتمل'),'-')
	                else (isnull((select NewAmount 
			        From NewAmountAward na,ResearchRewardForm rf 
			        where na.ReqId=rf.AutoId and na.AcdId=ri.AcdId),N'0 دينار اردني'))
	                end) FeeValue,RaName,
                    Month((select distinct RequestDate From RequestsFollowUp where ReqStatus=N'مكتمل' and RequestId=rfi.autoid and type=" + ddlType.SelectedValue + @")) Month,
                    year((select distinct RequestDate From RequestsFollowUp where ReqStatus=N'مكتمل' and RequestId=rfi.autoid and type=" + ddlType.SelectedValue + @")) Year,
                    ROW_NUMBER() 
                    OVER(ORDER BY year((select distinct RequestDate From RequestsFollowUp where ReqStatus=N'مكتمل' and RequestId=rfi.autoid and type=" + ddlType.SelectedValue + @"))
                    , Month((select distinct RequestDate From RequestsFollowUp where ReqStatus=N'مكتمل' and RequestId=rfi.autoid and type=" + ddlType.SelectedValue + @"))) AS row_num
                    from " + (ddlType.SelectedValue == "0" ? "ResearchFeeInfo" : "ResearchRewardForm")
                    + @" rfi,ReDirectorInfo rdi,ResearcherInfo RI 
                    where rfi.autoid=rdi.requestid 
                    and rfi.JobId=RI.AcdId
                    and RequestFinalStatus=N'مكتمل' and rdi.Type=" + ddlType.SelectedValue + cond 
                    + @" order by year((select distinct RequestDate From RequestsFollowUp where ReqStatus=N'مكتمل' and RequestId=rfi.autoid and type=" + ddlType.SelectedValue+ @"))
                        ,Month((select distinct RequestDate From RequestsFollowUp where ReqStatus=N'مكتمل' and RequestId=rfi.autoid and type=" + ddlType.SelectedValue+@"))";

            SqlCommand cmd = new SqlCommand(sql, conn);
            GridView1.DataSource = cmd.ExecuteReader();
            GridView1.DataBind();

            conn.Close();
            if (GridView1.Rows.Count != 0)
            {
                double valJD = 0;
                double valUS = 0;
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    string[] value = GridView1.Rows[i].Cells[3].Text.Split(' ');
                    if (value[1] == "دينار")
                        valJD += Convert.ToDouble(value[0]);
                    else
                        valUS += Convert.ToDouble(value[0]);
                }

                GridView1.FooterRow.BackColor = System.Drawing.Color.LightGray;

                GridView1.FooterRow.Cells[1].Text = "عدد الأبحاث : " + GridView1.Rows.Count;
                GridView1.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                GridView1.FooterRow.Cells[1].Font.Bold = true;


                string val = valJD.ToString() + " دينار اردني" + " / " + valUS.ToString() + " دولار أمريكي";

                GridView1.FooterRow.Cells[2].Text = "المجموع";
                GridView1.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                GridView1.FooterRow.Cells[2].Font.Bold = true;

                GridView1.FooterRow.Cells[3].Text = val.ToString();
                GridView1.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Center;
                GridView1.FooterRow.Cells[3].Font.Bold = true;
            }
            //Session["data"] = GridView1.DataSource;
            //if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
            //    conn.Open();
            //SqlCommand cmd1 = new SqlCommand("select * from YearSetting", conn);
            //DataTable dtYear = new DataTable();
            //dtYear.Load(cmd1.ExecuteReader());

            //int mf = Convert.ToInt16(dtYear.Rows[0][1]);
            //int y1 = Convert.ToInt16(dtYear.Rows[0][2]);
            //int mt = Convert.ToInt16(dtYear.Rows[0][3]);
            //int y2 = Convert.ToInt16(dtYear.Rows[0][4]);

            //int y = DateTime.Now.Year;
            //int m = DateTime.Now.Month;
            //string cond = " where reyear=" + y1 + " and remonth between " + mf + " and " + m;
            //if (m >= 1 && m < mf)
            //{
            //    cond = " where ((reyear=" + (y1) + " and remonth between " + mf + " and 12) or (reyear=" + y2 + " and remonth between 1 and " + m + ")) ";
            //}

            //    string sql = "";
            //sql = "select *, ROW_NUMBER() OVER(ORDER BY ReYear DESC, ReMonth DESC,MClassInt ASC) AS row_num" +
            //      " FROM " +
            //      " (SELECT distinct RE.ReId, ReTitle, ReType, ReLevel, ReYear,ReMonth, ReCitation, ReParticipate, Magazine," +
            //      " SourceType, MClass, CitationAvg, InSupport, Reward, outSupport,ReStatus,MClassInt,TopMag " +
            //      " FROM ResearchsInfo RE "+((DropDownList1.SelectedIndex != -1 ?( DropDownList1.SelectedValue=="0"? cond:""):cond)) + ") final";
            //      //" where ReId in (select ReId from Research_Researcher rr where rr.RId in (select ri.RId from ResearcherInfo ri where College = N'" + ddlResearcher.SelectedValue + "'))) final";
            //SqlCommand cmd = new SqlCommand(sql, conn);
            //DataTable dt = new DataTable();
            //dt.Load(cmd.ExecuteReader());
            //if (dt.Rows.Count != 0)
            //{
            //    Session["data"] = dt;

            //    GridView1.DataSource = dt;
            //    GridView1.DataBind();
            //    msgDiv.Visible = false;
            //    //infoDiv.Visible = true;
            //}
            //else
            //{
            //    msgDiv.Visible = true;
            //    lblMsg.Text = "لا يوجد معلومات حاليا";
            //}

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("IndexUni.aspx");
        }

        //protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if(e.Row.RowType==DataControlRowType.DataRow)
        //    {
        //        if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
        //            conn.Open();
        //        string sql = "";
        //        sql = "Select distinct Aff_Auther From ResearchsInfo ri ";
        //        sql += " where ri.reid='"+e.Row.Cells[1].Text+"'";
        //        SqlCommand cmd = new SqlCommand(sql, conn);
        //        DataTable dt = new DataTable();
        //        dt.Load(cmd.ExecuteReader());
        //        HtmlGenericControl Rdiv = e.Row.FindControl("ReInfoDiv") as HtmlGenericControl;

        //        string[] aff = dt.Rows[0][0].ToString().Trim().Split(';');
        //        for (int i = 0; i < aff.Length; i++)
        //        {
        //            HtmlGenericControl newdiv = new HtmlGenericControl();
        //            newdiv.InnerText=(i+1)+") "+ aff[i].ToString();
        //            Rdiv.Controls.Add(newdiv);
        //            Rdiv.Controls.Add(new LiteralControl("<br />"));
        //            Rdiv.Controls.Add(new LiteralControl("<br />"));
        //        }
        //    }
        //}

        //protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    GridView1.DataSource = (DataTable)Session["data"];
        //    GridView1.PageIndex = e.NewPageIndex;
        //    GridView1.DataBind();
        //}

        protected void Button1_Click(object sender, EventArgs e)
        {
            //if (GridView1.AllowPaging)
            //{
            //    GridView1.AllowPaging = false;
            //    GridView1.DataSource = Session["data"];
            //    GridView1.DataBind();
            //}
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
            GridView1.HeaderRow.BackColor = System.Drawing.Color.LightGray;
            GridView1.HeaderRow.Height =Unit.Pixel(40);
            foreach (GridViewRow row in GridView1.Rows)
            {
                //if (row.RowIndex % 10 == 0 && row.RowIndex != 0)
                //{
                //    row.Attributes["style"] = "page-break-after:always;";

                //}
                row.Attributes["style"] = "color:black;text-align:center;font-size:18px";
                row.Cells[1].Attributes["style"] = "color:black;text-align:left;padding:10px";
                row.Cells[3].Attributes["style"] = "color:black;text-align:right;padding:10px";
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
                        "    قسم البحث العلمي" + "<br>تقرير الدعم والمكافأة" +

                        "</div>" +
                        "<div style = 'clear: both' ></ div >";

            gridHTML += sw.ToString().Replace("\"", "'").Replace(System.Environment.NewLine, "");
            //gridHTML += "<p style='direction:rtl'>محددات التقرير: هذا التقرير خاص بالنتاج البحثي المنجز تحت مظلة الجامعة ومفهرس في قاعدة بيانات سكوبس</p>";
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
            //GridView1.AllowPaging = true;
            //GridView1.DataBind();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /*Verifies that the control is rendered */
        }

        //protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        //{
            
        //}

        protected void btnApply_Click(object sender, EventArgs e)
        {
            getData();
        }

        protected void ddlFromYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("Year");
            for (int i = Convert.ToInt16(ddlFromYear.SelectedValue); i <= Convert.ToInt16(DateTime.Now.Year) + 1; i++)
            {
                DataRow row = dt1.NewRow();
                row[0] = i;
                dt1.Rows.Add(row);
            }

            ddlToYear.DataSource = dt1;
            ddlToYear.DataTextField = "Year";
            ddlToYear.DataValueField = "Year";
            ddlToYear.DataBind();
            ddlToYear.Items.Insert(0, "To Year");
            ddlToYear.Items[0].Value = "0";
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "NotInserted_" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            GridView1.GridLines = GridLines.Both;
            GridView1.HeaderStyle.Font.Bold = true;
            GridView1.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());

            //Response.Redirect("NewResearch.aspx");
            Response.End();

        }

        protected void btnAll_Click(object sender, EventArgs e)
        {
            string sql = "";
            sql = @"SELECT      distinct ri.ReId, ReTitle, Magazine, MClass, ReYear, ReMonth, ReStatus, ReParticipate, TeamType, Aff_Auther,
(select 
STUFF((
select '; ' + rii.RaName
from ResearcherInfo rii,Research_Researcher rrr
where rii.RId=rrr.RId and rrr.ReId=rs.reid
order by AutoId
FOR XML PATH('')), 1, 1, '') researchers
from ResearchsInfo rs where rs.ReId=ri.ReId) 'RNames', TotalR, TotalRIn,
InSupport,isnull((select distinct RaName from ResearcherInfo r1,Reward_Support rs where r1.RId=rs.rid and rs.reid=ri.ReId and rtype=1),'-') 'SupRName',
isnull((SELECT rinfo.[FeeValue] FROM ReDirectorInfo rinfo,ResearchFeeInfo reinfo where rinfo.RequestId=reinfo.AutoId and reinfo.ReTitle=ri.ReTitle and type=0 and reinfo.RequestFinalStatus=N'مكتمل'),'-') 'SupAmount',
Reward,isnull((select distinct RaName from ResearcherInfo r1,Reward_Support rs where r1.RId=rs.rid and rs.reid=ri.ReId and rtype=2 ),'-') 'AwardRName',

(case when isnull((SELECT rinfo.[FeeValue] FROM ReDirectorInfo rinfo,ResearchRewardForm reinfo where rinfo.RequestId=reinfo.AutoId and reinfo.ReTitle=ri.ReTitle and type=1 and reinfo.RequestFinalStatus=N'مكتمل'),'-')<>N'' 
then isnull((SELECT rinfo.[FeeValue] FROM ReDirectorInfo rinfo,ResearchRewardForm reinfo where rinfo.RequestId=reinfo.AutoId and reinfo.ReTitle=ri.ReTitle and type=1 and reinfo.RequestFinalStatus=N'مكتمل'),'-')
else (select NewAmount From NewAmountAward na,ResearchRewardForm rf where na.ReqId=rf.AutoId and na.AcdId=r.AcdId) end) 'AwardAmount'
FROM            ResearchsInfo ri,ResearcherInfo r,Research_Researcher rr
WHERE   ri.reid=rr.reid and rr.rid=r.RId
and (insupport=N'نعم' or reward=N'نعم')
ORDER BY ReTitle";
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand(sql, conn);
            GridView2.DataSource = cmd.ExecuteReader();
            GridView2.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "NotInserted_" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            GridView2.GridLines = GridLines.Both;
            GridView2.HeaderStyle.Font.Bold = true;
            GridView2.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());

            //Response.Redirect("NewResearch.aspx");
            Response.End();


        }
    }
}