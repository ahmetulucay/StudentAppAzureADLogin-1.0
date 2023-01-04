using System.ComponentModel.DataAnnotations;
using StudentApp.Models;

namespace StudentApp.Controllers.Validations;

public class ValidateNotNullOrEmptyAttribute : ValidationAttribute
{
    public new string ErrorMessage = "Adress can not be null or empty";
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        var model = (Students)validationContext.ObjectInstance;
        if(model.Address is null || model.Address.Equals(string.Empty))
            return new ValidationResult(ErrorMessage);
        return ValidationResult.Success;
    }
}