using Dapper;
using drugstore_branch.Domain.Model;
using drugstore_branch.Domain.Repository;
using Npgsql;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace drugstore_branch.Infrastrucure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string _connectionString;

        public OrderRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") 
                                ?? throw new ArgumentNullException("DefaultConnection is missing in configuration.");
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        private NpgsqlConnection GetConnection() => new NpgsqlConnection(_connectionString);

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
