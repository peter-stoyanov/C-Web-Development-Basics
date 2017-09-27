using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace SocialNetwork.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class PasswordAttribute : ValidationAttribute
    {
        /// <summary>
        /// Validates that the password has at least 1 uppercase, 1 lowercase letter, 1 digit and 1 special symbol 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            bool validPassword = false;

            string reason = String.Empty;

            string Password = value == null ? String.Empty : value.ToString();

            if (String.IsNullOrEmpty(Password) || Password.Length < 6 || Password.Length > 50)
            {
                reason = "Your new password must be between 6 and 50 characters long. ";
            }
            else
            {
                Regex rgxCapitalLetter = new Regex("[A-Z]");
                Regex rgxSmallLetter = new Regex("[a-z]");
                Regex rgxDigit = new Regex("[0-9]");
                Regex rgxSpecialSymbol = new Regex(@"[!@#$%^&*()_+<>?]");

                if (!rgxCapitalLetter.IsMatch(Password))
                {
                    reason += "Your new password must contain at least 1 capital letter.";
                }
                if (!rgxSmallLetter.IsMatch(Password))
                {
                    reason += "Your new password must contain at least 1 small letter.";
                }
                if (!rgxDigit.IsMatch(Password))
                {
                    reason += "Your new password must contain at least 1 digit.";
                }
                if (!rgxSpecialSymbol.IsMatch(Password))
                {
                    reason += "Your new password must contain at least 1 special symbol.";
                }
                else
                {
                    validPassword = true;
                }
            }
            if (validPassword)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(reason);
            }
        }
    }
}
