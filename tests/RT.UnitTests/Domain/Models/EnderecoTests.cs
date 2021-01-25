using FluentAssertions;
using RT.Domain.Models;
using RT.UnitTests.Builders.Entities;
using System;
using Xunit;

namespace RT.UnitTests.Domain.Models
{
    public class EnderecoTests
    {
        private EnderecoTestBuilder _enderecoTestBuilder;
        private CidadeAggregateTestBuilder _cidadeTestBuilder;

        public EnderecoTests()
        {
            _enderecoTestBuilder = new EnderecoTestBuilder();
            _cidadeTestBuilder = new CidadeAggregateTestBuilder();
        }

        [Fact]
        public void Deve_ser_valido_criar_endereco_com_tudo_preenchido()
        {
            //arrange
            var rua = "Rua C";
            var bairro = "Centro";
            var numero = "5";
            var complemento = "Torre 2 ap 302";
            var cep = "78000000";
            var tipoEndereco = TipoEndereco.EnderecoComercial;
            var cidade = _cidadeTestBuilder.Build();


            //act
            var novoEndereco = new Endereco(rua, bairro, numero, complemento, cep, tipoEndereco, cidade.CidadeId);

            //assert
            novoEndereco.Rua.Should().Be(rua);
            novoEndereco.Bairro.Should().Be(bairro);
            novoEndereco.Numero.Should().Be(numero);
            novoEndereco.Complemento.Should().Be(complemento);
            novoEndereco.Cep.Should().Be(cep);
            novoEndereco.TipoEndereco.Should().Be(tipoEndereco);
            novoEndereco.CidadeId.Should().Be(cidade.CidadeId);
        }

        [Theory]
        [InlineData(null, "Centro", "5", "Torre 2 ap 302", "78000000", TipoEndereco.EnderecoComercial, 1)]
        [InlineData("Rua C", null, "5", "Torre 2 ap 302", "78000000", TipoEndereco.EnderecoComercial, 1)]
        [InlineData("Rua C", "Centro",  null, "Torre 2 ap 302", "78000000", TipoEndereco.EnderecoComercial, 1)]
        [InlineData("Rua C", "Centro", "5", null, "78000000", TipoEndereco.EnderecoComercial, 1)]
        [InlineData("Rua C", "Centro", "5", "Torre 2 ap 302", null, TipoEndereco.EnderecoComercial, 1)]
        public void Deve_ser_invalido_criar_endereco_com_algum_campo_nulo(
            string rua, string bairro, string numero, string complemento, string cep, TipoEndereco tipoEndereco, int cidadeId)
        {
            //arrange
            var cidade = _cidadeTestBuilder.ComId(cidadeId).Build();

            //act
            Action criarEndereco = () => new Endereco(rua, bairro, numero, complemento, cep, tipoEndereco, cidade.CidadeId);


            //assert
            criarEndereco.Should().Throw<ArgumentException>();
        }

    }
}
