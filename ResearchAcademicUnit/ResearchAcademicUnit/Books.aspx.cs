using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ResearchAcademicUnit
{
    public partial class Books : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["MEUCV"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uid"] == null)
                Response.Redirect("Login.aspx");
            Session["backurl"] = "Default.aspx";
            ContentPlaceHolder cp = this.Master.Master.FindControl("ContentPlaceHolder1") as ContentPlaceHolder;
            HtmlGenericControl list = (HtmlGenericControl)cp.FindControl("menu8");//.FindControl("menu1");
            list.Attributes.Add("class", "ca-menu activeLi");

            //Label lbl = (Label)Master.FindControl("lblWelcome");
            //lbl.Text = "النشاطات التأليفية - الكتب";
            if (!IsPostBack)
                getData();
        }

        protected void getData()
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("Year");
            for (int i = 1980; i <= DateTime.Now.Date.Year; i++)
            {
                DataRow row = dt1.NewRow();
                row[0] = i;
                dt1.Rows.Add(row);
            }

            ddlFYear.DataSource = dt1;
            ddlFYear.DataTextField = "Year";
            ddlFYear.DataValueField = "Year";
            ddlFYear.DataBind();
            ddlFYear.Items.Insert(0, "حدد السنة");
            ddlFYear.Items[0].Value = "0";

            DataTable dt = new DataTable();
            dt.Columns.Add("Year");
            for (int i = 1; i <= 100; i++)
            {
                DataRow row = dt.NewRow();
                row[0] = i;
                dt.Rows.Add(row);
            }

            ddlAllR.DataSource = dt;
            ddlAllR.DataTextField = "Year";
            ddlAllR.DataValueField = "Year";
            ddlAllR.DataBind();
            ddlAllR.Items.Insert(0, "حدد العدد");
            ddlAllR.Items[0].Value = "0";

            ddlInR.DataSource = dt;
            ddlInR.DataTextField = "Year";
            ddlInR.DataValueField = "Year";
            ddlInR.DataBind();
            ddlInR.Items.Insert(0, "حدد العدد");
            ddlInR.Items[0].Value = "0";

            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand cmdTrain = new SqlCommand("select *,ROW_NUMBER() OVER(ORDER BY autoid ASC) AS serial from Books where jobid=" + Session["uid"], conn);
            GridView1.DataSource = cmdTrain.ExecuteReader();
            GridView1.DataBind();
            conn.Close();
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(Session["saved"]))
            {
                lblMsg.Visible = false;
                Timer1.Enabled = false;
                Response.Redirect("Books.aspx");
            }
        }

        protected void btnSaveCert_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            try
            {
                SqlCommand cmdGet = new SqlCommand("Insert Into SectionsInserted values(10," + Session["uid"] + ")", conn);
                cmdGet.ExecuteNonQuery();
            }
            catch { }

            Response.Redirect("WorkShops.aspx");
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand cmd = new SqlCommand("Delete From Books where Autoid=" + row.Cells[0].Text, conn);
            cmd.ExecuteNonQuery();

            if (File.Exists(Server.MapPath(row.Cells[17].Text)))
            {
                File.Delete(Server.MapPath(row.Cells[17].Text));
            }


            cmd = new SqlCommand("Insert Into SectionsUpdated values(9," + Session["uid"] + ",@du)", conn);
            cmd.Parameters.AddWithValue("@du", Convert.ToDateTime(DateTime.Now.Date));
            cmd.ExecuteNonQuery();

            conn.Close();
            Response.Redirect("Books.aspx");
        }

        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            lblUpdate.Text = row.Cells[0].Text;
            txtBTitle.Text = row.Cells[2].Text;
            ddlBLang.SelectedValue = row.Cells[3].Text;
            if (ddlBLang.SelectedValue == "3")
                otherLangDiv.Visible = true;
            else
                otherLangDiv.Visible = false;
            txtOtherLang.Text= row.Cells[16].Text;
            txtCName.Text = row.Cells[5].Text;
            txtVersion.Text = row.Cells[6].Text;
            ddlFYear.SelectedValue = row.Cells[7].Text;
            ddlMonth.SelectedValue = row.Cells[8].Text;
            txtISBN.Text = row.Cells[9].Text;
            ddlOrder.SelectedValue = row.Cells[10].Text;
            ddlAllR.SelectedValue = row.Cells[12].Text;
            ddlInR.SelectedValue = row.Cells[13].Text;
            ddlPubStatus.SelectedValue = row.Cells[14].Text;
            LinkButton1.Text = "تعديل";
        }

        protected void btnTrainSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand cmdGet = new SqlCommand("select * from Books where jobid=" + Session["uid"] + " and AutoId=" + lblUpdate.Text, conn);
                if (cmdGet.ExecuteReader().HasRows)
                {
                    cmdGet = new SqlCommand("delete from Books where jobid=" + Session["uid"] + " and AutoId=" + lblUpdate.Text, conn);
                    cmdGet.ExecuteNonQuery();

                    cmdGet = new SqlCommand("Insert Into SectionsUpdated values(9," + Session["uid"] + ",@du)", conn);
                    cmdGet.Parameters.AddWithValue("@du", Convert.ToDateTime(DateTime.Now.Date));
                    cmdGet.ExecuteNonQuery();
                    Session["saved"] = false;

                }

                string subPath = "document/" + Session["uid"];

                bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));
                bool dexists = false;
                string subdir = subPath;
                string fullname = "";
                if (fluRScopus.HasFile)
                {
                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(subPath));



                    subdir = subPath + "/books";
                    dexists = System.IO.Directory.Exists(Server.MapPath(subdir));
                    if (!dexists)
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(subdir));
                    }
                    string fileName = System.IO.Path.GetFileName(fluRScopus.PostedFile.FileName);
                    string fileExtension = System.IO.Path.GetExtension(fluRScopus.PostedFile.FileName);

                    int count = System.IO.Directory.GetFiles(Server.MapPath(subdir), "*", System.IO.SearchOption.AllDirectories).Length + 1;
                    fullname = "book_" + (count) + fileExtension;
                    string fileLocation = Server.MapPath(subdir + "/" + fullname);
                    //if (File.Exists(fileLocation))
                    //{
                    //    File.Delete(fileLocation);
                    //}
                    fluRScopus.SaveAs(fileLocation);
                }


                SqlCommand cmd = new SqlCommand("insert into Books values(" +
                    "@instjob,@BookTitle,@BookLangI,@BookLangS,@Publisher,@BookVer,@BookYear,@BookMonth" +
                    ",@ISBN,@AuthOrderI,@AuthOrderS,@AllR,@InR,@PubStatusI,@PubStatusS,@OtherLang,@filename)", conn);

                cmd.Parameters.AddWithValue("@instjob", Session["uid"]);
                cmd.Parameters.AddWithValue("@BookTitle", txtBTitle.Text);
                cmd.Parameters.AddWithValue("@BookLangI", ddlBLang.SelectedValue);
                cmd.Parameters.AddWithValue("@BookLangS", ddlBLang.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Publisher", txtCName.Text);
                cmd.Parameters.AddWithValue("@BookVer", txtVersion.Text);
                cmd.Parameters.AddWithValue("@BookYear", ddlFYear.SelectedValue);
                cmd.Parameters.AddWithValue("@BookMonth", ddlMonth.SelectedValue);
                cmd.Parameters.AddWithValue("@ISBN", txtISBN.Text);
                cmd.Parameters.AddWithValue("@AuthOrderI", ddlOrder.SelectedValue);
                cmd.Parameters.AddWithValue("@AuthOrderS", ddlOrder.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@AllR", ddlAllR.SelectedValue);
                cmd.Parameters.AddWithValue("@InR", ddlInR.SelectedValue);
                cmd.Parameters.AddWithValue("@PubStatusI", ddlPubStatus.SelectedValue);
                cmd.Parameters.AddWithValue("@PubStatusS", ddlPubStatus.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@OtherLang", txtOtherLang.Text);
                cmd.Parameters.AddWithValue("@filename", subdir + "/" + fullname);
                cmd.ExecuteNonQuery();

                SqlCommand cmdTrain = new SqlCommand("select *,ROW_NUMBER() OVER(ORDER BY autoid ASC) AS serial from Books where jobid=" + Session["uid"], conn);
                GridView1.DataSource = cmdTrain.ExecuteReader();
                GridView1.DataBind();

                conn.Close();
                Session["saved"] = true;
                lblMsg.Visible = true;
                lblMsg.Text = "تم التخزين بنجاح";
                Timer1.Enabled = true;

                Response.Redirect("Books.aspx");
            }
            catch
            {
                lblMsg.Visible = true;
                lblMsg.Text = "حصل خطأ أثناء التخزين";
                Timer1.Enabled = true;
                Session["saved"] = false;
            }

        }

        protected void ddlBLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBLang.SelectedValue == "3")
                otherLangDiv.Visible = true;
            else
                otherLangDiv.Visible = false;
        }

        protected void ddlAllR_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAllR.SelectedValue != "0")
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Year");
                for (int i = 0; i <= Convert.ToInt16(ddlAllR.SelectedValue); i++)
                {
                    DataRow row = dt.NewRow();
                    row[0] = i;
                    dt.Rows.Add(row);
                }
                ddlInR.DataSource = dt;
                ddlInR.DataTextField = "Year";
                ddlInR.DataValueField = "Year";
                ddlInR.DataBind();
                ddlInR.Items.Insert(0, "حدد العدد");
                ddlInR.Items[0].Value = "-1";

            }
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            Response.Redirect("Conference.aspx");
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            Response.Redirect("WorkShops.aspx");
        }

        protected void lnkView_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
                string path = row.Cells[17].Text;


                WebClient User = new WebClient();
                Byte[] FileBuffer = User.DownloadData(Server.MapPath(path));
                if (FileBuffer != null)
                {
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-length", FileBuffer.Length.ToString());
                    //Response.Write("<script>window.open('" + path + "','_blank');</script>");
                    Response.BinaryWrite(FileBuffer);
                }
            }
            catch (Exception err) { }

        }
    }
}