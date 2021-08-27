using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class TrimArrribute : ValidationAttribute
    {
        public string CommaSeperatedProperties { get; private set; }
        public TrimArrribute() : base() { }
     
        protected override ValidationResult IsValid (object value, ValidationContext validationContext)
        {                 
            string input = value as string;
            if (input == null || input?.TrimEnd().TrimStart() == "")
                return new ValidationResult("input mustn't be empty", new List<string> { validationContext.MemberName });           
            return ValidationResult.Success;
        }       
    }
}
