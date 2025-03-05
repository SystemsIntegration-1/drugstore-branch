using drugstore_branch.Domain.Model;

namespace drugstore_branch.Domain.Repository;

/* 
 * Defines the contract for product data operations, including adding, 
 * retrieving, updating, and searching for products.
 */
public interface IProductRepository
{
    Task AddAsync(Product product);
    Task<List<Product>> GetAllAsync();
    Task<Product> GetByIdAsync(Guid id);
    Task UpdateAsync(Product product);
    Task<List<Product>> GetByNameAsync(string name);
}
