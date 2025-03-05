using Dapper;
using drugstore_branch.Domain.Model;
using drugstore_branch.Domain.Repository;
using Npgsql;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace drugstore_branch.Infrastrucure.Repository;

/* 
 * This class implements the IOrderRepository interface and provides methods 
 * to interact with the 'orders' table in the database. It performs CRUD 
 * operations for Order entities using Dapper with PostgreSQL and handles 
 * serialization of product quantities using JSON.
 */
public class OrderRepository : IOrderRepository
{
    private readonly string _connectionString;

    /* 
     * Constructor that initializes the repository with the connection string 
     * from the configuration.
     */
    public OrderRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") 
                            ?? throw new ArgumentNullException("DefaultConnection is missing in configuration.");
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
    }

    /* 
     * Returns a new instance of NpgsqlConnection using the connection string.
     * @return A new NpgsqlConnection object.
     */
    private NpgsqlConnection GetConnection() => new NpgsqlConnection(_connectionString);

    /* 
     * Creates a new order in the 'orders' table and returns the created order.
     * @param entity - The Order entity to be created.
     * @return The created Order entity with the assigned ID and order date.
     */
    public async Task<Order> Create(Order entity)
    {
        using var connection = GetConnection();
        var sql = @"
            INSERT INTO orders (total_price, product_quantities, order_date)
            VALUES (@TotalPrice, @ProductQuantities::jsonb, @OrderDate)
            RETURNING id, total_price, order_date";

        var insertedOrder = await connection.QuerySingleAsync<Order>(sql, new 
        { 
            entity.TotalPrice, 
            ProductQuantities = JsonConvert.SerializeObject(entity.ProductQuantities), 
            OrderDate = DateTime.UtcNow 
        });

        entity.Id = insertedOrder.Id;
        entity.OrderDate = insertedOrder.OrderDate;

        return entity;
    }

    /* 
     * Retrieves all orders from the 'orders' table, along with their associated product quantities.
     * @return A list of all orders with their product quantities.
     */
    public async Task<IEnumerable<Order>> ReadAllAsync()
    {
        using var connection = GetConnection();
        var sql = "SELECT * FROM orders";
        var orders = await connection.QueryAsync<Order>(sql);
        Console.WriteLine(sql);

        foreach (var order in orders)
        {
            var sqlQuantities = "SELECT product_id, quantity FROM order_products WHERE order_id = @OrderId";
            var orderProducts = await connection.QueryAsync<(Guid product_id, int quantity)>(sqlQuantities, new { OrderId = order.Id });
            var dict = new Dictionary<Guid, int>();
            foreach (var item in orderProducts)
            {
                dict[item.product_id] = item.quantity;
            }
            order.ProductQuantities = dict;
        }
        return orders;
    }

    /* 
     * Returns a list of parameter names used for order entities.
     * @return A list of parameter names for 'order_date' and 'total_price'.
     */
    public List<string> SetParams() => new List<string> { "order_date", "total_price" };

    /* 
     * Searches the 'orders' table based on a dynamic parameter and search value.
     * @param parameter - The column to search on.
     * @param search - The value to search for in the specified column.
     * @return A list of orders that match the search criteria.
     */
    public async Task<IEnumerable<Order>> SearchDbAsync(string parameter, object search)
    {
        using var connection = GetConnection();
        string sql = $"SELECT * FROM orders WHERE {parameter} = @Search";
        var orders = await connection.QueryAsync<Order>(sql, new { Search = search });

        foreach (var order in orders)
        {
            var sqlQuantities = "SELECT product_id, quantity FROM order_products WHERE order_id = @OrderId";
            var orderProducts = await connection.QueryAsync<(Guid product_id, int quantity)>(sqlQuantities, new { OrderId = order.Id });
            var dict = new Dictionary<Guid, int>();
            foreach (var item in orderProducts)
            {
                dict[item.product_id] = item.quantity;
            }
            order.ProductQuantities = dict;
        }
        return orders;
        
    }
}
