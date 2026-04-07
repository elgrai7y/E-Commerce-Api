
using E_Commerce.Common.GeneralResult;
using FluentValidation.Results;

namespace E_Commerce.BLL
{
    public class ErrorMapper : IErrorMapper
    {
        public Dictionary<string, List<Error>> MapError(ValidationResult validationResult)
        {
            return validationResult.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                 g => g.Key,
                 g => g.Select(e => new Error
                 {
                     ErrorCode = e.ErrorCode,
                     ErrorMessage = e.ErrorMessage,
                 }
                     ).ToList()
                );
        }
    }
}
