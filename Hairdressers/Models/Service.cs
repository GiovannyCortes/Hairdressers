using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hairdressers.Models {
    [Table("SERVICES")]
    public class Service {
        
        [Key][Column("service_id")]
        public int ServiceId { get; set; }
        
        [Column("hairdresser_id")]
        public int HairdresserId { get; set; }
        
        [Column("name")]
        public string Name { get; set; }
        
        [Column("price")]
        public decimal Price { get; set; }
        
        [Column("daprox")]
        public int TiempoAprox { get; set; }

    }
}
