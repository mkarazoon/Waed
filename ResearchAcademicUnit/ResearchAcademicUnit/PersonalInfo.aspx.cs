using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ResearchAcademicUnit
{
    public partial class PersonalInfo : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["MEUCV"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uid"] == null)
                Response.Redirect("Login.aspx");
            //Label lbl = (Label)Master.FindControl("lblWelcome");

            Session["backurl"] = "Default.aspx";
            //lbl.Text = "المعلومات الشخصية";
            if (!IsPostBack)
            {
                fillData();
                //ContentPlaceHolder cph = (ContentPlaceHolder)this.Master.FindControl("ContentPlaceHolder1");
                //HtmlGenericControl list = (HtmlGenericControl)this.Master.FindControl("menu1");//.FindControl("menu1");

            }
                ContentPlaceHolder cp = this.Master.Master.FindControl("ContentPlaceHolder1") as ContentPlaceHolder;
                HtmlGenericControl list = (HtmlGenericControl)cp.FindControl("menu1");//.FindControl("menu1");
                list.Attributes.Add("class", "ca-menu activeLi");


            if (IsPostBack && flCV.PostedFile != null)
            {
                if (flCV.PostedFile.FileName.Length > 0)
                {
                    string subPath = "document/" + Session["uid"];

                    bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));

                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
                    if (File.Exists(lblImageName.Text))
                    {
                        File.Delete(lblImageName.Text);
                    }
                    string imgname = "";
                    //if (flCV.HasFile)
                    //{
                    string fileName = Path.GetFileName(flCV.PostedFile.FileName);
                    string fileExtension = Path.GetExtension(flCV.PostedFile.FileName);
                    imgname = "img_" + Session["uid"] + fileExtension;
                    string fileLocation = Server.MapPath(subPath + "/" + imgname);
                    flCV.SaveAs(fileLocation);
                    imgF.ImageUrl = subPath + "/" + imgname;
                    lblImageName.Text = subPath + "/" + imgname;
                }
            }

        }

        protected void fillData()
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand("select * from College", conn);
            ddlCollege.DataSource = cmd.ExecuteReader();
            ddlCollege.DataTextField = "CollegeName";
            ddlCollege.DataValueField = "Autoid";
            ddlCollege.DataBind();
            ddlCollege.Items.Insert(0, "حدد الكلية");
            ddlCollege.Items[0].Value = "0";

            DataTable dt = new DataTable();
            dt.Columns.Add("Year");
            for (int i = 1; i <= 100; i++)
            {
                DataRow row = dt.NewRow();
                row[0] = i;
                dt.Rows.Add(row);
            }

            ddlAllExpYear.DataSource = dt;
            ddlAllExpYear.DataTextField = "Year";
            ddlAllExpYear.DataValueField = "Year";
            ddlAllExpYear.DataBind();
            ddlAllExpYear.Items.Insert(0, "حدد العدد");
            ddlAllExpYear.Items[0].Value = "0";

            ddlUExp.DataSource = dt;
            ddlUExp.DataTextField = "Year";
            ddlUExp.DataValueField = "Year";
            ddlUExp.DataBind();
            ddlUExp.Items.Insert(0, "حدد العدد");
            ddlUExp.Items[0].Value = "0";

            DataTable dt1 = new DataTable();
            dt1.Columns.Add("Year");
            for (int i = 1920; i <= 2050; i++)
            {
                DataRow row = dt1.NewRow();
                row[0] = i;
                dt1.Rows.Add(row);
            }

            ddlDegreeYear.DataSource = dt1;
            ddlDegreeYear.DataTextField = "Year";
            ddlDegreeYear.DataValueField = "Year";
            ddlDegreeYear.DataBind();
            ddlDegreeYear.Items.Insert(0, "حدد السنة");
            ddlDegreeYear.Items[0].Value = "0";

            cmd = new SqlCommand("Select * From Country", conn);
            ddlNat.DataSource = cmd.ExecuteReader();
            ddlNat.DataTextField = "Name";
            ddlNat.DataValueField = "Code"; ;
            ddlNat.DataBind();
            ddlNat.Items.Insert(0, "حدد الجنسية");
            ddlNat.Items[0].Value = "0";
            conn.Close();

            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand cmdGet = new SqlCommand("select * from instinfo where instjobid=" + Session["uid"], conn);
            SqlDataReader dr = cmdGet.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                txtJobId.Text = Session["uid"].ToString();
                txtAName.Text = dr["AName"].ToString();
                txtSName.Text= dr["SName"].ToString();
                txtEName.Text = dr["EName"].ToString();
                ddlNat.SelectedValue= dr["Nat"].ToString();
                ddlGender.SelectedValue= dr["Sex"].ToString();
                txtAddress.Text= dr["Address"].ToString();
                txtEmail.Text= dr["Email"].ToString();
                txtMobile.Text= dr["Mobile"].ToString();
                ddlPositionName.SelectedValue= dr["PositionName"].ToString();
                ddlCollege.SelectedValue= dr["College"].ToString();
                SqlCommand cmd1 = new SqlCommand("select * from Department where CAutoid=" + ddlCollege.SelectedValue, conn);
                ddlDept.DataSource = cmd1.ExecuteReader();
                ddlDept.DataTextField = "DeptName";
                ddlDept.DataValueField = "AutoId";
                ddlDept.DataBind();
                ddlDept.Items.Insert(0, "حدد القسم");
                ddlDept.Items[0].Value = "0";


                ddlDept.SelectedValue= dr["Dept"].ToString();
                ddlDegree.SelectedValue= dr["Degree"].ToString();
                ddlDegreeYear.SelectedValue= dr["DegreeYear"].ToString();
                ddlQual.SelectedValue= dr["Qual"].ToString();
                txtMajor.Text= dr["Major"].ToString();
                txtMinor.Text= dr["Minor"].ToString();
                ddlRSector.SelectedValue= dr["RSector"].ToString();
                cmd = new SqlCommand("Select * From RFields where RSector=" + ddlRSector.SelectedValue, conn);
                ddlField.DataSource = cmd.ExecuteReader();
                ddlField.DataValueField = "id";
                ddlField.DataTextField = "RField";
                ddlField.DataBind();
                ddlField.Items.Insert(0, "حدد المجال البحثي");
                ddlField.Items[0].Value = "0";
                //conn.Close();


                ddlField.SelectedValue= dr["RField"].ToString();
                ddlAllExpYear.SelectedValue= dr["AllExp"].ToString();
                ddlUExp.SelectedValue= dr["UniExp"].ToString();
                txtOtherPos.Text= dr["otherPosName"].ToString();
                //lblImageName.Text= dr["EmpImage"].ToString();
                txtBOD.Text=Convert.ToDateTime( dr["BirthDate"]).ToString("dd-MM-yyyy");
                txtHDate.Text = Convert.ToDateTime(dr["HireDate"]).ToString("dd-MM-yyyy");
                imgF.ImageUrl = dr["EmpImage"].ToString();// "document/" + Session["uid"]/;// (lblImageName.Text.Contains(Session["uid"].ToString()) ? lblImageName.Text : "images/user1.png");
            }
            conn.Close();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand cmdGet = new SqlCommand("select * from instinfo where instjobid=" + Session["uid"], conn);
                if (!cmdGet.ExecuteReader().HasRows)
                {
                    string sql = "insert into InstInfo values(";
                    sql += Session["uid"] + ",N'";
                    sql += txtAName.Text + "',N'";
                    sql += txtSName.Text + "',@bod,";
                    sql += ddlNat.SelectedValue + ",'";
                    sql += ddlGender.SelectedValue + "',N'";
                    sql += txtAddress.Text + "',N'";
                    sql += txtEmail.Text + "','";
                    sql += txtMobile.Text + "',";
                    sql += ddlPositionName.SelectedValue + ",";
                    sql += ddlCollege.SelectedValue + ",";
                    sql += ddlDept.SelectedValue + ",";
                    sql += ddlDegree.SelectedValue + ",";
                    sql += ddlDegreeYear.SelectedValue + ",@hd,";
                    sql += ddlQual.SelectedValue + ",N'";
                    sql += txtMajor.Text + "',N'";
                    sql += txtMinor.Text + "',";
                    sql += ddlRSector.SelectedValue + ",";
                    sql += ddlField.SelectedValue + ",";
                    sql += ddlAllExpYear.SelectedValue + ",";
                    sql += ddlUExp.SelectedValue + ",N'";
                    sql += (txtOtherPos.Visible ? (txtOtherPos.Text != "" ? txtOtherPos.Text : "") : "") + "','',N'";
                    sql += txtEName.Text + "',1)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@bod", Convert.ToDateTime(txtBOD.Text));
                    cmd.Parameters.AddWithValue("@hd", Convert.ToDateTime(txtHDate.Text));
                    cmd.ExecuteNonQuery();

                    cmdGet = new SqlCommand("Insert Into SectionsInserted values(2," + Session["uid"] + ")", conn);
                    cmdGet.ExecuteNonQuery();

                }
                else
                {
                    SqlCommand cmd = new SqlCommand("update instinfo set " +
                        "aname=@name,sname=@sn,birthdate=@bdate," +
                        "nat=@nat,sex=@sex,address=@address,email=@email,mobile=@mob,PositionName=@pn,college=@college," +
                        "dept=@dept,degree=@degree,degreeyear=@dgy,hiredate=@hdate,qual=@qual,major=@mj,minor=@mi,"+
                        "RSector=@rs,RField=@rf,allexp=@aexp,Uniexp=@uexp,otherPosName=@otposname,empimage=@eimg,ename=@ename where instjobid=" + Session["uid"], conn);
                    cmd.Parameters.AddWithValue("@name", txtAName.Text);
                    cmd.Parameters.AddWithValue("@sn", txtSName.Text);
                    cmd.Parameters.AddWithValue("@bdate", Convert.ToDateTime(txtBOD.Text));
                    cmd.Parameters.AddWithValue("@nat", ddlNat.SelectedValue);
                    cmd.Parameters.AddWithValue("@sex", ddlGender.SelectedValue);
                    cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@mob", txtMobile.Text);
                    cmd.Parameters.AddWithValue("@pn", ddlPositionName.SelectedValue);
                    cmd.Parameters.AddWithValue("@college", ddlCollege.SelectedValue);
                    cmd.Parameters.AddWithValue("@dept", ddlDept.SelectedValue);
                    cmd.Parameters.AddWithValue("@degree", ddlDegree.SelectedValue);
                    cmd.Parameters.AddWithValue("@dgy", ddlDegreeYear.SelectedValue);
                    cmd.Parameters.AddWithValue("@hdate", Convert.ToDateTime(txtHDate.Text));
                    cmd.Parameters.AddWithValue("@qual", ddlQual.SelectedValue);
                    cmd.Parameters.AddWithValue("@mj", txtMajor.Text);
                    cmd.Parameters.AddWithValue("@mi", txtMinor.Text);
                    cmd.Parameters.AddWithValue("@rs", ddlRSector.SelectedValue);
                    cmd.Parameters.AddWithValue("@rf", ddlField.SelectedValue);
                    cmd.Parameters.AddWithValue("@aexp", ddlAllExpYear.SelectedValue);
                    cmd.Parameters.AddWithValue("@uexp", ddlUExp.SelectedValue);
                    cmd.Parameters.AddWithValue("@otposname", txtOtherPos.Text);
                    cmd.Parameters.AddWithValue("@eimg", lblImageName.Text);
                    cmd.Parameters.AddWithValue("@ename", txtEName.Text);
                    cmd.ExecuteNonQuery();

                    try
                    {
                        cmdGet = new SqlCommand("Insert Into SectionsInserted values(2," + Session["uid"] + ")", conn);
                        cmdGet.ExecuteNonQuery();

                        

                    }
                    catch { }


                    //try
                    //{
                    //    cmdGet = new SqlCommand("Insert Into SectionsInserted values(3," + Session["uid"] + ")", conn);
                    //    cmdGet.ExecuteNonQuery();
                    //}

                    //catch { }
                    cmdGet = new SqlCommand("Insert Into SectionsUpdated values(1," + Session["uid"] + ",@du)", conn);
                    cmdGet.Parameters.AddWithValue("@du", Convert.ToDateTime(DateTime.Now.Date));
                    cmdGet.ExecuteNonQuery();

                }

                conn.Close();

                lblMsg.Visible = true;
                lblMsg.Text = "تم التخزين بنجاح";
                Session["saved"] = true;
                //Response.Redirect("ResearchInterests.aspx");
                //Timer1.Enabled = true;
                
            }
            catch
            {
                lblMsg.Visible = true;
                lblMsg.Text = "حصل خطأ أثناء التخزين";
                Timer1.Enabled = true;
                Session["saved"] = false;
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(Session["saved"]))
            {
                lblMsg.Visible = false;
                Timer1.Enabled = false;
                Response.Redirect("Qualifications.aspx");
            }
        }

        protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();


            SqlCommand cmd1 = new SqlCommand("select * from Department where CAutoid=" + ddlCollege.SelectedValue, conn);
            ddlDept.DataSource = cmd1.ExecuteReader();
            ddlDept.DataTextField = "DeptName";
            ddlDept.DataValueField = "AutoId";
            ddlDept.DataBind();
            ddlDept.Items.Insert(0, "حدد القسم");
            ddlDept.Items[0].Value = "0";


            conn.Close();
        }

        protected void ddlPositionName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPositionName.SelectedValue != "0")
            {
                if (ddlPositionName.SelectedValue == "4")
                    otherPosDiv.Visible = true;
                else
                    otherPosDiv.Visible = false;
            }
        }

        protected void ddlRSector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlRSector.SelectedValue!="0")
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand cmd = new SqlCommand("Select * From RFields where RSector=" + ddlRSector.SelectedValue, conn);
                ddlField.DataSource = cmd.ExecuteReader();
                ddlField.DataValueField = "id";
                ddlField.DataTextField = "RField";
                ddlField.DataBind();
                ddlField.Items.Insert(0, "حدد المجال البحثي");
                ddlField.Items[0].Value = "0";
                conn.Close();
            }
            else
            {
                ddlField.Items.Clear();
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            Response.Redirect("ResearchInterests.aspx");
        }
    }
}