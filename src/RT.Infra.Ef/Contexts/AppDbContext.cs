using Microsoft.EntityFrameworkCore;
using RT.Domain;
using RT.Domain.Models;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace RT.Infra.Ef.Contexts
{
    public class AppDbContext : DbContext, IUnitOfWork
    {
        static AppDbContext()
        {
        }

        public AppDbContext(
             DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ClienteAggregate> Clientes { get; set; }
        public DbSet<CidadeAggregate> Cidades { get; set; }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync();

            return result;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}