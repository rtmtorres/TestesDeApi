using FluentValidation;
using RT.Application.Abstractions;
using RT.Application.Abstractions.Validators;

namespace RT.Application.Queries
{
    public class PesquisarClienteRequestValidator : BaseAbstractValidator<PesquisarClienteRequest>, IPesquisarClienteRequestValidator
    {
        public PesquisarClienteRequestValidator()
        {
            RuleFor(q => q.Empresa)
                .Must(q => q.HasValue)
                .WithMessage(CampoObrigatorio);
        }
    }
}
