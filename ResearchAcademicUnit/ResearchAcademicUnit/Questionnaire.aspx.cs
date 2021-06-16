using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchAcademicUnit
{
    public partial class Questionnaire : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["RConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.IsNewSession || Session["userid"] == null)
                Response.Redirect("Index.aspx");
            if(!IsPostBack)
            {
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("Select * From ResearcherInfo where AcdId=" + Session["userid"], conn);
                SqlDataReader dr = cmd.ExecuteReader();
                if(dr.HasRows)
                {
                    dr.Read();
                    lblCollege.Text = dr["College"].ToString();
                    lblDept.Text = dr["Dept"].ToString();
                    lblAcdDegree.Text = dr["RLevel"].ToString();
                }
                conn.Close();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand("insert into TableQuest values(@col,@dept,@exp,@sex,@acddeg,@pos,@rd1,@rd2,@rd3,@rd4,@rd5,@rd6,@rd7,@rd8,@rd9,@rd10,@rd11,@rd12,@rd13,@rd14,@rd15,@rd16,@note,@jobid)", conn);
            cmd.Parameters.AddWithValue("@col",lblCollege.Text);
            cmd.Parameters.AddWithValue("@dept",lblDept.Text);
            cmd.Parameters.AddWithValue("@exp",txtExp.Text);
            cmd.Parameters.AddWithValue("@sex",rdSex.SelectedValue);
            cmd.Parameters.AddWithValue("@acddeg",lblAcdDegree.Text);
            cmd.Parameters.AddWithValue("@pos",ddlPos.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@rd1",rd1.SelectedValue);
            cmd.Parameters.AddWithValue("@rd2", rd2.SelectedValue);
            cmd.Parameters.AddWithValue("@rd3", rd3.SelectedValue);
            cmd.Parameters.AddWithValue("@rd4", rd4.SelectedValue);
            cmd.Parameters.AddWithValue("@rd5", rd5.SelectedValue);
            cmd.Parameters.AddWithValue("@rd6", rd6.SelectedValue);
            cmd.Parameters.AddWithValue("@rd7", rd7.SelectedValue);
            cmd.Parameters.AddWithValue("@rd8", rd8.SelectedValue);
            cmd.Parameters.AddWithValue("@rd9", rd9.SelectedValue);
            cmd.Parameters.AddWithValue("@rd10", rd10.SelectedValue);
            cmd.Parameters.AddWithValue("@rd11", rd11.SelectedValue);
            cmd.Parameters.AddWithValue("@rd12", rd12.SelectedValue);
            cmd.Parameters.AddWithValue("@rd13", rd13.SelectedValue);
            cmd.Parameters.AddWithValue("@rd14", rd14.SelectedValue);
            cmd.Parameters.AddWithValue("@rd15", rd15.SelectedValue);
            cmd.Parameters.AddWithValue("@rd16", rd16.SelectedValue);
            cmd.Parameters.AddWithValue("@note",txtuggestion.Text);
            cmd.Parameters.AddWithValue("@jobid", Session["userid"]);
            if (cmd.ExecuteNonQuery()>0)
            {
                msg.Visible = true;
                Timer1.Enabled = true;
            }
            conn.Close();
        }

        protected void btnIgnore_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Timer1.Enabled = false;
            Response.Redirect("Default.aspx");
        }
    }
}