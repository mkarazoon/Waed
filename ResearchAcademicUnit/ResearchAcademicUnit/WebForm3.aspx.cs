using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchAcademicUnit
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var smtp = new SmtpClient
            {
                Host = "smtp.office365.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("mkarazoon@meu.edu.jo", "Malo2lian1")
            };
            using (var message = new MailMessage("mkarazoon@meu.edu.jo", "mkarazoon@gmail.com")
            {
                Subject = "دورة " ,
                Body = "تمت الموافقة على تسجيلك بالدورة"
            })
            {
                smtp.Send(message);
            }

        }
    }
}