using Hairdressers.Filters;
using Hairdressers.helpers;
using Hairdressers.Helpers;
using Hairdressers.Interfaces;
using Hairdressers.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            List<Appointment> appointments;
            int user_id = int.Parse(HttpContext.User.FindFirstValue("ID"));

            bool administrator_privileges; // Administrador de la peluquería
            bool hairdresser_view = false; // Vista de una única peluquería

            Hairdresser? hairdresser = null;
            List<BussinesHours>? bussines_hours = null;
            List<Service>? services = null;

            if (hairdresserId != null) { // ¿Qué datos de citas queremos? ¿De usuario o de peluquería?

                hairdresser_view = true;
                if (HttpContext.User.FindFirst(ClaimTypes.Role).Value == "ADMIN") {
                    administrator_privileges = await this.repo.AdminExistAsync(hairdresserId.Value, user_id);
                } else { 
                    administrator_privileges = false;
                }

                // Recuperamos la peluquería
                hairdresser = await this.repo.FindHairdresserAsync(hairdresserId.Value);

                // Listamos su horario activo para fijarlo como horario laboral
                List<Schedule_Row> schedule_rows = await this.repo.GetActiveScheduleRowsAsync(hairdresserId.Value);
                bussines_hours = HelperCalendar.GetBussinesHours(schedule_rows);

                // Listamos los servicios de la peluquería
                services = await this.repo.GetServicesByHairdresserAsync(hairdresserId.Value);

                // Listamos las citas de la peluquería
                appointments = await this.repo.GetAppointmentsByHairdresserAsync(hairdresserId.Value);

            } else { // Se solicitan datos de citas de Usuario
                administrator_privileges = true;
                appointments = await this.repo.GetAppointmentsByUserAsync(user_id);
            }

            // La lista recuperada es transformada y enviada a la vista para su representación
            List<Object> appointments_json = await this.GenerateInfoCalendar(appointments, administrator_privileges);
            ViewData["HAIRDRESSER"] = hairdresser;
            ViewData["SERVICES"] = (services != null && services.Count > 0) ? HelperJson.SerializeObject(services) : null;
            ViewData["BUSSINESS_HOURS"] = (bussines_hours != null) ? HelperJson.SerializeObject(bussines_hours) : null;
            ViewData["JSON_APPOINTMENTS"] = HelperJson.SerializeObject(appointments_json);
            ViewData["USER"] = await this.repo.FindUserAsync(user_id);
            TempData["ADMIN_PRIV"] = administrator_privileges;
            TempData["HAIRDRESSER_VIEW"] = hairdresser_view;
            return View();
        }

        [HttpPost] [AuthorizeUsers]
        public async Task<IActionResult> CreateAppointment(string mydata) {
            GetCalendarAppointment appointment = JsonConvert.DeserializeObject<GetCalendarAppointment>(mydata);

            // Creación de la cita
            int appointment_id = await this.repo.InsertAppointmentAsync(appointment.user_id, appointment.hairdresser_id, appointment.date, appointment.time);
            foreach (int service_id in appointment.services) {
                await this.repo.InsertAppointmentServiceAsync(appointment_id, service_id);
            }

            // Envío del correo de confirmación


            return Json("/Appointments/Appointments?hairdresserId=" + appointment.hairdresser_id);
        }

        [HttpPost] [AuthorizeUsers]
        public async Task<IActionResult> DeleteAppointment(string idAppoinment, string hairdresser_id) {
            await this.repo.DeleteAppointmentAsync(int.Parse(idAppoinment));
            return Json("/Appointments/Appointments?hairdresserId=" + hairdresser_id);
        }

        [AuthorizeUsers]
        public IActionResult SelectAppointments() {
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

                dynamic services_json = new JObject();
                if (superUser) { // Solo los super usuarios pueden ver los servicios de las citas
                    int serviceCount = 0; // Enumeración de los servicios a realizar en la cita
                    foreach (Service service in services) {
                        serviceCount++;
                        string propertyName = $"service_{serviceCount}";
                        string finalValue = service.Name + " (" + service.Price + "€)";
                        services_json.Add(propertyName, finalValue);
                    }
                }

                var element = new {
                    title = userName,
                    start = date + app.Time.ToString(@"hh\:mm") + ":00",
                    end = date + this.CalculateEndAppointment(app.Time, timeAprox),
                    extendedProps = services_json,
                    description = (superUser) ? ("Precio Total: " + price) : "",
                    appoinmentId = app.AppointmentId,
                    hairdresserId = app.HairdresserId
                };

                appointments_json.Add(element); // Almacenamos cada elemento en el JSON a devolver
            }
            return appointments_json;
        }

        private string CalculateEndAppointment(TimeSpan start, int duration) {
            TimeSpan end = start.Add(TimeSpan.FromMinutes(duration));
            return end.ToString(@"hh\:mm") + ":00";
        }

        private int GetMinutesByAppointment(List<Service> services) {
            int time = 0;
            foreach (Service service in services) {
                time += service.TiempoAprox;
            }
            return time;
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
