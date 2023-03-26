using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hairdressers.Models {
    [Table("SCHEDULE_ROWS")]
    public class Schedule_Row {

        [Key] [Column("schedule_row_id")]
        public int ScheduleRowId { get; set; }
        
        [Column("schedule_id")]
        public int ScheduleId { get; set; }

        [Column("start_time")]
        public TimeSpan Start { get; set; }
        
        [Column("end_time")]
        public TimeSpan End { get; set; }
        
        [Column("monday")]
        public bool Monday { get; set; }
        
        [Column("tuesday")]
        public bool Tuesday { get; set; }
        
        [Column("wednesday")]
        public bool Wednesday  { get; set; }
        
        [Column("thursday")]
        public bool Thursday { get; set; }
        
        [Column("friday")]
        public bool Friday { get; set; }
        
        [Column("saturday")]
        public bool Saturday { get; set; }
        
        [Column("sunday")]
        public bool Sunday { get; set; }

    }
}
