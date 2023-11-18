using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PruebaIngresoBibliotecario.Domain.Aggregates;
using System.Threading.Tasks;

namespace PruebaIngresoBibliotecario.Api
{
    public class PersistenceContextDeprecate : DbContext
    {
        public DbSet<Prestamo> Prestamos { get; set; }

        private readonly IConfiguration Config;

        public PersistenceContextDeprecate(DbContextOptions<PersistenceContextDeprecate> options, IConfiguration config) : base(options)
        {
            Config = config;
        }

        public async Task CommitAsync()
        {
            await SaveChangesAsync().ConfigureAwait(false);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Config.GetValue<string>("SchemaName"));

            base.OnModelCreating(modelBuilder);
        }
    }
}
