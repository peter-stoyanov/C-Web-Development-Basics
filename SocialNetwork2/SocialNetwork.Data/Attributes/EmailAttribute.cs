using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SocialNetwork.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class EmailAttribute : ValidationAttribute
    {
        /// <summary>
        /// Vaalidates email address
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            string email = value as string;

            Regex regex = new Regex("^[^_.-].+[^_.-]@\\w+.\\w+$");

            return regex.Match(email).Success;
        }
    }
}