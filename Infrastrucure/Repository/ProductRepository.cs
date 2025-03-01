using drugstore_branch.Domain.Model;
using drugstore_branch.Domain.Repository;
using Npgsql;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace drugstore_branch.Infrastrucure.Repository;

public class ProductRepository : IProductRepository
{
    private readonly string _connectionString;

    public ProductRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
    }

    private NpgsqlConnection GetConnection() => new NpgsqlConnection(_connectionString);

    public async Task AddAsync(Product product)
    {
        using var connection = GetConnection();
        var sql = @"
        INSERT INTO products (id, shared_id, name, description, price, category, warehouse_location) 
        VALUES (@Id, @SharedId, @Name, @Description, @Price, @Category, @WarehouseLocation)";
        await connection.ExecuteAsync(sql, product);
    }

    public async Task<List<Product>> GetAllAsync()
    {
        using var connection = GetConnection();
        var sql = "SELECT * FROM products";
        var products = await connection.QueryAsync<Product>(sql);
        return products.ToList();
    }

    public async Task<Product> GetByIdAsync(Guid id)
    {
        using var connection = GetConnection();
        var sql = "SELECT * FROM products WHERE id = @Id";
        return await connection.QuerySingleOrDefaultAsync<Product>(sql, new { Id = id });
    }

    public async Task UpdateAsync(Product product)
    {
        using var connection = GetConnection();
        var sql = @"
            UPDATE products 
            SET name = @Name, description = @Description, price = @Price,
            WHERE id = @Id";
        await connection.ExecuteAsync(sql, product);
    }

    public async Task<List<Product>> GetByNameAsync(string name)
    {
        using var connection = GetConnection();
        var sql = "SELECT * FROM products WHERE name ILIKE @Name";
        var products = await connection.QueryAsync<Product>(sql, new { Name = $"%{name}%" });
        return products.ToList();
    }
}
