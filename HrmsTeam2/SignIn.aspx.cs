using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HrmsTeam2
{
    public partial class SignIn : System.Web.UI.Page
    {
        SqlConnection conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["DBconn"].ConnectionString;
            conn = new SqlConnection(cs);
            conn.Open();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string email = TextBox1.Text, pass = TextBox2.Text;

            string qi = $"exec SignIn '{email}'";
            SqlCommand cmd = new SqlCommand(qi, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string dbHashPassword = reader["PasswordHash"].ToString();
                    string uRole = reader["UserRole"].ToString();
                    string userStatus = reader["UserStatus"].ToString();
                    int userId = int.Parse(reader["UserId"].ToString());

                    // Check if user is active
                    if (userStatus != "Active")
                    {
                        Response.Write("<script>alert('Your account is inactive. Please contact the administrator.');</script>");
                        return;
                    }

                    if (VerifyPassword(pass, dbHashPassword))
                    {
                        Session["MyUser"] = email;
                        Session["UserId"] = userId;    // Store UserID in session
                        Session["UserRole"] = uRole;
                        if (uRole == "User")
                        {
                            Response.Redirect("UserHome.aspx");
                        }
                        else if(uRole == "Admin")
                        {
                            Response.Redirect("AdminHome.aspx");
                        }
                    }
                }
            }
            else
            {
                Response.Write("<script>alert('Invalid Users Credential!!! TRY AGAIN.');</script>");
            }
        }

        // Method to verify the password
        private bool VerifyPassword(string inputPassword, string storedHash)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(inputPassword));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in inputBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                string hashedInput = sb.ToString();

                return hashedInput.Equals(storedHash); // Compare hashed passwords
            }
        }
    }

}
