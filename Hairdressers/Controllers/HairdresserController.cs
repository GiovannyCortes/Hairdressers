using Hairdressers.Models;
using Hairdressers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hairdressers.Controllers {
    public class HairdresserController : Controller {

        private IRepositoryHairdresser repo_hairdresser;

        public HairdresserController(IRepositoryHairdresser repo_hairdresser) {
            this.repo_hairdresser = repo_hairdresser;
        }

        public IActionResult ControlPanel(string hid) {
            int hairdresser_id = int.Parse(hid);
            Hairdresser? hairdresser = this.repo_hairdresser.FindHairdresser(hairdresser_id);
            if(hairdresser != null) {

                return View(hairdresser);
            } else {
                ViewData["ERROR_MESSAGE_TITLE"] = "Se ha producido un error inesperado";
                ViewData["ERROR_MESSAGE_SUBTITLE"] = "Peluquería no encontrada";
                return RedirectToAction("Error", "Redirect");
            }
        }

        public IActionResult CreateHairdresser(int userid) {
            return View();
        }

        [ValidateAntiForgeryToken] [HttpPost]
        public IActionResult CreateHairdresser(Hairdresser hairdresser) {
            return View();
        }

        [ValidateAntiForgeryToken] [HttpPost]
        public async Task<IActionResult> UpdateHairdresser(Hairdresser hairdresser) {
            //await this.repo_hairdresser.UpdateHairdresserAsync
            return RedirectToAction("ControlPanel", "Hairdresser");
        }

    }
}
