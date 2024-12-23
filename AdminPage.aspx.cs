using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRMS
{
    public partial class AdminPage : System.Web.UI.Page
    {
        SqlConnection conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string cs = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
                conn = new SqlConnection(cs);
                conn.Open();
                BindLeaveRequests();
            }

        }

        protected void LeaveRequestsGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Approve")
            {
                int leaveId = Convert.ToInt32(e.CommandArgument);
                ApproveLeave(leaveId);
            }
            else if (e.CommandName == "Reject")
            {
                int leaveId = Convert.ToInt32(e.CommandArgument);
                RejectLeave(leaveId);
            }
        }

        private void ApproveLeave(int leaveId)
        {
            string cs = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            conn = new SqlConnection(cs);
            conn.Open();

            string adminName = "Admin";

            // Fetch details for the leave ID
            string query = "SELECT Email, StartDate, EndDate FROM LeaveRequests WHERE LeaveId = @LeaveId";
            SqlCommand fetchCmd = new SqlCommand(query, conn);
            fetchCmd.Parameters.AddWithValue("@LeaveId", leaveId);

            SqlDataReader reader = fetchCmd.ExecuteReader();
            string email = string.Empty;
            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MinValue;

            if (reader.Read())
            {
                email = reader["Email"]?.ToString();
                startDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]) : DateTime.MinValue;
                endDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]) : DateTime.MinValue;
            }
            reader.Close();

            if (string.IsNullOrEmpty(email) || startDate == DateTime.MinValue || endDate == DateTime.MinValue)
            {
                Response.Write("Error: Leave request details are invalid.");
                return;
            }

            SqlCommand cmd = new SqlCommand("sp_UpdateLeaveStatus", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@LeaveStatus", "Approved");
            cmd.Parameters.AddWithValue("@AdminName", adminName);
            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);

            cmd.ExecuteNonQuery();

            UpdateLeaveBalance(conn, leaveId);
            BindLeaveRequests();
        }

        private void RejectLeave(int leaveId)
        {
            string cs = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            conn = new SqlConnection(cs);
            conn.Open();

            string adminName = "Admin";

            // Fetch details for the leave ID
            string query = "SELECT Email, StartDate, EndDate FROM LeaveRequests WHERE LeaveId = @LeaveId";
            SqlCommand fetchCmd = new SqlCommand(query, conn);
            fetchCmd.Parameters.AddWithValue("@LeaveId", leaveId);

            SqlDataReader reader = fetchCmd.ExecuteReader();
            string email = string.Empty;
            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MinValue;

            if (reader.Read())
            {
                email = reader["Email"]?.ToString();
                startDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]) : DateTime.MinValue;
                endDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]) : DateTime.MinValue;
            }
            reader.Close();

            if (string.IsNullOrEmpty(email) || startDate == DateTime.MinValue || endDate == DateTime.MinValue)
            {
                Response.Write("Error: Leave request details are invalid.");
                return;
            }

            SqlCommand cmd = new SqlCommand("sp_UpdateLeaveStatus", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@LeaveStatus", "Rejected");
            cmd.Parameters.AddWithValue("@AdminName", adminName);
            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);

            cmd.ExecuteNonQuery();

            BindLeaveRequests();
        }


        private void BindLeaveRequests()
        {
            string query = @"
            SELECT LeaveId, Name, Email, LeaveType, StartDate, EndDate, TotalDays, Reason, LeaveStatus, AdminActionBy, AdminActionDate 
            FROM LeaveRequests";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            System.Data.DataTable dt = new System.Data.DataTable();
            adapter.Fill(dt);
            //string query = "SELECT LeaveId, Name, Email, LeaveType, StartDate, EndDate, TotalDays, Reason FROM LeaveRequests WHERE LeaveStatus = 'Pending'";
            //SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            //System.Data.DataTable dt = new System.Data.DataTable();
            //adapter.Fill(dt);

            LeaveRequestsGrid.DataSource = dt;
            LeaveRequestsGrid.DataBind();
        }

        //protected void ApproveLeave_Click(object sender, EventArgs e)
        // {
        //     Button btn = (Button)sender;
        //     int leaveId = Convert.ToInt32(btn.CommandArgument);
        //     string cs = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
        //     conn = new SqlConnection(cs);
        //     conn.Open();
        //     SqlCommand cmd = new SqlCommand("sp_UpdateLeaveStatus", conn);
        //     cmd.Parameters.AddWithValue("@LeaveId", leaveId);
        //     cmd.Parameters.AddWithValue("@LeaveStatus", "Approved");
        //     cmd.Parameters.AddWithValue("@AdminName", Session["AdminName"]); 
        //     cmd.ExecuteNonQuery();

        //     UpdateLeaveBalance(conn, leaveId);

        //     BindLeaveRequests();
        // }

        // protected void RejectLeave_Click(object sender, EventArgs e)
        // {
        //     Button btn = (Button)sender;
        //     int leaveId = Convert.ToInt32(btn.CommandArgument);

        //     SqlCommand cmd = new SqlCommand("sp_UpdateLeaveStatus", conn);
        //     cmd.Parameters.AddWithValue("@LeaveId", leaveId);
        //     cmd.Parameters.AddWithValue("@LeaveStatus", "Rejected");
        //     cmd.Parameters.AddWithValue("@AdminName", Session["AdminName"]); 
        //     cmd.ExecuteNonQuery();

        //     BindLeaveRequests();
        // }

        private void UpdateLeaveBalance(SqlConnection conn, int leaveId)
        {
            // Ensure connection is open
            if (conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();
            }

            // Fetch details of the leave request
            string email = string.Empty;
            string leaveType = string.Empty;
            int totalDays = 0;

            string fetchQuery = "SELECT Email, LeaveType, TotalDays FROM LeaveRequests WHERE LeaveId = @LeaveId";
            using (SqlCommand fetchCmd = new SqlCommand(fetchQuery, conn))
            {
                fetchCmd.Parameters.AddWithValue("@LeaveId", leaveId);

                using (SqlDataReader reader = fetchCmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        email = reader["Email"]?.ToString();
                        leaveType = reader["LeaveType"]?.ToString();
                        totalDays = reader["TotalDays"] != DBNull.Value ? Convert.ToInt32(reader["TotalDays"]) : 0;
                    }
                }
            }

            // Validate fetched data
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(leaveType) || totalDays <= 0)
            {
                Response.Write("Error: Invalid leave details. Cannot update leave balance.");
                return;
            }

            // Update leave balance
            string updateQuery = "EXEC sp_UpdateLeaveBalance @Email, @LeaveType, @Days";
            using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
            {
                updateCmd.Parameters.AddWithValue("@Email", email);
                updateCmd.Parameters.AddWithValue("@LeaveType", leaveType);
                updateCmd.Parameters.AddWithValue("@Days", totalDays);

                try
                {
                    updateCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Response.Write($"Error: Unable to update leave balance. Details: {ex.Message}");
                }
            }
        }

    }
}