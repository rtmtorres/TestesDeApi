using MediatR;

namespace RT.Application.Commands.ClienteFeatures
{
    public class RemoverClienteCommand : IRequest
    {
        public RemoverClienteCommand(int clienteId)
        {
            ClienteId = clienteId;
        }

        public int ClienteId { get; }
    }
}
