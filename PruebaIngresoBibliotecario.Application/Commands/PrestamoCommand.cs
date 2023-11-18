
using MediatR;
using PruebaIngresoBibliotecario.Application.Dto;
using System;

namespace PruebaIngresoBibliotecario.Application.Commands
{
    public class PrestamoCommand : IRequest<PrestamoResponsePost>
    {
        public Guid Isbn { get; set; }
        public string IdentificacionUsuario { get; set; }
        public int TipoUsuario { get; set; }
        public DateTime FechaMaximaDevolucion { get; set; }
    }
}
