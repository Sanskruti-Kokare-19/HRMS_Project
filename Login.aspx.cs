using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRMS
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Clear the session on load
                Session.Clear();
            }
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            // Hardcoded credentials for demonstration
            string username = "user";
            string password = "sans";
            string email = "user@example.com";

            string enteredEmail = EmailTextBox.Text;

            if (UsernameTextBox.Text == username && PasswordTextBox.Text == password && enteredEmail == email)
            {
                Session["Username"] = username;
                Session["Email"] = enteredEmail;

                
                Response.Redirect("WebForm1.aspx");
            }
            else
            {
                ErrorLabel.Text = "Invalid username, password, or email!";
            }
        }
    }
}