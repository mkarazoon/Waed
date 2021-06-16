using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchAcademicUnit
{
    public partial class UploadData : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["logSession"] == null || Session["userid"] == null || Session["userrole"].ToString() != "1")
            {
                Response.Redirect("Index.aspx");
            }
            Session["backurl"] = "Index.aspx";

            Label lblUserVal = (Label)Page.Master.FindControl("lblPageName");
            lblUserVal.Text = "تحميل البيانات";

            if(!IsPostBack)
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand cmdG = new SqlCommand("select isnull(numberofr,0) r,isnull(numberofc,0) c,isnull(numberofp,0) p from SupportSetting", conn);
                DataTable dt = new DataTable();
                dt.Load(cmdG.ExecuteReader());
                if (dt.Rows.Count != 0)
                {
                    dt.Load(cmdG.ExecuteReader());
                    txtR.Text = dt.Rows[0][0].ToString();
                    txtC.Text = dt.Rows[0][1].ToString();
                    //txtP.Text = dt.Rows[0][2].ToString();
                }

                for(int i=2007;i<=DateTime.Now.Year+10;i++)
                {
                    ddlFromYear.Items.Add(i.ToString());
                    ddlFromYear.Items[i - 2007].Value = i.ToString();
                    ddlToYear.Items.Add(i.ToString());
                    ddlToYear.Items[i - 2007].Value = i.ToString();
                }

                cmdG = new SqlCommand("select * from YearSetting", conn);
                DataTable ddd = new DataTable();
                ddd.Load(cmdG.ExecuteReader());
                if (ddd.Rows.Count != 0)
                {
                    ddlFromMonth.Items[Convert.ToInt16(ddd.Rows[0][1]) - 1].Selected = true;
                    ddlFromYear.Items[Convert.ToInt16(ddd.Rows[0][2]) - 2007].Selected = true;
                    ddlToMonth.Items[Convert.ToInt16(ddd.Rows[0][3]) - 1].Selected = true;
                    ddlToYear.Items[Convert.ToInt16(ddd.Rows[0][4]) - 2007].Selected = true;
                }
                else
                {
                    ddlFromYear.Items[DateTime.Now.Year - 2007].Selected = true;
                    ddlToYear.Items[DateTime.Now.Year - 2007+1].Selected = true;
                }

                cmdG = new SqlCommand("select Acdid,RAName From ResearcherInfo where RStatus='IN'", conn);
                ListBox1.DataSource = cmdG.ExecuteReader(); 
                ListBox1.DataTextField= "RAName";
                ListBox1.DataValueField = "AcdId";
                ListBox1.DataBind();
                conn.Close();
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            //new method

            //CompareResearchs();

            //end new method

            //old method to upload everything 
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand cmd1 = new SqlCommand("delete From ResearcherInfo", conn);
            cmd1.ExecuteNonQuery();
            cmd1 = new SqlCommand("delete From ResearchsInfo", conn);
            cmd1.ExecuteNonQuery();
            cmd1 = new SqlCommand("delete from log", conn);
            cmd1.ExecuteNonQuery();

            bool FirstSheet = GetReasearchInfoFromExcel();
            bool SecondSheet = GetResearchsFromInfoFromExcel();
            if (FirstSheet && SecondSheet)
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                cmd1 = new SqlCommand("select * from Log", conn);
                GridView1.DataSource = cmd1.ExecuteReader();
                GridView1.DataBind();
                if (GridView1.Rows.Count == 0)
                {
                    abstractDiv.Visible = false;
                    System.Web.UI.ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('تم تحميل البيانات بنجاح');", true);
                }
                else
                {
                    abstractDiv.Visible = true;
                    System.Web.UI.ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('لم يتم تحميل كامل البيانات، أنظر للتفاصيل');", true);
                }
            }
        }

        public bool CompareResearchs()
        {
            if (FileUpload1.HasFile)
            {
                string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                string fileLocation = Server.MapPath("Docs/" + fileName);

                if (File.Exists(fileLocation))
                {
                    Random n = new Random();
                    fileLocation = fileLocation + n.Next();
                }

                FileUpload1.SaveAs(fileLocation);

                string conStr = "";
                if (fileExtension == ".xlsx")
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('The file type should be Excel 97-2003 (.xls)');", true);
                }
                else if (fileExtension == ".xls")
                {
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                    conStr = String.Format(conStr, fileLocation, "Yes");
                    OleDbConnection connExcel = new OleDbConnection(conStr);
                    OleDbCommand cmdExcel = new OleDbCommand();
                    OleDbDataAdapter oda = new OleDbDataAdapter();
                    DataTable dt = new DataTable();
                    cmdExcel.Connection = connExcel;

                    //Get the name of First Sheet
                    try
                    {
                        connExcel.Close();
                    }
                    catch { }

                    connExcel.Open();
                    DataTable dtExcelSchema;
                    dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    String[] excelSheetNames = new String[dtExcelSchema.Rows.Count];
                    string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                    connExcel.Close();

                    connExcel.Open();
                    cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                    oda.SelectCommand = cmdExcel;
                    IDataReader excelDr = cmdExcel.ExecuteReader();
                    int x = excelDr.FieldCount;

                    if (x == 7)
                    {
                        if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
                            conn.Open();
                                SqlCommand cmdDTemp = new SqlCommand("Delete From RTemp", conn);
                                cmdDTemp.ExecuteNonQuery();

                        SqlBulkCopy bulkCopy = new SqlBulkCopy(conn);
                        bulkCopy.DestinationTableName = "RTemp";
                        bulkCopy.WriteToServer(excelDr);
                        bulkCopy.Close();

                        //try
                        //{
                        //string sql = "";
                        //    while (excelDr.Read())
                        //    {


                        //    //sql.Append("insert into RTemp values(N'" + excelDr[0].ToString().Trim() + "',N'" + excelDr[2].ToString().Trim() + "')");
                        //    sql="insert into RTemp values(@RT,@EID)";
                        //    SqlCommand cmd = new SqlCommand(sql, conn);
                        //    cmd.Parameters.AddWithValue("@RT", excelDr[0].ToString().Trim());
                        //    cmd.Parameters.AddWithValue("@EID", excelDr[2].ToString().Trim());
                        //    cmd.ExecuteNonQuery();
                        //}
                        //sql = sql.Replace("\"", "\\\"");
                        //SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
                        //cmd.Parameters.AddWithValue("@RT", excelDr[0].ToString().Trim());
                        //cmd.Parameters.AddWithValue("@EID", excelDr[2].ToString().Trim());
                        //cmd.ExecuteNonQuery();
                        //}
                        //catch
                        //{
                        //}
                        connExcel.Close();
                        conn.Close();
                    }
                    else
                    {
                        System.Web.UI.ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('خطأ في تحميل الملف: عدد الأعمدة أكثر/أقل من 7 ');", true);
                        return false;
                    }
                    conn.Close();
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool GetReasearchInfoFromExcel()
        {
            if (FileUpload1.HasFile)
            {
                string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                string fileLocation = Server.MapPath("Docs/" + fileName);

                if (File.Exists(fileLocation))
                {
                    Random n = new Random();
                    fileLocation = fileLocation + n.Next();
                }

                FileUpload1.SaveAs(fileLocation);

                string conStr = "";
                if (fileExtension == ".xlsx")
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('The file type should be Excel 97-2003 (.xls)');", true);
                }
                else if (fileExtension == ".xls")
                {
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                    conStr = String.Format(conStr, fileLocation, "Yes");
                    OleDbConnection connExcel = new OleDbConnection(conStr);
                    OleDbCommand cmdExcel = new OleDbCommand();
                    OleDbDataAdapter oda = new OleDbDataAdapter();
                    DataTable dt = new DataTable();
                    cmdExcel.Connection = connExcel;

                    //Get the name of First Sheet
                    try
                    {
                        connExcel.Close();
                    }
                    catch { }

                    connExcel.Open();
                    DataTable dtExcelSchema;
                    dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    String[] excelSheetNames = new String[dtExcelSchema.Rows.Count];
                    string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                    connExcel.Close();

                    //Read Data from First Sheetc:\users\ta\documents\visual studio 2010\Projects\excel\excel\Global.asax
                    connExcel.Open();
                    cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                    oda.SelectCommand = cmdExcel;
                    IDataReader excelDr = cmdExcel.ExecuteReader();
                    int x = excelDr.FieldCount;

                    if (x == 17)
                    {
                        if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
                            conn.Open();
                        int cnt = 1;
                        while (excelDr.Read())
                        {
                            if (excelDr[0].ToString().Trim() != "0")
                            {
                                try
                                {
                                    if (cnt == 4)
                                    {
                                        string sql = "insert into ResearcherInfo values(N'";
                                        sql += excelDr[0].ToString().Trim() + "',@ENAME,@ANAME,N'";
                                        sql += excelDr[3].ToString().Trim() + "',N'";
                                        sql += excelDr[4].ToString().Trim() + "',N'";
                                        sql += excelDr[5].ToString().Trim() + "',N'";
                                        sql += excelDr[6].ToString().Trim() + "',";
                                        sql += "@da,";
                                        sql += excelDr[8].ToString().Trim() + ",N'";
                                        sql += excelDr[9].ToString().Trim() + "',";
                                        sql += excelDr[10].ToString().Trim() + ",N'";
                                        sql += excelDr[11].ToString().Trim() + "',";
                                        sql += (excelDr[12].ToString().Trim() == "" ? "0" : excelDr[12].ToString().Trim()) + ",";
                                        sql += (excelDr[13].ToString().Trim() == "" ? "0" : excelDr[13].ToString().Trim()) + ",";
                                        sql += (excelDr[14].ToString().Trim() == "" ? "0" : excelDr[14].ToString().Trim()) + ",N'";
                                        sql += (excelDr[15].ToString().Trim() == "" ? "0" : excelDr[15].ToString().Trim()) + "',N'";
                                        sql += (excelDr[16].ToString().Trim() == "" ? "0" : excelDr[16].ToString().Trim()) + "')";
                                        SqlCommand cmd = new SqlCommand(sql, conn);
                                        cmd.Parameters.AddWithValue("@da", (excelDr[7].ToString().Trim() != "" ? DateTime.Parse(excelDr[7].ToString().Trim()) : DateTime.Parse("1-1-1900")));
                                        cmd.Parameters.AddWithValue("@ENAME", excelDr[1].ToString().Trim());
                                        cmd.Parameters.AddWithValue("@ANAME", excelDr[2].ToString().Trim());
                                        cmd.ExecuteNonQuery();
                                        if (Convert.ToInt16(excelDr[8].ToString().Trim()) != 0)
                                        {
                                            try
                                            {
                                                cmd = new SqlCommand("insert into users values(" + excelDr[8].ToString().Trim() + ",'" + excelDr[8].ToString().Trim() + "',N'6')", conn);
                                                cmd.ExecuteNonQuery();
                                            }
                                            catch { }
                                        }
                                        else if (excelDr[15].ToString().Trim() == "IN")
                                        {
                                            cmd = new SqlCommand("Insert into Log values(N'" + excelDr[0].ToString().Trim() + "',N'لم يتم إنشاء مستخدم للباحث ذو الرمز " + excelDr[0].ToString().Trim() + " لأن رقمه الوظيفي 0')", conn);
                                            cmd.ExecuteNonQuery();

                                        }
                                    }
                                    else
                                        cnt++;
                                }
                                catch
                                {
                                    SqlCommand cmd = new SqlCommand("Insert into Log values(N'" + excelDr[0].ToString().Trim() + "',N'لم يتم تحميل معلومات الباحث ذو الرمز " + excelDr[0].ToString().Trim() + "')", conn);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        connExcel.Close();
                        conn.Close();
                    }
                    else
                    {
                        System.Web.UI.ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('خطأ في تحميل الملف: عدد الأعمدة أكثر من 16 في صفحة الباحثين');", true);
                        return false;
                    }
                    conn.Close();
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool GetResearchsFromInfoFromExcel()
        {
            string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            string fileLocation = Server.MapPath("Docs/" + fileName);

            if (File.Exists(fileLocation))
            {
                Random n = new Random();
                fileLocation = fileLocation + n.Next();
            }

            FileUpload1.SaveAs(fileLocation);

            string conStr = "";
            conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
            conStr = String.Format(conStr, fileLocation, "Yes");
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            try
            {
                connExcel.Close();
            }
            catch { }

            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[1]["TABLE_NAME"].ToString();
            connExcel.Close();

            //Read Data from First Sheetc:\users\ta\documents\visual studio 2010\Projects\excel\excel\Global.asax
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            IDataReader excelDr = cmdExcel.ExecuteReader();
            int x = excelDr.FieldCount;

            if (x >= 30)
            {
                if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
                    conn.Open();
                int cnt = 1;
                while (excelDr.Read())
                {
                    string dddd = excelDr[0].ToString();
                    if (excelDr[0].ToString().Trim() != "")
                    {
                        try
                        {
                            if (cnt == 1)
                            {
                                string sql = "insert into ResearchsInfo values(N'";
                                sql += excelDr[0].ToString().Trim() + "',";
                                sql += "@RT,";
                                sql += "@RAbst,N'";
                                sql += excelDr[3].ToString().Trim() + "',N'";
                                sql += excelDr[4].ToString().Trim() + "',";
                                sql += excelDr[5].ToString().Trim() + ",";
                                sql += excelDr[6].ToString().Trim() + ",N'";
                                sql += excelDr[7].ToString().Trim() + "',";
                                sql += excelDr[8].ToString().Trim() + ",N'";
                                sql += excelDr[9].ToString().Trim() + "',N'";
                                sql += excelDr[10].ToString().Trim() + "',";
                                sql += "@affAuther,N'";
                                sql += excelDr[12].ToString().Trim() + "',N'";
                                sql += excelDr[13].ToString().Trim() + "',";
                                sql += "@MG,N'";
                                sql += excelDr[15].ToString().Trim() + "',N'";
                                sql += excelDr[16].ToString().Trim() + "',";
                                sql += "@PB,N'";
                                sql += excelDr[18].ToString().Trim() + "',N'";
                                sql += excelDr[19].ToString().Trim() + "',N'";
                                sql += excelDr[20].ToString().Trim() + "',N'";
                                sql += excelDr[21].ToString().Trim() + "',N'";
                                sql += excelDr[22].ToString().Trim() + "',N'";
                                sql += excelDr[23].ToString().Trim() + "',N'";
                                sql += excelDr[25].ToString().Trim() + "',N'";
                                sql += excelDr[27].ToString().Trim() + "',";
                                sql += excelDr[28].ToString().Trim() + ",";
                                sql += excelDr[29].ToString().Trim() + ",@MC)";

                                SqlCommand cmd = new SqlCommand(sql, conn);
                                cmd.Parameters.AddWithValue("@RT", excelDr[1].ToString().Trim());
                                cmd.Parameters.AddWithValue("@RAbst", excelDr[2].ToString().Trim());
                                cmd.Parameters.AddWithValue("@affAuther", excelDr[11].ToString().Trim());
                                cmd.Parameters.AddWithValue("@MG", excelDr[14].ToString().Trim());
                                cmd.Parameters.AddWithValue("@PB", excelDr[17].ToString().Trim());
                                switch (excelDr[18].ToString().Trim())
                                {
                                    case "الربع الأول":
                                        cmd.Parameters.AddWithValue("@MC", 1);
                                        break;
                                    case "الربع الثاني":
                                        cmd.Parameters.AddWithValue("@MC", 2);
                                        break;
                                    case "الربع الثالث":
                                        cmd.Parameters.AddWithValue("@MC", 3);
                                        break;
                                    case "الربع الرابع":
                                        cmd.Parameters.AddWithValue("@MC", 4);
                                        break;
                                    case "خارج التصنيف":
                                        cmd.Parameters.AddWithValue("@MC", 5);
                                        break;
                                    case "غير متاح":
                                        cmd.Parameters.AddWithValue("@MC", 6);
                                        break;
                                }
                                try
                                {
                                    cmd.ExecuteNonQuery();
                                }
                                catch
                                {
                                    cmd = new SqlCommand("Insert into Log values(N'" + excelDr[0].ToString().Trim() + "',N'لم يتم تحميل معلومات البحث - أحد الأسباب أنه مكرر')", conn);
                                    cmd.ExecuteNonQuery();
                                }

                                if (excelDr[23].ToString().Trim() != "لا")
                                {
                                    try
                                    {
                                        sql = "insert into Reward_Support values('" +
                                            excelDr[24].ToString().Trim() + "',1,'"+ excelDr[0].ToString().Trim() + "')";
                                        cmd = new SqlCommand(sql, conn);
                                        cmd.ExecuteNonQuery();
                                    }
                                    catch
                                    {
                                        cmd = new SqlCommand("Insert into Log values(N'" + excelDr[0].ToString().Trim() + "',N'لم يتم ربط معلومات الدعم للباحث رقم " + excelDr[26].ToString().Trim() + " مع البحث ذو الرمز " + excelDr[0].ToString().Trim() + "')", conn);
                                        cmd.ExecuteNonQuery();
                                    }
                                }

                                if (excelDr[25].ToString().Trim() != "لا")
                                {
                                    try
                                    {
                                        sql = "insert into Reward_Support values('" +
                                            excelDr[26].ToString().Trim() + "',2,'" + excelDr[0].ToString().Trim() + "')";
                                        cmd = new SqlCommand(sql, conn);
                                        cmd.ExecuteNonQuery();
                                    }
                                    catch
                                    {
                                        cmd = new SqlCommand("Insert into Log values(N'" + excelDr[0].ToString().Trim() + "',N'لم يتم ربط معلومات المكافأة للباحث رقم " + excelDr[26].ToString().Trim() + " مع البحث ذو الرمز " + excelDr[0].ToString().Trim() + "')", conn);
                                        cmd.ExecuteNonQuery();
                                    }

                                }

                                for (int i = 30; i < excelDr.FieldCount; i++)
                                {
                                    string xx = excelDr[i].ToString().Trim();
                                    if (excelDr[i].ToString().Trim() != "" && excelDr[i].ToString().Trim() != "0")
                                    {
                                        try
                                        {
                                            sql = "insert into Research_Researcher values('" +
                                                excelDr[0].ToString().Trim() + "','" +
                                                excelDr[i].ToString().Trim() + "')";
                                            cmd = new SqlCommand(sql, conn);
                                            cmd.ExecuteNonQuery();
                                        }
                                        catch
                                        {
                                            //if(excelDr[0].ToString().Trim()=="RE5")
                                            //{
                                            //    string dd = "";
                                            //}
                                            cmd = new SqlCommand("Insert into Log values(N'" + excelDr[0].ToString().Trim() + "',N'لم يتم ربط معلومات الباحث ذو الرمز " + excelDr[i].ToString().Trim() + " بالبحث ذو الرمز " + excelDr[0].ToString().Trim() + "')", conn);
                                            cmd.ExecuteNonQuery();
                                        }

                                    }
                                }
                            }
                            else
                                cnt++;
                        }
                        catch
                        {
                            SqlCommand cmd = new SqlCommand("Insert into Log values(N'" + excelDr[0].ToString().Trim() + "',N'لم يتم تحميل معلومات البحث - أحد الأسباب أنه مكرر')", conn);
                            cmd.ExecuteNonQuery();

                        }
                    }
                }
                connExcel.Close();
                conn.Close();
            }
            else
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('خطأ: عدد الأعمدة أقل من 30 عمود');", true);
                return false;
            }
            conn.Close();
            return true;
        }

        protected void btnUploadSetting_Click(object sender, EventArgs e)
        {
            
            if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
                conn.Open();


            SqlCommand cmd1 = new SqlCommand("delete From SupportSetting", conn);
            cmd1.ExecuteNonQuery();
            if (txtR.Text != "" && txtC.Text != "")
            {
                try
                {
                    string sql = "insert into SupportSetting values(";
                    sql += txtR.Text + "," + txtC.Text + ",0)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                }
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('تم تحميل البيانات بنجاح');", true);
            }
            conn.Close();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
                conn.Open();


            SqlCommand cmd1 = new SqlCommand("delete From YearSetting", conn);
            cmd1.ExecuteNonQuery();
                try
                {
                    string sql = "insert into YearSetting values(";
                    sql += ddlFromMonth.SelectedValue + "," + ddlFromYear.SelectedValue + "," +
                           ddlToMonth.SelectedValue+"," + ddlToYear.SelectedValue +")";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                }
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('تم تحميل البيانات بنجاح');", true);
            conn.Close();
        }

        protected void ddlAuthLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlAuthLevel.SelectedValue!="0")
            {
                try
                {


                    if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
                        conn.Open();
                    for (int j = 0; j < ListBox1.Items.Count; j++)
                        ListBox1.Items[j].Selected = false;

                    SqlCommand cmd = new SqlCommand("Select AcdId From Users where userType='" + ddlAuthLevel.SelectedValue +"'", conn);
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    //SqlDataReader dr = cmd.ExecuteReader();
                    //dr.Read();
                    for (int i = 0; i < dt.Rows.Count; i++)
                        for (int j = 0; j < ListBox1.Items.Count; j++)
                            if (ListBox1.Items[j].Value == dt.Rows[i][0].ToString())
                            {
                                ListBox1.Items[j].Selected = true;
                                break;
                            }

                    conn.Close();
                }
                catch
                { }
            }
        }

        protected void btnSaveAuth_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("update Users set usertype='6' where usertype='" + ddlAuthLevel.SelectedValue + "'", conn);
                cmd.ExecuteNonQuery();

                for (int i = 0; i < ListBox1.Items.Count; i++)
                    if (ListBox1.Items[i].Selected == true)
                    {
                        cmd = new SqlCommand("update Users set usertype='"+ ddlAuthLevel.SelectedValue + "' where acdid=" + ListBox1.Items[i].Value, conn);
                        cmd.ExecuteNonQuery();
                    }
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('تم تحميل البيانات بنجاح');", true);
            }
            catch { }
        }

        protected void btnOldYearSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
                    conn.Open();
                SqlCommand cmd1 = new SqlCommand("delete From OldYears", conn);
                cmd1.ExecuteNonQuery();

                SqlCommand cmd = new SqlCommand("insert into OldYears values(@FYear,0,@FInst)", conn);
                cmd.Parameters.AddWithValue("@FYear", txtFirstYear.Text);
                //cmd.Parameters.AddWithValue("@FPub", txtFPub.Text);
                cmd.Parameters.AddWithValue("@FInst", txtFirstInst.Text);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("insert into OldYears values(@SYear,0,@SInst)", conn);
                cmd.Parameters.AddWithValue("@SYear", txtSecondYear.Text);
                //cmd.Parameters.AddWithValue("@SPub", txtSPub.Text);
                cmd.Parameters.AddWithValue("@SInst", txtSecondInst.Text);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("insert into OldYears values('',0,@SInst)", conn);
                //cmd.Parameters.AddWithValue("@CYear", "");
                //cmd.Parameters.AddWithValue("@SPub", txtSPub.Text);
                cmd.Parameters.AddWithValue("@SInst", txtCInst.Text);
                cmd.ExecuteNonQuery();

                System.Web.UI.ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('تم التخزين بنجاح');", true);
                conn.Close();
            }

            catch { }
        }
    }
}