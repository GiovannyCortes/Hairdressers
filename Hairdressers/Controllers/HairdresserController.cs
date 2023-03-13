using Hairdressers.Models;
using Hairdressers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Hairdressers.helpers;
using Hairdressers.Extensions;

namespace Hairdressers.Controllers {
    public class HairdresserController : Controller {

        private IRepositoryHairdresser repo_hairdresser;

        public HairdresserController(IRepositoryHairdresser repo_hairdresser) {
            this.repo_hairdresser = repo_hairdresser;
        }

        public IActionResult ControlPanel(string hid) {
            User user = HttpContext.Session.GetObject<User>("USER");
            if (user != null) {
                int hairdresser_id = int.Parse(hid);
                Hairdresser? hairdresser = this.repo_hairdresser.FindHairdresser(hairdresser_id);
                if (hairdresser != null) {
                    List<Schedule> schedules = this.repo_hairdresser.GetSchedules(hairdresser_id, true);
                    ViewData["SCHEDULES"] = schedules;
                    return View(hairdresser);
                } else {
                    ViewData["ERROR_MESSAGE_TITLE"] = "Se ha producido un error inesperado";
                    ViewData["ERROR_MESSAGE_SUBTITLE"] = "Peluquería no encontrada";
                    return RedirectToAction("Error", "Redirect");
                }
            } else {
                return RedirectToAction("DeniedAccess", "Redirect");
            }
        }

        public IActionResult CreateHairdresser() {
            User user = HttpContext.Session.GetObject<User>("USER");
            if (user != null) {
                return View();
            } else {
                return RedirectToAction("DeniedAccess", "Redirect");
            }
        }

        [ValidateAntiForgeryToken] [HttpPost]
        public async Task<IActionResult> CreateHairdresser(Hairdresser hairdresser, string schedules) {
            User user = HttpContext.Session.GetObject<User>("USER");
            if (user != null) {
                // Insertamos la nueva peluquería 
                int newHId = await this.repo_hairdresser.InsertHairdresserAsync(hairdresser.Name, hairdresser.Phone, hairdresser.Address, hairdresser.PostalCode, user.UserId);

                // Insertamos el horario por defecto 'Horario General'
                int newSid = await this.repo_hairdresser.InsertScheduleAsync(newHId, "Horario General", true);
         
                // Recuperamos la lista de registros del horario
                List<Schedule_Row> schedules_rows = HelperJson.DeserializeObject<List<Schedule_Row>>(schedules);
                foreach(Schedule_Row r in schedules_rows) {
                    await this.repo_hairdresser.InsertScheduleRowsAsync(newSid, r.Start, r.End, r.Monday, r.Tuesday, r.Wednesday, r.Thursday, r.Friday, r.Saturday, r.Sunday);
                }
                return RedirectToAction("ControlPanel", "User");
            } else {
                ViewData["ERROR_MESSAGE_TITLE"] = "Se ha producido un error inesperado";
                ViewData["ERROR_MESSAGE_SUBTITLE"] = "Sesión cerrada, usuario no encontrado";
                return RedirectToAction("Error", "Redirect");
            }
        }

        [ValidateAntiForgeryToken] [HttpPost]
        public async Task<IActionResult> UpdateHairdresser(Hairdresser hairdresser) {
            //await this.repo_hairdresser.UpdateHairdresserAsync
            return RedirectToAction("ControlPanel", "Hairdresser");
        }

    }
}
