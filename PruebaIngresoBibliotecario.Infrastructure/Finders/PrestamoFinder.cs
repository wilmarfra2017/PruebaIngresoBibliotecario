using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PruebaIngresoBibliotecario.Domain.Aggregates;
using PruebaIngresoBibliotecario.Domain.Aggregates.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaIngresoBibliotecario.Infrastructure.Finders
{
    public class PrestamoFinder : IPrestamoFinder<Prestamo>
    {

        private readonly PersistenceContext _dbContext;
        private readonly ILogger<PrestamoFinder> _logger;

        public PrestamoFinder(PersistenceContext dbContext, ILogger<PrestamoFinder> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Prestamo> FindByIsbnAsync(Guid isbnId)
        {
            _logger.Log(LogLevel.Information, "Metodo FindByIsbnAsync - Finder - Infraestructura");
            return await _dbContext.Prestamos.FirstOrDefaultAsync(p => p.Isbn == isbnId);
        }

        public Task<bool> GuestUserHasLoan(string identificacionUsuario)
        {
            _logger.Log(LogLevel.Information, "Metodo GuestUserHasLoan - Finder - Infraestructura");
            return Task.FromResult(_dbContext.Prestamos.Any(u => u.IdentificacionUsuario == identificacionUsuario && u.TipoUsuario == 3));
        }
    }
}
