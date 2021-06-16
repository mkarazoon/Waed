using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchAcademicUnit
{
    public partial class IndexUni : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["logSession"] == null || Session["userid"] == null || Session["userrole"].ToString()=="5" || Session["userrole"].ToString()=="6" || Session["userrole"].ToString() == "7")
            {
                OtherDiv.Visible = false;
            }
            Session["backurl"] = "Index.aspx";
            Label lblUserVal = (Label)Page.Master.FindControl("lblPageName");
            lblUserVal.Text = "نظام الاستعلام البحثي على مستوى الجامعة";

            //else
            //{
            //    if (!IsPostBack)
            //    {
            //        if (Session["logSession"].ToString() == "login")
            //        {
            //            logDiv.Visible = false;
            //            mainDiv.Visible = true;
            //        }
            //        else
            //        {
            //            logDiv.Visible = true;
            //            mainDiv.Visible = false;
            //        }
            //    }
            //}
        }

        //protected void btnUpload_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("UploadData.aspx");
        //}

        //protected void btnView_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("Researcher.aspx");
        //}

        //protected void btnResearcherInfo_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("Info.aspx");
        //}

        //protected void btnBack_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("Index.aspx");
        //}

        //protected void btnLogin_Click(object sender, EventArgs e)
        //{
        //    if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
        //        conn.Open();

        //    SqlCommand cmd = new SqlCommand("Select * From Users where AcdId=" + txtUser.Text + " and PW=N'" + txtPW.Text + "'", conn);
        //    SqlDataReader dr = cmd.ExecuteReader();
        //    if (dr.HasRows)
        //    {
        //        dr.Read();
        //        Session["logSession"] = "login";
        //        Session["userid"] = dr[0];
        //        Session["userrole"] = dr[2];
        //    }
        //    else
        //    {
        //        Session["logSession"] = "logout";
        //    }
        //    conn.Close();
        //    Response.Redirect("IndexUni.aspx");
        //}

        //protected void btnLogout_Click(object sender, EventArgs e)
        //{
        //    Session["logSession"] = "logout";
        //    Session["userid"] = null;
        //    Session["userrole"] = null;
        //    Response.Redirect("IndexUni.aspx");
        //}

        //protected void btnChangePW_Click(object sender, EventArgs e)
        //{
        //    Session["backurl"] = "indexUni.aspx";
        //    Response.Redirect("changepw.aspx");
        //}
    }
}