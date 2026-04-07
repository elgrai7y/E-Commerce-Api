using E_Commerce.Common;
using E_Commerce.Common.GeneralResult;
using E_Commerce.Common.Pagination;
using E_Commerce.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.BLL
{
    public class CategoryManager : ICategoryManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<GeneralResult<IEnumerable<CategoryReadDto>>> GetAllCategory()
        {
            var categories = await _unitOfWork._categoryRepository.GetCategoryWithPoductsAsync();
            var categoryDtos = categories.Select(c => new CategoryReadDto
            {
                Id = c.Id,
                Name = c.Name,
                ProductsCount = c.Products.Count(),
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            }).ToList();
            return GeneralResult<IEnumerable<CategoryReadDto>>.SuccessResult(categoryDtos);

        }
        public async Task<GeneralResult<PageResult<Category>>> GetAllCategoryWithPagination(PaginationParameters paginationParameters)
        {
            PageResult<Category> categories = await _unitOfWork._categoryRepository.GetWithPaginationAsync( paginationParameters);
           
            return GeneralResult<PageResult<Category>>.SuccessResult(categories);

        }

        public async Task<GeneralResult<CategoryReadDto>> GetCategoryById(Guid id)
        {
            var category = await _unitOfWork._categoryRepository.GetCategoryWithPoductsByIdAsync(id);
            if (category is null)
            {
                return GeneralResult<CategoryReadDto>.NotFound("Category not found");
            }
            var categoryDto = new CategoryReadDto
            {
                Id = category.Id,
                Name = category.Name,
                ProductsCount = category.Products.Count(),
                CreatedAt = category.CreatedAt,
                UpdatedAt = category.UpdatedAt
            };
            return GeneralResult<CategoryReadDto>.SuccessResult(categoryDto);

        }
        public async Task<GeneralResult<IEnumerable< ProductReadDto>>> GetCategorysProductsById(Guid id)
        {
            var category = await _unitOfWork._categoryRepository.GetCategoryWithPoductsByIdAsync(id);
            if (category is null)
            {
                return GeneralResult< IEnumerable < ProductReadDto >>.NotFound("Category not found");
            }
            var products = category.Products.Select(p => new ProductReadDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Price = p.Price,
                UrlImage = p.UrlImage,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            }) .ToList();
           
            return GeneralResult< IEnumerable < ProductReadDto >>.SuccessResult(products);

        }
        public async Task<GeneralResult<CategoryCreateDto>> CreateCategory(CategoryCreateDto categoryCreateDto)
        {
            var category = new Category
            {
                Name = categoryCreateDto.Name,
                Description = categoryCreateDto.Description,
                UrlImage = categoryCreateDto.UrlImage
            };
            _unitOfWork._categoryRepository.Add(category);
            await _unitOfWork.SaveAsync();
            return GeneralResult<CategoryCreateDto>.SuccessResult(categoryCreateDto);
        }

        public async Task<GeneralResult> DeleteCategory(Guid id)
        {
            var category = await _unitOfWork._categoryRepository.GetById(id);
            if (category is null)
            {
                return GeneralResult.NotFound("Category not found");
            }
            _unitOfWork._categoryRepository.DeleteAsync(category);
            await _unitOfWork.SaveAsync();
            return GeneralResult.SuccessResult("Category deleted successfully");
        }

        public async Task<GeneralResult> EditCategory(CategoryEditDto categoryEditDto)
        {
            var category = await _unitOfWork._categoryRepository.GetById(categoryEditDto.Id);
            if (category is null)
            {
                return GeneralResult.NotFound("Category not found");
            }
            category.Name = categoryEditDto.Name;
            category.Description = categoryEditDto.Description;
            category.UrlImage = categoryEditDto.UrlImage;
            await _unitOfWork.SaveAsync();
            return GeneralResult.SuccessResult("Category updated successfully");
        }
        public async Task<GeneralResult<ImageResultDto>> UploadImageCategory(Guid id, string urlImage)
        {
            var category = await _unitOfWork._categoryRepository.GetById(id);
            if (category is null)
            {
                return GeneralResult<ImageResultDto>.NotFound("Category not found");
            }
            category.UrlImage = urlImage;
            var image = new ImageResultDto(urlImage);
            await _unitOfWork.SaveAsync();
            return GeneralResult<ImageResultDto>.SuccessResult(image);
        }
    }

}
