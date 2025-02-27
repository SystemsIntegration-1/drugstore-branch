using Dapper;
using drugstore_branch.Domain.Model;
using drugstore_branch.Domain.Repository;
using Npgsql;

namespace drugstore_branch.Infrastrucure.Repository;

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
            var sql = "INSERT INTO Orders (Id, TotalPrice, OrderDate) VALUES (@Id, @TotalPrice, @OrderDate)";
            await connection.ExecuteAsync(sql, new { entity.Id, entity.TotalPrice, entity.OrderDate });

            if (entity.Products != null && entity.Products.Count > 0)
            {
                foreach (var product in entity.Products)
                {
                    var sqlRelation = "INSERT INTO OrderProducts (OrderId, ProductId) VALUES (@OrderId, @ProductId)";
                    await connection.ExecuteAsync(sqlRelation, new { OrderId = entity.Id, ProductId = product.Id });
                }
            }
            return entity;
        }

        public async Task<IEnumerable<Order>> ReadAllAsync()
        {
            using var connection = GetConnection();
            var sql = "SELECT * FROM Orders";
            var orders = await connection.QueryAsync<Order>(sql);

            foreach (var order in orders)
            {
                var sqlProducts = @"SELECT p.* FROM Products p
                                    INNER JOIN OrderProducts op ON p.Id = op.ProductId
                                    WHERE op.OrderId = @OrderId";
                var products = await connection.QueryAsync<Product>(sqlProducts, new { OrderId = order.Id });
                order.Products = products.AsList();
            }
            return orders;
        }

        public List<string> SetParams() => new List<string> { "OrderDate", "TotalPrice" };

        public async Task<IEnumerable<Order>> SearchDbAsync(string parameter, object search)
        {
            using var connection = GetConnection();
            string sql = $"SELECT * FROM Orders WHERE {parameter} = @Search";
            var orders = await connection.QueryAsync<Order>(sql, new { Search = search });

            foreach (var order in orders)
            {
                var sqlProducts = @"SELECT p.* FROM Products p
                                    INNER JOIN OrderProducts op ON p.Id = op.ProductId
                                    WHERE op.OrderId = @OrderId";
                var products = await connection.QueryAsync<Product>(sqlProducts, new { OrderId = order.Id });
                order.Products = products.AsList();
            }
            return orders;
        }
    }