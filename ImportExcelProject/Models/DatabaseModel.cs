using System.ComponentModel.DataAnnotations;

namespace ImportExcelProject.Models;

public class DatabaseModel
{
    [Required(ErrorMessage = "Server Name reqired")]
    public string ServerName { get; set; }

    [Required(ErrorMessage = "Database Name reqired")]
    public string DatabaseName { get; set; }
    public string? UserID { get; set; }
    public string? Password { get; set; }
    public bool IsLocalhost { get; set; }
}
