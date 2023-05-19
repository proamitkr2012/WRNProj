using System.ComponentModel.DataAnnotations;

namespace AdmissionUI.Models
{
    public class AAdharValidation : ValidationAttribute
    {
        
        protected override  ValidationResult  IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                  
                if (value ==null ) 
                    return new ValidationResult("Aadhar is required.");

                 else 
                if (value != null&& !string.IsNullOrEmpty(value.ToString()) && value.ToString().Length<12)
                    return new ValidationResult("Aadhar should be 12 digit");
                 else
                return  ValidationResult.Success;
            }
            catch (Exception ex)
            {
               
                return new ValidationResult(ex.Message);
            }


        }


         



    }
    
}
