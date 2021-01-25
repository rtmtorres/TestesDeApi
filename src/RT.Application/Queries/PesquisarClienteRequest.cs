using MediatR;
using RT.Domain.Models;

namespace RT.Application.Queries
{
    public class PesquisarClienteRequest : IRequest<PesquisarClienteResponse>
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public Empresa? Empresa { get; set; }

    }
}
