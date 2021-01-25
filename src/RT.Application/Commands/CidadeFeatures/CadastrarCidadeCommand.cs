using MediatR;

namespace RT.Application.Commands.CidadeFeatures
{
    public class CadastrarCidadeCommand : IRequest
    {

        public CadastrarCidadeCommand(string nome, string ufSigla)
        {
            Nome = nome;
            UfSigla = ufSigla;
        }
        public string Nome { get; set; }
        public string UfSigla { get; set; }
    }
}
