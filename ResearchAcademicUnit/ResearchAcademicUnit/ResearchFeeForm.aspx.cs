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
    public partial class ResearchFeeForm : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);
        DataTable dtRInfo = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["NotDefault"] != null)
                Session["backurl"] = Session["NotDefault"];
            else
                Session["backurl"] = "Default.aspx";
            //if (Session["ViewRequestFrom"] != null && Session["ViewRequestFrom"].ToString() == "7")
            if (Session["PrintForm"] != null)
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
                    //hfResearchId.Value = Session["ResearchId"].ToString();
                    dtRInfo.Columns.Add("Serial");
                    dtRInfo.Columns.Add("ReName");
                    Session["dtRInfo"] = dtRInfo;

                    if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                        conn.Open();

                    SqlCommand cmdd = new SqlCommand("select * from ResearcherInfo where acdid=" + Session["ResearchId"], conn);
                    DataTable dtt = new DataTable();
                    dtt.Load(cmdd.ExecuteReader());
                    if (dtt.Rows.Count != 0)
                    {
                        lblRName.Text = dtt.Rows[0]["RaName"].ToString();
                        lblReqName.Text = "الباحث : " + dtt.Rows[0]["RaName"].ToString();
                        lblReqNameSig.Text = "التوقيع : " + dtt.Rows[0]["RaName"].ToString();
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

                        cmdd = new SqlCommand("Select *, (Currency +'-'+CurCode) Name From Currency", conn);
                        ddlCurrency.DataSource = cmdd.ExecuteReader();
                        ddlCurrency.DataTextField = "Name";
                        ddlCurrency.DataValueField = "Code";
                        ddlCurrency.DataBind();
                        ddlCurrency.Items.Insert(0, "حدد العملة");
                        ddlCurrency.Items[0].Value = "0";
                        //string ff = Session["RequestComeFrom"].ToString();
                        //string ff1 = Session["ViewRequestFrom"].ToString();

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
                            reqid.InnerText ="رقم الطلب : "+ Session["ViewRequestFrom"].ToString();
                            int ReqId = Convert.ToInt32(Session["RequestComeFrom"] != null ? Session["RequestComeFrom"] : Session["ViewRequestFrom"]);

                            cmdd = new SqlCommand("Select * From ResearchFeeInfo where AutoId=" + ReqId, conn);
                            SqlDataReader dr = cmdd.ExecuteReader();
                            dr.Read();
                            txtHcst.Text = dr[2].ToString();
                            txtOrcid.Text = dr[3].ToString();
                            txtGSLink.Text = dr[4].ToString();
                            txtRGLink.Text = dr[5].ToString();
                            txtRTitle.Text = dr[6].ToString();
                            rtDiv.InnerHtml = dr[6].ToString();
                            //rtDiv.Style.Add("display", "block");
                            txtAcceptDate.Text = dr[7].ToString();
                            txtMagName.Text = dr[8].ToString();
                            txtMagISSN.Text = dr[9].ToString();
                            txtPubName.Text = dr[10].ToString();
                            txtMagYear.Text = dr[11].ToString();
                            txtFeeValue.Text = dr[12].ToString();
                            ddlCurrency.SelectedValue = dr[13].ToString();
                            txtDBIndex.Text = dr["DBIndex"].ToString();
                            txtResearcherId.Text = dr["ResearcherId"].ToString();

                            cmdd = new SqlCommand("Select ROW_NUMBER() over(order by autoid) Serial,ReName From RePartInfo where RequestId=" + ReqId, conn);
                            dtRInfo = (DataTable)Session["dtRInfo"];
                            dtRInfo.Load(cmdd.ExecuteReader());
                            GridView1.DataSource = dtRInfo;
                            GridView1.DataBind();
                            lblReqDate.Text = "التاريخ : " + Convert.ToDateTime(dr["ReqDate"]).ToString("dd-MM-yyyy");
                            try
                            {
                                var resultData = Directory.GetFiles(Server.MapPath("document/forms/support/" + ReqId), "*.*", SearchOption.AllDirectories)
                                                .Select(x => new { FileName = Path.GetFileName(x), FilePath = x, DirName = Path.GetDirectoryName(x) });

                                foreach (var item in resultData)
                                {
                                    //if (item.FileName.Contains("1"))
                                    //{
                                    //    lnkViewF1.Visible = true;
                                    //    lblf1.Text = item.FilePath;
                                    //    reqFile1.Visible = false;
                                    //}
                                    if (item.FileName.Contains("2"))
                                    {
                                        A2.HRef = "document/forms/support/" + ReqId + "/" + item.FileName; //+ "/2_research.pdf";// item.FilePath;
                                        A2.Visible = true;
                                        lnkViewF2.Visible = false;
                                        lblf2.Text = item.FilePath;
                                        RequiredFieldValidator11.Visible = false;
                                    }
                                    if (item.FileName.Contains("3"))
                                    {
                                        A3.HRef = "document/forms/support/" + ReqId + "/" + item.FileName; //"/3_accept.pdf";// item.FilePath;
                                        A3.Visible = true;

                                        lnkViewF3.Visible = false;
                                        lblf3.Text = item.FilePath;
                                        RequiredFieldValidator12.Visible = false;
                                    }
                                    if (item.FileName.Contains("4"))
                                    {
                                        A4.HRef = "document/forms/support/" + ReqId + "/" + item.FileName; // "/4_payedfee.pdf";// item.FilePath;
                                        A4.Visible = true;
                                        lnkViewF4.Visible = false;
                                        lblf4.Text = item.FilePath;
                                        RequiredFieldValidator13.Visible = false;
                                    }
                                    if (item.FileName.Contains("5"))
                                    {
                                        A5.HRef = "document/forms/support/" + ReqId + "/" + item.FileName; // "/5_bill.pdf";// item.FilePath;
                                        A5.Visible = true;
                                        lnkViewF5.Visible = false;
                                        lblf5.Text = item.FilePath;
                                        RequiredFieldValidator20.Visible = false;
                                    }
                                }

                                if (dr[14].ToString() == "S" || (Session["justView"] != null && Session["justView"].ToString() == "ok"))
                                {
                                    txtHcst.ReadOnly = true;
                                    txtEmail.ReadOnly = true;
                                    txtOrcid.ReadOnly = true;
                                    txtGSLink.ReadOnly = true;
                                    txtRGLink.ReadOnly = true;
                                    txtRTitle.ReadOnly = true;
                                    txtAcceptDate.ReadOnly = true;
                                    txtMagName.ReadOnly = true;
                                    txtMagISSN.ReadOnly = true;
                                    txtPubName.ReadOnly = true;
                                    txtMagYear.ReadOnly = true;
                                    txtFeeValue.ReadOnly = true;
                                    txtPartReName.ReadOnly = true;
                                    txtDBIndex.ReadOnly = true;
                                    GridView1.Columns[2].Visible = false;
                                    ddlCurrency.Enabled = false;
                                    lnkAddPartR.Visible = false;
                                    FileUpload1.Visible = false;
                                    FileUpload2.Visible = false;
                                    FileUpload3.Visible = false;
                                    FileUpload4.Visible = false;
                                    FileUpload5.Visible = false;
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
                                                A1.HRef = "document/forms/support/" + ReqId + "/" + item.FileName; // "/1_index.pdf";// item.FilePath;
                                                A1.Visible = true;

                                                lnkViewF1.Visible = false;
                                                lblf1.Text = item.FilePath;
                                                reqFile1.Visible = false;
                                            }
                                        }
                                        DirectorDiv.Visible = true;
                                        DeanDiv.Visible = true;
                                        SqlCommand cmdDirector = new SqlCommand("select Notes from RequestsFollowUp where type=0 and requestid=" + Session["ViewRequestFrom"] + " and ReqFromId =( SELECT p.[AutoId] FROM Priviliges p,Department d where p.PrivDeptId=d.autoid and d.DeptName=N'" + lblDept.Text + "')", conn);
                                        SqlDataReader drDirector = cmdDirector.ExecuteReader();
                                        drDirector.Read();
                                        txtDirNotes.Text = drDirector[0].ToString();
                                        txtDirNotes.ReadOnly = true;

                                        SqlCommand cmdDean = new SqlCommand("select Notes from RequestsFollowUp where type=0 and requestid=" + Session["ViewRequestFrom"] + " and ReqFromId =( SELECT p.[AutoId] FROM Priviliges p,Faculty f where p.PrivFacultyId=f.autoid and p.privtype=1 and f.CollegeName=N'" + lblFaculty.Text + "')", conn);
                                        SqlDataReader drDean = cmdDean.ExecuteReader();
                                        drDean.Read();
                                        txtDeanNotes.Text = drDean[0].ToString();
                                        txtDeanNotes.ReadOnly = true;

                                        ReDirectorDiv.Visible = true;
                                        lblff1.Visible = true;
                                        FileUpload1.Visible = true;
                                        reqFile1.Visible = true;
                                        SqlCommand cmdDirInfo = new SqlCommand("select * from ReDirectorInfo where type=0 and requestid=" + Session["ViewRequestFrom"], conn);
                                        DataTable dtDirInfo = new DataTable();
                                        dtDirInfo.Load(cmdDirInfo.ExecuteReader());
                                        if (dtDirInfo.Rows.Count != 0)
                                        {
                                            txtyearmonth.Text = dtDirInfo.Rows[0][2].ToString();
                                            txtFeeV.Text = dtDirInfo.Rows[0][3].ToString();
                                            txtReDirector.Text = dtDirInfo.Rows[0][11].ToString();

                                            chkFiles.Items[0].Selected = dtDirInfo.Rows[0][5].ToString() != "0" ? true : false;
                                            chkFiles.Items[1].Selected = dtDirInfo.Rows[0][6].ToString() != "0" ? true : false;
                                            chkFiles.Items[2].Selected = dtDirInfo.Rows[0][7].ToString() != "0" ? true : false;
                                            chkFiles.Items[3].Selected = dtDirInfo.Rows[0][8].ToString() != "0" ? true : false;
                                            chkFiles.Items[4].Selected = dtDirInfo.Rows[0][9].ToString() != "0" ? true : false;
                                            chkFiles.Items[5].Selected = dtDirInfo.Rows[0][10].ToString() != "0" ? true : false;
                                            //chkFiles.Enabled = false;
                                            //txtReDirector.ReadOnly = true;
                                            //txtyearmonth.ReadOnly = true;
                                        }

                                    }
                                    if (Session["Dir_Dean_Priv"] != null && Session["Dir_Dean_Priv"].ToString() == "ReDean")
                                    {
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
                                        lblff1.Visible = true;
                                        SqlCommand cmdDirector = new SqlCommand("select Notes from RequestsFollowUp where type=0 and requestid=" + Session["ViewRequestFrom"] + " and ReqFromId =( SELECT p.[AutoId] FROM Priviliges p,Department d where p.PrivDeptId=d.autoid and d.DeptName=N'" + lblDept.Text + "')", conn);
                                        SqlDataReader drDirector = cmdDirector.ExecuteReader();
                                        drDirector.Read();
                                        txtDirNotes.Text = drDirector[0].ToString();
                                        txtDirNotes.ReadOnly = true;

                                        SqlCommand cmdDean = new SqlCommand("select Notes from RequestsFollowUp where type=0 and requestid=" + Session["ViewRequestFrom"] + " and ReqFromId =( SELECT p.[AutoId] FROM Priviliges p,Faculty f where p.PrivFacultyId=f.autoid and p.privtype=1 and f.CollegeName=N'" + lblFaculty.Text + "')", conn);
                                        SqlDataReader drDean = cmdDean.ExecuteReader();
                                        drDean.Read();
                                        txtDeanNotes.Text = drDean[0].ToString();
                                        txtDeanNotes.ReadOnly = true;

                                        SqlCommand cmdDirInfo = new SqlCommand("select * from ReDirectorInfo where type=0 and requestid=" + Session["ViewRequestFrom"], conn);
                                        DataTable dtDirInfo = new DataTable();
                                        dtDirInfo.Load(cmdDirInfo.ExecuteReader());
                                        if (dtDirInfo.Rows.Count != 0)
                                        {
                                            txtyearmonth.Text = dtDirInfo.Rows[0][2].ToString();
                                            txtFeeV.Text = dtDirInfo.Rows[0][3].ToString();
                                            txtReDirector.Text = dtDirInfo.Rows[0][16].ToString();
                                            lblReDirDate.Text = Convert.ToDateTime(dtDirInfo.Rows[0][17]).ToString("dd-MM-yyyy");

                                            chkFiles.Items[0].Selected = dtDirInfo.Rows[0][5].ToString() != "0" ? true : false;
                                            chkFiles.Items[1].Selected = dtDirInfo.Rows[0][6].ToString() != "0" ? true : false;
                                            chkFiles.Items[2].Selected = dtDirInfo.Rows[0][7].ToString() != "0" ? true : false;
                                            chkFiles.Items[3].Selected = dtDirInfo.Rows[0][8].ToString() != "0" ? true : false;
                                            chkFiles.Items[4].Selected = dtDirInfo.Rows[0][9].ToString() != "0" ? true : false;
                                            chkFiles.Items[5].Selected = dtDirInfo.Rows[0][10].ToString() != "0" ? true : false;
                                            chkFiles.Items[6].Selected = dtDirInfo.Rows[0][11].ToString() != "0" ? true : false;
                                            rdQuarter.Visible = true;
                                            try
                                            {
                                                rdQuarter.SelectedValue = dtDirInfo.Rows[0][4].ToString();
                                            }
                                            catch { }
                                            rdCheck.SelectedValue = dtDirInfo.Rows[0][12].ToString();
                                            try
                                            {
                                                chkClarivate.Items[0].Selected = dtDirInfo.Rows[0][13].ToString() != "0" ? true : false;
                                                chkClarivate.Items[1].Selected = dtDirInfo.Rows[0][14].ToString() != "0" ? true : false;
                                                chkClarivate.Items[2].Selected = dtDirInfo.Rows[0][15].ToString() != "0" ? true : false;
                                            }
                                            catch { }
                                            chkFiles.Enabled = false;
                                            chkClarivate.Enabled = false;
                                            txtReDirector.ReadOnly = true;
                                            txtyearmonth.ReadOnly = true;
                                            txtFeeV.ReadOnly = true;
                                            rdQuarter.Enabled = false;
                                            rdCheck.Enabled = false;
                                        }
                                        foreach (var item in resultData)
                                        {
                                            if (item.FileName.Contains("1"))
                                            {
                                                A1.HRef = "document/forms/support/" + ReqId + "/" + item.FileName; // "/1_index.pdf";// item.FilePath;
                                                A1.Visible = true;

                                                lnkViewF1.Visible = false;
                                                lblf1.Text = item.FilePath;
                                                reqFile1.Visible = false;
                                            }
                                        }

                                        lblReDeanDate.Text = "التاريخ : " + DateTime.Now.Date.ToString("dd-MM-yyyy");
                                        //cmdd = new SqlCommand("Select Notes,RequestDate From RequestsFollowUp ru,researcherinfo ri where ru.actualid=ri.AcdId and Autoid=(Select Max(AutoId) From RequestsFollowUp where RequestId=" + Session["ViewRequestFrom"] + " and ReqFromName like N'%رئيس قسم البحث العلمي%' and RquToName like N'%عميد الدراسات العليا والبحث العلمي%')", conn);
                                        //SqlDataReader drdirDNotes1 = cmdd.ExecuteReader();
                                        //drdirDNotes1.Read();
                                        //txtReDirector.Text = drdirDNotes1[0].ToString();
                                        //lblReDirDate.Text = Convert.ToDateTime(drdirDNotes1[1]).ToString("dd-MM-yyyy");
                                    }
                                    if (Session["PrintForm"] != null)
                                    {
                                        //DirectorDiv.Visible = false;
                                        //DeanDiv.Visible = false;
                                        ReDirectorDiv.Visible = false;
                                        foreach (var item in resultData)
                                        {
                                            if (item.FileName.Contains("1"))
                                            {
                                                A1.HRef = "document/forms/support/" + ReqId + "/1_index.pdf";// item.FilePath;
                                                A1.Visible = true;

                                                lnkViewF1.Visible = false;
                                                lblf1.Text = item.FilePath;
                                                reqFile1.Visible = false;
                                            }
                                        }

                                        //lblff1.Visible = true;
                                        //FileUpload1.Visible = true;
                                        //reqFile1.Visible = true;

                                    }
                                    try
                                    {
                                        cmdd = new SqlCommand("Select ActualId,RequestDate,RaName From RequestsFollowUp ru,researcherinfo ri,Priviliges p where ReqFromId=p.autoid and p.PrivTo=ri.AcdId and ru.type=0 and ru.Autoid=(Select Max(AutoId) From RequestsFollowUp where type=0 and RequestId=" + Session["ViewRequestFrom"] + " and ReqFromName like N'%رئيس قسم%' and RquToName like N'%عميد كلية%')", conn);
                                        SqlDataReader drdirDNotes = cmdd.ExecuteReader();
                                        drdirDNotes.Read();
                                        lblDirName.Text = "رئيس القسم : " + drdirDNotes[2].ToString();
                                        lblDirNameSig.Text = "التوقيع : " + drdirDNotes[2].ToString();
                                        lblDirDate.Text = "التاريخ : " + Convert.ToDateTime(drdirDNotes[1]).ToString("dd-MM-yyyy");

                                        cmdd = new SqlCommand("Select Notes,RequestDate,RaName From RequestsFollowUp ru,researcherinfo ri ,priviliges p where  p.AutoId=ru.ReqFromId and ru.ActualId=ri.AcdId and type=0 and ru.Autoid=(Select Max(AutoId) From RequestsFollowUp where type=0 and RequestId=" + Session["ViewRequestFrom"] + " and ReqFromName like N'%عميد كلية%')", conn);
                                        SqlDataReader drDNotes = cmdd.ExecuteReader();
                                        drDNotes.Read();
                                        deanDecDiv.InnerText = drDNotes[0].ToString();
                                        deanDecDivSig.InnerHtml = "الاسم : " + drDNotes[2].ToString() + "<br> التوقيع : " + drDNotes[2].ToString();
                                        lblDeanDecDate.Text = "التاريخ : " + Convert.ToDateTime(drDNotes[1]).ToString("dd-MM-yyyy");

                                        cmdd = new SqlCommand("Select Notes,RequestDate,RaName From RequestsFollowUp,researcherinfo ri where actualid=ri.AcdId and type=0 and Autoid=(Select Max(AutoId) From RequestsFollowUp where type=0 and RequestId=" + Session["ViewRequestFrom"] + " and RquToName like N'%البحث العلمي%')", conn);
                                        SqlDataReader drRDirNotes = cmdd.ExecuteReader();
                                        drRDirNotes.Read();
                                        RDirDecDiv.InnerText = drRDirNotes[0].ToString();
                                        RDirDecDivSig.InnerHtml = "الاسم : " + drRDirNotes[2].ToString() + "<br> التوقيع : " + drRDirNotes[2].ToString();
                                        Label2.Text = "التاريخ : " + Convert.ToDateTime(drRDirNotes[1]).ToString("dd-MM-yyyy");


                                        cmdd = new SqlCommand("Select Notes,RequestDate,RaName From RequestsFollowUp,researcherinfo ri where actualid=ri.AcdId and type=0 and Autoid=(Select Max(AutoId) From RequestsFollowUp where type=0 and RequestId=" + Session["ViewRequestFrom"] + " and ReqStatus like N'%مكتمل%')", conn);
                                        SqlDataReader drRDNotes = cmdd.ExecuteReader();
                                        drRDNotes.Read();
                                        RdeanDecDiv.InnerText = drRDNotes[0].ToString();
                                        RdeanDecDivSig.InnerHtml = "الاسم : " + drRDNotes[2].ToString() + "<br> التوقيع :" + drRDNotes[2].ToString();
                                        lblRDeanDecDate.Text = "التاريخ : " + Convert.ToDateTime(drRDNotes[1]).ToString("dd-MM-yyyy");
                                        foreach (var item in resultData)
                                        {
                                            if (item.FileName.Contains("1"))
                                            {
                                                A1.HRef = "document/forms/support/" + ReqId + "/1_index.pdf";// item.FilePath;
                                                A1.Visible = true;

                                                lblff1.Visible = true;
                                                lnkViewF1.Visible = false;
                                                lblf1.Text = item.FilePath;
                                                reqFile1.Visible = false;
                                            }
                                        }

                                    }
                                    catch { }
                                }
                            }
                            catch { }
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
            //if (txtOrcid.Text != "" || txtRGLink.Text != "" || txtResearcherId.Text!="")
            //{
                if (GridView1.Rows.Count != 0)
                {
                    if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                        conn.Open();
                    string sql = "";
                    SqlCommand cmd;

                    //cmd = new SqlCommand("Select * from ResearchFeeInfo where AutoId=" + Session["RequestComeFrom"], conn);
                    //SqlDataReader drCheck = cmd.ExecuteReader();
                    //drCheck.Read();
                    //Session


                    DataTable dtt = new DataTable();
                    if (Session["RequestComeFrom"] == null)
                    {
                        sql = "Insert into ResearchFeeInfo values(@rid,@hcst,@orcid,@gslink,@rglink,@rtitle,@d,@magname,@magissn,@pub,@year,@fee,@cur,'S',@ReqDate,'',N'قيد الدراسة','','',@DBIndex,@researcherid,0)";
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
                        cmd.Parameters.AddWithValue("@fee", txtFeeValue.Text);
                        cmd.Parameters.AddWithValue("@cur", ddlCurrency.SelectedValue);
                        cmd.Parameters.AddWithValue("@d", txtAcceptDate.Text);
                        cmd.Parameters.AddWithValue("@ReqDate", DateTime.Now.Date);
                        cmd.Parameters.AddWithValue("@DBIndex", txtDBIndex.Text);
                        cmd.Parameters.AddWithValue("@researcherid", txtResearcherId.Text);
                        cmd.ExecuteNonQuery();

                        cmd = new SqlCommand("select max(autoid) from ResearchFeeInfo where jobid=" + Session["uid"], conn);
                        dtt.Load(cmd.ExecuteReader());
                        Session["RequestComeFrom"] = dtt.Rows[0][0];
                        for (int i = 0; i < GridView1.Rows.Count; i++)
                        {
                            cmd = new SqlCommand("Insert into RePartInfo values(" + dtt.Rows[0][0] + ",N'" + GridView1.Rows[i].Cells[1].Text + "')", conn);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        sql = "Update ResearchFeeInfo Set ";
                        sql += "hcstID=@hcst,orcidId=@orcid,GSLink=@gslink,RGLink=@rglink,";
                        sql += "ReTitle=@rtitle,AcceptDate=@d,MagName=@magname,MagISSN=@magissn,";
                        sql += "PublisherName=@pub,MagYear=@year,FeeValue=@fee,FeeCurrency=@cur,";
                        sql += "RequestType='S',RequestFinalStatus=N'قيد الدراسة',DBIndex=@DBIndex";
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
                        cmd.Parameters.AddWithValue("@fee", txtFeeValue.Text);
                        cmd.Parameters.AddWithValue("@cur", ddlCurrency.SelectedValue);
                        cmd.Parameters.AddWithValue("@d", txtAcceptDate.Text);
                        cmd.Parameters.AddWithValue("@DBIndex", txtDBIndex.Text);
                        cmd.Parameters.AddWithValue("@researcherid", txtResearcherId.Text);
                        cmd.ExecuteNonQuery();

                        cmd = new SqlCommand("delete from RePartInfo where RequestId=" + Session["RequestComeFrom"], conn);
                        cmd.ExecuteNonQuery();

                        for (int i = 0; i < GridView1.Rows.Count; i++)
                        {
                            cmd = new SqlCommand("Insert into RePartInfo values(" + Session["RequestComeFrom"] + ",N'" + GridView1.Rows[i].Cells[1].Text + "')", conn);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    //SqlCommand cmdEmail = new SqlCommand("Update Users Set Email='" + txtEmail.Text + "' where AcdId=" + lblJobId.Text, conn);
                    //cmdEmail.ExecuteNonQuery();
                    //Session["UserEmail"] = txtEmail.Text;


                    //cmd = new SqlCommand("Update RequestsFollowUp Set ReqStatus=N'انجزت' where AutoId=(select max(Autoid) from RequestsFollowUp where RequestId=" + Session["RequestComeFrom"] + ")", conn);
                    //cmd.ExecuteNonQuery();

                    //sql = @"SELECT p.AutoId, p.PrivTo,Email
                    //        FROM ResearcherInfo r, faculty f, department d, priviliges p
                    //        where r.College=f.CollegeName and r.Dept=d.DeptName and f.AutoId=p.PrivFacultyId and d.AutoId=p.PrivDeptId and r.AcdId=p.PrivTo
                    //        and f.CollegeName=N'" + lblFaculty.Text + "' and d.DeptName=N'" + lblDept.Text + "'";

                    //cmd = new SqlCommand(sql, conn);
                    //DataTable dtPrivDept = new DataTable();
                    //dtPrivDept.Load(cmd.ExecuteReader());

                    //cmd = new SqlCommand("Select * from ResearchFeeInfo where AutoId=" + Session["RequestComeFrom"], conn);
                    //SqlDataReader drCheck = cmd.ExecuteReader();
                    //drCheck.Read();
                    //cmd = new SqlCommand("insert into RequestsFollowUp values(@reqid,@fromid,@fromname,@toid,@toname,@reqdate,@reqst,@actualid,N'',N'')", conn);
                    //cmd.Parameters.AddWithValue("@reqid", Session["RequestComeFrom"]);
                    //cmd.Parameters.AddWithValue("@fromid", Session["uid"]);
                    //cmd.Parameters.AddWithValue("@fromname", Session["userName"]);
                    //if (drCheck["RequestFinalStatus"].ToString().Contains("معلق"))
                    //{
                    //    cmd.Parameters.AddWithValue("@toid", 11);
                    //    cmd.Parameters.AddWithValue("@toname", "رئيس قسم البحث العلمي");
                    //}
                    //else
                    //{
                    //    cmd.Parameters.AddWithValue("@toid", dtPrivDept.Rows[0][0]);
                    //    cmd.Parameters.AddWithValue("@toname", "رئيس قسم " + lblDept.Text);
                    //}
                    //cmd.Parameters.AddWithValue("@reqdate", DateTime.Now.Date);
                    //cmd.Parameters.AddWithValue("@reqst", "قيد التنفيذ");

                    //cmd.Parameters.AddWithValue("@actualid", Session["uid"]);
                    //cmd.ExecuteNonQuery();
                    //Session["ss"]= dtPrivDept.Rows[0][2].ToString();
                    upload();
                    Session["ReDirectorFinal"] = "no";
                    ConfirmDiv.Visible = true;
                    //msgDiv.Visible = true;
                    //lblMsg.Text = "تم ارسال الطلب بنجاح";
                    //Session["RequestComeFrom"] = null;
                    //Session["ViewRequestFrom"] = null;
                    //StringBuilder msg = new StringBuilder();
                    //msg.Append("<body dir='rtl'><b>الباحث الكريم،</b>");
                    //msg.Append("<br><br>تم استقبال طلبك بنجاح");
                    //msg.Append("<br><a href='http://meusr-ra.meu.edu.jo/'>اضغط هنا لمتابعة الطلب</a>");
                    //msg.Append("<br><br><b>عمادة الدرسات العليا والبحث العلمي</b>");
                    //msg.Append("<br><b>قسم البحث العلمي</b>");
                    ////sendEmail(txtEmail.Text, msg.ToString());

                    //StringBuilder msg1 = new StringBuilder();
                    //msg1.Append("<body dir='rtl'><b>رئيس قسم " + lblDept.Text + " المحترم،</b>");
                    //msg1.Append("<br>تم ارسال طلب جديد أرجو من حضرتكم التكرم");
                    //msg1.Append(" <a href='http://meusr-ra.meu.edu.jo/' target='_blank'>بالدخول إلى موقع البحث العلمي </a> ");
                    //msg1.Append(" ودراسة الطلب وارسال توصيتكم الى عميد الكلية ");
                    //msg1.Append("<br><br><b>عمادة الدرسات العليا والبحث العلمي</b>");
                    //msg1.Append("<br><b>قسم البحث العلمي</b>");
                    ////sendEmail(dtPrivDept.Rows[0][2].ToString(), msg1.ToString());

                    //conn.Close();
                    //Response.Redirect("Requests.aspx");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('يجب إدخال اسماء المشاركين بالبحث');", true);
                }
            //}
            //else
            //{
            //    Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('يجب إدخال أحد المعلومات التالية : ORCID - Google Scholar - Research Gate -  الرقم الوطني البحثي');", true);
            //}
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
                Subject = "طلب دعم رسوم نشر بحث علمي",
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
                string sql = "Insert into ResearchFeeInfo values(@rid,@hcst,@orcid,@gslink,@rglink,@rtitle,@d,@magname,@magissn,@pub,@year,@fee,@cur,'NS',@ReqDate,'',N'قيد الدراسة','','',@DBIndex,@researcherid,0)";
                SqlCommand cmd = new SqlCommand(sql, conn);
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
                cmd.Parameters.AddWithValue("@fee", txtFeeValue.Text);
                cmd.Parameters.AddWithValue("@cur", ddlCurrency.SelectedValue);
                cmd.Parameters.AddWithValue("@ReqDate", DateTime.Now.Date);
                cmd.Parameters.AddWithValue("@d", txtAcceptDate.Text);
                cmd.Parameters.AddWithValue("@DBIndex", txtDBIndex.Text);
                cmd.Parameters.AddWithValue("@researcherid", txtResearcherId.Text);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("select max(autoid) from ResearchFeeInfo where jobid=" + Session["uid"], conn);
                DataTable dtt = new DataTable();
                dtt.Load(cmd.ExecuteReader());
                Session["RequestComeFrom"] = dtt.Rows[0][0];

                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    cmd = new SqlCommand("Insert into RePartInfo values(" + dtt.Rows[0][0] + ",N'" + GridView1.Rows[i].Cells[1].Text + "')", conn);
                    cmd.ExecuteNonQuery();
                }
            }
            else
            {
                string sql = "Update ResearchFeeInfo Set ";
                sql += "hcstID=@hcst,orcidId=@orcid,GSLink=@gslink,RGLink=@rglink,";
                sql += "ReTitle=@rtitle,AcceptDate=@d,MagName=@magname,MagISSN=@magissn,";
                sql += "PublisherName=@pub,MagYear=@year,FeeValue=@fee,FeeCurrency=@cur,";
                sql += "RequestType='NS',RequestFinalStatus=N'قيد الدراسة',DBIndex=@DBIndex ";
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
                cmd.Parameters.AddWithValue("@fee", txtFeeValue.Text);
                cmd.Parameters.AddWithValue("@cur", ddlCurrency.SelectedValue);
                cmd.Parameters.AddWithValue("@d", txtAcceptDate.Text);
                cmd.Parameters.AddWithValue("@DBIndex", txtDBIndex.Text);
                cmd.Parameters.AddWithValue("@researcherid", txtResearcherId.Text);
                cmd.ExecuteNonQuery();


                cmd = new SqlCommand("delete from RePartInfo where RequestId=" + Session["RequestComeFrom"], conn);
                cmd.ExecuteNonQuery();

                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    cmd = new SqlCommand("Insert into RePartInfo values(" + Session["RequestComeFrom"] + ",N'" + GridView1.Rows[i].Cells[1].Text + "')", conn);
                    cmd.ExecuteNonQuery();
                }
            }

            SqlCommand cmdEmail = new SqlCommand("Update Users Set Email='" + txtEmail.Text + "' where AcdId=" + lblJobId.Text, conn);
            cmdEmail.ExecuteNonQuery();
            Session["UserEmail"] = txtEmail.Text;
            upload();

            msgDiv.Visible = true;
            lblMsg.Text = "تم التخزين بشكل ناجح مع إمكانية التعديل على البيانات";
            //Timer1.Enabled = true;

        }

        protected void upload()
        {
            int ReqId = Convert.ToInt32(Session["RequestComeFrom"] != null ? Session["RequestComeFrom"] : Session["ViewRequestFrom"]);
            string subPath = "document/forms/support/" + ReqId;

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

            if (FileUpload3.HasFile)
            {
                string fileName = Path.GetFileName(FileUpload3.PostedFile.FileName);
                string fileExtension = Path.GetExtension(FileUpload3.PostedFile.FileName);

                fullname = "3_accept" + fileExtension;
                string fileLocation = Server.MapPath(subdir + "/" + fullname);
                Session["file3"] = fileLocation;
                if (File.Exists(fileLocation))
                {
                    File.Delete(fileLocation);
                }
                FileUpload3.SaveAs(fileLocation);
            }

            if (FileUpload4.HasFile)
            {
                string fileName = Path.GetFileName(FileUpload4.PostedFile.FileName);
                string fileExtension = Path.GetExtension(FileUpload4.PostedFile.FileName);

                fullname = "4_payedfee" +
                    "" + fileExtension;
                string fileLocation = Server.MapPath(subdir + "/" + fullname);
                Session["file4"] = fileLocation;
                if (File.Exists(fileLocation))
                {
                    File.Delete(fileLocation);
                }
                FileUpload4.SaveAs(fileLocation);
            }

            if (FileUpload5.HasFile)
            {
                string fileName = Path.GetFileName(FileUpload5.PostedFile.FileName);
                string fileExtension = Path.GetExtension(FileUpload5.PostedFile.FileName);

                fullname = "5_bill" +
                    "" + fileExtension;
                string fileLocation = Server.MapPath(subdir + "/" + fullname);
                Session["file5"] = fileLocation;
                if (File.Exists(fileLocation))
                {
                    File.Delete(fileLocation);
                }
                FileUpload5.SaveAs(fileLocation);
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
                case "F3":
                    path = lblf3.Text;
                    break;
                case "F4":
                    path = lblf4.Text;
                    break;
                case "F5":
                    path = lblf5.Text;
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
                    Session["imagefile"] = "~/document/forms/support/" + p[p.Length - 2] + "/" + p[p.Length - 1];
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
                    zip.AddFile(lblf3.Text, Session["ViewRequestFrom"].ToString());
                    zip.AddFile(lblf4.Text, Session["ViewRequestFrom"].ToString());

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
            catch (Exception ex){ }
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

            cmd = new SqlCommand("insert into RequestsFollowUp values(@reqid,@fromid,@fromname,@toid,@toname,@reqdate,@reqst,@actualid,@notes,@dec,0)", conn);
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
                SqlCommand cmdUp = new SqlCommand("Update ResearchFeeInfo Set RequestType='S',RequestFinalStatus=N'مغلقة' where AutoId=" + Session["ViewRequestFrom"], conn);
                cmdUp.ExecuteNonQuery();
            }
            else if (id.ToLower().Contains("post")) //if (rdDirDecision.SelectedValue == "2")
            {
                
                cmd.Parameters.AddWithValue("@toid", lblJobId.Text);
                cmd.Parameters.AddWithValue("@toname", lblRName.Text);
                SqlCommand cmdUp = new SqlCommand("Update ResearchFeeInfo Set RequestType='NS',RequestFinalStatus=N'" + txtDirNotes.Text + "' where AutoId=" + Session["ViewRequestFrom"], conn);
                cmdUp.ExecuteNonQuery();
            }


            cmd.Parameters.AddWithValue("@reqdate", DateTime.Now.Date);
            cmd.Parameters.AddWithValue("@reqst",st);
            cmd.Parameters.AddWithValue("@actualid", Session["uid"]);
            cmd.Parameters.AddWithValue("@notes", txtDirNotes.Text);
            cmd.ExecuteNonQuery();

            cmd = new SqlCommand("update RequestsFollowUp Set ReqStatus=N'انجزت' where type=0 and AutoId=" + Session["AutoIdUpdated"], conn);
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
                            where type=0 and autoid=(select max(autoid) from RequestsFollowUp where type=0 and requestid=" + Session["ViewRequestFrom"]+")";
            cmd = new SqlCommand(sql, conn);
            SqlDataReader drDean = cmd.ExecuteReader();
            drDean.Read();
            string privdean = drDean[4].ToString();

            cmd = new SqlCommand("insert into RequestsFollowUp values(@reqid,@fromid,@fromname,@toid,@toname,@reqdate,@reqst,@actualid,@notes,@dec,0)", conn);
            cmd.Parameters.AddWithValue("@reqid", Session["ViewRequestFrom"]);
            cmd.Parameters.AddWithValue("@fromid", privdean);
            cmd.Parameters.AddWithValue("@fromname", "عميد كلية " + lblFaculty.Text);
            string st = "قيد التنفيذ";
            if(id.ToLower().Contains("ok"))// if (rdDeanDecision.SelectedValue == "1")
            {
                cmd.Parameters.AddWithValue("@toid", 11);
                cmd.Parameters.AddWithValue("@toname", "رئيس قسم البحث العلمي");
                SqlCommand cmdUp = new SqlCommand("Update ResearchFeeInfo Set RequestFinalStatus=N'قيد المعالجة',FacultyReqNo=N'" + lblSaderNo.Text +"/"+ Session["ViewRequestFrom"] + "',GSInNo=N'ع د ع / "+ Session["ViewRequestFrom"] + "' where AutoId=" + Session["ViewRequestFrom"], conn);
                cmdUp.ExecuteNonQuery();

                SqlCommand cmd11 = new SqlCommand("Select * From Priviliges where PrivType=2 and PrivFacultyId=11", conn);
                SqlDataReader drSecGS = cmd11.ExecuteReader();
                drSecGS.Read();

                System.Text.StringBuilder msg = new System.Text.StringBuilder();
                msg.Clear();
                msg.Append("<body dir='rtl'><b>رئيس قسم البحث العلمي المحترم،</b>");
                msg.Append("<br>الرجاء دراسة طلب دعم رسوم نشر بحث علمي لطلب الباحث " + lblRName.Text);
                msg.Append(" <a href='http://meusr-ra.meu.edu.jo/' target='_blank'>بالدخول إلى موقع البحث العلمي </a> ");
                msg.Append("<br><br><b>عميد كلية "+lblFaculty.Text+"</b>");
                sendEmail(drSecGS[5].ToString(), msg.ToString());
            }
            else if (id.ToLower().Contains("post"))//if (rdDeanDecision.SelectedValue == "3")
            {
                cmd.Parameters.AddWithValue("@toid", lblJobId.Text);
                cmd.Parameters.AddWithValue("@toname", lblRName.Text);
                SqlCommand cmdUp = new SqlCommand("Update ResearchFeeInfo Set RequestType='NS',RequestFinalStatus=N'" + txtDeanNotes.Text + "' where AutoId=" + Session["ViewRequestFrom"], conn);
                cmdUp.ExecuteNonQuery();
            }
            else if (id.ToLower().Contains("no"))//if (rdDeanDecision.SelectedValue == "2")
            {
                st = "مغلقة";
                cmd.Parameters.AddWithValue("@toid", lblJobId.Text);
                cmd.Parameters.AddWithValue("@toname", lblRName.Text);
                SqlCommand cmdUp = new SqlCommand("Update ResearchFeeInfo Set RequestType='S',RequestFinalStatus=N'مغلقة' where AutoId=" + Session["ViewRequestFrom"], conn);
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

            cmd = new SqlCommand("update RequestsFollowUp Set ReqStatus=N'انجزت' where type=0 and AutoId=" + Session["AutoIdUpdated"], conn);
            cmd.ExecuteNonQuery();
            //upload();
            conn.Close();
            Response.Redirect("Requests.aspx");
        }

        protected void btnReDirector_Click(object sender, EventArgs e)
        {
            int ReqId = Convert.ToInt32(Session["RequestComeFrom"] != null ? Session["RequestComeFrom"] : Session["ViewRequestFrom"]);
            string subPath = "document/forms/support/" + ReqId;

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

            //    SqlCommand cmdUp = new SqlCommand("Update ResearchFeeInfo Set RequestFinalStatus=N'تعليق - "+txtReDirector.Text+"' where AutoId=" + Session["ViewRequestFrom"], conn);
            //    cmdUp.ExecuteNonQuery();

            //}
            //else if(id.ToLower().Contains("no")) // if (rdReDir.SelectedValue == "2")
            //{
            //    st = "مغلقة";
            //    cmd.Parameters.AddWithValue("@toid", lblJobId.Text);
            //    cmd.Parameters.AddWithValue("@toname", lblRName.Text);
            //    SqlCommand cmdUp = new SqlCommand("Update ResearchFeeInfo Set RequestType='S',RequestFinalStatus=N'" + txtDirNotes.Text + "' where AutoId=" + Session["ViewRequestFrom"], conn);
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
            //if (txtReDeanValue.Text != "")
            //{
                if (rdReDeanDecision1.Checked)
                {
                    id = "agree";
                    ReDeanDecision = "التوصية بالموافقة على دعم رسوم نشر بقيمة " + txtReDeanValue.Text + " " + (ddlCurValue.SelectedValue != "4" ? ddlCurValue.SelectedItem.Text : txtCurOther.Text) + " كون المجلة مفهرسة بقاعدة بيانات SCOPUS في العام " + txtReDeanYear.Text + " مع الشكر ";
                }
                else if (rdReDeanDecision2.Checked)
                {
                    id = "notagree";
                    ReDeanDecision = "التوصية بعدم الموافقة على دعم رسوم نشر كون المجلة غير مفهرسة بقاعدة بيانات SCOPUS";
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
                    cmd = new SqlCommand("insert into RequestsFollowUp values(@reqid,@fromid,@fromname,@toid,@toname,@reqdate,@reqst,@actualid,@notes,@dec,0)", conn);
                    cmd.Parameters.AddWithValue("@reqid", Session["ViewRequestFrom"]);
                    cmd.Parameters.AddWithValue("@fromid", 14);
                    cmd.Parameters.AddWithValue("@fromname", "عميد الدراسات العليا والبحث العلمي");

                    if (id == "agree" || id == "notagree") // if (rdReDean.SelectedValue == "1")
                    {
                        cmd.Parameters.AddWithValue("@dec", id == "agree" ? "1" : "2");
                        cmd.Parameters.AddWithValue("@toid", 13);
                        cmd.Parameters.AddWithValue("@toname", "نائب رئيس الجامعة");
                        cmd.Parameters.AddWithValue("@reqst", "مكتمل");
                        SqlCommand cmdUp = new SqlCommand("Update ResearchFeeInfo Set RequestFinalStatus=N'مكتمل',GSOutNo=N'ع د ع / د / 8 /'  where AutoId=" + Session["ViewRequestFrom"], conn);
                        cmdUp.ExecuteNonQuery();

                        SqlCommand cmdUpdate = new SqlCommand("Update ReDirectorInfo Set FeeValue=N'" + txtReDeanValue.Text + " " + (ddlCurValue.SelectedValue != "4" ? ddlCurValue.SelectedItem.Text : txtCurOther.Text) + "' where Type=0 and RequestId=" + Session["ViewRequestFrom"], conn);
                        cmdUpdate.ExecuteNonQuery();

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
                        //SqlCommand cmdUp = new SqlCommand("Update ResearchFeeInfo Set RequestType='S',RequestFinalStatus=N'" + txtDirNotes.Text + "' where AutoId=" + Session["ViewRequestFrom"], conn);
                        //cmdUp.ExecuteNonQuery();
                    }
                    cmd.Parameters.AddWithValue("@reqdate", DateTime.Now.Date);
                    cmd.Parameters.AddWithValue("@actualid", Session["uid"]);
                    //cmd.Parameters.AddWithValue("@notes", txtReDean.Text);
                    cmd.Parameters.AddWithValue("@notes", ReDeanDecision);

                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("update RequestsFollowUp Set ReqStatus=N'انجزت' where type=0 and AutoId=" + Session["AutoIdUpdated"], conn);
                    cmd.ExecuteNonQuery();

                    SqlCommand cmd11 = new SqlCommand("Select * From Priviliges where PrivType=2 and PrivFacultyId=11", conn);
                    SqlDataReader drSecGS = cmd11.ExecuteReader();
                    drSecGS.Read();

                    System.Text.StringBuilder msg = new System.Text.StringBuilder();
                    msg.Clear();
                    //msg.Append("<body dir='rtl'><b>المساعد الاداري لعمادة الدراسات العليا والبحث العلمي المحترم،</b>");
                    //if (id == "agree" || id == "notagree")
                    //    msg.Append("<br>الرجاء كتابة رقم الصادر  لطلب دعم رسوم نشر بحث علمي للباحث " + lblRName.Text + " رقم " + Session["ViewRequestFrom"]);
                    ////else
                    ////    msg.Append("<br>أرجو مراجعة  " + txtReDean.Text);
                    //msg.Append(" <a href='http://meusr-ra.meu.edu.jo/' target='_blank'>بالدخول إلى موقع البحث العلمي </a> ");
                    //msg.Append("<br><br><b>عميد الدراسات العليا والبحث العلمي</b>");
                    //sendEmail("Sec-Graduate@meu.edu.jo", msg.ToString());

                    msg.Append("<body dir='rtl'><b>المساعد الاداري لعمادة الدراسات العليا والبحث العلمي المحترم،</b>");
                    if (id == "agree" || id == "notagree")
                        msg.Append("<br>الرجاء طباعة  طلب دعم رسوم نشر بحث علمي للباحث " + lblRName.Text + " رقم " + Session["ViewRequestFrom"] + "مع تجميع كامل الملفات");
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
            //}
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
                //cmd = new SqlCommand("Select * from ResearchFeeInfo where AutoId=" + Session["RequestComeFrom"], conn);
                //SqlDataReader drCheck = cmd.ExecuteReader();
                //drCheck.Read();


                if (Session["RequestComeFrom"] == null)
                {
                    sql = "Insert into ResearchFeeInfo values(@rid,@hcst,@orcid,@gslink,@rglink,@rtitle,@d,@magname,@magissn,@pub,@year,@fee,@cur,'S',@ReqDate,'',N'قيد الدراسة','','',@DBIndex,@researcherid,0)";
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
                    cmd.Parameters.AddWithValue("@fee", txtFeeValue.Text);
                    cmd.Parameters.AddWithValue("@cur", ddlCurrency.SelectedValue);
                    cmd.Parameters.AddWithValue("@d", txtAcceptDate.Text);
                    cmd.Parameters.AddWithValue("@ReqDate", DateTime.Now.Date);
                    cmd.Parameters.AddWithValue("@DBIndex", txtDBIndex.Text);
                    cmd.Parameters.AddWithValue("@researcherid", txtResearcherId.Text);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("select max(autoid) from ResearchFeeInfo where jobid=" + Session["uid"], conn);
                    dtt.Load(cmd.ExecuteReader());
                    Session["RequestComeFrom"] = dtt.Rows[0][0];
                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        cmd = new SqlCommand("Insert into RePartInfo values(" + dtt.Rows[0][0] + ",N'" + GridView1.Rows[i].Cells[1].Text + "')", conn);
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    sql = "Update ResearchFeeInfo Set ";
                    sql += "hcstID=@hcst,orcidId=@orcid,GSLink=@gslink,RGLink=@rglink,";
                    sql += "ReTitle=@rtitle,AcceptDate=@d,MagName=@magname,MagISSN=@magissn,";
                    sql += "PublisherName=@pub,MagYear=@year,FeeValue=@fee,FeeCurrency=@cur,";
                    sql += "RequestType='S',RequestFinalStatus=N'قيد الدراسة',@DBIndex=DBIndex";
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
                    cmd.Parameters.AddWithValue("@fee", txtFeeValue.Text);
                    cmd.Parameters.AddWithValue("@cur", ddlCurrency.SelectedValue);
                    cmd.Parameters.AddWithValue("@d", txtAcceptDate.Text);
                    cmd.Parameters.AddWithValue("@DBIndex", txtDBIndex.Text);
                    cmd.Parameters.AddWithValue("@researcherid", txtResearcherId.Text);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from RePartInfo where RequestId=" + Session["RequestComeFrom"], conn);
                    cmd.ExecuteNonQuery();

                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        cmd = new SqlCommand("Insert into RePartInfo values(" + Session["RequestComeFrom"] + ",N'" + GridView1.Rows[i].Cells[1].Text + "')", conn);
                        cmd.ExecuteNonQuery();
                    }
                }

                SqlCommand cmdEmail = new SqlCommand("Update Users Set Email='" + txtEmail.Text + "' where AcdId=" + lblJobId.Text, conn);
                cmdEmail.ExecuteNonQuery();
                Session["UserEmail"] = txtEmail.Text;


                cmd = new SqlCommand("Update RequestsFollowUp Set ReqStatus=N'انجزت' where type=0 and AutoId=(select max(Autoid) from RequestsFollowUp where type=0 and RequestId=" + Session["RequestComeFrom"] + ")", conn);
                cmd.ExecuteNonQuery();

                //sql = @"SELECT p.AutoId, p.PrivTo,Email
                //            FROM ResearcherInfo r, faculty f, department d, priviliges p
                //            where r.College=f.CollegeName and r.Dept=d.DeptName and f.AutoId=p.PrivFacultyId and d.AutoId=p.PrivDeptId and r.AcdId=p.PrivTo
                //            and f.CollegeName=N'" + lblFaculty.Text + "' and d.DeptName=N'" + lblDept.Text + "'";

                sql = @"SELECT p.AutoId, p.PrivTo,Email
                        FROM priviliges p
                        where PrivFacultyId=(select AutoId from faculty where CollegeName=N'" + lblFaculty.Text + @"')
                        and PrivDeptId=(select AutoId from Department where DeptName=N'" + lblDept.Text + @"')";

                int EmailFlag = 0;
                cmd = new SqlCommand(sql, conn);
                DataTable dtPrivDept = new DataTable();
                dtPrivDept.Load(cmd.ExecuteReader());

                cmd = new SqlCommand("insert into RequestsFollowUp values(@reqid,@fromid,@fromname,@toid,@toname,@reqdate,@reqst,@actualid,N'',N'',0)", conn);
                cmd.Parameters.AddWithValue("@reqid", Session["RequestComeFrom"]);
                cmd.Parameters.AddWithValue("@fromid", Session["uid"]);
                cmd.Parameters.AddWithValue("@fromname", Session["userName"]);

                SqlCommand cmdLastDec = new SqlCommand("Select * From RequestsFollowUp where type=0 and AutoId=(select max(Autoid) from RequestsFollowUp where type=0 and RequestId=" + Session["RequestComeFrom"] + ")", conn);
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
                //upload();

                //msgDiv.Visible = true;
                //lblMsg.Text = "تم ارسال الطلب بنجاح";
                Session["RequestComeFrom"] = null;
                Session["ViewRequestFrom"] = null;

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
                //Response.Redirect("Requests.aspx");
            }
            else if (Session["ReDirectorFinal"].ToString() == "ok")
            {
                string id = Session["btnName"].ToString();
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand cmd = new SqlCommand();

                //get dean priv id
                string sql = @"select * from RequestsFollowUp 
                            where type=0 and autoid=(select max(autoid) from RequestsFollowUp where type=0 and requestid=" + Session["ViewRequestFrom"] + ")";

                cmd = new SqlCommand("insert into RequestsFollowUp values(@reqid,@fromid,@fromname,@toid,@toname,@reqdate,@reqst,@actualid,@notes,@dec,0)", conn);
                cmd.Parameters.AddWithValue("@reqid", Session["ViewRequestFrom"]);
                cmd.Parameters.AddWithValue("@fromid", 11);
                cmd.Parameters.AddWithValue("@fromname", "رئيس قسم البحث العلمي");
                cmd.Parameters.AddWithValue("@dec", id.ToLower().Contains("ok") ? "1" : id.ToLower().Contains("post") ? "3" : "2");
                string st = "قيد التنفيذ";
                if (id.ToLower().Contains("ok")) // if (rdReDir.SelectedValue == "1")
                {
                    cmd.Parameters.AddWithValue("@toid", 14);
                    cmd.Parameters.AddWithValue("@toname", "عميد الدراسات العليا والبحث العلمي");

                    SqlCommand cmd1 = new SqlCommand("Delete From ReDirectorInfo where Type=0 and RequestId=" + Session["ViewRequestFrom"], conn);
                    cmd1.ExecuteNonQuery();

                    string sqlDir = "insert into ReDirectorInfo values(" + Session["ViewRequestFrom"] + ",N'" + txtyearmonth.Text + "',N'" + txtFeeV.Text + "',"
                        + rdQuarter.SelectedValue + "," + chkFiles.Items[0].Value + "," + chkFiles.Items[1].Value + "," + chkFiles.Items[2].Value
                        + "," + chkFiles.Items[3].Value + "," + chkFiles.Items[4].Value + "," + chkFiles.Items[5].Value + "," + chkFiles.Items[6].Value + "," +
                        rdCheck.SelectedValue + "," + (chkClarivate.Items[0].Selected ? 1 : 0) + "," + (chkClarivate.Items[1].Selected ? 1 : 0) + "," + (chkClarivate.Items[2].Selected ? 1 : 0) +
                        ",N'" + txtReDirector.Text + "',@d,0)";
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
                    SqlCommand cmdd = new SqlCommand("Select ReqFromId,ReqFromName From RequestsFollowUp where type=0 and Autoid=(Select Min(AutoId) From RequestsFollowUp where type=0 and RequestId=" + Session["ViewRequestFrom"] + ")", conn);
                    SqlDataReader drDNotes = cmdd.ExecuteReader();
                    drDNotes.Read();

                    cmd.Parameters.AddWithValue("@toid", drDNotes[0]);
                    cmd.Parameters.AddWithValue("@toname", drDNotes[1]);

                    SqlCommand cmdUp = new SqlCommand("Update ResearchFeeInfo Set RequestType='NS',RequestFinalStatus=N'تعليق - " + txtReDirector.Text + "' where AutoId=" + Session["ViewRequestFrom"], conn);
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
                    SqlCommand cmdUp = new SqlCommand("Update ResearchFeeInfo Set RequestType='S',RequestFinalStatus=N'مغلقة' where AutoId=" + Session["ViewRequestFrom"], conn);
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


                cmd = new SqlCommand("update RequestsFollowUp Set ReqStatus=N'انجزت' where type=0 and AutoId=" + Session["AutoIdUpdated"], conn);
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
            RequiredFieldValidator10.Visible = true;
            RequiredFieldValidator14.Visible = true;
            RequiredFieldValidator21.Visible = false;
        }

        protected void rdReDeanDecision2_CheckedChanged(object sender, EventArgs e)
        {
            RequiredFieldValidator10.Visible = false;
            RequiredFieldValidator14.Visible = false;
            RequiredFieldValidator21.Visible = false;
        }

        protected void rdReDeanDecision3_CheckedChanged(object sender, EventArgs e)
        {
            RequiredFieldValidator10.Visible = false;
            RequiredFieldValidator14.Visible = false;
            RequiredFieldValidator21.Visible = true;
        }

        protected void ddlCurValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCurValue.SelectedValue == "4")
                txtCurOther.Visible = true;
            else
                txtCurOther.Visible = false;
        }

        protected void rdCheck_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdCheck.SelectedIndex != -1)
                rdQuarter.Visible = true;
            else
                rdQuarter.Visible = false;
        }
    }
}