using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HrmsTeam2
{
    public partial class AdminHome : System.Web.UI.Page
    {
        SqlConnection conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["DBconn"].ConnectionString;
            conn = new SqlConnection(cs);
            conn.Open();

            if(!IsPostBack)
            {
                BindGridView();
            }
        }

        private void BindGridView()
        {
            string user = "User";

            string qf = $"exec FetchUserInfo '{user}'";
            SqlCommand cmd = new SqlCommand(qf, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();   
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            BindGridView();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            BindGridView();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int userId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);            
            string userStatus = ((DropDownList)GridView1.Rows[e.RowIndex].FindControl("DropDownList1")).SelectedValue;

            string qu = $"exec UpdateUser '{userId}', '{userStatus}'";
            SqlCommand cmd = new SqlCommand(qu, conn);
            cmd.ExecuteNonQuery();
           
            GridView1.EditIndex = -1;
            BindGridView();
        }

    }
}