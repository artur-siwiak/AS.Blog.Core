using AS.Blog.Core.DB;
using AS.Blog.Core.Models;
using AS.Blog.Core.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AS.Blog.Core.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public UserController(SignInManager<User> signInManager,
            UserManager<User> userManager,
            IUserService userService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userService = userService;
        }

        public IActionResult Login(string returnUrl = default)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl = default)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userService.FindUserByName(model.Email).ConfigureAwait(false);

            if (user == null)
            {
                return View(model);
            }

            if (!user.Active)
            {

            }

            var x = await _signInManager.PasswordSignInAsync(user.Email, model.Password, model.RememberMe, true).ConfigureAwait(false);

            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        public async Task<IActionResult> Logoff()
        {
            await _signInManager.SignOutAsync().ConfigureAwait(false);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User
            {
                Active = true,
                DisplayName = model.DisplayName,
                Email = model.Email,
            };

            var x = await _userManager.CreateAsync(user, model.Password).ConfigureAwait(false);
            await _userManager.AddToRoleAsync(user, "User").ConfigureAwait(false);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}