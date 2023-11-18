using MediatR;
using PruebaIngresoBibliotecario.Application.Dto;
using PruebaIngresoBibliotecario.Domain.Aggregates;
using PruebaIngresoBibliotecario.Domain.Aggregates.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PruebaIngresoBibliotecario.Application.Queries
{
    public class PrestamoQueryHandler : IRequestHandler<PrestamoQuery, PrestamoResponseGet>
    {
        private readonly IPrestamoFinder<Prestamo> _prestamoFinder;

        public PrestamoQueryHandler(IPrestamoFinder<Prestamo> prestamoFinder)
        {
            _prestamoFinder = prestamoFinder;
        }

        public async Task<PrestamoResponseGet> Handle(PrestamoQuery request, CancellationToken cancellationToken)
        {
            var prestamo = await _prestamoFinder.FindByIsbnAsync(request.Isbn);

            if (prestamo == null)
            {
                return null;
            }

            return new PrestamoResponseGet
            {
                Isbn = prestamo.Isbn,
                IdentificacionUsuario = prestamo.IdentificacionUsuario,
                TipoUsuario = prestamo.TipoUsuario,
                FechaMaximaDevolucion = prestamo.FechaMaximaDevolucion
            };
        }
    }
}
