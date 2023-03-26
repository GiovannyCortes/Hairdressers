using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hairdressers.Models {
    [Table("USERS")]
    public class User {

        [Key][Column("user_id")]
        public int UserId { get; set; }
        
        [Column("password")]
        public byte[] Password { get; set; }

        [Column("password_read")]
        public string PasswordRead { get; set; }

        [Column("salt")]
        public string Salt { get; set; }

        [Column("name")]
        public string Name { get; set; }
        
        [Column("last_name")]
        public string? LastName { get; set; }
        
        [Column("number_phone")]
        public string? Phone { get; set; }
        
        [Column("email")]
        public string Email { get; set; }
        
        [Column("email_validated")]
        public bool EmailConfirmed { get; set; }

        [Column("temp_token")]
        public string? TempToken { get; set; }

    }
}
