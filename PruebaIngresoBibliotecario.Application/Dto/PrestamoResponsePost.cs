using System;

namespace PruebaIngresoBibliotecario.Application.Dto
{
    public class PrestamoResponsePost
    {
        public Guid Id { get; set; }
        public DateTime FechaMaximaDevolucion { get; set; }
    }
}
