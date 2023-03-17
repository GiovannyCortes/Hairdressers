using Hairdressers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hairdressers.Controllers {
    public class AppointmentsController : Controller {

        private IRepositoryHairdresser repo;

        public AppointmentsController(IRepositoryHairdresser repo) {
            this.repo = repo;
        }

        public IActionResult AppointmentsAdmin() {
            var jaime = new {
                            Title= "BCH237",
                            Start= "2023-03-01T10:35:00",
                            End= "2023-03-02T11:30:00",
                            ExtendedProps = new  {
                                Department= "BioChemistry"
                            },
                            Description= "Lecture"
                        };
            ViewData["JAIME"] = jaime;
            return View();
        }

    }
}
