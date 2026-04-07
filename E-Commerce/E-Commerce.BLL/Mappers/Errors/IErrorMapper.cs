

using E_Commerce.Common.GeneralResult;
using FluentValidation.Results;
namespace E_Commerce.BLL
{
    public interface IErrorMapper
    {
         Dictionary<string, List<Error>> MapError(ValidationResult validationResult);
    }
}
