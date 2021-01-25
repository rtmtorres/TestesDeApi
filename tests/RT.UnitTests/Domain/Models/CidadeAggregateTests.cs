using FluentAssertions;
using RT.Domain;
using RT.Domain.Exceptions;
using RT.Domain.Models;
using System;
using Xunit;

namespace RT.UnitTests.Domain.Models
{
    public class CidadeAggregateTests
    {
        public CidadeAggregateTests()
        {
        }

        [Fact]
        public void Deve_ser_invalido_criar_cidade_com_uf_inexistente()
        {
            //arrange
            var siglaUf = "XX";
            var nomeMunicipio = "Cuiabá";

            //act
            Action criarCidade = () => new CidadeAggregate(siglaUf, nomeMunicipio);

            //assert
            criarCidade.Should().Throw<InvalidArgumentException>(Constantes.UF_INEXISTENTE);
        }

        [Fact]
        public void Deve_ser_invalido_criar_cidade_com_uf_nula()
        {
            //arrange
            string siglaUf = null;
            var nomeMunicipio = "Cuiabá";

            //act
            Action criarCidade = () => new CidadeAggregate(siglaUf, nomeMunicipio);

            //assert
            criarCidade.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Deve_ser_invalido_criar_cidade_com_nome_cidade_nula()
        {
            //arrange
            string siglaUf = "MT";
            string nomeMunicipio = null;

            //act
            Action criarCidade = () => new CidadeAggregate(siglaUf, nomeMunicipio);

            //assert
            criarCidade.Should().Throw<ArgumentNullException>();
        }


        [Theory]
        [InlineData("MT","Cuiabá")]
        [InlineData("MS","Campo Grande")]
        public void Deve_ser_valido_criar_cidade_com_uf_correta(string uf, string nomeCidade)
        {
            //arrange
            //act
            var cidade = new CidadeAggregate(uf, nomeCidade);

            //assert
            cidade.Nome.Should().Be(nomeCidade);
            cidade.Uf.Sigla.Should().Be(uf);
        }


    }
}
