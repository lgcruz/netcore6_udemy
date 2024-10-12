using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiPrueba.Validations;

namespace WebApiPrueba.Entidades
{
    public class Autor: IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Nombre es requerido")]
        [StringLength(maximumLength: 120, ErrorMessage = "El campo {0} no debe ser de mas de {1}")]
        // [FirstLetterUppercase]
        public string Nombre { get; set; }
        // [Range(18, 100)]
        // [NotMapped]
        // public int Edad { get; set; }
        // [CreditCard]
        // [NotMapped]
        // public int creditnumber { get; set; }

        // public int Menor { get; set; }
        // public int Mayor { get; set; }
        public List<Libro> Libros { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(!string.IsNullOrEmpty(Nombre)) {
                var primeraLetra = Nombre[0].ToString();

                if (primeraLetra != primeraLetra.ToUpper()) {
                    yield return new ValidationResult("La primera letra del nombre debe ser Mayúscula", 
                        new string[] { nameof(Nombre) });
                }
            }

            // if (Menor > Mayor) {
            //     yield return new ValidationResult("Este campo no puede ser mayor que el campo Mayor", new string[] { nameof(Menor) });
            // }
        }
    } 
}

