using System.Collections.Generic;
using System.Linq;

namespace RT.Application.Queries
{
    public class PesquisarClienteResponse
    {
        public PesquisarClienteResponse(IEnumerable<ClienteResumido> clientesEncontrados)
        {
            ClientesEncontrados = clientesEncontrados;
        }

        public IEnumerable<ClienteResumido> ClientesEncontrados { get; set; }
        public int Quantidade => ClientesEncontrados?.Count() ?? 0;
        
        public class ClienteResumido
        {
            public string ClienteId { get; set; }
            public string Nome { get; set; }
            public string Cpf { get; set; }
            public string Email { get; set; }
        }
    }
}
