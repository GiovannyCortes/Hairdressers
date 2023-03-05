using Hairdressers.Extensions;
using Hairdressers.Models;
using Hairdressers.Repositories;
using Hairdressers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hairdressers.Controllers {
    public class UserController : Controller {

        IRepositoryUser repo_user;
        IRepositoryHairdresser repo_hairdresser;

        public UserController(IRepositoryHairdresser repo_hairdresser, IRepositoryUser repo_user) {
            this.repo_user = repo_user;
            this.repo_hairdresser = repo_hairdresser;
        }

        public IActionResult ControlPanel() {
            User user = HttpContext.Session.GetObject<User>("USER");
            if (user == null) {
                return RedirectToAction("Error", "Redirect");
            } else {
                if (this.repo_user.IsAdmin(user.UserId)) {
                    List<Hairdresser> hairdressers = this.repo_hairdresser.GetHairdressers(user.UserId);
                    ViewData["HAIRDRESSERS"] = hairdressers;
                }
                return View(user);
            }
        }

    }
}
