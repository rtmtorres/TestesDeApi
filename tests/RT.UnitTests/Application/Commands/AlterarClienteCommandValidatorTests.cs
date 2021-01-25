using FluentAssertions;
using Moq;
using RT.Application.Commands.ClienteFeatures;
using RT.Domain;
using RT.Domain.Models;
using RT.Domain.Repositories;
using RT.UnitTests.Builders.Entities;
using System.Collections.Generic;
using Xunit;
using static RT.Application.Commands.ClienteFeatures.AtualizarClienteCommand;
using static RT.Application.Commands.ClienteFeatures.CadastrarClienteCommand;

namespace RT.UnitTests.Application.Commands
{
    public class AlterarClienteCommandValidatorTests
    {
        private IClienteRepository _clienteRepository;
        private ICidadeRepository _cidadeRepository;
        private string _cpfCadastradoCarrefour = "01010101101";

        private int _clienteId = 5;

        public AlterarClienteCommandValidatorTests()
        {
            var mockClienteRepository = new Mock<IClienteRepository>();
            mockClienteRepository.Setup(e => e.VerificaSeClienteJaCadastrado(_cpfCadastradoCarrefour, Empresa.Carrefour, _clienteId)).Returns(true);
            _clienteRepository = mockClienteRepository.Object;


            var cidadeTestBuilder = new CidadeAggregateTestBuilder();
            var cidades = new List<CidadeAggregate>()
            {
                cidadeTestBuilder.ComId(1).ComEstado("MT").ComNome("Cuiaba").Build(),
                cidadeTestBuilder.ComId(2).ComEstado("MT").ComNome("Sinop").Build(),
                cidadeTestBuilder.ComId(3).ComEstado("MT").ComNome("Tangara").Build()
            };

            var mockCidadeRepository = new Mock<ICidadeRepository>();
            cidades.ForEach(c => mockCidadeRepository.Setup(e => e.ObterPorId(c.CidadeId)).Returns(c));
            _cidadeRepository = mockCidadeRepository.Object;
        }

        [Fact]
        public void Deve_ser_valido_ao_validar_command_para_alterar_cliente()
        {
            //arrange
            var comando = new AtualizarClienteCommand(_clienteId, "Renan", "1111", "000000010", new System.DateTime(1980, 01, 20), "123123123", "1231233", Empresa.Carrefour, null);
            var validator = new AtualizarClienteCommandValidator(_clienteRepository, _cidadeRepository);

            //act 
            var result = validator.Validate(comando);

            //assert
            result.Errors.Should().HaveCount(0);
        }


        [Fact]
        public void Deve_ser_invalido_ao_validar_command_para_alterar_cliente_com_cpf_repetido_na_mesma_empresa()
        {
            //arrange
            var comando = new AtualizarClienteCommand(_clienteId, "Renan", "1111", _cpfCadastradoCarrefour, new System.DateTime(1980, 01, 20), "123123123", "1231233", Empresa.Carrefour, null);
            var validator = new AtualizarClienteCommandValidator(_clienteRepository, _cidadeRepository);

            //act 
            var result = validator.Validate(comando);

            //assert
            result.Errors.Should().HaveCount(1);
            result.Errors.Should().ContainSingle(t => t.ErrorMessage == Constantes.CLIENTE_JA_CADASTRADO);
        }

        [Fact]
        public void Deve_ser_valido_ao_validar_command_para_alterar_cliente_com_cpf_repetido_em_outra_empresa()
        {
            //arrange
            var comando = new AtualizarClienteCommand(_clienteId, "Renan", "1111", _cpfCadastradoCarrefour, new System.DateTime(1980, 01, 20), "123123123", "1231233", Empresa.Atacadao, null);
            var validator = new AtualizarClienteCommandValidator(_clienteRepository, _cidadeRepository);

            //act 
            var result = validator.Validate(comando);

            //assert
            result.Errors.Should().HaveCount(0);
        }


        [Fact]
        public void Deve_ser_valido_ao_validar_command_para_alterar_cliente_com_endereco_em_cidade_que_existe()
        {
            //arrange
            var comando = new AtualizarClienteCommand(_clienteId, "Renan", "1111", _cpfCadastradoCarrefour, new System.DateTime(1980, 01, 20), "123123123", "1231233", Empresa.Atacadao,
                 new List<AtualizarClienteEnderecoDto>() { new AtualizarClienteEnderecoDto(1,"rua c", "bairro", "nmero", "complemento", "cep", TipoEndereco.EnderecoComercial, 1) });

            var validator = new AtualizarClienteCommandValidator(_clienteRepository, _cidadeRepository);

            //act 
            var result = validator.Validate(comando);

            //assert
            result.Errors.Should().HaveCount(0);
        }

        [Fact]
        public void Deve_ser_invalido_ao_validar_command_para_alterar_cliente_com_endereco_em_cidade_que_nao_existe()
        {
            //arrange
            var comando = new AtualizarClienteCommand(_clienteId, "Renan", "1111", _cpfCadastradoCarrefour, new System.DateTime(1980, 01, 20), "123123123", "1231233", Empresa.Atacadao,
                 new List<AtualizarClienteEnderecoDto>() { new AtualizarClienteEnderecoDto(1,"rua c", "bairro", "nmero", "complemento", "cep", TipoEndereco.EnderecoComercial, 4) });

            var validator = new AtualizarClienteCommandValidator(_clienteRepository, _cidadeRepository);

            //act 
            var result = validator.Validate(comando);

            //assert
            result.Errors.Should().HaveCount(1);
            result.Errors.Should().ContainSingle(t => t.ErrorMessage == Constantes.CIDADE_NAO_EXISTE);
        }

    }
}
