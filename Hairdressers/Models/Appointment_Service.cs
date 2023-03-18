using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hairdressers.Models {
    [Table("APPOINTMENT_SERVICES")]
    public class Appointment_Service {

        [Key] [Column("appointment_id")]
        public int AppointmentId { get; set; }
        
        [Column("service_id")]
        public int ServiceId { get; set; }

        [ForeignKey(nameof(AppointmentId))]
        public virtual Appointment Appointment { get; set; }

        [ForeignKey(nameof(ServiceId))]
        public virtual Service Service { get; set; }

    }
}
