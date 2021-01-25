using RT.Domain.Abstractions;
using RT.Domain.Exceptions;
using RT.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RT.Domain.Models
{
    public class ClienteAggregate : IAggregateRoot
    {
        private ClienteAggregate(
            string nome,
            string rg,
            string cpf,
            DateTime dataNascimento,
            string telefone,
            string email,
            Empresa empresa) : this()
        {
            Nome = new Nome(nome);
            Rg = rg;
            Cpf = new Cpf(cpf);
            DataNascimento = dataNascimento;
            Telefone = telefone;
            Email = email;
            Empresa = empresa;
        }
        public ClienteAggregate(
            int clienteId,
            string nome,
            string rg,
            string cpf,
            DateTime dataNascimento,
            string telefone,
            string email,
            Empresa empresa) : this(nome, rg, cpf, dataNascimento, telefone, email, empresa)
        {
            ClienteId = clienteId;
        }

        private ClienteAggregate()
        {
            _enderecos = new List<ClienteEndereco>();
            _enderecosParaRemover = new List<ClienteEndereco>();
        }

        public int ClienteId { get; private set; }
        public Nome Nome { get; private set; }
        public string Rg { get; private set; }
        public Cpf Cpf { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string Telefone { get; private set; }
        public string Email { get; private set; }
        public Empresa Empresa { get; private set; }
        public IReadOnlyCollection<ClienteEndereco> Enderecos => _enderecos;


        public IReadOnlyCollection<ClienteEndereco> EnderecosParaRemover => _enderecosParaRemover;
        
        private readonly List<ClienteEndereco> _enderecosParaRemover;
        private readonly List<ClienteEndereco> _enderecos;



        public void AdicionarEndereco(Endereco endereco)
        {
            if (_enderecos.Any(q => q.Endereco.TipoEndereco == endereco.TipoEndereco))
                throw new InvalidArgumentException(Constantes.ENDERECO_COM_TIPO_REPETIDO);


            var clienteEndereco = new ClienteEndereco(this, endereco);
            _enderecos.Add(clienteEndereco);
        }


        public void AlterarDados(
            string nome,
            string rg,
            string cpf,
            DateTime dataNascimento,
            string telefone,
            string email,
            Empresa empresa,
            IEnumerable<Endereco> enderecosAtualizados
            )
        {
            Nome = new Nome(nome);
            Rg = rg;
            Cpf = new Cpf(cpf);
            DataNascimento = dataNascimento;
            Telefone = telefone;
            Email = email;
            Empresa = empresa;


            RemoverEnderecos(enderecosAtualizados);
            AlterarDadosEnderecosExistentes(enderecosAtualizados);
            IncluirNovosEnderecos(enderecosAtualizados);
        }


        private void RemoverEnderecos(IEnumerable<Endereco> enderecosAtualizados)
        {
            List<ClienteEndereco> enderecosParaRemover = IdentificarEnderecosParaRemover(enderecosAtualizados);
            enderecosParaRemover.ForEach(end => {
                _enderecos.Remove(end);
                _enderecosParaRemover.Add(end);
            });
        }
        private void AlterarDadosEnderecosExistentes(IEnumerable<Endereco> enderecosAtualizados)
        {
            foreach (var endereco in _enderecos)
            {
                var dadosEnderecoNovo = enderecosAtualizados.FirstOrDefault(q => q.EnderecoId == endereco.EnderecoId);
                endereco.Endereco.AlterarDadosEndereco(dadosEnderecoNovo.Rua, dadosEnderecoNovo.Bairro, dadosEnderecoNovo.Numero, dadosEnderecoNovo.Complemento, dadosEnderecoNovo.Cep, dadosEnderecoNovo.TipoEndereco, dadosEnderecoNovo.CidadeId);
            }
        }
        private void IncluirNovosEnderecos(IEnumerable<Endereco> enderecosAtualizados)
        {
            var enderecosParaIncluir = IdentificarEnderecosParaAdicionar(enderecosAtualizados);
            enderecosParaIncluir.ForEach(q => AdicionarEndereco(q));
        }

        private List<Endereco> IdentificarEnderecosParaAdicionar(IEnumerable<Endereco> enderecos) => enderecos.Where(q => !_enderecos.Any(t => t.Endereco.EnderecoId == q.EnderecoId)).ToList();

        private List<ClienteEndereco> IdentificarEnderecosParaRemover(IEnumerable<Endereco> enderecos) => _enderecos.Where(q => !enderecos.Any(t => t.EnderecoId == q.Endereco.EnderecoId)).ToList();


        public static ClienteAggregate CriarCliente(
            string nome,
            string rg,
            string cpf,
            DateTime dataNascimento,
            string telefone,
            string email,
            Empresa empresa,
            IEnumerable<Endereco> enderecos)
        {
            var cliente = new ClienteAggregate(nome, rg, cpf, dataNascimento, telefone, email, empresa);

            foreach (var endereco in enderecos)
            {
                cliente.AdicionarEndereco(endereco);
            }

            return cliente;
        }

    }
}
