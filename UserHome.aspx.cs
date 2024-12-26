using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AttendanceTask5
{
    public partial class UserHome : System.Web.UI.Page
    {
        SqlConnection conn;

        protected void Page_Load(object sender, EventArgs e)

        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
            conn.Open();
            if (!IsPostBack)
            {
                
                Button2.Enabled = false;
                DateTime currentDateTime = DateTime.Now;
                string username = Session["MyUser"].ToString();
                string checkQuery = "SELECT COUNT(*) FROM CheckInCheckOut WHERE Name = @Name AND Date = @Date";
                using (SqlCommand cmd = new SqlCommand(checkQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", username);
                    cmd.Parameters.AddWithValue("@Date", currentDateTime.Date);

                    int count = (int)cmd.ExecuteScalar();

                    //if (currentDateTime.Hour > 12 && count == 0)
                    //{
                    //    Button1.Enabled = true;
                    //}
                    //else
                    //{
                    //    Button1.Enabled = false;
                    //}
                }
            }
        
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string username = Session["MyUser"].ToString();
            DateTime currentDateTime = DateTime.Now;

            
            string insertQuery = "INSERT INTO CheckInCheckOut VALUES (@Name, @CheckIn, @CheckOut, @TotalHours, @Status, @Date)";
            using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
            {
                cmd.Parameters.AddWithValue("@Name", username);
                cmd.Parameters.AddWithValue("@CheckIn", currentDateTime.TimeOfDay);
                cmd.Parameters.AddWithValue("@CheckOut", DBNull.Value);
                cmd.Parameters.AddWithValue("@TotalHours", DBNull.Value);
                cmd.Parameters.AddWithValue("@Status", "Absent");
                cmd.Parameters.AddWithValue("@Date", currentDateTime.Date);
                cmd.ExecuteNonQuery();
            }

            
            Button1.Enabled = false;
            Button2.Enabled = true;

            
            refreshgridview(username);
        }


        protected void Button2_Click(object sender, EventArgs e)
        {
            string username = Session["MyUser"].ToString();
            DateTime currentDateTime = DateTime.Now;

            string getCheckInQuery = "SELECT CheckIn FROM CheckInCheckOut WHERE Name = @Name AND Date = @Date";
            TimeSpan checkInTime;

            using (SqlCommand cmd = new SqlCommand(getCheckInQuery, conn))
            {
                cmd.Parameters.AddWithValue("@Name", username);
                cmd.Parameters.AddWithValue("@Date", currentDateTime.Date);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    checkInTime = (TimeSpan)reader["CheckIn"];
                }
            }

            TimeSpan checkOutTime = currentDateTime.TimeOfDay;
            double totalHours = (checkOutTime - checkInTime).TotalHours;
            string status = totalHours < 4 ? "Half Day" : "Present";

            string updateQuery = "UPDATE CheckInCheckOut SET CheckOut = @CheckOut, TotalHours = @TotalHours, Status = @Status WHERE Name = @Name AND Date = @Date";
            using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
            {
                cmd.Parameters.AddWithValue("@CheckOut", checkOutTime);
                cmd.Parameters.AddWithValue("@TotalHours", totalHours);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@Name", username);
                cmd.Parameters.AddWithValue("@Date", currentDateTime.Date);
                cmd.ExecuteNonQuery();
            }

            
            

            string Query = $"SELECT * FROM CheckInCheckOut WHERE Name ='{username}'";
            SqlCommand cmd1 = new SqlCommand(Query, conn);
            SqlDataReader reader1 = cmd1.ExecuteReader();
            GridView1.DataSource = reader1;
            GridView1.DataBind();
            Button2.Enabled = false;
            Button1.Enabled = false;

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            
            DateTime fromDate=DateTime.Parse(TextBox1.Text);
            DateTime toDate= DateTime.Parse(TextBox2.Text);


           
                
                string filterQuery = "SELECT * FROM CheckInCheckOut WHERE Date BETWEEN @FromDate AND @ToDate";

                using (SqlCommand cmd = new SqlCommand(filterQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@FromDate", fromDate); 
                    cmd.Parameters.AddWithValue("@ToDate", toDate); 

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        GridView1.DataSource = reader; 
                        GridView1.DataBind(); 
                    }
                }
            
           
        }
        private void refreshgridview(string username)
        {
            string selectQuery = "SELECT * FROM CheckInCheckOut WHERE Name = @Name";
            using (SqlCommand cmd = new SqlCommand(selectQuery, conn))
            {
                cmd.Parameters.AddWithValue("@Name", username);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
        }

    }
}
