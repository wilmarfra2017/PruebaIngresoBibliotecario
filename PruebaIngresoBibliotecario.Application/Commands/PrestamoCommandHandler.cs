using MediatR;
using PruebaIngresoBibliotecario.Application.Dto;
using PruebaIngresoBibliotecario.Domain.Aggregates;
using PruebaIngresoBibliotecario.Domain.Aggregates.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PruebaIngresoBibliotecario.Application.Commands
{
    public class PrestamoCommandHandler : IRequestHandler<PrestamoCommand, PrestamoResponsePost>
    {        
        private readonly IPrestamoService<Prestamo> _prestamoService;

        public PrestamoCommandHandler(IPrestamoService<Prestamo> prestamoService)
        {
            _prestamoService = prestamoService;
        }

        public async Task<PrestamoResponsePost> Handle(PrestamoCommand request, CancellationToken cancellationToken)
        {
            var prestamo = new Prestamo
            {
                Isbn = request.Isbn,
                IdentificacionUsuario = request.IdentificacionUsuario,
                TipoUsuario = request.TipoUsuario,
                FechaMaximaDevolucion = request.FechaMaximaDevolucion
            };

            if (prestamo.TipoUsuario == 3 && await _prestamoService.GuestUserHasLoan(prestamo.IdentificacionUsuario))
            {
                throw new UserHasLoanException(prestamo.IdentificacionUsuario);
            }

            await _prestamoService.InsertAsync(prestamo);

            return new PrestamoResponsePost
            {
                Id = prestamo.Isbn,
                FechaMaximaDevolucion = prestamo.FechaMaximaDevolucion
            };
        }
    }
}
