using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchAcademicUnit
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.IsNewSession || Session["userid"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            Session["backurl"] = "Default.aspx";
            switch (Session["userrole"].ToString())
            {
                case "6":
                    RDiv.Visible = true;
                    break;
                case "5":
                    RDiv.Visible = true;
                    CDiv.Visible = true;
                    break;
                case "1":
                case "2":
                case "3":
                case "4":
                    if (Session["userrole"].ToString() == "1")
                    {
                        upDiv.Visible = true;
                        //uni4.Visible = true;
                        uni6.Visible = true;
                        uni7.Visible = true;
                        uni8.Visible = true;
                        uni5.Visible = true;
                        //uni1.Visible = true;
                    }
                    uni3.Visible = true;
                    UDiv.Visible = true;
                    CDiv.Visible = true;
                    RDiv.Visible = true;
                    break;
                case "7":
                    CDiv.Visible = true;
                    RDiv.Visible = true;
                    break;
            }
            Label lblUserVal = (Label)Page.Master.FindControl("lblPageName");
            lblUserVal.Text = "نظام الاستعلام البحثي";
        }
    }
}