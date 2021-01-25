using RT.Application.Commands.ClienteFeatures;
using RT.Domain.Models;
using System;
using System.Collections.Generic;
using static RT.Application.Commands.ClienteFeatures.AtualizarClienteCommand;

namespace RT.Api.Models
{
    /// <summary>
    /// Representa uma requisição para atualizar cliente
    /// </summary>
    public class AtualizarClienteRequest
    {
        /// <summary>
        /// Nome da pessoa
        /// </summary>
        /// <example>Renan Torres</example>
        public string Nome { get; set; }
        /// <summary>
        /// Rg da pessoa que voce esta cadastrando
        /// </summary>
        /// <example>1112223445</example>
        public string Rg { get; set; }
        /// <summary>
        /// CPF da pessoa que voce esta cadastrando
        /// </summary>
        /// <example>00011122233</example>
        public string Cpf { get; set; }
        /// <summary>
        /// Dawta de nascimento da pessoa
        /// </summary>
        /// <example>1990-01-20</example>
        public DateTime DataNascimento { get; set; }
        /// <summary>
        /// Telefone da pessoa
        /// </summary>
        /// <example>65981111111</example>
        public string Telefone { get; set; }
        /// <summary>
        /// E-mail da pessoa
        /// </summary>
        /// <example>renan.torres87@gmail.com</example>
        public string Email { get; set; }
        /// <summary>
        /// Empresa que voce ta cadastrando a pessoa (1-Carrefour, 2-Atacadao)
        /// </summary>
        /// <remarks>Somente um CPF por empresa</remarks>
        /// <example>1</example>
        public Empresa Empresa { get; set; }

        /// <summary>
        /// Lista de endereços da pessoa
        /// </summary>
        public IEnumerable<AtualizarClienteEnderecoDto> Enderecos { get; set; }


        /// <summary>
        /// Responsável por transformar Requisiçao da aplicaçao em commando.
        /// </summary>
        /// <returns></returns>
        public AtualizarClienteCommand Map(int clienteId)
        {
            return new AtualizarClienteCommand(clienteId, Nome, Rg, Cpf, DataNascimento, Telefone, Email, Empresa, Enderecos);
        }
    }
}
