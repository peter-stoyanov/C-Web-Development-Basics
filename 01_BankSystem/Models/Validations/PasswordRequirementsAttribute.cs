namespace BankSystem.Models.Validations
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class PasswordRequirementsAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string password = value as string;

            return password.Any(char.IsLower) && password.Any(char.IsUpper) && password.Any(char.IsDigit);
        }
    }
}