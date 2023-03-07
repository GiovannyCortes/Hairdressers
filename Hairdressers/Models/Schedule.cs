using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hairdressers.Models {
    [Table("SCHEDULES")]
    public class Schedule {

        [Key] [Column("schedule_id")]
        public int ScheduleId { get; set; }
        
        [Column("hairdresser_id")]
        public int HairdresserId { get; set; }
        
        [Column("name")]
        public string Name { get; set; }
        
        [Column("active")]
        public bool Active { get; set; }

    }
}
