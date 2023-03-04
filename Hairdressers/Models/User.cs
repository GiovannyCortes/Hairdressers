using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hairdressers.Models {
    [Table("USERS")]
    public class User {

        [Key][Column("user_id")]
        public int UserId { get; set; }
        
        [Column("password")]
        public string Password { get; set; }
        
        [Column("name")]
        public string Name { get; set; }
        
        [Column("last_name")]
        public string LastName { get; set; }
        
        [Column("number_phone")]
        public string Phone { get; set; }
        
        [Column("email")]
        public string Email { get; set; }
        
        [Column("email_validated")]
        public bool EmailConfirmed { get; set; }

        public User() {
            this.UserId = 0;
            this.Password = "";
            this.Name = "";
            this.LastName = "";
            this.Phone = "";
            this.Email = "";
            this.EmailConfirmed = false;
        }

        public User(int userId, string password, string name, string lastName, string phone, string email, bool emailConfirmed) {
            this.UserId = userId;
            this.Password = password;
            this.Name = name;
            this.LastName = lastName;
            this.Phone = phone;
            this.Email = email;
            this.EmailConfirmed = emailConfirmed;
        }

    }
}
