using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace WebApplication1
{
    public partial class Hyperlink4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if there are TextBox values in the ViewState (after button click)
            if (ViewState["StartDate"] != null && ViewState["EndDate"] != null && ViewState["SemesterCode"] != null)
            {
                // Use values from ViewState to call the stored procedure
                string startDate = ViewState["StartDate"].ToString();
                string endDate = ViewState["EndDate"].ToString();
                string semesterCode = ViewState["SemesterCode"].ToString();

                // Call the stored procedure without displaying the result
                CallAdminAddingSemester(startDate, endDate, semesterCode);
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            // Get the values from TextBoxes
            string startDateText = startDateTextBox.Text;
            string endDateText = endDateTextBox.Text;
            string semesterCode = semesterCodeTextBox.Text;

            // Validate date format
            if (!DateTime.TryParse(startDateText, out DateTime startDate) || !DateTime.TryParse(endDateText, out DateTime endDate))
            {
                // Display error message for incorrect date format
                ClientScript.RegisterStartupScript(this.GetType(), "ErrorScript", "alert('Invalid date format. Please enter dates in the correct format.');", true);
                return;
            }

            // Validate semester code
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

            // Function to show JavaScript alert
            void ShowAlert(string message)
            {
                string script = $"alert('{message.Replace("'", "\\'")}');";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "AlertScript", script, true);
            }


            // Call the method to handle the logic with these values
            bool success = CallAdminAddingSemester(startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"), semestercode);

            // Display success message and clear TextBoxes if the operation was successful
            if (success)
            {
                // Display a success message using JavaScript alert
                string script = "alert('Operation successful!');";
                ClientScript.RegisterStartupScript(this.GetType(), "SuccessScript", script, true);

                // Clear TextBox values
                startDateTextBox.Text = "";
                endDateTextBox.Text = "";
                semesterCodeTextBox.Text = "";
            }
        }


        private bool CallAdminAddingSemester(string startDate, string endDate, string semesterCode)
        {
            try
            {
                string connStr = WebConfigurationManager.ConnectionStrings["Advising_System"].ToString();

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand("AdminAddingSemester", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@start_date", startDate);
                        command.Parameters.AddWithValue("@end_date", endDate);
                        command.Parameters.AddWithValue("@semester_code", semesterCode);

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
