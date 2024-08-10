using ImportExcelProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace ImportExcelProject.Controllers
{
    public class DatabaseController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Message = TempData["Message"];
            return View();
        }

        [HttpPost]
        public IActionResult Index(DatabaseModel model)
        {
            if (ModelState.IsValid)
            {
                HttpContext.Session.SetString("ServerName", model.ServerName);
                HttpContext.Session.SetString("DatabaseName", model.DatabaseName);
                HttpContext.Session.SetString("UserID", model.UserID ?? "");
                HttpContext.Session.SetString("Password", model.Password ?? "");
                HttpContext.Session.SetInt32("IsLocalhost", Convert.ToInt32(model.IsLocalhost));
                ViewBag.Message = "<h4 class=\"p-2 mt-2 w-25 alert alert-success text-success\">Connected</h4>";
                return RedirectToAction("Index", "UploadExcel");
            }
            return View(model);
        }

        public IActionResult Disconnect()
        {
            HttpContext.Session.Clear();
            TempData["Message"] = "<h4 class=\"p-2 mt-2 w-25 alert alert-success text-success\">Successfully Disconnected</h4>";
            return RedirectToAction("Index");
        }
    }
}
