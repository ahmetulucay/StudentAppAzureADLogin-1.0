
using System.ComponentModel.DataAnnotations;
using StudentApp.Models;

namespace StudentApp.Controllers.Validations;

// Null check
public class ValidateNotNullOrEmptyAttribute : ValidationAttribute
{
    public new string? ErrorMessage;
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        var model = (AddStudentRequest)validationContext.ObjectInstance;
        ErrorMessage = $"{model} can not be null or empty";
        if ((model.UserName is null || model.UserName.Equals(string.Empty)) ||
        (model.FirstName is null || model.FirstName.Equals(string.Empty))   ||
        (model.SecondName is null || model.SecondName.Equals(string.Empty)) ||
        (model.LastName is null || model.LastName.Equals(string.Empty))     ||
        (model.Address is null || model.Address.Equals(string.Empty))       ||
        (model.TlfNo is null || model.TlfNo.Equals(string.Empty))           ||
        (model.School is null || model.School.Equals(string.Empty)))
            return new ValidationResult(ErrorMessage);

        return ValidationResult.Success;
    }
}


public class ValidJoinDate : ValidationAttribute
{
    public new string ErrorMessage = "Join date can not be smaller than current date";
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        DateTime _dateJoin = Convert.ToDateTime(value);

        if (_dateJoin < DateTime.Now)
        {
            return new ValidationResult(ErrorMessage);
        }
        return ValidationResult.Success;
    }
}

