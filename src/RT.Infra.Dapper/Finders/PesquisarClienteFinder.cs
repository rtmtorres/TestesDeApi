using Dapper;
using RT.Application.Queries;
using RT.Application.Queries.Abstractions;
using System.Data;
using System.Threading.Tasks;
using static RT.Application.Queries.PesquisarClienteResponse;

namespace RT.Infra.Dapper.Finders
{
    public class PesquisarClienteFinder : IPesquisarClienteFinder
    {
        private readonly IDbConnection _dbConnection;

        public PesquisarClienteFinder(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }


        public async Task<PesquisarClienteResponse> Pesquisar(PesquisarClienteRequest pesquisarClienteRequest)
        {
            var sql = _defaultSql;

            if (!string.IsNullOrEmpty(pesquisarClienteRequest.Nome))
            {
                sql += " AND NOME LIKE @NOME";
            }
            if (!string.IsNullOrEmpty(pesquisarClienteRequest.Cpf))
            {
                sql += " AND CPF=@CPF";
            }
            if (!string.IsNullOrEmpty(pesquisarClienteRequest.Cidade))
            {
                sql += @" AND EXISTS (SELECT 1 FROM TB_CLIENTE_ENDERECO tb_ce 
INNER JOIN TB_ENDERECO tb_e ON tb_ce.tb_endereco_id = tb_e.id
INNER JOIN TB_CIDADE tb_ci on tb_e.tb_cidade_id = tb_ci.id
WHERE tb_c.id = tb_ce.tb_cliente_id and tb_ci.NOME LIKE @CIDADE)";
            }
            if (!string.IsNullOrEmpty(pesquisarClienteRequest.Estado))
            {
                sql += @" AND EXISTS (SELECT 1 FROM TB_CLIENTE_ENDERECO tb_ce 
INNER JOIN TB_ENDERECO tb_e ON tb_ce.tb_endereco_id = tb_e.id
INNER JOIN TB_CIDADE tb_ci on tb_e.tb_cidade_id = tb_ci.id
WHERE tb_c.id = tb_ce.tb_cliente_id and tb_ci.ESTADO = @ESTADO)";
            }

            var result = await _dbConnection.QueryAsync<ClienteResumido>(sql, new
            {
                COD_EMPRESA = pesquisarClienteRequest.Empresa,
                NOME = $"%{pesquisarClienteRequest.Nome}%",
                CPF = pesquisarClienteRequest.Cpf,
                CIDADE = $"%{pesquisarClienteRequest.Cidade}%",
                ESTADO = pesquisarClienteRequest.Estado,
            });

            return new PesquisarClienteResponse(result);
        }


        private const string _defaultSql = @"
SELECT ID AS ClienteId, NOME as Nome, CPF as Cpf, EMAIL_ as EMAIL from TB_CLIENTE tb_c 
WHERE COD_EMPRESA = @COD_EMPRESA";

    }
}
