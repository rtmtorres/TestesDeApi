using RT.Domain.Abstractions;
using RT.Domain.Exceptions;
using System.Collections.Generic;

namespace RT.Domain.ValueObjects
{
    public class Uf : IValueObject
    {

        private static readonly HashSet<string> _ufsPermitido = new HashSet<string>() {
        "RO", "AC", "AM", "RR", "PA", "AP", "TO", "MA", "PI", "CE", "RN",
        "PB", "PE", "AL", "SE", "BA", "MG", "ES", "RJ", "SP", "PR", "SC",
        "RS", "MT", "MS", "GO", "DF"
        };

        public string Sigla { get; private set; }


        public Uf( string sigla)
        {
            if (!_ufsPermitido.Contains(sigla))
            {
                throw new InvalidArgumentException(Constantes.UF_INEXISTENTE);
            }

            Sigla = sigla;
        }

        private Uf(){}
    }
}
