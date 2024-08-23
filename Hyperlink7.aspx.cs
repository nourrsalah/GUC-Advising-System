using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Hyperlink7 : System.Web.UI.Page
    {
        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            // Validate and get the values from TextBoxes
            if (!ValidateInputs())
            {
                return; // Validation failed, exit the method
            }

            int studentId = Convert.ToInt32(studentIdTextBox.Text);
            int advisorId = Convert.ToInt32(advisorIdTextBox.Text);

            // Call the method to handle the logic with these values
            bool success = CallAdminLinkStudentToAdvisor(studentId, advisorId);

            // Display success message and clear TextBoxes if the operation was successful
            if (success)
            {
                // Display a success message using JavaScript alert
                string script = "alert('Operation successful!');";
                ClientScript.RegisterStartupScript(this.GetType(), "SuccessScript", script, true);

                // Clear TextBox values
                studentIdTextBox.Text = "";
                advisorIdTextBox.Text = "";
            }
        }

        private bool ValidateInputs()
        {
            // Validate studentId (int)
            if (!int.TryParse(studentIdTextBox.Text, out _))
            {
                ShowAlert("Please enter a valid Student ID (numeric value).");
                return false;
            }

            // Validate advisorId (int)
            if (!int.TryParse(advisorIdTextBox.Text, out _))
            {
                ShowAlert("Please enter a valid Advisor ID (numeric value).");
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

        private bool CallAdminLinkStudentToAdvisor(int studentId, int advisorId)
        {
            try
            {
                string connStr = WebConfigurationManager.ConnectionStrings["Advising_System"].ToString();

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    // Check if the studentId exists in the Student table
                    string studentCheckQuery = "SELECT COUNT(*) FROM Student WHERE student_id = @studentID";

                    using (SqlCommand studentCheckCommand = new SqlCommand(studentCheckQuery, conn))
                    {
                        studentCheckCommand.Parameters.AddWithValue("@studentID", studentId);

                        conn.Open();

                        int studentCount = (int)studentCheckCommand.ExecuteScalar();

                        if (studentCount == 0)
                        {
                            // StudentID doesn't exist, show an alert
                            ShowAlert("The entered Student ID does not exist. Please enter a valid Student ID.");
                            return false;
                        }
                    }

                    // Check if the advisorId exists in the Advisor table
                    string advisorCheckQuery = "SELECT COUNT(*) FROM Advisor WHERE advisor_id = @advisorID";

                    using (SqlCommand advisorCheckCommand = new SqlCommand(advisorCheckQuery, conn))
                    {
                        advisorCheckCommand.Parameters.AddWithValue("@advisorID", advisorId);

                        int advisorCount = (int)advisorCheckCommand.ExecuteScalar();

                        if (advisorCount == 0)
                        {
                            // AdvisorID doesn't exist, show an alert
                            ShowAlert("The entered Advisor ID does not exist. Please enter a valid Advisor ID.");
                            return false;
                        }
                    }

                    // Proceed with the insertion as both StudentID and AdvisorID exist
                    using (SqlCommand command = new SqlCommand("Procedures_AdminLinkStudentToAdvisor", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@studentID", studentId);
                        command.Parameters.AddWithValue("@advisorID", advisorId);

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
        protected void btnRedirect_Click(object sender, EventArgs e)
        {
            // Redirect to the AllFunctionalities page
            Response.Redirect("AllFunctionalities.aspx");
        }

    }
}
