using ClosedXML.Excel;
using ImportExcelProject.BAL;
using ImportExcelProject.Common;
using ImportExcelProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ImportExcelProject.Controllers;

public class UploadExcelController : Controller
{
    public IActionResult Index()
    {
        return View(new ExcelFileModel());
    }

    [HttpPost]
    public IActionResult ImportExcel(ExcelFileModel formData)
    {
        Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();
        try
        {
            UploadExcel_BAL balUploadExcel = new UploadExcel_BAL();
            var table = CommonFunctions.ConvertExcelIntoDataTable(formData.File, formData.SelectedSheetName);
            if (table != null)
            {
                bool isSuccess = balUploadExcel.CreateTable(formData.TableName, table);
                if (isSuccess)
                {
                    bool isImported = balUploadExcel.UploadExcel(formData.TableName, table);
                    if (isImported)
                    {
                        response.Add("success", true);
                        response.Add("message", "Successfully imported data to the table");
                    }
                    else
                    {
                        response.Add("success", false);
                        response.Add("message", "Cannot import data");
                    }
                }
            }
            else
            {
                response.Add("success", false);
                response.Add("message", "Select all required fields");
            }
        }
        catch (Exception ex)
        {
            response.Add("success", false);
            response.Add("message", ex.Message.ToString());
        }
        return Json(response);
    }

    [HttpPost]
    public IActionResult GetSheetNames(IFormFile file)
    {
        if (file != null && file.Length > 0)
        {
            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                stream.Position = 0;

                using (var workbook = new XLWorkbook(stream))
                {
                    var sheetNames = new List<string>();
                    foreach (var sheet in workbook.Worksheets)
                    {
                        sheetNames.Add(sheet.Name);
                    }
                    return Json(sheetNames);
                }
            }
        }
        return Json(new List<string>());
    }

    [HttpPost]
    public IActionResult GetTableData(string selectedSheet, IFormFile file)
    {
        Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();
        var table = CommonFunctions.ConvertExcelIntoDataTable(file, selectedSheet);
        if (table != null)
        {
            var tableData = table.AsEnumerable()
                                     .Select(row => table.Columns.Cast<DataColumn>()
                                     .ToDictionary(col => col.ColumnName, col => row[col]))
                                     .ToList();
            response.Add("success", true);
            response.Add("message", "Data Found");
            response.Add("data", tableData);
            return Json(response);
        }
        response.Add("success", false);
        response.Add("message", "Data Not Found");
        response.Add("data", new List<Dictionary<string, object>>());
        return Json(response);
    }

    [HttpPost]
    public IActionResult ViewExcelData(string SelectedSheetName, IFormFile File)
    {
        var table = CommonFunctions.ConvertExcelIntoDataTable(File, SelectedSheetName);
        return View(table);
    }
}



//public DataTable? GetDataTable(string selectedSheet, IFormFile file)
//{
//    if (file != null && file.Length > 0 && !string.IsNullOrEmpty(selectedSheet))
//    {
//        using (var stream = new MemoryStream())
//        {
//            file.CopyTo(stream);
//            stream.Position = 0;

//            using (var workbook = new XLWorkbook(stream))
//            {
//                var worksheet = workbook.Worksheet(selectedSheet);
//                var firstRow = true;

//                foreach (var row in worksheet.RowsUsed())
//                {
//                    if (firstRow)
//                    {
//                        foreach (var cell in row.Cells())
//                        {
//                            dataTable.Columns.Add(cell.Value.ToString().Replace(" ", ""));
//                        }
//                        firstRow = false;
//                    }
//                    else
//                    {
//                        dataTable.Rows.Add(row.Cells().Select(cell => cell.Value.ToString()).ToArray());
//                    }
//                }
//                return dataTable;
//            }
//        }
//    }

//    return null;
//}
