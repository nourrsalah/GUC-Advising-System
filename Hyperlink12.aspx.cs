using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace WebApplication1
{
    public partial class Hyperlink12 : System.Web.UI.Page
    {
        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            // Validate and get the values from TextBoxes
            if (!ValidateInputs())
            {
                return; // Validation failed, exit the method
            }

            string semesterCodeToDelete = semesterTextBox.Text;

            // Validate if the semester code exists in the table
            if (SemesterCodeExists(semesterCodeToDelete))
            {
                // Call the method to handle the logic with these values
                bool success = CallAdminDeleteSlots(semesterCodeToDelete);

                // Display success message and clear TextBoxes if the operation was successful
                if (success)
                {
                    // Display a success message using JavaScript alert
                    string script = "alert('Operation successful!');";
                    ClientScript.RegisterStartupScript(this.GetType(), "SuccessScript", script, true);

                    // Clear TextBox values
                    semesterTextBox.Text = "";
                }
            }
            else
            {
                // Semester code does not exist, show an alert to the user
                ShowAlert("Semester code does not exist in the table. Please reenter.");
            }
        }

        private bool ValidateInputs()
        {
            // Validate semesterCode (varchar)
            if (string.IsNullOrWhiteSpace(semesterTextBox.Text))
            {
                ShowAlert("Please enter a valid semester code.");
                return false;
            }

            return true; // All validations passed
        }

        private void ShowAlert(string message)
        {
            // Display error message using JavaScript alert
            string script = $"alert('{message.Replace("'", "\\'")}');";
            ClientScript.RegisterStartupScript(this.GetType(), "AlertScript", script, true);
        }

        private bool SemesterCodeExists(string semesterCode)
        {
            try
            {
                string connStr = WebConfigurationManager.ConnectionStrings["Advising_System"].ToString();

                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Course_Semester WHERE semester_code = @current_semester", connection))
                    {
                        command.Parameters.AddWithValue("@current_semester", semesterCode);

                        connection.Open();
                        int count = (int)command.ExecuteScalar();

                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                return false; // Assume semester code does not exist in case of an exception
            }
        }

        private bool CallAdminDeleteSlots(string semesterCode)
        {
            try
            {
                string connStr = WebConfigurationManager.ConnectionStrings["Advising_System"].ToString();

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand("Procedures_AdminDeleteSlots", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add the parameter for semesterCode
                        command.Parameters.AddWithValue("@current_semester", semesterCode);

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
    }
}
