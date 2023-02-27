using Hairdressers.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Hairdressers.Controllers {
    public class LandingController : Controller {

        public IActionResult Index() {
            return View();
        }

    }
}
