﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchAcademicUnit
{
    public partial class ShowImageFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            img4.ImageUrl = Session["imagefile"].ToString();
        }
    }
}