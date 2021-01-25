using RT.Domain.Models;

namespace RT.UnitTests.Builders.Entities
{
    public class CidadeAggregateTestBuilder
    {
        private int _parametroId = 1;
        private string _parametroNome = "Cuiabá";
        private string _parametroEstado = "MT";


        public CidadeAggregateTestBuilder ComNome(string nome)
        {
            _parametroNome = nome;

            return this;
        }

        public CidadeAggregateTestBuilder ComEstado(string siglaUf)
        {
            _parametroEstado = siglaUf;

            return this;
        }
        public CidadeAggregateTestBuilder ComId(int id)
        {
            _parametroId = id;

            return this;
        }

        public CidadeAggregate Build()
        {
            return new CidadeAggregate(_parametroId, _parametroEstado, _parametroNome);
        }
    }
}
