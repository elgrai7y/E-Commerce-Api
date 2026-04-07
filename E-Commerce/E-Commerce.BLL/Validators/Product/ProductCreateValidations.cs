
using E_Commerce.BLL;
using E_Commerce.DAL;
using FluentValidation;


namespace E_Commerce.BLL
{
    public class ProductCreateValidations : AbstractValidator<ProductCreateDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductCreateValidations(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(p=>p.Title)
                .NotEmpty()
                .WithMessage("Can't be empty")
                .WithErrorCode("ERR-01")

                 .MinimumLength(3)
                .WithMessage("Title at least 3 char")
                .WithErrorCode("ERR-02")

                .MaximumLength(50)
                .WithMessage("Title at cannot be longer than 3 char")
                .WithErrorCode("ERR-03")

                .MustAsync(CheckNameIsUnique)
                .WithMessage("Title already exists")
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


            RuleFor(p => p.Price)
               .InclusiveBetween(1,int.MaxValue)
               .WithMessage("Price should more than or equal 1  ")
               .WithErrorCode("ERR-08");
            RuleFor(p => p.Stock)
               .InclusiveBetween(1, int.MaxValue)
               .WithMessage("Count should more than or equal 1  ")
               .WithErrorCode("ERR-09");
        }
        private async Task<bool> CheckNameIsUnique(string title,CancellationToken cancellationToken)
        {
            var products =await _unitOfWork._productRepository.GetAll();
            
            return !(products.Any(p=>p.Title == title));
        }
    }
}
