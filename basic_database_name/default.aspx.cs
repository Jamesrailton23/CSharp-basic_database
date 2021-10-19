using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace basic_database_name
{
    public partial class _default : System.Web.UI.Page
    {
        // Establised the connection to the SQL database 
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            // Open's the connection to the database 
            con.Open(); 
        }

        protected void button_enter_data_Click(object sender, EventArgs e)
        {
            // inputs the data into the database from the textbox's 
            SqlCommand cmd = new SqlCommand("insert into utbl values('" + tb_username.Text + "','" + tb_email.Text + "','" + tb_age.Text + "','" + tb_password.Text + "')",con);
           
           // Submits the data into the database 
            cmd.ExecuteNonQuery();
           
            // Closes the database 
            con.Close();
           
            // shows the data within the gridview database 
            gridview_data.DataBind();
            
        }
    }
}