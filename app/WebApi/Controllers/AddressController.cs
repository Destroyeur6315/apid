using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductExercise;
using WebApi.Dto;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AddressController : ControllerBase
{
    private readonly DBContext _context;
    private readonly ILogger<AddressController> logger;
    
    public AddressController(DBContext context, ILogger<AddressController> logger)
    {
        _context = context;
            this.logger = logger;
    }
    
    // Endpoint pour afficher une page web afin de gérer les adresses
    [HttpGet]
    [Route("view")]
    public IActionResult GetHtmlPage()
    {
        return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html"), "text/html");
    }
    
    // Endpont pour obtenir toutes les adresses
    [HttpGet]
    public async Task<IActionResult> GetAddresses()
    {
        var addresses = await _context.Addresses.ToListAsync();
        return Ok(addresses);
    }
    
    // Endpoint pour obtenir une adresse par id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAddress([FromRoute] int id)
    {
        var address = await _context.Addresses.FindAsync(id);
        if (address == null) return NotFound();
        return Ok(address);
    }
    
    // Endpoint pour ajouter une nouvelle adresse 
    [HttpPost]
    public async Task<IActionResult> CreateAddress([FromBody] AddressDto addressDto)
    {
        
        
        var address = new Address
        {
            Street = addressDto.Street,
            ZipCode = addressDto.ZipCode,
            City = addressDto.City,
            Country = addressDto.Country,
            ClientId = addressDto.ClientId
        };

        _context.Addresses.Add(address);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAddress), new { id = address.AddressId }, address);
    }
    
    // Endpoint pour modifier une adresse
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateAddress(
        [FromRoute] int id,
        [FromBody] AddressDto addressDto
    )
    {

        var address = await _context.Addresses.FindAsync(id);
        if (address == null) return NotFound();
        
        var client = await _context.Clients.FindAsync(addressDto.ClientId);
        if (client == null) return NotFound("Client not found");

        address.Street = addressDto.Street;
        address.ZipCode = addressDto.ZipCode;
        address.City = addressDto.City;
        address.Country = addressDto.Country;
        address.ClientId = addressDto.ClientId;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // Endpoint pour supprimer une adresse par id
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAddress([FromRoute] int id)
    {
        var address = await _context.Addresses.FindAsync(id);
        if (address == null) return NotFound();

        _context.Addresses.Remove(address);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}