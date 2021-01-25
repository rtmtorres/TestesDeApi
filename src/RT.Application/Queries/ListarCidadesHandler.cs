using MediatR;
using RT.Application.Queries.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace RT.Application.Queries
{
    public class ListarCidadesHandler : IRequestHandler<ListarCidadesRequest, ListarCidadesResponse>
    {
        private readonly IListarCidadesFinder listarCidadesFinder;

        public ListarCidadesHandler(IListarCidadesFinder listarCidadesFinder)
        {
            this.listarCidadesFinder = listarCidadesFinder;
        }
        public async Task<ListarCidadesResponse> Handle(ListarCidadesRequest request, CancellationToken cancellationToken)
        {
            var cidades = await listarCidadesFinder.Obter();

            return cidades;
        }
    }
}
