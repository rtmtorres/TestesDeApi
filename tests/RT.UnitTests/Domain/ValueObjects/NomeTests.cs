using FluentAssertions;
using RT.Domain;
using RT.Domain.Exceptions;
using RT.Domain.ValueObjects;
using System;
using Xunit;

namespace RT.UnitTests.Domain.ValueObjects
{
    public class NomeTests
    {

        public NomeTests()
        {

        }

        [Fact]
        public void Deve_ser_valido_criar_um_nome()
        {
            //arrange
            var nomeStr = "Renan";

            //act
            var nome = new Nome(nomeStr);

            //assert
            nome.Valor.Should().Be(nomeStr);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Deve_ser_invalido_criar_um_nome_com_valor_vazio_ou_nulo(string valor)
        {
            //act
            Action criarNome = () => new Nome(valor);

            //assert
            criarNome.Should().Throw<InvalidArgumentException>(Constantes.NOME_NULO_OU_VAZIO);
        }
    }
}
