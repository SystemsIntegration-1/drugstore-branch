namespace drugstore_branch.Infrastrucure.Service;

using AutoMapper;
using drugstore_branch.Domain.Dto;
using drugstore_branch.Domain.Model;
using drugstore_branch.Domain.Repository;
using drugstore_branch.Domain.Service;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ProductDto> CreateProduct(CreateProductDto createProductDto)
    {
        var product = _mapper.Map<Product>(createProductDto);
        product.Id = Guid.NewGuid();

        await _productRepository.AddAsync(product);
        return _mapper.Map<ProductDto>(product);
    }

    public async Task<List<ProductDto>> GetAllProducts()
    {
        var products = await _productRepository.GetAllAsync();
        return _mapper.Map<List<ProductDto>>(products);
    }

    public async Task<ProductDto> GetProductById(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        return _mapper.Map<ProductDto>(product);
    }

    public async Task<ProductDto> UpdateProduct(Guid id, UpdateProductDto updateProductDto)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null) return null;

        _mapper.Map(updateProductDto, product);
        await _productRepository.UpdateAsync(product);
        
        return _mapper.Map<ProductDto>(product);
    }

    public async Task<List<ProductDto>> SearchProductsByName(string name)
    {
        var products = await _productRepository.GetByNameAsync(name);
        return _mapper.Map<List<ProductDto>>(products);
    }
}
