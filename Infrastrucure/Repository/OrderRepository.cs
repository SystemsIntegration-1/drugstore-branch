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
        }

        private NpgsqlConnection GetConnection() => new NpgsqlConnection(_connectionString);

        public async Task<Order> Create(Order entity)
        {
            using var connection = GetConnection();
            // Usamos nombres en minúsculas según la base de datos
            var sql = "INSERT INTO orders (id, total_price, order_date) VALUES (@Id, @TotalPrice, @OrderDate)";
            await connection.ExecuteAsync(sql, new { entity.Id, entity.TotalPrice, entity.OrderDate });

            if (entity.Products != null && entity.Products.Count > 0)
            {
                foreach (var product in entity.Products)
                {
                    var sqlRelation = "INSERT INTO order_products (order_id, product_id) VALUES (@OrderId, @ProductId)";
                    await connection.ExecuteAsync(sqlRelation, new { OrderId = entity.Id, ProductId = product.Id });
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
                var sqlProducts = @"SELECT p.* 
                                    FROM products p
                                    INNER JOIN order_products op ON p.id = op.product_id
                                    WHERE op.order_id = @OrderId";
                var products = await connection.QueryAsync<Product>(sqlProducts, new { OrderId = order.Id });
                order.Products = products.AsList();
            }
            return orders;
        }

        public List<string> SetParams() => new List<string> { "order_date", "total_price" };

        public async Task<IEnumerable<Order>> SearchDbAsync(string parameter, object search)
        {
            using var connection = GetConnection();
            // Se asume que 'parameter' viene en minúsculas y coincide con el de la base de datos
            string sql = $"SELECT * FROM orders WHERE {parameter} = @Search";
            var orders = await connection.QueryAsync<Order>(sql, new { Search = search });

            foreach (var order in orders)
            {
                var sqlProducts = @"SELECT p.* 
                                    FROM products p
                                    INNER JOIN order_products op ON p.id = op.product_id
                                    WHERE op.order_id = @OrderId";
                var products = await connection.QueryAsync<Product>(sqlProducts, new { OrderId = order.Id });
                order.Products = products.AsList();
            }
            return orders;
        }
    }
}
