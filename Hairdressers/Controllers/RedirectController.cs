using Microsoft.AspNetCore.Mvc;

namespace Hairdressers.Controllers {
    public class RedirectController : Controller {

        public IActionResult LogOut() {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Landing");
        }

        public IActionResult Error() {
            return View();
        }

    }
}
