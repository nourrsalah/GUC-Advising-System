using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace WebApplication1
{
    public partial class Hyperlink11 : System.Web.UI.Page
    {
        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            // Validate and get the values from TextBoxes
            if (!ValidateInputs())
            {
                return; // Validation failed, exit the method
            }

            int courseIdToDelete = Convert.ToInt32(courseIdTextBox.Text);

            // Validate if the CourseId exists in the table
            if (CourseExists(courseIdToDelete))
            {
                // Call the method to handle the logic with these values
                bool success = CallAdminDeletingCourse(courseIdToDelete);

                // Display success message and clear TextBoxes if the operation was successful
                if (success)
                {
                    // Display a success message using JavaScript alert
                    string script = "alert('Operation successful!');";
                    ClientScript.RegisterStartupScript(this.GetType(), "SuccessScript", script, true);

                    // Clear TextBox values
                    courseIdTextBox.Text = "";
                }
            }
            else
            {
                // CourseId does not exist, show an alert to the user
                ShowAlert("CourseId does not exist in the Course table. Please reenter.");
            }
        }

        private bool ValidateInputs()
        {
            // Validate courseId (int)
            if (!int.TryParse(courseIdTextBox.Text, out _))
            {
                ShowAlert("Please enter a valid CourseId (numeric value).");
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

        private bool CourseExists(int courseId)
        {
            try
            {
                string connStr = WebConfigurationManager.ConnectionStrings["Advising_System"].ToString();

                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Course WHERE course_id = @courseID", connection))
                    {
                        command.Parameters.AddWithValue("@courseID", courseId);

                        connection.Open();
                        int count = (int)command.ExecuteScalar();

                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                return false; // Assume courseId does not exist in case of an exception
            }
        }

        private bool CallAdminDeletingCourse(int courseId)
        {
            try
            {
                string connStr = WebConfigurationManager.ConnectionStrings["Advising_System"].ToString();

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand("Procedures_AdminDeleteCourse", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add the parameter for courseId
                        command.Parameters.AddWithValue("@courseID", courseId);

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
