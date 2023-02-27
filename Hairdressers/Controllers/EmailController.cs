using Hairdressers.Helpers;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace Hairdressers.Controllers {
    public class EmailController : Controller {

        private readonly IConfiguration _configuration;

        public EmailController(IConfiguration configuration) {
            this._configuration = configuration;
        }

        public IActionResult Bien() {
            return View();
        }        
        
        public IActionResult Mal() {
            return View();
        }

        [HttpGet] // Esta vista no hará falta, se sustituirá por el botón de verificación en la vista de user config
        public IActionResult SendConfirmationEmail() {
            return View();
        }

        [HttpPost]
        public IActionResult SendConfirmationEmailAsync(string receiver, string email) {
            EmailService emailService = new EmailService(this._configuration);
            int codigo = emailService.SendConfirmationEmail(receiver, email);

            //int codigo = 12345;
            HttpContext.Session.SetString("CodigoVerificacion", codigo.ToString());
            return RedirectToAction("CompareConfirmation");
        }

        [HttpGet]
        public IActionResult CompareConfirmation() {
            return View();
        }

        [HttpPost]
        public IActionResult CompareConfirmation(string codigoIngresado) {
            string codigoGenerado = HttpContext.Session.GetString("CodigoVerificacion");
            if (codigoIngresado == codigoGenerado) {
                return RedirectToAction("Bien");
            } else {
                return RedirectToAction("Mal");
            }
        }
    }
}
