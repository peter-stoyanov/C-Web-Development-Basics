using System;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int maxFileSize;

        public MaxFileSizeAttribute(int maxFileSize)
        {
            this.maxFileSize = maxFileSize;
        }

        public override bool IsValid(object value)
        {
            if (value == null) { return true; }

            byte[] image = value as byte[];
            return image?.Length <= this.maxFileSize;
        }

        public override string FormatErrorMessage(string name)
        {
            return base.FormatErrorMessage(this.maxFileSize.ToString());
        }
    }
}