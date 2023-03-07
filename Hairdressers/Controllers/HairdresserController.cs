using Microsoft.AspNetCore.Mvc;

namespace Hairdressers.Controllers {
    public class HairdresserController : Controller {

        public IActionResult ControlPanel(string hid) {
            ViewData["PRUEBA"] = hid;
            return View();
        }

    }
}
