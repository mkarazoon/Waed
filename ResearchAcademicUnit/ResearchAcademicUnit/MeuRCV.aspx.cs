using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchAcademicUnit
{
    public partial class MeuRCV : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["MEUCV"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["meuuid"] == null)
            //    Response.Redirect("Login.aspx");

            //if (!IsPostBack)
            //{
            string id = Request.QueryString["id"];
            if (!string.IsNullOrEmpty(id))
            {
                Session["meuuid"] = id;
                fillPersonalData();
                fillQualData();
                fillPublishedResearch();
                fillOtherAcivities();
                fillCommittee();
                fillAchievement();
            }
            //}
        }

        protected void fillPersonalData()
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand("select * From InstInfo,Country,College,Department where code=nat and college=College.AutoId and dept=Department.AutoId and InstJobId=" + Session["meuuid"], conn);
            DataTable dtPersonalData = new DataTable();
            dtPersonalData.Load(cmd.ExecuteReader());
            if(dtPersonalData.Rows.Count!=0)
            {
                name.InnerText = dtPersonalData.Rows[0]["AName"].ToString();
                //imgR.Src = dtPersonalData.Rows[0]["EmpImage"].ToString();
                BOD.InnerText = "تاريخ الميلاد : " + Convert.ToDateTime(dtPersonalData.Rows[0]["BirthDate"]).ToShortDateString();
                Nat.InnerText = "الجنسية : " + dtPersonalData.Rows[0]["Name"].ToString();
                Address.InnerText = "العنوان : " + dtPersonalData.Rows[0]["Address"].ToString();
                Mobile.InnerText = "رقم الموبايل : " + dtPersonalData.Rows[0]["Mobile"].ToString();
                Email.InnerText = "البريد الالكتروني : " + dtPersonalData.Rows[0]["Email"].ToString();
                switch(dtPersonalData.Rows[0]["PositionName"])
                {
                    case 1:
                        PosName.InnerText = "المسمى الوظيفي : عميد";
                        break;
                    case 2:
                        PosName.InnerText = "المسمى الوظيفي : رئيس قسم";
                        break;
                    case 3:
                        PosName.InnerText = "المسمى الوظيفي : عضو هيئة تدريس";
                        break;
                    case 4:
                        PosName.InnerText = "المسمى الوظيفي : "+ dtPersonalData.Rows[0]["otherPosName"].ToString();
                        break;
                }

                College_Dept.InnerText = "كلية " + dtPersonalData.Rows[0]["CollegeName"].ToString() + " قسم " + dtPersonalData.Rows[0]["DeptName"].ToString();

                switch (dtPersonalData.Rows[0]["Degree"])
                {
                    case 1:
                        Degree.InnerText = "(استاذ)";
                        break;
                    case 2:
                        Degree.InnerText = "(استاذ مشارك)";
                        break;
                    case 3:
                        Degree.InnerText = "(استاذ مساعد)";
                        break;
                    case 4:
                        Degree.InnerText = "(مدرس)";
                        break;
                }

                
                switch (dtPersonalData.Rows[0]["Qual"])
                {
                    case 1:
                        Qual.InnerText = "المؤهل العلمي : دكتوراة";
                        break;
                    case 2:
                        Qual.InnerText = "المؤهل العلمي : ماجستير";
                        break;
                    case 3:
                        Qual.InnerText = "المؤهل العلمي : بكالوريوس";
                        break;
                }

                Major.InnerText = "التخصص العام : " + dtPersonalData.Rows[0]["Major"].ToString();
                Minor.InnerText = "التخصص الدقيق : " + dtPersonalData.Rows[0]["Minor"].ToString();
                Exp.InnerText = "سنوات الخبرة : " + dtPersonalData.Rows[0]["AllExp"].ToString();

            }

            conn.Close();
        }

        protected void fillQualData()
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            string sql = "";
            sql = "select * From Qualification,Country where GradCountry=Code and InstJobId=" + Session["meuuid"]+" order by AutoId DESC";
            SqlCommand cmd = new SqlCommand(sql, conn);
            DataTable dtQual = new DataTable();
            dtQual.Load(cmd.ExecuteReader());
            if (dtQual.Rows.Count != 0)
            {
                QualDiv.Visible = true;
                for (int i = 0; i < dtQual.Rows.Count; i++)
                {
                    switch (dtQual.Rows[i]["QualType"])
                    {
                        case "الدكتوراة":
                            PhDMain.Visible = true;
                            PhDYear.InnerText = dtQual.Rows[i]["GradYear"].ToString();
                            PhDUni.InnerText = "الجامعة : " + dtQual.Rows[i]["QualUniName"].ToString();
                            PhDCollege.InnerText = "الكلية : " + dtQual.Rows[i]["QualColName"].ToString();
                            PhDCountry.InnerText = "بلد التخرج : " + dtQual.Rows[i]["Name"].ToString();
                            PhDMajor.InnerText = "التخصص العام : " + dtQual.Rows[i]["Major"].ToString();
                            PhDMinor.InnerText = "التخصص الدقيق : " + dtQual.Rows[i]["Minor"].ToString();
                            switch (dtQual.Rows[i]["Degree"].ToString())
                            {
                                case "1":
                                    PhDDegree.InnerText = "التقدير : ممتاز";
                                    break;
                                case "2":
                                    PhDDegree.InnerText = "التقدير : جيد جدا";
                                    break;
                                case "3":
                                    PhDDegree.InnerText = "التقدير : جيد";
                                    break;
                                case "4":
                                    PhDDegree.InnerText = "التقدير : مقبول";
                                    break;
                            }
                            break;
                        case "الماجستير":
                            MsMain.Visible = true;
                            MsYear.InnerText = dtQual.Rows[i]["GradYear"].ToString();
                            MsUni.InnerText = "الجامعة : " + dtQual.Rows[i]["QualUniName"].ToString();
                            MsCollege.InnerText = "الكلية : " + dtQual.Rows[i]["QualColName"].ToString();
                            MsCountry.InnerText = "بلد التخرج : " + dtQual.Rows[i]["Name"].ToString();
                            MsMajor.InnerText = "التخصص العام : " + dtQual.Rows[i]["Major"].ToString();
                            MsMinor.InnerText = "التخصص الدقيق : " + dtQual.Rows[i]["Minor"].ToString();
                            switch (dtQual.Rows[i]["Degree"].ToString())
                            {
                                case "1":
                                    MsDegree.InnerText = "التقدير : ممتاز";
                                    break;
                                case "2":
                                    MsDegree.InnerText = "التقدير : جيد جدا";
                                    break;
                                case "3":
                                    MsDegree.InnerText = "التقدير : جيد";
                                    break;
                                case "4":
                                    MsDegree.InnerText = "التقدير : مقبول";
                                    break;
                            }
                            break;
                        case "البكالوريوس":
                            BscMain.Visible = true;
                            BscYear.InnerText = dtQual.Rows[i]["GradYear"].ToString();
                            BscUni.InnerText = "الجامعة : " + dtQual.Rows[i]["QualUniName"].ToString();
                            BscCollege.InnerText =  "الكلية : " + dtQual.Rows[i]["QualColName"].ToString();
                            BscCountry.InnerText = "بلد التخرج : " +dtQual.Rows[i]["Name"].ToString();
                            BscMajor.InnerText = "التخصص العام : " + dtQual.Rows[i]["Major"].ToString();
                            BscMinor.InnerText = "التخصص الدقيق : " + dtQual.Rows[i]["Minor"].ToString();
                            switch (dtQual.Rows[i]["Degree"].ToString())
                            {
                                case "1":
                                    BscDegree.InnerText = "التقدير : ممتاز";
                                    break;
                                case "2":
                                    BscDegree.InnerText = "التقدير : جيد جدا";
                                    break;
                                case "3":
                                    BscDegree.InnerText = "التقدير : جيد";
                                    break;
                                case "4":
                                    BscDegree.InnerText = "التقدير : مقبول";
                                    break;
                            }

                            break;
                    }
                }
            }
            conn.Close();
        }

        protected void fillPublishedResearch()
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            bool found = false;
            string sql = "";
            sql = "select * From Research where JobId=" + Session["meuuid"] + " order by PYear DESC";
            SqlCommand cmd = new SqlCommand(sql, conn);
            DataTable dtPubResearch = new DataTable();
            dtPubResearch.Load(cmd.ExecuteReader());
            grdPublishedResearch.DataSource = dtPubResearch;
            grdPublishedResearch.DataBind();
            if(grdPublishedResearch.Rows.Count!=0)
            {
                found = true;
                PublishedReDiv.Visible = true;
            }

            cmd = new SqlCommand("select * from Conference where jobid=" + Session["meuuid"] +" order by PYear", conn);
            grdConf.DataSource = cmd.ExecuteReader();
            grdConf.DataBind();
            if (grdConf.Rows.Count != 0)
            {
                found = true;
                ConfDiv.Visible = true;
            }

            cmd = new SqlCommand("select * from Books where jobid=" + Session["meuuid"] + " order by BookYear", conn);
            grdBook.DataSource = cmd.ExecuteReader();
            grdBook.DataBind();
            if (grdBook.Rows.Count != 0)
            {
                found = true;
                BookDiv.Visible = true;
            }


            if (found)
                AllResearchDiv.Visible = true;
            conn.Close();
        }

        protected void fillOtherAcivities()
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            bool found = false;
            string sql = "";
            sql = "select * From WorkShop where ActTypeI=1 and JobId=" + Session["meuuid"] + " order by FDate DESC";
            SqlCommand cmd = new SqlCommand(sql, conn);
            grdSeminar.DataSource = cmd.ExecuteReader();
            grdSeminar.DataBind();
            if (grdSeminar.Rows.Count != 0)
            {
                found = true;
                SeminarDiv.Visible = true;
            }

            cmd = new SqlCommand("select * from WorkShop where ActTypeI=3 and jobid=" + Session["meuuid"] + " order by FDate", conn);
            grdWorkShop.DataSource = cmd.ExecuteReader();
            grdWorkShop.DataBind();
            if (grdWorkShop.Rows.Count != 0)
            {
                found = true;
                WorkShopDiv.Visible = true;
            }

            cmd = new SqlCommand("select * from WorkShop where ActTypeI=2 and jobid=" + Session["meuuid"] + " order by FDate", conn);
            grdTrain.DataSource = cmd.ExecuteReader();
            grdTrain.DataBind();
            if (grdTrain.Rows.Count != 0)
            {
                found = true;
                TrainDiv.Visible = true;
            }

            sql = "select * From EvaluationExps where JobId=" + Session["meuuid"] + " order by TYear DESC";
            cmd = new SqlCommand(sql, conn);
            grdExp.DataSource = cmd.ExecuteReader();
            grdExp.DataBind();
            if (grdExp.Rows.Count != 0)
            {
                found = true;
                EvaluationDiv.Visible = true;
            }

            if (found)
                OtherActivitiesDiv.Visible = true;
            conn.Close();
        }

        protected void fillCommittee()
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            string sql = "";
            sql = "select * From Committee where JobId=" + Session["meuuid"] + " order by CommDate DESC";
            SqlCommand cmd = new SqlCommand(sql, conn);
            grdCommittee.DataSource = cmd.ExecuteReader();
            grdCommittee.DataBind();
            if (grdCommittee.Rows.Count != 0)
            {
                CommitteeDiv.Visible = true;
            }

        }

        protected void fillAchievement()
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            bool found = false;
            string sql = "";
            sql = "select * From Achievement where JobId=" + Session["meuuid"] + " order by AutoId";
            SqlCommand cmd = new SqlCommand(sql, conn);
            grdAchievement.DataSource = cmd.ExecuteReader();
            grdAchievement.DataBind();
            if (grdAchievement.Rows.Count != 0)
            {
                found = true;
                AchievementDiv.Visible = true;
            }

            sql = "select * From RCertificate where JobId=" + Session["meuuid"] + " order by CommDate DESC";
            cmd = new SqlCommand(sql, conn);
            grdCertificate.DataSource = cmd.ExecuteReader();
            grdCertificate.DataBind();
            if (grdCertificate.Rows.Count != 0)
            {
                found = true;
                CertificateDiv.Visible = true;
            }

            if (found)
                AchDiv.Visible = true;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}