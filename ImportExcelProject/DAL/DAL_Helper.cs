using ImportExcelProject.Common;

namespace ImportExcelProject.DAL;

public class DAL_Helper
{
    //public static string connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("myConnectionString").ToString();

    public static string connectionString = "";
    Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();

    public DAL_Helper()
    {
        data = CommonVariables.GetCredentials();
        if (data.GetValueOrDefault("IsLocalhost") && data.GetValueOrDefault("UserID") == "" && data.GetValueOrDefault("Password") == "")
        {
            connectionString = $"Data Source = {data.GetValueOrDefault("ServerName")};Initial Catalog = {data.GetValueOrDefault("DatabaseName")};Integrated Security = true;";
        }
        else
        {
            connectionString = $"Data Source = {data.GetValueOrDefault("ServerName")};Initial Catalog = {data.GetValueOrDefault("DatabaseName")};User Id = {data.GetValueOrDefault("UserID")};Password = {data.GetValueOrDefault("Password")};Connect Timeout=180";
        }
    }
}
