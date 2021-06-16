using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ResearchAcademicUnit
{
    public partial class UploadFiles : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["MEUCV"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uid"] == null)
                Response.Redirect("Login.aspx");
            Session["backurl"] = "Agreement.aspx";
            ContentPlaceHolder cp = this.Master.Master.FindControl("ContentPlaceHolder1") as ContentPlaceHolder;
            HtmlGenericControl list = (HtmlGenericControl)cp.FindControl("menu13");//.FindControl("menu1");
            list.Attributes.Add("class", "ca-menu activeLi");

            //if (!this.IsPostBack)
            //{
            try
            {
                string[] foldersName = { "/الابحاث المنشورة", "/المؤتمرات", "/الكتب", "/الورش-الندوات-الدورات", "/شهادات التميز" };
                for (int folderIndex = 0; folderIndex <= 4; folderIndex++)
                {
                    DataTable FilesTable = new DataTable();
                    FilesTable.Columns.Add("FileName");
                    FilesTable.Columns.Add("FilePath");
                    FilesTable.Columns.Add("FolderName");
                    DataRow dRow;
                    //string yourPath = @"your path here";
                    string searchPattern = "*.pdf";
                    try
                    {
                        var resultData = Directory.GetFiles(Server.MapPath("document/" + Session["uid"] + foldersName[folderIndex]), searchPattern, SearchOption.AllDirectories)
                                        .Select(x => new { FileName = Path.GetFileName(x), FilePath = x, DirName = Path.GetDirectoryName(x) });

                        foreach (var item in resultData)
                        {
                            dRow = FilesTable.NewRow();
                            dRow["FileName"] = item.FileName;
                            dRow["FilePath"] = item.FilePath;
                            string[] Folder = item.DirName.Split('\\');
                            string Ftype = "";
                            for (int i = 0; i < Folder.Length; i++)
                            {
                                if (Folder[i] == Session["uid"].ToString())
                                {
                                    for (int j = i + 1; j < Folder.Length; j++)
                                        Ftype += Folder[j] + " - ";
                                    break;
                                }
                            }
                            dRow["FolderName"] = Ftype.Substring(0, Ftype.Length - 3);
                            FilesTable.Rows.Add(dRow);
                        }
                    }
                    catch { }
                    switch (folderIndex)
                    {
                        case 0:
                            GridView1.DataSource = FilesTable;
                            GridView1.DataBind();
                            break;
                        case 1:
                            GridView2.DataSource = FilesTable;
                            GridView2.DataBind();
                            break;
                        case 2:
                            GridView3.DataSource = FilesTable;
                            GridView3.DataBind();
                            break;
                        case 3:
                            GridView4.DataSource = FilesTable;
                            GridView4.DataBind();
                            break;
                        case 4:
                            GridView5.DataSource = FilesTable;
                            GridView5.DataBind();
                            break;
                    }
                }
                //DirectoryInfo rootInfo = new DirectoryInfo(Server.MapPath("document/" + Session["uid"]));
                //this.PopulateTreeView(rootInfo, null);
            }
            catch { }
            //}
        }

        //private void PopulateTreeView(DirectoryInfo dirInfo, TreeNode treeNode)
        //{
        //    foreach (DirectoryInfo directory in dirInfo.GetDirectories())
        //    {
        //        TreeNode directoryNode = new TreeNode
        //        {
        //            Text = directory.Name,
        //            Value = directory.FullName,
        //            SelectAction = TreeNodeSelectAction.None
        //        };

        //        if (treeNode == null)
        //        {
        //            //If Root Node, add to TreeView.
        //            TreeView1.Nodes.Add(directoryNode);
        //        }
        //        else
        //        {
        //            //If Child Node, add to Parent Node.
        //            treeNode.ChildNodes.Add(directoryNode);
        //        }

        //        //Get all files in the Directory.
        //        foreach (FileInfo file in directory.GetFiles())
        //        {
        //            //Add each file as Child Node.
        //            TreeNode fileNode = new TreeNode
        //            {
        //                Text = file.Name,
        //                Value = file.FullName,
        //                Target = "_blank",
        //                NavigateUrl = (new Uri(file.FullName)).ToString()
                        
                        
        //            };
        //            directoryNode.ChildNodes.Add(fileNode);
        //        }

        //        PopulateTreeView(directory, directoryNode);
        //    }
        //}

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                string subPath = "document/" + Session["uid"];

                bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));
                bool dexists = false;
                string subdir = subPath;
                if (!exists)
                    System.IO.Directory.CreateDirectory(Server.MapPath(subPath));

                string fullname = "";
                //upload scopus researches
                if (fluRScopus.HasFile)
                {
                    subdir = subPath + "/الابحاث المنشورة";
                    dexists = System.IO.Directory.Exists(Server.MapPath(subdir));
                    if (!dexists)
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(subdir));
                    }
                    string fileName = Path.GetFileName(fluRScopus.PostedFile.FileName);
                    string fileExtension = Path.GetExtension(fluRScopus.PostedFile.FileName);

                    int count = Directory.GetFiles(Server.MapPath(subdir), "*", SearchOption.AllDirectories).Length + 1;
                    fullname = "research_" + (count) + fileExtension;
                    string fileLocation = Server.MapPath(subdir + "/" + fullname);
                    //if (File.Exists(fileLocation))
                    //{
                    //    File.Delete(fileLocation);
                    //}
                    fluRScopus.SaveAs(fileLocation);
                }

                //upload other researches
                //if (fluROther.HasFile)
                //{
                //    subdir = subPath + "/الابحاث المنشورة/اخرى";
                //    dexists = System.IO.Directory.Exists(Server.MapPath(subdir));
                //    if (!dexists)
                //    {
                //        System.IO.Directory.CreateDirectory(Server.MapPath(subdir));
                //    }
                //    string fileName = Path.GetFileName(fluROther.PostedFile.FileName);
                //    string fileExtension = Path.GetExtension(fluROther.PostedFile.FileName);
                //    fullname = "other_" + Session["uid"] + fileExtension;
                //    string fileLocation = Server.MapPath(subdir + "/" + fullname);
                //    if (File.Exists(fileLocation))
                //    {
                //        File.Delete(fileLocation);
                //    }
                //    fluROther.SaveAs(fileLocation);
                //}

                //upload conference
                if (fluCINU.HasFile)
                {
                    subdir = subPath + "/المؤتمرات";
                    dexists = System.IO.Directory.Exists(Server.MapPath(subdir));
                    if (!dexists)
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(subdir));
                    }
                    string fileName = Path.GetFileName(fluCINU.PostedFile.FileName);
                    string fileExtension = Path.GetExtension(fluCINU.PostedFile.FileName);
                int count = Directory.GetFiles(Server.MapPath(subdir), "*", SearchOption.AllDirectories).Length + 1;
                fullname = "conference_" + count + fileExtension;

                    //fullname = "in_university_" + Session["uid"] + fileExtension;
                    string fileLocation = Server.MapPath(subdir + "/" + fullname);
                    //if (File.Exists(fileLocation))
                    //{
                    //    File.Delete(fileLocation);
                    //}
                    fluCINU.SaveAs(fileLocation);
                }

                //if (fluCINJo.HasFile)
                //{
                //    subdir = subPath + "/المؤتمرات/داخل الاردن";
                //    dexists = System.IO.Directory.Exists(Server.MapPath(subdir));
                //    if (!dexists)
                //    {
                //        System.IO.Directory.CreateDirectory(Server.MapPath(subdir));
                //    }
                //    string fileName = Path.GetFileName(fluCINJo.PostedFile.FileName);
                //    string fileExtension = Path.GetExtension(fluCINJo.PostedFile.FileName);
                //    fullname = "in_jordan_" + Session["uid"] + fileExtension;
                //    string fileLocation = Server.MapPath(subdir + "/" + fullname);
                //    if (File.Exists(fileLocation))
                //    {
                //        File.Delete(fileLocation);
                //    }
                //    fluCINJo.SaveAs(fileLocation);
                //}

                //if (fluCOutJo.HasFile)
                //{
                //    subdir = subPath + "/المؤتمرات/اقليمي - الدول العربية";
                //    dexists = System.IO.Directory.Exists(Server.MapPath(subdir));
                //    if (!dexists)
                //    {
                //        System.IO.Directory.CreateDirectory(Server.MapPath(subdir));
                //    }
                //    string fileName = Path.GetFileName(fluCOutJo.PostedFile.FileName);
                //    string fileExtension = Path.GetExtension(fluCOutJo.PostedFile.FileName);
                //    fullname = "out_jordan_" + Session["uid"] + fileExtension;
                //    string fileLocation = Server.MapPath(subdir + "/" + fullname);
                //    if (File.Exists(fileLocation))
                //    {
                //        File.Delete(fileLocation);
                //    }
                //    fluCOutJo.SaveAs(fileLocation);
                //}

                //if (fluCNational.HasFile)
                //{
                //    subdir = subPath + "/المؤتمرات/دولي";
                //    dexists = System.IO.Directory.Exists(Server.MapPath(subdir));
                //    if (!dexists)
                //    {
                //        System.IO.Directory.CreateDirectory(Server.MapPath(subdir));
                //    }
                //    string fileName = Path.GetFileName(fluCNational.PostedFile.FileName);
                //    string fileExtension = Path.GetExtension(fluCNational.PostedFile.FileName);
                //    fullname = "international_" + Session["uid"] + fileExtension;
                //    string fileLocation = Server.MapPath(subdir + "/" + fullname);
                //    if (File.Exists(fileLocation))
                //    {
                //        File.Delete(fileLocation);
                //    }
                //    fluCNational.SaveAs(fileLocation);
                //}

                //upload books
                if (fluBNational.HasFile)
                {
                    subdir = subPath + "/الكتب";
                    dexists = System.IO.Directory.Exists(Server.MapPath(subdir));
                    if (!dexists)
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(subdir));
                    }
                    string fileName = Path.GetFileName(fluBNational.PostedFile.FileName);
                    string fileExtension = Path.GetExtension(fluBNational.PostedFile.FileName);
                int count = Directory.GetFiles(Server.MapPath(subdir), "*", SearchOption.AllDirectories).Length + 1;
                fullname = "Book_" +count + fileExtension;
                    string fileLocation = Server.MapPath(subdir + "/" + fullname);
                    //if (File.Exists(fileLocation))
                    //{
                    //    File.Delete(fileLocation);
                    //}
                    fluBNational.SaveAs(fileLocation);
                }

                //if (fluBLocal.HasFile)
                //{
                //    subdir = subPath + "/الكتب/نشر محلي";
                //    dexists = System.IO.Directory.Exists(Server.MapPath(subdir));
                //    if (!dexists)
                //    {
                //        System.IO.Directory.CreateDirectory(Server.MapPath(subdir));
                //    }
                //    string fileName = Path.GetFileName(fluBLocal.PostedFile.FileName);
                //    string fileExtension = Path.GetExtension(fluBLocal.PostedFile.FileName);
                //    fullname = "local_" + Session["uid"] + fileExtension;
                //    string fileLocation = Server.MapPath(subdir + "/" + fullname);
                //    if (File.Exists(fileLocation))
                //    {
                //        File.Delete(fileLocation);
                //    }
                //    fluBLocal.SaveAs(fileLocation);
                //}

                //if (fluBIn.HasFile)
                //{
                //    subdir = subPath + "/الكتب/نشر وطني";
                //    dexists = System.IO.Directory.Exists(Server.MapPath(subdir));
                //    if (!dexists)
                //    {
                //        System.IO.Directory.CreateDirectory(Server.MapPath(subdir));
                //    }
                //    string fileName = Path.GetFileName(fluBIn.PostedFile.FileName);
                //    string fileExtension = Path.GetExtension(fluBIn.PostedFile.FileName);
                //    fullname = "national_" + Session["uid"] + fileExtension;
                //    string fileLocation = Server.MapPath(subdir + "/" + fullname);
                //    if (File.Exists(fileLocation))
                //    {
                //        File.Delete(fileLocation);
                //    }
                //    fluBIn.SaveAs(fileLocation);
                //}

                //upload workshops - seminar - training
                if (fluWorkShop.HasFile)
                {
                    subdir = subPath + "/الورش-الندوات-الدورات";
                    dexists = System.IO.Directory.Exists(Server.MapPath(subdir));
                    if (!dexists)
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(subdir));
                    }
                    string fileName = Path.GetFileName(fluWorkShop.PostedFile.FileName);
                    string fileExtension = Path.GetExtension(fluWorkShop.PostedFile.FileName);
                int count = Directory.GetFiles(Server.MapPath(subdir), "*", SearchOption.AllDirectories).Length + 1;
                fullname = "File_" + count + fileExtension;
                    string fileLocation = Server.MapPath(subdir + "/" + fullname);
                    //if (File.Exists(fileLocation))
                    //{
                    //    File.Delete(fileLocation);
                    //}
                    fluWorkShop.SaveAs(fileLocation);
                }

                //if (fluSeminar.HasFile)
                //{
                //    subdir = subPath + "/الندوات";
                //    dexists = System.IO.Directory.Exists(Server.MapPath(subdir));
                //    if (!dexists)
                //    {
                //        System.IO.Directory.CreateDirectory(Server.MapPath(subdir));
                //    }
                //    string fileName = Path.GetFileName(fluSeminar.PostedFile.FileName);
                //    string fileExtension = Path.GetExtension(fluSeminar.PostedFile.FileName);
                //    fullname = "seminar_" + Session["uid"] + fileExtension;
                //    string fileLocation = Server.MapPath(subdir + "/" + fullname);
                //    if (File.Exists(fileLocation))
                //    {
                //        File.Delete(fileLocation);
                //    }
                //    fluSeminar.SaveAs(fileLocation);
                //}

                //if (fluTrain.HasFile)
                //{
                //    subdir = subPath + "/الدورات";
                //    dexists = System.IO.Directory.Exists(Server.MapPath(subdir));
                //    if (!dexists)
                //    {
                //        System.IO.Directory.CreateDirectory(Server.MapPath(subdir));
                //    }
                //    string fileName = Path.GetFileName(fluTrain.PostedFile.FileName);
                //    string fileExtension = Path.GetExtension(fluTrain.PostedFile.FileName);
                //    fullname = "training_" + Session["uid"] + fileExtension;
                //    string fileLocation = Server.MapPath(subdir + "/" + fullname);
                //    if (File.Exists(fileLocation))
                //    {
                //        File.Delete(fileLocation);
                //    }
                //    fluTrain.SaveAs(fileLocation);
                //}

                //upload certificate
                if (fluCertificate.HasFile)
                {
                    subdir = subPath + "/شهادات التميز";
                    dexists = System.IO.Directory.Exists(Server.MapPath(subdir));
                    if (!dexists)
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(subdir));
                    }
                    string fileName = Path.GetFileName(fluCertificate.PostedFile.FileName);
                    string fileExtension = Path.GetExtension(fluCertificate.PostedFile.FileName);
                int count = Directory.GetFiles(Server.MapPath(subdir), "*", SearchOption.AllDirectories).Length + 1;
                fullname = "certificate_" + count + fileExtension;
                    string fileLocation = Server.MapPath(subdir + "/" + fullname);
                    //if (File.Exists(fileLocation))
                    //{
                    //    File.Delete(fileLocation);
                    //}
                    fluCertificate.SaveAs(fileLocation);
                }
                lblMsg.Visible = true;
                lblMsg.Text = "تم تحميل الملفات بنجاح";
                Timer1.Enabled = true;
                msgDiv.Visible = true;
            }
            catch
            {
                lblMsg.Visible = true;
                lblMsg.Text = "حصل خطأ اثناء تحميل الملفات";
                Timer1.Enabled = true;
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            lblMsg.Visible = false;
            Timer1.Enabled = false;
            msgDiv.Visible = false;
            Response.Redirect("UploadFiles.aspx");
        }


        //protected void Button6_Click(object sender, EventArgs e)
        //{
        //    //string dir = @"C:\Test\";
        //    try
        //    {

            
        //    string path = TreeView1.SelectedNode.Value;

        //        WebClient User = new WebClient();
        //        Byte[] FileBuffer = User.DownloadData(path);
        //        if (FileBuffer != null)
        //        {
        //            Response.ContentType = "application/pdf";
        //            Response.AddHeader("content-length", FileBuffer.Length.ToString());
        //            Response.BinaryWrite(FileBuffer);
        //        }
        //    }
        //    catch { }
        //}

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            Response.Redirect("RCertificate.aspx");
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            Response.Redirect("Resume.aspx");
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            string path = row.Cells[1].Text;

            if (File.Exists(path))
            {
                File.Delete(path);
                Response.Redirect("UploadFiles.aspx");
            }

        }

        protected void lnkView_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
                string path = row.Cells[1].Text;


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
            catch { }
        }
    }
}