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
            return View();
        }

        [ValidateAntiForgeryToken] [HttpPost]
        public IActionResult Login(string email, string password) {
            User? user = this.repo_user.ValidateUser(email, password);
            if (user != null) {
                HttpContext.Session.SetObject("USER", user);
                return RedirectToAction("Index", "Landing");
            } else {
                ViewData["VERIFICATION"] = "Credenciales incorrectas";
                return View();
            }
        }

        public IActionResult Registrer() {
            return View();
        }

        [ValidateAntiForgeryToken] [HttpPost]
        public async Task<IActionResult> Registrer(User user) {
            User newuser = await this.repo_user.InsertUserAsync(user.Password, user.Name, user.LastName, user.Phone, user.Email, user.EmailConfirmed);
            HttpContext.Session.SetObject("USER", newuser);
            return RedirectToAction("Index", "Landing");
        }

        public IActionResult AccesoDenegado() {
            return View();
        }

    }
}
