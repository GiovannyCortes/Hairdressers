using Hairdressers.Extensions;
using Hairdressers.Interfaces;
using Hairdressers.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hairdressers.Controllers {
    public class LoginController : Controller {

        public IRepositoryHairdresser repo_hairdresser;

        public LoginController(IRepositoryHairdresser repo_hairdresser) {
            this.repo_hairdresser = repo_hairdresser;
        }

        public IActionResult Login() {
            return View();
        }

        [ValidateAntiForgeryToken] [HttpPost]
        public async Task<IActionResult> Login(string email, string password) {
            User? user = await this.repo_hairdresser.ValidateUserAsync(email, password);
            if (user != null) {
                HttpContext.Session.SetObject("USER", user);
                return RedirectToAction("ControlPanel", "User");
            } else {
                ViewData["VERIFICATION"] = "Credenciales incorrectas";
                return View();
            }
        }

        public IActionResult Registrer() {
            return View();
        }

        [ValidateAntiForgeryToken] [HttpPost]
        public async Task<IActionResult> Registrer(string Password, string Name, string LastName, string Phone, string Email, bool EmailConfirmed) {
            User newuser = await this.repo_hairdresser.InsertUserAsync(Password, Name, LastName, Phone, Email, EmailConfirmed);
            HttpContext.Session.SetObject("USER", newuser);
            return RedirectToAction("ControlPanel", "User");
        }

        public IActionResult AccesoDenegado() {
            return View();
        }

    }
}
