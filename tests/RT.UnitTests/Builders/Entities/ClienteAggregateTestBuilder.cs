using RT.Domain.Models;
using System;
using System.Collections.Generic;

namespace RT.UnitTests.Builders.Entities
{
    public class ClienteAggregateTestBuilder
    {


        private int _parametroClienteId = 1;
        private string _parametroNome = "Renan";
        private string _parametroRg = "11111111";
        private string _parametroCpf = "26429540057";
        private DateTime _parametroDataNascimento = new DateTime(1990, 1, 20);
        private string _parametroTelefone = "65981177574";
        private string _parametroEmail = "emailfake@emailfake.com";
        private Empresa _parametroEmpresa = Empresa.Carrefour;

        private Endereco _enderecoComercial;
        private Endereco _enderecoResidencial;
        private Endereco _enderecoOutros;

        private List<Endereco> _enderecos = new List<Endereco>();


        private EnderecoTestBuilder _enderecoTestBuilder;

        public ClienteAggregateTestBuilder(EnderecoTestBuilder enderecoTestBuilder =null)
        {
            _enderecoTestBuilder = enderecoTestBuilder ?? new EnderecoTestBuilder(new CidadeAggregateTestBuilder());

            _enderecoComercial = _enderecoTestBuilder.ComId(1).ComTipoEndereco(TipoEndereco.EnderecoComercial).Build();
            _enderecoResidencial = _enderecoTestBuilder.ComId(2).ComTipoEndereco(TipoEndereco.EnderecoResidencial).Build();
            _enderecoOutros = _enderecoTestBuilder.ComId(3).ComTipoEndereco(TipoEndereco.Outros).Build();

            _enderecos.Add(_enderecoComercial);
            _enderecos.Add(_enderecoResidencial);
            _enderecos.Add(_enderecoOutros);

        }

        public ClienteAggregateTestBuilder LimparEnderecos()
        {
            _enderecos.Clear();
            return this;
        }

        public ClienteAggregateTestBuilder ComCpf(string cpf)
        {
            _parametroCpf = cpf;

            return this;
        }

        public ClienteAggregateTestBuilder ComEndereco(Endereco endereco)
        {
            if (!_enderecos.Contains(endereco))
                _enderecos.Add(endereco);

            return this;
        }

        public ClienteAggregateTestBuilder ComEnderecoComercial()
        {
            if (!_enderecos.Contains(_enderecoComercial))
                _enderecos.Add(_enderecoComercial);

            return this;
        }
        public ClienteAggregateTestBuilder ComEnderecoResidencial()
        {
            if (!_enderecos.Contains(_enderecoResidencial))
                _enderecos.Add(_enderecoResidencial);

            return this;
        }
        public ClienteAggregateTestBuilder ComEnderecoOutros()
        {
            if (!_enderecos.Contains(_enderecoOutros))
                _enderecos.Add(_enderecoOutros);

            return this;
        }


        public ClienteAggregate Build()
        {
            var cliente = new ClienteAggregate(_parametroClienteId, _parametroNome, _parametroRg, _parametroCpf, _parametroDataNascimento, _parametroTelefone, _parametroEmail, _parametroEmpresa);

            foreach (var endereco in _enderecos)
            {
                cliente.AdicionarEndereco(endereco);
            }

            return cliente;

        }


    }
}
