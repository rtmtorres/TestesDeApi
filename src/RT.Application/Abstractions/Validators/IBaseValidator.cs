using FluentValidation.Results;

namespace RT.Application.Abstractions.Validators
{
    public interface IBaseValidator<in T>
    {
        ValidationResult Validate(T instance);
    }
}
