using AutoMapper;
using drugstore_branch.Domain.Dto;
using drugstore_branch.Domain.Model;
using drugstore_branch.Domain.Repository;
using drugstore_branch.Domain.Service;

namespace drugstore_branch.Infrastrucure.Service;

/* 
 * The ProductService class implements the IProductService interface and provides methods 
 * to manage product-related operations such as creating, retrieving, updating, and searching products. 
 * It interacts with the IProductRepository to perform CRUD operations and uses AutoMapper 
 * to map between domain models and DTOs.
 */
public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    /* 
     * Constructor that initializes the ProductService with the IProductRepository 
     * and IMapper dependencies.
     * @param productRepository - The repository to access product data.
     * @param mapper - The AutoMapper instance to map between models and DTOs.
     */
    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    /* 
     * Creates a new product based on the provided CreateProductDto.
     * Maps the DTO to a Product model, assigns a new GUID to the product's Id, 
     * and adds it to the repository.
     * @param createProductDto - The DTO containing the product details.
     * @return The created ProductDto.
     */
    public async Task<ProductDto> CreateProduct(CreateProductDto createProductDto)
    {
        var product = _mapper.Map<Product>(createProductDto);
        product.Id = Guid.NewGuid();

        await _productRepository.AddAsync(product);
        return _mapper.Map<ProductDto>(product);
    }

    /* 
     * Retrieves all products from the repository and maps them to ProductDto.
     * @return A list of ProductDto representing all products.
     */
    public async Task<List<ProductDto>> GetAllProducts()
    {
        var products = await _productRepository.GetAllAsync();
        return _mapper.Map<List<ProductDto>>(products);
    }

    /* 
     * Retrieves a product by its ID from the repository and maps it to ProductDto.
     * @param id - The unique identifier of the product.
     * @return The corresponding ProductDto, or null if the product is not found.
     */
    public async Task<ProductDto> GetProductById(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        return _mapper.Map<ProductDto>(product);
    }

    /* 
     * Updates an existing product with the provided UpdateProductDto.
     * If the product is found, it maps the updates from the DTO to the product model,
     * and saves the updated product back to the repository.
     * @param id - The unique identifier of the product to update.
     * @param updateProductDto - The DTO containing the updated product details.
     * @return The updated ProductDto, or null if the product is not found.
     */
    public async Task<ProductDto> UpdateProduct(Guid id, UpdateProductDto updateProductDto)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null) return null;

        _mapper.Map(updateProductDto, product);
        await _productRepository.UpdateAsync(product);
        
        return _mapper.Map<ProductDto>(product);
    }

    /* 
     * Searches for products by their name using a case-insensitive match.
     * @param name - The name to search for in the products.
     * @return A list of ProductDto representing products with names matching the search term.
     */
    public async Task<List<ProductDto>> SearchProductsByName(string name)
    {
        var products = await _productRepository.GetByNameAsync(name);
        return _mapper.Map<List<ProductDto>>(products);
    }
}
