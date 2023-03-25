using Hairdressers.Models;
using Hairdressers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Hairdressers.Filters;
using System.Security.Claims;
using Hairdressers.Helpers;

namespace Hairdressers.Controllers {
    public class UserController : Controller {

        private readonly IConfiguration _configuration;
        private IRepositoryHairdresser repo_hairdresser;

        public UserController(IRepositoryHairdresser repo_hairdresser, IConfiguration configuration) {
            this.repo_hairdresser = repo_hairdresser;
            this._configuration = configuration;
        }

        [AuthorizeUsers]
        public async Task<IActionResult> ControlPanel() {
            User user = new User {
                UserId = int.Parse(HttpContext.User.FindFirst("ID").Value),
                Name = HttpContext.User.Identity.Name,
                LastName = HttpContext.User.FindFirst("LAST_NAME").Value,
                Email = HttpContext.User.FindFirst("EMAIL").Value,
                EmailConfirmed = bool.Parse(HttpContext.User.FindFirst("EMAIL_CONFIRMED").Value),
                Phone = HttpContext.User.FindFirst("PHONE").Value
            };

            if (HttpContext.User.FindFirst(ClaimTypes.Role).Value == "ADMIN") {
                List<Hairdresser> hairdressers = await this.repo_hairdresser.GetHairdressersAsync(user.UserId);
                ViewData["HAIRDRESSERS"] = hairdressers;
            }

            return View(user);
        }

        [AuthorizeUsers]
        public async Task<IActionResult> SendValidateEmail() {
            User user = new User {
                UserId = int.Parse(HttpContext.User.FindFirst("ID").Value),
                Name = HttpContext.User.Identity.Name,
                LastName = HttpContext.User.FindFirst("LAST_NAME").Value,
                Email = HttpContext.User.FindFirst("EMAIL").Value,
                EmailConfirmed = bool.Parse(HttpContext.User.FindFirst("EMAIL_CONFIRMED").Value),
                Phone = HttpContext.User.FindFirst("PHONE").Value
            };
            string token = this.repo_hairdresser.GenerateToken() + user.UserId;
            await this.repo_hairdresser.AssignTokenAsync(user.UserId, token);
            await new HelperEmailService(this._configuration).SendTemplateVerificationEmailAsync(user.Email, user.Name + " " + user.LastName, token);
            return Json("OK");
        }

        public async Task<IActionResult> ValidateEmail(string token) {
            int user_id = int.Parse(token.Substring(50));
            User? user = await this.repo_hairdresser.FindUserAsync(user_id);
            if (user != null) {
                bool response = await this.repo_hairdresser.ValidateTokenAsync(user_id, token);
                if (response) { // Validamos el email
                    await this.repo_hairdresser.ValidateEmailAsync(user_id);
                }
                ViewData["RESPONSE"] = (response) ? 1 : 2; /// 1: Usuario encontrado y validado | 2: Usuario encontrado pero no validado
                return View(user);
            } else {
                ViewData["RESPONSE"] = 3; /// 3: Usuario NO encontrado
                return View();
            }
        }
    }
}