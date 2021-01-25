using FluentValidation.Results;
using System.Linq;

namespace System
{
    public static class ValidationResultExtensions
    {
        public static string GetConcatenatedMessages(this ValidationResult validationResult)
        {
            return string.Join("\n", validationResult.Errors.Select(m => m.ErrorMessage));
        }
    }
}
