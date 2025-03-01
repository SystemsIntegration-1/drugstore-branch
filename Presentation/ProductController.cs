using drugstore_branch.Domain.Dto;
using drugstore_branch.Domain.Service;
using Microsoft.AspNetCore.Mvc;

namespace drugstore_branch.Presentation;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto createProductDto)
    {
        var product = await _productService.CreateProduct(createProductDto);
        return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _productService.GetAllProducts();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        var product = await _productService.GetProductById(id);
        return product != null ? Ok(product) : NotFound();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductDto updateProductDto)
    {
        var product = await _productService.UpdateProduct(id, updateProductDto);
        return Ok(product);
    }

    [HttpGet("search/{name}")]
    public async Task<IActionResult> SearchProductsByName(string name)
    {
        var products = await _productService.SearchProductsByName(name);
        return Ok(products);
    }
}