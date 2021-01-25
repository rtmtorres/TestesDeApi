using System.Collections.Generic;

namespace RT.Application.Queries
{
    public sealed class ListarCidadesResponse
    {

        public IEnumerable<CidadeResponse> Cidades { get; }

        public ListarCidadesResponse(IEnumerable<CidadeResponse> cidades)
        {
            Cidades = cidades;
        }

        public sealed class CidadeResponse
        {
            public CidadeResponse(string nome, string uf, int cidadeId)
            {
                Nome = nome;
                Uf = uf;
                CidadeId = cidadeId;
            }
            public string Nome { get; }
            public string Uf { get; }
            public int CidadeId { get;  }
        }
    }
}