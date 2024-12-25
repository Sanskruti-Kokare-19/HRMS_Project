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
    public partial class UserProfile : System.Web.UI.Page
    {
        SqlConnection conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            conn = new SqlConnection(cs);
            conn.Open();
            if (!IsPostBack)
            {  
                LoadNotifications();
            }
        }
        private void LoadNotifications()
        {
            string cs = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            conn = new SqlConnection(cs);

            string userEmail = "FetchLoggedInUserEmail"; // Replace with user session or login context
            SqlCommand cmd = new SqlCommand(
                    "SELECT NotificationMessage, NotificationDate, NotificationTime FROM Notification WHERE Email = @Email",
                    conn
                );
            cmd.Parameters.AddWithValue("@Email", userEmail);

            conn.Open(); // Open the connection before executing the command
            SqlDataReader reader = cmd.ExecuteReader();

            
            notificationRepeater.DataSource = reader;
            notificationRepeater.DataBind();
        }
    }
}