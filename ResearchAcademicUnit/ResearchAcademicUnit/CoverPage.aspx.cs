using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ResearchAcademicUnit
{
    public partial class CoverPage : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.IsNewSession || Session["uid"] == null)
                Response.Redirect("Login.aspx");
            HtmlGenericControl divh = (HtmlGenericControl)Page.Master.FindControl("prinOut");
            divh.Visible = false;

            HtmlGenericControl divf = (HtmlGenericControl)Page.Master.FindControl("printfooter");
            divf.Visible = false;

            if (!IsPostBack)
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmdd = new SqlCommand("select * from ResearcherInfo where acdid=" + Session["ResearchId"], conn);
                DataTable dtt = new DataTable();
                dtt.Load(cmdd.ExecuteReader());

                cmdd = new SqlCommand("select * from "+(Session["FormTypePrint"].ToString().Contains("رسوم")? "ResearchFeeInfo":"ResearchRewardForm")+" where AutoId=" + Session["ViewRequestFrom"], conn);
                DataTable dtForm = new DataTable();
                dtForm.Load(cmdd.ExecuteReader());

                cmdd = new SqlCommand(@"SELECT raname
                                      FROM ResearcherInfo r,Faculty f,Priviliges p
                                      where r.College=f.CollegeName and f.AutoId=p.PrivFacultyId
                                      and r.College like N'%" + dtt.Rows[0]["College"].ToString() + "%' and p.PrivTo=r.AcdId and p.PrivType=1", conn);
                SqlDataReader dr = cmdd.ExecuteReader();
                dr.Read();
                lblFacultyPrint.Text ="كلية " + dtt.Rows[0]["College"].ToString();
                lblFacultyNo.Text ="الرقم : " + dtForm.Rows[0][16].ToString();
                lblCoverDate.Text ="التاريخ : " +  DateTime.Now.Date.ToString("dd-MM-yyyy");
                FParagDiv.InnerHtml = @"أرفق لكم طيا نموذج " + (Session["FormTypePrint"].ToString().Contains("رسوم")? " طلب دعم نشر " : " مكافأة على نشر ") +"بحث علمي لد. " + dtt.Rows[0]["RaName"].ToString() + " " + dtt.Rows[0]["RLevel"].ToString() + " في قسم " + dtt.Rows[0]["Dept"].ToString() + " بكلية " + dtt.Rows[0]["College"].ToString() + " والمعنون بـ:";
                SParagDiv.InnerHtml = dtForm.Rows[0][6].ToString();
                TParagDiv.InnerHtml = dtForm.Rows[0][8].ToString() + "<br>رقم ISSN للمجلة: " + dtForm.Rows[0][9].ToString() + " والمصنفة ضمن قواعد بيانات Scopus";
                SigDiv.InnerHtml = @"<div style='position:relative;text-align:center'>عميد كلية " 
                + dtt.Rows[0]["College"].ToString() 
                //+ "<div style='font-family:Diwani Bent;font-size:20px'>" 
                //+ dr[0].ToString().Split(' ')[0]+ "</div>" 
                +"<p>"+ dr[0]+"</p>"
                    //+"<img src='signature/sign_gs.jpg' style='position:absolute;top:0;left:10%;z-index:-1;width:128px'/>"
                    +"</div>";
                if (Session["SecType"].ToString() == "Faculty")
                    gsDivWared.Visible = false;
                else
                {
                    gsDivWared.Visible = true;
                    lblGSIn.Text = dtForm.Rows[0][18].ToString();
                    lblGSDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                }
            }
        }
    }
}