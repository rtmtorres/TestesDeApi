using RT.Domain.Abstractions;
using RT.Domain.Exceptions;

namespace RT.Domain.ValueObjects
{
    public class Nome : IValueObject
    {
        public Nome(string nome)
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new InvalidArgumentException(Constantes.NOME_NULO_OU_VAZIO);
            }

            Valor = nome;
        }

        private Nome() { }
        public string Valor { get; private set; }
    }
}
