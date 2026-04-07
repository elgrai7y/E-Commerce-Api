using E_Commerce.BLL;
using E_Commerce.Common;
using E_Commerce.Common.GeneralResult;
using E_Commerce.Common.Pagination;
using E_Commerce.DAL;
using FluentValidation;

namespace E_Commerce.BLL
{
    public class ProductManager : IProductManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<ProductCreateDto> _productCreateDtoValidator;
        private readonly IErrorMapper _errorMapper;

        public ProductManager(IUnitOfWork unitOfWork, IValidator<ProductCreateDto> productCreateDtoValidator, IErrorMapper errorMapper)
        {
            _unitOfWork = unitOfWork;
            _productCreateDtoValidator = productCreateDtoValidator;
            _errorMapper = errorMapper;
        }
        public async Task<GeneralResult<PageResult<Product>>> GetAllProductWithPagination(PaginationParameters paginationParameters, ProductFiltersParameters ProductFiltersParameters)
        {
            var products = await _unitOfWork._productRepository.GetProductsWithPaginationAsync(paginationParameters, ProductFiltersParameters);

            return GeneralResult<PageResult<Product>>.SuccessResult(products);
        }

        public async Task<GeneralResult<IEnumerable<ProductReadDto>>> GetAllProduct()
        {
            var products = await _unitOfWork._productRepository.GetProductWithCategoryAsync();
            var productDtos = products.Select(p => new ProductReadDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Unit = p.Unit,
                UrlImage = p.UrlImage,
                Price = p.Price,
                Stock = p.Stock,
                Reviews = p.Reviews,
                Rate = p.Rate,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            }).ToList();
            return GeneralResult<IEnumerable<ProductReadDto>>.SuccessResult(productDtos);
        }

        public async Task<GeneralResult<ProductReadDto>> GetProductById(Guid id)
        {
            var p = await _unitOfWork._productRepository.GetProductByIdWithCategoryAsync(id);
            if (p is null)
            {
                return GeneralResult<ProductReadDto>.NotFound("Product not found");
            }
            var productDto = new ProductReadDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Unit = p.Unit,
                UrlImage = p.UrlImage,
                Price = p.Price,
                Stock = p.Stock,
                Reviews = p.Reviews,
                Rate = p.Rate,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            };
            return GeneralResult<ProductReadDto>.SuccessResult(productDto);
        }

        public async Task<GeneralResult<ProductCreateDto>> CreateProduct(ProductCreateDto productCreateDto)
        {
            var validationResult = await _productCreateDtoValidator.ValidateAsync(productCreateDto);
            if (!validationResult.IsValid)
            {
                var errors = _errorMapper.MapError(validationResult);

                return GeneralResult<ProductCreateDto>.FailResult(errors);
            }

            var product = new Product
            {
                Title = productCreateDto.Title,
                Description = productCreateDto.Description,
                Price = productCreateDto.Price,
                Unit = productCreateDto.Unit,
                Stock = productCreateDto.Stock,
                CategoryId = productCreateDto.CategoryId

            };
            _unitOfWork._productRepository.Add(product);
            await _unitOfWork.SaveAsync();
            return GeneralResult<ProductCreateDto>.SuccessResult(productCreateDto);
        }

        public async Task<GeneralResult<ProductEditDto>> EditeProduct(ProductEditDto productEditDto)
        {
            Product p = await _unitOfWork._productRepository.GetById(productEditDto.Id);
            if (p is null)
            {
                return GeneralResult<ProductEditDto>.NotFound("Product not found");
            }
            p.Title = productEditDto.Title;
            p.Description = productEditDto.Description;
            p.Price = productEditDto.Price;
            p.Unit = productEditDto.Unit;
            p.Stock = productEditDto.Stock;
            p.CategoryId = productEditDto.CategoryId;

            await _unitOfWork.SaveAsync();
            return GeneralResult<ProductEditDto>.SuccessResult(productEditDto);
        }


        public async Task<GeneralResult<ProductReadDto>> DeleteProduct(Guid id)
        {
            Product p = await _unitOfWork._productRepository.GetProductByIdWithCategoryAsync(id);
            if (p is null)
            {
                return GeneralResult<ProductReadDto>.NotFound("Product not found");
            }
            var productDto = new ProductReadDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Unit = p.Unit,
                UrlImage = p.UrlImage,
                Price = p.Price,
                Stock = p.Stock,
                Reviews = p.Reviews,
                Rate = p.Rate,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            };

            _unitOfWork._productRepository.DeleteAsync(p);


            await _unitOfWork.SaveAsync();
            return GeneralResult<ProductReadDto>.SuccessResult(productDto);
        }
        public async Task<GeneralResult<ImageResultDto>> UploadImageProduct(Guid id, String urlImage)
        {
            Product p = await _unitOfWork._productRepository.GetById(id);
            if (p is null)
            {
                return GeneralResult<ImageResultDto>.NotFound("Product not found");
            }
            p.UrlImage = urlImage;

            ImageResultDto image = new ImageResultDto(urlImage);
            await _unitOfWork.SaveAsync();
            return GeneralResult<ImageResultDto>.SuccessResult(image);
        }

    }
}
