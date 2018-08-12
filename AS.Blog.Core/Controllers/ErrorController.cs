using Microsoft.AspNetCore.Mvc;

namespace AS.Blog.Core.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index(int errorCode)
        {
            HttpContext.Response.StatusCode = errorCode;
            return View(errorCode.ToString());
        }
    }
}