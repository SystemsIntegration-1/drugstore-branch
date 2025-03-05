using drugstore_branch.Domain.Dto;

namespace drugstore_branch.Domain.Service;

/* 
 * Defines the contract for product-related operations in the service layer.
 * Includes methods for creating products, retrieving all products, 
 * updating product details, searching products by name, and fetching 
 * product details by ID.
 */
public interface IProductService
{
    Task<ProductDto> CreateProduct(CreateProductDto createProductDto);
    Task<List<ProductDto>> GetAllProducts();
    Task<ProductDto> GetProductById(Guid id);
    Task<ProductDto> UpdateProduct(Guid id, UpdateProductDto updateProductDto);
    Task<List<ProductDto>> SearchProductsByName(string name);
}
