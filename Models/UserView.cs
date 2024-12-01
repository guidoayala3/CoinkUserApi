using System.ComponentModel.DataAnnotations.Schema;

namespace UserRegistrationApi.Models
{
    public class UserView
    {
        public int Id { get; set; }

        [Column("user_name")]
        public required string Name { get; set; }

        [Column("user_phone")]
        public required string Phone { get; set; }

        [Column("user_address")]
        public required string Address { get; set; }

        [Column("municipality_name")]
        public required string Municipality { get; set; }

        [Column("department_name")]
        public required string Department { get; set; }

        [Column("country_name")]
        public required string Country { get; set; }
    }
}
