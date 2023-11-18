using System;
using System.Threading.Tasks;

namespace PruebaIngresoBibliotecario.Domain.Aggregates.Interfaces
{
    public interface IPrestamoFinder<T>
    {
        public Task<bool> GuestUserHasLoan(string identificacionUsuario);

        public Task<Prestamo> FindByIsbnAsync(Guid isbnId);
    }
}
