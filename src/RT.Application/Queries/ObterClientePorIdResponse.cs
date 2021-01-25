using RT.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RT.Application.Queries
{
    public sealed class ObterClientePorIdResponse
    {
        public ObterClientePorIdResponse(int clienteId, string nome, string rg, string cpf, DateTime dataNascimento, string telefone, string email, Empresa empresa, IEnumerable<ObterClientePorIdEnderecoResponse> enderecos)
        {
            ClienteId = clienteId;
            Nome = nome;
            Rg = rg;
            Cpf = cpf;
            DataNascimento = dataNascimento;
            Telefone = telefone;
            Email = email;
            Empresa = empresa;
            Enderecos = enderecos;
        }
        public int ClienteId { get; set; }
        public string Nome { get; }
        public string Rg { get; }
        public string Cpf { get; }
        public DateTime DataNascimento { get; }
        public string Telefone { get; }
        public string Email { get; }
        public Empresa Empresa { get; }
        public IEnumerable<ObterClientePorIdEnderecoResponse> Enderecos { get; }

        public sealed class ObterClientePorIdEnderecoResponse
        {
            public ObterClientePorIdEnderecoResponse(int enderecoId, string rua, string bairro, string numero, string complemento, string cep, TipoEndereco tipoEndereco, int cidadeId)
            {
                EnderecoId = enderecoId;
                Rua = rua;
                Bairro = bairro;
                Numero = numero;
                Complemento = complemento;
                Cep = cep;
                TipoEndereco = tipoEndereco;
                CidadeId = cidadeId;
            }

            public int EnderecoId { get; }
            public string Rua { get; }
            public string Bairro { get; }
            public string Numero { get; }
            public string Complemento { get; }
            public string Cep { get; }
            public TipoEndereco TipoEndereco { get; }
            public int CidadeId { get; }
        }

        public static ObterClientePorIdResponse From(ClienteAggregate aggregate)
        {
            var clienteResponse = new ObterClientePorIdResponse(
                aggregate.ClienteId,
                aggregate.Nome.Valor,
                aggregate.Rg,
                aggregate.Cpf.Numero,
                aggregate.DataNascimento,
                aggregate.Telefone,
                aggregate.Email,
                aggregate.Empresa,
                aggregate.Enderecos.Select(c => new ObterClientePorIdEnderecoResponse(c.EnderecoId, c.Endereco.Rua, c.Endereco.Bairro, c.Endereco.Numero, c.Endereco.Complemento, c.Endereco.Cep, c.Endereco.TipoEndereco, c.Endereco.CidadeId))
                );

            return clienteResponse;
        }
    }
}