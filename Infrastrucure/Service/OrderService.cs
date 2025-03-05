using AutoMapper;
using drugstore_branch.Domain.Dto;
using drugstore_branch.Domain.Model;
using drugstore_branch.Domain.Repository;
using drugstore_branch.Domain.Service;

namespace drugstore_branch.Infrastrucure.Service;

/* 
 * The OrderService class implements the IOrderService interface and provides methods 
 * to manage order-related operations such as creating, retrieving, and filtering orders. 
 * It interacts with the IOrderRepository to perform CRUD operations and uses AutoMapper 
 * to map between domain models and DTOs.
 */
public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    /* 
     * Constructor that initializes the OrderService with the IOrderRepository 
     * and IMapper dependencies.
     * @param orderRepository - The repository to access order data.
     * @param mapper - The AutoMapper instance to map between models and DTOs.
     */
    public OrderService(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    /* 
     * Creates a new order based on the provided CreateOrderDto. 
     * Validates if ProductQuantities is not null or empty. 
     * Then maps the order to an OrderDto and returns it.
     * @param createOrderDto - The DTO containing the order details.
     * @return The created OrderDto.
     * @throws ArgumentException if ProductQuantities is null or empty.
     */
    public async Task<OrderDto> CreateOrder(CreateOrderDto createOrderDto)
    {
        if (createOrderDto.ProductQuantities == null || !createOrderDto.ProductQuantities.Any())
        {
            throw new ArgumentException("ProductQuantities cannot be null or empty");
        }

        var order = new Order
        {
            TotalPrice = createOrderDto.TotalPrice,
            ProductQuantities = createOrderDto.ProductQuantities,
            OrderDate = DateTime.UtcNow
        };

        var createdOrder = await _orderRepository.Create(order);
        return _mapper.Map<OrderDto>(createdOrder);
    }

    /* 
     * Retrieves all orders from the repository and maps them to OrderDto.
     * @return A list of OrderDto representing all orders.
     */
    public async Task<List<OrderDto>> GetAllOrders()
    {
        var orders = await _orderRepository.ReadAllAsync();
        return _mapper.Map<List<OrderDto>>(orders);
    }

    /* 
     * Retrieves all orders by the specified order date from the repository and maps them to OrderDto.
     * @param date - The order date to filter by.
     * @return A list of OrderDto representing orders created on the specified date.
     */
    public async Task<List<OrderDto>> GerAllOrdersByDate(DateTime date)
    {
        var orders = await _orderRepository.ReadAsync("order_date", date);
        return _mapper.Map<List<OrderDto>>(orders);
    }

    /* 
     * Retrieves an order by its ID from the repository and maps it to OrderDto.
     * @param id - The unique identifier of the order.
     * @return The corresponding OrderDto, or null if the order is not found.
     */
    public async Task<OrderDto> GetOrderById(Guid id)
    {
        var orders = await _orderRepository.ReadAllAsync();
        var order = System.Linq.Enumerable.FirstOrDefault(orders, o => o.Id == id);
        return _mapper.Map<OrderDto>(order);
    }
}
