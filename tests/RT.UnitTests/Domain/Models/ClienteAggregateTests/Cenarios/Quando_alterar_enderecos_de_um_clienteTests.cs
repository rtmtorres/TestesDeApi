using FluentAssertions;
using RT.Domain.Models;
using RT.UnitTests.Builders.Entities;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace RT.UnitTests.Domain.Models.ClienteAggregateTests.Cenarios
{
    public class Quando_alterar_endereco_de_um_clienteTests
    {
        private ClienteAggregate _clienteAggregate;
        private EnderecoTestBuilder _enderecoBuilder;
        private CidadeAggregateTestBuilder _cidadeTestBuilder;

        private Endereco _enderecoComercial;
        private Endereco _enderecoResidencial;
        private Endereco _enderecoOutros;


        public Quando_alterar_endereco_de_um_clienteTests()
        {
            //arrange
            var clienteBuilder = new ClienteAggregateTestBuilder();
            _enderecoBuilder = new EnderecoTestBuilder();

            _enderecoComercial = _enderecoBuilder.ComId(1).ComTipoEndereco(TipoEndereco.EnderecoComercial).Build();
            _enderecoResidencial = _enderecoBuilder.ComId(2).ComTipoEndereco(TipoEndereco.EnderecoResidencial).Build();
            _enderecoOutros = _enderecoBuilder.ComId(3).ComTipoEndereco(TipoEndereco.Outros).Build();
            _cidadeTestBuilder = new CidadeAggregateTestBuilder();

            clienteBuilder
                .LimparEnderecos()
                .ComEndereco(_enderecoComercial)
                .ComEndereco(_enderecoResidencial)
                .ComEndereco(_enderecoOutros);

            _clienteAggregate = clienteBuilder.Build();
        }

        [Fact]
        public void Deve_remover_Endereco_que_nao_esteja_na_chamada()
        {

            //act    
            _clienteAggregate.AlterarDados(
                _clienteAggregate.Nome.Valor,
                _clienteAggregate.Rg,
                _clienteAggregate.Cpf.Numero,
                _clienteAggregate.DataNascimento,
                 _clienteAggregate.Telefone,
                 _clienteAggregate.Email,
                 _clienteAggregate.Empresa,
                 new List<Endereco>
                 {
                     _enderecoComercial,
                 });


            //assert
            _clienteAggregate.Enderecos.Should().HaveCount(1);
            _clienteAggregate.Enderecos.Select(q => q.Endereco).Should().Contain(_enderecoComercial);
            _clienteAggregate.EnderecosParaRemover.Should().HaveCount(2);
            _clienteAggregate.EnderecosParaRemover.Select(q => q.Endereco).Should().Contain(_enderecoResidencial);
            _clienteAggregate.EnderecosParaRemover.Select(q => q.Endereco).Should().Contain(_enderecoOutros);
        }

        [Fact]
        public void Deve_alterar_valores_do_endereco_informado()
        {
            //arrange
            var bombinhas = _cidadeTestBuilder.ComEstado("SC").ComNome("Bombinhas").ComId(75).Build();

            var alteracaoCamposdoEnderecoResidencial =
                _enderecoBuilder
                    .ComId(_enderecoResidencial.EnderecoId)
                    .ComTipoEndereco(_enderecoResidencial.TipoEndereco)
                    .ComRua("Rua Navegantes")
                    .ComBairro("Bombinhas")
                    .ComNumero("30")
                    .ComComplemento("Novo complemento")
                    .ComCidade(bombinhas).Build();


            //act
            _clienteAggregate.AlterarDados(
                _clienteAggregate.Nome.Valor,
                _clienteAggregate.Rg,
                _clienteAggregate.Cpf.Numero,
                _clienteAggregate.DataNascimento,
                 _clienteAggregate.Telefone,
                 _clienteAggregate.Email,
                 _clienteAggregate.Empresa,
                 new List<Endereco>
                 {
                     _enderecoComercial,
                     _enderecoOutros,
                     alteracaoCamposdoEnderecoResidencial
                 });


            //assert
            _clienteAggregate.Enderecos.Should().HaveCount(3);
            _clienteAggregate.Enderecos.Select(q => q.Endereco).Should().Contain(_enderecoComercial);
            _clienteAggregate.Enderecos.Select(q => q.Endereco).Should().Contain(_enderecoOutros);
            _clienteAggregate.Enderecos.Select(q => q.Endereco).Should().Contain(_enderecoResidencial);
            _enderecoResidencial.Bairro.Should().Be(alteracaoCamposdoEnderecoResidencial.Bairro);
            _enderecoResidencial.Rua.Should().Be(alteracaoCamposdoEnderecoResidencial.Rua);
            _enderecoResidencial.Numero.Should().Be(alteracaoCamposdoEnderecoResidencial.Numero);
            _enderecoResidencial.CidadeId.Should().Be(alteracaoCamposdoEnderecoResidencial.CidadeId);
            _enderecoResidencial.Cidade.Should().Be(alteracaoCamposdoEnderecoResidencial.Cidade);
            _enderecoResidencial.Complemento.Should().Be(alteracaoCamposdoEnderecoResidencial.Complemento);
            _enderecoResidencial.Cep.Should().Be(alteracaoCamposdoEnderecoResidencial.Cep);
            _enderecoResidencial.EnderecoId.Should().Be(alteracaoCamposdoEnderecoResidencial.EnderecoId);
            _enderecoResidencial.TipoEndereco.Should().Be(alteracaoCamposdoEnderecoResidencial.TipoEndereco);
        }


        [Fact]
        public void Deve_incluir_caso_seja_novo_endereco()
        {
            //arrange
            var bombinhas = _cidadeTestBuilder.ComEstado("SC").ComNome("Bombinhas").ComId(75).Build();

            var novoEnderecoResidencial =
                _enderecoBuilder
                    .ComId(4)
                    .ComTipoEndereco(TipoEndereco.EnderecoResidencial)
                    .ComRua("Rua 2")
                    .ComBairro("Bombinhas")
                    .ComNumero("35")
                    .ComComplemento("Apt 302 torre 2")
                    .ComCidade(bombinhas).Build();

            var novoEnderecoComercial =
                _enderecoBuilder
                    .ComId(5)
                    .ComTipoEndereco(TipoEndereco.EnderecoComercial)
                    .ComRua("Rua 1")
                    .ComBairro("Bombinhas")
                    .ComNumero("210")
                    .ComComplemento("Em frente a praia")
                    .ComCidade(bombinhas).Build();


            //act
            _clienteAggregate.AlterarDados(
                _clienteAggregate.Nome.Valor,
                _clienteAggregate.Rg,
                _clienteAggregate.Cpf.Numero,
                _clienteAggregate.DataNascimento,
                 _clienteAggregate.Telefone,
                 _clienteAggregate.Email,
                 _clienteAggregate.Empresa,
                 new List<Endereco>
                 {
                     novoEnderecoResidencial,
                     novoEnderecoComercial
                 });


            //assert
            _clienteAggregate.Enderecos.Should().HaveCount(2);
            _clienteAggregate.Enderecos.Select(q => q.Endereco).Should().Contain(novoEnderecoResidencial);
            _clienteAggregate.Enderecos.Select(q => q.Endereco).Should().Contain(novoEnderecoComercial);
            _clienteAggregate.Enderecos.Select(q => q.Endereco).Should().NotContain(_enderecoComercial);
            _clienteAggregate.Enderecos.Select(q => q.Endereco).Should().NotContain(_enderecoOutros);
            _clienteAggregate.Enderecos.Select(q => q.Endereco).Should().NotContain(_enderecoResidencial);
        }

    }
}