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
    public partial class ReasearchFileReport : System.Web.UI.Page
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
                dt.Columns.Add("JobId");
                dt.Columns.Add("Name");
                dt.Columns.Add("College");
                dt.Columns.Add("Dept");
                dt.Columns.Add("PInfo");
                dt.Columns.Add("Qual");
                dt.Columns.Add("WorkExperience");
                dt.Columns.Add("Interest");
                dt.Columns.Add("DbLink");
                dt.Columns.Add("Exp");
                dt.Columns.Add("Research");
                dt.Columns.Add("Conf");
                dt.Columns.Add("Book");
                dt.Columns.Add("Seminar");
                dt.Columns.Add("Ach");
                dt.Columns.Add("Comm");
                dt.Columns.Add("Cert");
                dt.Columns.Add("Thesis");
                dt.Columns.Add("UploadR");
                dt.Columns.Add("UploadC");
                dt.Columns.Add("UploadB");
                dt.Columns.Add("UploadW");
                dt.Columns.Add("UploadCR");
                dt.Columns.Add("LastSeen");
                Session["dt"] = dt;
                getinfo();
            }
        }

        protected void getinfo()
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            if (conn1.State == ConnectionState.Broken || conn1.State == ConnectionState.Closed)
                conn1.Open();
            DataTable dt = (DataTable)Session["dt"];
            string sql = @"SELECT InstJobId,AName,c.CollegeName,d.DeptName
                        FROM InstInfo I,College C, Department D
                        where I.College=C.AutoId and I.Dept=d.AutoId and I.status=1
                        order by collegename";

            SqlCommand cmd = new SqlCommand(sql, conn);
            DataTable dtInstInfo = new DataTable();
            dtInstInfo.Load(cmd.ExecuteReader());

            for (int i = 0; i < dtInstInfo.Rows.Count; i++)
            {
                if(dtInstInfo.Rows[i][0].ToString()=="2422")
                {
                    string x = "";
                }
                DataRow row = dt.NewRow();
                row[0] = dtInstInfo.Rows[i][0];
                row[1] = dtInstInfo.Rows[i][1];
                row[2] = dtInstInfo.Rows[i][2];
                row[3] = dtInstInfo.Rows[i][3];
                string[] tables = {"InstInfo_audits","Qual_audits","WorkExperience","researchinterest", "LinksDataBase", "EvaluationExps"
                        , "Research","Conference","Books","WorkShop","Achievement","Committee"
                        ,"RCertificate","ThesisInfo" };

                for (int j = 0; j < tables.Length; j++)
                {
                    sql = "select * from " + tables[j] + " where JobId=" + row[0];
                    cmd = new SqlCommand(sql, conn);
                    DataTable dtDet = new DataTable();
                    dtDet.Load(cmd.ExecuteReader());
                    if (dtDet.Rows.Count != 0)
                        row[4 + j] = "OK" + (j>1?" - "+ dtDet.Rows.Count.ToString():"");
                    else
                        row[4 + j] = "N/A";
                }

                string[] foldersName = { "/researchs", "/conference", "/books", "/workshops", "/certificate" };
                for (int folderIndex = 0; folderIndex <= 4; folderIndex++)
                {
                    string searchPattern = "*.*";
                    try
                    {
                        var resultData = Directory.GetFiles(Server.MapPath("document/" + row[0] + foldersName[folderIndex]), searchPattern, SearchOption.AllDirectories)
                                        .Select(x => new { FileName = Path.GetFileName(x), FilePath = x, DirName = Path.GetDirectoryName(x) });
                        row[18 + folderIndex] = resultData.Count();
                    }
                    catch
                    {
                        row[18 + folderIndex] =0;
                    }
                }
                cmd = new SqlCommand("Select ActionDate from LogFileLogin where ActionText='Login' and JobId=" + dtInstInfo.Rows[i][0] + " and autoid=(select max(autoid) from LogFileLogin where jobid=" + dtInstInfo.Rows[i][0] + " and ActionText='login')", conn1);
                DataTable dtLog = new DataTable();
                dtLog.Load(cmd.ExecuteReader());
                if (dtLog.Rows.Count != 0)
                    row[dt.Columns.Count-1] = dtLog.Rows[0][0];
                else
                    row[dt.Columns.Count-1] = "لم يتم الدخول للموقع";
                dt.Rows.Add(row);
            }

            GridView1.DataSource = dt;
            GridView1.DataBind();
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

        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                for (int j = 4; j < GridView1.Columns.Count; j++)
                    if (GridView1.Rows[i].Cells[j].Text.ToUpper() == "N/A" || GridView1.Rows[i].Cells[j].Text=="0")
                        GridView1.Rows[i].Cells[j].BackColor = Color.Red;
                    else
                        GridView1.Rows[i].Cells[j].BackColor = Color.Green;
            }
        }
    }
}