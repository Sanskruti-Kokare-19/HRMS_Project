using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AttendanceTask5
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        SqlConnection conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
            conn.Open();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string username = TextBox1.Text;

            string password = TextBox2.Text;

            string q = $"select * from UserRegistration where UserName='{username}' and PasswordHash='{password}'";
            SqlCommand cmd = new SqlCommand(q, conn);
            SqlDataReader r = cmd.ExecuteReader();
            if (r.HasRows)
            {
                while (r.Read())
                {
                    if ((r["UserName"].Equals(username) && r["PasswordHash"].Equals(password)) && r["UserRole"].Equals("Admin"))
                    {
                        Session["MyUser"] = username;
                        Response.Redirect("AdminHome.aspx");
                    }
                    if (r["UserName"].Equals(username) && r["PasswordHash"].Equals(password) && r["UserRole"].Equals("User"))
                    {
                        Session["MyUser"] = username;
                        Response.Redirect("UserHome.aspx");
                    }

                }
            }
        }
    }
}