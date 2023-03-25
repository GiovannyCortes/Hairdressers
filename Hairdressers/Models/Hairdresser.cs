using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hairdressers.Models {
    [Table("HAIRDRESSERS")]
    public class Hairdresser {

        [Key] [Column("hairdresser_id")]
        public int HairdresserId { get; set; }
        
        [Column("name")]
        public string Name { get; set; }
        
        [Column("number_phone")]
        public string Phone { get; set; }
        
        [Column("address")]
        public string Address { get; set; }
        
        [Column("postal_code")]
        public int PostalCode { get; set; }

        [Column("token")]
        public string Token { get; set; }

    }
}
