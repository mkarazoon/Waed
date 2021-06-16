using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ResearchAcademicUnit
{
    public partial class RCertificate : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["MEUCV"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uid"] == null)
                Response.Redirect("Login.aspx");
            Session["backurl"] = "Agreement.aspx";
            ContentPlaceHolder cp = this.Master.Master.FindControl("ContentPlaceHolder1") as ContentPlaceHolder;
            HtmlGenericControl list = (HtmlGenericControl)cp.FindControl("menu12");//.FindControl("menu1");
            list.Attributes.Add("class", "ca-menu activeLi");

            //Label lbl = (Label)Master.FindControl("lblWelcome");
            //lbl.Text = "شهادات التميز البحثي";
            if (!IsPostBack)
                getData();
        }

        protected void getData()
        {

            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand("Select * From Country", conn);
            ddlCountry.DataSource = cmd.ExecuteReader();
            ddlCountry.DataTextField = "Name";
            ddlCountry.DataValueField = "Code"; ;
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, "حدد الدولة");
            ddlCountry.Items[0].Value = "0";

            SqlCommand cmdTrain = new SqlCommand("select *,ROW_NUMBER() OVER(ORDER BY autoid ASC) AS serial from RCertificate where jobid=" + Session["uid"], conn);
            GridView1.DataSource = cmdTrain.ExecuteReader();
            GridView1.DataBind();
            conn.Close();
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            //if (Convert.ToBoolean(Session["saved"]))
            //{
                lblMsg.Visible = false;
                Timer1.Enabled = false;
                Response.Redirect("RCertificate.aspx");
            //}
        }

        protected void btnSaveCert_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            try
            {
                SqlCommand cmdGet = new SqlCommand("Insert Into SectionsInserted values(15," + Session["uid"] + ")", conn);
                cmdGet.ExecuteNonQuery();
            }
            catch { }

            Response.Redirect("UploadFiles.aspx");
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand cmd = new SqlCommand("Delete From RCertificate where Autoid=" + row.Cells[0].Text, conn);
            cmd.ExecuteNonQuery();

            if (File.Exists(Server.MapPath(row.Cells[7].Text)))
            {
                File.Delete(Server.MapPath(row.Cells[7].Text));
            }


            cmd = new SqlCommand("Insert Into SectionsUpdated values(13," + Session["uid"] + ",@du)", conn);
            cmd.Parameters.AddWithValue("@du", Convert.ToDateTime(DateTime.Now.Date));
            cmd.ExecuteNonQuery();

            conn.Close();
            Response.Redirect("RCertificate.aspx");
        }

        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            lblUpdate.Text = row.Cells[0].Text;
            txtName.Text = row.Cells[2].Text;
            txtFromName.Text= row.Cells[3].Text;
            txtDate.Text = row.Cells[4].Text;
            ddlCountry.SelectedValue= row.Cells[5].Text;
            LinkButton1.Text = "تعديل";
        }

        protected void btnTrainSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand cmdGet = new SqlCommand("select * from RCertificate where jobid=" + Session["uid"] + " and AutoId=" + lblUpdate.Text, conn);
                if (cmdGet.ExecuteReader().HasRows)
                {
                    cmdGet = new SqlCommand("delete from RCertificate where jobid=" + Session["uid"] + " and AutoId=" + lblUpdate.Text, conn);
                    cmdGet.ExecuteNonQuery();

                    cmdGet = new SqlCommand("Insert Into SectionsUpdated values(13," + Session["uid"] + ",@du)", conn);
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



                    subdir = subPath + "/certificate";
                    dexists = System.IO.Directory.Exists(Server.MapPath(subdir));
                    if (!dexists)
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(subdir));
                    }
                    string fileName = System.IO.Path.GetFileName(fluRScopus.PostedFile.FileName);
                    string fileExtension = System.IO.Path.GetExtension(fluRScopus.PostedFile.FileName);

                    int count = System.IO.Directory.GetFiles(Server.MapPath(subdir), "*", System.IO.SearchOption.AllDirectories).Length + 1;
                    fullname = "cert_" + (count) + fileExtension;
                    string fileLocation = Server.MapPath(subdir + "/" + fullname);
                    //if (File.Exists(fileLocation))
                    //{
                    //    File.Delete(fileLocation);
                    //}
                    fluRScopus.SaveAs(fileLocation);
                }


                SqlCommand cmd = new SqlCommand("insert into RCertificate values(" +
                    "@instjob,@CName,@FromName,@CDate,@CI,@CN,@filename)", conn);

                cmd.Parameters.AddWithValue("@instjob", Session["uid"]);
                cmd.Parameters.AddWithValue("@CName", txtName.Text);
                cmd.Parameters.AddWithValue("@FromName", txtFromName.Text);
                cmd.Parameters.AddWithValue("@CDate", Convert.ToDateTime(txtDate.Text));
                cmd.Parameters.AddWithValue("@CI", ddlCountry.SelectedValue);
                cmd.Parameters.AddWithValue("@CN", ddlCountry.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@filename", subdir + "/" + fullname);
                cmd.ExecuteNonQuery();

                SqlCommand cmdTrain = new SqlCommand("select *,ROW_NUMBER() OVER(ORDER BY autoid ASC) AS serial from RCertificate where jobid=" + Session["uid"], conn);
                GridView1.DataSource = cmdTrain.ExecuteReader();
                GridView1.DataBind();

                conn.Close();
                Session["saved"] = true;
                lblMsg.Visible = true;
                lblMsg.Text = "تم التخزين بنجاح";
                Timer1.Enabled = true;

                Response.Redirect("RCertificate.aspx");
            }
            catch
            {
                lblMsg.Visible = true;
                lblMsg.Text = "حصل خطأ أثناء التخزين";
                Timer1.Enabled = true;
                Session["saved"] = false;
            }
        }

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            DateTime insertedDate;
            if (DateTime.TryParse(txtDate.Text, out insertedDate))
            {
                var parameterDate = DateTime.ParseExact("01/01/2014", "MM/dd/yyyy", CultureInfo.InvariantCulture);
                //insertedDate = DateTime.ParseExact(txtFDate.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                if (insertedDate < parameterDate)
                {
                    txtDate.Text = "";
                    lblFDateError.Visible = true;
                }
                else
                    lblFDateError.Visible = false;
            }
            else
                txtDate.Text = "";
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            Response.Redirect("RCommittees.aspx");
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            Response.Redirect("UploadFiles.aspx");
        }

        protected void lnkView_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
                string path = row.Cells[7].Text;


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