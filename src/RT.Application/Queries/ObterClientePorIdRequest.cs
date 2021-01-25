using MediatR;

namespace RT.Application.Queries
{
    public class ObterClientePorIdRequest : IRequest<ObterClientePorIdResponse>
    {
        public ObterClientePorIdRequest(int clienteId)
        {
            ClienteId = clienteId;
        }

        public int ClienteId { get; }
    }
}
