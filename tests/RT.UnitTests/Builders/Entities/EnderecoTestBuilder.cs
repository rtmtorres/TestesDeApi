using RT.Domain.Models;

namespace RT.UnitTests.Builders.Entities
{
    public class EnderecoTestBuilder
    {
        private readonly CidadeAggregateTestBuilder _cidadeAggregateTestBuilder;

        private int _parametroEnderecoId = 1;
        private string _parametroRua = "Rua C";
        private string _parametroBairro = "Centro";
        private string _parametroNumero = "5";
        private string _parametroComplemento = "Torre 2";
        private string _parametroCep = "78000000";
        private TipoEndereco _parametroTipoEndereco = TipoEndereco.EnderecoComercial;

        private CidadeAggregate _parametroCidade;
        public EnderecoTestBuilder(CidadeAggregateTestBuilder cidadeAggregateTestBuilder = null)
        {
            _cidadeAggregateTestBuilder = cidadeAggregateTestBuilder ?? new CidadeAggregateTestBuilder();
            _parametroCidade = _cidadeAggregateTestBuilder.Build();
        }

        public EnderecoTestBuilder ComId(int id)
        {
            _parametroEnderecoId = id;
            return this;
        }

        public EnderecoTestBuilder ComRua(string rua)
        {
            _parametroRua = rua;
            return this;
        }

        public EnderecoTestBuilder ComBairro(string bairro)
        {
            _parametroBairro = bairro;
            return this;
        }

        public EnderecoTestBuilder ComNumero(string numero)
        {
            _parametroNumero = numero;
            return this;
        }

        public EnderecoTestBuilder ComComplemento(string complemento)
        {
            _parametroComplemento = complemento;
            return this;
        }

        public EnderecoTestBuilder ComCep(string cep)
        {
            _parametroCep = cep;
            return this;

        }

        public EnderecoTestBuilder ComTipoEndereco(TipoEndereco tipoEndereco)
        {
            _parametroTipoEndereco = tipoEndereco;
            return this;
        }

        public EnderecoTestBuilder ComCidade(CidadeAggregate cidade)
        {
            _parametroCidade = cidade;
            return this;
        }

        public Endereco Build()
        {
            return new Endereco(_parametroEnderecoId, _parametroRua, _parametroBairro, _parametroNumero, _parametroComplemento, _parametroCep, _parametroTipoEndereco, _parametroCidade.CidadeId);
        }
    }
}
