using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace WebApplication1
{
    public partial class Hyperlink16 : System.Web.UI.Page
    {
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Get the StudentID from the TextBox
            if (!int.TryParse(txtStudentID.Text, out int studentID))
            {
                // Display an alert if the entered StudentID is not a valid integer
                ShowAlert("Please enter a valid Student ID.");
                return;
            }

            // Call the method to handle the logic with the StudentID
            bool success = CallAdminUpdateStudentStatus(studentID);

            // Display success message and clear TextBox if the operation was successful
            if (success)
            {
                // Display a success message using JavaScript alert
                ShowAlert("Operation successful!");

                // Clear TextBox value
                txtStudentID.Text = "";
            }
            else
            {
                Response.Write("Error occured");

            }
        }

        private bool CallAdminUpdateStudentStatus(int studentID)
        {
            try
            {
                string connStr = WebConfigurationManager.ConnectionStrings["Advising_System"].ToString();

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand("Procedures_AdminUpdateStudentStatus", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@student_id", studentID);

                        conn.Open();

                        // Execute the stored procedure
                        command.ExecuteNonQuery();
                    }
                }

                // Operation was successful
                return true;
            }
            catch (Exception ex)
            {
                // Log or handle the exception if needed

                // Operation failed
                return false;
            }
            
        }

        private void ShowAlert(string message)
        {
            // Display error message using JavaScript alert
            string script = $"alert('{message.Replace("'", "\\'")}');";
            ClientScript.RegisterStartupScript(this.GetType(), "AlertScript", script, true);
        }
    }
}
