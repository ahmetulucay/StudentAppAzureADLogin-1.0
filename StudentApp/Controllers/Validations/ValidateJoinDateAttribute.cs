
using System.ComponentModel.DataAnnotations;
using StudentApp.Models;

namespace StudentApp.Controllers.Validations;
public class ValidateAddJoinDateAttribute : ValidationAttribute
{
    public new string ErrorMessage = "Registration date can not be greater than current date";
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        var model = (AddStudentRequest)validationContext.ObjectInstance;

        if (model.RegistrationDate < DateTime.UtcNow) return ValidationResult.Success;

        return new ValidationResult(ErrorMessage);
    }
}

public class ValidateUpdateJoinDateAttribute : ValidationAttribute
{
    public new string ErrorMessage = "Registration date can not be greater than current date";
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        var model = (UpdateStudentRequest)validationContext.ObjectInstance;

        if (model.RegistrationDate < DateTime.UtcNow) return ValidationResult.Success;

        return new ValidationResult(ErrorMessage);
    }
}

