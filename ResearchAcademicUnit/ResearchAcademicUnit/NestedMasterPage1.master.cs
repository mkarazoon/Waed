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
    public partial class NestedMasterPage1 : System.Web.UI.MasterPage
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["MEUCV"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["uid"] == null)
                    Response.Redirect("Login.aspx");
                //else
                //{
                //    if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                //        conn.Open();

                //    pinfo.Enabled = false;
                //    qual.Enabled = false;
                //    rCare.Enabled = false;
                //    links.Enabled = false;
                //    evalexp.Enabled = false;
                //    rpublished.Enabled = false;
                //    conf.Enabled = false;
                //    books.Enabled = false;
                //    train.Enabled = false;
                //    racheiv.Enabled = false;
                //    committe.Enabled = false;
                //    cert.Enabled = false;
                //    upload.Enabled = false;

                //    SqlCommand cmd = new SqlCommand("Select * From SectionsInserted where JobId=" + Session["uid"], conn);
                //    DataTable dt = new DataTable();
                //    dt.Load(cmd.ExecuteReader());
                //    for (int i = 0; i < dt.Rows.Count; i++)
                //    {
                //        switch (dt.Rows[i][0])
                //        {
                //            case 1:
                //                pinfo.Enabled = true;
                //                break;
                            
                                
                //                //break;
                //            case 2:
                //                rCare.Enabled = true;
                //                break;
                //            case 3:
                //            case 4:
                //            case 5:
                //            case 6:
                //            case 7:
                //            case 8:
                //            case 9:
                //            case 10:
                //            case 11:
                //            case 12:
                //            case 13:qual.Enabled = true;
                //                links.Enabled = true;
                //            //    break;
                //            //case 5:
                //                evalexp.Enabled = true;
                //            //    break;
                //            //case 7:
                //                rpublished.Enabled = true;
                //            //    break;
                //            //case 8:
                //                conf.Enabled = true;
                //            //    break;
                //            //case 9:
                //                books.Enabled = true;
                //            //    break;
                //            //case 10:
                //                train.Enabled = true;
                //            //    break;
                //            //case 11:
                //                racheiv.Enabled = true;
                //            //    break;
                //            //case 12:
                //                committe.Enabled = true;
                //            //    break;
                //            //case 13:
                //                cert.Enabled = true;
                //            //    break;
                //            //case 15:
                //                upload.Enabled = true;
                //                break;
                //        }
                //    }


                //conn.Close();
                //}
            }
            catch { }
        }

        protected void setActive(object sender, EventArgs e)
        {
            //nav1.Visible = true;
            LinkButton id = (LinkButton)sender;
            string n = id.ID;
            switch (n)
            {
                case "pinfo":
                    Session["currentURL"] = "PersonalInfo.aspx";
                    menu1.Attributes.Add("class", "active");
                    Response.Redirect("PersonalInfo.aspx");
                    break;
                case "qual":
                    Session["currentURL"] = "Qualifications.aspx";
                    Response.Redirect("Qualifications.aspx");
                    break;
                case "rCare":
                    Session["currentURL"] = "ResearchInterests.aspx";
                    Response.Redirect("ResearchInterests.aspx");
                    break;
                case "links":
                    Session["currentURL"] = "LinksDB.aspx";
                    Response.Redirect("LinksDB.aspx");
                    break;
                case "evalexp":
                    Session["currentURL"] = "EvaluationExp.aspx";
                    Response.Redirect("EvaluationExp.aspx");
                    break;
                case "rpublished":
                    Session["currentURL"] = "PublishedR.aspx";
                    Response.Redirect("PublishedR.aspx");
                    break;
                case "conf":
                    Session["currentURL"] = "Conference.aspx";
                    Response.Redirect("Conference.aspx");
                    break;
                case "books":
                    Session["currentURL"] = "Books.aspx";
                    Response.Redirect("Books.aspx");
                    break;
                case "train":
                    Session["currentURL"] = "WorkShops.aspx";
                    Response.Redirect("WorkShops.aspx");
                    break;
                case "racheiv":
                    Session["currentURL"] = "Achievements.aspx";
                    Response.Redirect("Achievements.aspx");
                    break;
                case "committe":
                    Session["currentURL"] = "RCommittees.aspx";
                    Response.Redirect("RCommittees.aspx");
                    break;
                case "cert":
                    Session["currentURL"] = "RCertificate.aspx";
                    Response.Redirect("RCertificate.aspx");
                    break;
                case "confirmData":
                    Session["currentURL"] = "Agreement.aspx";
                    Response.Redirect("Agreement.aspx");
                    break;
                case "upload":
                    Session["currentURL"] = "UploadFiles.aspx";
                    Response.Redirect("UploadFiles.aspx");
                    break;
                case "Resume":
                    Session["currentURL"] = "Resume.aspx";
                    Response.Redirect("Resume.aspx");
                    break;
                case "workexp":
                    Session["currentURL"] = "WorkExperience.aspx";
                    Response.Redirect("WorkExperience.aspx");
                    break;
                case "thesis":
                    Session["currentURL"] = "ThesisSup.aspx";
                    Response.Redirect("ThesisSup.aspx");
                    break;

            }
        }

    }
}