using MediatR;
using RT.Domain;
using RT.Domain.Exceptions;
using RT.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace RT.Application.Commands.ClienteFeatures
{
    public class RemoverClienteCommandHandler : IRequestHandler<RemoverClienteCommand>
    {
        private readonly IClienteRepository clienteRepository;
        private readonly IUnitOfWork unitOfWork;

        public RemoverClienteCommandHandler(IClienteRepository clienteRepository,
            IUnitOfWork unitOfWork)
        {
            this.clienteRepository = clienteRepository;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(RemoverClienteCommand request, CancellationToken cancellationToken)
        {
            var clienteAggregate = await clienteRepository.ObterPorIdAsync(request.ClienteId);

            if (clienteAggregate == null)
                throw new InvalidArgumentException(Constantes.CLIENTE_NAO_ENCONTRADO);


            await clienteRepository.RemoverAsync(clienteAggregate);

            await unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }
}
