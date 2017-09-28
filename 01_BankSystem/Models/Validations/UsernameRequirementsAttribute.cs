namespace BankSystem.Models.Validations
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class UsernameRequirementsAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string username = value as string;
            Regex regex = new Regex("^[^\\d]([A-Za-z0-9]+){3}$");

            return regex.IsMatch(username);
        }
    }
}