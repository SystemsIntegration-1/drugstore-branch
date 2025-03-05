using drugstore_branch.Domain.Dto;
using drugstore_branch.Domain.Model;

namespace drugstore_branch.Domain.Service;

/* 
 * Defines the contract for order-related operations in the service layer.
 * Provides methods for creating orders, retrieving all orders, 
 * filtering orders by date, and fetching order details by ID.
 */
public interface IOrderService
{
    Task<OrderDto> CreateOrder(CreateOrderDto createOrderDto);
    Task<List<OrderDto>> GetAllOrders();
    Task<List<OrderDto>> GerAllOrdersByDate(DateTime date);
    Task<OrderDto> GetOrderById(Guid id);
}
