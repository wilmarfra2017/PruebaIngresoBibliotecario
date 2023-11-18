namespace PruebaIngresoBibliotecario.Domain.Aggregates.Interfaces
{
    public interface IPrestamoService<T> : IPrestamoFinder<T>, IPrestamoRepository<T>
    {
    }
}
