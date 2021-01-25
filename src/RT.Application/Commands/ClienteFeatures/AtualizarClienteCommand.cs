using MediatR;
using RT.Domain.Models;
using System;
using System.Collections.Generic;

namespace RT.Application.Commands.ClienteFeatures
{
    public class AtualizarClienteCommand : IRequest
    {
        public AtualizarClienteCommand(
            int clienteId, string nome, string rg,
            string cpf, DateTime dataNascimento, string telefone,
            string email, Empresa empresa, IEnumerable<AtualizarClienteEnderecoDto> enderecos)
        {
            ClienteId = clienteId;
            Nome = nome;
            Rg = rg;
            Cpf = cpf;
            DataNascimento = dataNascimento;
            Telefone = telefone;
            Email = email;
            Empresa = empresa;
            Enderecos = enderecos ?? new List<AtualizarClienteEnderecoDto>();
        }

        public int ClienteId { get; set; }
        public string Cpf { get; }
        public DateTime DataNascimento { get; }
        public string Email { get; }
        public Empresa Empresa { get; }
        public IEnumerable<AtualizarClienteEnderecoDto> Enderecos { get; }
        public string Nome { get; }
        public string Rg { get; }
        public string Telefone { get; }

        public sealed class AtualizarClienteEnderecoDto
        {
            public AtualizarClienteEnderecoDto(
                int enderecoId, string rua, string bairro,
                string numero, string complemento, string cep,
                TipoEndereco tipoEndereco, int cidadeId)
            {
                Rua = rua;
                Bairro = bairro;
                Numero = numero;
                Complemento = complemento;
                Cep = cep;
                TipoEndereco = tipoEndereco;
                CidadeId = cidadeId;
                EnderecoId = enderecoId;
            }
            public string Bairro { get; set; }
            public string Cep { get; set; }
            public int CidadeId { get; set; }
            public string Complemento { get; set; }
            public int EnderecoId { get; set; }
            public string Numero { get; set; }
            public string Rua { get; set; }
            public TipoEndereco TipoEndereco { get; set; }
        }
    }
}
