using ClosedXML.Excel;
using System.Data;

namespace ImportExcelProject.Common
{
    public class CommonFunctions
    {
        public static DataTable? ConvertExcelIntoDataTable(IFormFile file, string sheetName)
        {
            DataTable? dataTable = new DataTable();

            if (file != null && file.Length > 0 && !string.IsNullOrEmpty(sheetName))
            {
                using (var stream = new MemoryStream())
                {
                    file.CopyTo(stream);
                    stream.Position = 0;

                    using (var workbook = new XLWorkbook(stream))
                    {
                        var worksheet = workbook.Worksheet(sheetName);
                        var firstRow = true;

                        foreach (var row in worksheet.RowsUsed())
                        {
                            if (firstRow)
                            {
                                foreach (var cell in row.Cells())
                                {
                                    dataTable.Columns.Add(cell.Value.ToString().Replace(" ", ""));
                                }
                                firstRow = false;
                            }
                            else
                            {
                                dataTable.Rows.Add(row.Cells().Select(cell => cell.Value.ToString()).ToArray());
                            }
                        }
                        return dataTable;
                    }
                }
            }
            return null;
        }
    }
}
