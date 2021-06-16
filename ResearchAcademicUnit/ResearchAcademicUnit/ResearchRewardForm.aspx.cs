using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
//using System.Web.Mail;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ResearchAcademicUnit
{
    public partial class ResearchRewardForm : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);
        DataTable dtRInfo = new DataTable();
        DataTable dtRewardedInfo = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["NotDefault"] != null)
                Session["backurl"] = Session["NotDefault"];
            else
                Session["backurl"] = "Default.aspx";
            //if (Session["ViewRequestFrom"] != null && Session["ViewRequestFrom"].ToString() == "7")
            if(Session["PrintForm"]!=null )
                Session["backurl"] = "SecPrintRequests.aspx";
            HtmlGenericControl divh = (HtmlGenericControl)Page.Master.FindControl("prinOut");
            divh.Visible = false;

            HtmlGenericControl divf = (HtmlGenericControl)Page.Master.FindControl("printfooter");
            divf.Visible = false;

            if (Session.IsNewSession || Session["uid"] == null)
                Response.Redirect("Login.aspx");

            if (!IsPostBack)
            {
                try
                {
                    hfResearchId.Value = Session["ResearchId"].ToString();
                    dtRInfo.Columns.Add("Serial");
                    dtRInfo.Columns.Add("ReName");
                    Session["dtRInfo"] = dtRInfo;

                    dtRewardedInfo.Columns.Add("Serial");
                    dtRewardedInfo.Columns.Add("ReTitle");
                    dtRewardedInfo.Columns.Add("ReDate");
                    Session["dtRewardedInfo"] = dtRewardedInfo;


                    if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                        conn.Open();

                    SqlCommand cmdd = new SqlCommand("select * from ResearcherInfo where acdid=" + Session["ResearchId"], conn);
                    DataTable dtt = new DataTable();
                    dtt.Load(cmdd.ExecuteReader());
                    if (dtt.Rows.Count != 0)
                    {
                        lblRName.Text = dtt.Rows[0]["RaName"].ToString();
                        lblReqName.Text = "الباحث : " + dtt.Rows[0]["RaName"].ToString();
                        lblReqNameSig.Text = "الباحث : " + dtt.Rows[0]["RaName"].ToString();
                        lblFaculty.Text = dtt.Rows[0]["College"].ToString();
                        lblDept.Text = dtt.Rows[0]["Dept"].ToString();
                        lblDegree.Text = dtt.Rows[0]["RLevel"].ToString();
                        lblJobId.Text = Session["ResearchId"].ToString();


                        cmdd = new SqlCommand("Select * From Users where AcdId=" + lblJobId.Text, conn);
                        SqlDataReader drEmail = cmdd.ExecuteReader();
                        drEmail.Read();
                        txtEmail.Text = drEmail["Email"].ToString();

                        cmdd = new SqlCommand("Select * From Faculty where CollegeName=N'" + lblFaculty.Text + "'", conn);
                        SqlDataReader drr = cmdd.ExecuteReader();
                        drr.Read();
                        lblSaderNo.Text = drr[3].ToString();

                        cmdd = new SqlCommand("Select * from ResearchFeeInfo where JobId=" + Session["ResearchId"], conn);
                        DataTable dataTable = new DataTable();
                        dataTable.Load(cmdd.ExecuteReader());
                        if (dataTable.Rows.Count != 0)
                        {
                            txtHcst.Text = dataTable.Rows[0]["hcstID"].ToString();
                            txtOrcid.Text = dataTable.Rows[0]["orcidId"].ToString();
                            txtResearcherId.Text = dataTable.Rows[0]["researcherid"].ToString();
                            txtGSLink.Text = dataTable.Rows[0]["GSLink"].ToString();
                            txtRGLink.Text = dataTable.Rows[0]["RGLink"].ToString();
                        }
                        else
                        {
                            cmdd = new SqlCommand("Select * from ResearchRewardForm where JobId=" + Session["ResearchId"], conn);
                            dataTable = new DataTable();
                            dataTable.Load(cmdd.ExecuteReader());
                            if (dataTable.Rows.Count != 0)
                            {
                                txtHcst.Text = dataTable.Rows[0]["hcstID"].ToString();
                                txtOrcid.Text = dataTable.Rows[0]["orcidId"].ToString();
                                txtResearcherId.Text = dataTable.Rows[0]["researcherid"].ToString();
                                txtGSLink.Text = dataTable.Rows[0]["GSLink"].ToString();
                                txtRGLink.Text = dataTable.Rows[0]["RGLink"].ToString();
                            }
                        }

                        if (Session["RequestComeFrom"] != null || Session["ViewRequestFrom"] != null)
                        {
                            int ReqId = Convert.ToInt32(Session["RequestComeFrom"] != null ? Session["RequestComeFrom"] : Session["ViewRequestFrom"]);
                            reqid.InnerText = "رقم الطلب : " + Session["ViewRequestFrom"].ToString();
                            cmdd = new SqlCommand("Select * From ResearchRewardForm where AutoId=" + ReqId, conn);
                            SqlDataReader dr = cmdd.ExecuteReader();
                            dr.Read();
                            txtHcst.Text = dr[2].ToString();
                            txtOrcid.Text = dr[3].ToString();
                            txtGSLink.Text = dr[4].ToString();
                            txtRGLink.Text = dr[5].ToString();
                            txtRTitle.Text = dr[6].ToString();
                            rtDiv.InnerHtml = dr[6].ToString();
                            txtPublishDate.Text = dr[7].ToString();
                            txtMagName.Text = dr[8].ToString();
                            txtMagISSN.Text = dr[9].ToString();
                            txtPubName.Text = dr[10].ToString();
                            txtMagYear.Text = dr[11].ToString();
                            txtFolder.Text = dr[12].ToString();
                            txtVolume.Text = dr[13].ToString();
                            txtDBIndex.Text = dr["DBIndex"].ToString();
                            txtPages.Text = dr["Pages"].ToString();
                            rdPrevReward.SelectedValue = dr["RewardedBefore"].ToString();
                            rdCheckList1.SelectedValue = dr["chkList1"].ToString();
                            rdCheckList2.SelectedValue = dr["chkList2"].ToString();
                            rdCheckList3.SelectedValue = dr["chkList3"].ToString();
                            rdCheckList4.SelectedValue = dr["chkList4"].ToString();
                            rdCheckList5.SelectedValue = dr["chkList5"].ToString();
                            txtResearcherId.Text = dr["ResearcherId"].ToString();

                            cmdd = new SqlCommand("Select ROW_NUMBER() over(order by autoid) Serial,ReName From RePartInfoReward where RequestId=" + ReqId, conn);
                            dtRInfo = (DataTable)Session["dtRInfo"];
                            dtRInfo.Load(cmdd.ExecuteReader());
                            GridView1.DataSource = dtRInfo;
                            GridView1.DataBind();

                            if (rdPrevReward.SelectedValue == "نعم")
                            {
                                PrevRewardDiv.Visible = true;
                                cmdd = new SqlCommand("Select ROW_NUMBER() over(order by autoid) Serial,ReTitle,ReDate From RewardedInfo where FormId=" + ReqId, conn);
                                dtRewardedInfo = (DataTable)Session["dtRewardedInfo"];
                                dtRewardedInfo.Load(cmdd.ExecuteReader());
                                GridView2.DataSource = dtRewardedInfo;
                                GridView2.DataBind();
                            }
                            lblReqDate.Text = "التاريخ : " + Convert.ToDateTime(dr["ReqDate"]).ToString("dd-MM-yyyy");
                            var resultData = Directory.GetFiles(Server.MapPath("document/forms/reward/" + ReqId), "*.*", SearchOption.AllDirectories)
                                            .Select(x => new { FileName = Path.GetFileName(x), FilePath = x, DirName = Path.GetDirectoryName(x) });

                            foreach (var item in resultData)
                            {
                                if (item.FileName.Contains("2"))
                                {
                                    lnkViewF2.Visible = true;
                                    lblf2.Text = item.FilePath;
                                    RequiredFieldValidator11.Visible = false;
                                }
                            }

                            if (dr[14].ToString() == "T" || (Session["justView"] != null && Session["justView"].ToString() == "ok"))
                            {
                                txtHcst.ReadOnly = true;
                                txtEmail.ReadOnly = true;
                                txtOrcid.ReadOnly = true;
                                txtGSLink.ReadOnly = true;
                                txtRGLink.ReadOnly = true;
                                txtRTitle.ReadOnly = true;
                                //txtAcceptDate.ReadOnly = true;
                                txtMagName.ReadOnly = true;
                                txtMagISSN.ReadOnly = true;
                                txtPubName.ReadOnly = true;
                                txtMagYear.ReadOnly = true;
                                //txtFeeValue.ReadOnly = true;
                                txtPartReName.ReadOnly = true;
                                txtDBIndex.ReadOnly = true;
                                GridView1.Columns[2].Visible = false;
                                GridView2.Columns[3].Visible = false;
                                rdPrevReward.Enabled = false;
                                txtRewardedReTitle.Visible = false;
                                txtRewardedReDate.Visible = false;
                                Label6.Visible = false;
                                Label7.Visible = false;
                                txtPublishDate.ReadOnly = true;
                                txtFolder.ReadOnly = true;
                                txtVolume.ReadOnly = true;
                                txtPages.ReadOnly = true;
                                rdCheckList1.Enabled = false;
                                rdCheckList2.Enabled = false;
                                rdCheckList3.Enabled = false;
                                rdCheckList4.Enabled = false;
                                rdCheckList5.Enabled = false;
                                //ddlCurrency.Enabled = false;
                                lnkAddPartR.Visible = false;
                                lnkAddRewardedRe.Visible = false;
                                FileUpload1.Visible = false;
                                FileUpload2.Visible = false;
                                //FileUpload3.Visible = false;
                                //FileUpload4.Visible = false;
                                btnUpload.Visible = false;
                                btnSubmit.Visible = false;
                                txtResearcherId.ReadOnly = true;
                            }

                            if (Session["ViewRequestFrom"] != null)
                            {
                                if (Session["Dir_Dean_Priv"] != null && Session["Dir_Dean_Priv"].ToString() == "Dir")
                                    DirectorDiv.Visible = true;
                                if (Session["Dir_Dean_Priv"] != null && Session["Dir_Dean_Priv"].ToString() == "Dean")
                                {
                                    DirectorDiv.Visible = false;
                                    DeanDiv.Visible = true;
                                }
                                if (Session["Dir_Dean_Priv"] != null && Session["Dir_Dean_Priv"].ToString() == "ReDir")
                                {
                                    foreach (var item in resultData)
                                    {
                                        if (item.FileName.Contains("1"))
                                        {
                                            lnkViewF1.Visible = true;
                                            lblf1.Text = item.FilePath;
                                            reqFile1.Visible = false;
                                        }
                                    }
                                    DirectorDiv.Visible = true;
                                    ReDeanDiv.Visible = true;
                                    RDeanDecisionDiv.Visible = false;
                                    DeanDiv.Visible = true;
                                    ReDirectorDiv.Visible = true;
                                    lblff1.Visible = true;
                                    FileUpload1.Visible = true;
                                    reqFile1.Visible = true;

                                    SqlCommand cmdDirector = new SqlCommand("select Notes from RequestsFollowUp where type=1 and requestid=" + Session["ViewRequestFrom"] + " and ReqFromId =( SELECT p.[AutoId] FROM Priviliges p,Department d where p.PrivDeptId=d.autoid and d.DeptName=N'" + lblDept.Text + "')", conn);
                                    SqlDataReader drDirector = cmdDirector.ExecuteReader();
                                    drDirector.Read();
                                    txtDirNotes.Text = drDirector[0].ToString();
                                    txtDirNotes.ReadOnly = true;
                                    btnDirDecisionOk.Visible = false;
                                    btnDirDecisionNo.Visible = false;
                                    btnDirDecisionPost.Visible = false;


                                    SqlCommand cmdDean = new SqlCommand("select Notes from RequestsFollowUp where type=1 and requestid=" + Session["ViewRequestFrom"] + " and ReqFromId =( SELECT p.[AutoId] FROM Priviliges p,Faculty f where p.PrivFacultyId=f.autoid and p.privtype=1 and f.CollegeName=N'" + lblFaculty.Text + "')", conn);
                                    SqlDataReader drDean = cmdDean.ExecuteReader();
                                    drDean.Read();
                                    txtDeanNotes.Text = drDean[0].ToString();
                                    txtDeanNotes.ReadOnly = true;
                                    btnDeanDecisionOK.Visible = false;
                                    btnDeanDecisionNo.Visible = false;
                                    btnDeanDecisionPost.Visible = false;

                                    SqlCommand cmdDirInfo = new SqlCommand("select * from ReDirectorInfo where type=1 and requestid=" + Session["ViewRequestFrom"], conn);
                                    DataTable dtDirInfo = new DataTable();
                                    dtDirInfo.Load(cmdDirInfo.ExecuteReader());
                                    if (dtDirInfo.Rows.Count != 0)
                                    {
                                        txtyearmonth.Text = dtDirInfo.Rows[0][2].ToString();
                                        //txtFeeV.Text = dtDirInfo.Rows[0][3].ToString();
                                        txtReDirector.Text = dtDirInfo.Rows[0][16].ToString();

                                        chkFiles.Items[0].Selected = dtDirInfo.Rows[0][5].ToString() != "0" ? true : false;
                                        chkFiles.Items[1].Selected = dtDirInfo.Rows[0][6].ToString() != "0" ? true : false;
                                        chkFiles.Items[2].Selected = dtDirInfo.Rows[0][8].ToString() != "0" ? true : false;
                                        //chkFiles.Items[3].Selected = dtDirInfo.Rows[0][8].ToString() != "0" ? true : false;
                                        //chkFiles.Items[4].Selected = dtDirInfo.Rows[0][9].ToString() != "0" ? true : false;
                                        //chkFiles.Items[5].Selected = dtDirInfo.Rows[0][10].ToString() != "0" ? true : false;
                                        //chkFiles.Enabled = false;
                                        //txtReDirector.ReadOnly = true;
                                        //txtyearmonth.ReadOnly = true;
                                        rdQuarter.SelectedValue = dtDirInfo.Rows[0][4].ToString();

                                        rdCheck.SelectedValue = dtDirInfo.Rows[0][12].ToString();
                                        try
                                        {
                                            chkClarivate.Items[0].Selected = dtDirInfo.Rows[0][13].ToString() != "0" ? true : false;
                                            chkClarivate.Items[1].Selected = dtDirInfo.Rows[0][14].ToString() != "0" ? true : false;
                                            chkClarivate.Items[2].Selected = dtDirInfo.Rows[0][15].ToString() != "0" ? true : false;
                                        }
                                        catch { }
                                    }
                                    SqlCommand cmd = new SqlCommand("Select * From NewAmountAward where ReqId=" + Session["ViewRequestFrom"], conn);
                                    GridView3.DataSource = cmd.ExecuteReader();
                                    GridView3.DataBind();
                                    GridView3.Columns[5].Visible = false;

                                    SqlCommand cmdRName = new SqlCommand("Select AcdId,RaName,College from ResearcherInfo where Rstatus='IN'", conn);
                                    DataTable dtRInfoForNewAmount = new DataTable();
                                    dtRInfoForNewAmount.Load(cmdRName.ExecuteReader());
                                    Session["dtRInfoForNewAmount"] = dtRInfoForNewAmount;
                                    ddlRName.DataSource = dtRInfoForNewAmount;
                                    ddlRName.DataTextField = "RaName";
                                    ddlRName.DataValueField = "AcdId";
                                    ddlRName.DataBind();
                                    ddlRName.Items.Insert(0, "اختيار");
                                    ddlRName.Items[0].Value = "0";
                                    NewAmountDiv.Visible = true;
                                    AllRInfo.Visible = true;

                                }
                                if (Session["Dir_Dean_Priv"] != null && Session["Dir_Dean_Priv"].ToString() == "ReDean")
                                {
                                    foreach (var item in resultData)
                                    {
                                        if (item.FileName.Contains("1"))
                                        {
                                            lnkViewF1.Visible = true;
                                            lblf1.Text = item.FilePath;
                                            reqFile1.Visible = false;
                                        }
                                    }
                                    NewAmountDiv.Visible = false;
                                    lblff1.Visible = true;
                                    btnDirDecisionOk.Visible = false;
                                    btnDirDecisionNo.Visible = false;
                                    btnDirDecisionPost.Visible = false;
                                    btnDeanDecisionOK.Visible = false;
                                    btnDeanDecisionNo.Visible = false;
                                    btnDeanDecisionPost.Visible = false;
                                    btnReDirectorOk.Visible = false;
                                    btnReDirectorNo.Visible = false;
                                    btnReDirectorPost.Visible = false;
                                    ReDirSigDiv.Visible = true;
                                    DirectorDiv.Visible = true;
                                    DeanDiv.Visible = true;
                                    ReDirectorDiv.Visible = true;
                                    ReDeanDiv.Visible = true;
                                    btnReDirectorOk.Visible = false;
                                    rdCheck.Enabled = false;
                                    chkClarivate.Enabled = false;
                                    SqlCommand cmd = new SqlCommand("Select * From NewAmountAward where ReqId=" + Session["ViewRequestFrom"], conn);
                                    GridView3.DataSource = cmd.ExecuteReader();
                                    GridView3.DataBind();
                                    GridView3.Columns[6].Visible = false;

                                    SqlCommand cmdDirector = new SqlCommand("select Notes from RequestsFollowUp where type=1 and requestid=" + Session["ViewRequestFrom"] + " and ReqFromId =( SELECT p.[AutoId] FROM Priviliges p,Department d where p.PrivDeptId=d.autoid and d.DeptName=N'" + lblDept.Text + "')", conn);
                                    SqlDataReader drDirector = cmdDirector.ExecuteReader();
                                    drDirector.Read();
                                    txtDirNotes.Text = drDirector[0].ToString();
                                    txtDirNotes.ReadOnly = true;

                                    SqlCommand cmdDean = new SqlCommand("select Notes from RequestsFollowUp where type=1 and requestid=" + Session["ViewRequestFrom"] + " and ReqFromId =( SELECT p.[AutoId] FROM Priviliges p,Faculty f where p.PrivFacultyId=f.autoid and p.privtype=1 and f.CollegeName=N'" + lblFaculty.Text + "')", conn);
                                    SqlDataReader drDean = cmdDean.ExecuteReader();
                                    drDean.Read();
                                    txtDeanNotes.Text = drDean[0].ToString();
                                    txtDeanNotes.ReadOnly = true;

                                    SqlCommand cmdDirInfo = new SqlCommand("select * from ReDirectorInfo where type=1 and requestid=" + Session["ViewRequestFrom"], conn);
                                    DataTable dtDirInfo = new DataTable();
                                    dtDirInfo.Load(cmdDirInfo.ExecuteReader());
                                    if (dtDirInfo.Rows.Count != 0)
                                    {
                                        txtyearmonth.Text = dtDirInfo.Rows[0][2].ToString();
                                        //txtFeeV.Text = dtDirInfo.Rows[0][3].ToString();
                                        txtReDirector.Text = dtDirInfo.Rows[0][16].ToString();
                                        lblReDirDate.Text = Convert.ToDateTime(dtDirInfo.Rows[0][17]).ToString("dd-MM-yyyy");

                                        chkFiles.Items[0].Selected = dtDirInfo.Rows[0][5].ToString() != "0" ? true : false;
                                        chkFiles.Items[1].Selected = dtDirInfo.Rows[0][6].ToString() != "0" ? true : false;
                                        chkFiles.Items[2].Selected = dtDirInfo.Rows[0][8].ToString() != "0" ? true : false;
                                        //chkFiles.Items[3].Selected = dtDirInfo.Rows[0][8].ToString() != "0" ? true : false;
                                        //chkFiles.Items[4].Selected = dtDirInfo.Rows[0][9].ToString() != "0" ? true : false;
                                        //chkFiles.Items[5].Selected = dtDirInfo.Rows[0][10].ToString() != "0" ? true : false;
                                        rdCheck.SelectedValue = dtDirInfo.Rows[0][12].ToString();
                                        try
                                        {
                                            chkClarivate.Items[0].Selected = dtDirInfo.Rows[0][13].ToString() != "0" ? true : false;
                                            chkClarivate.Items[1].Selected = dtDirInfo.Rows[0][14].ToString() != "0" ? true : false;
                                            chkClarivate.Items[2].Selected = dtDirInfo.Rows[0][15].ToString() != "0" ? true : false;
                                        }
                                        catch { }

                                        chkFiles.Enabled = false;
                                        txtReDirector.ReadOnly = true;
                                        txtyearmonth.ReadOnly = true;
                                        rdQuarter.SelectedValue = dtDirInfo.Rows[0][4].ToString();
                                        rdQuarter.Enabled = false;
                                        //txtReDeanValue.Text = "300 دينار أردني";
                                        //switch(rdQuarter.SelectedValue)
                                        //{
                                        //    case "1":
                                        //        txtReDeanValue.Text = "600 دينار أردني";
                                        //        break;
                                        //    case "2":
                                        //        txtReDeanValue.Text = "500 دينار أردني";
                                        //        break;
                                        //    case "3":
                                        //        txtReDeanValue.Text = "400 دينار أردني";
                                        //        break;
                                        //}
                                        lblReDeanDate.Text = "التاريخ : " + DateTime.Now.Date.ToString("dd-MM-yyyy");
                                        //txtFeeV.ReadOnly = true;
                                    }
                                    //cmdd = new SqlCommand("Select Notes,RequestDate From RequestsFollowUp ru,researcherinfo ri where ru.actualid=ri.AcdId and Autoid=(Select Max(AutoId) From RequestsFollowUp where RequestId=" + Session["ViewRequestFrom"] + " and ReqFromName like N'%رئيس قسم البحث العلمي%' and RquToName like N'%عميد الدراسات العليا والبحث العلمي%')", conn);
                                    //SqlDataReader drdirDNotes1 = cmdd.ExecuteReader();
                                    //drdirDNotes1.Read();
                                    //txtReDirector.Text = drdirDNotes1[0].ToString();
                                    //lblReDirDate.Text = Convert.ToDateTime(drdirDNotes1[1]).ToString("dd-MM-yyyy");
                                }
                                if (Session["PrintForm"] != null)
                                {
                                    foreach (var item in resultData)
                                    {
                                        if (item.FileName.Contains("1"))
                                        {
                                            lnkViewF1.Visible = true;
                                            lblf1.Text = item.FilePath;
                                            reqFile1.Visible = false;
                                        }
                                    }
                                    //DirectorDiv.Visible = false;
                                    //DeanDiv.Visible = false;
                                    //ReDirectorDiv.Visible = true;
                                    lblff1.Visible = true;
                                    //FileUpload1.Visible = true;
                                    //reqFile1.Visible = true;

                                }
                                try
                                {
                                    lblff1.Visible = true;
                                    cmdd = new SqlCommand("Select ActualId,RequestDate,RaName From RequestsFollowUp ru,researcherinfo ri,Priviliges p where ReqFromId=p.autoid and p.PrivTo=ri.AcdId and ru.type=1 and ru.Autoid=(Select Max(AutoId) From RequestsFollowUp where type=1 and RequestId=" + Session["ViewRequestFrom"] + " and ReqFromName like N'%رئيس قسم%' and RquToName like N'%عميد كلية%')", conn);
                                    SqlDataReader drdirDNotes = cmdd.ExecuteReader();
                                    drdirDNotes.Read();
                                    lblDirName.Text = "رئيس القسم : " + drdirDNotes[2].ToString();
                                    lblDirNameSig.Text = "التوقيع : " + drdirDNotes[2].ToString();
                                    lblDirDate.Text = "التاريخ : " + Convert.ToDateTime(drdirDNotes[1]).ToString("dd-MM-yyyy");

                                    cmdd = new SqlCommand("Select Notes,RequestDate,RaName From RequestsFollowUp ru,researcherinfo ri ,priviliges p where  p.AutoId=ru.ReqFromId and ru.ActualId=ri.AcdId and type=1 and ru.Autoid=(Select Max(AutoId) From RequestsFollowUp where type=1 and RequestId=" + Session["ViewRequestFrom"] + " and ReqFromName like N'%عميد كلية%')", conn);
                                    SqlDataReader drDNotes = cmdd.ExecuteReader();
                                    drDNotes.Read();
                                    deanDecDiv.InnerText = drDNotes[0].ToString();
                                    deanDecDivSig.InnerHtml = "الاسم : " + drDNotes[2].ToString() + "<br> التوقيع : " + drDNotes[2].ToString();
                                    lblDeanDecDate.Text = "التاريخ : " + Convert.ToDateTime(drDNotes[1]).ToString("dd-MM-yyyy");

                                    cmdd = new SqlCommand("Select Notes,RequestDate,RaName From RequestsFollowUp,researcherinfo ri where actualid=ri.AcdId and type=1 and Autoid=(Select Max(AutoId) From RequestsFollowUp where type=1 and RequestId=" + Session["ViewRequestFrom"] + " and RquToName like N'%البحث العلمي%')", conn);
                                    SqlDataReader drRDirNotes = cmdd.ExecuteReader();
                                    drRDirNotes.Read();
                                    RDirDecDiv.InnerText = drRDirNotes[0].ToString();
                                    RDirDecDivSig.InnerHtml = "الاسم : " + drRDirNotes[2].ToString() + "<br> التوقيع : " + drRDirNotes[2].ToString();
                                    Label2.Text = "التاريخ : " + Convert.ToDateTime(drRDirNotes[1]).ToString("dd-MM-yyyy");

                                    cmdd = new SqlCommand("Select * From ReDirectorInfo where Type=1 and requestid=" + Session["ViewRequestFrom"], conn);
                                    SqlDataReader drQrt = cmdd.ExecuteReader();
                                    drQrt.Read();
                                    string qrt = "";
                                    switch (drQrt["Quarter"].ToString())
                                    {
                                        case "1":
                                            qrt = "( الأول )( Q1 )";
                                            break;
                                        case "2":
                                            qrt = "( الثاني )( Q2 )";
                                            break;
                                        case "3":
                                            qrt = "( الثالث )( Q3 )";
                                            break;
                                        case "4":
                                            qrt = "( الرابع )( Q4 )";
                                            break;
                                        case "5":
                                            qrt = "( Non Qs )";
                                            break;
                                    }

                                    qrtDiv.InnerHtml = "المجلة مصنفة بالربع " + qrt;

                                    if (chkClarivate.Items[0].Selected)
                                        ReDirCheckList.InnerHtml += chkClarivate.Items[0].Text;
                                    if (chkClarivate.Items[1].Selected)
                                        ReDirCheckList.InnerHtml += chkClarivate.Items[1].Text;
                                    if (chkClarivate.Items[2].Selected)
                                        ReDirCheckList.InnerHtml += chkClarivate.Items[2].Text;


                                    cmdd = new SqlCommand("Select Notes,RequestDate,RaName From RequestsFollowUp,researcherinfo ri where actualid=ri.AcdId and type=1 and Autoid=(Select Max(AutoId) From RequestsFollowUp where type=1 and RequestId=" + Session["ViewRequestFrom"] + " and ReqStatus like N'%مكتمل%')", conn);
                                    SqlDataReader drRDNotes = cmdd.ExecuteReader();
                                    drRDNotes.Read();
                                    RdeanDecDiv.InnerText = drRDNotes[0].ToString();

                                    RdeanDecDiv.InnerHtml += "<div style='margin-top:10px;margin-bottom:10px; font-family: 'Khalid Art';'><table width='100%' style='border:1px solid black;border-spacing:0' border=1>";
                                    RdeanDecDiv.InnerHtml += "<thead><th>اسم الباحث</th><th>الكلية</th><th>الترتيب</th><th>قيمة المكافأة</th></thead>";
                                    SqlCommand cmdTable = new SqlCommand("Select * From NewAmountAward where ReqId=" + Session["ViewRequestFrom"], conn);
                                    DataTable dtTable = new DataTable();
                                    dtTable.Load(cmdTable.ExecuteReader());
                                    GridView3.DataSource = dtTable;
                                    GridView3.DataBind();
                                    for (int i = 0; i < GridView3.Rows.Count; i++)
                                    {
                                        RdeanDecDiv.InnerHtml += "<tr><td>" + GridView3.Rows[i].Cells[3].Text + "</td><td>" + GridView3.Rows[i].Cells[4].Text + "</td><td>" + GridView3.Rows[i].Cells[5].Text + "</td><td>" +((Label)GridView3.Rows[i].Cells[6].FindControl("Label1")).Text + "</td></tr>";
                                    }
                                    RdeanDecDiv.InnerHtml += "</table></div>";


                                    RdeanDecDivSig.InnerHtml = "الاسم : " + drRDNotes[2].ToString() + "<br> التوقيع :" + drRDNotes[2].ToString();
                                    lblRDeanDecDate.Text = "التاريخ : " + Convert.ToDateTime(drRDNotes[1]).ToString("dd-MM-yyyy");
                                    foreach (var item in resultData)
                                    {
                                        if (item.FileName.Contains("1"))
                                        {
                                            lblff1.Visible = true;
                                            lnkViewF1.Visible = true;
                                            lblf1.Text = item.FilePath;
                                            reqFile1.Visible = false;
                                        }
                                    }

                                }
                                catch { }
                            }
                        }
                        conn.Close();
                    }
                }
                catch { }
            }
        }

        protected void lnkAddPartR_Click(object sender, EventArgs e)
        {
            dtRInfo = (DataTable)Session["dtRInfo"];
            DataRow row = dtRInfo.NewRow();
            row[0] = dtRInfo.Rows.Count + 1;
            row[1] = txtPartReName.Text;
            dtRInfo.Rows.Add(row);
            Session["dtRInfo"] = dtRInfo;
            txtPartReName.Text = "";
            GridView1.DataSource = dtRInfo;
            GridView1.DataBind();
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            dtRInfo = (DataTable)Session["dtRInfo"];
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            dtRInfo.Rows.RemoveAt(row.RowIndex);
            GridView1.DataSource = dtRInfo;
            GridView1.DataBind();
            for (int i = 0; i < GridView1.Rows.Count; i++)
                GridView1.Rows[i].Cells[0].Text = (i + 1).ToString();

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (rdPrevReward.SelectedValue == "لا" || (rdPrevReward.SelectedValue == "نعم" && GridView2.Rows.Count != 0))
            {
                if (GridView1.Rows.Count != 0)
                {
                    if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                        conn.Open();
                    string sql = "";
                    SqlCommand cmd;

                    DataTable dtt = new DataTable();
                    if (Session["RequestComeFrom"] == null)
                    {
                        sql = "Insert into ResearchRewardForm values(@rid,@hcst,@orcid,@gslink,@rglink,@rtitle,@pubDate,@magname,@magissn,@pub,@year,@folder,@vol,'T',@ReqDate,'',N'قيد الدراسة','','',@DBIndex,@Pages,@Rewarded,@chk1,@chk2,@chk3,@chk4,@chk5,@researcherid,0)";
                        cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@rid", Session["uid"].ToString());
                        cmd.Parameters.AddWithValue("@hcst", txtHcst.Text);
                        cmd.Parameters.AddWithValue("@orcid", txtOrcid.Text);
                        cmd.Parameters.AddWithValue("@gslink", txtGSLink.Text);
                        cmd.Parameters.AddWithValue("@rglink", txtRGLink.Text);
                        cmd.Parameters.AddWithValue("@rtitle", txtRTitle.Text);
                        cmd.Parameters.AddWithValue("@magname", txtMagName.Text);
                        cmd.Parameters.AddWithValue("@magissn", txtMagISSN.Text);
                        cmd.Parameters.AddWithValue("@pub", txtPubName.Text);
                        cmd.Parameters.AddWithValue("@year", txtMagYear.Text);
                        cmd.Parameters.AddWithValue("@folder", txtFolder.Text);
                        cmd.Parameters.AddWithValue("@vol", txtVolume.Text);
                        cmd.Parameters.AddWithValue("@ReqDate", DateTime.Now.Date);
                        cmd.Parameters.AddWithValue("@pubDate", txtPublishDate.Text);
                        cmd.Parameters.AddWithValue("@DBIndex", txtDBIndex.Text);
                        cmd.Parameters.AddWithValue("@Pages", txtPages.Text);
                        cmd.Parameters.AddWithValue("@Rewarded", rdPrevReward.SelectedValue);
                        cmd.Parameters.AddWithValue("@chk1", rdCheckList1.SelectedValue);
                        cmd.Parameters.AddWithValue("@chk2", rdCheckList2.SelectedValue);
                        cmd.Parameters.AddWithValue("@chk3", rdCheckList3.SelectedValue);
                        cmd.Parameters.AddWithValue("@chk4", rdCheckList4.SelectedValue);
                        cmd.Parameters.AddWithValue("@chk5", rdCheckList5.SelectedValue);
                        cmd.Parameters.AddWithValue("@researcherid", txtResearcherId.Text);

                        cmd.ExecuteNonQuery();

                        cmd = new SqlCommand("select max(autoid) from ResearchRewardForm where jobid=" + Session["uid"], conn);
                        //DataTable dtt = new DataTable();
                        dtt.Load(cmd.ExecuteReader());
                        Session["RequestComeFrom"] = dtt.Rows[0][0];

                        for (int i = 0; i < GridView1.Rows.Count; i++)
                        {
                            cmd = new SqlCommand("Insert into RePartInfoReward values(" + dtt.Rows[0][0] + ",N'" + GridView1.Rows[i].Cells[1].Text + "')", conn);
                            cmd.ExecuteNonQuery();
                        }

                        for (int i = 0; i < GridView2.Rows.Count; i++)
                        {
                            cmd = new SqlCommand("Insert into RewardedInfo values(" + dtt.Rows[0][0] + ",N'" + GridView2.Rows[i].Cells[1].Text + "',N'" + GridView2.Rows[i].Cells[2].Text + "')", conn);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        sql = "Update ResearchRewardForm Set ";
                        sql += "hcstID=@hcst,orcidId=@orcid,GSLink=@gslink,RGLink=@rglink,";
                        sql += "ReTitle=@rtitle,AcceptDate=@pubDate,MagName=@magname,MagISSN=@magissn,";
                        sql += "PublisherName=@pub,MagYear=@year,FolderMag=@folder,Volume=@vol,";
                        sql += "RequestType='T',RequestFinalStatus=N'قيد الدراسة',DBIndex=@DBIndex,";
                        sql += "Pages=@Pages,RewardedBefore=@Rewarded,";
                        sql += "chkList1=@chk1,chkList2=@chk2,chkList3=@chk3,chkList4=@chk4,chkList5=@chk5";
                        sql += ",researcherid=@researcherid where Autoid=" + Session["RequestComeFrom"];
                        cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@hcst", txtHcst.Text);
                        cmd.Parameters.AddWithValue("@orcid", txtOrcid.Text);
                        cmd.Parameters.AddWithValue("@gslink", txtGSLink.Text);
                        cmd.Parameters.AddWithValue("@rglink", txtRGLink.Text);
                        cmd.Parameters.AddWithValue("@rtitle", txtRTitle.Text);
                        cmd.Parameters.AddWithValue("@magname", txtMagName.Text);
                        cmd.Parameters.AddWithValue("@magissn", txtMagISSN.Text);
                        cmd.Parameters.AddWithValue("@pub", txtPubName.Text);
                        cmd.Parameters.AddWithValue("@year", txtMagYear.Text);
                        cmd.Parameters.AddWithValue("@folder", txtFolder.Text);
                        cmd.Parameters.AddWithValue("@vol", txtVolume.Text);
                        cmd.Parameters.AddWithValue("@pubDate", txtPublishDate.Text);
                        cmd.Parameters.AddWithValue("@DBIndex", txtDBIndex.Text);
                        cmd.Parameters.AddWithValue("@Pages", txtPages.Text);
                        cmd.Parameters.AddWithValue("@Rewarded", rdPrevReward.SelectedValue);
                        cmd.Parameters.AddWithValue("@chk1", rdCheckList1.SelectedValue);
                        cmd.Parameters.AddWithValue("@chk2", rdCheckList2.SelectedValue);
                        cmd.Parameters.AddWithValue("@chk3", rdCheckList3.SelectedValue);
                        cmd.Parameters.AddWithValue("@chk4", rdCheckList4.SelectedValue);
                        cmd.Parameters.AddWithValue("@chk5", rdCheckList5.SelectedValue);
                        cmd.Parameters.AddWithValue("@researcherid", txtResearcherId.Text);

                        cmd.ExecuteNonQuery();

                        cmd = new SqlCommand("delete from RePartInfoReward where RequestId=" + Session["RequestComeFrom"], conn);
                        cmd.ExecuteNonQuery();

                        cmd = new SqlCommand("delete from RewardedInfo where FormId=" + Session["RequestComeFrom"], conn);
                        cmd.ExecuteNonQuery();

                        for (int i = 0; i < GridView1.Rows.Count; i++)
                        {
                            cmd = new SqlCommand("Insert into RePartInfoReward values(" + Session["RequestComeFrom"] + ",N'" + GridView1.Rows[i].Cells[1].Text + "')", conn);
                            cmd.ExecuteNonQuery();
                        }

                        for (int i = 0; i < GridView2.Rows.Count; i++)
                        {
                            cmd = new SqlCommand("Insert into RewardedInfo values(" + Session["RequestComeFrom"] + ",N'" + GridView2.Rows[i].Cells[1].Text + "',N'" + GridView2.Rows[i].Cells[2].Text + "')", conn);
                            cmd.ExecuteNonQuery();
                        }

                    }

                    upload();
                    Session["ReDirectorFinal"] = "no";
                    ConfirmDiv.Visible = true;

                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('يجب إدخال اسماء المشاركين بالبحث');", true);
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('يجب إدخال معلومات البحوث المدعومة');", true);
            }
        }

        protected void sendEmail(string email,string msg)
        {
            //send email
            var smtp = new SmtpClient
            {
                Host = "smtp.office365.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("Researchoffice@meu.edu.jo", "Qad11204")
            };
            using (var message = new MailMessage("Researchoffice@meu.edu.jo", email)
            {
                IsBodyHtml=true,
                Subject = "طلب مكافأة على نشر بحث علمي",
                Body =msg

            })
            {
                smtp.Send(message);
            }

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            if (Session["RequestComeFrom"] == null)
            {
                string sql = "Insert into ResearchRewardForm values(@rid,@hcst,@orcid,@gslink,@rglink,@rtitle,@pubDate,@magname,@magissn,@pub,@year,@folder,@vol,'NT',@ReqDate,'',N'قيد الدراسة','','',@DBIndex,@Pages,@Rewarded,@chk1,@chk2,@chk3,@chk4,@chk5,@researcherid,0)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@rid", Session["uid"].ToString());
                cmd.Parameters.AddWithValue("@hcst", txtHcst.Text);
                cmd.Parameters.AddWithValue("@orcid", txtOrcid.Text);
                cmd.Parameters.AddWithValue("@gslink",  txtGSLink.Text);
                cmd.Parameters.AddWithValue("@rglink",  txtRGLink.Text);
                cmd.Parameters.AddWithValue("@rtitle", txtRTitle.Text);
                cmd.Parameters.AddWithValue("@magname", txtMagName.Text);
                cmd.Parameters.AddWithValue("@magissn", txtMagISSN.Text);
                cmd.Parameters.AddWithValue("@pub", txtPubName.Text);
                cmd.Parameters.AddWithValue("@year", txtMagYear.Text);
                cmd.Parameters.AddWithValue("@folder", txtFolder.Text);
                cmd.Parameters.AddWithValue("@vol", txtVolume.Text);
                cmd.Parameters.AddWithValue("@ReqDate", DateTime.Now.Date);
                cmd.Parameters.AddWithValue("@pubDate", txtPublishDate.Text);
                cmd.Parameters.AddWithValue("@DBIndex", txtDBIndex.Text);
                cmd.Parameters.AddWithValue("@Pages", txtPages.Text);
                cmd.Parameters.AddWithValue("@Rewarded", rdPrevReward.SelectedValue);
                cmd.Parameters.AddWithValue("@chk1", rdCheckList1.SelectedValue);
                cmd.Parameters.AddWithValue("@chk2", rdCheckList2.SelectedValue);
                cmd.Parameters.AddWithValue("@chk3", rdCheckList3.SelectedValue);
                cmd.Parameters.AddWithValue("@chk4", rdCheckList4.SelectedValue);
                cmd.Parameters.AddWithValue("@chk5", rdCheckList5.SelectedValue);
                cmd.Parameters.AddWithValue("@researcherid", txtResearcherId.Text);

                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("select max(autoid) from ResearchRewardForm where jobid=" + Session["uid"], conn);
                DataTable dtt = new DataTable();
                dtt.Load(cmd.ExecuteReader());
                Session["RequestComeFrom"] = dtt.Rows[0][0];

                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    cmd = new SqlCommand("Insert into RePartInfoReward values(" + dtt.Rows[0][0] + ",N'" + GridView1.Rows[i].Cells[1].Text + "')", conn);
                    cmd.ExecuteNonQuery();
                }

                for(int i=0;i<GridView2.Rows.Count;i++)
                {
                    cmd = new SqlCommand("Insert into RewardedInfo values(" + dtt.Rows[0][0] + ",N'" + GridView2.Rows[i].Cells[1].Text + "',N'"+ GridView2.Rows[i].Cells[2].Text + "')", conn);
                    cmd.ExecuteNonQuery();
                }
            }
            else
            {
                string sql = "Update ResearchRewardForm Set ";
                sql += "hcstID=@hcst,orcidId=@orcid,GSLink=@gslink,RGLink=@rglink,";
                sql += "ReTitle=@rtitle,AcceptDate=@pubDate,MagName=@magname,MagISSN=@magissn,";
                sql += "PublisherName=@pub,MagYear=@year,FolderMag=@folder,Volume=@vol,";
                sql += "RequestType='NT',RequestFinalStatus=N'قيد الدراسة',DBIndex=@DBIndex,";
                sql += "Pages=@Pages,RewardedBefore=@Rewarded,";
                sql += "chkList1=@chk1,chkList2=@chk2,chkList3=@chk3,chkList4=@chk4,chkList5=@chk5";
                sql += ",researcherid=@researcherid where Autoid=" + Session["RequestComeFrom"];
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@hcst", txtHcst.Text);
                cmd.Parameters.AddWithValue("@orcid", txtOrcid.Text);
                cmd.Parameters.AddWithValue("@gslink", txtGSLink.Text);
                cmd.Parameters.AddWithValue("@rglink", txtRGLink.Text);
                cmd.Parameters.AddWithValue("@rtitle", txtRTitle.Text);
                cmd.Parameters.AddWithValue("@magname", txtMagName.Text);
                cmd.Parameters.AddWithValue("@magissn", txtMagISSN.Text);
                cmd.Parameters.AddWithValue("@pub", txtPubName.Text);
                cmd.Parameters.AddWithValue("@year", txtMagYear.Text);
                cmd.Parameters.AddWithValue("@folder", txtFolder.Text);
                cmd.Parameters.AddWithValue("@vol", txtVolume.Text);
                cmd.Parameters.AddWithValue("@pubDate", txtPublishDate.Text);
                cmd.Parameters.AddWithValue("@DBIndex", txtDBIndex.Text);
                cmd.Parameters.AddWithValue("@Pages", txtPages.Text);
                cmd.Parameters.AddWithValue("@Rewarded", rdPrevReward.SelectedValue);
                cmd.Parameters.AddWithValue("@chk1", rdCheckList1.SelectedValue);
                cmd.Parameters.AddWithValue("@chk2", rdCheckList2.SelectedValue);
                cmd.Parameters.AddWithValue("@chk3", rdCheckList3.SelectedValue);
                cmd.Parameters.AddWithValue("@chk4", rdCheckList4.SelectedValue);
                cmd.Parameters.AddWithValue("@chk5", rdCheckList5.SelectedValue);
                cmd.Parameters.AddWithValue("@researcherid", txtResearcherId.Text);

                cmd.ExecuteNonQuery();


                cmd = new SqlCommand("delete from RePartInfoReward where RequestId=" + Session["RequestComeFrom"], conn);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("delete from RewardedInfo where FormId=" + Session["RequestComeFrom"], conn);
                cmd.ExecuteNonQuery();

                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    cmd = new SqlCommand("Insert into RePartInfoReward values(" + Session["RequestComeFrom"] + ",N'" + GridView1.Rows[i].Cells[1].Text + "')", conn);
                    cmd.ExecuteNonQuery();
                }

                for (int i = 0; i < GridView2.Rows.Count; i++)
                {
                    cmd = new SqlCommand("Insert into RewardedInfo values(" + Session["RequestComeFrom"] + ",N'" + GridView2.Rows[i].Cells[1].Text + "',N'" + GridView2.Rows[i].Cells[2].Text + "')", conn);
                    cmd.ExecuteNonQuery();
                }

            }

            SqlCommand cmdEmail = new SqlCommand("Update Users Set Email='" + txtEmail.Text + "' where AcdId=" + lblJobId.Text, conn);
            cmdEmail.ExecuteNonQuery();
            Session["UserEmail"] = txtEmail.Text;
            upload();

            msgDiv.Visible = true;
            lblMsg.Text = "تم التخزين بشكل ناجح مع إمكانية التعديل على البيانات";

        }

        protected void upload()
        {
            int ReqId = Convert.ToInt32(Session["RequestComeFrom"] != null ? Session["RequestComeFrom"] : Session["ViewRequestFrom"]);
            string subPath = "document/forms/reward/" + ReqId;

            bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));
            string subdir = subPath;
            if (!exists)
                System.IO.Directory.CreateDirectory(Server.MapPath(subPath));

            string fullname = "";

            if (FileUpload1.HasFile)
            {
                string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);

                fullname = "1_index" + fileExtension;
                string fileLocation = Server.MapPath(subdir + "/" + fullname);
                Session["file1"] = fileLocation;
                if (File.Exists(fileLocation))
                {
                    File.Delete(fileLocation);
                }
                FileUpload1.SaveAs(fileLocation);
            }

            if (FileUpload2.HasFile)
            {
                string fileName = Path.GetFileName(FileUpload2.PostedFile.FileName);
                string fileExtension = Path.GetExtension(FileUpload2.PostedFile.FileName);

                fullname = "2_research" + fileExtension;
                string fileLocation = Server.MapPath(subdir + "/" + fullname);
                Session["file2"] = fileLocation;
                if (File.Exists(fileLocation))
                {
                    File.Delete(fileLocation);
                }
                FileUpload2.SaveAs(fileLocation);
            }

        }

        protected void lnkViewF1_Command(object sender, CommandEventArgs e)
        {

            string path = "";
            switch (e.CommandArgument)
            {
                case "F1":
                    path = lblf1.Text;
                    break;
                case "F2":
                    path = lblf2.Text;
                    break;
            }
            try
            {
                if (path.ToLower().Contains("pdf"))
                {
                    WebClient User = new WebClient();


                    Byte[] FileBuffer = User.DownloadData(path);
                    if (FileBuffer != null)
                    {
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-length", FileBuffer.Length.ToString());
                        //Response.Write("<script>window.open('" + path + "','_blank');</script>");
                        Response.BinaryWrite(FileBuffer);
                    }
                }
                else
                {
                    string[] p = path.Split('\\');
                    Session["imagefile"] = "~/document/forms/reward/" + p[p.Length - 2] + "/" + p[p.Length - 1];
                    Response.Redirect("ShowImageFile.aspx");
                }
                //else
                //    ReadWordDoc();
            }
            catch { }

        }

        //protected void ReadWordDoc()
        //{
        //    object documentFormat = 8;
        //    string randomName = DateTime.Now.Ticks.ToString();
        //    object htmlFilePath = Server.MapPath("~/document/forms/support/"+Session["ViewRequestFrom"]+"/") + randomName + ".htm";
        //    string directoryPath = Server.MapPath("~/document/forms/support/" + Session["ViewRequestFrom"] + "/") + randomName + "_files";
        //    string fileSavePath = Server.MapPath(Session["imagefile"].ToString());// + Path.GetFileName(FileUpload1.PostedFile.FileName);

        //    //If Directory not present, create it.
        //    if (!Directory.Exists(Server.MapPath("~/document/forms/support/" + Session["ViewRequestFrom"] + "/")))
        //    {
        //        Directory.CreateDirectory(Server.MapPath("~/document/forms/support/" + Session["ViewRequestFrom"] + "/"));
        //    }

        //    ////Upload the word document and save to Temp folder.
        //    //FileUpload1.PostedFile.SaveAs(fileSavePath.ToString());

        //    //Open the word document in background.
        //    Microsoft.Office.Interop.Word._Application applicationclass = new Microsoft.Office.Interop.Word.Application();
        //    applicationclass.Documents.Open(fileSavePath);
        //    applicationclass.Visible = false;
        //    Microsoft.Office.Interop.Word.Document document = applicationclass.ActiveDocument;

        //    //Save the word document as HTML file.
        //    document.SaveAs(ref htmlFilePath, ref documentFormat);

        //    //Close the word document.
        //    document.Close();

        //    //Read the saved Html File.
        //    string wordHTML = System.IO.File.ReadAllText(htmlFilePath.ToString());

        //    //Loop and replace the Image Path.
        //    foreach (Match match in Regex.Matches(wordHTML, "<v:imagedata.+?src=[\"'](.+?)[\"'].*?>", RegexOptions.IgnoreCase))
        //    {
        //        wordHTML = Regex.Replace(wordHTML, match.Groups[1].Value, "~/document/forms/support/" + Session["ViewRequestFrom"] + "/" + match.Groups[1].Value);
        //    }

        //    //Delete the Uploaded Word File.
        //    System.IO.File.Delete(fileSavePath.ToString());

        //    dvWord.InnerHtml = wordHTML;
        //}

        protected void lnkDownLoad_Click(object sender, EventArgs e)
        {
            try
            {
                using (ZipFile zip = new ZipFile())
                {
                string x = Session["ViewRequestFrom"].ToString();
                    zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                    zip.AddDirectoryByName(Session["ViewRequestFrom"].ToString());
                    zip.AddFile(lblf1.Text, Session["ViewRequestFrom"].ToString());
                    zip.AddFile(lblf2.Text, Session["ViewRequestFrom"].ToString());
                    //zip.AddFile(lblf3.Text, Session["ViewRequestFrom"].ToString());
                    //zip.AddFile(lblf4.Text, Session["ViewRequestFrom"].ToString());

                    //foreach (GridViewRow row in GridView1.Rows)
                    //    {
                    //        string filePath = (row.FindControl("lblFilePath") as Label).Text;
                    //        zip.AddFile(filePath, "Files");
                    //    }
                    Response.Clear();
                    Response.BufferOutput = false;
                    //string zipName = String.Format("Zip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                    string zipName = String.Format("RequestID_{0}.zip", Session["ViewRequestFrom"].ToString());
                    Response.ContentType = "application/zip";
                    Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                    zip.Save(Response.OutputStream);
                    Response.End();
                }
            }
            catch { }
        }

        protected void btnDirDecision_Click(object sender, EventArgs e)
        {
            string id = (sender as Control).ID;
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            //get dirctor priv id
            string sql = @"SELECT p.autoid,[PrivFacultyId]
                        ,[PrivDeptId]
                        ,[PrivTo]
                        FROM Priviliges p,Faculty f,department d
                        where p.PrivFacultyId=f.AutoId and p.PrivDeptId=d.AutoId
                        and f.CollegeName = N'" + lblFaculty.Text + "' and d.DeptName=N'" + lblDept.Text + "'";
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand(sql, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            string privid = dr[0].ToString();

            //get dean priv id
            sql = @"SELECT p.autoid,[PrivFacultyId]
                        ,[PrivDeptId]
                        ,[PrivTo],Email
                        FROM Priviliges p
                        where PrivFacultyId = " + dr[1] + " and PrivType=1";
            cmd = new SqlCommand(sql, conn);
            SqlDataReader drDean = cmd.ExecuteReader();
            drDean.Read();
            string privdean = drDean[0].ToString();

            cmd = new SqlCommand("insert into RequestsFollowUp values(@reqid,@fromid,@fromname,@toid,@toname,@reqdate,@reqst,@actualid,@notes,@dec,1)", conn);
            cmd.Parameters.AddWithValue("@reqid", Session["ViewRequestFrom"]);
            cmd.Parameters.AddWithValue("@fromid", privid);
            cmd.Parameters.AddWithValue("@fromname", "رئيس قسم " + lblDept.Text);
            cmd.Parameters.AddWithValue("@dec", id.ToLower().Contains("ok") ? "1" : id.ToLower().Contains("no") ? "3" : "2");
            string st = "قيد التنفيذ";
            //if (rdDirDecision.SelectedValue == "1")
            if(id.ToLower().Contains("ok"))
            {
                cmd.Parameters.AddWithValue("@toid", privdean);
                cmd.Parameters.AddWithValue("@toname", "عميد كلية " + lblFaculty.Text);
                StringBuilder msg = new StringBuilder();
                msg.Clear();
                msg.Append("<body dir='rtl'><b>عميد كلية " + lblFaculty.Text + " المحترم،</b>");
                msg.Append("<br>تم ارسال توصية رئيس قسم  "+lblDept.Text+" لطلب الباحث "+ lblRName.Text + " وهي كالآتي");
                msg.Append("<br><b>" + txtDirNotes.Text + "</b>");
                msg.Append("<br>أرجو من حضرتكم التكرم ");
                msg.Append(" <a href='http://meusr-ra.meu.edu.jo/' target='_blank'>بالدخول إلى موقع البحث العلمي </a> ");
                msg.Append(" ودراسة الطلب وارسال توصيتكم الى عمادة الدراسات العليا والبحث العلمي ");
                msg.Append("<br><br><b>عمادة الدرسات العليا والبحث العلمي");
                msg.Append("<br>قسم البحث العلمي</b>");
                sendEmail(drDean[4].ToString(), msg.ToString());
            }
            else if (id.ToLower().Contains("no")) //if (rdDirDecision.SelectedValue == "3")
            {
                st = "مغلقة";
                cmd.Parameters.AddWithValue("@toid", lblJobId.Text);
                cmd.Parameters.AddWithValue("@toname", lblRName.Text);
                SqlCommand cmdUp = new SqlCommand("Update ResearchRewardForm Set RequestType='T',RequestFinalStatus=N'مغلقة' where AutoId=" + Session["ViewRequestFrom"], conn);
                cmdUp.ExecuteNonQuery();
            }
            else if (id.ToLower().Contains("post")) //if (rdDirDecision.SelectedValue == "2")
            {
                
                cmd.Parameters.AddWithValue("@toid", lblJobId.Text);
                cmd.Parameters.AddWithValue("@toname", lblRName.Text);
                SqlCommand cmdUp = new SqlCommand("Update ResearchRewardForm Set RequestType='NT',RequestFinalStatus=N'" + txtDirNotes.Text + "' where AutoId=" + Session["ViewRequestFrom"], conn);
                cmdUp.ExecuteNonQuery();
            }


            cmd.Parameters.AddWithValue("@reqdate", DateTime.Now.Date);
            cmd.Parameters.AddWithValue("@reqst",st);
            cmd.Parameters.AddWithValue("@actualid", Session["uid"]);
            cmd.Parameters.AddWithValue("@notes", txtDirNotes.Text);
            cmd.ExecuteNonQuery();

            cmd = new SqlCommand("update RequestsFollowUp Set ReqStatus=N'انجزت' where Type=1 and AutoId=" + Session["AutoIdUpdated"], conn);
            cmd.ExecuteNonQuery();
            //upload();
            conn.Close();
            Response.Redirect("Requests.aspx");

        }

        protected void btnDeanDecision_Click(object sender, EventArgs e)
        {
            string id = (sender as Control).ID;
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand cmd = new SqlCommand();

            //get dean priv id
            string sql = @"select * from RequestsFollowUp 
                            where Type=1 and  autoid=(select max(autoid) from RequestsFollowUp where  type=1 and requestid=" + Session["ViewRequestFrom"]+")";
            cmd = new SqlCommand(sql, conn);
            SqlDataReader drDean = cmd.ExecuteReader();
            drDean.Read();
            string privdean = drDean[4].ToString();

            cmd = new SqlCommand("insert into RequestsFollowUp values(@reqid,@fromid,@fromname,@toid,@toname,@reqdate,@reqst,@actualid,@notes,@dec,1)", conn);
            cmd.Parameters.AddWithValue("@reqid", Session["ViewRequestFrom"]);
            cmd.Parameters.AddWithValue("@fromid", privdean);
            cmd.Parameters.AddWithValue("@fromname", "عميد كلية " + lblFaculty.Text);
            string st = "قيد التنفيذ";
            if(id.ToLower().Contains("ok"))// if (rdDeanDecision.SelectedValue == "1")
            {
                cmd.Parameters.AddWithValue("@toid", 11);
                cmd.Parameters.AddWithValue("@toname", "رئيس قسم البحث العلمي");
                SqlCommand cmdUp = new SqlCommand("Update ResearchRewardForm Set RequestFinalStatus=N'قيد المعالجة',FacultyReqNo=N'" + lblSaderNo.Text +"/"+ Session["ViewRequestFrom"] + "',GSInNo=N'ع د ع / "+ Session["ViewRequestFrom"] + "' where AutoId=" + Session["ViewRequestFrom"], conn);
                cmdUp.ExecuteNonQuery();

                SqlCommand cmd11 = new SqlCommand("Select * From Priviliges where PrivType=2 and PrivFacultyId=11", conn);
                SqlDataReader drSecGS = cmd11.ExecuteReader();
                drSecGS.Read();

                System.Text.StringBuilder msg = new System.Text.StringBuilder();
                msg.Clear();
                msg.Append("<body dir='rtl'><b>رئيس قسم البحث العلمي المحترم،</b>");
                msg.Append("<br>الرجاء دراسة طلب مكافأة نشر بحث علمي لطلب الباحث " + lblRName.Text);
                msg.Append(" <a href='http://meusr-ra.meu.edu.jo/' target='_blank'>بالدخول إلى موقع البحث العلمي </a> ");
                msg.Append("<br><br><b>عميد كلية "+lblFaculty.Text+"</b>");
                sendEmail(drSecGS[5].ToString(), msg.ToString());
            }
            else if (id.ToLower().Contains("post"))//if (rdDeanDecision.SelectedValue == "3")
            {
                cmd.Parameters.AddWithValue("@toid", lblJobId.Text);
                cmd.Parameters.AddWithValue("@toname", lblRName.Text);
                SqlCommand cmdUp = new SqlCommand("Update ResearchRewardForm Set RequestType='NT',RequestFinalStatus=N'" + txtDeanNotes.Text + "' where AutoId=" + Session["ViewRequestFrom"], conn);
                cmdUp.ExecuteNonQuery();
            }
            else if (id.ToLower().Contains("no"))//if (rdDeanDecision.SelectedValue == "2")
            {
                st = "مغلقة";
                cmd.Parameters.AddWithValue("@toid", lblJobId.Text);
                cmd.Parameters.AddWithValue("@toname", lblRName.Text);
                SqlCommand cmdUp = new SqlCommand("Update ResearchRewardForm Set RequestType='T',RequestFinalStatus=N'مغلقة' where AutoId=" + Session["ViewRequestFrom"], conn);
                cmdUp.ExecuteNonQuery();
            }

            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            
            cmd.Parameters.AddWithValue("@reqdate", DateTime.Now.Date);
            cmd.Parameters.AddWithValue("@reqst", st);
            cmd.Parameters.AddWithValue("@actualid", Session["uid"]);
            cmd.Parameters.AddWithValue("@notes", txtDeanNotes.Text);
            cmd.Parameters.AddWithValue("@dec", id.ToLower().Contains("ok") ? "1" : id.ToLower().Contains("post") ? "3" : "2");
            cmd.ExecuteNonQuery();

            cmd = new SqlCommand("update RequestsFollowUp Set ReqStatus=N'انجزت' where Type=1 and AutoId=" + Session["AutoIdUpdated"], conn);
            cmd.ExecuteNonQuery();
            //upload();
            conn.Close();
            Response.Redirect("Requests.aspx");
        }

        protected void btnReDirector_Click(object sender, EventArgs e)
        {
            int ReqId = Convert.ToInt32(Session["RequestComeFrom"] != null ? Session["RequestComeFrom"] : Session["ViewRequestFrom"]);
            string subPath = "document/forms/reward/" + ReqId;

            bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));
            string subdir = subPath;
            if (!exists)
                System.IO.Directory.CreateDirectory(Server.MapPath(subPath));

            string fullname = "";

            if (FileUpload1.HasFile)
            {
                string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);

                fullname = "1_index" + fileExtension;
                string fileLocation = Server.MapPath(subdir + "/" + fullname);
                Session["file1"] = fileLocation;
                if (File.Exists(fileLocation))
                {
                    File.Delete(fileLocation);
                }
                FileUpload1.SaveAs(fileLocation);
            }
            Session["ReDirectorFinal"] = "ok";
            Session["btnName"]= (sender as Control).ID;
            ConfirmDiv.Visible = true;
            //string id = (sender as Control).ID;
            //if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
            //    conn.Open();
            //SqlCommand cmd = new SqlCommand();

            ////get dean priv id
            //string sql = @"select * from RequestsFollowUp 
            //                where autoid=(select max(autoid) from RequestsFollowUp where requestid=" + Session["ViewRequestFrom"] + ")";

            //cmd = new SqlCommand("insert into RequestsFollowUp values(@reqid,@fromid,@fromname,@toid,@toname,@reqdate,@reqst,@actualid,@notes,@dec)", conn);
            //cmd.Parameters.AddWithValue("@reqid", Session["ViewRequestFrom"]);
            //cmd.Parameters.AddWithValue("@fromid", 11);
            //cmd.Parameters.AddWithValue("@fromname", "رئيس قسم البحث العلمي");
            //cmd.Parameters.AddWithValue("@dec", id.ToLower().Contains("ok") ? "1" : id.ToLower().Contains("post") ? "3" : "2");
            //string st = "قيد التنفيذ";
            //if(id.ToLower().Contains("ok")) // if (rdReDir.SelectedValue == "1")
            //{
            //    cmd.Parameters.AddWithValue("@toid", 10);
            //    cmd.Parameters.AddWithValue("@toname", "عميد الدراسات العليا والبحث العلمي");
            //    string sqlDir = "insert into ReDirectorInfo values(" + Session["ViewRequestFrom"] + ",N'" + txtyearmonth.Text + "',N'" + txtFeeV.Text + "',"
            //        + "-1," + chkFiles.Items[0].Value + "," + chkFiles.Items[1].Value + "," + chkFiles.Items[2].Value
            //        + "," + chkFiles.Items[3].Value + "," + chkFiles.Items[4].Value + "," + chkFiles.Items[5].Value + ")";
            //    SqlCommand cmdReDir = new SqlCommand(sqlDir, conn);
            //    cmdReDir.ExecuteNonQuery();

            //    StringBuilder msg = new StringBuilder();
            //    msg.Clear();
            //    msg.Append("<body dir='rtl'><b>عميد الدراسات العليا والبحث العلمي المحترم،</b>");
            //    msg.Append("<br>بعد دراسة طلب الباحث "+lblRName.Text+"أوصي بما يلي");
            //    msg.Append("<br><b>"+txtReDirector.Text+"</b>");
            //    msg.Append("<br>أرجو التكرم ");
            //    msg.Append(" <a href='http://meusr-ra.meu.edu.jo/' target='_blank'>بالدخول إلى موقع البحث العلمي </a> ");
            //    msg.Append(" واتخاذ اللازم ");
            //    msg.Append("<br><br><b>رئيس قسم البحث العلمي</b>");
            //    msg.Append("<br><br><b>عمادة الدراسات العليا والبحث العلمي</b>");
            //    //sendEmail("Dean-Graduate@meu.edu.jo", msg.ToString());


            //}
            //else if(id.ToLower().Contains("post"))// if (rdReDir.SelectedValue == "3")
            //{
            //    SqlCommand cmdd = new SqlCommand("Select ReqFromId,ReqFromName From RequestsFollowUp where Autoid=(Select Min(AutoId) From RequestsFollowUp where RequestId=" + Session["ViewRequestFrom"] + ")", conn);
            //    SqlDataReader drDNotes = cmdd.ExecuteReader();
            //    drDNotes.Read();

            //    cmd.Parameters.AddWithValue("@toid", drDNotes[0]);
            //    cmd.Parameters.AddWithValue("@toname", drDNotes[1]);

            //    SqlCommand cmdUp = new SqlCommand("Update ResearchRewardForm Set RequestFinalStatus=N'تعليق - "+txtReDirector.Text+"' where AutoId=" + Session["ViewRequestFrom"], conn);
            //    cmdUp.ExecuteNonQuery();

            //}
            //else if(id.ToLower().Contains("no")) // if (rdReDir.SelectedValue == "2")
            //{
            //    st = "مغلقة";
            //    cmd.Parameters.AddWithValue("@toid", lblJobId.Text);
            //    cmd.Parameters.AddWithValue("@toname", lblRName.Text);
            //    SqlCommand cmdUp = new SqlCommand("Update ResearchRewardForm Set RequestType='S',RequestFinalStatus=N'" + txtDirNotes.Text + "' where AutoId=" + Session["ViewRequestFrom"], conn);
            //    cmdUp.ExecuteNonQuery();
            //}


            //cmd.Parameters.AddWithValue("@reqdate", DateTime.Now.Date);
            //cmd.Parameters.AddWithValue("@reqst", st);
            //cmd.Parameters.AddWithValue("@actualid", Session["uid"]);
            //cmd.Parameters.AddWithValue("@notes", txtReDirector.Text);
            //cmd.ExecuteNonQuery();


            //cmd = new SqlCommand("update RequestsFollowUp Set ReqStatus=N'انجزت' where AutoId=" + Session["AutoIdUpdated"], conn);
            //cmd.ExecuteNonQuery();
            ////upload();
            //conn.Close();
            //Response.Redirect("Requests.aspx");
        }

        protected void btnReDean_Click(object sender, EventArgs e)
        {
            string id = "";
            string ReDeanDecision = "";
            if (rdReDeanDecision1.Checked)
            {
                id = "agree";
                string qrt = "";
                switch (rdQuarter.SelectedValue)
                {
                    case "1":
                        qrt = "( الأول )( Q1 )";
                        break;
                    case "2":
                        qrt = "( الثاني )( Q2 )";
                        break;
                    case "3":
                        qrt = "( الثالث )( Q3 )";
                        break;
                    case "4":
                        qrt = "( الرابع )( Q4 )";
                        break;
                    case "5":
                        qrt = "( خارج التصنيف )( out of classification )";
                        break;
                }
                //for(int i=0;i<GridView3.Rows.Count;i++)

                ReDeanDecision = "التوصية بالموافقة على مكافأة على نشر بحث لأعضاء الهيئة التدريسية حسب الجدول التالي";// + txtReDeanValue.Text + " كون البحث مفهرس بقاعدة بيانات SCOPUS بالربع "+ qrt+" في العام " + txtReDeanYear.Text + " مع الشكر ";

            }
            else if (rdReDeanDecision2.Checked)
            {
                id = "notagree";
                ReDeanDecision = "التوصية بعدم الموافقة على مكافأة على نشر كون البحث غير مفهرس بقاعدة بيانات SCOPUS";
            }
            else if (rdReDeanDecision3.Checked)
            {
                id = "rediretor";
                ReDeanDecision = "رئيس قسم البحث العلمي لمراجعة " + txtReDean.Text;
            }
            if (id != "")
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand cmd = new SqlCommand();
                string st = "قيد التنفيذ";
                cmd = new SqlCommand("insert into RequestsFollowUp values(@reqid,@fromid,@fromname,@toid,@toname,@reqdate,@reqst,@actualid,@notes,@dec,1)", conn);
                cmd.Parameters.AddWithValue("@reqid", Session["ViewRequestFrom"]);
                cmd.Parameters.AddWithValue("@fromid", 14);
                cmd.Parameters.AddWithValue("@fromname", "عميد الدراسات العليا والبحث العلمي");
                
                if (id== "agree" || id=="notagree") // if (rdReDean.SelectedValue == "1")
                {
                    cmd.Parameters.AddWithValue("@dec", id == "agree" ? "1" : "2");
                    cmd.Parameters.AddWithValue("@toid", 13);
                    cmd.Parameters.AddWithValue("@toname", "نائب رئيس الجامعة");
                    cmd.Parameters.AddWithValue("@reqst", "مكتمل");
                    //SqlCommand cmdUp = new SqlCommand("Update ResearchRewardForm Set RequestFinalStatus=N'مكتمل',GSOutNo=N'ع د ع / ص /" + Session["ViewRequestFrom"] + "' where AutoId=" + Session["ViewRequestFrom"], conn);
                    SqlCommand cmdUp = new SqlCommand("Update ResearchRewardForm Set RequestFinalStatus=N'مكتمل', GSOutNo=N'ع د ع / د / 8 /' where AutoId=" + Session["ViewRequestFrom"], conn);
                    cmdUp.ExecuteNonQuery();

                    //SqlCommand cmdUpdate = new SqlCommand("Update ReDirectorInfo Set FeeValue=N'" + txtReDeanValue.Text + " دينار أردني ' where type=1 and RequestId=" + Session["ViewRequestFrom"], conn);
                    //cmdUpdate.ExecuteNonQuery();

                }
                //else if (id.ToLower().Contains("post")) //if (rdReDean.SelectedValue == "3")
                //{
                //    SqlCommand cmdd = new SqlCommand("Select ReqFromId From RequestsFollowUp where Autoid=(Select Max(AutoId) From RequestsFollowUp where RequestId=" + Session["ViewRequestFrom"] + ")", conn);
                //    SqlDataReader drDNotes = cmdd.ExecuteReader();
                //    drDNotes.Read();

                //    cmd.Parameters.AddWithValue("@toid", drDNotes[0]);
                //    cmd.Parameters.AddWithValue("@toname", "عميد كلية " + lblFaculty.Text);
                //    cmd.Parameters.AddWithValue("@reqst", "قيد التنفيذ");
                //}
                else if (id.ToLower().Contains("rediretor"))  //if (rdReDean.SelectedValue == "2")
                {
                    //st = "مغلقة";
                    cmd.Parameters.AddWithValue("@dec", 3);
                    cmd.Parameters.AddWithValue("@toid", 11);
                    cmd.Parameters.AddWithValue("@toname", "رئيس قسم البحث العلمي");
                    cmd.Parameters.AddWithValue("@reqst", st);
                    //SqlCommand cmdUp = new SqlCommand("Update ResearchRewardForm Set RequestType='S',RequestFinalStatus=N'" + txtDirNotes.Text + "' where AutoId=" + Session["ViewRequestFrom"], conn);
                    //cmdUp.ExecuteNonQuery();
                }
                cmd.Parameters.AddWithValue("@reqdate", DateTime.Now.Date);
                cmd.Parameters.AddWithValue("@actualid", Session["uid"]);
                //cmd.Parameters.AddWithValue("@notes", txtReDean.Text);
                cmd.Parameters.AddWithValue("@notes", ReDeanDecision);

                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("update RequestsFollowUp Set ReqStatus=N'انجزت' where type=1 and AutoId=" + Session["AutoIdUpdated"], conn);
                cmd.ExecuteNonQuery();

                SqlCommand cmd11 = new SqlCommand("Select * From Priviliges where PrivType=2 and PrivFacultyId=11", conn);
                SqlDataReader drSecGS = cmd11.ExecuteReader();
                drSecGS.Read();

                System.Text.StringBuilder msg = new System.Text.StringBuilder();
                msg.Clear();
                msg.Append("<body dir='rtl'><b>المساعد الاداري لعمادة الدراسات العليا والبحث العلمي المحترم،</b>");
                if (id == "agree" || id == "notagree")
                    msg.Append("<br>الرجاء طباعة طلب مكافأة على نشر بحث علمي لطلب الباحث " + lblRName.Text + " رقم " + Session["ViewRequestFrom"] + " مع تجميع كامل الملفات");
                //else
                //    msg.Append("<br>أرجو مراجعة  " + txtReDean.Text);
                msg.Append(" <a href='http://meusr-ra.meu.edu.jo/' target='_blank'>بالدخول إلى موقع البحث العلمي </a> ");
                msg.Append("<br><br><b>عميد الدراسات العليا والبحث العلمي</b>");
                sendEmail("Sec-Graduate@meu.edu.jo,atarawneh@meu.edu.jo", msg.ToString());

                conn.Close();
                Response.Redirect("Requests.aspx");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('يجب تحديد أحد القرارات');", true);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Requests.aspx");
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            //msgDiv.Visible = false;
            //Timer1.Enabled = false;
            //Response.Redirect("ResearchFeeForm.aspx");
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            Response.Redirect("Requests.aspx");
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            if (Session["ReDirectorFinal"].ToString() != "ok")
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();
                string sql = "";
                SqlCommand cmd;
                DataTable dtt = new DataTable();
                //cmd = new SqlCommand("Select * from ResearchRewardForm where AutoId=" + Session["RequestComeFrom"], conn);
                //SqlDataReader drCheck = cmd.ExecuteReader();
                //drCheck.Read();


                if (Session["RequestComeFrom"] == null)
                {
                    sql = "Insert into ResearchRewardForm values(@rid,@hcst,@orcid,@gslink,@rglink,@rtitle,@pubDate,@magname,@magissn,@pub,@year,@folder,@vol,'T',@ReqDate,'',N'قيد الدراسة','','',@DBIndex,@Pages,@Rewarded,@chk1,@chk2,@chk3,@chk4,@chk5,@researcherid,0)";
                    cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@rid", Session["uid"].ToString());
                    cmd.Parameters.AddWithValue("@hcst", txtHcst.Text);
                    cmd.Parameters.AddWithValue("@orcid", txtOrcid.Text);
                    cmd.Parameters.AddWithValue("@gslink", txtGSLink.Text);
                    cmd.Parameters.AddWithValue("@rglink", txtRGLink.Text);
                    cmd.Parameters.AddWithValue("@rtitle", txtRTitle.Text);
                    cmd.Parameters.AddWithValue("@magname", txtMagName.Text);
                    cmd.Parameters.AddWithValue("@magissn", txtMagISSN.Text);
                    cmd.Parameters.AddWithValue("@pub", txtPubName.Text);
                    cmd.Parameters.AddWithValue("@year", txtMagYear.Text);
                    cmd.Parameters.AddWithValue("@folder", txtFolder.Text);
                    cmd.Parameters.AddWithValue("@vol", txtVolume.Text);
                    cmd.Parameters.AddWithValue("@ReqDate", DateTime.Now.Date);
                    cmd.Parameters.AddWithValue("@pubDate", txtPublishDate.Text);
                    cmd.Parameters.AddWithValue("@DBIndex", txtDBIndex.Text);
                    cmd.Parameters.AddWithValue("@Pages", txtPages.Text);
                    cmd.Parameters.AddWithValue("@Rewarded", rdPrevReward.SelectedValue);
                    cmd.Parameters.AddWithValue("@chk1", rdCheckList1.SelectedValue);
                    cmd.Parameters.AddWithValue("@chk2", rdCheckList2.SelectedValue);
                    cmd.Parameters.AddWithValue("@chk3", rdCheckList3.SelectedValue);
                    cmd.Parameters.AddWithValue("@chk4", rdCheckList4.SelectedValue);
                    cmd.Parameters.AddWithValue("@chk5", rdCheckList5.SelectedValue);
                    cmd.Parameters.AddWithValue("@researcherid", txtResearcherId.Text);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("select max(autoid) from ResearchRewardForm where jobid=" + Session["uid"], conn);
                    //DataTable dtt = new DataTable();
                    dtt.Load(cmd.ExecuteReader());
                    Session["RequestComeFrom"] = dtt.Rows[0][0];

                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        cmd = new SqlCommand("Insert into RePartInfoReward values(" + dtt.Rows[0][0] + ",N'" + GridView1.Rows[i].Cells[1].Text + "')", conn);
                        cmd.ExecuteNonQuery();
                    }

                    for (int i = 0; i < GridView2.Rows.Count; i++)
                    {
                        cmd = new SqlCommand("Insert into RewardedInfo values(" + dtt.Rows[0][0] + ",N'" + GridView2.Rows[i].Cells[1].Text + "',N'" + GridView2.Rows[i].Cells[2].Text + "')", conn);
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    sql = "Update ResearchRewardForm Set ";
                    sql += "hcstID=@hcst,orcidId=@orcid,GSLink=@gslink,RGLink=@rglink,";
                    sql += "ReTitle=@rtitle,AcceptDate=@pubDate,MagName=@magname,MagISSN=@magissn,";
                    sql += "PublisherName=@pub,MagYear=@year,FolderMag=@folder,Volume=@vol,";
                    sql += "RequestType='T',RequestFinalStatus=N'قيد الدراسة',DBIndex=@DBIndex,";
                    sql += "Pages=@Pages,RewardedBefore=@Rewarded,";
                    sql += "chkList1=@chk1,chkList2=@chk2,chkList3=@chk3,chkList4=@chk4,chkList5=@chk5";
                    sql += ",researcherid=@researcherid where Autoid=" + Session["RequestComeFrom"];
                    cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@hcst", txtHcst.Text);
                    cmd.Parameters.AddWithValue("@orcid", txtOrcid.Text);
                    cmd.Parameters.AddWithValue("@gslink", txtGSLink.Text);
                    cmd.Parameters.AddWithValue("@rglink", txtRGLink.Text);
                    cmd.Parameters.AddWithValue("@rtitle", txtRTitle.Text);
                    cmd.Parameters.AddWithValue("@magname", txtMagName.Text);
                    cmd.Parameters.AddWithValue("@magissn", txtMagISSN.Text);
                    cmd.Parameters.AddWithValue("@pub", txtPubName.Text);
                    cmd.Parameters.AddWithValue("@year", txtMagYear.Text);
                    cmd.Parameters.AddWithValue("@folder", txtFolder.Text);
                    cmd.Parameters.AddWithValue("@vol", txtVolume.Text);
                    cmd.Parameters.AddWithValue("@pubDate", txtPublishDate.Text);
                    cmd.Parameters.AddWithValue("@DBIndex", txtDBIndex.Text);
                    cmd.Parameters.AddWithValue("@Pages", txtPages.Text);
                    cmd.Parameters.AddWithValue("@Rewarded", rdPrevReward.SelectedValue);
                    cmd.Parameters.AddWithValue("@chk1", rdCheckList1.SelectedValue);
                    cmd.Parameters.AddWithValue("@chk2", rdCheckList2.SelectedValue);
                    cmd.Parameters.AddWithValue("@chk3", rdCheckList3.SelectedValue);
                    cmd.Parameters.AddWithValue("@chk4", rdCheckList4.SelectedValue);
                    cmd.Parameters.AddWithValue("@chk5", rdCheckList5.SelectedValue);
                    cmd.Parameters.AddWithValue("@researcherid", txtResearcherId.Text);
                    cmd.ExecuteNonQuery();


                    cmd = new SqlCommand("delete from RePartInfoReward where RequestId=" + Session["RequestComeFrom"], conn);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from RewardedInfo where FormId=" + Session["RequestComeFrom"], conn);
                    cmd.ExecuteNonQuery();

                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        cmd = new SqlCommand("Insert into RePartInfoReward values(" + Session["RequestComeFrom"] + ",N'" + GridView1.Rows[i].Cells[1].Text + "')", conn);
                        cmd.ExecuteNonQuery();
                    }

                    for (int i = 0; i < GridView2.Rows.Count; i++)
                    {
                        cmd = new SqlCommand("Insert into RewardedInfo values(" + Session["RequestComeFrom"] + ",N'" + GridView2.Rows[i].Cells[1].Text + "',N'" + GridView2.Rows[i].Cells[2].Text + "')", conn);
                        cmd.ExecuteNonQuery();
                    }

                }

                SqlCommand cmdEmail = new SqlCommand("Update Users Set Email='" + txtEmail.Text + "' where AcdId=" + lblJobId.Text, conn);
                cmdEmail.ExecuteNonQuery();
                Session["UserEmail"] = txtEmail.Text;


                cmd = new SqlCommand("Update RequestsFollowUp Set ReqStatus=N'انجزت' where Type=1 and AutoId=(select max(Autoid) from RequestsFollowUp where Type=1 and RequestId=" + Session["RequestComeFrom"] + ")", conn);
                cmd.ExecuteNonQuery();

                sql = @"SELECT p.AutoId, p.PrivTo,Email
                        FROM priviliges p
                        where PrivFacultyId=(select AutoId from faculty where CollegeName=N'" + lblFaculty.Text + @"')
                        and PrivDeptId=(select AutoId from Department where DeptName=N'" + lblDept.Text + @"')";

                int EmailFlag = 0;
                cmd = new SqlCommand(sql, conn);
                DataTable dtPrivDept = new DataTable();
                dtPrivDept.Load(cmd.ExecuteReader());

                cmd = new SqlCommand("insert into RequestsFollowUp values(@reqid,@fromid,@fromname,@toid,@toname,@reqdate,@reqst,@actualid,N'',N'',1)", conn);
                cmd.Parameters.AddWithValue("@reqid", Session["RequestComeFrom"]);
                cmd.Parameters.AddWithValue("@fromid", Session["uid"]);
                cmd.Parameters.AddWithValue("@fromname", Session["userName"]);
                SqlCommand cmdLastDec = new SqlCommand("Select * From RequestsFollowUp where type=1 and AutoId=(select max(Autoid) from RequestsFollowUp where type=1 and RequestId=" + Session["RequestComeFrom"] + ")", conn);
                SqlDataReader drLastDec = cmdLastDec.ExecuteReader();
                if (drLastDec.HasRows)
                {
                    drLastDec.Read();
                    //if (Session["FinalStatus"] != null && Session["FinalStatus"].ToString().Contains("تعليق"))
                    if (Convert.ToInt16(drLastDec["Decision"].ToString()) == 3 && Convert.ToInt16(drLastDec["ReqFromId"].ToString()) == 11)
                    {
                        cmd.Parameters.AddWithValue("@toid", 11);
                        cmd.Parameters.AddWithValue("@toname", "رئيس قسم البحث العلمي");
                        EmailFlag = 1;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@toid", dtPrivDept.Rows[0][0]);
                        cmd.Parameters.AddWithValue("@toname", "رئيس قسم " + lblDept.Text);
                    }
                }
                else
                {
                    cmd.Parameters.AddWithValue("@toid", dtPrivDept.Rows[0][0]);
                    cmd.Parameters.AddWithValue("@toname", "رئيس قسم " + lblDept.Text);
                }
                cmd.Parameters.AddWithValue("@reqdate", DateTime.Now.Date);
                cmd.Parameters.AddWithValue("@reqst", "قيد التنفيذ");

                cmd.Parameters.AddWithValue("@actualid", Session["uid"]);
                cmd.ExecuteNonQuery();
                Session["ss"] = dtPrivDept.Rows[0][2].ToString();

                Session["RequestComeFrom"] = null;
                Session["ViewRequestFrom"] = null;
                //if (Session["FinalStatus"] != null || !Session["FinalStatus"].ToString().Contains("تعليق"))
                //{
                    StringBuilder msg = new StringBuilder();
                    msg.Append("<body dir='rtl'><b>الباحث الكريم،</b>");
                    msg.Append("<br><br>تم استقبال طلبك بنجاح");
                    msg.Append("<br><a href='http://meusr-ra.meu.edu.jo/'>اضغط هنا لمتابعة الطلب</a>");
                    msg.Append("<br><br><b>عمادة الدرسات العليا والبحث العلمي</b>");
                    msg.Append("<br><b>قسم البحث العلمي</b>");
                    sendEmail(txtEmail.Text, msg.ToString());

                if (EmailFlag == 0)
                {
                    StringBuilder msg1 = new StringBuilder();
                    msg1.Append("<body dir='rtl'><b>رئيس قسم " + lblDept.Text + " المحترم،</b>");
                    msg1.Append("<br>تم ارسال طلب جديد أرجو من حضرتكم التكرم");
                    msg1.Append(" <a href='http://meusr-ra.meu.edu.jo/' target='_blank'>بالدخول إلى موقع البحث العلمي </a> ");
                    msg1.Append(" ودراسة الطلب وارسال توصيتكم الى عميد الكلية ");
                    msg1.Append("<br><br><b>عمادة الدرسات العليا والبحث العلمي</b>");
                    msg1.Append("<br><b>قسم البحث العلمي</b>");
                    sendEmail(dtPrivDept.Rows[0][2].ToString(), msg1.ToString());
                }
                else
                {
                    StringBuilder msg1 = new StringBuilder();
                    msg1.Append("<body dir='rtl'><b>رئيس قسم البحث العلمي المحترم،</b>");
                    msg1.Append("<br>تم تعديل طلب دعم رسوم بحث علمي أرجو من حضرتكم التكرم");
                    msg1.Append(" <a href='http://meusr-ra.meu.edu.jo/' target='_blank'>بالدخول إلى موقع البحث العلمي </a> ");
                    msg1.Append(" ودراسة الطلب ");
                    msg1.Append("<br><br><b>عمادة الدرسات العليا والبحث العلمي</b>");
                    msg1.Append("<br><b>قسم البحث العلمي</b>");
                    sendEmail("Atarawneh@meu.edu.jo", msg1.ToString());
                }



                conn.Close();
            }
            else if(Session["ReDirectorFinal"].ToString() == "ok")
            {
                string id = Session["btnName"].ToString();
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand cmd = new SqlCommand();

                //get dean priv id
                string sql = @"select * from RequestsFollowUp 
                            where type=1 and autoid=(select max(autoid) from RequestsFollowUp where type=1 and requestid=" + Session["ViewRequestFrom"] + ")";

                cmd = new SqlCommand("insert into RequestsFollowUp values(@reqid,@fromid,@fromname,@toid,@toname,@reqdate,@reqst,@actualid,@notes,@dec,1)", conn);
                cmd.Parameters.AddWithValue("@reqid", Session["ViewRequestFrom"]);
                cmd.Parameters.AddWithValue("@fromid", 11);
                cmd.Parameters.AddWithValue("@fromname", "رئيس قسم البحث العلمي");
                cmd.Parameters.AddWithValue("@dec", id.ToLower().Contains("ok") ? "1" : id.ToLower().Contains("post") ? "3" : "2");
                string st = "قيد التنفيذ";
                if (id.ToLower().Contains("ok")) // if (rdReDir.SelectedValue == "1")
                {
                    cmd.Parameters.AddWithValue("@toid", 14);
                    cmd.Parameters.AddWithValue("@toname", "عميد الدراسات العليا والبحث العلمي");

                    SqlCommand cmd1 = new SqlCommand("Delete From ReDirectorInfo where type=1 and RequestId=" + Session["ViewRequestFrom"], conn);
                    cmd1.ExecuteNonQuery();

                    string sqlDir = "insert into ReDirectorInfo values(" + Session["ViewRequestFrom"] + ",N'" + txtyearmonth.Text + "',N'',"
                        + rdQuarter.SelectedValue + "," + chkFiles.Items[0].Value + "," + chkFiles.Items[1].Value + ",-1"
                        + "," + chkFiles.Items[2].Value + ",-1,-1,-1," +
                        rdCheck.SelectedValue + "," + (chkClarivate.Items[0].Selected ? 1 : 0) + "," + (chkClarivate.Items[1].Selected ? 1 : 0) + "," + (chkClarivate.Items[2].Selected ? 1 : 0) + 
                        ",N'" + txtReDirector.Text + "',@d,1)";
                    SqlCommand cmdReDir = new SqlCommand(sqlDir, conn);
                    cmdReDir.Parameters.AddWithValue("@d", DateTime.Now.Date);
                    cmdReDir.ExecuteNonQuery();

                    StringBuilder msg = new StringBuilder();
                    msg.Clear();
                    msg.Append("<body dir='rtl'><b>عميد الدراسات العليا والبحث العلمي المحترم،</b>");
                    msg.Append("<br>بعد دراسة طلب الباحث " + lblRName.Text + "أوصي بما يلي");
                    msg.Append("<br><b>" + txtReDirector.Text + "</b>");
                    msg.Append("<br>أرجو التكرم ");
                    msg.Append(" <a href='http://meusr-ra.meu.edu.jo/' target='_blank'>بالدخول إلى موقع البحث العلمي </a> ");
                    msg.Append(" واتخاذ اللازم ");
                    msg.Append("<br><br><b>رئيس قسم البحث العلمي</b>");
                    msg.Append("<br><br><b>عمادة الدراسات العليا والبحث العلمي</b>");
                    sendEmail("Dean-Research@meu.edu.jo", msg.ToString());


                }
                else if (id.ToLower().Contains("post"))// if (rdReDir.SelectedValue == "3")
                {
                    SqlCommand cmdd = new SqlCommand("Select ReqFromId,ReqFromName From RequestsFollowUp where type=1 and Autoid=(Select Min(AutoId) From RequestsFollowUp where type=1 and RequestId=" + Session["ViewRequestFrom"] + ")", conn);
                    SqlDataReader drDNotes = cmdd.ExecuteReader();
                    drDNotes.Read();

                    cmd.Parameters.AddWithValue("@toid", drDNotes[0]);
                    cmd.Parameters.AddWithValue("@toname", drDNotes[1]);

                    SqlCommand cmdUp = new SqlCommand("Update ResearchRewardForm Set RequestType='NT',RequestFinalStatus=N'تعليق - " + txtReDirector.Text + "' where AutoId=" + Session["ViewRequestFrom"], conn);
                    cmdUp.ExecuteNonQuery();
                    StringBuilder msg = new StringBuilder();
                    msg.Clear();
                    msg.Append("<body dir='rtl'><b>الباحث الكريم،</b>");
                    msg.Append("<br> يوجد لديك طلب معلق");
                    msg.Append("<br><b>" + txtReDirector.Text + "</b>");
                    msg.Append("<br>أرجو التكرم ");
                    msg.Append(" <a href='http://meusr-ra.meu.edu.jo/' target='_blank'>بالدخول إلى موقع البحث العلمي </a> ");
                    msg.Append(" واتخاذ اللازم ");
                    msg.Append("<br><br><b>رئيس قسم البحث العلمي</b>");
                    msg.Append("<br><br><b>عمادة الدراسات العليا والبحث العلمي</b>");
                    sendEmail(txtEmail.Text, msg.ToString());


                }
                else if (id.ToLower().Contains("no")) // if (rdReDir.SelectedValue == "2")
                {
                    st = "مغلقة";
                    cmd.Parameters.AddWithValue("@toid", lblJobId.Text);
                    cmd.Parameters.AddWithValue("@toname", lblRName.Text);
                    SqlCommand cmdUp = new SqlCommand("Update ResearchRewardForm Set RequestType='T',RequestFinalStatus=N'مغلقة' where AutoId=" + Session["ViewRequestFrom"], conn);
                    cmdUp.ExecuteNonQuery();
                    StringBuilder msg = new StringBuilder();
                    msg.Clear();
                    msg.Append("<body dir='rtl'><b>عميد الدراسات العليا والبحث العلمي المحترم،</b>");
                    msg.Append("<br> يوجد لديك طلب معاد");
                    msg.Append("<br><b>" + txtReDirector.Text + "</b>");
                    msg.Append("<br>أرجو التكرم ");
                    msg.Append(" <a href='http://meusr-ra.meu.edu.jo/' target='_blank'>بالدخول إلى موقع البحث العلمي </a> ");
                    msg.Append(" واتخاذ اللازم ");
                    msg.Append("<br><br><b>رئيس قسم البحث العلمي</b>");
                    msg.Append("<br><br><b>عمادة الدراسات العليا والبحث العلمي</b>");
                    sendEmail(txtEmail.Text, msg.ToString());

                }


                cmd.Parameters.AddWithValue("@reqdate", DateTime.Now.Date);
                cmd.Parameters.AddWithValue("@reqst", st);
                cmd.Parameters.AddWithValue("@actualid", Session["uid"]);
                cmd.Parameters.AddWithValue("@notes", txtReDirector.Text);
                cmd.ExecuteNonQuery();


                cmd = new SqlCommand("update RequestsFollowUp Set ReqStatus=N'انجزت' where type=1 and AutoId=" + Session["AutoIdUpdated"], conn);
                cmd.ExecuteNonQuery();
                //upload();
                conn.Close();
                //Response.Redirect("Requests.aspx");

            }
            ConfirmDiv.Visible = false;
            Div1.Visible = true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ConfirmDiv.Visible = false;
        }

        protected void btnDone_Click(object sender, EventArgs e)
        {
            Response.Redirect("Requests.aspx");
        }

        protected void rdReDeanDecision1_CheckedChanged(object sender, EventArgs e)
        {
            //RequiredFieldValidator10.Visible = true;
            //RequiredFieldValidator14.Visible = true;
            RequiredFieldValidator21.Visible = false;
            AllRInfo.Visible = true;
            txtReDean.Visible = false;
        }

        protected void rdReDeanDecision2_CheckedChanged(object sender, EventArgs e)
        {
            //RequiredFieldValidator10.Visible = false;
            //RequiredFieldValidator14.Visible = false;
            RequiredFieldValidator21.Visible = false;
            AllRInfo.Visible = false;
            txtReDean.Visible = false;
        }

        protected void rdReDeanDecision3_CheckedChanged(object sender, EventArgs e)
        {
            //RequiredFieldValidator10.Visible = false;
            //RequiredFieldValidator14.Visible = false;
            RequiredFieldValidator21.Visible = true;
            AllRInfo.Visible = false;
            txtReDean.Visible = true;

        }

        protected void rdPrevReward_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdPrevReward.SelectedValue == "نعم")
                PrevRewardDiv.Visible = true;
            else
                PrevRewardDiv.Visible = false;
        }

        protected void lnkAddRewardedRe_Click(object sender, EventArgs e)
        {
            dtRewardedInfo = (DataTable)Session["dtRewardedInfo"];
            DataRow row = dtRewardedInfo.NewRow();
            row[0] = dtRewardedInfo.Rows.Count + 1;
            row[1] = txtRewardedReTitle.Text;
            row[2] = txtRewardedReDate.Text;
            dtRewardedInfo.Rows.Add(row);
            Session["dtRewardedInfo"] = dtRewardedInfo;
            txtRewardedReTitle.Text = "";
            txtRewardedReDate.Text="";
            GridView2.DataSource = dtRewardedInfo;
            GridView2.DataBind();

        }

        protected void lnkDeleteRewarded_Click(object sender, EventArgs e)
        {
            dtRewardedInfo = (DataTable)Session["dtRewardedInfo"];
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            dtRewardedInfo.Rows.RemoveAt(row.RowIndex);
            GridView2.DataSource = dtRewardedInfo;
            GridView2.DataBind();
            for (int i = 0; i < GridView2.Rows.Count; i++)
                GridView2.Rows[i].Cells[0].Text = (i + 1).ToString();


        }

        protected void lnkDelete_Click1(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand("Delete From NewAmountAward where AutoId="+row.Cells[1].Text, conn);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("Select * From NewAmountAward where ReqId=" + Session["ViewRequestFrom"], conn);
            GridView3.DataSource = cmd.ExecuteReader();
            GridView3.DataBind();

            conn.Close();
        }

        protected void btnAddToGrid_Click(object sender, EventArgs e)
        {

            if (ddlRName.SelectedValue != "0")
            {
                string college = "";
                DataTable dtRInfoForNewAmount = (DataTable)Session["dtRInfoForNewAmount"];
                var result = dtRInfoForNewAmount.Select("AcdId=" + ddlRName.SelectedValue);
                if (result.Length != 0)
                {
                    college = result[0][2].ToString();
                }

                //Session["ViewRequestFrom"];
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("insert into NewAmountAward values(" + Session["ViewRequestFrom"] + "," + ddlRName.SelectedValue + ",N'" + ddlRName.SelectedItem.Text
                    + "',N'" + college + "',N'" + txtOrder.Text + "','0')", conn);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("Select * From NewAmountAward where ReqId=" + Session["ViewRequestFrom"], conn);
                GridView3.DataSource = cmd.ExecuteReader();
                GridView3.DataBind();

                conn.Close();
            }

        }

        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView3.EditIndex = e.NewEditIndex;
            GridViewRow row = GridView3.Rows[e.NewEditIndex];
            string customerId = row.Cells[5].Text;
            string amount = (row.Cells[6].Controls[1] as Label).Text;

            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand cmd = new SqlCommand("Select * From NewAmountAward where ReqId=" + Session["ViewRequestFrom"], conn);
            GridView3.DataSource = cmd.ExecuteReader();
            GridView3.DataBind();
            row = GridView3.Rows[e.NewEditIndex];
            string amount1 = (row.Cells[6].Controls[1] as HtmlInputGenericControl).Value;
            (row.Cells[6].Controls[1] as HtmlInputGenericControl).Value = amount1.Split(' ')[0];
            conn.Close();
        }

        protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView3.Rows[e.RowIndex];
            string customerId = (row.Cells[1].Controls[0] as TextBox).Text;
            string amount = (row.Cells[6].Controls[1] as HtmlInputGenericControl).Value;
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand cmd = new SqlCommand("Update NewAmountAward Set NewAmount=N'"+ amount + " دينار اردني' where autoid="+ customerId + " and ReqId=" + Session["ViewRequestFrom"], conn);
            cmd.ExecuteNonQuery();
            GridView3.EditIndex = -1;
            cmd = new SqlCommand("Select * From NewAmountAward where ReqId=" + Session["ViewRequestFrom"], conn);
            GridView3.DataSource = cmd.ExecuteReader();
            GridView3.DataBind();

            conn.Close();

            //int customerId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
            //string name = (row.Cells[2].Controls[0] as TextBox).Text;
            //string country = (row.Cells[3].Controls[0] as TextBox).Text;
            
            //string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            //using (SqlConnection con = new SqlConnection(constr))
            //{
            //    using (SqlCommand cmd = new SqlCommand("UPDATE Customers SET Name = @Name, Country = @Country WHERE CustomerId = @CustomerId"))
            //    {
            //        cmd.Parameters.AddWithValue("@CustomerId", customerId);
            //        cmd.Parameters.AddWithValue("@Name", name);
            //        cmd.Parameters.AddWithValue("@Country", country);
            //        cmd.Connection = con;
            //        con.Open();
            //        cmd.ExecuteNonQuery();
            //        con.Close();
            //    }
            //}
            
            
        }
        protected void OnRowCancelingEdit(object sender, EventArgs e)
        {
            GridView3.EditIndex = -1;
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand cmd = new SqlCommand("Select * From NewAmountAward where ReqId=" + Session["ViewRequestFrom"], conn);
            GridView3.DataSource = cmd.ExecuteReader();
            GridView3.DataBind();

            conn.Close();

        }
    }
}