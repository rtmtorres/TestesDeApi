using MediatR;
using RT.Application.Abstractions.Validators;
using RT.Application.Exceptions;
using RT.Domain;
using RT.Domain.Models;
using RT.Domain.Repositories;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RT.Application.Commands.ClienteFeatures
{
    public class CadastrarClienteCommandHandler : IRequestHandler<CadastrarClienteCommand>
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly ICadastrarClienteCommandValidator _validationRules;
        private readonly IUnitOfWork _unitOfWork;

        public CadastrarClienteCommandHandler(
            IClienteRepository clienteRepository,
            ICadastrarClienteCommandValidator validationRules,
            IUnitOfWork unitOfWork)
        {
            _clienteRepository = clienteRepository;
            _validationRules = validationRules;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(CadastrarClienteCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _validationRules.Validate(request);
            if (validationResult.Errors.Any())
            {
                throw new CommandValidatorException(validationResult.GetConcatenatedMessages());
            }

            var cliente = ClienteAggregate.CriarCliente(
                request.Nome,
                request.Rg,
                request.Cpf,
                request.DataNascimento,
                request.Telefone,
                request.Email,
                request.Empresa,
                request.Enderecos.Select(t => new Endereco(t.Rua, t.Bairro, t.Numero, t.Complemento, t.Cep, t.TipoEndereco, t.CidadeId)));

            await _clienteRepository.IncluirAsync(cliente);

            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }
}

