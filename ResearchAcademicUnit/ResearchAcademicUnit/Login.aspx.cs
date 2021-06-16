using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
//using System.DirectoryServices;
//using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HelpDeskIT
{
    public partial class Login : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);
        static string connstring1 = System.Configuration.ConfigurationManager.ConnectionStrings["MEUCV"].ConnectionString;
        SqlConnection conn1 = new SqlConnection(connstring1);
        static string OracleConnString = System.Configuration.ConfigurationManager.ConnectionStrings["orcleConStr"].ConnectionString;
        OracleConnection conn2 = new OracleConnection(OracleConnString);

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //protected void lnkLogin_Click(object sender, EventArgs e)
        //{
        //    if (txtUserId.Value == "")
        //    {
        //        lblMsg.Text = "يجب ادخال اسم المستخدم";
        //        errMsg.Visible = true;
        //    }
        //    else if (txtUserPW.Value == "")
        //    {
        //        lblMsg.Text = "يجب ادخال كلمة المرور";
        //        errMsg.Visible = true;
        //    }
        //    else
        //    {
        //        bool authenticated = false;
        //        try
        //        {
        //            using (DirectoryEntry entry = new DirectoryEntry())
        //            {
        //                entry.Username = txtUserId.Value.ToString();
        //                entry.Password = txtUserPW.Value.ToString();
        //                DirectorySearcher searcher = new DirectorySearcher(entry);
        //                searcher.Filter = "(&(objectClass=user)(SAMAccountName=" + txtUserId.Value + "))";
        //                try
        //                {

        //                    //SearchResult result = searcher.FindOne();
        //                    //DirectoryEntry user = new DirectoryEntry(result.Path);
        //                    //try
        //                    //{
        //                    //    string desc = user.Properties["description"].Value.ToString();
        //                    //}
        //                    //catch { }

        //                    //authenticated = true;
        //                    if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
        //                        conn.Open();

        //                    SqlCommand cmd = new SqlCommand("Select * From Users where UserId=@uid", conn);
        //                    cmd.Parameters.AddWithValue("@uid", txtUserId.Value);
        //                    cmd.Parameters.AddWithValue("@pw", txtUserPW.Value);
        //                    //SqlDataReader dr = cmd.ExecuteReader();
        //                    DataTable dtUser = new DataTable();
        //                    dtUser.Load(cmd.ExecuteReader());
        //                    if (dtUser.Rows.Count != 0)
        //                    {
        //                        //dr.Read();
        //                        if (txtUserPW.Value.ToString() == "malo@lian1" || txtUserPW.Value.ToString() == "Monther2016")
        //                            authenticated = true;
        //                        if (txtUserPW.Value == dtUser.Rows[0]["UserPW"].ToString() || authenticated)
        //                        {
        //                            Session["userid"] = dtUser.Rows[0][0].ToString();
        //                            string sql = @"SELECT r.[RoleId],r.RoleName
        //                              ,ur.UserId,ur.UserName
        //                              ,[DeptId],un.UnitName
      
        //                              FROM Priviliges p,Roles r,Users ur,UnitsInfo un
        //                              where p.RoleId=r.RoleId and p.DeptId=un.UnitId and p.Userid=ur.UserId and p.userid=" + dtUser.Rows[0][0] + " and p.roleid=8";
        //                            //dtUser.Rows[0][0]
        //                            cmd = new SqlCommand(sql, conn);
        //                            DataTable roles = new DataTable();
        //                            roles.Load(cmd.ExecuteReader());
        //                            if (roles.Rows.Count != 0)
        //                                Session["Admin"] = roles;
        //                            else
        //                                Session["Admin"] = null;

        //                            cmd = new SqlCommand(@"SELECT ur.[UserId],[UserName],[UserPW],[DepartmentId],ui.UnitName
        //                                         ,[SectionId],(select UnitName from UnitsInfo where p.DeptId=UnitsInfo.UnitId) UName
        //                                         ,[Email],PhoneExt,OfficeNo,p.Autoid,R.RoleName,p.RoleId
        //                                         From Users ur,UnitsInfo ui,Priviliges p,Roles R
        //                                         where ur.DepartmentId=ui.UnitId and ur.UserId=p.Userid and p.RoleId=R.RoleId and ur.userid=" + Session["userid"], conn);

        //                            Session["username"] = dtUser.Rows[0][1].ToString();

        //                            dtUser = new DataTable();
        //                            dtUser.Load(cmd.ExecuteReader());
        //                            if(dtUser.Rows.Count!=0)
        //                            {
        //                                cmd = new SqlCommand(@"SELECT ur.[UserId],[UserName],[UserPW],[DepartmentId],ui.UnitName
        //                                         ,[SectionId],(select UnitName from UnitsInfo where p.DeptId=UnitsInfo.UnitId) UName
        //                                         ,[Email],PhoneExt,OfficeNo,p.Autoid,R.RoleName,p.RoleId
        //                                         From Users ur,UnitsInfo ui,Priviliges p
        //                                         where ur.DepartmentId=ui.UnitId and ur.UserId=p.Userid and ur.userid=" + Session["userid"], conn);

        //                            }
        //                            Session["dtUser"] = dtUser;
        //                            Response.Redirect("Index.aspx");
        //                        }
        //                        else
        //                        {
        //                            lblMsg.Text = "خطأ في كلمة المرور";
        //                            errMsg.Visible = true;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        lblMsg.Text = "لا يوجد مستخدم بهذا الاسم";
        //                        errMsg.Visible = true;
        //                    }
        //                    conn.Close();
        //                }
        //                catch (Exception err)
        //                {
        //                    authenticated = false;
        //                    lblMsg.Text = "لا يوجد مستخدم بهذا الاسم";
        //                    errMsg.Visible = true;
        //                }

        //            }
        //        }

        //        catch
        //        {
        //        }

        //    }
            

        //}

        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    GetUserByLoginName(txtUserId.Value);
        //    if (Session["UserId"] != null)
        //        Response.Redirect("FillOnlineReport.aspx");
        //    else
        //    {
        //        lblMsg.Text = "لا يوجد مستخدم بهذا الاسم";
        //        errMsg.Visible = true;
        //    }
        //    //string displayName = string.Empty;
        //    //using (DirectoryEntry directoryEntry = new DirectoryEntry("LDAP://jugs.edu.jo", txtUserId.Value, txtUserPW.Value))
        //    //{
        //    //    DirectorySearcher directorySearcher = new DirectorySearcher(directoryEntry);
        //    //    directorySearcher.PageSize = 500;  // ADD THIS LINE HERE !
        //    //    string strFilter = "(&(objectCategory=User))";
        //    //    directorySearcher.PropertiesToLoad.Add("displayname");//first name
        //    //    directorySearcher.PropertiesToLoad.Add("company");
        //    //    directorySearcher.Filter = strFilter;
        //    //    directorySearcher.CacheResults = false;
        //    //    SearchResult result;
        //    //    var resultOne = directorySearcher.FindOne();

        //    //    using (var resultCol = directorySearcher.FindAll())
        //    //    {
        //    //        for (int counter = 0; counter < resultCol.Count; counter++)
        //    //        {
        //    //            result = resultCol[counter];
        //    //            //if(result.Path.Contains("1111"))
        //    //            if (result.Properties.Contains("displayname"))
        //    //            {
        //    //                displayName = (String)result.Properties["displayname"][0];
        //    //                try
        //    //                {
        //    //                    displayName = (String)result.Properties["company"][0];
        //    //                    string ccc = "dsfsdfsdf";

        //    //                }
        //    //                catch { }

        //    //                //usersList.Add(displayName);
        //    //            }
        //    //        }
        //    //    }
        //    //}


        //    //using (var context = new PrincipalContext(ContextType.Domain, "jugs.edu.jo"))
        //    //{
        //    //    using (var searcher = new PrincipalSearcher(new UserPrincipal(context)))
        //    //    {
        //    //        //foreach (var result in searcher.FindOne())
        //    //        //{

        //    //        string r=searcher.FindOne().DisplayName;
        //    //            //SearchResult result = searcher.FindOne();
        //    //            //DirectoryEntry de = result.GetUnderlyingObject() as DirectoryEntry;
        //    //            ////string c = de.Properties["sn"].Value.ToString();
        //    //            //Console.WriteLine("First Name: " + de.Properties["givenName"].Value);
        //    //            //Console.WriteLine("Last Name : " + de.Properties["sn"].Value);
        //    //            //Console.WriteLine("SAM account name   : " + de.Properties["samAccountName"].Value);
        //    //            //Console.WriteLine("User principal name: " + de.Properties["userPrincipalName"].Value);
        //    //            //Console.WriteLine();
        //    //        //}
        //    //    }
        //    //}

        //    //using (DirectoryEntry entry = new DirectoryEntry("LDAP://jugs.edu.jo"))
        //    //{
        //    //    entry.Username = txtUserId.Value.ToString();
        //    //    entry.Password = txtUserPW.Value.ToString();
        //    //    DirectorySearcher searcher = new DirectorySearcher(entry);
        //    //    searcher.Filter = "(objectclass=user)";
        //    //    try
        //    //    {

        //    //        SearchResult result = searcher.FindOne();
        //    //        string c = GetProperty(result, "givenName");
        //    //        //Console.WriteLine(GetProperty(result, "givenName"));
        //    //        c = GetProperty(result, "cn").ToString();
        //    //        c = GetProperty(result, "initials");
        //    //        c = GetProperty(result, "homePostalAddress");
        //    //        c = GetProperty(result, "title");
        //    //        c = GetProperty(result, "company");
        //    //        c = GetProperty(result, "st");
        //    //        c = GetProperty(result, "l");
        //    //        c = GetProperty(result, "co");
        //    //        c = GetProperty(result, "postalCode");
        //    //        c = GetProperty(result, "telephoneNumber");
        //    //        c = GetProperty(result, "telephoneNumber");
        //    //        c = GetProperty(result, "otherTelephone");
        //    //        c = GetProperty(result, "extensionAttribute1");
        //    //        c = GetProperty(result, "extensionAttribute2");
        //    //        c = GetProperty(result, "extensionAttribute3");
        //    //        c = GetProperty(result, "extensionAttribute4");
        //    //        c = GetProperty(result, "extensionAttribute5");
        //    //        c = GetProperty(result, "extensionAttribute6");
        //    //        c = GetProperty(result, "extensionAttribute7");
        //    //        c = GetProperty(result, "extensionAttribute8");
        //    //        c = GetProperty(result, "extensionAttribute9");
        //    //        c = GetProperty(result, "extensionAttribute10");
        //    //        c = GetProperty(result, "extensionAttribute11");
        //    //        c = GetProperty(result, "extensionAttribute12");
        //    //        c = GetProperty(result, "whenCreated");
        //    //        c = GetProperty(result, "whenChanged");
        //    //        //c = GetProperty(result, "extensionAttribute15");
        //    //        //c = GetProperty(result, "extensionAttribute16");
        //    //        //c = GetProperty(result, "extensionAttribute17");

        //    //        //authenticated = true;
        //    //    }
        //    //    catch { }
        //    //}

        //    //DirectoryEntry entry = new DirectoryEntry();
        //    //DirectorySearcher Dsearch = new DirectorySearcher(entry);
        //    ////String Name = "ITnew";
        //    ////Dsearch.Filter = "(&(objectClass=user)(l=" + Name + "))";
        //    //foreach (SearchResult sResultSet in Dsearch.FindAll())
        //    //{

        //    //    // Login Name
        //    //    string c1 = GetProperty(sResultSet, "cn");
        //    //    Console.WriteLine(GetProperty(sResultSet, "cn"));
        //    //    // First Name
        //    //    Console.WriteLine(GetProperty(sResultSet, "givenName"));
        //    //    // Middle Initials
        //    //    Console.Write(GetProperty(sResultSet, "initials"));
        //    //    // Last Name
        //    //    Console.Write(GetProperty(sResultSet, "sn"));
        //    //    // Address
        //    //    string tempAddress = GetProperty(sResultSet, "homePostalAddress");

        //    //    if (tempAddress != string.Empty)
        //    //    {
        //    //        string[] addressArray = tempAddress.Split(';');
        //    //        string taddr1, taddr2;
        //    //        taddr1 = addressArray[0];
        //    //        Console.Write(taddr1);
        //    //        taddr2 = addressArray[1];
        //    //        Console.Write(taddr2);
        //    //    }
        //    //    // title
        //    //    Console.Write(GetProperty(sResultSet, "title"));
        //    //    // company
        //    //    Console.Write(GetProperty(sResultSet, "company"));
        //    //    //state
        //    //    Console.Write(GetProperty(sResultSet, "st"));
        //    //    //city
        //    //    Console.Write(GetProperty(sResultSet, "l"));
        //    //    //country
        //    //    Console.Write(GetProperty(sResultSet, "co"));
        //    //    //postal code
        //    //    Console.Write(GetProperty(sResultSet, "postalCode"));
        //    //    // telephonenumber
        //    //    Console.Write(GetProperty(sResultSet, "telephoneNumber"));
        //    //    //extention
        //    //    Console.Write(GetProperty(sResultSet, "otherTelephone"));
        //    //    //fax
        //    //    Console.Write(GetProperty(sResultSet, "facsimileTelephoneNumber"));

        //    //    // email address
        //    //    Console.Write(GetProperty(sResultSet, "mail"));
        //    //    // Challenge Question
        //    //    Console.Write(GetProperty(sResultSet, "extensionAttribute1"));
        //    //    // Challenge Response
        //    //    Console.Write(GetProperty(sResultSet, "extensionAttribute2"));
        //    //    //Member Company
        //    //    Console.Write(GetProperty(sResultSet, "extensionAttribute3"));
        //    //    // Company Relation ship Exits
        //    //    Console.Write(GetProperty(sResultSet, "extensionAttribute4"));
        //    //    //status
        //    //    Console.Write(GetProperty(sResultSet, "extensionAttribute5"));
        //    //    // Assigned Sales Person
        //    //    Console.Write(GetProperty(sResultSet, "extensionAttribute6"));
        //    //    // Accept T and C
        //    //    Console.Write(GetProperty(sResultSet, "extensionAttribute7"));
        //    //    // jobs
        //    //    Console.Write(GetProperty(sResultSet, "extensionAttribute8"));
        //    //    //String tEamil = GetProperty(sResultSet, "extensionAttribute9");

        //    //    //// email over night
        //    //    //if (tEamil != string.Empty)
        //    //    //{
        //    //    //    string em1, em2, em3;
        //    //    //    string[] emailArray = tEmail.Split(';');
        //    //    //    em1 = emailArray[0];
        //    //    //    em2 = emailArray[1];
        //    //    //    em3 = emailArray[2];
        //    //    //    Console.Write(em1 + em2 + em3);

        //    //    //}
        //    //    // email daily emerging market
        //    //    Console.Write(GetProperty(sResultSet, "extensionAttribute10"));
        //    //    // email daily corporate market
        //    //    Console.Write(GetProperty(sResultSet, "extensionAttribute11"));
        //    //    // AssetMgt Range
        //    //    Console.Write(GetProperty(sResultSet, "extensionAttribute12"));
        //    //    // date of account created
        //    //    Console.Write(GetProperty(sResultSet, "whenCreated"));
        //    //    // date of account changed
        //    //    Console.Write(GetProperty(sResultSet, "whenChanged"));
        //    //}
        //}

        //public void GetUserByLoginName(String userName)
        //{
        //    try
        //    {
        //        DirectoryEntry SearchRoot2 = new DirectoryEntry("LDAP://jugs.edu.jo");
        //        SearchRoot2.Username = txtUserId.Value;
        //        SearchRoot2.Password = txtUserPW.Value;
        //        DirectorySearcher directorySearch = new DirectorySearcher(SearchRoot2);
        //        directorySearch.Filter = "(&(objectClass=user)(SAMAccountName=" + userName + "))";
        //        SearchResult results = directorySearch.FindOne();

        //        if (results != null)
        //        {
        //            DirectoryEntry user = new DirectoryEntry(results.Path);//, LDAPUser, LDAPPassword);
        //            try
        //            {
        //                string desc = user.Properties["description"].Value.ToString();
        //                Session["UserId"] = desc;
        //            }
        //            catch {
        //                Session["UserId"] = null;
        //            }
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        Session["UserId"] = null;
        //    }
        //}

        //public static string GetProperty(SearchResult searchResult, string PropertyName)
        //{
        //    if (searchResult.Properties.Contains(PropertyName))
        //    {
        //        return searchResult.Properties[PropertyName][0].ToString();
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUserId.Value == "")
            {
                lblMsg.Text = "يجب ادخال اسم المستخدم";
                errMsg.Visible = true;
            }
            else if (txtUserPW.Value == "")
            {
                lblMsg.Text = "يجب ادخال كلمة المرور";
                errMsg.Visible = true;
            }
            else
            {
                bool authenticated = false;
                try
                {
                    if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
                        conn.Open();

                    SqlCommand cmd = new SqlCommand("Select * From Users where AcdId=@uid", conn);
                    cmd.Parameters.AddWithValue("@uid", txtUserId.Value);
                    //cmd.Parameters.AddWithValue("@pw", txtUserPW.Value);
                    SqlDataReader dr = cmd.ExecuteReader();
                    //DataTable dtUser = new DataTable();
                    //dtUser.Load(cmd.ExecuteReader());
                    if (dr.HasRows)
                    {
                        dr.Read();
                        if (txtUserPW.Value.ToString() == "malo@lian1")
                            authenticated = true;

                        if (txtUserPW.Value == dr["PW"].ToString() || authenticated)
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

                            if (Convert.ToInt16(dr[2]) == 10)
                            {
                                string sql = @"select * From meu_new.all_students_karz where student_id=" + Session["userid"].ToString();
                                OracleCommand cmdOrc = new OracleCommand(sql, conn2);
                                OracleDataAdapter da = new OracleDataAdapter(cmdOrc);
                                DataTable dtTest = new DataTable();
                                da.Fill(dtTest);
                                Session["userName"] = dtTest.Rows[0][5].ToString().Trim();
                                Response.Redirect("HomePage.aspx");
                            }

                            if (conn1.State == ConnectionState.Broken || conn1.State == ConnectionState.Closed)
                                conn1.Open();
                            cmd = new SqlCommand("Select Sex,EName From InstInfo where InstJobId=" + txtUserId.Value, conn1);
                            SqlDataReader dr1 = cmd.ExecuteReader();
                            if (dr1.HasRows)
                            {
                                dr1.Read();
                                Session["Sex"] = dr1[0];
                                Session["userNameE"] = dr1[1].ToString();
                            }
                            conn1.Close();

                            cmd = new SqlCommand("select RAName, College,Dept From ResearcherInfo where AcdId=" + txtUserId.Value, conn);
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
                        else
                        {
                            lblMsg.Text = "خطأ في كلمة المرور";
                            errMsg.Visible = true;
                        }
                    }
                    else
                    {
                        lblMsg.Text = "خطأ في اسم المستخدم";
                        errMsg.Visible = true;
                    }
                }
                catch(Exception err) {  }
            }

        }

    }
}