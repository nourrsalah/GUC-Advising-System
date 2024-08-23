using Microsoft.SqlServer.Server;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI.HtmlControls;

namespace WebApplication1
{
    public partial class Hyperlink14 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["Advising_System"].ToString();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                // Replace YourViewName with the actual name of your view
                string query = "SELECT * FROM Student_Payment";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    conn.Open();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Create HtmlTable control
                        HtmlTable htmlTable = new HtmlTable();

                        // Add visible borders to the table
                        htmlTable.Attributes["border"] = "1";

                        // Create table header row
                        HtmlTableRow headerRow = new HtmlTableRow();
                        foreach (DataColumn column in dataTable.Columns)
                        {
                            HtmlTableCell cell = new HtmlTableCell();
                            cell.InnerHtml = column.ColumnName;
                            headerRow.Cells.Add(cell);
                        }
                        htmlTable.Rows.Add(headerRow);

                        // Create table data rows
                        foreach (DataRow row in dataTable.Rows)
                        {
                            HtmlTableRow dataRow = new HtmlTableRow();
                            foreach (DataColumn column in dataTable.Columns)
                            {
                                HtmlTableCell cell = new HtmlTableCell();
                                cell.InnerHtml = row[column].ToString();
                                dataRow.Cells.Add(cell);
                            }
                            htmlTable.Rows.Add(dataRow);
                        }

                        // Add the HtmlTable to your container (e.g., a Panel)
                        form1.Controls.Add(htmlTable);
                    }
                }
            }
        }
    }
}
