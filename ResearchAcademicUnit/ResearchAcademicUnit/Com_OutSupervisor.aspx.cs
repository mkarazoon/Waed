using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchAcademicUnit
{
    public partial class Com_OutSupervisor : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);
        DataTable dtRTable = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userid"] == null || Session.IsNewSession)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("Select * from Country", conn);
                ddlNat.DataSource = cmd.ExecuteReader();
                ddlNat.DataTextField = "Name";
                ddlNat.DataValueField = "Code";
                ddlNat.DataBind();
                ddlNat.Items.Insert(0, "اختيار");
                ddlNat.Items[0].Value = "";
                ddlNat.SelectedValue = "101";

                cmd = new SqlCommand("select * from Faculty", conn);
                ddlInFaculty.DataSource = cmd.ExecuteReader();
                ddlInFaculty.DataTextField = "CollegeName";
                ddlInFaculty.DataValueField = "AutoId";
                ddlInFaculty.DataBind();
                ddlInFaculty.Items.Insert(0, "اختيار الكلية");
                ddlInFaculty.Items[0].Value = "";


                dtRTable.Columns.Add("AutoId");
                dtRTable.Columns.Add("RTitle");
                dtRTable.Columns.Add("Journal");
                dtRTable.Columns.Add("PubDate");
                dtRTable.Columns.Add("JournalClass");
                dtRTable.Columns.Add("RStatus");
                Session["dtRTable"] = dtRTable;

                cmd = new SqlCommand("Select *,(case when RDegree=1 then N'أستاذ' when RDegree=2 then N'أستاذ مشارك' when RDegree=3 then N'أستاذ مساعد' when RDegree=4 then N'أستاذ شرف' end) RDegreeT From Com_OuterSupervisorInfo where CreatedBy=" + Session["userid"], conn);
                GridView2.DataSource = cmd.ExecuteReader();
                GridView2.DataBind();

                conn.Close();
            }
        }

        protected void ddlNat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlNat.SelectedValue == "101")
            {
                SSNDiv.Visible = true;
                PassportDiv.Visible = false;
            }
            else
            {
                SSNDiv.Visible = false;
                PassportDiv.Visible = true;
            }
        }

        //protected void lnkDelete_Click(object sender, EventArgs e)
        //{
        //    GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
        //    dtRTable = (DataTable)Session["dtRTable"];
        //    dtRTable.Rows.RemoveAt(row.RowIndex);
        //    for (int i = 0; i < dtRTable.Rows.Count; i++)
        //        dtRTable.Rows[i][0] = (i + 1).ToString();
        //    Session["dtRTable"] = dtRTable;
        //    GridView1.DataSource = dtRTable;
        //    GridView1.DataBind();
        //    if (dtRTable.Rows.Count != 0)
        //        btnSubmit.Visible = true;
        //    else
        //        btnSubmit.Visible = false;
        //}

        //protected void lnkUpdate_Click(object sender, EventArgs e)
        //{
        //    GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
        //    txtRTitle.Text = row.Cells[1].Text;
        //    txtJournal.Text = row.Cells[2].Text;
        //    txtPubDate.Text = row.Cells[3].Text;
        //    txtJournalClass.Text = row.Cells[4].Text;
        //    ddlRStatus.SelectedValue = row.Cells[5].Text == "منشور" ? "Pub" : "Acc";
        //    lnkAddResearch.Text = "تعديل بحث";
        //    Session["UpdatedRow"] = row.RowIndex;
        //    if (dtRTable.Rows.Count != 0)
        //        btnSubmit.Visible = true;
        //    else
        //        btnSubmit.Visible = false;
        //}

        //protected void lnkAddResearch_Click(object sender, EventArgs e)
        //{
        //    dtRTable = (DataTable)Session["dtRTable"];
        //    if (Session["update"].ToString() == ViewState["update"].ToString())
        //    {
                
        //        DataRow row = dtRTable.NewRow();
        //        if (lnkAddResearch.Text.Contains("إضافة"))
        //        {
        //            row[0] = dtRTable.Rows.Count + 1;
        //            row[1] = txtRTitle.Text;
        //            row[2] = txtJournal.Text;
        //            row[3] = txtPubDate.Text;
        //            row[4] = txtJournalClass.Text;
        //            row[5] = ddlRStatus.SelectedValue;
        //            dtRTable.Rows.Add(row);
        //        }
        //        else
        //        {
        //            dtRTable.Rows[Convert.ToInt16(Session["UpdatedRow"])][1] = txtRTitle.Text;
        //            dtRTable.Rows[Convert.ToInt16(Session["UpdatedRow"])][2] = txtJournal.Text;
        //            dtRTable.Rows[Convert.ToInt16(Session["UpdatedRow"])][3] = txtPubDate.Text;
        //            dtRTable.Rows[Convert.ToInt16(Session["UpdatedRow"])][4] = txtJournalClass.Text;
        //            dtRTable.Rows[Convert.ToInt16(Session["UpdatedRow"])][5] = ddlRStatus.SelectedValue;
        //        }
        //        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
        //    }

        //    Session["dtRTable"] = dtRTable;
        //    //GridView1.DataSource = dtRTable;
        //    //GridView1.DataBind();
        //    //txtRTitle.Text = "";
        //    //txtJournal.Text = "";
        //    //txtPubDate.Text = "";
        //    //txtJournalClass.Text = "";
        //    //ddlRStatus.SelectedValue = "0";
        //    //lnkAddResearch.Text = "إضافة بحث";
        //    //if (dtRTable.Rows.Count != 0)
        //    //    btnSubmit.Visible = true;
        //    //else
        //    //    btnSubmit.Visible = false;
        //}

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            string subPath = "document/UploadedResearch";
            //string fileLocation1 = "";
            string fileLocation2 = "";
            string fullname = "";
            //if (uploadResearch.Attributes["data-default-file"].ToString() == "" || (uploadResearch.PostedFile != null && uploadResearch.PostedFile.FileName != ""))
            //{
            //    bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));
            //    string subdir = subPath;
            //    if (!exists)
            //        System.IO.Directory.CreateDirectory(Server.MapPath(subPath));

            //    if (uploadResearch.Visible)
            //        if (uploadResearch.PostedFile.FileName != "")
            //        {
            //            string fileName = Path.GetFileName(uploadResearch.PostedFile.FileName);
            //            string fileExtension = Path.GetExtension(uploadResearch.PostedFile.FileName);
            //            fullname = "Research_" + (SSNDiv.Visible ? txtSSN.Text : txtPassport.Text) + fileExtension;
            //            fileLocation1 = subdir + "/" + fullname;
            //            if (File.Exists(fileLocation1))
            //            {
            //                File.Delete(fileLocation1);
            //            }

            //            uploadResearch.PostedFile.SaveAs(Server.MapPath(fileLocation1));
            //        }
            //        else
            //            fileLocation1 = uploadResearch.Attributes["data-default-file"].ToString();
            //}

            if (File1.Attributes["data-default-file"].ToString() == "" || (File1.PostedFile != null && File1.PostedFile.FileName != ""))
            {
                bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));
                string subdir = subPath;
                if (!exists)
                    System.IO.Directory.CreateDirectory(Server.MapPath(subPath));

                if (File1.PostedFile.FileName != "")
                {
                    string fileName = Path.GetFileName(File1.PostedFile.FileName);
                    string fileExtension = Path.GetExtension(File1.PostedFile.FileName);
                    fullname = "CV_" + (SSNDiv.Visible ? txtSSN.Text : txtPassport.Text) + fileExtension;
                    fileLocation2 = subdir + "/" + fullname;
                    if (File.Exists(fileLocation2))
                    {
                        File.Delete(fileLocation2);
                    }

                    File1.PostedFile.SaveAs(Server.MapPath(fileLocation2));
                }
                else
                    fileLocation2 = File1.Attributes["data-default-file"].ToString();
            }


            SqlCommand cmd = new SqlCommand("insert into Com_OuterSupervisorInfo output inserted.AutoId values" +
                "(@RName,@RMajor,@RDegree,@RUni,@RFaculty,@Rdept,@RNat,@RIdintity,@InFaculty,@InDept,@RStatus,@CVPath,@RFilePath,@CreatedBy)", conn);
            cmd.Parameters.AddWithValue("@RName",txtName.Text);
            cmd.Parameters.AddWithValue("@RMajor",txtMajor.Text);
            cmd.Parameters.AddWithValue("@RDegree",ddlDegree.SelectedValue);
            cmd.Parameters.AddWithValue("@RUni",txtUni.Text);
            cmd.Parameters.AddWithValue("@RFaculty",txtFaculty.Text);
            cmd.Parameters.AddWithValue("@Rdept",txtDept.Text);
            cmd.Parameters.AddWithValue("@RNat",ddlNat.SelectedValue);
            cmd.Parameters.AddWithValue("@RIdintity", ddlNat.SelectedValue == "101" ? txtSSN.Text : txtPassport.Text);
            cmd.Parameters.AddWithValue("@InFaculty", ddlInFaculty.SelectedValue);
            cmd.Parameters.AddWithValue("@InDept", ddlInDept.SelectedValue);
            cmd.Parameters.AddWithValue("@RStatus",0);
            cmd.Parameters.AddWithValue("@CVPath", fileLocation2);
            cmd.Parameters.AddWithValue("@RFilePath", "");
            cmd.Parameters.AddWithValue("@CreatedBy", Session["userid"]);
            string autoid = cmd.ExecuteScalar().ToString();

            //for(int i=0;i<GridView1.Rows.Count;i++)
            //{
            //    cmd = new SqlCommand("Insert into Com_OuterSupervisorResearch values(@ROutId,@RTitle,@Journal,@PubDate,@JournalClass,@RStatus)", conn);
            //    cmd.Parameters.AddWithValue("@ROutId", autoid);
            //    cmd.Parameters.AddWithValue("@RTitle",GridView1.Rows[i].Cells[1].Text);
            //    cmd.Parameters.AddWithValue("@Journal", GridView1.Rows[i].Cells[2].Text);
            //    cmd.Parameters.AddWithValue("@PubDate", GridView1.Rows[i].Cells[3].Text);
            //    cmd.Parameters.AddWithValue("@JournalClass", GridView1.Rows[i].Cells[4].Text);
            //    cmd.Parameters.AddWithValue("@RStatus", GridView1.Rows[i].Cells[5].Text);
            //    cmd.ExecuteNonQuery();
            //}


            conn.Close();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('تم ارسال معلوماتك للإعتماد','success');", true);
            Timer1.Enabled = true;

            
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType==DataControlRowType.DataRow)
            {
                if (e.Row.Cells[5].Text == "Pub")
                    e.Row.Cells[5].Text = "منشور";
                else
                    e.Row.Cells[5].Text = "مقبول للنشر";
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            ViewState["update"] = Session["update"];
            base.OnPreRender(e);
        }

        protected void ddlInFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlInFaculty.SelectedValue!="")
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("Select * From Department where CAutoid=" + ddlInFaculty.SelectedValue, conn);
                ddlInDept.DataSource = cmd.ExecuteReader();
                ddlInDept.DataTextField = "DeptName";
                ddlInDept.DataValueField = "AutoId";
                ddlInDept.DataBind();
                ddlInDept.Items.Insert(0,"اختيار القسم");
                ddlInDept.Items[0].Value = "";


                conn.Close();
            }
            else
            {
                ddlInDept.Items.Clear();
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Response.Redirect("Com_OutSupervisor.aspx");
        }

        protected void lnkRView_Click(object sender, EventArgs e)
        {

        }

        protected void lnkREdit_Click(object sender, EventArgs e)
        {

        }

        protected void lnkRDelete_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;

            if (File.Exists(Server.MapPath(row.Cells[5].Text)))
            {
                File.Delete(Server.MapPath(row.Cells[5].Text));
            }

            if (File.Exists(Server.MapPath(row.Cells[6].Text)))
            {
                File.Delete(Server.MapPath(row.Cells[6].Text));
            }

            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand("Delete From Com_OuterSupervisorInfo where AutoId=" + row.Cells[0].Text, conn);
            cmd.ExecuteNonQuery();

            cmd = new SqlCommand("Select * From Com_OuterSupervisorInfo where CreatedBy=" + Session["userid"], conn);
            GridView2.DataSource = cmd.ExecuteReader();
            GridView2.DataBind();


            conn.Close();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "executeExample('تم الحذف بنجاح','success');", true);
        }
    }
}