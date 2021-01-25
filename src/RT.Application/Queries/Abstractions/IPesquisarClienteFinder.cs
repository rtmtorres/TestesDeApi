using System.Threading.Tasks;

namespace RT.Application.Queries.Abstractions
{
    public interface IPesquisarClienteFinder
    {
        Task<PesquisarClienteResponse> Pesquisar(PesquisarClienteRequest pesquisarClienteRequest);
    }
}
