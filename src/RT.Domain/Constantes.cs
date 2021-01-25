namespace RT.Domain
{
    public static class Constantes
    {

        public const string UF_INEXISTENTE = "A sigla da UF informada não existe.";
        public const string CPF_INVALIDO = "O CPF deve ser valido.";
        public const string ENDERECO_COM_TIPO_REPETIDO = "Não é permitido cadastro de multiplos endereços do mesmo tipo.";
        public const string CAMPO_OBRIGATORIO = "{PropertyName} deve ser preenchido.";
        public const string CLIENTE_JA_CADASTRADO = "Cliente ja cadastrado nesta empresa.";
        public const string CLIENTE_NAO_ENCONTRADO = "Não foi possivel encontrar o cliente.";
        public const string CIDADE_NAO_EXISTE = "Cidade informada não existe em nossa base.";
        public const string DUPLICIDADE_TIPO_ENDERECO = "Não é permitido vários endereços com o mesmo tipo de endereço.";
        public const string NOME_NULO_OU_VAZIO = "Nome não deve ser nulo ou vazio.";
        public const string CPF_NULO_OU_VAZIO = "Cpf não deve ser nulo ou vazio.";
        public const string CLIENTE_MENOR_18 = "O Cliente não pode ser menor de 18";
    }
}
