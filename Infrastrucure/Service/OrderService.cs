using AutoMapper;
using drugstore_branch.Domain.Dto;
using drugstore_branch.Domain.Model;
using drugstore_branch.Domain.Repository;
using drugstore_branch.Domain.Service;

namespace drugstore_branch.Infrastrucure.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        
        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        
        public async Task<OrderDto> CreateOrder(CreateOrderDto createOrderDto)
        {
            if (createOrderDto.ProductQuantities == null || !createOrderDto.ProductQuantities.Any())
            {
                throw new ArgumentException("ProductQuantities cannot be null or empty");
            }
            var order = new Order
            {
                TotalPrice = createOrderDto.TotalPrice,
                ProductQuantities = createOrderDto.ProductQuantities
            };

            var createdOrder = await _orderRepository.Create(order);
            return _mapper.Map<OrderDto>(createdOrder);
        }
        
        public async Task<List<OrderDto>> GetAllOrders()
        {
            var orders = await _orderRepository.ReadAllAsync();
            return _mapper.Map<List<OrderDto>>(orders);
        }
        
        public async Task<List<OrderDto>> GerAllOrdersByDate(DateTime date)
        {
            var orders = await _orderRepository.ReadAsync("order_date", date);
            return _mapper.Map<List<OrderDto>>(orders);
        }
        
        public async Task<OrderDto> GetOrderById(Guid id)
        {
            var orders = await _orderRepository.ReadAllAsync();
            var order = System.Linq.Enumerable.FirstOrDefault(orders, o => o.Id == id);
            return _mapper.Map<OrderDto>(order);
        }
    }
}