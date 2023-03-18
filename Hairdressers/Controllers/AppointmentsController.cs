using Hairdressers.Filters;
using Hairdressers.helpers;
using Hairdressers.Interfaces;
using Hairdressers.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hairdressers.Controllers {
    public class AppointmentsController : Controller {

        private IRepositoryHairdresser repo;

        public AppointmentsController(IRepositoryHairdresser repo) {
            this.repo = repo;
        }

        /*
         *  Recibiremos un identificador de peluquería (hairdresser_id) para determinar la solicitud de datos 
         *  que se le debe pasar al calendario (citas de una peluquería o de un usuario). Si no se añade un id
         *  de peluquería se entiende que se solicitan datos de citas de un usuario.
         *  
         *  En el caso de solicitar los datos de las citas de una peluquería, será necesario comprobar el rol
         *  de dicho usuario y su relación con dicha peluquería. (En futuras versiones se podría implementar
         *  en caché un validador para no recuperar en varias ocasiones la validación de la relación user-hairdresser)
         */

        [AuthorizeUsers]
        public async Task<IActionResult> Appointments(int? hairdresserId) {
            List<Appointment> appointments; // ¿Qué datos de citas queremos? ¿De usuario o de peluquería?
            int user_id = int.Parse(HttpContext.User.FindFirstValue("ID"));
            bool superUser; // Determina la precisión de datos de citas a recoger

            if (hairdresserId != null) { 
                if (HttpContext.User.FindFirst(ClaimTypes.Role).Value == "ADMIN") {
                    superUser = await this.repo.AdminExistAsync(hairdresserId.Value, user_id);
                } else { superUser = false; }
                appointments = await this.repo.GetAppointmentsByHairdresserAsync(hairdresserId.Value);
            } else { // Se solicitan datos de citas de Usuario
                superUser = true;
                appointments = await this.repo.GetAppointmentsByUserAsync(user_id);
            }

            // La lista recuperada es transformada y enviada a la vista para su representación
            List<Object> appointments_json = await this.GenerateInfoCalendar(appointments, superUser);
            string json = HelperJson.SerializeObject(appointments_json);
            ViewData["JSON_APPOINTMENTS"] = json;
            TempData["SUPERUSUARIO"] = superUser;
            return View();
        }

        private async Task<List<Object>> GenerateInfoCalendar(List<Appointment> appointments, bool superUser) {
            List<Object> appointments_json = new List<Object>();
            foreach (Appointment app in appointments) { // Recorremos las citas encontradas en la peluquería

                User? user = (superUser) ? await this.repo.FindUserAsync(app.UserId) : null;
                List<Service> services = await this.repo.GetServicesByAppointmentAsync(app.AppointmentId);

                int timeAprox = this.GetMinutesByAppointment(services);
                string price = (superUser) ? this.GetTotalPriceByAppointment(services) : "";
                string userName = (superUser && user != null) ? user.Name : "CITA";
                string date = app.Date.ToString("yyyy-MM-ddT");

                var element = new {
                    Title = userName,
                    Start = date + app.Time.ToString("hh\\:mm\\:00"),
                    End = date + this.CalculateEndAppointment(app.Time, timeAprox),
                    ExtendedProps = new { },
                    Description = (superUser) ? ("Precio Total: " + price + "€") : ""
                };

                if (superUser) { // Solo los super usuarios pueden ver los servicios de las citas
                    int serviceCount = 0; // Enumeración de los servicios a realizar en la cita
                    foreach (Service service in services) {
                        serviceCount++;
                        string propertyName = $"service_{serviceCount}";
                        ((dynamic)element.ExtendedProps).TrySetMember(propertyName, service.Name + "(" + service.Price + "€)");
                    }
                }

                appointments_json.Add(element); // Almacenamos cada elemento en el JSON a devolver
            }
            return appointments_json;
        }

        private int GetMinutesByAppointment(List<Service> services) {
            int time = 0;
            foreach (Service service in services) {
                time += service.TiempoAprox;
            }
            return time;
        }

        private string CalculateEndAppointment(TimeSpan start, int duration) {
            TimeSpan end = start.Add(TimeSpan.FromMinutes(duration));
            return end.ToString("hh\\:mm\\:00");
        }

        private string GetTotalPriceByAppointment(List<Service> services) {
            decimal price = 0;
            foreach (Service service in services) {
                price += service.Price;
            }
            return price.ToString() + " €";
        }

    }
}
