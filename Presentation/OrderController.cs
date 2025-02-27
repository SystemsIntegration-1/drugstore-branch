using AutoMapper;
using drugstore_branch.Domain.Dto;
using drugstore_branch.Domain.Model;
using drugstore_branch.Domain.Service;
using Microsoft.AspNetCore.Mvc;

namespace drugstore_branch.Presentation;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;
        
    public OrderController(IOrderService orderService, IMapper mapper)
    {
        _orderService = orderService;
        _mapper = mapper;
    }
        
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto createOrderDto)
    {
        var order = new Order
        {
            TotalPrice = createOrderDto.TotalPrice,
            Products = createOrderDto.ProductIds.Select(pid => new Product { Id = pid }).ToList()
        };
            
        var createdOrderDto = await _orderService.CreateOrder(order);
        return CreatedAtAction(nameof(GetOrderById), new { id = createdOrderDto.Id }, createdOrderDto);
    }


        
    [HttpGet]
    public async Task<IActionResult> GetAllOrders()
    {
        var orders = await _orderService.GetAllOrders();
        return Ok(orders);
    }
        
    [HttpGet("by-date")]
    public async Task<IActionResult> GetOrdersByDate([FromQuery] DateTime date)
    {
        var orders = await _orderService.GerAllOrdersByDate(date);
        return Ok(orders);
    }
        
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(Guid id)
    {
        var order = await _orderService.GetOrderById(id);
        if (order == null)
            return NotFound();
        return Ok(order);
    }
}