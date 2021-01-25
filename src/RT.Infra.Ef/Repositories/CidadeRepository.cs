using Microsoft.EntityFrameworkCore;
using RT.Domain.Models;
using RT.Domain.Repositories;
using RT.Infra.Ef.Contexts;
using System.Threading.Tasks;

namespace RT.Infra.Ef.Repositories
{
    public class CidadeRepository : ICidadeRepository
    {
        private readonly AppDbContext _appDbContext;

        public CidadeRepository(AppDbContext appDbContext)
        {
            
            _appDbContext = appDbContext;
        }


        public async Task IncluirAsync(CidadeAggregate aggregateRoot)
        {
            await _appDbContext.Cidades.AddAsync(aggregateRoot);
        }

        public CidadeAggregate ObterPorId(int id)
        {
            var task = ObterPorIdAsync(id);

            return task.GetAwaiter().GetResult();
        }

        public async Task<CidadeAggregate> ObterPorIdAsync(int id)
        {
            return (await _appDbContext
               .Cidades
               .SingleOrDefaultAsync(t => t.CidadeId == id));
        }

        public Task RemoverAsync(CidadeAggregate aggregate)
        {
            _appDbContext.Cidades.Remove(aggregate);

            return Task.CompletedTask;
        }
    }
}
