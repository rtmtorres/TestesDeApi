using FluentValidation;
using RT.Application.Abstractions;
using RT.Application.Abstractions.Validators;
using RT.Domain;
using RT.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using static RT.Application.Commands.ClienteFeatures.AtualizarClienteCommand;

namespace RT.Application.Commands.ClienteFeatures
{
    public class AtualizarClienteCommandValidator : BaseAbstractValidator<AtualizarClienteCommand>, IAtualizarClienteCommandValidator
    {
        public AtualizarClienteCommandValidator(IClienteRepository clienteRepository, ICidadeRepository cidadeRepository)
        {

            RuleFor(m => m.ClienteId)
                .Must(m => m > 0)
                .WithMessage(CampoObrigatorio);

            RuleFor(m => m.Nome)
                .NotEmpty()
                .WithMessage(CampoObrigatorio);

            RuleFor(m => m.Rg)
                .NotEmpty()
                .WithMessage(CampoObrigatorio);

            RuleFor(m => m.Cpf)
                .NotEmpty()
                .WithMessage(CampoObrigatorio);

            RuleFor(m => m.Telefone)
                .NotEmpty()
                .WithMessage(CampoObrigatorio);

            RuleFor(m => m.Email)
                .NotEmpty()
                .WithMessage(CampoObrigatorio);

            RuleFor(m => m)
                .Must(NovoUsuario)
                .WithMessage(Constantes.CLIENTE_JA_CADASTRADO);

            RuleFor(m => m)
                .Must(PossuiSomenteUmTipoDeEnderecoNosEnderecosCadastrados)
                .WithMessage(Constantes.DUPLICIDADE_TIPO_ENDERECO);

            RuleFor(m => m.Enderecos)
                .Must(CidadeExiste)
                .WithMessage(Constantes.CIDADE_NAO_EXISTE);


            bool NovoUsuario(AtualizarClienteCommand command)
            {
                var usuarioCadastrado = clienteRepository.VerificaSeClienteJaCadastrado(command.Cpf, command.Empresa, command.ClienteId);

                return !usuarioCadastrado;
            }

            bool CidadeExiste(IEnumerable<AtualizarClienteEnderecoDto> enderecos)
            {
                var algumaCidadeNaoExiste = enderecos.Any(q => cidadeRepository.ObterPorId(q.CidadeId) == null);

                return !algumaCidadeNaoExiste;
            }

            bool PossuiSomenteUmTipoDeEnderecoNosEnderecosCadastrados(AtualizarClienteCommand command)
            {
                var possuiTipoRepetido = command.Enderecos
                    .ToLookup(q => q.TipoEndereco)
                    .Select(q => new { TipoEndereco = q.Key, Quantidade = q.Count() })
                    .Any(q => q.Quantidade > 1);

                return !possuiTipoRepetido;
            }
        }



    }
}