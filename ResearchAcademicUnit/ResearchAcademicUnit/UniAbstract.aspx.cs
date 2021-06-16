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
    public partial class UniAbstract : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["logSession"] == null || Session["userid"] == null || Session["userrole"].ToString() == "5" || Session["userrole"].ToString() == "6")
            {
                Response.Redirect("Index.aspx");
            }
            Session["cond"] = null;
            Session["backurl"] = "Index.aspx";
            Label lblUserVal = (Label)Page.Master.FindControl("lblPageName");
            Label lblinfo = (Label)Page.Master.FindControl("lblinfo");
            lblinfo.Text = "ملخص أبحاث الجامعة";
            lblUserVal.Text = "ملخص أبحاث الجامعة";
            
            if (!IsPostBack)
            {
                fillSetting();
                getPer();
                getData();
            }
        }

        protected void getPer()
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand cmd1 = new SqlCommand("select * from YearSetting", conn);
            DataTable dtYear = new DataTable();
            dtYear.Load(cmd1.ExecuteReader());

            int mf = Convert.ToInt16(dtYear.Rows[0][1]);
            int y1 = Convert.ToInt16(dtYear.Rows[0][2]);
            int mt = Convert.ToInt16(dtYear.Rows[0][3]);
            int y2 = Convert.ToInt16(dtYear.Rows[0][4]);

            int y = DateTime.Now.Year;
            int m = DateTime.Now.Month;
            string cond = " and reyear=" + y1 + " and remonth between " + mf + " and " + m;
            if(m>=1 && m<mf)
            {
                cond = " and(( reyear=" + (y1) + " and remonth between "+mf+" and 12) or (reyear=" + y2 + " and remonth between 1 and " + m + ")) ";
            }

            SqlCommand cmd = new SqlCommand("select isnull(count(distinct reid),0) from Research_Researcher where reid in (select reid from ResearchsInfo where (retype=N'بحوث علمية' or retype=N'نشاط تأليفي') and (InSupport=N'نعم' or Reward=N'نعم')" + cond+")", conn);
            SqlDataReader drR = cmd.ExecuteReader();
            drR.Read();
            int R =Convert.ToInt16( drR[0]);

            cmd = new SqlCommand("select isnull(count(distinct reid),0) from Research_Researcher where reid in (select reid from ResearchsInfo where retype=N'مؤتمر علمي' and (InSupport=N'نعم' or Reward=N'نعم') "+ cond + ")", conn);
            SqlDataReader drC = cmd.ExecuteReader();
            drC.Read();
            int C = Convert.ToInt16(drC[0]);

            //cmd = new SqlCommand("select isnull(count(distinct reid),0) from Research_Researcher where reid in (select reid from ResearchsInfo where retype=N'نشاط تأليفي' and (InSupport=N'نعم' or Reward=N'نعم') "+cond + ")", conn);
            //SqlDataReader drP = cmd.ExecuteReader();
            //drP.Read();
            //int P = Convert.ToInt16(drP[0]);

            //cmd = new SqlCommand("select isnull(count(distinct reid),0) from ResearchsInfo where reyear=" + DateTime.Now.Year, conn);
            //SqlDataReader drAll = cmd.ExecuteReader();
            //drAll.Read();
            //double all = Convert.ToInt16(drAll[0]);

            lblRSupportPer.Text = (R).ToString();
            lblCSupportPer.Text = (C).ToString();
            //lblPSupportPer.Text = (P).ToString();

            cmd = new SqlCommand("select * from SupportSetting", conn);
            DataTable dtt = new DataTable();
            dtt.Load(cmd.ExecuteReader());
            if (dtt.Rows.Count != 0)
            {
                RDiv.Style.Add("background-color", (R <= Convert.ToInt16(dtt.Rows[0][1].ToString()) ? "#6d8f28" : "#e61b23"));
                CDiv.Style.Add("background-color", (C <= Convert.ToInt16(dtt.Rows[0][2].ToString()) ? "#6d8f28" : "#e61b23"));
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
            DataTable data = new DataTable();
            data.Columns.Add("College");
            data.Columns.Add("Dept");
            data.Columns.Add("RCount");
            data.Columns.Add("CCount");
            data.Columns.Add("PCount");
            data.Columns.Add("total");
            string sql = "";
            sql = "select Distinct College,Dept From ResearcherInfo where College<>'' order by College,Dept";
            SqlCommand cmdCD = new SqlCommand(sql, conn);
            DataTable dtCD = new DataTable();
            dtCD.Load(cmdCD.ExecuteReader());
            string[] retype = { "بحوث علمية", "مؤتمر علمي", "نشاط تأليفي" };
            for (int i = 0; i < dtCD.Rows.Count; i++)
            {
                DataRow row = data.NewRow();
                row[0] = dtCD.Rows[i]["College"].ToString();
                row[1] = dtCD.Rows[i]["Dept"].ToString();
                int total = 0;
                for (int j = 0; j <= 2; j++)
                {
                    sql = "select isnull(COUNT(distinct rr1.ReId),0) cnt";
                    sql += " from Research_Researcher rr1,ResearchsInfo r1, ResearcherInfo ri";
                    sql += " where rr1.ReId = r1.ReId and rr1.RId = ri.RId";
                    sql += " and College = N'" + dtCD.Rows[i][0] + "' and Dept = N'" + dtCD.Rows[i][1] + "'";
                    sql += " and ReType = N'" + retype[j] + "'" +( Session["cond"]!=null? Session["cond"].ToString() :"");// and ReLevel = N'بحث علمي'";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    dr.Read();
                    row[j + 2] = dr[0];
                    total += Convert.ToInt16(dr[0]);
                }
                row[5] = total;// (row[2].ToString()+row[3].ToString() + row[4]);
                data.Rows.Add(row);
            }
            int t = 0, r = 0, c = 0, p = 0;
            DataTable newData = new DataTable();
            newData = data.Copy();
            int newindex = 0;
            for (int x = 1; x < data.Rows.Count; x++)
            {
                if (data.Rows[x - 1][0].ToString() != data.Rows[x][0].ToString())
                {
                    DataRow row = newData.NewRow();
                    row[0] = "المجموع الكلي للكلية";
                    row[1] = "";
                    row[2] = Convert.ToInt16(data.Rows[x - 1][2].ToString()) + r;
                    row[3] = Convert.ToInt16(data.Rows[x - 1][3].ToString()) + c;
                    row[4] = Convert.ToInt16(data.Rows[x - 1][4].ToString()) + p;
                    row[5] = Convert.ToInt16(data.Rows[x - 1][5].ToString()) + t;
                    t = r = c = p = 0;
                    newData.Rows.InsertAt(row, x+newindex);
                    newindex++;
                }
                else
                {
                    r += Convert.ToInt16(data.Rows[x - 1][2].ToString());
                    c += Convert.ToInt16(data.Rows[x - 1][3].ToString());
                    p += Convert.ToInt16(data.Rows[x - 1][4].ToString());
                    t += Convert.ToInt16(data.Rows[x - 1][5].ToString());
                }
            }
            DataRow row1 = newData.NewRow();
            row1[0] = "المجموع الكلي للكلية";
            row1[1] = "";
            row1[2] = Convert.ToInt16(data.Rows[data.Rows.Count-1][2].ToString()) + r;
            row1[3] = Convert.ToInt16(data.Rows[data.Rows.Count - 1][3].ToString()) + c;
            row1[4] = Convert.ToInt16(data.Rows[data.Rows.Count - 1][4].ToString()) + p;
            row1[5] = Convert.ToInt16(data.Rows[data.Rows.Count - 1][5].ToString()) + t;
            t = r = c = p = 0;
            newData.Rows.InsertAt(row1, data.Rows.Count + newindex);

            Session["data"] = newData;
                //Label4.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");

                GridView1.DataSource = Session["data"];
                GridView1.DataBind();
                msgDiv.Visible = false;
                dataDiv.Visible = true;

            //}
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


        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType==DataControlRowType.DataRow)
            {
                if(e.Row.Cells[0].Text== "المجموع الكلي للكلية")
                {
                    e.Row.BackColor = Color.White; //Color.FromArgb(163, 20, 20);
                    e.Row.ForeColor = Color.Black;
                    e.Row.Cells[0].ColumnSpan = 2;
                    e.Row.Cells[1].Visible = false;
                }
            }
        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            for (int rowIndex = GridView1.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow gvRow = GridView1.Rows[rowIndex];
                GridViewRow gvPreviousRow = GridView1.Rows[rowIndex + 1];
                for (int cellCount = 0; cellCount < 1; cellCount++)
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
            if ((sender as Control).ID.ToLower() != "btnall")
            {
                int mf = Convert.ToInt16(ddlFromMonth.SelectedValue);
                int y1 = Convert.ToInt16(ddlFromYear.SelectedValue);
                int mt = Convert.ToInt16(ddlToMonth.SelectedValue);
                int y2 = Convert.ToInt16(ddlToYear.SelectedValue);
                //lblPeriod.Text = "01/" + mf.ToString("00") + "/" + y1 + " - " + DateTime.DaysInMonth(y2, mt) + "/ " + mt.ToString("00") + "/" + y2;
                int y = DateTime.Now.Year;
                int m = DateTime.Now.Month;

                string cond = "";
                if (y1 == y2)
                    cond = " and reyear=" + y1 + " and remonth between " + mf + " and " + (mt);
                else
                {
                    cond = " and ((reyear=" + (y1) + " and remonth between " + mf + " and 12) or (reyear=" + y2 + " and remonth between 1 and " + (mt) + ")) ";
                }
                Session["cond"] = cond;
            }
            else
                Session["cond"] = null;
            getData();
        }
    }
}