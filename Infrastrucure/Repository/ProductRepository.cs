using drugstore_branch.Domain.Model;
using drugstore_branch.Domain.Repository;
using Npgsql;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace drugstore_branch.Infrastrucure.Repository;

/*
 * The ProductRepository class implements the IProductRepository interface and provides methods 
 * to interact with the database for CRUD operations related to the Product model. 
 * It uses Dapper as the ORM to execute SQL queries and map results to the Product model.
 */
public class ProductRepository : IProductRepository
{
    private readonly string _connectionString;

    /*
     * Constructor that initializes the ProductRepository with the provided configuration 
     * to retrieve the database connection string.
     * @param configuration - The application configuration to access connection string.
     */
    public ProductRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
    }

    /*
     * Creates and returns a new NpgsqlConnection using the connection string.
     * @return A new NpgsqlConnection instance.
     */
    private NpgsqlConnection GetConnection() => new NpgsqlConnection(_connectionString);

    /*
     * Adds a new product to the database.
     * @param product - The product to be added.
     */
    public async Task AddAsync(Product product)
    {
        using var connection = GetConnection();
        var sql = @"
        INSERT INTO products (id, shared_id, name, description, price, category, warehouse_location) 
        VALUES (@Id, @SharedId, @Name, @Description, @Price, @Category, @WarehouseLocation)";
        await connection.ExecuteAsync(sql, product);
    }

    /*
     * Retrieves all products from the database.
     * @return A list of products from the products table.
     */
    public async Task<List<Product>> GetAllAsync()
    {
        using var connection = GetConnection();
        var sql = "SELECT * FROM products";
        var products = await connection.QueryAsync<Product>(sql);
        return products.ToList();
    }

    /*
     * Retrieves a product by its unique identifier.
     * @param id - The unique identifier of the product.
     * @return The product with the given ID, or null if not found.
     */
    public async Task<Product> GetByIdAsync(Guid id)
    {
        using var connection = GetConnection();
        var sql = "SELECT * FROM products WHERE id = @Id";
        return await connection.QuerySingleOrDefaultAsync<Product>(sql, new { Id = id });
    }

    /*
     * Updates an existing product in the database.
     * @param product - The product with updated details.
     */
    public async Task UpdateAsync(Product product)
    {
        using var connection = GetConnection();
        var sql = @"
            UPDATE products 
            SET name = @Name, description = @Description, price = @Price,
            WHERE id = @Id";
        await connection.ExecuteAsync(sql, product);
    }

    /*
     * Retrieves products based on the product name, performing a case-insensitive search.
     * @param name - The product name to search for.
     * @return A list of products whose name matches the given pattern.
     */
    public async Task<List<Product>> GetByNameAsync(string name)
    {
        using var connection = GetConnection();
        var sql = "SELECT * FROM products WHERE name ILIKE @Name";
        var products = await connection.QueryAsync<Product>(sql, new { Name = $"%{name}%" });
        return products.ToList();
    }
}
