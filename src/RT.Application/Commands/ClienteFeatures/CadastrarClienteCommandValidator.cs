using FluentValidation;
using RT.Application.Abstractions;
using RT.Application.Abstractions.Validators;
using RT.Domain;
using RT.Domain.Repositories;
using System;
using System.Linq;

namespace RT.Application.Commands.ClienteFeatures
{
    public class CadastrarClienteCommandValidator : BaseAbstractValidator<CadastrarClienteCommand>, ICadastrarClienteCommandValidator
    {
        public CadastrarClienteCommandValidator(IClienteRepository clienteRepository, ICidadeRepository cidadeRepository)
        {
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

            RuleFor(evd => evd.DataNascimento)
                .Must(MaiorDe18)
                .WithMessage(Constantes.CLIENTE_MENOR_18);

            RuleFor(m => m.Email)
                .NotEmpty()
                .WithMessage(CampoObrigatorio);

            RuleFor(m => m)
                .Must(EhNovoUsuario)
                .WithMessage(Constantes.CLIENTE_JA_CADASTRADO);

            RuleFor(m => m)
                .Must(PossuiSomenteUmTipoDeEnderecoNosEnderecosCadastrados)
                .WithMessage(Constantes.DUPLICIDADE_TIPO_ENDERECO);

            RuleFor(m => m)
                .Must(CidadeExiste)
                .WithMessage(Constantes.CIDADE_NAO_EXISTE);


            bool EhNovoUsuario(CadastrarClienteCommand command)
            {
                var usuarioCadastrado = clienteRepository.VerificaSeClienteJaCadastrado(command.Cpf, command.Empresa);

                return !usuarioCadastrado;
            }

            bool CidadeExiste(CadastrarClienteCommand cadastrarClienteCommand)
            {
                var algumaCidadeExiste = !cadastrarClienteCommand.Enderecos?.Any(q => cidadeRepository.ObterPorId(q.CidadeId) == null);

                return algumaCidadeExiste ?? true;
            }

            bool MaiorDe18(DateTime dataNascimento)
            {
                var dataMaximaNascimento = DateTime.Today.AddYears(-18);

                return dataMaximaNascimento > dataNascimento;
            }

            bool PossuiSomenteUmTipoDeEnderecoNosEnderecosCadastrados(CadastrarClienteCommand command)
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
