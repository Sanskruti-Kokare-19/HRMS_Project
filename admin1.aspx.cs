using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AttendanceTask5
{
    public partial class admin1 : System.Web.UI.Page
    {
        SqlConnection conn;

        protected void Page_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
            conn.Open();

            if (!IsPostBack)
            {
                LoadAttendanceData();
            }
        }

        private void LoadAttendanceData()
        {
            // Fetch daily attendance for the current month
            string dailyQuery = @"
        SELECT 
            Name,
            DAY(Date) AS DayOfMonth,
            Status
        FROM 
            CheckInCheckOut
        WHERE 
            MONTH(Date) = MONTH(GETDATE()) AND YEAR(Date) = YEAR(GETDATE())";

            DataTable rawData = new DataTable();
            using (SqlCommand cmd = new SqlCommand(dailyQuery, conn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(rawData);
            }

            // Fetch monthly attendance using stored procedure
            DataTable monthlyData = new DataTable();
            using (SqlCommand cmd = new SqlCommand("GetMonthlyAttendance", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(monthlyData);
            }

            // Prepare daily attendance table
            DataTable attendanceTable = new DataTable();
            attendanceTable.Columns.Add("Name");

            // Add columns for each day of the month
            int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            for (int i = 1; i <= daysInMonth; i++)
            {
                attendanceTable.Columns.Add(i.ToString());
            }

            // Add monthly summary columns
            attendanceTable.Columns.Add("PresentDays");
            attendanceTable.Columns.Add("AbsentDays");
            attendanceTable.Columns.Add("HalfDays");

            // Populate daily attendance
            var userGroups = rawData.AsEnumerable().GroupBy(row => row.Field<string>("Name"));
            foreach (var group in userGroups)
            {
                DataRow newRow = attendanceTable.NewRow();
                newRow["Name"] = group.Key;

                foreach (var record in group)
                {
                    int day = record.Field<int>("DayOfMonth");
                    newRow[day.ToString()] = record.Field<string>("Status");
                }

                // Add monthly summary
                DataRow monthlyRecord = monthlyData.AsEnumerable()
                                                   .FirstOrDefault(row => row.Field<string>("Name") == group.Key);
                if (monthlyRecord != null)
                {
                    newRow["PresentDays"] = monthlyRecord["PresentDays"];
                    newRow["AbsentDays"] = monthlyRecord["AbsentDays"];
                    newRow["HalfDays"] = monthlyRecord["HalfDays"];
                }

                attendanceTable.Rows.Add(newRow);
            }

            // Bind data to GridView
            GridView1.DataSource = attendanceTable;
            GridView1.DataBind();
        }
    }
    }