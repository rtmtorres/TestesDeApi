using FluentValidation;
using RT.Application.Abstractions.Validators;
using RT.Domain;

namespace RT.Application.Abstractions
{
    public abstract class BaseAbstractValidator<T> : AbstractValidator<T>, IBaseValidator<T>
    {
        protected string CampoObrigatorio => Constantes.CAMPO_OBRIGATORIO;

    }
}
