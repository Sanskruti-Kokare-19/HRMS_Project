using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HrmsTeam2
{
    public partial class PerformanceAdmin : System.Web.UI.Page
    {
        SqlConnection conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["DBconn"].ConnectionString;
            conn = new SqlConnection(cs);
            conn.Open();

            if(!IsPostBack)
            {
                BindDropdown(); 
            }
        }

        private void BindDropdown()
        {
            string query = "SELECT UserId, UserName FROM UserRegistration WHERE UserStatus = 'Active'";
            SqlCommand cmd = new SqlCommand(query, conn);
              
            SqlDataReader reader = cmd.ExecuteReader();

            DropDownList1.DataSource = reader;
            DropDownList1.DataTextField = "UserName";
            DropDownList1.DataValueField = "UserId";
            DropDownList1.DataBind();

            // Add "All Users" option
            DropDownList1.Items.Insert(0, new ListItem("All Users", "0"));
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int adminId = (int)Session["UserId"];  // Get Admin ID from session
            int selectedUserId = int.Parse(DropDownList1.SelectedValue);
            DateTime sentDate = DateTime.Now;
            if (selectedUserId == 0) // "All Users" selected
            {
                // Query to get all active users
                string query = "SELECT UserId FROM UserRegistration WHERE UserRole = 'User'";
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                // Collect all User IDs in a list to avoid keeping the reader open
                List<int> userIds = new List<int>();
                while (reader.Read())
                {
                    userIds.Add((int)reader["UserId"]);
                }
                reader.Close(); // Close the reader before running other queries

                // Loop through each User ID and execute queries
                foreach (int userId in userIds)
                {
                    // Send performance review
                    string sendQuery = $"exec SendPerReviews '{adminId}', '{sentDate}', 0, 'User'";
                    SqlCommand sendCmd = new SqlCommand(sendQuery, conn);
                    sendCmd.ExecuteNonQuery();

                    // Update UserNotificationStatus
                    string updateNotificationQuery = $"exec UpdateUser 1, '{userId}'";
                    SqlCommand updateCmd = new SqlCommand(updateNotificationQuery, conn);
                    updateCmd.ExecuteNonQuery();
                }

                lblMessage.Text = "Performance reviews sent to all users successfully!";
            }
            else // Specific user selected
            {
                string query = $"exec SendPerReviews '{adminId}', 'User', '{sentDate}', 0, '{selectedUserId}'";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();

                // Update UserNotificationStatus in UserRegistration table
                string updateNotificationQuery = $"exec UpdateUser 1, '{selectedUserId}'";
                SqlCommand updateCmd = new SqlCommand(updateNotificationQuery, conn);
                updateCmd.ExecuteNonQuery();

                lblMessage.Text = "Performance review sent successfully!";
            }
            lblMessage.ForeColor = System.Drawing.Color.Green;
        }
    }
}