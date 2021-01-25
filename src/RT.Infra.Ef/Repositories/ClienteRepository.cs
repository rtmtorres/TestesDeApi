using Microsoft.EntityFrameworkCore;
using RT.Domain.Models;
using RT.Domain.Repositories;
using RT.Infra.Ef.Contexts;
using System.Linq;
using System.Threading.Tasks;

namespace RT.Infra.Ef.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _appDbContext;

        public ClienteRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Task Alterar(ClienteAggregate clienteAggregate)
        {
            _appDbContext.Entry(clienteAggregate).State = EntityState.Modified;

            foreach (var item in clienteAggregate.EnderecosParaRemover)
            {
                _appDbContext.Entry(item).State = EntityState.Deleted;
                _appDbContext.Entry(item.Endereco).State = EntityState.Deleted;
            }

            return Task.CompletedTask;
        }

        public async Task IncluirAsync(ClienteAggregate aggregateRoot)
        {
            await _appDbContext.Clientes.AddAsync(aggregateRoot);
        }

        public ClienteAggregate ObterPorId(int id)
        {
            var task = ObterPorIdAsync(id);

            return task.GetAwaiter().GetResult();
        }

        public async Task<ClienteAggregate> ObterPorIdAsync(int id)
        {
            return await _appDbContext
               .Clientes
               .Include(t => t.Enderecos)
               .ThenInclude(t => t.Endereco)
               .SingleOrDefaultAsync(t => t.ClienteId == id);
        }

        public Task RemoverAsync(ClienteAggregate aggregate)
        {
            foreach (var item in aggregate.Enderecos)
            {
                _appDbContext.Entry(item.Endereco).State = EntityState.Deleted;
            }
            _appDbContext.Clientes.Remove(aggregate);

            return Task.CompletedTask;
        }

        public bool VerificaSeClienteJaCadastrado(string cpf, Empresa empresa, int? clienteId = null)
        {
            var query = _appDbContext
                 .Clientes.Where(t => t.Cpf.Numero == cpf && t.Empresa == empresa);


            if (clienteId.HasValue)
            {
                query = query.Where(q => q.ClienteId != clienteId.Value);
            }

            return query.Any();
        }
    }
}
