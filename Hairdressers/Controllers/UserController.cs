using Hairdressers.Models;
using Hairdressers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Hairdressers.Filters;
using System.Security.Claims;

namespace Hairdressers.Controllers {
    public class UserController : Controller {

        private IRepositoryHairdresser repo_hairdresser;

        public UserController(IRepositoryHairdresser repo_hairdresser) {
            this.repo_hairdresser = repo_hairdresser;
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

    }
}