using Dapper;
using drugstore_branch.Domain.Model;
using drugstore_branch.Domain.Repository;
using Npgsql;
using Microsoft.Extensions.Configuration;

namespace drugstore_branch.Infrastrucure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string _connectionString;

        public OrderRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        private NpgsqlConnection GetConnection() => new NpgsqlConnection(_connectionString);

        public async Task<Order> Create(Order entity)
        {
            using var connection = GetConnection();
            var sql = @"
                INSERT INTO orders (total_price)
                VALUES (@TotalPrice)
                RETURNING id, total_price, order_date";
            var insertedOrder = await connection.QuerySingleAsync<Order>(sql, new { entity.TotalPrice });
            entity.Id = insertedOrder.Id;
            entity.OrderDate = insertedOrder.OrderDate;

            if (entity.ProductQuantities != null && entity.ProductQuantities.Count > 0)
            {
                foreach (var kvp in entity.ProductQuantities)
                {
                    var sqlRelation = "INSERT INTO order_products (order_id, product_id, quantity) VALUES (@OrderId, @ProductId, @Quantity)";
                    await connection.ExecuteAsync(sqlRelation, new { OrderId = entity.Id, ProductId = kvp.Key, Quantity = kvp.Value });
                }
            }
            return entity;
        }

        public async Task<IEnumerable<Order>> ReadAllAsync()
        {
            using var connection = GetConnection();
            var sql = "SELECT * FROM orders";
            var orders = await connection.QueryAsync<Order>(sql);

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

        public List<string> SetParams() => new List<string> { "order_date", "total_price" };

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
}
