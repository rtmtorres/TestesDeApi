using RT.Domain.Abstractions;
using System.Threading.Tasks;

namespace RT.Domain.Repositories
{
    public interface IBaseRepository<TAggregateRoot, TId>
      where TAggregateRoot : class, IAggregateRoot
    {
        Task<TAggregateRoot> ObterPorIdAsync(TId id);
        TAggregateRoot ObterPorId(TId id);

        Task IncluirAsync(TAggregateRoot aggregateRoot);

        Task RemoverAsync(TAggregateRoot aggregate);
    }
}
