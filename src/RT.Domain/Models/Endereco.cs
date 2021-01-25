namespace RT.Domain.Models
{
    public class Endereco
    {
        public Endereco(string rua, string bairro, string numero, string complemento, string cep, TipoEndereco tipoEndereco, int cidadeId)
        {
            if (string.IsNullOrEmpty(rua))
            {
                throw new System.ArgumentException($"'{nameof(rua)}' não pode ser nulo", nameof(rua));
            }

            if (string.IsNullOrEmpty(bairro))
            {
                throw new System.ArgumentException($"'{nameof(bairro)}' não pode ser nulo", nameof(bairro));
            }

            if (string.IsNullOrEmpty(numero))
            {
                throw new System.ArgumentException($"'{nameof(numero)}' não pode ser nulo", nameof(numero));
            }

            if (string.IsNullOrEmpty(complemento))
            {
                throw new System.ArgumentException($"'{nameof(complemento)}' não pode ser nulo", nameof(complemento));
            }

            if (string.IsNullOrEmpty(cep))
            {
                throw new System.ArgumentException($"'{nameof(cep)}' não pode ser nulo", nameof(cep));
            }

            Rua = rua;
            Bairro = bairro;
            Numero = numero;
            Complemento = complemento;
            Cep = cep;
            TipoEndereco = tipoEndereco;
            CidadeId = cidadeId;
        }
        public Endereco(int enderecoId, string rua, string bairro, string numero, string complemento, string cep, TipoEndereco tipoEndereco, int cidadeId)
            : this(rua, bairro, numero, complemento, cep, tipoEndereco, cidadeId)
        {
            EnderecoId = enderecoId;
        }


        private Endereco()
        {

        }
        public int EnderecoId { get; private set; }
        public string Rua { get; private set; }
        public string Bairro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Cep { get; private set; }
        public TipoEndereco TipoEndereco { get; private set; }

        public int CidadeId { get; private set; }
        public virtual CidadeAggregate Cidade { get; private set; }

        internal void AlterarDadosEndereco(string rua, string bairro, string numero, string complemento, string cep, TipoEndereco tipoEndereco, int cidadeId)
        {
            Rua = rua;
            Bairro = bairro;
            Numero = numero;
            Complemento = complemento;
            Cep = cep;
            TipoEndereco = tipoEndereco;
            CidadeId = cidadeId;
        }
    }
}
