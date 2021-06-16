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
    public partial class Conference : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["MEUCV"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uid"] == null)
                Response.Redirect("Login.aspx");

            Session["backurl"] = "Default.aspx";
            ContentPlaceHolder cp = this.Master.Master.FindControl("ContentPlaceHolder1") as ContentPlaceHolder;
            HtmlGenericControl list = (HtmlGenericControl)cp.FindControl("menu7");//.FindControl("menu1");
            list.Attributes.Add("class", "ca-menu activeLi");
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

            //ddlInR.DataSource = dt;
            //ddlInR.DataTextField = "Year";
            //ddlInR.DataValueField = "Year";
            //ddlInR.DataBind();
            //ddlInR.Items.Insert(0, "حدد العدد");
            //ddlInR.Items[0].Value = "0";

            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand cmdTrain = new SqlCommand("select *,ROW_NUMBER() OVER(ORDER BY autoid ASC) AS serial from Conference where jobid=" + Session["uid"], conn);
            GridView1.DataSource = cmdTrain.ExecuteReader();
            GridView1.DataBind();

            SqlCommand cmd1 = new SqlCommand("Select * From Country", conn);
            DataTable dtCountry = new DataTable();
            dtCountry.Load(cmd1.ExecuteReader());
            ddlCountry.DataSource = dtCountry;
            ddlCountry.DataTextField = "Name";
            ddlCountry.DataValueField = "Code"; ;
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, "حدد بلد المؤتمر");
            ddlCountry.Items[0].Value = "0";

            conn.Close();
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(Session["saved"]))
            {
                lblMsg.Visible = false;
                Timer1.Enabled = false;
                Response.Redirect("Conference.aspx");
            }
        }

        protected void btnSaveCert_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            try
            {
                SqlCommand cmdGet = new SqlCommand("Insert Into SectionsInserted values(9," + Session["uid"] + ")", conn);
                cmdGet.ExecuteNonQuery();
            }
            catch { }

            Response.Redirect("Books.aspx");
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand cmd = new SqlCommand("Delete From Conference where Autoid=" + row.Cells[0].Text, conn);
            cmd.ExecuteNonQuery();

            if (File.Exists(Server.MapPath(row.Cells[24].Text)))
            {
                File.Delete(Server.MapPath(row.Cells[24].Text));
            }


            cmd = new SqlCommand("Insert Into SectionsUpdated values(8," + Session["uid"] + ",@du)", conn);
            cmd.Parameters.AddWithValue("@du", Convert.ToDateTime(DateTime.Now.Date));
            cmd.ExecuteNonQuery();

            conn.Close();
            Response.Redirect("Conference.aspx");
        }

        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            LinkButton1.Text = "تعديل";
            lblUpdate.Text = row.Cells[0].Text;
            txtCName.Text = row.Cells[2].Text;
            txtRTitle.Text = row.Cells[5].Text;
            ddlCType.SelectedValue = row.Cells[3].Text;
            txtMagId.Text = row.Cells[6].Text;
            ddlPubStatus.SelectedValue = row.Cells[7].Text;
            ddlDBType.SelectedValue = row.Cells[9].Text;
            txtJorMag.Text = row.Cells[23].Text;
            string[] newData = txtJorMag.Text.Split(',');

            for (int i = 0; i < newData.Length; i++)
            {
                if (newData[i] != "")
                    for (int j = 0; j < ListBox1.Items.Count; j++)
                        if (newData[i] == ListBox1.Items[j].Value)
                            ListBox1.Items[j].Selected = true;
            }

            if (ddlDBType.SelectedValue == "1")
                dbInfoDiv.Visible = true;
            else
                globaldbDiv.Visible = true;
            ddlFYear.SelectedValue = row.Cells[13].Text;
            ddlMonth.SelectedValue = row.Cells[14].Text;
            ddlCPlace.SelectedValue = row.Cells[15].Text;
            if (ddlCPlace.SelectedValue == "3" || ddlCPlace.SelectedValue == "4")
            {
                CountryDiv.Visible = true;
                ddlCountry.SelectedValue = row.Cells[17].Text;
            }
            else
                CountryDiv.Visible = false;

            ddlOrder.SelectedValue = row.Cells[19].Text;
            ddlAllR.SelectedValue = row.Cells[21].Text;

            DataTable dt = new DataTable();
            dt.Columns.Add("Year");
            for (int i = 0; i <= Convert.ToInt16(ddlAllR.SelectedValue); i++)
            {
                DataRow r = dt.NewRow();
                r[0] = i;
                dt.Rows.Add(r);
            }
            ddlInR.DataSource = dt;
            ddlInR.DataTextField = "Year";
            ddlInR.DataValueField = "Year";
            ddlInR.DataBind();
            ddlInR.Items.Insert(0, "حدد العدد");
            ddlInR.Items[0].Value = "0";


            ddlInR.SelectedValue = row.Cells[22].Text;
        }

        protected void btnTrainSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand cmdGet = new SqlCommand("select * from Conference where jobid=" + Session["uid"] + " and AutoId=" + lblUpdate.Text, conn);
                if (cmdGet.ExecuteReader().HasRows)
                {
                    cmdGet = new SqlCommand("delete from Conference where jobid=" + Session["uid"] + " and AutoId=" + lblUpdate.Text, conn);
                    cmdGet.ExecuteNonQuery();

                    cmdGet = new SqlCommand("Insert Into SectionsUpdated values(8," + Session["uid"] + ",@du)", conn);
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



                    subdir = subPath + "/conference";
                    dexists = System.IO.Directory.Exists(Server.MapPath(subdir));
                    if (!dexists)
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(subdir));
                    }
                    string fileName = System.IO.Path.GetFileName(fluRScopus.PostedFile.FileName);
                    string fileExtension = System.IO.Path.GetExtension(fluRScopus.PostedFile.FileName);

                    int count = System.IO.Directory.GetFiles(Server.MapPath(subdir), "*", System.IO.SearchOption.AllDirectories).Length + 1;
                    fullname = "conference_" + (count) + fileExtension;
                    string fileLocation = Server.MapPath(subdir + "/" + fullname);
                    //if (File.Exists(fileLocation))
                    //{
                    //    File.Delete(fileLocation);
                    //}
                    fluRScopus.SaveAs(fileLocation);
                }


                SqlCommand cmd = new SqlCommand("insert into Conference values(" +
                    "@instjob,@CTitle,@CTypeI,@CTypeS,@PaperTitle,@MagName,@PubStatusI,@PubStatusS" +
                    ",@DBTypeI,@DBTypeS,@JorDB,@GlobalDBI,@GlobalDBS,@PYear,@PMonth,@ROrderI,@ROrderS,@AllR,@InR,@PlaceI,@PlaceS,@CountryI,@CountryS,@filename)", conn);

                cmd.Parameters.AddWithValue("@instjob", Session["uid"]);
                cmd.Parameters.AddWithValue("@CTitle", txtCName.Text);
                cmd.Parameters.AddWithValue("@CTypeI", ddlCType.SelectedValue);
                cmd.Parameters.AddWithValue("@CTypeS", ddlCType.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@PaperTitle", txtRTitle.Text);
                cmd.Parameters.AddWithValue("@MagName", txtMagId.Text);
                cmd.Parameters.AddWithValue("@PubStatusI", ddlPubStatus.SelectedValue);
                cmd.Parameters.AddWithValue("@PubStatusS", ddlPubStatus.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@DBTypeI", ddlDBType.SelectedValue);
                cmd.Parameters.AddWithValue("@DBTypeS", ddlDBType.SelectedItem.Text);
                string newString = "";
                if (ddlDBType.SelectedValue == "2")
                {
                    for (int i = 0; i < ListBox1.Items.Count; i++)
                        if (ListBox1.Items[i].Selected)
                            newString += ListBox1.Items[i].Value + ",";
                }
                cmd.Parameters.AddWithValue("@JorDB", (ddlDBType.SelectedValue == "2" ? newString : txtJorMag.Text));
                cmd.Parameters.AddWithValue("@GlobalDBI", 0);
                cmd.Parameters.AddWithValue("@GlobalDBS", "");
                cmd.Parameters.AddWithValue("@PYear", ddlFYear.SelectedValue);
                cmd.Parameters.AddWithValue("@PMonth", ddlMonth.SelectedValue);
                cmd.Parameters.AddWithValue("@ROrderI", ddlOrder.SelectedValue);
                cmd.Parameters.AddWithValue("@ROrderS", ddlOrder.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@AllR", ddlAllR.SelectedValue);
                cmd.Parameters.AddWithValue("@InR", ddlInR.SelectedValue);
                cmd.Parameters.AddWithValue("@PlaceI", ddlCPlace.SelectedValue);
                cmd.Parameters.AddWithValue("@PlaceS", ddlCPlace.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@CountryI", ddlCountry.SelectedValue);
                cmd.Parameters.AddWithValue("@CountryS", ddlCountry.SelectedValue=="0"?"": ddlCountry.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@filename", subdir + "/" + fullname);
                cmd.ExecuteNonQuery();

                SqlCommand cmdTrain = new SqlCommand("select *,ROW_NUMBER() OVER(ORDER BY autoid ASC) AS serial from Conference where jobid=" + Session["uid"], conn);
                GridView1.DataSource = cmdTrain.ExecuteReader();
                GridView1.DataBind();

                conn.Close();
                Session["saved"] = true;
               // lblMsg.Visible = true;
                //lblMsg.Text = "تم التخزين بنجاح";
                //Timer1.Enabled = true;

                Response.Redirect("Conference.aspx");
                //txtLink.Text = "";
                //ddlRSector.SelectedValue = "0";
                //lblUpdate.Text = "0";
            }
            catch
            {
                lblMsg.Visible = true;
                lblMsg.Text = "حصل خطأ أثناء التخزين";
                Timer1.Enabled = true;
                Session["saved"] = false;
            }

        }

        protected void ddlDBType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlDBType.SelectedValue=="1")
            {
                dbInfoDiv.Visible = true;
                globaldbDiv.Visible = false;
                
            }
            else if (ddlDBType.SelectedValue == "2")
            {
                dbInfoDiv.Visible = false;
                globaldbDiv.Visible = true;
            }
            else
            {
                dbInfoDiv.Visible = false;
                globaldbDiv.Visible = false;
            }
            //ddlDB.SelectedValue = "0";
            txtJorMag.Text = "";
        }

        protected void ddlCType_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool selType = false;
            if(ddlCType.SelectedValue=="2")
            {
                //Label28.Visible = true;
                //txtRTitle.Visible = true;
                selType = true;
            }
            else if (ddlCType.SelectedValue == "1")
            {
                selType = false;
                //Label28.Visible = false;
                //txtRTitle.Visible = false;

                //Label31.Visible = false;
                //txtMagId.Visible = false;

                //Label32.Visible = false;
                //ddlCPlace.Visible = false;

                //Label30.Visible = false;
                //ddlDBType.Visible = false;

                //Label6.Visible = false;
                //ddlFYear.Visible = false;

                //Label7.Visible = false;
                //ddlMonth.Visible = false;

                //Label4.Visible = false;
                //ddlPubStatus.Visible = false;

                //Label33.Visible = false;
                //ddlOrder.Visible = false;

                //Label34.Visible = false;
                //ddlAllR.Visible = false;

                //Label35.Visible = false;
                //ddlInR.Visible = false;
                //Image1.Visible = false;
                //Image2.Visible = false;

            }
            Label28.Visible = selType;
            txtRTitle.Visible = selType;

            Label31.Visible = selType;
            txtMagId.Visible = selType;

            Label32.Visible = selType;
            ddlCPlace.Visible = selType;

            Label30.Visible = selType;
            ddlDBType.Visible = selType;

            Label6.Visible = selType;
            ddlFYear.Visible = selType;

            Label7.Visible = selType;
            ddlMonth.Visible = selType;

            Label4.Visible = selType;
            ddlPubStatus.Visible = selType;

            Label33.Visible = selType;
            ddlOrder.Visible = selType;

            Label34.Visible = selType;
            ddlAllR.Visible = selType;

            Label35.Visible = selType;
            ddlInR.Visible = selType;
            Image1.Visible = selType;
            Image2.Visible = selType;


        }

        protected void ddlCPlace_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCPlace.SelectedValue == "3" || ddlCPlace.SelectedValue == "4")
                CountryDiv.Visible = true;
            else
                CountryDiv.Visible = false;
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
            Response.Redirect("PublishedR.aspx");
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            Response.Redirect("Books.aspx");
        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            string[] DBName = { "Science Citation Index -SCI", "Science Citation Index Extended - SCIE",
                        "Thomson Reuters", "Scopus", "ERA", "EBSCO Abstract", "EconLit" };


            for (int j = 0; j < GridView1.Rows.Count; j++)
            {
                if (GridView1.Rows[j].Cells[9].Text == "2")
                {

                    Label lbl = GridView1.Rows[j].Cells[12].FindControl("lblRName") as Label;
                    string[] newData = lbl.Text.Split(',');
                    lbl.Text = "";
                    for (int i = 0; i < newData.Length; i++)
                    {
                        switch (newData[i])
                        {
                            case "1":
                                lbl.Text += DBName[0] + ",";
                                break;
                            case "2":
                                lbl.Text += DBName[1] + ",";
                                break;
                            case "3":
                                lbl.Text += DBName[2] + ",";
                                break;
                            case "4":
                                lbl.Text += DBName[3] + ",";
                                break;
                            case "5":
                                lbl.Text += DBName[4] + ",";
                                break;
                            case "6":
                                lbl.Text += DBName[5] + ",";
                                break;
                            case "7":
                                lbl.Text += DBName[6] + ",";
                                break;
                        }
                    }
                    lbl.Text = lbl.Text.Substring(0, lbl.Text.Length - 1);
                }
            }
        }

        protected void lnkView_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
                string path = row.Cells[24].Text;


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