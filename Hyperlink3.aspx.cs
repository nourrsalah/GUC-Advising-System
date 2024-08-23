using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

namespace WebApplication1
{
    public partial class Hyperlink3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["Advising_System"].ToString();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                // Specify the name of your view in the SELECT statement
                string query = "SELECT * FROM all_Pending_Requests";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    conn.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Create HtmlTable control
                        // Create HtmlTable control
                        HtmlTable htmlTable = new HtmlTable();
                        htmlTable.Attributes["border"] = "1";  // Set border attribute for the whole table

                        // Create table header row
                        HtmlTableRow headerRow = new HtmlTableRow();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            HtmlTableCell cell = new HtmlTableCell();
                            cell.InnerHtml = reader.GetName(i);
                            cell.Attributes["style"] = "border: 1px solid black;";  // Set border style for each cell
                            headerRow.Cells.Add(cell);
                        }
                        htmlTable.Rows.Add(headerRow);

                        // Create table data rows
                        while (reader.Read())
                        {
                            HtmlTableRow dataRow = new HtmlTableRow();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                HtmlTableCell cell = new HtmlTableCell();
                                cell.InnerHtml = reader[i].ToString();
                                cell.Attributes["style"] = "border: 1px solid black;";  // Set border style for each cell
                                dataRow.Cells.Add(cell);
                            }
                            htmlTable.Rows.Add(dataRow);
                        }

                        // Add the HtmlTable to your form
                        form1.Controls.Add(htmlTable);

                    }
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