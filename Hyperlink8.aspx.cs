using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Drawing.Printing;

namespace WebApplication1
{
    public partial class Hyperlink8 : System.Web.UI.Page
    {
        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            // Validate and get the values from TextBoxes
            if (!ValidateInputs())
            {
                return; // Validation failed, exit the method
            }

            int studentId = Convert.ToInt32(studentIdTextBox.Text);
            int instructorId = Convert.ToInt32(instructorIdTextBox.Text);
            int courseId = Convert.ToInt32(courseIdTextBox.Text);
            string semesterCode = semesterCodeTextBox.Text;
            String semestercode = semesterCodeTextBox.Text;

            if ((semestercode.Length != 3) && (semestercode.Length != 5))
            {
                ShowAlert("Please enter a valid semester code");
                return;
            }

            int j = semestercode.Length;

            if (j == 3)
            {
                if ((semestercode[0] != 'W') && (semestercode[0] != 'S'))
                {
                    ShowAlert("Please enter a valid semester code");
                    return;
                }

                if (!((semestercode[1] >= '0' && semestercode[1] <= '9') && (semestercode[2] >= '0' && semestercode[2] <= '9')))
                {
                    ShowAlert("Please enter a valid semester code");
                    return;
                }
            }

            if (j == 5)
            {
                if (semestercode[0] != 'S')
                {
                    ShowAlert("Please enter a valid semester code");
                    return;
                }

                if (!((semestercode[1] >= '0' && semestercode[1] <= '9') && (semestercode[2] >= '0' && semestercode[2] <= '9')))
                {
                    ShowAlert("Please enter a valid semester code");
                    return;
                }

                if (semestercode[3] != 'R')
                {
                    ShowAlert("Please enter a valid semester code");
                    return;
                }

                if (!((semestercode[4] == '1') || (semestercode[4] == '2')))
                {
                    ShowAlert("Please enter a valid semester code");
                    return;
                }
            }

            // Call the method to handle the logic with these values
            bool success = CallAdminLinkStudent(studentId, instructorId, courseId, semesterCode);

            // Display success message and clear TextBoxes if the operation was successful
            if (success)
            {
                // Display a success message using JavaScript alert
                string script = "alert('Operation successful!');";
                ClientScript.RegisterStartupScript(this.GetType(), "SuccessScript", script, true);

                // Clear TextBox values
                studentIdTextBox.Text = "";
                instructorIdTextBox.Text = "";
                courseIdTextBox.Text = "";
                semesterCodeTextBox.Text = "";
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

            // Validate semesterCode (varchar)
            if (string.IsNullOrWhiteSpace(semesterCodeTextBox.Text))
            {
                ShowAlert("Please enter a valid Semester Code.");
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

        private bool CallAdminLinkStudent(int studentId, int instructorId, int courseId, string semesterCode)
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

   
                    string semesterCheckQuery = "SELECT COUNT(*) FROM Semester WHERE semester_code = @semester_code";

                    using (SqlCommand semesterCheckCommand = new SqlCommand(semesterCheckQuery, conn))
                    {
                        semesterCheckCommand.Parameters.AddWithValue("@semester_code", semesterCode);

                        int semesterCount = (int)semesterCheckCommand.ExecuteScalar();

                        if (semesterCount == 0)
                        {
                            // SemesterCode doesn't exist, show an alert
                            ShowAlert("The entered Semester Code does not exist. Please enter a valid Semester Code.");
                            return false;
                        }
                    }
                    string instructorCourseCheckQuery = "SELECT COUNT(*) FROM Instructor_Course WHERE instructor_id = @instructor_id AND course_id = @cours_id";

                    using (SqlCommand instructorCourseCheckCommand = new SqlCommand(instructorCourseCheckQuery, conn))
                    {
                        instructorCourseCheckCommand.Parameters.AddWithValue("@instructor_id", instructorId);
                        instructorCourseCheckCommand.Parameters.AddWithValue("@cours_id", courseId);

                        int instructorCourseCount = (int)instructorCourseCheckCommand.ExecuteScalar();

                        if (instructorCourseCount == 0)
                        {
                            // Instructor_Course record doesn't exist, show an alert
                            ShowAlert("The entered Instructor does not teach the specified Course. Please enter valid Instructor and Course IDs.");
                            return false;
                        }
                    }

                    // Proceed with the insertion as both StudentID and InstructorID exist
                    using (SqlCommand command = new SqlCommand("Procedures_AdminLinkStudent", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@cours_id", courseId);
                        command.Parameters.AddWithValue("@instructor_id", instructorId);
                        command.Parameters.AddWithValue("@studentID", studentId);
                        command.Parameters.AddWithValue("@semester_code", semesterCode);
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