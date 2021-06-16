using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchAcademicUnit
{
    public partial class OverAllResearchReport : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["MEUCV"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);
        static string connstring1 = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn1 = new SqlConnection(connstring1);

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ReId");
                dt.Columns.Add("ReTitle");
                dt.Columns.Add("Magazine");
                dt.Columns.Add("MClass");
                dt.Columns.Add("ReYear");
                dt.Columns.Add("ReMonth");
                dt.Columns.Add("ReStatus");
                dt.Columns.Add("ReParticipate");
                dt.Columns.Add("TeamType");
                dt.Columns.Add("Aff_Auther");
                dt.Columns.Add("resector");
                dt.Columns.Add("refield");
                dt.Columns.Add("RNames");
                dt.Columns.Add("TotalR");
                dt.Columns.Add("TotalRIn");
                dt.Columns.Add("InSupport");
                dt.Columns.Add("SupRName");
                dt.Columns.Add("SupAmount");
                dt.Columns.Add("Reward");
                dt.Columns.Add("AwardRName");
                dt.Columns.Add("AwardAmount");
                Session["dt"] = dt;
                fillSetting();
                //getinfo();
            }
        }
        protected void fillSetting()
        {
            if (conn1.State == ConnectionState.Broken || conn1.State == ConnectionState.Closed)
                conn1.Open();


            SqlCommand cmd = new SqlCommand("select * from YearSetting", conn1);
            DataTable dtSetting = new DataTable();
            dtSetting.Load(cmd.ExecuteReader());

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


            conn.Close();
        }

        protected void getinfo(string cond)
        {
            if (conn1.State == ConnectionState.Broken || conn1.State == ConnectionState.Closed)
                conn1.Open();
            DataTable dt = (DataTable)Session["dt"];
            string sql = @"SELECT      distinct ri.ReId, ReTitle, Magazine, MClass, ReYear, ReMonth, ReStatus, ReParticipate, TeamType, Aff_Auther,resector,refield,
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
isnull((SELECT rinfo.[FeeValue] FROM ReDirectorInfo rinfo,ResearchRewardForm reinfo where rinfo.RequestId=reinfo.AutoId and reinfo.ReTitle=ri.ReTitle and type=1 and reinfo.RequestFinalStatus=N'مكتمل'),'-') 'AwardAmount'
FROM            ResearchsInfo ri,ResearcherInfo r,Research_Researcher rr
WHERE   ri.reid=rr.reid and rr.rid=r.RId " + cond + "ORDER BY ReTitle";

            SqlCommand cmd = new SqlCommand(sql, conn1);
            DataTable dtInstInfo = new DataTable();
            dtInstInfo.Load(cmd.ExecuteReader());


            GridView1.DataSource = dtInstInfo;
            GridView1.DataBind();
            conn1.Close();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "Vithal" + DateTime.Now + ".xls";
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
            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //required to avoid the runtime error "  
            //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
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

        protected void btnApply_Click(object sender, EventArgs e)
        {
            int mf = Convert.ToInt16(ddlFromMonth.SelectedValue);
            int y1 = Convert.ToInt16(ddlFromYear.SelectedValue);
            int mt = Convert.ToInt16(ddlToMonth.SelectedValue);
            int y2 = Convert.ToInt16(ddlToYear.SelectedValue);
            int y = DateTime.Now.Year;
            int m = DateTime.Now.Month;

            string cond = "";
            if (y1 == y2)
                cond = " and reyear=" + y1 + " and remonth between " + mf + " and " + (mt);
            else
            {
                cond = " and ((reyear=" + (y1) + " and remonth between " + mf + " and 12) or (reyear=" + y2 + " and remonth between 1 and " + (mt) + ")) ";
            }

            //Session["cond"] = cond;
            getinfo(cond);
        }

    }
}