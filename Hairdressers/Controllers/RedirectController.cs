using Hairdressers.Extensions;
using Hairdressers.Interfaces;
using Hairdressers.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hairdressers.Controllers {
    public class RedirectController : Controller {

        private IRepositoryUser repo_user;
        private IRepositoryHairdresser repo_hairdresser;

        public RedirectController(IRepositoryUser repo_user, IRepositoryHairdresser repo_hairdresser) {
            this.repo_user = repo_user;
            this.repo_hairdresser = repo_hairdresser;
        }

        public IActionResult LogOut() {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Landing");
        }

        public IActionResult Error() {
            return View();
        }

        public IActionResult Appointments() { // Seguir con esto más tarde
            User user = HttpContext.Session.GetObject<User>("USER");
            if (user == null) {
                ViewData["ERROR_MESSAGE_TITLE"] = "Se ha producido un error inesperado";
                ViewData["ERROR_MESSAGE_SUBTITLE"] = "Usuario no encontrado";
                return RedirectToAction("Error", "Redirect");
            } else { // Usuario en la Session
                if (this.repo_user.IsAdmin(user.UserId)) { // Es administrador, necesitamos que elija opción
                    return RedirectToAction("", "");
                } else { // Solo puede pedir citas
                    return RedirectToAction("ClientAppointments", "User"); 
                }
            }
        }

        public async Task<IActionResult> Prueba() {
            await this.repo_hairdresser.InsertHairdresserAsync("ESTUDIO DE PRUEBA", "5165156", "MI CASA", 65980, 1);
            //List<Hairdresser> hairdressers = this.repo_hairdresser.GetHairdressers(1);
            List<Hairdresser> hairdressers = this.repo_hairdresser.GetHairdressers();
            return View();
        }

    }
}
