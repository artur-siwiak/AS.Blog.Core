using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AS.Blog.Core.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        public string Index()
        {
            return "dsad";
        }
    }
}