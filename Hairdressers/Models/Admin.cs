using System.ComponentModel.DataAnnotations.Schema;

namespace Hairdressers.Models {
    [Table("ADMINS")]
    public class Admin {

        [Column("hairdresser_id")]
        public int HairdresserId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }
        
        [Column("role")]
        public string Role { get; set; }

        [ForeignKey(nameof(HairdresserId))]
        public virtual Hairdresser Hairdresser { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

    }
}
