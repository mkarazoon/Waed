using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ResearchAcademicUnit
{
    public partial class Com_SupervisorAdoptedReport : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        static string connstring1 = System.Configuration.ConfigurationManager.ConnectionStrings["MEUCV"].ConnectionString;
        SqlConnection conn1 = new SqlConnection(connstring1);

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

                SqlCommand cmd = new SqlCommand("Select AutoId,CollegeName from Faculty", conn);
                ddlFaculty.DataSource = cmd.ExecuteReader();
                ddlFaculty.DataTextField = "CollegeName";
                ddlFaculty.DataValueField = "AutoId";
                ddlFaculty.DataBind();
                ddlFaculty.Items.Insert(0, "اختيار");
                ddlFaculty.Items[0].Value = "0";

                //cmd = new SqlCommand("Select * from Com_OuterSupervisorInfo", conn);
                //ddlOutName.DataSource = cmd.ExecuteReader();
                //ddlOutName.DataTextField = "RName";
                //ddlOutName.DataValueField = "AutoId";
                //ddlOutName.DataBind();
                //ddlOutName.Items.Insert(0,"اختيار");
                //ddlOutName.Items[0].Value = "0";

                conn.Close();
            }
        }

        //protected void btnAdopted_Click(object sender, EventArgs e)
        //{
        //    bool adopted = true;
        //    if (chkAdopted.SelectedIndex == -1)
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('يجب تحديد نوع الإعتماد','error');", true);
        //        adopted = false;
        //    }
        //    else
        //    {
        //        string sql = "";
        //        if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
        //            conn.Open();
        //        if (chkAdopted.Items[0].Selected)
        //            sql += "Insert into Adopted_Supervisor values('" + (rdType.SelectedValue == "IN" ? ddlRName.SelectedValue : ddlOutName.SelectedValue) + "',1,@d,'" + rdType.SelectedValue + "')";
        //        if (chkAdopted.Items[1].Selected)
        //            sql += "Insert into Adopted_Supervisor values('" + (rdType.SelectedValue == "IN" ? ddlRName.SelectedValue : ddlOutName.SelectedValue) + "',2,@d,'" + rdType.SelectedValue + "')";
        //        if (chkAdopted.Items[2].Selected)
        //            sql += "Insert into Adopted_Supervisor values('" + (rdType.SelectedValue == "IN" ? ddlRName.SelectedValue : ddlOutName.SelectedValue) + "',3,@d,'" + rdType.SelectedValue + "')";



        //        SqlCommand cmdDel = new SqlCommand("Delete From Adopted_Supervisor where JobId='" + (rdType.SelectedValue == "IN" ? ddlRName.SelectedValue : ddlOutName.SelectedValue) + "'", conn);
        //        cmdDel.ExecuteNonQuery();

        //        SqlCommand cmd = new SqlCommand(sql, conn);
        //        cmd.Parameters.AddWithValue("@d", DateTime.Now);
        //        cmd.ExecuteNonQuery();
        //        if (rdType.SelectedValue == "OUT")
        //        {
        //            cmd = new SqlCommand("Update Com_OuterSupervisorInfo set RStatus=1 where AutoId=" + ddlOutName.SelectedValue, conn);
        //            cmd.ExecuteNonQuery();
        //        }

        //        Timer1.Enabled = true;
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('تم ادخال معلومات الباحث بنجاح','success');", true);
        //    }
        //    conn.Close();
        //}

//        protected void btnNotAdopted_Click(object sender, EventArgs e)
//        {
//            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
//                conn.Open();
//             string   sql = "Insert into Adopted_Supervisor values('" + ddlRName.SelectedValue + "',0,@d,'IN')";

//            SqlCommand cmdDel = new SqlCommand("Delete From Adopted_Supervisor where JobId='" + ddlRName.SelectedValue+"'", conn);
//            cmdDel.ExecuteNonQuery();



//            SqlCommand cmd = new SqlCommand(sql, conn);
//            cmd.Parameters.AddWithValue("@d", DateTime.Now);
//            cmd.ExecuteNonQuery();

//            if (rdType.SelectedValue == "OUT")
//            {
//                cmd = new SqlCommand("Update Com_OuterSupervisorInfo set RStatus=1 where AutoId=" + ddlOutName.SelectedValue, conn);
//                cmd.ExecuteNonQuery();
//            }


//            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('لم يتم اعتماد الباحث','success');", true);
//        }

//        protected void lnkDelete_Click(object sender, EventArgs e)
//        {
//            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
//            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
//                conn.Open();

//            SqlCommand cmdDel = new SqlCommand("Delete From Adopted_Supervisor where JobId='" + row.Cells[6].Text + "' and Place='" + row.Cells[7].Text + "'", conn);
//            cmdDel.ExecuteNonQuery();

//            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('تم إلغاء اعتماد الباحث','success');", true);
//        }

//        protected void lnkTab1_Click(object sender, EventArgs e)
//        {
//            tab1.Visible = true;
//            tab2.Visible = false;
//            lnkTab1.Attributes.Add("class", "nav-link active");
//            lnkTab2.Attributes.Add("class", "nav-link");
//        }

//        protected void lnkTab2_Click(object sender, EventArgs e)
//        {
//            tab2.Visible = true;
//            tab1.Visible = false;
//            lnkTab2.Attributes.Add("class", "nav-link active");
//            lnkTab1.Attributes.Add("class", "nav-link");

//            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
//                conn.Open();
//            SqlCommand cmdGet = new SqlCommand("Select * from Adopted_Supervisor", conn);
//            DataTable dtAdoptedStatus = new DataTable();
//            dtAdoptedStatus.Load(cmdGet.ExecuteReader());
//            if (dtAdoptedStatus.Rows.Count != 0)
//            {
//                string s = string.Join(", ", dtAdoptedStatus.Rows.OfType<DataRow>().Select(r => r[1].ToString()));
//                if (conn1.State == ConnectionState.Broken || conn1.State == ConnectionState.Closed)
//                    conn1.Open();

//                //SqlCommand cmd = new SqlCommand("SELECT InstJobId,AName,b.CollegeName,c.DeptName,Minor FROM InstInfo a,College b,Department c where a.College=b.AutoId and a.Dept=c.AutoId and InstJobId in (" + s + ")", conn1);
//                SqlCommand cmd = new SqlCommand(@"SELECT distinct [jobId],b.RaName,b.College,b.Dept,[AdoptedDate],Place,
//STUFF((SELECT ', ' +cast( t2.Status as nvarchar)
//          FROM Adopted_Supervisor t2
//          where a.jobId = t2.jobId
//          FOR XML PATH (''))
//          , 1, 1, '')  AS Status FROM Adopted_Supervisor a,ResearcherInfo b where a.jobid=b.AcdId
//        union all

//SELECT distinct [jobId],b.RName RaName,b.RFaculty College,b.Rdept Dept,[AdoptedDate],Place,
//STUFF((SELECT ', ' +cast( t2.Status as nvarchar)
//          FROM Adopted_Supervisor t2
//          where a.jobId = t2.jobId
//          FOR XML PATH (''))
//          , 1, 1, '')  AS Status FROM Adopted_Supervisor a,Com_OuterSupervisorInfo b where a.jobid=b.AutoId", conn);
//                DataTable dt = new DataTable();
//                dt.Load(cmd.ExecuteReader());
//                GridView2.DataSource = dt;
//                GridView2.DataBind();
//                //if (dt.Rows.Count != 0)
//                //{
//                //    switch (dt.Rows[0]["Degree"].ToString())
//                //    {
//                //        case "1":
//                //            lblDegree.Text = "استاذ";
//                //            break;
//                //        case "2":
//                //            lblDegree.Text = "استاذ مشارك";
//                //            break;
//                //        case "3":
//                //            lblDegree.Text = "استاذ مساعد";
//                //            break;
//                //        case "4":
//                //            lblDegree.Text = "مدرس";
//                //            break;
//                //    }
//                //    lblMinor.Text = dt.Rows[0]["Minor"].ToString();
//                //}
//                conn1.Close();
//            }
//            conn.Close();

//        }

        protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFaculty.SelectedValue != "0")
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("Select distinct AutoId,DeptName from Department where CAutoid="+ddlFaculty.SelectedValue, conn);
                ddlDept.DataSource = cmd.ExecuteReader();
                ddlDept.DataTextField = "DeptName";
                ddlDept.DataValueField = "AutoId";
                ddlDept.DataBind();
                ddlDept.Items.Insert(0, "اختيار");
                ddlDept.Items[0].Value = "0";
                GridView1.DataSource = "";
                GridView1.DataBind();
                conn.Close();
                ddlRName.Items.Clear();
                lblDegree.Text = "";
                lblMinor.Text = "";
            }
        }

        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDept.SelectedValue != "0")
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("Select AcdId,RaName from ResearcherInfo where RStatus='IN' and College=N'" + ddlFaculty.SelectedItem.Text + "' and dept=N'" + ddlDept.SelectedItem.Text + "'", conn);
                ddlRName.DataSource = cmd.ExecuteReader();
                ddlRName.DataTextField = "RaName";
                ddlRName.DataValueField = "AcdId";
                ddlRName.DataBind();
                ddlRName.Items.Insert(0, "اختيار");
                ddlRName.Items[0].Value = "0";
                GridView1.DataSource = "";
                GridView1.DataBind();
                lblDegree.Text = "";
                lblMinor.Text = "";

                conn.Close();
            }
        }

        protected void ddlRName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRName.SelectedValue != "0")
            {
                if (conn1.State == ConnectionState.Broken || conn1.State == ConnectionState.Closed)
                    conn1.Open();

                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("Select * from InstInfo where InstJobId=" + ddlRName.SelectedValue , conn1);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count != 0)
                {
                    switch (dt.Rows[0]["Degree"].ToString())
                    {
                        case "1":
                            lblDegree.Text = "استاذ";
                            break;
                        case "2":
                            lblDegree.Text = "استاذ مشارك";
                            break;
                        case "3":
                            lblDegree.Text = "استاذ مساعد";
                            break;
                        case "4":
                            lblDegree.Text = "مدرس";
                            break;
                    }
                    lblMinor.Text = dt.Rows[0]["Minor"].ToString();
                }

                cmd = new SqlCommand(@"select JobId,rtitle,magname,DBTypeS,pyear,pmonth,ROrderS,
                                       (case when DBTypeI=1 then JorDB else GlobalDBS end) globaldbi,cast(AutoId as nvarchar) ReId,'' RStatus,filepath
                                       from Research where jobid=" + ddlRName.SelectedValue, conn1);
                DataTable dtResearch = new DataTable();
                dtResearch.Load(cmd.ExecuteReader());

                cmd = new SqlCommand(@"SELECT AcdId jobid,ReTitle rtitle,c.Magazine magname,N'مجلة علمية محكمة مصنفة عالميا' DBTypeS,c.ReYear pyear,c.ReMonth pmonth,
                                        c.ReParticipate ROrderS,'SCOPUS' globaldbi,a.ReId,'' RStatus,filepath
                                          FROM Research_Researcher a,ResearcherInfo b,ResearchsInfo c
                                          where a.RId=b.RId and a.ReId=c.ReId and AcdId=" + ddlRName.SelectedValue, conn);
                dtResearch.Load(cmd.ExecuteReader());
                dtResearch.DefaultView.Sort = "rtitle";
                GridView1.DataSource = dtResearch;
                GridView1.DataBind();
                if (GridView1.Rows.Count != 0)
                    AdoptedDiv.Visible = true;

                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                cmd = new SqlCommand("SELECT * FROM Adopted_Supervisor where jobid=" + ddlRName.SelectedValue, conn);
                DataTable dtAdopted = new DataTable();
                dtAdopted.Load(cmd.ExecuteReader());
                for(int i=0;i<dtAdopted.Rows.Count;i++)
                {
                    if (dtAdopted.Rows[i][2].ToString() == "1")
                        chkAdopted.Items[0].Selected = true;
                    if (dtAdopted.Rows[i][2].ToString() == "2")
                        chkAdopted.Items[1].Selected = true;
                    if (dtAdopted.Rows[i][2].ToString() == "3")
                        chkAdopted.Items[2].Selected = true;
                }
                conn.Close();
            }
        }

        //protected void GridView2_DataBound(object sender, EventArgs e)
        //{
        //    for (int i = 0; i < GridView2.Rows.Count; i++)
        //    {
        //        string[] newStatus = GridView2.Rows[i].Cells[3].Text.Split(',');
        //        string st = "";
        //        for (int j = 0;j< newStatus.Length; j++)
        //        {
        //            if (newStatus[j].ToString().Trim() == "0")
        //            {
        //                st+= "غير معتمد";
        //                //GridView2.Rows[i].Cells[3].Text = "غير معتمد";
        //                //GridView2.Rows[i].Cells[3].BackColor = Color.Red;
        //                //GridView2.Rows[i].Cells[3].ForeColor = Color.White;
        //            }
        //            else if (newStatus[j].ToString().Trim() == "1")
        //            {
        //                st+= "مناقش";
        //                GridView2.Rows[i].Cells[3].Text = "مناقش";
        //                GridView2.Rows[i].Cells[3].BackColor = Color.Green;
        //                GridView2.Rows[i].Cells[3].ForeColor = Color.White;
        //            }
        //            else if (newStatus[j].ToString().Trim() == "2")
        //            {
        //                st += "مشرف";
        //                GridView2.Rows[i].Cells[3].Text = "مشرف";
        //                GridView2.Rows[i].Cells[3].BackColor = Color.Green;
        //                GridView2.Rows[i].Cells[3].ForeColor = Color.White;
        //            }
        //            else if (newStatus[j].ToString().Trim() == "3")
        //            {
        //                st += "مدرس";
        //                GridView2.Rows[i].Cells[3].Text = "مدرس";
        //                GridView2.Rows[i].Cells[3].BackColor = Color.Green;
        //                GridView2.Rows[i].Cells[3].ForeColor = Color.White;
        //            }
        //            st += ",";
        //        }
        //        st = st.Substring(0, st.Length - 1);
        //        GridView2.Rows[i].Cells[3].Text = st;
        //        GridView2.Rows[i].Cells[3].ForeColor = Color.White;
        //        if (st== "غير معتمد")
        //        {
        //            GridView2.Rows[i].Cells[3].BackColor = Color.Red;
        //        }
        //        else
        //        {
        //            GridView2.Rows[i].Cells[3].BackColor = Color.Green;
        //        }
                
        //    }
        //}

        //protected void lnkResAdopted_Click(object sender, EventArgs e)
        //{
        //    GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
        //    ListBox listBox = row.FindControl("lstDB") as ListBox;
        //    if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
        //        conn.Open();

        //    if (conn1.State == ConnectionState.Broken || conn1.State == ConnectionState.Closed)
        //        conn1.Open();

        //    if (listBox.Visible)
        //    {
        //        if (listBox.SelectedIndex != -1)
        //        {
        //            SqlCommand sql = new SqlCommand("Select * From Adopted_Research where ReId = N'" + row.Cells[0].Text + "' and JobId='" + (rdType.SelectedValue == "IN" ? ddlRName.SelectedValue : ddlOutName.SelectedValue) + "'", conn);
        //            SqlDataReader dr = sql.ExecuteReader();
        //            if (!dr.HasRows)
        //            {
        //                for (int i = 0; i < listBox.Items.Count; i++)
        //                    if (listBox.Items[i].Selected)
        //                    {
        //                        SqlCommand sqlCommand = new SqlCommand("Insert into Adopted_Research values(" + (rdType.SelectedValue == "IN" ? ddlRName.SelectedValue : ddlOutName.SelectedValue) + ",'" + row.Cells[0].Text + "',N'" + listBox.Items[i].Value + "',@d,'"+ rdType.SelectedValue+ "')", conn);
        //                        sqlCommand.Parameters.AddWithValue("@d", DateTime.Now);
        //                        sqlCommand.ExecuteNonQuery();
        //                    }
        //                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('تم اعتماد البحث','success');", true);
        //            }
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('يجب تحديد قاعدة البيانات المعتمدة','error');", true);
        //        }
        //    }
        //    else
        //    {
        //        SqlCommand sqlCommand = new SqlCommand("Insert into Adopted_Research values(" + (rdType.SelectedValue == "IN" ? ddlRName.SelectedValue : ddlOutName.SelectedValue) + ",'" + row.Cells[0].Text + "','SCOPUS',@d,'" + rdType.SelectedValue + "')", conn);
        //        sqlCommand.Parameters.AddWithValue("@d", DateTime.Now);
        //        sqlCommand.ExecuteNonQuery();
        //    }

        //    DataTable dtResearch = new DataTable();
        //    if (rdType.SelectedValue == "IN")
        //    {
        //        SqlCommand cmd = new SqlCommand(@"select JobId,rtitle,magname,DBTypeS,pyear,pmonth,ROrderS,
        //                               (case when DBTypeI=1 then JorDB else GlobalDBS end) globaldbi,cast(AutoId as nvarchar) ReId,'' RStatus
        //                               from Research where jobid=" + ddlRName.SelectedValue, conn1);
                
        //        dtResearch.Load(cmd.ExecuteReader());

        //        cmd = new SqlCommand(@"SELECT AcdId jobid,ReTitle rtitle,c.Magazine magname,N'مجلة علمية محكمة مصنفة عالميا' DBTypeS,c.ReYear pyear,c.ReMonth pmonth,
        //                                c.ReParticipate ROrderS,'SCOPUS' globaldbi,a.ReId,'' RStatus
        //                                  FROM Research_Researcher a,ResearcherInfo b,ResearchsInfo c
        //                                  where a.RId=b.RId and a.ReId=c.ReId and AcdId=" + ddlRName.SelectedValue, conn);
        //        dtResearch.Load(cmd.ExecuteReader());
        //    }
        //    else
        //    {
        //        SqlCommand cmd = new SqlCommand(@"select ROutAutoId JobId,rtitle,Journal magname,JournalClass DBTypeS,PubDate pyear,'' pmonth,'' ROrderS,
        //                               '' globaldbi,cast(AutoId as nvarchar) ReId,'' RStatus
        //                               from Com_OuterSupervisorResearch where ROutAutoId=" + ddlOutName.SelectedValue, conn);

        //        dtResearch.Load(cmd.ExecuteReader());

        //    }

        //    GridView1.DataSource = dtResearch;
        //    GridView1.DataBind();
        //    conn.Close();
        //    conn1.Close();
        //}

        //protected void lnkResNotAdopted_Click(object sender, EventArgs e)
        //{
        //    GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;

        //    if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
        //        conn.Open();
        //    if (conn1.State == ConnectionState.Broken || conn1.State == ConnectionState.Closed)
        //        conn1.Open();

        //    SqlCommand cmd = new SqlCommand("Delete From Adopted_Research where ReId='" + row.Cells[0].Text + "' and JobId='" + (rdType.SelectedValue=="IN"?ddlRName.SelectedValue:ddlOutName.SelectedValue)+"'", conn);
        //    cmd.ExecuteNonQuery();

        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('تم إلغاء اعتماد البحث','info');", true);
        //    DataTable dtResearch = new DataTable();
        //    if (rdType.SelectedValue == "IN")
        //    {
        //        cmd = new SqlCommand(@"select JobId,rtitle,magname,DBTypeS,pyear,pmonth,ROrderS,
        //                               (case when DBTypeI=1 then JorDB else GlobalDBS end) globaldbi,cast(AutoId as nvarchar) ReId,'' RStatus
        //                               from Research where jobid=" + ddlRName.SelectedValue, conn1);
                
        //        dtResearch.Load(cmd.ExecuteReader());

        //        cmd = new SqlCommand(@"SELECT AcdId jobid,ReTitle rtitle,c.Magazine magname,N'مجلة علمية محكمة مصنفة عالميا' DBTypeS,c.ReYear pyear,c.ReMonth pmonth,
        //                                c.ReParticipate ROrderS,'SCOPUS' globaldbi,a.ReId,'' RStatus
        //                                  FROM Research_Researcher a,ResearcherInfo b,ResearchsInfo c
        //                                  where a.RId=b.RId and a.ReId=c.ReId and AcdId=" + ddlRName.SelectedValue, conn);
        //        dtResearch.Load(cmd.ExecuteReader());
        //    }
        //    else
        //    {
        //        cmd = new SqlCommand(@"select ROutAutoId JobId,rtitle,Journal magname,JournalClass DBTypeS,PubDate pyear,'' pmonth,'' ROrderS,
        //                               '' globaldbi,cast(AutoId as nvarchar) ReId,'' RStatus
        //                               from Com_OuterSupervisorResearch where ROutAutoId=" + ddlOutName.SelectedValue, conn);
        //        dtResearch.Load(cmd.ExecuteReader());

        //    }
        //    GridView1.DataSource = dtResearch;
        //    GridView1.DataBind();

        //    conn.Close();
        //}

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[4].Text == "SCOPUS")
                {
                    ((ListBox)(e.Row.FindControl("lstDB"))).Visible = false;

                }
                HtmlAnchor lnk1 = (HtmlAnchor)(e.Row.FindControl("localFile"));
                HtmlAnchor lnk2 = (HtmlAnchor)(e.Row.FindControl("localLink"));
                lnk1.Visible = false;
                lnk2.Visible = false;
                int res;
                if (int.TryParse(e.Row.Cells[0].Text,out res))
                {
                    lnk1.Visible = true;
                    lnk1.HRef = e.Row.Cells[4].Text;
                }
                else
                {
                    if (e.Row.Cells[4].Text != "" && e.Row.Cells[4].Text != "&nbsp;")
                    {
                        lnk2.Visible = true;
                        lnk2.HRef = e.Row.Cells[4].Text;
                    }
                }
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("Select * From Adopted_Research where ReId=N'" + e.Row.Cells[0].Text + "'", conn);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                SqlDataReader dr = cmd.ExecuteReader();
                if (dt.Rows.Count == 0)
                {
                    //dr.Read();
                    e.Row.Cells[7].Text = "غير معتمد";
                    //if (dr[0].ToString() == "0")
                    //{
                    //((LinkButton)(e.Row.FindControl("lnkResAdopted"))).Visible = true;
                    //    ((LinkButton)(e.Row.FindControl("lnkResNotAdopted"))).Visible = false;
                    //}
                }
                else
                {
                    e.Row.Cells[7].Text = "معتمد";
                    //((LinkButton)(e.Row.FindControl("lnkResAdopted"))).Visible = true;
                    //((LinkButton)(e.Row.FindControl("lnkResNotAdopted"))).Visible = true;
                    //cmd = new SqlCommand("Select * From Adopted_Research where ");
                    ListBox ldb = (ListBox)(e.Row.FindControl("lstDB"));
                    Label lbldb = (Label)(e.Row.FindControl("lblDB"));
                    for (int i=0;i<dt.Rows.Count;i++)
                    {
                        for (int j = 0; j < ldb.Items.Count; j++)
                            if (dt.Rows[i][3].ToString() == ldb.Items[j].Value)
                            {
                                ldb.Items[j].Selected = true;
                                lbldb.Text += ldb.Items[j].Text + " ;";
                            }

                    }
                    lbldb.Text =(lbldb.Text!=""? lbldb.Text.Substring(0, lbldb.Text.Length - 2):"");
                }

                

                conn.Close();


            }
        }

        protected void ddlOutName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRName.SelectedValue != "0")
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd = new SqlCommand(@"SELECT a.[AutoId]
                                                  ,[RName]
                                                  ,[RMajor]
                                                  ,[RDegree]
                                                  ,[RUni]
                                                  ,[RFaculty]
                                                  ,[Rdept]
                                                  ,b.name RNat
                                                  ,[RIdintity]
                                                  ,c.CollegeName InFaculty
                                                  ,d.DeptName InDept
                                                  ,[RStatus]
                                              FROM Com_OuterSupervisorInfo a,Country b,Faculty c,Department d
                                              where a.RNat=b.Code and a.InFaculty=c.AutoId and a.InDept=d.AutoId and a.AutoId=" + ddlOutName.SelectedValue, conn);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count != 0)
                {
                    switch (dt.Rows[0]["RDegree"].ToString())
                    {
                        case "1":
                            lblOutDegree.Text = "استاذ";
                            break;
                        case "2":
                            lblOutDegree.Text = "استاذ مشارك";
                            break;
                        case "3":
                            lblOutDegree.Text = "استاذ مساعد";
                            break;
                        case "4":
                            lblOutDegree.Text = "أستاذ شرف";
                            break;
                    }
                }
                lblOutUni.Text = dt.Rows[0]["RUni"].ToString();
                lblOutFaculty.Text = dt.Rows[0]["RFaculty"].ToString();
                lblOutDept.Text = dt.Rows[0]["RDept"].ToString();
                lblOutMajor.Text = dt.Rows[0]["RMajor"].ToString();
                lblNat.Text= dt.Rows[0]["RNat"].ToString();
                lblFaculty.Text = dt.Rows[0]["InFaculty"].ToString();
                lblDept.Text = dt.Rows[0]["InDept"].ToString();
                
                lblRStatus.Text = dt.Rows[0]["RStatus"].ToString();
                switch (lblRStatus.Text)
                {
                    case "0":
                        lblRStatus.Text = "بانتظار الإعتماد";
                        lblRStatus.BackColor = Color.Gray;
                        break;
                    case "1":
                        lblRStatus.Text = "معتمد";
                        lblRStatus.BackColor = Color.Green;
                        break;
                    case "2":
                        lblRStatus.Text = "غير معتمد";
                        lblRStatus.BackColor = Color.Red;
                        break;
                }

                cmd = new SqlCommand(@"select ROutAutoId JobId,rtitle,Journal magname,JournalClass DBTypeS,PubDate pyear,'' pmonth,'' ROrderS,
                                       '' globaldbi,cast(AutoId as nvarchar) ReId,'' RStatus
                                       from Com_OuterSupervisorResearch where ROutAutoId=" + ddlOutName.SelectedValue, conn);
                DataTable dtResearch = new DataTable();
                dtResearch.Load(cmd.ExecuteReader());

                //cmd = new SqlCommand(@"SELECT AcdId jobid,ReTitle rtitle,c.Magazine magname,N'مجلة علمية محكمة مصنفة عالميا' DBTypeS,c.ReYear pyear,c.ReMonth pmonth,
                //                        c.ReParticipate ROrderS,'SCOPUS' globaldbi,a.ReId,'' RStatus
                //                          FROM Research_Researcher a,ResearcherInfo b,ResearchsInfo c
                //                          where a.RId=b.RId and a.ReId=c.ReId and AcdId=" + ddlRName.SelectedValue, conn);
                //dtResearch.Load(cmd.ExecuteReader());

                GridView1.DataSource = dtResearch;
                GridView1.DataBind();
                if (GridView1.Rows.Count != 0)
                    AdoptedDiv.Visible = true;

                conn.Close();
            }
        }

    //    protected void rdType_SelectedIndexChanged(object sender, EventArgs e)
    //    {
    //        AdoptedDiv.Visible = false;
    //        GridView1.DataSource = "";
    //        GridView1.DataBind();

    //        if (rdType.SelectedValue=="IN")
    //        {
    //            InDiv.Visible = true;
    //            OutDiv.Visible = false;
    //            ddlFaculty.SelectedValue = "0";
    //            ddlDept.SelectedValue = "0";
    //            ddlRName.SelectedValue = "0";
    //            lblDegree.Text = "";
    //            lblMinor.Text = "";
    //        }
    //        else
    //        {
    //            InDiv.Visible = false;
    //            OutDiv.Visible = true;
    //            ddlOutName.SelectedValue = "0";
    //            lblOutUni.Text = "";
    //            lblOutFaculty.Text = "";
    //            lblOutDept.Text = "";
    //            lblOutMajor.Text = "";
    //            lblNat.Text = "";
    //            lblFaculty.Text = "";
    //            lblDept.Text = "";

    //            lblRStatus.Text = "";

    //        }
    //    }

    //    protected void Timer1_Tick(object sender, EventArgs e)
    //    {
    //        Response.Redirect("Com_SupervisorAdopted.aspx");
    //    }
    }
}