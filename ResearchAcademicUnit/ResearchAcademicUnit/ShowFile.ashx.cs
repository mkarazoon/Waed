using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.IO;

namespace ResearchAcademicUnit
{
    /// <summary>
    /// Summary description for ShowFile
    /// </summary>
    public class ShowFile : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            
            context.Response.Clear();
            context.Response.ContentType = "application/pdf";
            string filePath = System.Web.HttpContext.Current.Server.MapPath("");
            context.Response.TransmitFile(filePath);

            // Show the passed data from the code behind. It might be handy in the future to pass some parameters and not expose then on url, for database updating, etc.
            context.Response.Write(context.Session["myData"].ToString());
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}