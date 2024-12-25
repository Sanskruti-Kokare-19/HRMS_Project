using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRMS
{
    public partial class Notification : System.Web.UI.Page
    {
        SqlConnection conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            conn = new SqlConnection(cs);
            conn.Open();
            if (!IsPostBack)
            {
                LoadUserEmails();
            }
        }

        private void LoadUserEmails()
        {
            SqlCommand cmd = new SqlCommand("SELECT UserEmail FROM UserRegistration WHERE UserStatus = 'Active'", conn);

            SqlDataReader reader = cmd.ExecuteReader();

            ddlUsers.DataSource = reader;
            ddlUsers.DataTextField = "UserEmail";
            ddlUsers.DataValueField = "UserEmail";
            ddlUsers.DataBind();

            ddlUsers.Items.Insert(0, new ListItem("-- Select Users --", ""));
        }


        protected void btnSend_Click(object sender, EventArgs e)
        {
            string notificationMessage = txtNotification.Text.Trim();
            int notificationsSent = 0;

            foreach (ListItem item in ddlUsers.Items)
            {
                if (item.Selected)
                {
                    // Call method to send notification
                    if (SendNotification(item.Value, notificationMessage))
                    {
                        notificationsSent++;
                    }
                }
            }

            lblMessage.Text = $"{notificationsSent} notifications sent successfully!";
        }

        private bool SendNotification(string email, string message)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    throw new ArgumentException("Email cannot be null or empty.");
                }

                string cs = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    using (SqlCommand cmd = new SqlCommand("AddNotification", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        // Add parameters
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@NotificationMessage", message);

                        conn.Open(); // Open the connection
                        cmd.ExecuteNonQuery(); // Execute the stored procedure
                    }
                }

                return true; // Return true if operation succeeds
            }
            catch (Exception ex)
            {
                // Log or display the exception (optional)
                lblMessage.Text = $"Error: {ex.Message}";
                return false; // Return false if any error occurs
            }
        }


        //    private bool SendNotification(string email, string message)
        //    {
        //        string cs = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
        //        conn = new SqlConnection(cs);

        //        SqlCommand cmd = new SqlCommand("AddNotification", conn);
        //        cmd.Parameters.AddWithValue("@Email", email);
        //        cmd.Parameters.AddWithValue("@NotificationMessage", message);

        //        conn.Open();
        //        cmd.ExecuteNonQuery();
        //        return true;


        //    }
        //}
    }
}