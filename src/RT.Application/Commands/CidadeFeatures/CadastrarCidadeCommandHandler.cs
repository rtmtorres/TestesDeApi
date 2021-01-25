using MediatR;
using RT.Domain;
using RT.Domain.Models;
using RT.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace RT.Application.Commands.CidadeFeatures
{
    public class CadastrarCidadeCommandHandler : IRequestHandler<CadastrarCidadeCommand>
    {
        private readonly ICidadeRepository cidadeRepository;
        private readonly IUnitOfWork unitofwork;

        public CadastrarCidadeCommandHandler(
            ICidadeRepository cidadeRepository,
            IUnitOfWork unitofwork)
        {
            this.cidadeRepository = cidadeRepository;
            this.unitofwork = unitofwork;
        }
        public async Task<Unit> Handle(CadastrarCidadeCommand request, CancellationToken cancellationToken)
        {
            var cidade = new CidadeAggregate(request.UfSigla, request.Nome);

            await cidadeRepository.IncluirAsync(cidade);
            await unitofwork.CommitAsync();

            return Unit.Value;
        }
    }
}
