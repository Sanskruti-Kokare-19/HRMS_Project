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
    public partial class WebForm1 : System.Web.UI.Page
    {
        private int totalLeaves = 24;
        //private const int MaxCL = 2;
        //private const int MaxPL = 18;
        //private const int MaxML = 2;
        //private const int MaxSL = 2;
        SqlConnection conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            conn = new SqlConnection(cs);
            conn.Open();

            string email = "user@example.com";
            string name = "user";

            // No need to check for session, since values are hardcoded
            //if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email))
            //{
            //    Response.Redirect("Login.aspx");
            //    return;
            //}

            //if (!IsPostBack)
            //{
            //    Label1.Text = "The total Balance Leaves: " + totalLeaves;
            //    DisplayLeaveBalance(email);
            //}

            //if (Session["Username"] == null)
            //{
            //    Response.Redirect("Login.aspx");
            //    return;
            //}

            //if (!IsPostBack)
            //{
            //    Label1.Text = "The total Balance Leaves: " + totalLeaves;

            //    DisplayLeaveBalance();

            //}
        }

        //protected void SubmitLeaveButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string leaveType = LeaveTypeDropDown.SelectedValue;
        //        DateTime startDate = DateTime.Parse(StartDateTextBox.Text);
        //        DateTime endDate = DateTime.Parse(EndDateTextBox.Text);
        //        string reason = ReasonTextBox.Text;
        //        string username = "user";
        //        string email = "user@example.com";
        //        //string email = Session["Email"].ToString();
        //        //string name = Session["Username"].ToString();

        //        if (startDate > endDate)
        //        {
        //            LeaveStatusLabel.Text = "Invalid date range!";
        //            return;
        //        }

        //        int leaveDays = 0;
        //        for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
        //        {
        //            if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
        //            {
        //                leaveDays++;
        //            }
        //        }

        //        // Get the available leave balance for the user
        //        string cs = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
        //        conn = new SqlConnection(cs);
        //        conn.Open();

        //        string query = "SELECT CL, PL, ML, SL FROM EmployeeLeaveBalances WHERE Email = @Email";
        //        SqlCommand cmd = new SqlCommand(query, conn);
        //        cmd.Parameters.AddWithValue("@Email", email);

        //        SqlDataReader reader = cmd.ExecuteReader();
        //        int availableCL = 0, availablePL = 0, availableML = 0, availableSL = 0;
        //        if (reader.Read())
        //        {
        //            availableCL = Convert.ToInt32(reader["CL"]);
        //            availablePL = Convert.ToInt32(reader["PL"]);
        //            availableML = Convert.ToInt32(reader["ML"]);
        //            availableSL = Convert.ToInt32(reader["SL"]);
        //        }
        //        reader.Close();

        //        int maxLeaveDays = 0;
        //        int extraLeaves = 0;
        //        //string leaveStatus = "Approved"; //regular leave request
        //        if (leaveType == "CL")
        //        {
        //            maxLeaveDays = 2;
        //        }
        //        else if (leaveType == "PL")
        //        {
        //            maxLeaveDays = 18;
        //        }
        //        else if (leaveType == "SL")
        //        {
        //            maxLeaveDays = 2;
        //        }
        //        else if (leaveType == "ML")
        //        {
        //            maxLeaveDays = 2;
        //        }
        //        // Check if requested leave days exceed the available balance
        //        if (leaveDays > maxLeaveDays)
        //        {
        //            extraLeaves = leaveDays - maxLeaveDays;
        //            LeaveStatusLabel.Text = $"You can only request {maxLeaveDays} days for {leaveType}.";
        //            // Display extra leaves in an alert message
        //            Response.Write($"<script>alert('You are taking {extraLeaves} extra days for {leaveType} leave.');</script>");
        //        }
        //        else
        //        {
        //            LeaveStatusLabel.Text = $"Leave request submitted successfully! Total leave days requested: {leaveDays}. No extra leaves required.";
        //            // No extra leaves, no need for alert
        //        }
        //        //if (leaveDays > maxLeaveDays)
        //        //{
        //        //    LeaveStatusLabel.Text = $"You can only request {maxLeaveDays} days for {leaveType}.";
        //        //    return;
        //        //}

        //        if (leaveDays > totalLeaves)
        //        {
        //            LeaveStatusLabel.Text = "Insufficient leave balance.";
        //            return;
        //        }

        //        //string q = $"exec sp_InsertLeaveRequest '{name}','{email}','{leaveType}','{startDate}','{endDate}','{reason}','{leaveDays}'";
        //        //SqlCommand cmd = new SqlCommand(q, conn);
        //        //cmd.ExecuteNonQuery();

        //        // Using string formatting with parameterized SQL query
        //        string q = "exec sp_InsertLeaveRequest @Name, @Email, @LeaveType, @StartDate, @EndDate, @TotalDays, @Reason";

        //        // Create command and add parameters
        //        SqlCommand cmd1 = new SqlCommand(q, conn);
        //        cmd.Parameters.AddWithValue("@Name", username);
        //        cmd.Parameters.AddWithValue("@Email", email);
        //        cmd.Parameters.AddWithValue("@LeaveType", leaveType);
        //        cmd.Parameters.AddWithValue("@StartDate", startDate);  
        //        cmd.Parameters.AddWithValue("@EndDate", endDate);    
        //        cmd.Parameters.AddWithValue("@TotalDays", leaveDays);
        //        cmd.Parameters.AddWithValue("@Reason", reason);
        //        cmd.ExecuteNonQuery();
        //        //Response.Write("<script>alert('Leave Requested')</script>");

        //        totalLeaves -= leaveDays;
        //        Label1.Text = $"Remaining Leaves are: {totalLeaves}";
        //        LeaveStatusLabel.Text = "Leave request submitted successfully!";

        //        Response.Write("<script>alert('Leave Requested Successfully')</script>");
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log error for debugging
        //        LeaveStatusLabel.Text = $"Error: {ex.Message}";
        //        Response.Write($"<script>alert('Error: {ex.Message}')</script>");
        //    }
        //}

        //// Update the leave balance after the request is submitted
        ////private void UpdateLeaveBalance(string email, string leaveType, int leaveDays)
        ////{
        ////    string updateQuery = "UPDATE EmployeeLeaveBalances SET ";

        ////    // Update the balance based on the leave type
        ////    if (leaveType == "CL")
        ////    {
        ////        updateQuery += "CL = CL - @LeaveDays ";
        ////    }
        ////    else if (leaveType == "PL")
        ////    {
        ////        updateQuery += "PL = PL - @LeaveDays ";
        ////    }
        ////    else if (leaveType == "SL")
        ////    {
        ////        updateQuery += "SL = SL - @LeaveDays ";
        ////    }
        ////    else if (leaveType == "ML")
        ////    {
        ////        updateQuery += "ML = ML - @LeaveDays ";
        ////    }

        ////    updateQuery += "WHERE Email = @Email";

        ////    SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
        ////    updateCmd.Parameters.AddWithValue("@LeaveDays", leaveDays);
        ////    updateCmd.Parameters.AddWithValue("@Email", email);

        ////    updateCmd.ExecuteNonQuery();
        ////}

        //private void DisplayLeaveBalance()
        //{
        //    string email = Session["Email"].ToString();
        //    string query = "SELECT CL, PL, ML, SL FROM EmployeeLeaveBalances WHERE Email = @Email";

        //    SqlCommand cmd = new SqlCommand(query, conn);
        //    cmd.Parameters.AddWithValue("@Email", email);

        //    SqlDataReader reader = cmd.ExecuteReader();
        //    if (reader.Read())
        //    {
        //        Label2.Text = "CL: " + reader["CL"].ToString();
        //        Label3.Text = "PL: " + reader["PL"].ToString();
        //        Label4.Text = "ML: " + reader["ML"].ToString();
        //        Label5.Text = "SL: " + reader["SL"].ToString();
        //    }
        //    reader.Close();
        //}

        protected void SubmitLeaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                string leaveType = LeaveTypeDropDown.SelectedValue;
                DateTime startDate = DateTime.Parse(StartDateTextBox.Text);
                DateTime endDate = DateTime.Parse(EndDateTextBox.Text);
                string reason = ReasonTextBox.Text;
                string username = "user";
                string email = "user@example.com";

                if (startDate > endDate)
                {
                    LeaveStatusLabel.Text = "Invalid date range!";
                    return;
                }

                int leaveDays = 0;
                for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                    {
                        leaveDays++;
                    }
                }

                string cs = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    conn.Open();

                    // Retrieve leave balances
                    string query = "SELECT CL, PL, ML, SL FROM EmployeeLeaveBalances WHERE Email = @Email";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        SqlDataReader reader = cmd.ExecuteReader();

                        int availableCL = 0, availablePL = 0, availableML = 0, availableSL = 0;
                        if (reader.Read())
                        {
                            availableCL = Convert.ToInt32(reader["CL"]);
                            availablePL = Convert.ToInt32(reader["PL"]);
                            availableML = Convert.ToInt32(reader["ML"]);
                            availableSL = Convert.ToInt32(reader["SL"]);
                        }
                        reader.Close();

                        int maxLeaveDays = 0;
                        if (leaveType == "CL")
                        {
                            maxLeaveDays = 2;
                        }
                        else if (leaveType == "PL")
                        {
                            maxLeaveDays = 18;
                        }
                        else if (leaveType == "SL")
                        {
                            maxLeaveDays = 2;
                        }
                        else if (leaveType == "ML")
                        {
                            maxLeaveDays = 2;
                        }
                        else
                        {
                            maxLeaveDays = 0;
                        }


                        if (leaveDays > maxLeaveDays)
                        {
                            LeaveStatusLabel.Text = $"You can only request {maxLeaveDays} days for {leaveType}.";
                            Response.Write($"<script>alert('You are taking {leaveDays - maxLeaveDays} extra days for {leaveType} leave.');</script>");
                            string insertQuery = "exec sp_InsertLeaveRequest @Name, @Email, @LeaveType, @StartDate, @EndDate, @TotalDays, @Reason";
                            using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                            {
                                insertCmd.Parameters.AddWithValue("@Name", username);
                                insertCmd.Parameters.AddWithValue("@Email", email);
                                insertCmd.Parameters.AddWithValue("@LeaveType", leaveType);
                                insertCmd.Parameters.AddWithValue("@StartDate", startDate);
                                insertCmd.Parameters.AddWithValue("@EndDate", endDate);
                                insertCmd.Parameters.AddWithValue("@TotalDays", leaveDays);
                                insertCmd.Parameters.AddWithValue("@Reason", reason);
                                insertCmd.ExecuteNonQuery();
                            }

                            totalLeaves -= leaveDays;
                            Label1.Text = $"Remaining Leaves are: {totalLeaves}";
                            LeaveStatusLabel.Text = "Leave request submitted successfully!";
                            Response.Write("<script>alert('Leave Requested Successfully')</script>");
                        }

                        if (leaveDays > totalLeaves)
                        {
                            LeaveStatusLabel.Text = "Insufficient leave balance.";
                            return;
                        }

                        // Insert leave request
                        
                    }
                }
            }
            catch (Exception ex)
            {
                LeaveStatusLabel.Text = $"Error: {ex.Message}";
                Response.Write($"<script>alert('Error: {ex.Message}')</script>");
            }
        }


    }
}
