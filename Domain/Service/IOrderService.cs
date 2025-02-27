using drugstore_branch.Domain.Dto;
using drugstore_branch.Domain.Model;

namespace drugstore_branch.Domain.Service;

public interface IOrderService
{
    Task<OrderDto> CreateOrder(CreateOrderDto createOrderDto);
    Task<List<OrderDto>> GetAllOrders();
    Task<List<OrderDto>> GerAllOrdersByDate(DateTime date);
    Task<OrderDto> GetOrderById(Guid id);
}