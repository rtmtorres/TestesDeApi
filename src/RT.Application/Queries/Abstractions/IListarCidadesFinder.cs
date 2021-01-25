using System.Threading.Tasks;

namespace RT.Application.Queries.Abstractions
{
    public interface IListarCidadesFinder
    {
        Task<ListarCidadesResponse> Obter();
    }
}
