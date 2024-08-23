using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace WebApplication1
{
    public partial class Hyperlink13 : System.Web.UI.Page
    {
        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            // Get the values from TextBoxes
            string type = typeTextBox.Text;
            string dateTimeText = dateTimeTextBox.Text;
            string courseIDText = courseIDTextBox.Text;

            // Validate date format
            if (!DateTime.TryParse(dateTimeText, out DateTime dateTime))
            {
                // Display error message for incorrect date format
                ShowAlert("Invalid date format. Please enter a valid date and time.");
                return;
            }

            // Validate course ID format
            if (!int.TryParse(courseIDText, out int courseID))
            {
                ShowAlert("Please enter a valid Course ID.");
                return;
            }

            // Check if the entered courseID exists in the Course table
            if (!CourseExists(courseID))
            {
                ShowAlert("Course ID does not exist. Please enter a valid Course ID.");
                return;
            }

            // Additional validation for the exam type (assuming "Normal", "First_makeup", "2nd_makeup" are valid)
            string[] validExamTypes = { "Normal", "First MakeUp", "Second MakeUp" };
            if (!Array.Exists(validExamTypes, t => t.Equals(type, StringComparison.OrdinalIgnoreCase)))
            {
                ShowAlert("Invalid exam type. Please enter a valid exam type.");
                return;
            }

            // Call the method to handle the logic with these values
            bool success = CallProcedures_AdminAddExam(type, dateTime, courseID);

            // Display success message and clear TextBoxes if the operation was successful
            if (success)
            {
                // Display a success message using JavaScript alert
                ShowAlert("Operation successful!");

                // Clear TextBox values
                typeTextBox.Text = "";
                dateTimeTextBox.Text = "";
                courseIDTextBox.Text = "";
            }
        }

        private bool CallProcedures_AdminAddExam(string type, DateTime dateTime, int courseID)
        {
            try
            {
                string connStr = WebConfigurationManager.ConnectionStrings["Advising_System"].ToString();

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand("Procedures_AdminAddExam", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@Type", type);
                        command.Parameters.AddWithValue("@date", dateTime);
                        command.Parameters.AddWithValue("@courseID", courseID);

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

        protected void btnRedirect_Click(object sender, EventArgs e)
        {
            // Redirect to the AllFunctionalities page
            Response.Redirect("AllFunctionalities.aspx");
        }

        // Function to check if the courseID exists in the Course table
        private bool CourseExists(int courseID)
        {
            try
            {
                string connStr = WebConfigurationManager.ConnectionStrings["Advising_System"].ToString();

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM course WHERE course_id = @courseID", conn))
                    {
                        command.Parameters.AddWithValue("@courseID", courseID);
                        conn.Open();

                        int count = (int)command.ExecuteScalar();

                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception if needed

                // Assume the course exists to avoid blocking the user due to an exception
                return true;
            }
        }
    }
}
