using Hairdressers.Models;
using Hairdressers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Hairdressers.helpers;
using Hairdressers.Extensions;
using Hairdressers.Filters;

namespace Hairdressers.Controllers {
    public class HairdresserController : Controller {

        private IRepositoryHairdresser repo_hairdresser;

        public HairdresserController(IRepositoryHairdresser repo_hairdresser) {
            this.repo_hairdresser = repo_hairdresser;
        }

        [AuthorizeUsers]
        public async Task<IActionResult> ControlPanel(string hid) {
            int hairdresser_id = int.Parse(hid);
            Hairdresser? hairdresser = await this.repo_hairdresser.FindHairdresserAsync(hairdresser_id);
            if (hairdresser != null) {
                List<Schedule> schedules = await this.repo_hairdresser.GetSchedulesAsync(hairdresser_id, true);
                ViewData["SCHEDULES"] = schedules;
                return View(hairdresser);
            } else {
                ViewData["ERROR_MESSAGE_TITLE"] = "Se ha producido un error inesperado";
                ViewData["ERROR_MESSAGE_SUBTITLE"] = "Peluquería no encontrada";
                return RedirectToAction("Error", "Managed");
            }
        }

        [AuthorizeUsers]
        public IActionResult CreateHairdresser() {
            return View();
        }

        [AuthorizeUsers] [ValidateAntiForgeryToken] [HttpPost]
        public async Task<IActionResult> CreateHairdresser(Hairdresser hairdresser, string schedules) {
            // Insertamos la nueva peluquería 
            int user_id = int.Parse(HttpContext.User.FindFirst("ID").Value);
            int newHId = await this.repo_hairdresser.InsertHairdresserAsync(hairdresser.Name, hairdresser.Phone, hairdresser.Address, hairdresser.PostalCode, user_id);

            // Insertamos el horario por defecto 'Horario General'
            int newSid = await this.repo_hairdresser.InsertScheduleAsync(newHId, "Horario General", true);

            // Recuperamos la lista de registros del horario
            List<Schedule_Row> schedules_rows = HelperJson.DeserializeObject<List<Schedule_Row>>(schedules);
            foreach (Schedule_Row r in schedules_rows) {
                await this.repo_hairdresser.InsertScheduleRowsAsync(newSid, r.Start, r.End, r.Monday, r.Tuesday, r.Wednesday, r.Thursday, r.Friday, r.Saturday, r.Sunday);
            }
            return RedirectToAction("ControlPanel", "User");
        }

        [ValidateAntiForgeryToken] [HttpPost]
        public async Task<IActionResult> UpdateHairdresser(Hairdresser hairdresser) {
            //await this.repo_hairdresser.UpdateHairdresserAsync
            return RedirectToAction("ControlPanel", "Hairdresser");
        }

        public async Task<JsonResult> GetHairdresserSuggestions(string searchString) {
            List<Hairdresser> hairdressers = await this.repo_hairdresser.GetHairdressersByFilter(searchString);
            string sugerencias = HelperJson.SerializeObject(hairdressers);
            return Json(sugerencias);
        }

        [AuthorizeUsers]
        public async Task<IActionResult> Services(int hairdresserId, string hairdresserName) {
            List<Service> services = await this.repo_hairdresser.GetServicesByHairdresserAsync(hairdresserId);
            ViewData["HAIRDRESSER_ID"] = hairdresserId;
            ViewData["HAIRDRESSER_NAME"] = hairdresserName;
            return View(services);
        }

        [AuthorizeUsers]
        public async Task<IActionResult> AddService(int hairdresser_id, string name, string price, string time) {
            byte time_in_minutes = (byte)TimeSpan.Parse(time.Replace('.',',')).TotalMinutes;
            int service_id = await this.repo_hairdresser.InsertServiceAsync(hairdresser_id, name, decimal.Parse(price.Replace('.',',')), time_in_minutes);
            return Json(service_id);
        }

        [AuthorizeUsers]
        public async Task<IActionResult> RemoveService(int service_id) {
            await this.repo_hairdresser.DeleteServiceAsync(service_id);
            return Json("OK");
        }

    }
}
