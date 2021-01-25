namespace RT.Domain.Models
{
    public class ClienteEndereco
    {
        public ClienteEndereco(ClienteAggregate cliente, Endereco endereco)
        {
            Cliente = cliente;
            Endereco = endereco;
            ClienteId = cliente.ClienteId;
            EnderecoId = endereco.EnderecoId;
        }

        private ClienteEndereco()
        {

        }

        public virtual ClienteAggregate Cliente { get; private set; }
        public int ClienteId { get; private set; }
        public virtual Endereco Endereco { get; private set; }
        public int EnderecoId { get; private set; }
    }
}
