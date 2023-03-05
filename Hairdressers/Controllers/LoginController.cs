using Hairdressers.Extensions;
using Hairdressers.Interfaces;
using Hairdressers.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hairdressers.Controllers {
    public class LoginController : Controller {

        public IRepositoryUser repo_user;

        public LoginController(IRepositoryUser repo_user) {
            this.repo_user = repo_user;
        }

        public IActionResult Login() {
            ViewData["VERIFICATION"] = "0";  // 0 - NO LOGIN 1 - ERROR LOGIN
            return View();
        }

        [ValidateAntiForgeryToken] [HttpPost]
        public IActionResult Login(string email, string password) {
            User? user = this.repo_user.ValidateUser(email, password);
            if (user != null) {
                HttpContext.Session.SetObject("USER", user);
                return RedirectToAction("Index", "Landing");
            } else {
                ViewData["VERIFICATION"] = "1"; // 0 - NO LOGIN 1 - ERROR LOGIN
                return View();
            }
        }

        public IActionResult Registrer() {
            return View();
        }

        [ValidateAntiForgeryToken] [HttpPost]
        public async Task<IActionResult> Registrer(User user) {
            User newuser = await this.repo_user.InsertUserAsync(user.Password, user.Name, user.LastName, user.Phone, user.Email, user.EmailConfirmed);
            HttpContext.Session.SetObject("USER", user);
            return RedirectToAction("Index", "Landing");
        }

        public IActionResult AccesoDenegado() {
            return View();
        }

    }
}
