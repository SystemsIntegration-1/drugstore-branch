using drugstore_branch.Domain.Dto;

namespace drugstore_branch.Domain.Service;

public interface IProductService
{
    Task<ProductDto> CreateProduct(CreateProductDto createProductDto);
    Task<List<ProductDto>> GetAllProducts();
    Task<ProductDto> GetProductById(Guid id);
    Task<ProductDto> UpdateProduct(Guid id, UpdateProductDto updateProductDto);
    Task<List<ProductDto>> SearchProductsByName(string name);
}
