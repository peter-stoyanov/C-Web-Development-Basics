namespace BankSystem.Models
{
    using BankSystem.Models.Validations;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [UsernameRequirements]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        [PasswordRequirements]
        public string Password { get; set; }

        [Required]
        [EmailValidation]
        public string EmailAddress { get; set; }

        public ICollection<Account> Accounts { get; set; } = new HashSet<Account>();
    }
}