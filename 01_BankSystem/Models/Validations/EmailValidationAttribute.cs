namespace BankSystem.Models.Validations
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class EmailValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string email = value as string;

            Regex regex = new Regex("^[^_.-].+[^_.-]@\\w+.\\w+$");

            return regex.Match(email).Success;
        }
    }
}