using ImportExcelProject.DAL;
using System.Data.SqlClient;
using System.Data;

namespace ImportExcelProject.BAL;

public class UploadExcel_BAL
{
    UploadExcel_DAL dalUploadExcel = new UploadExcel_DAL();

    public bool CreateTable(string? tableName, DataTable? dataTable)
    {
        return dalUploadExcel.CreateTable(tableName, dataTable);
    }

    public bool UploadExcel(string? tableName, DataTable? dataTable)
    {
        return dalUploadExcel.UploadExcel(tableName, dataTable);
    }
}
