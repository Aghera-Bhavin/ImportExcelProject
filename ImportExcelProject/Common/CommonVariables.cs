namespace ImportExcelProject.Common;

public class CommonVariables
{
    private static IHttpContextAccessor _httpContextAccessor;

    static CommonVariables()
    {
        _httpContextAccessor = new HttpContextAccessor();
    }

    public static Dictionary<string, dynamic> GetCredentials()
    {
        Dictionary<string, dynamic> credential = new Dictionary<string, dynamic>();
        credential.Add("ServerName", _httpContextAccessor?.HttpContext?.Session.GetString("ServerName") ?? "");
        credential.Add("DatabaseName", _httpContextAccessor?.HttpContext?.Session.GetString("DatabaseName") ?? "");
        credential.Add("UserID", _httpContextAccessor?.HttpContext?.Session.GetString("UserID") ?? "");
        credential.Add("Password", _httpContextAccessor?.HttpContext?.Session.GetString("Password") ?? "");
        credential.Add("IsLocalhost", Convert.ToBoolean(_httpContextAccessor?.HttpContext?.Session.GetInt32("IsLocalhost")));
        return credential;
    }
}