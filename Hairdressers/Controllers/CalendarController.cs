using Microsoft.AspNetCore.Mvc;

namespace Hairdressers.Controllers {
    public class CalendarController : Controller {

        public IActionResult Index() {
            return View();
        }

    }
}
