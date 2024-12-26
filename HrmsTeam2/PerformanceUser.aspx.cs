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
    public partial class PerformanceUser : System.Web.UI.Page
    {
        SqlConnection conn;
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    // Initialize connection
        //    string cs = ConfigurationManager.ConnectionStrings["DBconn"].ConnectionString;
        //    conn = new SqlConnection(cs);
        //    conn.Open();

        //    if (!IsPostBack)
        //    {
        //        // Check notification status
        //        string notificationQuery = $"SELECT UserNotificationStatus FROM UserRegistration WHERE UserRole = 'User'";
        //        SqlCommand cmd = new SqlCommand(notificationQuery, conn);
        //        SqlDataReader reader = cmd.ExecuteReader();

        //        if (reader.Read())
        //        {
        //            int notificationStatus = reader["UserNotificationStatus"] != DBNull.Value
        //                ? Convert.ToInt32(reader["UserNotificationStatus"])
        //                : 0;

        //            if (notificationStatus == 0)
        //            {
        //                Button1.Enabled = false;
        //                lblMessage.Text = "Awaiting notification to fill the performance review form.";
        //                lblMessage.ForeColor = System.Drawing.Color.Red;
        //            }
        //            else
        //            {
        //                Button1.Enabled = true;
        //            }
        //        }
        //    }
        //}
        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    if (Session["UserId"] == null)
        //    {
        //        Response.Write("<script>alert('User session expired. Please log in again.');</script>");
        //        return;
        //    }

        //    int userId = (int)Session["UserId"];
        //    DateTime submittedDate = DateTime.Now;

        //    // Fetch Review ID for the current user where Completed = 0
        //    string getReviewQuery = $"exec getReview '{userId}', 0";
        //    SqlCommand cmd = new SqlCommand(getReviewQuery, conn);
        //    SqlDataReader reader = cmd.ExecuteReader();

        //    if (reader.Read())
        //    {
        //        int reviewId = reader["review_id"] != DBNull.Value ? Convert.ToInt32(reader["review_id"]) : 0;

        //        if (reviewId > 0)
        //        {
        //            // Insert answers into database
        //            for (int i = 1; i <= 10; i++)
        //            {
        //                TextBox txtAnswer = this.FindControl($"q{i}") as TextBox;

        //                if (txtAnswer != null)
        //                {
        //                    string answerText = txtAnswer.Text.Trim();
        //                    string insertQuery = $"exec InsertAnswer '{i}','{answerText}', '{userId}', '{submittedDate}'";
        //                    SqlCommand comd = new SqlCommand(insertQuery, conn);
        //                    comd.ExecuteNonQuery();
        //                }
        //            }
        //            // Update review status
        //            string updateReviewQuery = $"exec UpdateReview 1, '{reviewId}'";
        //            SqlCommand updateCmd = new SqlCommand(updateReviewQuery, conn);
        //            updateCmd.ExecuteNonQuery();

        //            // Reset notification status
        //            string resetNotificationQuery = $"exec UpdateUser '{userId}', '0'";
        //            SqlCommand resetCmd = new SqlCommand(resetNotificationQuery, conn);
        //            resetCmd.ExecuteNonQuery();}

        //            Response.Write("<script>alert('Performance review submitted successfully!');</script>");
        //        }
        //    else
        //    {
        //        Response.Write("<script>alert('No review found for the user.');</script>");
        //    }
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // Initialize database connection
                string cs = ConfigurationManager.ConnectionStrings["DBconn"].ConnectionString;
                conn = new SqlConnection(cs);
                conn.Open();

                if (!IsPostBack)
                {
                    CheckNotificationStatus();
                }
            }
            catch (Exception ex)
            {
                // Handle any connection errors
                ShowErrorMessage("Database connection error: " + ex.Message);
            }
        }

        // Separate method to check notification status for better organization
        private void CheckNotificationStatus()
        {
            using (SqlCommand cmd = new SqlCommand("SELECT UserNotificationStatus FROM UserRegistration WHERE UserRole = 'User'", conn))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    int notificationStatus = reader["UserNotificationStatus"] != DBNull.Value
                        ? Convert.ToInt32(reader["UserNotificationStatus"])
                        : 0;

                    // Enable/disable the submit button based on notification status
                    Button1.Enabled = notificationStatus != 0;
                    if (!Button1.Enabled)
                    {
                        lblMessage.Text = "Awaiting notification to fill the performance review form.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Verify user session
                if (Session["UserId"] == null)
                {
                    ShowErrorMessage("User session expired. Please log in again.");
                    return;
                }

                int userId = (int)Session["UserId"];
                DateTime submittedDate = DateTime.Now;

                // Get the review ID
                int reviewId = GetReviewId(userId);
                if (reviewId <= 0)
                {
                    ShowErrorMessage("No review found for the user.");
                    return;
                }

                // Process all answers using direct TextBox references
                ProcessAnswers(userId, submittedDate);

                // Update review status and reset notification
                UpdateReviewStatus(reviewId);
                ResetNotificationStatus(userId);

                ShowSuccessMessage("Performance review submitted successfully!");
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Error submitting review: " + ex.Message);
            }
        }

        // Helper method to get review ID
        private int GetReviewId(int userId)
        {
            using (SqlCommand cmd = new SqlCommand("exec getReview @UserId, @Status", conn))
            {
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@Status", 0);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader["review_id"] != DBNull.Value ? Convert.ToInt32(reader["review_id"]) : 0;
                    }
                    return 0;
                }
            }
        }

        // Process answers using direct TextBox references
        private void ProcessAnswers(int userId, DateTime submittedDate)
        {
            // Direct access to TextBox controls using their IDs
            TextBox[] answerBoxes = new TextBox[]
            {
                q1, q2, q3, q4, q5, q6, q7, q8, q9, q10
            };

            for (int i = 0; i < answerBoxes.Length; i++)
            {
                if (answerBoxes[i] != null)
                {
                    string answerText = answerBoxes[i].Text.Trim();

                    // Use parameterized query to properly handle data types
                    using (SqlCommand cmd = new SqlCommand("exec InsertAnswer @QuestionId, @AnswerText, @UserId, @SubmittedDate", conn))
                    {
                        cmd.Parameters.AddWithValue("@QuestionId", i + 1);  // Integer - no quotes
                        cmd.Parameters.AddWithValue("@AnswerText", answerText);  // String - handled properly
                        cmd.Parameters.AddWithValue("@UserId", userId);  // Integer - no quotes
                        cmd.Parameters.AddWithValue("@SubmittedDate", submittedDate);  // DateTime - handled properly
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }


        // Update review status
        private void UpdateReviewStatus(int reviewId)
        {
            using (SqlCommand cmd = new SqlCommand("exec UpdateReview @Status, @ReviewId", conn))
            {
                cmd.Parameters.AddWithValue("@Status", 1);
                cmd.Parameters.AddWithValue("@ReviewId", reviewId);
                cmd.ExecuteNonQuery();
            }
        }

        // Reset notification status
        private void ResetNotificationStatus(int userId)
        {
            using (SqlCommand cmd = new SqlCommand("exec UpdateUser @UserId, @Status", conn))
            {
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@Status", 0);
                cmd.ExecuteNonQuery();
            }
        }

        // Helper methods for showing messages
        private void ShowErrorMessage(string message)
        {
            Response.Write($"<script>alert('{message}');</script>");
        }

        private void ShowSuccessMessage(string message)
        {
            Response.Write($"<script>alert('{message}');</script>");
        }

        // Clean up database connection
        protected override void OnUnload(EventArgs e)
        {
            if (conn != null && conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
            base.OnUnload(e);
        }
    }
}


