using FluentAssertions;
using RT.Domain;
using RT.Domain.Exceptions;
using RT.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RT.UnitTests.Domain.ValueObjects
{
    public class CpfTests
    {
        public CpfTests()
        {

        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Deve_ser_invalido_criar_um_cpf_nulo_ou_vazio(string valorCpf)
        {
            //act
            Action criarCpf = () => new Cpf(valorCpf);

            //assert
            criarCpf.Should().Throw<InvalidArgumentException>(Constantes.CPF_NULO_OU_VAZIO);
        }


        [Theory]
        [InlineData("01001001001")]
        [InlineData("010010010012131")]
        [InlineData("90988077741")]
        [InlineData("56165416544")]
        [InlineData("99999999999")]
        public void Deve_ser_invalido_criar_um_cpf_com_valores_falsos(string valorCpf)
        {
            //act
            Action criarCpf = () => new Cpf(valorCpf);

            //assert
            criarCpf.Should().Throw<InvalidArgumentException>(Constantes.CPF_INVALIDO);
        }
    }
}
