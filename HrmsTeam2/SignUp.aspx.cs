using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HrmsTeam2
{
    public partial class SignUp : System.Web.UI.Page
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
            string name=TextBox1.Text, email=TextBox2.Text, contact=TextBox3.Text, password=TextBox4.Text, urole="User", status="Active", uimg;
            string hashPassword = HashPassword(password);

            if (FileUpload1.HasFile) 
            {
                string fileExtension = Path.GetExtension(FileUpload1.FileName).ToLower();
                string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

                if(allowedExtensions.Contains(fileExtension))
                {
                    // Generate a unique filename to avoid conflicts
                    string fileName = Guid.NewGuid().ToString() + fileExtension;

                    // Save the file to the server
                    string savePath = Server.MapPath("ProfilePhoto/") + fileName;
                    FileUpload1.SaveAs(savePath);

                    // Set the image path for storage in the database
                    uimg = "ProfilePhoto/" + fileName;

                    string qf = $"exec FindExistingUser '{email}'";
                    SqlCommand command = new SqlCommand(qf, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        Response.Write("<script>alert('User already Exists');</script>");
                    }
                    else
                    {
                        string q = $"exec SignUp '{name}', '{email}', '{contact}', '{hashPassword}', '{urole}', '{status}', '{uimg}'";
                        SqlCommand cmd = new SqlCommand(q, conn);
                        cmd.ExecuteNonQuery();

                        Response.Write("<script>alert('User Register Successfully'); window.location.href='SignIn.aspx';</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('Only image files (.jpg, .jpeg, .png, .gif) are allowed.');</script>");
                }
            }            
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2")); // Convert to hexadecimal
                }
                return builder.ToString();
            }
        }
    }
}