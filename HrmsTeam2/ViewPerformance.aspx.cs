using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HrmsTeam2
{
    public partial class ViewPerformance : System.Web.UI.Page
    {
        private SqlConnection conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Initialize database connection
            string cs = ConfigurationManager.ConnectionStrings["DBconn"].ConnectionString;
            conn = new SqlConnection(cs);
            conn.Open();

            if (!IsPostBack)
            {
                BindDropdown();
                // Initialize GridView with empty data or a message
                DisplayEmptyGrid();
            }
        }

        // Method to display an empty grid with a message
        private void DisplayEmptyGrid()
        {
            DataTable dt = new DataTable();
            // Create the same columns that would be in our actual data
            dt.Columns.Add("Q_Text");
            dt.Columns.Add("answerText");
            dt.Columns.Add("SubmittedDate");

            // The GridView will show the EmptyDataText message since there are no rows
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        private void BindDropdown()
        {
            using (SqlCommand cmd = new SqlCommand("SELECT UserId, UserName FROM UserRegistration WHERE UserStatus = 'Active'", conn))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                DropDownList1.DataSource = reader;
                DropDownList1.DataTextField = "UserName";
                DropDownList1.DataValueField = "UserId";
                DropDownList1.DataBind();

                // Add a prompt item instead of "All Users"
                DropDownList1.Items.Insert(0, new ListItem("-- Select User --", "-1"));
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedUserId = DropDownList1.SelectedValue;

            // Only load data if a specific user is selected
            if (selectedUserId != "-1")
            {
                LoadPerformanceData(selectedUserId);
            }
            else
            {
                // If the prompt is selected, show empty grid
                DisplayEmptyGrid();
            }
        }

        private void LoadPerformanceData(string userId)
        {
            // Now we only need one query since we're always loading a specific user
            string query = @"SELECT q.Q_Text, pa.answerText, pa.SubmittedDate
                        FROM PerformanceAnswer pa
                        INNER JOIN PerformanceQuestion q ON pa.qid = q.Q_id
                        WHERE pa.userid = @UserId
                        ORDER BY pa.SubmittedDate DESC";

            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@UserId", userId);
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        // Clean up resources
        protected override void OnUnload(EventArgs e)
        {
            if (conn != null && conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            base.OnUnload(e);
        }
    }
}