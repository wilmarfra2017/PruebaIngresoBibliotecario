using System;

namespace PruebaIngresoBibliotecario.Application.Dto
{
    public class PrestamoResponseGet
    {
        public Guid Isbn { get; set; }
        public string IdentificacionUsuario { get; set; }
        public int TipoUsuario { get; set; }
        public DateTime FechaMaximaDevolucion { get; set; }
    }
}
