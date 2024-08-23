using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace WebApplication1
{
    public partial class Hyperlink5 : System.Web.UI.Page
    {
        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            // Validate and get the values from TextBoxes
            if (!ValidateInputs())
            {
                return; // Validation failed, exit the method
            }

            string major = majorTextBox.Text;
            int semester = Convert.ToInt32(semesterTextBox.Text);
            int creditHours = Convert.ToInt32(creditHoursTextBox.Text);
            string courseName = courseNameTextBox.Text;
            bool isOffered = Convert.ToInt32(isOfferedTextBox.Text) == 1;

            // Call the method to handle the logic with these values
            bool success = CallAdminAddingCourse(major, semester, creditHours, courseName, isOffered);

            // Display success message and clear TextBoxes if the operation was successful
            if (success)
            {
                // Display a success message using JavaScript alert
                string script = "alert('Operation successful!');";
                ClientScript.RegisterStartupScript(this.GetType(), "SuccessScript", script, true);

                // Clear TextBox values
                majorTextBox.Text = "";
                semesterTextBox.Text = "";
                creditHoursTextBox.Text = "";
                courseNameTextBox.Text = "";
                isOfferedTextBox.Text = "";
            }
        }

        private bool ValidateInputs()
        {
            // Validate major (varchar)
            if (string.IsNullOrWhiteSpace(majorTextBox.Text))
            {
                ShowAlert("Please enter a valid major.");
                return false;
            }

            // Validate semester (int)
            if (!int.TryParse(semesterTextBox.Text, out _))
            {
                ShowAlert("Please enter a valid semester (numeric value).");
                return false;
            }

            // Validate credit hours (int)
            if (!int.TryParse(creditHoursTextBox.Text, out _))
            {
                ShowAlert("Please enter a valid credit hours (numeric value).");
                return false;
            }

            // Validate course name (varchar)
            if (string.IsNullOrWhiteSpace(courseNameTextBox.Text))
            {
                ShowAlert("Please enter a valid course name.");
                return false;
            }

            // Validate isOffered (bit)
            int isOfferedValue;
            if (!int.TryParse(isOfferedTextBox.Text, out isOfferedValue) || (isOfferedValue != 0 && isOfferedValue != 1))
            {
                ShowAlert("Please enter a valid value for isOffered (0 or 1).");
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

        private bool CallAdminAddingCourse(string major, int semester, int creditHours, string courseName, bool isOffered)
        {
            try
            {
                string connStr = WebConfigurationManager.ConnectionStrings["Advising_System"].ToString();

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand("Procedures_AdminAddingCourse", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@major", major);
                        command.Parameters.AddWithValue("@semester", semester);
                        command.Parameters.AddWithValue("@credit_hours", creditHours);
                        command.Parameters.AddWithValue("@name", courseName);
                        command.Parameters.AddWithValue("@is_offered", isOffered);

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
        protected void btnRedirect_Click(object sender, EventArgs e)
        {
            // Redirect to the AllFunctionalities page
            Response.Redirect("AllFunctionalities.aspx");
        }
    }
}
