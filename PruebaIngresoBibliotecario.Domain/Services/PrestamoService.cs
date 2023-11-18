using Microsoft.Extensions.Logging;
using PruebaIngresoBibliotecario.Domain.Aggregates;
using PruebaIngresoBibliotecario.Domain.Aggregates.Interfaces;
using System;
using System.Threading.Tasks;

namespace PruebaIngresoBibliotecario.Domain.Services
{
    public class PrestamoService : IPrestamoService<Prestamo>
    {
        private readonly IPrestamoFinder<Prestamo> _prestamoFinder;
        private readonly IPrestamoRepository<Prestamo> _prestamoRepository;
        private readonly ILogger<PrestamoService> _logger;

        public PrestamoService(ILogger<PrestamoService> logger, IPrestamoFinder<Prestamo> prestamoFinder, IPrestamoRepository<Prestamo> prestamoRepository)
        {
            _logger = logger;
            _prestamoFinder = prestamoFinder;
            _prestamoRepository = prestamoRepository;
        }

        public async Task<Prestamo> FindByIsbnAsync(Guid isbnId)
        {
            _logger.Log(LogLevel.Information, "Metodo FindByIsbnAsync - Servicio - Dominio");
            return await _prestamoFinder.FindByIsbnAsync(isbnId);
        }

        public async Task<bool> GuestUserHasLoan(string identificacionUsuario)
        {
            _logger.Log(LogLevel.Information, "Metodo GuestUserHasLoan - Servicio - Dominio");
            return await _prestamoFinder.GuestUserHasLoan(identificacionUsuario);
        }

        public async Task InsertAsync(Prestamo entity)
        {
            _logger.Log(LogLevel.Information, "Metodo InsertAsync - Servicio - Dominio");
            switch (entity.TipoUsuario)
            {
                case 1:
                    entity.FechaMaximaDevolucion = CalculateDueDate(DateTime.Now, 10);
                    break;
                case 2:
                    entity.FechaMaximaDevolucion = CalculateDueDate(DateTime.Now, 8);
                    break;
                 
                case 3:
                    entity.FechaMaximaDevolucion = CalculateDueDate(DateTime.Now, 7);
                    break;
            }
            
            await _prestamoRepository.InsertAsync(entity);
        }

        private DateTime CalculateDueDate(DateTime startDate, int daysToAdd)
        {
            _logger.Log(LogLevel.Information, "Metodo CalculateDueDate - Servicio - Dominio");

            DateTime dueDate = startDate;
            int daysAdded = 0;

            while (daysAdded < daysToAdd)
            {
                dueDate = dueDate.AddDays(1);

                //si no es fin de semana
                if (dueDate.DayOfWeek != DayOfWeek.Saturday && dueDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    daysAdded++;
                }
            }
            return dueDate;
        }

    }
}
