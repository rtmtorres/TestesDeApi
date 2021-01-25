using Dapper;
using RT.Application.Queries;
using RT.Application.Queries.Abstractions;
using System.Data;
using System.Threading.Tasks;
using static RT.Application.Queries.ListarCidadesResponse;

namespace RT.Infra.Dapper.Finders
{
    public class ListarCidadesFinder : IListarCidadesFinder
    {
        private readonly IDbConnection _dbConnection;

        public ListarCidadesFinder(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<ListarCidadesResponse> Obter()
        {

            var result = await _dbConnection.QueryAsync<CidadeResponse>(_defaultSql);

            return new ListarCidadesResponse(result);
        }

        private const string _defaultSql = @"
SELECT NOME AS Nome, ESTADO AS Uf, ID AS CidadeId from TB_CIDADE";

    }
}
