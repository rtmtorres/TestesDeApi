using RT.Domain.Abstractions;
using RT.Domain.ValueObjects;

namespace RT.Domain.Models
{
    public class CidadeAggregate : IAggregateRoot
    {
        public CidadeAggregate(string uf, string nome)
        {
            if (uf is null)
            {
                throw new System.ArgumentNullException(nameof(uf));
            }

            Uf = new Uf(uf);
            Nome = nome ?? throw new System.ArgumentNullException(nameof(nome));
        }
        public CidadeAggregate(int cidadeId, string uf, string nome) : this(uf, nome)
        {
            CidadeId = cidadeId;
        }

        private CidadeAggregate() { }

        public int CidadeId { get; set; }
        public Uf Uf { get; set; }
        public string Nome { get; set; }
    }
}
