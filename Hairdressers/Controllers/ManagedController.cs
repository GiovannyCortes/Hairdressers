using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Hairdressers.Models;
using Hairdressers.Interfaces;
using Hairdressers.Extensions;

namespace Hairdressers.Controllers {
    public class ManagedController : Controller {

        private IRepositoryHairdresser repo_hairdresser;

        public ManagedController(IRepositoryHairdresser repo_hairdresser) {
            this.repo_hairdresser = repo_hairdresser;
        }

        public IActionResult LogIn() {
            return View();
        }

        [HttpPost] [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(string email, string password) {
            User? user = await this.repo_hairdresser.ValidateUserAsync(email, password);
            if (user != null) { // Usuario encontrado, credenciales correctas
                ClaimsIdentity identity = new ClaimsIdentity(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        ClaimTypes.Name, ClaimTypes.Role
                );

                string admin = await this.repo_hairdresser.IsAdmin(user.UserId) ? "ADMIN" : "CLIENT";

                Claim claimUserID = new Claim("ID", user.UserId.ToString());
                Claim claimUserName = new Claim(ClaimTypes.Name, user.Name);
                Claim claimUserLastName = new Claim("LAST_NAME", user.LastName);
                Claim claimUserEmail = new Claim("EMAIL", user.Email);
                Claim claimUserEmailConfirmed = new Claim("EMAIL_CONFIRMED", user.EmailConfirmed.ToString());
                Claim claimUserPhone = new Claim("PHONE", user.Phone);
                Claim claimUserRole = new Claim(ClaimTypes.Role, admin);

                identity.AddClaim(claimUserID);
                identity.AddClaim(claimUserName);
                identity.AddClaim(claimUserLastName);
                identity.AddClaim(claimUserEmail);
                identity.AddClaim(claimUserEmailConfirmed);
                identity.AddClaim(claimUserPhone);
                identity.AddClaim(claimUserRole);

                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync (
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    userPrincipal
                );

                string controller = TempData["controller"].ToString();
                string action = TempData["action"].ToString();
                return RedirectToAction(action, controller);
            } else {
                ViewData["VERIFICATION"] = "Credenciales incorrectas";
                return View();
            }
        }

        public async Task<IActionResult> LogOut() {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Landing");
        }

        public IActionResult Registrer() {
            return View();
        }

        [HttpPost] [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrer(string Password, string Name, string LastName, string Phone, string Email, bool EmailConfirmed) {
            await this.repo_hairdresser.InsertUserAsync(Password, Name, LastName, Phone, Email, EmailConfirmed);
            return RedirectToAction("Index", "Landing");
        }

        public IActionResult AccesoDenegado() {
            return View();
        }

        public IActionResult Error() {
            return View();
        }

    }
}
