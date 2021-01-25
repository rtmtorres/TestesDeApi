using MediatR;
using RT.Application.Abstractions.Validators;
using RT.Application.Exceptions;
using RT.Application.Queries.Abstractions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RT.Application.Queries
{
    public class PesquisarClienteHandler : IRequestHandler<PesquisarClienteRequest, PesquisarClienteResponse>
    {
        private readonly IPesquisarClienteRequestValidator _validationRules;
        private readonly IPesquisarClienteFinder _pesquisarClienteFinder;

        public PesquisarClienteHandler(
            IPesquisarClienteRequestValidator validationRules,
            IPesquisarClienteFinder pesquisarClienteFinder)
        {
            _validationRules = validationRules;
            _pesquisarClienteFinder = pesquisarClienteFinder;
        }



        public async Task<PesquisarClienteResponse> Handle(PesquisarClienteRequest request, CancellationToken cancellationToken)
        {
            var validationResult = _validationRules.Validate(request);
            if (validationResult.Errors.Any())
            {
                throw new CommandValidatorException(validationResult.GetConcatenatedMessages());
            }

            var result = await _pesquisarClienteFinder.Pesquisar(request);

            return result;
        }
    }
}
