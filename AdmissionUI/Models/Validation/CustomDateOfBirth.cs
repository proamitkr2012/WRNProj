 
using System.ComponentModel.DataAnnotations;

namespace AdmissionUI.Models 
{
    public class CustomDateOfBirth : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
               // var student = (EntranceForm)validationContext.ObjectInstance;
                if (value  == null)
                    return new ValidationResult("Date of Birth is required.");

                var age = DateTime.Today.Year - Convert.ToDateTime(value).Year;

                return (age >= 12) ? ValidationResult.Success : new ValidationResult("Student should be at least 12 years old.");
            }
            catch (Exception ex)
            {
                return new  ValidationResult(ex.Message);
            }

 
        }
    }


    public class InitialValue : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var errorMessage = FormatErrorMessage(validationContext.DisplayName);
            
            return (value != null && (value.ToString().Trim() != "0" && !string.IsNullOrEmpty(value.ToString()))) ? ValidationResult.Success : new ValidationResult(errorMessage);


        }
    }
}
