using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace WebApplication1
{
    public partial class Hyperlink15 : System.Web.UI.Page
    {
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Get the PaymentID from the TextBox
            if (!int.TryParse(txtPaymentID.Text, out int paymentID))
            {
                // Display an alert if the entered PaymentID is not a valid integer
                ShowAlert("Please enter a valid Payment ID.");
                return;
            }

            // Check if the entered paymentID exists in the Payment table
            if (!PaymentExists(paymentID))
            {
                ShowAlert("Payment ID does not exist. Please enter a valid Payment ID.");
                return;
            }

            // Call the method to handle the logic with the PaymentID
            bool success = CallAdminIssueInstallment(paymentID);

            // Display success message and clear TextBox if the operation was successful
            if (success)
            {
                // Display a success message using JavaScript alert
                ShowAlert("Operation successful!");

                // Clear TextBox value
                txtPaymentID.Text = "";
            }
        }

        private bool CallAdminIssueInstallment(int paymentID)
        {
            try
            {
                string connStr = WebConfigurationManager.ConnectionStrings["Advising_System"].ToString();

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand("Procedures_AdminIssueInstallment", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@payment_id", paymentID);

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

        private bool PaymentExists(int paymentID)
        {
            try
            {
                // Your connection string
                string connStr = WebConfigurationManager.ConnectionStrings["Advising_System"].ToString();

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Payment WHERE payment_id = @payment_id", conn))
                    {
                        command.Parameters.AddWithValue("@payment_id", paymentID);
                        conn.Open();

                        int count = (int)command.ExecuteScalar();

                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception if needed

                // Assume the payment exists to avoid blocking the user due to an exception
                return true;
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
