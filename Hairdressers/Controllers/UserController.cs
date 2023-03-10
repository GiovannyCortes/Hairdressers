using Hairdressers.Extensions;
using Hairdressers.Models;
using Hairdressers.Repositories;
using Hairdressers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hairdressers.Controllers {
    public class UserController : Controller {

        private IRepositoryHairdresser repo_hairdresser;

        public UserController(IRepositoryHairdresser repo_hairdresser) {
            this.repo_hairdresser = repo_hairdresser;
        }

        public IActionResult ControlPanel() {
            User user = HttpContext.Session.GetObject<User>("USER");
            if (user == null) {
                ViewData["ERROR_MESSAGE_TITLE"] = "Se ha producido un error inesperado";
                ViewData["ERROR_MESSAGE_SUBTITLE"] = "Usuario no encontrado";
                return RedirectToAction("Error", "Redirect");
            } else {
                if (this.repo_hairdresser.IsAdmin(user.UserId)) {
                    List<Hairdresser> hairdressers = this.repo_hairdresser.GetHairdressers(user.UserId);
                    ViewData["HAIRDRESSERS"] = hairdressers;
                }
                return View(user);
            }
        }

        public IActionResult ClientAppointments() { // Gestión de citas (Solicitudes)
            return View();
        }

    }
}
