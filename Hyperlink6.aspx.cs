using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace WebApplication1
{
    public partial class Hyperlink6 : System.Web.UI.Page
    {
        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            // Validate and get the values from TextBoxes
            if (!ValidateInputs())
            {
                return; // Validation failed, exit the method
            }

            int instructorId = Convert.ToInt32(instructorIdTextBox.Text);
            int courseId = Convert.ToInt32(courseIdTextBox.Text);
            int slotId = Convert.ToInt32(slotIdTextBox.Text);

            // Call the method to handle the logic with these values
            bool success = CallAdminLinkInstructor(instructorId, courseId, slotId);

            // Display success message and clear TextBoxes if the operation was successful
            if (success)
            {
                // Display a success message using JavaScript alert
                string script = "alert('Operation successful!');";
                ClientScript.RegisterStartupScript(this.GetType(), "SuccessScript", script, true);

                // Clear TextBox values
                instructorIdTextBox.Text = "";
                courseIdTextBox.Text = "";
                slotIdTextBox.Text = "";
            }
        }

        private bool ValidateInputs()
        {
            // Validate instructorId (int)
            if (!int.TryParse(instructorIdTextBox.Text, out _))
            {
                ShowAlert("Please enter a valid Instructor ID (numeric value).");
                return false;
            }

            // Validate courseId (int)
            if (!int.TryParse(courseIdTextBox.Text, out _))
            {
                ShowAlert("Please enter a valid Course ID (numeric value).");
                return false;
            }

            // Validate slotId (int)
            if (!int.TryParse(slotIdTextBox.Text, out _))
            {
                ShowAlert("Please enter a valid Slot ID (numeric value).");
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

        private bool CallAdminLinkInstructor(int instructorId, int courseId, int slotId)
        {
            try
            {
                string connStr = WebConfigurationManager.ConnectionStrings["Advising_System"].ToString();

                // Validate instructorId
                if (!IsIdExists(connStr, "Instructor", "instructor_id", instructorId))
                {
                    ShowAlert("Instructor ID not found. Please enter a valid Instructor ID.");
                    return false;
                }

                // Validate courseId
                if (!IsIdExists(connStr, "Course", "course_id", courseId))
                {
                    ShowAlert("Course ID not found. Please enter a valid Course ID.");
                    return false;
                }

                // Validate slotId
                if (!IsIdExists(connStr, "Slot", "slot_id", slotId))
                {
                    ShowAlert("Slot ID not found. Please enter a valid Slot ID.");
                    return false;
                }

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand("Procedures_AdminLinkInstructor", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@instructor_id", instructorId);
                        command.Parameters.AddWithValue("@cours_id", courseId);
                        command.Parameters.AddWithValue("@slot_id", slotId);

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
                // Log or handle the exception
                Console.WriteLine($"Error: {ex.Message}");

                // Operation failed
                return false;
            }
        }
        private bool IsIdExists(string connectionString, string tableName, string idColumnName, int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = $"SELECT COUNT(*) FROM {tableName} WHERE {idColumnName} = @Id";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    int count = (int)command.ExecuteScalar();

                    return count > 0;
                }
            }
        }
        protected void btnRedirect_Click(object sender, EventArgs e)
        {
            // Redirect to the AllFunctionalities page
            Response.Redirect("AllFunctionalities.aspx");
        }

    }
}
