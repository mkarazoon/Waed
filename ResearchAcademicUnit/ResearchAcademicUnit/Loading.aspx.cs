using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchAcademicUnit
{
    public partial class Loading : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                savefiles();
                sendEmail(Session["Em"].ToString(), Session["msg"].ToString());
                sendEmail(Session["Em1"].ToString(), Session["msg1"].ToString());
                
                //msgDiv.Visible = true;
                Session["uploadedSucc"] = true;
                //Timer1.Enabled = false;
                Response.Redirect("ResearchFeeForm.aspx");
            }
        }

        protected void savefiles()
        {
//FileUpload flu = new FileUpload();
//            //flu.FileName=
//                //Session["file1"] = fileLocation;
//            if (File.Exists(Session["file1"].ToString()))
//            {
//                File.Delete(Session["file1"].ToString());
//            }
            string f1 = Session["file1"].ToString();
            //System.IO.File.Create(f1);
            //flu.SaveAs(f1);

            HttpFileCollection UploadedFiles = Request.Files;
            HttpPostedFile UserPostedFile;
            int UploadFileCount = UploadedFiles.Count;
            if (UploadFileCount >= 1)
            {
                for (int i = 0; i < UploadFileCount; ++i)
                {
                    UserPostedFile = UploadedFiles[i];
                    UserPostedFile.SaveAs(UserPostedFile.FileName);
                }
            }


            foreach (HttpPostedFile file in Request.Files)
                file.SaveAs(f1);

            

            //Session["file2"] = fileLocation;
            //if (File.Exists(Session["file2"].ToString()))
            //{
            //    File.Delete(Session["file2"].ToString());
            //}
            //flu.SaveAs(Session["file2"].ToString());

            ////Session["file3"] = fileLocation;
            //if (File.Exists(Session["file3"].ToString()))
            //{
            //    File.Delete(Session["file3"].ToString());
            //}
            //flu.SaveAs(Session["file3"].ToString());

            ////Session["file4"] = fileLocation;
            //if (File.Exists(Session["file4"].ToString()))
            //{
            //    File.Delete(Session["file4"].ToString());
            //}
            //flu.SaveAs(Session["file4"].ToString());

        }
        protected void sendEmail(string email, string msg)
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
                IsBodyHtml = true,
                Subject = "طلب دعم رسوم نشر بحث علمي",
                Body = msg

            })
            {
                //message.Attachments.Add(new Attachment(""));
                smtp.Send(message);
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            msgDiv.Visible = false;
            Timer1.Enabled = false;
            Response.Redirect("Requests.aspx");
        }
    }
}