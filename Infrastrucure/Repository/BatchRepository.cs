using drugstore_branch.Domain.Model;
using drugstore_branch.Domain.Repository;
using Npgsql;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace drugstore_branch.Infrastructure.Repository;

/* 
 * This class implements the IBatchRepository interface and provides the 
 * methods to interact with the 'batches' table in the database. 
 * It performs CRUD operations for Batch entities using Dapper with 
 * PostgreSQL.
 */
public class BatchRepository : IBatchRepository
{
    private readonly string _connectionString;

    /* 
     * Constructor that initializes the repository with the database connection string 
     * from the configuration.
     */
    public BatchRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
    }

    private NpgsqlConnection GetConnection() => new NpgsqlConnection(_connectionString);

    /* 
     * Creates a new batch record in the 'batches' table.
     * @param batch - The Batch entity to be inserted into the database.
     */
    public async Task AddAsync(Batch batch)
    {
        using var connection = GetConnection();
        var sql = @"
        INSERT INTO batches (product_id, stock, entry_date, expiration_date) 
        VALUES (@ProductId, @Stock, @EntryDate, @ExpirationDate)";
    
        await connection.ExecuteAsync(sql, batch);
    }

    /* 
     * Retrieves all batches from the 'batches' table.
     * @return A list of all Batch entities in the database.
     */
    public async Task<List<Batch>> GetAllAsync()
    {
        using var connection = GetConnection();
        var sql = "SELECT * FROM batches";
        var batches = await connection.QueryAsync<Batch>(sql);
        return batches.ToList();
    }

     /* 
     * Retrieves a batch by its ID.
     * @param id - The unique identifier of the batch to retrieve.
     * @return The Batch entity with the specified ID.
     */
    public async Task<Batch> GetByIdAsync(Guid id)
    {
        using var connection = GetConnection();
        var sql = "SELECT * FROM batches WHERE id = @Id";
        return await connection.QuerySingleOrDefaultAsync<Batch>(sql, new { Id = id });
    }

    /* 
     * Retrieves all batches associated with a given product ID.
     * @param productId - The unique identifier of the product to search batches for.
     * @return A list of batches associated with the specified product.
     */
    public async Task<List<Batch>> GetByProductIdAsync(Guid productId)
    {
        using var connection = GetConnection();
        var sql = "SELECT * FROM batches WHERE product_id = @ProductId";
        var batches = await connection.QueryAsync<Batch>(sql, new { ProductId = productId });
        return batches.ToList();
    }

    /* 
     * Retrieves all batches associated with a given shared product ID.
     * @param sharedId - The shared identifier of the product to search batches for.
     * @return A list of batches associated with the specified shared product ID.
     */
    public async Task<List<Batch>> GetBySharedIdAsync(Guid sharedId)
    {
        using var connection = GetConnection();
        var sql = @"
            SELECT b.* 
            FROM batches b
            JOIN products p ON b.product_id = p.id
            WHERE p.shared_id = @SharedId";
        
        var batches = await connection.QueryAsync<Batch>(sql, new { SharedId = sharedId });
        return batches.ToList();
    }

    /* 
     * Updates an existing batch record in the 'batches' table.
     * @param batch - The Batch entity containing the updated values.
     */
    public async Task UpdateAsync(Batch batch)
    {
        using var connection = GetConnection();
        var sql = @"
            UPDATE batches 
            SET stock = @Stock, entry_date = @EntryDate, expiration_date = @ExpirationDate
            WHERE id = @Id";
        await connection.ExecuteAsync(sql, batch);
    }

    /* 
     * Deletes a batch record by its ID from the 'batches' table.
     * @param id - The unique identifier of the batch to delete.
     */
    public async Task DeleteAsync(Guid id)
    {
        using var connection = GetConnection();
        var sql = "DELETE FROM batches WHERE id = @Id";
        await connection.ExecuteAsync(sql, new { Id = id });
    }
}
