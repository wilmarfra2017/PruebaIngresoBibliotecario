using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PruebaIngresoBibliotecario.Domain.Aggregates;
using System.Threading.Tasks;


namespace PruebaIngresoBibliotecario.Infrastructure
{
    public class PersistenceContext : DbContext
    {
        
        private readonly IConfiguration Config;

        public PersistenceContext(DbContextOptions<PersistenceContext> options, IConfiguration config) : base(options)
        {
            Config = config;
        }

        public DbSet<Prestamo> Prestamos { get; set; }

        public async Task CommitAsync()
        {
            await SaveChangesAsync().ConfigureAwait(false);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {           
            base.OnModelCreating(modelBuilder);
        }
    }
}
