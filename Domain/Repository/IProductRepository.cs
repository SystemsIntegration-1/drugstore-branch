using drugstore_branch.Domain.Model;

namespace drugstore_branch.Domain.Repository;

public interface IProductRepository
{
    Task AddAsync(Product product);
    Task<List<Product>> GetAllAsync();
    Task<Product> GetByIdAsync(Guid id);
    Task UpdateAsync(Product product);
    Task<List<Product>> GetByNameAsync(string name);
}
