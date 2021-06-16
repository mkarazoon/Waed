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
    public partial class WorkShops : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["MEUCV"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uid"] == null)
                Response.Redirect("Login.aspx");
            Session["backurl"] = "Default.aspx";
            ContentPlaceHolder cp = this.Master.Master.FindControl("ContentPlaceHolder1") as ContentPlaceHolder;
            HtmlGenericControl list = (HtmlGenericControl)cp.FindControl("menu9");//.FindControl("menu1");
            list.Attributes.Add("class", "ca-menu activeLi");

            //Label lbl = (Label)Master.FindControl("lblWelcome");
            //lbl.Text = "الندوات - الدورات - ورش العمل";
            if (!IsPostBack)
                getData();
        }

        protected void getData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Year");
            for (int i = 1; i <= 1000; i++)
            {
                DataRow row = dt.NewRow();
                row[0] = i;
                dt.Rows.Add(row);
            }

            ddlHours.DataSource = dt;
            ddlHours.DataTextField = "Year";
            ddlHours.DataValueField = "Year";
            ddlHours.DataBind();
            ddlHours.Items.Insert(0, "حدد العدد");
            ddlHours.Items[0].Value = "0";

            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand("Select * From Country", conn);
            ddlCountry.DataSource = cmd.ExecuteReader();
            ddlCountry.DataTextField = "Name";
            ddlCountry.DataValueField = "Code"; ;
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, "حدد الدولة");
            ddlCountry.Items[0].Value = "0";

            SqlCommand cmdTrain = new SqlCommand("select *,ROW_NUMBER() OVER(ORDER BY autoid ASC) AS serial from WorkShop where jobid=" + Session["uid"], conn);
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
                Response.Redirect("WorkShops.aspx");
            }
            else
                Timer1.Enabled = false;
        }

        protected void btnSaveCert_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            try
            {
                SqlCommand cmdGet = new SqlCommand("Insert Into SectionsInserted values(11," + Session["uid"] + ")", conn);
                cmdGet.ExecuteNonQuery();
            }
            catch { }

            Response.Redirect("Achievements.aspx");
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand cmd = new SqlCommand("Delete From WorkShop where Autoid=" + row.Cells[0].Text, conn);
            cmd.ExecuteNonQuery();

            if (File.Exists(Server.MapPath(row.Cells[14].Text)))
            {
                File.Delete(Server.MapPath(row.Cells[14].Text));
            }


            cmd = new SqlCommand("Insert Into SectionsUpdated values(10," + Session["uid"] + ",@du)", conn);
            cmd.Parameters.AddWithValue("@du", Convert.ToDateTime(DateTime.Now.Date));
            cmd.ExecuteNonQuery();

            conn.Close();
            Response.Redirect("WorkShops.aspx");
        }

        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            lblUpdate.Text = row.Cells[0].Text;
            ddlActType.SelectedValue = row.Cells[2].Text;
            ddlRSector.SelectedValue = row.Cells[4].Text;
            txtCName.Text = row.Cells[6].Text;
            ddlHours.SelectedValue = row.Cells[7].Text;
            ddlCountry.SelectedValue = row.Cells[8].Text;
            txtFDate.Text = Convert.ToDateTime(row.Cells[10].Text).ToString("dd-MM-yyyy");
            txtTDate.Text = Convert.ToDateTime(row.Cells[11].Text).ToString("dd-MM-yyyy");
            ddlOrder.SelectedValue = row.Cells[12].Text;
            LinkButton1.Text = "تعديل";
        }

        protected void btnTrainSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand cmdGet = new SqlCommand("select * from WorkShop where jobid=" + Session["uid"] + " and AutoId=" + lblUpdate.Text, conn);
                if (cmdGet.ExecuteReader().HasRows)
                {
                    cmdGet = new SqlCommand("delete from WorkShop where jobid=" + Session["uid"] + " and AutoId=" + lblUpdate.Text, conn);
                    cmdGet.ExecuteNonQuery();

                    cmdGet = new SqlCommand("Insert Into SectionsUpdated values(10," + Session["uid"] + ",@du)", conn);
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



                    subdir = subPath + "/workshops";
                    dexists = System.IO.Directory.Exists(Server.MapPath(subdir));
                    if (!dexists)
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(subdir));
                    }
                    string fileName = System.IO.Path.GetFileName(fluRScopus.PostedFile.FileName);
                    string fileExtension = System.IO.Path.GetExtension(fluRScopus.PostedFile.FileName);

                    int count = System.IO.Directory.GetFiles(Server.MapPath(subdir), "*", System.IO.SearchOption.AllDirectories).Length + 1;
                    fullname = "w_" + (count) + fileExtension;
                    string fileLocation = Server.MapPath(subdir + "/" + fullname);
                    //if (File.Exists(fileLocation))
                    //{
                    //    File.Delete(fileLocation);
                    //}
                    fluRScopus.SaveAs(fileLocation);
                }


                SqlCommand cmd = new SqlCommand("insert into WorkShop values(" +
                    "@instjob,@ActTypeI,@ActTypeS,@SectorI,@SectorS,@ActTitle,@HoursCount,@CountryI" +
                    ",@CountryS,@FDate,@TDate,@StatusI,@StatusS,@filename)", conn);

                cmd.Parameters.AddWithValue("@instjob", Session["uid"]);
                cmd.Parameters.AddWithValue("@ActTypeI", ddlActType.SelectedValue);
                cmd.Parameters.AddWithValue("@ActTypeS", ddlActType.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@SectorI", ddlRSector.SelectedValue);
                cmd.Parameters.AddWithValue("@SectorS", ddlRSector.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@ActTitle", txtCName.Text);
                cmd.Parameters.AddWithValue("@HoursCount", ddlHours.SelectedValue);
                cmd.Parameters.AddWithValue("@CountryI", ddlCountry.SelectedValue);
                cmd.Parameters.AddWithValue("@CountryS", ddlCountry.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@FDate", Convert.ToDateTime(txtFDate.Text));
                cmd.Parameters.AddWithValue("@TDate", Convert.ToDateTime(txtTDate.Text));
                cmd.Parameters.AddWithValue("@StatusI", ddlOrder.SelectedValue);
                cmd.Parameters.AddWithValue("@StatusS", ddlOrder.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@filename", subdir + "/" + fullname);
                cmd.ExecuteNonQuery();

                SqlCommand cmdTrain = new SqlCommand("select *,ROW_NUMBER() OVER(ORDER BY autoid ASC) AS serial from Workshop where jobid=" + Session["uid"], conn);
                GridView1.DataSource = cmdTrain.ExecuteReader();
                GridView1.DataBind();

                conn.Close();
                Session["saved"] = true;
                lblMsg.Visible = true;
                lblMsg.Text = "تم التخزين بنجاح";
                Timer1.Enabled = true;

                Response.Redirect("WorkShops.aspx");
            }
            catch
            {
                lblMsg.Visible = true;
                lblMsg.Text = "حصل خطأ أثناء التخزين";
                Timer1.Enabled = true;
                Session["saved"] = false;
            }
        }

        protected void txtFDate_TextChanged(object sender, EventArgs e)
        {
            DateTime insertedDate;
            if (DateTime.TryParse(txtFDate.Text, out insertedDate))
            {
                var parameterDate = DateTime.ParseExact("01/01/2014", "MM/dd/yyyy", CultureInfo.InvariantCulture);
                //insertedDate = DateTime.ParseExact(txtFDate.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                if (insertedDate < parameterDate)
                {
                    txtFDate.Text = "";
                    lblFDateError.Visible = true;
                }
                else
                    lblFDateError.Visible = false;
            }
            else
                txtFDate.Text = "";
        }

        protected void txtTDate_TextChanged(object sender, EventArgs e)
        {
            DateTime insertedDate;
            if (DateTime.TryParse(txtTDate.Text, out insertedDate))
            {
                var parameterDate = DateTime.ParseExact("01/01/2014", "MM/dd/yyyy", CultureInfo.InvariantCulture);
                //insertedDate = DateTime.ParseExact(txtFDate.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                if (insertedDate < parameterDate)
                {
                    txtTDate.Text = "";
                    lblTDateError.Visible = true;
                }
                else
                    lblTDateError.Visible = false;
            }
            else
                txtTDate.Text = "";
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            Response.Redirect("Books.aspx");
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            Response.Redirect("Achievements.aspx");
        }

        protected void lnkView_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
                string path = row.Cells[14].Text;


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