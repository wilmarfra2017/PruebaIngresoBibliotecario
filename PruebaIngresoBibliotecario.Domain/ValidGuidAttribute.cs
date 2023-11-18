using System;
using System.ComponentModel.DataAnnotations;

namespace PruebaIngresoBibliotecario.Domain
{
    public class ValidGuidAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is null)
            {
                return new ValidationResult("El campo es requerido.");
            }

            if (value is Guid guid && guid != Guid.Empty)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("El valor no es un formato GUID válido.");
        }
    }

}
