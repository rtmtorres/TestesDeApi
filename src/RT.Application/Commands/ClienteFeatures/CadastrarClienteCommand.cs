using MediatR;
using RT.Domain.Models;
using System;
using System.Collections.Generic;

namespace RT.Application.Commands.ClienteFeatures
{
    public sealed class CadastrarClienteCommand : IRequest
    {
        public CadastrarClienteCommand(string nome, string rg, string cpf, DateTime dataNascimento, string telefone, string email, Empresa empresa, IEnumerable<CadastrarClienteEnderecoDto> enderecos)
        {
            Nome = nome;
            Rg = rg;
            Cpf = cpf;
            DataNascimento = dataNascimento;
            Telefone = telefone;
            Email = email;
            Empresa = empresa;
            Enderecos = enderecos ?? new List<CadastrarClienteEnderecoDto>();
        }

        public string Nome { get; }
        public string Rg { get; }
        public string Cpf { get; }
        public DateTime DataNascimento { get; }
        public string Telefone { get; }
        public string Email { get; }
        public Empresa Empresa { get; }
        public IEnumerable<CadastrarClienteEnderecoDto> Enderecos { get; }

        public sealed class CadastrarClienteEnderecoDto
        {
            public CadastrarClienteEnderecoDto(string rua, string bairro, string numero, string complemento, string cep, TipoEndereco tipoEndereco, int cidadeId)
            {
                Rua = rua;
                Bairro = bairro;
                Numero = numero;
                Complemento = complemento;
                Cep = cep;
                TipoEndereco = tipoEndereco;
                CidadeId = cidadeId;
            }

            public string Rua { get; set; }
            public string Bairro { get; set; }
            public string Numero { get; set; }
            public string Complemento { get; set; }
            public string Cep { get; set; }
            public TipoEndereco TipoEndereco { get; set; }
            public int CidadeId { get; set; }
        }
    }
}
