using System.ComponentModel.DataAnnotations;

namespace StudentApp.Controllers.Validations;

public class IsNotNullOrEmptyAttribute : ValidationAttribute
{
    public override bool IsValid(object? value) => value is not null && !value.Equals(string.Empty);
}