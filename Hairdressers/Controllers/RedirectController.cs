using Hairdressers.Extensions;
using Hairdressers.Interfaces;
using Hairdressers.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hairdressers.Controllers {
    public class RedirectController : Controller {

        private IRepositoryHairdresser repo_hairdresser;

        public RedirectController(IRepositoryHairdresser repo_hairdresser) {
            this.repo_hairdresser = repo_hairdresser;
        }

        public IActionResult LogOut() {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Landing");
        }

        public IActionResult Error() {
            return View();
        }

        public IActionResult DeniedAccess() {
            return View();
        }

        public IActionResult Appointments() { // Seguir con esto más tarde
            User user = HttpContext.Session.GetObject<User>("USER");
            if (user == null) {
                ViewData["ERROR_MESSAGE_TITLE"] = "Se ha producido un error inesperado";
                ViewData["ERROR_MESSAGE_SUBTITLE"] = "Usuario no encontrado";
                return RedirectToAction("Error", "Redirect");
            } else { // Usuario en la Session
                if (this.repo_hairdresser.IsAdmin(user.UserId)) { // Es administrador, necesitamos que elija opción
                    return RedirectToAction("", "");
                } else { // Solo puede pedir citas
                    return RedirectToAction("ClientAppointments", "User"); 
                }
            }
        }

        public async Task<IActionResult> Prueba() {
            return View();
        }

    }
}
