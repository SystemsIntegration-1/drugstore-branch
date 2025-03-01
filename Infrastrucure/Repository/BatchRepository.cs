using drugstore_branch.Domain.Model;
using drugstore_branch.Domain.Repository;
using Npgsql;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace drugstore_branch.Infrastructure.Repository;

public class BatchRepository : IBatchRepository
{
    private readonly string _connectionString;

    public BatchRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
    }

    private NpgsqlConnection GetConnection() => new NpgsqlConnection(_connectionString);

    public async Task AddAsync(Batch batch)
    {
        using var connection = GetConnection();
        var sql = @"
        INSERT INTO batches (id, product_id, stock, entry_date, expiration_date) 
        VALUES (@Id, @ProductId, @Stock, @EntryDate, @ExpirationDate)";
    
        await connection.ExecuteAsync(sql, batch);
    }



    public async Task<List<Batch>> GetAllAsync()
    {
        using var connection = GetConnection();
        var sql = "SELECT * FROM batches";
        var batches = await connection.QueryAsync<Batch>(sql);
        return batches.ToList();
    }

    public async Task<Batch> GetByIdAsync(Guid id)
    {
        using var connection = GetConnection();
        var sql = "SELECT * FROM batches WHERE id = @Id";
        return await connection.QuerySingleOrDefaultAsync<Batch>(sql, new { Id = id });
    }

    public async Task<List<Batch>> GetByProductIdAsync(Guid productId)
    {
        using var connection = GetConnection();
        var sql = "SELECT * FROM batches WHERE product_id = @ProductId";
        var batches = await connection.QueryAsync<Batch>(sql, new { ProductId = productId });
        return batches.ToList();
    }

    public async Task UpdateAsync(Batch batch)
    {
        using var connection = GetConnection();
        var sql = @"
            UPDATE batches 
            SET stock = @Stock, entry_date = @EntryDate, expiration_date = @ExpirationDate
            WHERE id = @Id";
        await connection.ExecuteAsync(sql, batch);
    }

    public async Task DeleteAsync(Guid id)
    {
        using var connection = GetConnection();
        var sql = "DELETE FROM batches WHERE id = @Id";
        await connection.ExecuteAsync(sql, new { Id = id });
    }
}
