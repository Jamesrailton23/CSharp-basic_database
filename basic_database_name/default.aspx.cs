using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//Added using statements below
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

/*
the lines of code

using System.Data.SqlClient;
using System.Configuration;
using System.Data;

have been added to allow the Sql database to work correctly
 */

namespace basic_database_name
{
    public partial class _default : System.Web.UI.Page
    {
        // Established the connection to the SQL database
        private SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString);

        // Global variables

        private int age;
        private string username;
        private string email;
        private string password;
        private string find;
        private SqlCommand database_cmd;
        private SqlDataAdapter adapter;
        private DataSet dataset_1;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Opens the connection to the database
            connect.Open();
        }

        protected void button_enter_data_Click(object sender, EventArgs e)
        {
            // Method calling database
            Sql_database();

            tb_username.Text = ""; tb_age.Text = "";
            tb_password.Text = ""; tb_email.Text = "";
        }

        protected void Sql_database()
        {
            // Inputs the data into the database from the text box
            database_cmd = new SqlCommand("insert into utbl values('" + username_method() + "','" + email_method() + "','" + user_age_method() + "','" + password_method() + "')", connect);

            // Submits the data into the database
            database_cmd.ExecuteNonQuery();

            // Closes the database
            connect.Close();

            // Shows the data within the grid view database
            gridview_data.DataBind();
        }

        protected void button_delete_data_Click(object sender, EventArgs e)
        {
            // Deletes the whole database item, located from the user name textbox

            database_cmd = new SqlCommand("delete from utbl where Username='" + tb_delete_data.Text + "'", connect);
            database_cmd.ExecuteNonQuery();
            connect.Close();
            gridview_data.DataBind();
        }

        protected void button_edit_data_Click(object sender, EventArgs e)
        {
            // Edits the database input of email and age located via the username texbox
            database_cmd = new SqlCommand("Update utbl set Email='" + tb_email.Text + "', Age='" + tb_age.Text + "', Password='" + tb_password.Text + "' where Username='" + tb_username.Text + "'", connect);

            database_cmd.ExecuteNonQuery();
            connect.Close();
            gridview_data.DataBind();
        }

        protected void button_data_search_Click(object sender, EventArgs e)
        {
            // Finds the input from the search texbox according to the username, then it changes the data table to only show that username

            find = "select * from utbl where (Username like '%' +@Username+ '%')";
            database_cmd = new SqlCommand(find, connect);
            database_cmd.Parameters.Add("@Username", SqlDbType.NVarChar).Value = tb_srh.Text;
            database_cmd.ExecuteNonQuery();

            adapter = new SqlDataAdapter();
            adapter.SelectCommand = database_cmd;
            dataset_1 = new DataSet();
            adapter.Fill(dataset_1, "Username");

            gridview_data.DataSourceID = null;
            gridview_data.DataSource = dataset_1;
            gridview_data.DataBind();

            tb_srh.Text = "";

            connect.Close();
        }

        protected string username_method()
        {
            //Returns the username from texbox
            username = tb_username.Text;
            return username;
        }

        protected string email_method()
        {
            //Returns the Email from texbox
            email = tb_email.Text;
            return email;
        }

        protected int user_age_method()
        {
            //Returns the user age from texbox
            age = int.Parse(tb_age.Text);
            return age;
        }

        protected string password_method()
        {
            //Returns the password from texbox
            password = tb_password.Text;
            return password;
        }
    }
}