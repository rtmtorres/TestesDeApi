using FluentAssertions;
using RT.Domain;
using RT.Domain.Exceptions;
using RT.Domain.Models;
using RT.UnitTests.Builders.Entities;
using System;
using System.Linq;
using Xunit;

namespace RT.UnitTests.Domain.Models.ClienteAggregateTests
{
    public class ClienteAggregateUnitTests
    {
        private ClienteAggregateTestBuilder _clienteAggregateTestBuilder;
        private EnderecoTestBuilder _enderecoBuilder;

        public ClienteAggregateUnitTests()
        {
            _clienteAggregateTestBuilder = new ClienteAggregateTestBuilder();
            _enderecoBuilder = new EnderecoTestBuilder();
        }


        [Fact]
        public void Deve_ser_invalido_cadastrar_um_cliente_com_cpf_falso()
        {
            //arrange
            var cpfFalso = "00000000001";

            var cliente = _clienteAggregateTestBuilder.ComCpf(cpfFalso);


            //act
            Action criarCliente = () => _clienteAggregateTestBuilder.Build();


            //assert
            criarCliente.Should().Throw<InvalidArgumentException>(Constantes.CPF_INVALIDO);
        }

        [Fact]
        public void Deve_ser_invalido_cadastrar_um_cliente_com_cpf_vazio()
        {
            //arrange
            var cpfFalso = string.Empty;

            var cliente = _clienteAggregateTestBuilder.ComCpf(cpfFalso);

            //act
            Action criarCliente = () => _clienteAggregateTestBuilder.Build();

            //assert
            criarCliente.Should().Throw<InvalidArgumentException>(Constantes.CPF_INVALIDO);
        }

        [Fact]
        public void Deve_ser_valido_cadastrar_um_cliente_com_dados_corretos()
        {
            //arrange
            var cliente = _clienteAggregateTestBuilder.Build();

            //act
            var clienteNovo = ClienteAggregate.CriarCliente(
                cliente.Nome.Valor, cliente.Rg, cliente.Cpf.Numero, cliente.DataNascimento,
                cliente.Telefone, cliente.Email, cliente.Empresa,
                cliente.Enderecos.Select(q =>
                    new Endereco(
                        q.Endereco.Rua, q.Endereco.Bairro, q.Endereco.Numero,
                        q.Endereco.Complemento, q.Endereco.Cep, q.Endereco.TipoEndereco,
                        q.Endereco.CidadeId)));

            //assert
            clienteNovo.Nome.Valor.Should().Be(cliente.Nome.Valor);
            clienteNovo.Rg.Should().Be(cliente.Rg);
            clienteNovo.Cpf.Numero.Should().Be(cliente.Cpf.Numero);
            clienteNovo.DataNascimento.Should().Be(cliente.DataNascimento);
            clienteNovo.Telefone.Should().Be(cliente.Telefone);
            clienteNovo.Email.Should().Be(cliente.Email);
            clienteNovo.Empresa.Should().Be(cliente.Empresa);
            clienteNovo.Enderecos.Should().HaveCount(cliente.Enderecos.Count());
        }


        [Fact]
        public void Deve_ser_invalido_cadastrar_um_cliente_com_dois_tipos_de_endereco_duplicado()
        {
            //arrange
            var enderecoComercial1 = _enderecoBuilder.ComId(1).ComTipoEndereco(TipoEndereco.EnderecoComercial).Build();
            var enderecoComercial2 = _enderecoBuilder.ComId(2).ComTipoEndereco(TipoEndereco.EnderecoComercial).Build();

            _clienteAggregateTestBuilder
                .LimparEnderecos()
                .ComEndereco(enderecoComercial1)
                .ComEndereco(enderecoComercial2);

            //act           
            Action criarCliente = () => _clienteAggregateTestBuilder.Build();

            //assert
            criarCliente.Should().Throw<InvalidArgumentException>(Constantes.ENDERECO_COM_TIPO_REPETIDO);
        }

        [Fact]
        public void Deve_ser_invalido_alterar_cpf_vazio_ou_nulo()
        {
            //arrange
            var cliente = _clienteAggregateTestBuilder.Build();
            var cpfFalso = string.Empty;

            //act
            Action alterarCliente = () => cliente.AlterarDados(cliente.Nome.Valor, cliente.Rg, cpfFalso, cliente.DataNascimento, cliente.Telefone, cliente.Email, cliente.Empresa, cliente.Enderecos.Select(q => q.Endereco));

            //assert
            alterarCliente.Should().Throw<InvalidArgumentException>(Constantes.CPF_INVALIDO);
        }
    }
}
