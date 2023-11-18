using System;
using System.ComponentModel.DataAnnotations;


namespace PruebaIngresoBibliotecario.Domain.Aggregates
{
    public class Prestamo
    {
        [Key]
        [ValidGuid(ErrorMessage = "El valor no es un formato GUID válido.")]        
        public Guid Isbn { get; set; }

        [StringLength(10, ErrorMessage = "La identificación del usuario no debe superar los 10 dígitos.")]
        public string IdentificacionUsuario { get; set; }

        [Range(1, 3, ErrorMessage = "El tipo de usuario debe ser un dígito entre 1 y 3.")]
        public int TipoUsuario { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public DateTime FechaMaximaDevolucion { get; set; }
    }
}
