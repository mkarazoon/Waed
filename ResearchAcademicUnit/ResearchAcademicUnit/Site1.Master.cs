using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchAcademicUnit
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userid"] == null)
                logDiv.Visible = false;
            else
                logDiv.Visible = true;

            if (Session["userName"] != null)
                lblUserName.Text = "أهلا بكم " + Session["userName"].ToString();

            lblDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");

            string path = HttpContext.Current.Request.Url.AbsolutePath;
            if (path.ToString() == "/Index.aspx")
                btnBack.Visible = false;
            else
                btnBack.Visible = true;

            if (path.ToString() == "/Default.aspx")
            {
                btnBack.Visible = false;
                btnPrint.Visible = false;
            }
            else
                btnBack.Visible = true;

            if (path.ToString().Contains("Index.aspx") || path.ToString().Contains("Default.aspx") || path.ToString().Contains("UploadData.aspx"))
            {
                btnPrint.Visible = false;
            }
            //else
            //    btnBack.Visible = false;
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd= new SqlCommand("Insert into LogFileLogin values(" + Session["userid"] + ",@d,'Logout')", conn);
            cmd.Parameters.AddWithValue("@d", DateTime.Now);
            cmd.ExecuteNonQuery();

            Session.Abandon();
            Session.Contents.RemoveAll();
            Session.Clear();
            FormsAuthentication.SignOut();

            Session["logSession"] = null;
            Session["backurl"] = null;
            Session["userid"] = null;
            Session["userrole"] = null;
            Session["userName"] = null;



            Response.Redirect("Login.aspx");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Session["RequestComeFrom"] = null;
            Session["ViewRequestFrom"] = null;
            Session["NotDefault"] = null;
            Session["justView"] = null;
            string d = Session["backurl"].ToString();
            Response.Redirect(Session["backurl"].ToString());
        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            if(Session["userrole"].ToString()!="10")
            Response.Redirect("Default.aspx");
        }
    }
}