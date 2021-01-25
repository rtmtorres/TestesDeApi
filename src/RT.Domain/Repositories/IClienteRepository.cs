using RT.Domain.Models;
using System.Threading.Tasks;

namespace RT.Domain.Repositories
{
    public interface IClienteRepository : IBaseRepository<ClienteAggregate, int>
    {
        bool VerificaSeClienteJaCadastrado(string cpf, Empresa empresa, int? clienteId = null);

        Task Alterar(ClienteAggregate clienteAggregate);

    }
}
