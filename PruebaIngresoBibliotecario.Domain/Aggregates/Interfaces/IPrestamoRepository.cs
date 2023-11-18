using System.Threading.Tasks;

namespace PruebaIngresoBibliotecario.Domain.Aggregates.Interfaces
{
    public interface IPrestamoRepository<in T>
    {
        public Task InsertAsync(T entity);
    }
}
