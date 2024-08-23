using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void LoginButton_Click(object sender, EventArgs e)
        {
            // Hardcoded username and password for demonstration purposes
            string validUsername = "Omar";
            string validPassword = "Admin1";

            // Get entered username and password
            string enteredUsername = username.Text;
            string enteredPassword = password.Text;

            // Check if entered credentials match the valid credentials
            if (enteredUsername == validUsername && enteredPassword == validPassword)
            {
                // Display a success message if credentials are valid
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Login successful!');", true);
                Response.Redirect("AllFunctionalities.aspx");
            }
            else
            {
                // Display an error message if credentials are invalid
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Invalid username or password. Please try again.');", true);
            }
        }
    }
}