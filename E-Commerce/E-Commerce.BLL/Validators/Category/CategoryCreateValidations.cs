
using E_Commerce.BLL;
using E_Commerce.DAL;
using FluentValidation;


namespace E_Commerce.BLL
{
    public class CategoryCreateValidations : AbstractValidator<CategoryCreateDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryCreateValidations(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(p=>p.Name)
                .NotEmpty()
                .WithMessage("Can't be empty")
                .WithErrorCode("ERR-01")

                 .MinimumLength(3)
                .WithMessage("Name at least 3 char")
                .WithErrorCode("ERR-02")

                .MaximumLength(50)
                .WithMessage("Name at cannot be longer than 3 char")
                .WithErrorCode("ERR-03")

                .MustAsync(CheckNameIsUnique)
                .WithMessage("Name already exists")
                .WithErrorCode("ERR-04");

            RuleFor(p => p.Description)
               .NotEmpty()
               .WithMessage("Description Can't be empty")
               .WithErrorCode("ERR-05")

                .MinimumLength(3)
               .WithMessage("Description at least 3 char")
               .WithErrorCode("ERR-06")

               .MaximumLength(200)
               .WithMessage("Description at cannot be longer than 3 char")
               .WithErrorCode("ERR-07");



        }
        private async Task<bool> CheckNameIsUnique(string name,CancellationToken cancellationToken)
        {
            var categories =await _unitOfWork._categoryRepository.GetAll();
            
            return !(categories.Any(p=>p.Name == name));
        }
    }
}
