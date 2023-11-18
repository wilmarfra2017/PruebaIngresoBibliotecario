using Microsoft.Extensions.Logging;
using PruebaIngresoBibliotecario.Domain.Aggregates;
using PruebaIngresoBibliotecario.Domain.Aggregates.Interfaces;
using System.Threading.Tasks;

namespace PruebaIngresoBibliotecario.Infrastructure.Repositories
{
    public class PrestamoRepository : IPrestamoRepository<Prestamo>
    {
        private readonly PersistenceContext _dbContext;
        private readonly ILogger<PrestamoRepository> _logger;

        public PrestamoRepository(PersistenceContext dbContext, ILogger<PrestamoRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        public async Task InsertAsync(Prestamo entity)
        {
            _logger.Log(LogLevel.Information, "Metodo InsertAsync - Repository - Infraestructura");

            _dbContext.Prestamos.Add(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
