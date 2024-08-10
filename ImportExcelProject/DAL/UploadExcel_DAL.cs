using Microsoft.Extensions.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace ImportExcelProject.DAL;

public class UploadExcel_DAL : DAL_Helper
{
    public bool CreateTable(string? tableName, DataTable? dataTable)
    {
        if (tableName != null && dataTable != null)
        {
            var sql = $"CREATE TABLE [dbo].[IMP_{tableName}] ( {tableName}ID INT IDENTITY(1,1) PRIMARY KEY,";
            foreach (DataColumn column in dataTable.Columns)
            {
                sql += $"[{column.ColumnName}] NVARCHAR(MAX),";
            }
            sql = sql.TrimEnd(',') + ")";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand($"Select OBJECT_ID('IMP_{tableName}', 'U')", connection);
                connection.Open();
                string? isExist = command.ExecuteScalar().ToString();
                if (string.IsNullOrEmpty(isExist))
                {
                    SqlCommand commandForCreateTable = new SqlCommand(sql, connection);
                    var isSuccess = Convert.ToBoolean(commandForCreateTable.ExecuteNonQuery());
                    return isSuccess;
                }
                return true;
            }
        }
        return false;
    }
    public bool UploadExcel(string? tableName, DataTable? dataTable)
    {
        if (!string.IsNullOrEmpty(tableName) && dataTable != null)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.DestinationTableName = "IMP_" + tableName;
                    try
                    {
                        // Optionally map columns if the column names in the DataTable don't match the SQL table
                        // bulkCopy.ColumnMappings.Add("SourceColumn", "DestinationColumn");
                        bulkCopy.WriteToServer(dataTable);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        // Handle the exception as needed
                        Console.WriteLine(ex.Message);
                        return false;
                    }
                }
            }
        }
        return false;
    }
}
