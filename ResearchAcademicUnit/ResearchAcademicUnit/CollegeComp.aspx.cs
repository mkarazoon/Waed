using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchAcademicUnit
{
    public partial class CollegeComp : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["logSession"] == null || Session["userid"] == null || Session["userrole"].ToString() == "5" || Session["userrole"].ToString()=="6")
            {
                Response.Redirect("Index.aspx");
            }
            Session["backurl"] = "Index.aspx";
            Label lblUserVal = (Label)Page.Master.FindControl("lblPageName");
            Label lblinfo = (Label)Page.Master.FindControl("lblinfo");
            lblinfo.Text = "الأداء البحثي للكليات";
            lblUserVal.Text = "الأداء البحثي للكليات";
            
            if (!IsPostBack)
            {
                //getPer();
                getData();
            }
        }

        protected void getPer()
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            int y = DateTime.Now.Year;
            int m = DateTime.Now.Month;
            string cond = " and reyear=" + y + " and remonth between 9 and " + m;
            if(m>=1 && m<9)
            {
                cond = " ((and reyear=" + (y - 1) + " and remonth between 9 and 12) or (reyear=" + y + " and remonth between 1 and " + m + ")) ";
            }
            SqlCommand cmd = new SqlCommand("select isnull(count(distinct reid),0) from Research_Researcher where reid in (select reid from ResearchsInfo where retype=N'بحوث علمية' and (InSupport=N'نعم' or Reward=N'نعم')"+ cond +")", conn);
            SqlDataReader drR = cmd.ExecuteReader();
            drR.Read();
            int R =Convert.ToInt16( drR[0]);

            cmd = new SqlCommand("select isnull(count(distinct reid),0) from Research_Researcher where reid in (select reid from ResearchsInfo where retype=N'مؤتمر علمي' and (InSupport=N'نعم' or Reward=N'نعم') "+ cond + ")", conn);
            SqlDataReader drC = cmd.ExecuteReader();
            drC.Read();
            int C = Convert.ToInt16(drC[0]);

            cmd = new SqlCommand("select isnull(count(distinct reid),0) from Research_Researcher where reid in (select reid from ResearchsInfo where retype=N'نشاط تأليفي' and (InSupport=N'نعم' or Reward=N'نعم') "+cond + ")", conn);
            SqlDataReader drP = cmd.ExecuteReader();
            drP.Read();
            int P = Convert.ToInt16(drP[0]);

            //cmd = new SqlCommand("select isnull(count(distinct reid),0) from ResearchsInfo where reyear=" + DateTime.Now.Year, conn);
            //SqlDataReader drAll = cmd.ExecuteReader();
            //drAll.Read();
            //double all = Convert.ToInt16(drAll[0]);

            //lblRSupportPer.Text = (R).ToString();
            //lblCSupportPer.Text = (C).ToString();
            //lblPSupportPer.Text = (P).ToString();

            cmd = new SqlCommand("select * from SupportSetting", conn);
            DataTable dtt = new DataTable();
            dtt.Load(cmd.ExecuteReader());
            if (dtt.Rows.Count != 0)
            {
                //RDiv.Style.Add("background-color", (R <= Convert.ToInt16(dtt.Rows[0][1].ToString()) ? "green" : "red"));
                //CDiv.Style.Add("background-color", (C <= Convert.ToInt16(dtt.Rows[0][2].ToString()) ? "green" : "red"));
                //PDiv.Style.Add("background-color", (P <= Convert.ToInt16(dtt.Rows[0][3].ToString()) ? "green" : "red"));
            }
            conn.Close();
        }

        protected void getData()
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            //try
            //{
            SqlCommand cmd1 = new SqlCommand("select * from YearSetting", conn);
            DataTable dtYear = new DataTable();
            dtYear.Load(cmd1.ExecuteReader());

            int mf = Convert.ToInt16(dtYear.Rows[0][1]);
            int y1 = Convert.ToInt16(dtYear.Rows[0][2]);
            int mt = Convert.ToInt16(dtYear.Rows[0][3]);
            int y2 = Convert.ToInt16(dtYear.Rows[0][4]);

            //int y = DateTime.Now.Year;
            int m1 = DateTime.Now.Month;
            int m2 = 0;
            string cond = " and ReYear=" + y1 + " and ReMonth between " + mf + " and " + m1;
            if (DateTime.Now.Month < mf)
            {
                //y = y - 1;
                m1 = 12;
                m2 = DateTime.Now.Month;
                cond = " and ((ReYear=" + (y1) + " and ReMonth between "+mf+" and 12) or (ReYear=" + y2 + " and ReMonth between 1 and " + m2+"))";
            }
            lblNote.Text = "هذا التقرير خاص بالنتاج البحثي للعام الحالي " + mf + "-" + y1 + " إلى " + mt + "-" + y2;
            DataTable data = new DataTable();
            data.Columns.Add("College");
            data.Columns.Add("EmpCount");
            data.Columns.Add("total");
            data.Columns.Add("CollegePer",typeof(double));
            data.Columns.Add("RName");
            data.Columns.Add("Ptotal");
            data.Columns.Add("Atotal");
            string sql = "";
            sql = "select Distinct College,count(RId) EmpCount From ResearcherInfo where RStatus=N'IN' group by College order by College";
            SqlCommand cmdCD = new SqlCommand(sql, conn);
            DataTable dtCD = new DataTable();
            dtCD.Load(cmdCD.ExecuteReader());
            for (int i = 0; i < dtCD.Rows.Count; i++)
            {
                
                //row[0] = dtCD.Rows[i]["College"].ToString();
                //row[1] = dtCD.Rows[i]["EmpCount"].ToString();
                int total = 0;
                sql = "select isnull(COUNT(distinct rr1.ReId),0) cnt";
                sql += " from Research_Researcher rr1,ResearchsInfo r1, ResearcherInfo ri";
                sql += " where rr1.ReId = r1.ReId and rr1.RId = ri.RId";
                sql += " and RStatus=N'IN' and College = N'" + dtCD.Rows[i][0] + "' "+cond;

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                
                total = Convert.ToInt16(dr[0]);
                if (total != 0)
                {
                    //row[2] = dr[0];
                    sql = "select Distinct r.RID,(RaName + ' - ' + REName) RName From ResearcherInfo r,Research_Researcher rr,ResearchsInfo ri where r.rid=rr.rid and ri.reid=rr.reid and RStatus=N'IN' and College=N'" + dtCD.Rows[i]["College"].ToString() + "' order by RName";
                    SqlCommand cmd10 = new SqlCommand(sql, conn);
                    DataTable dtR = new DataTable();
                    dtR.Load(cmd10.ExecuteReader());
                    for (int rid = 0; rid < dtR.Rows.Count; rid++)
                    {
                        DataRow row = data.NewRow();
                        row[0] = dtCD.Rows[i]["College"].ToString();
                        row[1] = dtCD.Rows[i]["EmpCount"].ToString();
                        row[2] = dr[0];
                        row[3] = (Convert.ToDouble(dr[0].ToString()) / Convert.ToDouble(dtCD.Rows[i]["EmpCount"]));
                        row[4] = dtR.Rows[rid][1];
                        sql = "select isnull(count(distinct rr.reid),0) from Research_Researcher rr,ResearchsInfo r where rr.reid=r.reid and ReStatus=N'منشور' and rid=N'" + dtR.Rows[rid][0] + "'" + cond;
                        SqlCommand cmd2 = new SqlCommand(sql, conn);
                        DataTable dtRCountP = new DataTable();
                        dtRCountP.Load(cmd2.ExecuteReader());
                        row[5] = dtRCountP.Rows[0][0];

                        sql = "select isnull(count(distinct rr.reid),0) from Research_Researcher rr,ResearchsInfo r where rr.reid=r.reid and ReStatus=N'مقبول للنشر' and rid=N'" + dtR.Rows[rid][0] + "'" + cond;
                        SqlCommand cmd3 = new SqlCommand(sql, conn);
                        DataTable dtRCountA = new DataTable();
                        dtRCountA.Load(cmd3.ExecuteReader());
                        row[6] = dtRCountA.Rows[0][0];
                        if (Convert.ToInt16(row[5]) != 0 || Convert.ToInt16(row[6]) != 0)
                            data.Rows.Add(row);
                    }
                }
                else
                {
                    DataRow row = data.NewRow();
                    row[0] = dtCD.Rows[i]["College"].ToString();
                    row[1] = dtCD.Rows[i]["EmpCount"].ToString();
                    row[2] = 0;
                    row[3] = 0;
                    row[4] = "";
                    row[5] = 0;
                    row[6] = 0;
                    data.Rows.Add(row);
                }
            }

            data.DefaultView.Sort= "CollegePer DESC, College";
            data = data.DefaultView.ToTable();
            //int t = 0, r = 0, c = 0, p = 0;
            DataTable newData = new DataTable();
            newData.Columns.Add("College");
            newData.Columns.Add("EmpCount");
            newData.Columns.Add("total");
            newData.Columns.Add("CollegePer");
            newData.Columns.Add("RName");
            newData.Columns.Add("Ptotal");
            newData.Columns.Add("Atotal");

            newData = data.Copy();
            int newindex = 0;
            for (int x = 1; x < data.Rows.Count; x++)
            {
                if (data.Rows[x - 1][0].ToString() != data.Rows[x][0].ToString())
                {
                    DataRow row = newData.NewRow();
                    row[0] = "";
                    row[1] = "";
                    row[2] = "";
                    row[3] = 0;
                    row[4] = "";
                    row[5] = "";
                    row[6] = "";
                    newData.Rows.InsertAt(row, x + newindex);
                    newindex++;
                }
            }
            DataRow row1 = newData.NewRow();
            row1[0] = "";
            row1[1] = "";
            row1[2] = "";
            row1[3] = 0;
            row1[4] = "";
            row1[5] = "";
            row1[6] = "";
            newData.Rows.InsertAt(row1, data.Rows.Count + newindex);

            //Session["data"] = newData;
            //Label4.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
            Session["data"] = newData;
            GridView1.DataSource = Session["data"];
            GridView1.DataBind();
            msgDiv.Visible = false;
            dataDiv.Visible = true;
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType==DataControlRowType.DataRow)
            {
                if(e.Row.Cells[0].Text== "&nbsp;")
                {
                    e.Row.BackColor = Color.White; //Color.FromArgb(163, 20, 20);
                    e.Row.ForeColor = Color.Black;
                    e.Row.Cells[0].ColumnSpan = 7;
                    e.Row.Cells[1].Visible = false;
                    e.Row.Cells[2].Visible = false;
                    e.Row.Cells[3].Visible = false;
                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[5].Visible = false;
                    e.Row.Cells[6].Visible = false;

                }
            }
        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            for (int rowIndex = GridView1.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow gvRow = GridView1.Rows[rowIndex];
                GridViewRow gvPreviousRow = GridView1.Rows[rowIndex + 1];
                for (int cellCount = 0; cellCount < 4; cellCount++)
                {

                    if (gvRow.Cells[cellCount].Text == gvPreviousRow.Cells[cellCount].Text)
                    {
                        if (gvPreviousRow.Cells[cellCount].RowSpan < 2)
                        {
                            gvRow.Cells[cellCount].RowSpan = 2;
                        }
                        else
                        {
                            gvRow.Cells[cellCount].RowSpan =
                            gvPreviousRow.Cells[cellCount].RowSpan + 1;
                        }
                        gvPreviousRow.Cells[cellCount].Visible = false;
                    }
                }
            }
        }
    }
}