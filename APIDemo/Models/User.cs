using System.ComponentModel.DataAnnotations;

namespace APIDemo.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public string? BankId { get; set; }

        public Gender Gender { get; set; }

        public string? Password { get; set; }

    }
}
