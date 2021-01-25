using MediatR;
using RT.Application.Exceptions;
using RT.Domain;
using RT.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RT.Application.Queries
{
    public class ObterClientePorIdHandler : IRequestHandler<ObterClientePorIdRequest, ObterClientePorIdResponse>
    {
        private readonly IClienteRepository _clienteRepository;

        public ObterClientePorIdHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public Task<ObterClientePorIdResponse> Handle(ObterClientePorIdRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return ObterCliente(request);
        }


        private async Task<ObterClientePorIdResponse> ObterCliente(ObterClientePorIdRequest request)
        {
            var cliente = await _clienteRepository.ObterPorIdAsync(request.ClienteId);

            if (cliente == null)
            {
                throw new NotFoundException(Constantes.CLIENTE_NAO_ENCONTRADO);

            }

            return ObterClientePorIdResponse.From(cliente);
        }
    }
}
