using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace SocialNetwork.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private long maxLength;

        /// <summary>
        /// Validates that size of byte[] property is under the specified limit
        /// </summary>
        /// <param name="maxSizeInBytes"></param>
        public MaxFileSizeAttribute(long maxSizeInBytes)
        {
            this.maxLength = maxSizeInBytes;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            long proposedValue;
            try
            {
                proposedValue = (long)value;
            }
            catch (Exception)
            {
                return new ValidationResult("Provided max file size should be integer number.");
            }

            if (proposedValue <= this.maxLength)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("File size exceeds the limit.");
            }
        }

        private bool IsLong(string input)
        {
            Int64 r;
            return Int64.TryParse(input, out r);
        }
    }
}
