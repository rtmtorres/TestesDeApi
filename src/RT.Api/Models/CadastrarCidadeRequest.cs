using RT.Application.Commands.CidadeFeatures;

namespace RT.Api.Models
{
    /// <summary>
    /// Representa uma requisição para cadastrar cidade
    /// </summary>
    public class CadastrarCidadeRequest
    {
        /// <summary>
        /// Sigla da UF que deseja cadastrar a cidade
        /// </summary>
        /// <example>MT</example>
        public string Uf { get; set; }
        /// <summary>
        /// Nome da cidade que será cadastrada
        /// </summary>
        /// <example>Cuiabá</example>
        public string Nome { get; set; }

        /// <summary>
        /// Responssável por transformar uma requisição de cadastrar cidade em um comando
        /// </summary>
        /// <returns></returns>
        public CadastrarCidadeCommand Map()
        {
            return new CadastrarCidadeCommand(Nome, Uf);
        }
    }
}
