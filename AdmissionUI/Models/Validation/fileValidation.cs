 
using System.ComponentModel.DataAnnotations;

namespace AdmissionUI.Models.Validation
{
 	public class PhotoVerification : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			try
			{
               // var student = (EntranceForm)validationContext.ObjectInstance;
				IFormFile formFile = value as IFormFile;
                if (formFile != null) {
                    return ValidationResult.Success;

				}
				 return (formFile == null && formFile.FileName.ToLower() != "photo.jpg"  ) ? ValidationResult.Success : new ValidationResult(GetErrorMessage());
				 
				 

			}
			catch (Exception ex)
			{
				return new ValidationResult(ex.Message);
			}


		}

		public string GetErrorMessage()
		{
			return $"Please select Photo !.";
		}


	}


	 



public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)

        {
            var file = value as IFormFile;
            if (file != null)
            {
                if (file.Length > _maxFileSize)
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"Maximum allowed file size is {_maxFileSize} bytes.";
        }
    }


    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;
        public AllowedExtensionsAttribute(string   extensions)
        {
            _extensions = extensions.Split(",");
        }

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!_extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"This photo extension is not allowed!";
        }
    }


    /*public class UserViewModel
    {
        [Required(ErrorMessage = "Please select a file.")]
        [DataType(DataType.Upload)]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        public IFormFile Photo { get; set; }
    }*/



}
