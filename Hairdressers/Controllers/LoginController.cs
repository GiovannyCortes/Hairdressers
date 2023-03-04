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
            string validation = this.repo_user.ValidateUser(email, password);
            if (validation != "") {
                HttpContext.Session.SetString("VALIDATION", validation);
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
        public IActionResult Registrer(User user) {
            return View();
        }

        public IActionResult AccesoDenegado() {
            return View();
        }

    }
}
