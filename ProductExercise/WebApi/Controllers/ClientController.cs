using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductExercise;
using WebApi.Dto;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly DBContext _context;
        public ClientController(
            DBContext context
            )
        {
            _context = context;
        }
        
        // Endpoint pour rechercher les clients par nom
        [HttpGet("nom")]
        public async Task<IActionResult> GetClientsByName(string nom)
        {
            if (string.IsNullOrWhiteSpace(nom))
            {
                return BadRequest("Le nom ne peut pas être vide.");
            }

            var clients = await _context.Clients
                .Where(c => c.LastName.Contains(nom))
                .ToListAsync();

            return Ok(clients);
        }

        // Endpoint pour rechercher les clients par pays
        [HttpGet("pays")]
        public async Task<IActionResult> GetClientsByCountry(string pays)
        {
            if (string.IsNullOrWhiteSpace(pays))
            {
                return BadRequest("Le pays ne peut pas être vide.");
            }

            var clients = await _context.Clients
                .Include(c => c.Addresses)
                .Where(c => c.Addresses.Any(a => a.Country == pays))
                .ToListAsync(); 

            return Ok(clients);
        }
        
        // Endpoint pour récupérer tous les clients
        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            var clients = await _context.Clients.Include(c => c.Addresses).ToListAsync();
            return Ok(clients);
        }
        
        // Endpoint pour rechercher un client par id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClient([FromRoute] int id)
        {
            if (id <= 0) return BadRequest("Invalid client ID.");

            var client = await _context.Clients.Include(c => c.Addresses).FirstOrDefaultAsync(c => c.ClientId == id);
            if (client == null) return NotFound();

            return Ok(client);
        }
        
        // Endpoint pour ajouter un nouveau client
        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] Client client)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
    
            if (client.DateOfBirth >= DateTime.Now) return BadRequest("Veuillez renseigner une date de naissance valide.");

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetClient), new { id = client.ClientId }, client);
        }
        
        // Endpoint pour modifier un client (nom, prénom et date de naissance)
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateClient(int id, [FromBody] ClientDto clientDto)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound("Client non trouvé.");
            }

            // Mise à jour des propriétés si elles sont fournies
            if (!string.IsNullOrWhiteSpace(clientDto.Nom))
                client.LastName = clientDto.Nom;

            if (!string.IsNullOrWhiteSpace(clientDto.Prenom))
                client.FirstName = clientDto.Prenom;

            if (clientDto.DateOfBirth.HasValue)
                client.DateOfBirth = clientDto.DateOfBirth.Value;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Endpoint pour supprimer un client par id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient([FromRoute] int id)
        {
            if (id <= 0) return BadRequest("Invalid client ID.");

            var client = await _context.Clients
                .Include(c => c.Orders)
                .FirstOrDefaultAsync(c => c.ClientId == id);
            if (client == null) return NotFound();

            if (client.Orders.Any()) return BadRequest("Client cannot be deleted because they have orders.");

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}