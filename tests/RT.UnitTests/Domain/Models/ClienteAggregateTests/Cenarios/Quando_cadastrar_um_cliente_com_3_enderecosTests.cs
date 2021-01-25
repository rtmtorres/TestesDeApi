using FluentAssertions;
using RT.Domain.Models;
using RT.UnitTests.Builders.Entities;
using System.Linq;
using Xunit;

namespace RT.UnitTests.Domain.ClienteAggregateTests.Cenarios
{
    public class Quando_cadastrar_um_cliente_com_3_enderecosTests
    {
        private ClienteAggregate _clienteAggregate;
        public Quando_cadastrar_um_cliente_com_3_enderecosTests()
        {
            //arrange
            var clienteBuilder = new ClienteAggregateTestBuilder();

            clienteBuilder
                .LimparEnderecos()
                .ComEnderecoComercial()
                .ComEnderecoResidencial()
                .ComEnderecoOutros();


            //act
            _clienteAggregate = clienteBuilder.Build();
        }

        [Fact]
        public void Cpf_deve_ser_valido()
        {
            //assert
            _clienteAggregate.Cpf.Validar();

        }

        [Fact]
        public void Nao_deve_ter_tipos_de_enderecos_duplicados()
        {
            //assert
            _clienteAggregate.Enderecos
                .ToLookup(q => q.Endereco.TipoEndereco)
                .Select(q => new { TipoEndereco = q.Key, Quantidade = q.Count() })
                .Any(q => q.Quantidade > 1).Should().Be(false);

        }

        [Fact]
        public void Deve_ter_3_enderecos()
        {
            //assert
            _clienteAggregate.Enderecos
                .Count().Should().Be(3);
        }
    }
}
