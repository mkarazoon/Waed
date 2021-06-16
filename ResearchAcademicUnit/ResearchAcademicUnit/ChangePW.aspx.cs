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
    public partial class ChangePW : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        protected void Page_Load(object sender, EventArgs e)
        {
            Label lblUserVal = (Label)Page.Master.FindControl("lblPageName");
            lblUserVal.Text = "تغيير كلمة المرور";
            Session["backurl"] = "Index.aspx";
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(Session["backurl"].ToString());
        }

        protected void btnChange_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmdCheckPW = new SqlCommand("select * from users where acdid=" + Session["userid"] + " and pw=N'" + txtOldPW.Text + "'", conn);
            SqlDataReader dr = cmdCheckPW.ExecuteReader();

            if (dr.HasRows)
            {
                SqlCommand cmd = new SqlCommand("Update users set PW=N'" + txtNewPW.Text + "' where AcdId=" + Session["userid"], conn);
                cmd.ExecuteNonQuery();
                lblMsg.Text = "تم تغيير كلمة المرور بنجاح";
            }
            else
            {
                lblMsg.Text = "يوجد خطأ في كلمة المرور القديمة";
            }
            msgDiv.Visible = true;
            
            conn.Close();
        }
    }
}