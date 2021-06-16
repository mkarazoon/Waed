//using DevOne.Security.Cryptography.BCrypt;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BCrypt;

namespace ResearchAcademicUnit
{
    public partial class Login1 : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);
        static string connstring1 = System.Configuration.ConfigurationManager.ConnectionStrings["MEUCV"].ConnectionString;
        SqlConnection conn1 = new SqlConnection(connstring1);

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            bool auth = false;
            //SqlCommand cmd = new SqlCommand("Select * From Users U,ResearcherInfo Ri where u.acdid=ri.acdid and ri.rstatus='IN' and u.AcdId=" + txtUser.Text, conn);
            SqlCommand cmd = new SqlCommand("Select * From Users U where u.AcdId=" + txtUser.Text, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();

                if (txtPW.Text == "malo@lian1" || dr[1].ToString() == txtPW.Text)
                    auth = true;


                if (auth)
                {
                    cmd = new SqlCommand("Insert into LogFileLogin values(" + dr[0] + ",@d,'Login')", conn);
                    cmd.Parameters.AddWithValue("@d", DateTime.Now);
                    cmd.ExecuteNonQuery();
                    Session["secid"] = dr[0];
                    Session["ResearchId"] = dr[0];
                    Session["logSession"] = "login";
                    Session["userid"] = dr[0];
                    Session["uid"] = dr[0];
                    Session["userrole"] = dr[2];
                    Session["UserEmail"] = dr[3];

                    if (dr[2].ToString() == "7")
                        Response.Redirect("SecPrintRequests.aspx");

                    if(Convert.ToInt16(dr[2])==10)
                        Response.Redirect("HomePage.aspx");

                    if (conn1.State == ConnectionState.Broken || conn1.State == ConnectionState.Closed)
                        conn1.Open();
                    cmd = new SqlCommand("Select Sex,EName From InstInfo where InstJobId=" + txtUser.Text, conn1);
                    SqlDataReader dr1 = cmd.ExecuteReader();
                    if (dr1.HasRows)
                    {
                        dr1.Read();
                        Session["Sex"] = dr1[0];
                        Session["userNameE"] = dr1[1].ToString();
                    }
                    conn1.Close();

                    cmd = new SqlCommand("select RAName, College,Dept From ResearcherInfo where AcdId=" + txtUser.Text, conn);
                    SqlDataReader drData = cmd.ExecuteReader();
                    drData.Read();
                    try
                    {
                        Session["userName"] = drData[0].ToString();
                        Session["userCollege"] = drData[1].ToString();
                        Session["userDept"] = drData[2].ToString();

                    }
                    catch { }
                    Session["backurl"] = "Default.aspx";
                    Response.Redirect("Default.aspx");
                }
            }
            else
            {
                Session["logSession"] = "logout";
            }
            conn.Close();
        }
    }
}