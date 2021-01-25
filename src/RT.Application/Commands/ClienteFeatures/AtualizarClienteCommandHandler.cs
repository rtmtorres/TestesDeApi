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
    public class AtualizarClienteCommandHandler : IRequestHandler<AtualizarClienteCommand>
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IAtualizarClienteCommandValidator _validationRules;
        private readonly IUnitOfWork _unitOfWork;

        public AtualizarClienteCommandHandler(
            IClienteRepository clienteRepository,
            IAtualizarClienteCommandValidator validationRules,
            IUnitOfWork unitOfWork)
        {
            _clienteRepository = clienteRepository;
            _validationRules = validationRules;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(AtualizarClienteCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _validationRules.Validate(request);
            if (validationResult.Errors.Any())
            {
                throw new CommandValidatorException(validationResult.GetConcatenatedMessages());
            }

            var cliente = await _clienteRepository.ObterPorIdAsync(request.ClienteId);


            cliente.AlterarDados(
                request.Nome, request.Rg, request.Cpf,
                request.DataNascimento, request.Telefone,
                request.Email, request.Empresa,
                request.Enderecos.Select(t => new Endereco(t.EnderecoId, t.Rua, t.Bairro, t.Numero, t.Complemento, t.Cep, t.TipoEndereco, t.CidadeId)));


            await _clienteRepository.Alterar(cliente);

            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }
}
