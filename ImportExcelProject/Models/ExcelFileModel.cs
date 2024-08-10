using System.ComponentModel.DataAnnotations;

namespace ImportExcelProject.Models
{
    public class ExcelFileModel
    {
        [Required(ErrorMessage = "File is required")]
        public IFormFile File { get; set; }     
        
        [Required(ErrorMessage = "Enter the table name")]
        public string TableName { get; set; }       
        public List<string> SheetNames { get; set; }

        [Required(ErrorMessage = "Select at least one proper sheet")]
        public string SelectedSheetName { get; set; } 

        // Constructor
        public ExcelFileModel()
        {
            SheetNames = new List<string>();
        }
    }
}
