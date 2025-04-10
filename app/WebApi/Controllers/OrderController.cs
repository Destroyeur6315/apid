using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductExercise;
using WebApi.Dto;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly DBContext _context;

    public OrderController(DBContext context)
    {
        _context = context;
    }
    
    // Endpoint pour récupérer toutes les commandes
    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var orders = await _context.Orders
            .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
            .ToListAsync();
        return Ok(orders);
    }
    
    // Endpoint pour récupérer une commande par id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder([FromRoute] int id)
    {
        var order = await _context.Orders
            .Include(o => o.OrderProducts)
                .ThenInclude(p => p.Product)
            .FirstOrDefaultAsync(p => p.OrderId == id);
        
        if (order == null) return NotFound();
        return Ok(order);
    }
    
    // Endpoint pour récupérer les commandes entre deux dates
    [HttpGet("periode")]
    public async Task<ActionResult> GetOrdersByPeriod(DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate)
        {
            return BadRequest("La date de début ne peut pas être postérieure à la date de fin.");
        }

        var orders = await _context.Orders
            .Where(o => o.Date >= startDate && o.Date <= endDate)
            .ToListAsync();

        return Ok(orders);
    }
    
    // Endpoint pour créer une commande
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] OrderDto orderDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var order = new Order
        {
            Date = orderDto.Date,
            ClientId = orderDto.ClientId,
            OrderProducts = orderDto.OrderProducts.Select(op => new OrderProduct
            {
                ProductId = op.ProductId,
                Quantity = op.Quantity
            }).ToList()
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetOrder), new { id = order.OrderId }, order);
    }
    
    // Endpoint pour modifier une commande
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateOrder([FromRoute] int id, [FromBody] OrderDto orderDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var order = await _context.Orders
            .Include(o => o.OrderProducts)
            .FirstOrDefaultAsync(o => o.OrderId == id);
        if (order == null) return NotFound();

        order.Date = orderDto.Date;
        order.ClientId = orderDto.ClientId;

        // Update OrderProducts
        order.OrderProducts.Clear();
        
        foreach (var op in orderDto.OrderProducts)
        {
            order.OrderProducts.Add(new OrderProduct
            {
                OrderId = order.OrderId,
                ProductId = op.ProductId,
                Quantity = op.Quantity
            });
        }

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // Endpoint pour supprimer une commande
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder([FromRoute] int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null) return NotFound();

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}