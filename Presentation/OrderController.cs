using AutoMapper;
using drugstore_branch.Domain.Dto;
using drugstore_branch.Domain.Service;
using Microsoft.AspNetCore.Mvc;

namespace drugstore_branch.Presentation;

/*
 * The OrderController class is an API controller that handles HTTP requests 
 * related to orders. It provides endpoints for creating, retrieving, and 
 * filtering orders by date or ID. The controller uses the IOrderService to 
 * interact with the order-related business logic and provides responses in 
 * the form of DTOs.
 */
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
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var createdOrderDto = await _orderService.CreateOrder(createOrderDto);
        return CreatedAtAction(nameof(GetOrderById), new { id = createdOrderDto.Id }, createdOrderDto);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllOrders()
    {
        try
    {
        var orders = await _orderService.GetAllOrders();
        return Ok(orders);
    }
    catch (Exception ex)
    {
        return StatusCode(500, "Error interno del servidor: " + ex.Message);
    }
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
