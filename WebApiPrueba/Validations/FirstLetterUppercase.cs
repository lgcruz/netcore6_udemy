using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPrueba.Validations
{
    public class FirstLetterUppercase: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext context) {
            if (value == null || string.IsNullOrEmpty(value.ToString())) {
                return ValidationResult.Success;
            }

            var primeraLetra = value.ToString()[0].ToString();
            if (primeraLetra != primeraLetra.ToUpper()) {
                return new ValidationResult("La primera letra debe ser Mayuscula");
            }
            return ValidationResult.Success;

        }
    }
}  