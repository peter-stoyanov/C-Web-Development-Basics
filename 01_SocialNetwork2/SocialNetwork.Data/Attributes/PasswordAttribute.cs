using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SocialNetwork.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class PasswordAttribute : ValidationAttribute
    {
        private readonly char[] specialSymbols = new char[] { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '_', '+', '<', '>', '?' };

        /// <summary>
        /// Validates that the password has at least 1 uppercase, 1 lowercase letter, 1 digit and 1 special symbol
        /// </summary>
        /// <param name="value"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            string password = value as String;

            // if attr is applied on non-string properties return valid by MS convention
            if (password == null) { return true; }

            if (String.IsNullOrEmpty(password) || password.Length < 6 || password.Length > 50)
            {
                this.ErrorMessage = "Your new password must be between 6 and 50 characters long.";
                return false;
            }
            else
            {
                if (!password.Any(c => Char.IsUpper(c)))
                {
                    this.ErrorMessage = "Your new password must contain at least 1 uppercase letter.";
                    return false;
                }
                if (!password.Any(c => Char.IsLower(c)))
                {
                    this.ErrorMessage = "Your new password must contain at least 1 lowercase letter.";
                    return false;
                }
                if (!password.Any(c => Char.IsDigit(c)))
                {
                    this.ErrorMessage = "Your new password must contain at least 1 digit.";
                    return false;
                }
                if (!password.Any(c => this.specialSymbols.Contains(c)))
                {
                    this.ErrorMessage = "Your new password must contain at least 1 special symbol.";
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}