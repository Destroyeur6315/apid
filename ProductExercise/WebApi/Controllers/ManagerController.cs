using DAL;
using Microsoft.AspNetCore.Mvc;
using ProductExercise;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly DBContext context;
        private readonly ILogger<ManagerController> logger;

        public ManagerController(DBContext context, ILogger<ManagerController> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult Create()
        {
            logger.Log(LogLevel.Information, "verification si la base existe OU création de la base");
            context.Database.EnsureCreated();
            logger.LogInformation("base de données crée OU déjà présente");

            return Ok("oki");
        }

        [HttpGet("destroy")]
        public IActionResult Destroy()
        {
            context.Database.EnsureDeleted();
            logger.LogInformation("base de données détruite OU non présente");

            return Ok("deleted");
        }
        
        [HttpGet("stub")]
        public async Task<IActionResult> Stub()
        {   
            // Création de commandesProduits
            var commandesproduit = new List<OrderProduct>
            {
                new OrderProduct { OrderProductId = 1, OrderId = 1, ProductId = 1, Quantity = 2 },
                new OrderProduct { OrderProductId = 2, OrderId = 1, ProductId = 2, Quantity = 1 },
                new OrderProduct { OrderProductId = 3, OrderId = 2, ProductId = 2, Quantity = 3 },
                new OrderProduct { OrderProductId = 4, OrderId = 4, ProductId = 4, Quantity = 3 },
                new OrderProduct { OrderProductId = 5, OrderId = 5, ProductId = 6, Quantity = 1 }
            };
            
            // Création de produits
            var products = new List<Product>
            {
                new Product
                {
                    Name = "Smartphone Galaxy X10",
                    Description = "Dernier modèle de smartphone avec écran 6,5 pouces et appareil photo 48MP",
                    Price = 899.99m,
                    UnitInStock = 50,
                    Weight = 0.189
                },
                new Product
                {
                    Name = "Ordinateur portable ProBook",
                    Description = "Ordinateur portable avec processeur i7, 16 Go de RAM et 512 Go SSD",
                    Price = 1299.99m,
                    UnitInStock = 20,
                    Weight = 1.8
                },
                new Product
                {
                    Name = "Écouteurs sans fil EarPods",
                    Description = "Écouteurs Bluetooth avec réduction de bruit active",
                    Price = 199.99m,
                    UnitInStock = 100,
                    Weight = 0.065
                },
                new Product
                {
                    Name = "Montre connectée FitTrack",
                    Description = "Montre intelligente avec suivi d'activité et notifications",
                    Price = 149.99m,
                    UnitInStock = 75,
                    Weight = 0.045
                },
                new Product
                {
                    Name = "Tablette MediaPad",
                    Description = "Tablette 10 pouces avec écran HD et 64 Go de stockage",
                    Price = 349.99m,
                    UnitInStock = 30,
                    Weight = 0.45
                },
                new Product
                {
                    Name = "Enceinte Bluetooth SoundBox",
                    Description = "Enceinte portable avec son stéréo et 10 heures d'autonomie",
                    Price = 79.99m,
                    UnitInStock = 60,
                    Weight = 0.35
                },
                new Product
                {
                    Name = "Caméra d'action AdventureCam",
                    Description = "Caméra 4K étanche pour vos aventures en plein air",
                    Price = 249.99m,
                    UnitInStock = 25,
                    Weight = 0.12
                },
                new Product
                {
                    Name = "Clavier mécanique GamerPro",
                    Description = "Clavier gaming avec rétroéclairage RGB et switches mécaniques",
                    Price = 129.99m,
                    UnitInStock = 40,
                    Weight = 0.98
                },
                new Product
                {
                    Name = "Souris sans fil UltraClick",
                    Description = "Souris ergonomique avec capteur haute précision",
                    Price = 59.99m,
                    UnitInStock = 80,
                    Weight = 0.085
                },
                new Product
                {
                    Name = "Disque dur externe DataBox",
                    Description = "Disque dur 2 To USB 3.0 avec boîtier en aluminium",
                    Price = 89.99m,
                    UnitInStock = 45,
                    Weight = 0.22
                },
                new Product
                {
                    Name = "Imprimante LaserJet",
                    Description = "Imprimante laser monochrome avec connectivité WiFi",
                    Price = 199.99m,
                    UnitInStock = 15,
                    Weight = 4.8
                },
                new Product
                {
                    Name = "Routeur WiFi NetConnect",
                    Description = "Routeur WiFi 6 avec couverture étendue pour grand domicile",
                    Price = 149.99m,
                    UnitInStock = 35,
                    Weight = 0.38
                },
                new Product
                {
                    Name = "Webcam HD StreamPro",
                    Description = "Webcam 1080p avec microphone intégré pour streaming et visioconférence",
                    Price = 69.99m,
                    UnitInStock = 55,
                    Weight = 0.11
                },
                new Product
                {
                    Name = "Batterie externe PowerBank",
                    Description = "Batterie 20000mAh avec charge rapide et 2 ports USB",
                    Price = 39.99m,
                    UnitInStock = 90,
                    Weight = 0.35
                },
                new Product
                {
                    Name = "Casque audio StudioSound",
                    Description = "Casque studio avec son Hi-Fi et arceau confortable",
                    Price = 179.99m,
                    UnitInStock = 25,
                    Weight = 0.28
                }
            };
            
            // Créatoon de commandes
            var orders = new List<Order>
            {
                new Order
                {
                    Date = new DateTime(2025, 1, 15),
                    ClientId = 1
                },
                new Order
                {
                    Date = new DateTime(2025, 2, 3),
                    ClientId = 1
                },
                new Order
                {
                    Date = new DateTime(2025, 3, 10),
                    ClientId = 1
                },
                new Order
                {
                    Date = new DateTime(2025, 1, 20),
                    ClientId = 2
                },
                new Order
                {
                    Date = new DateTime(2025, 2, 15),
                    ClientId = 2
                },
                new Order
                {
                    Date = new DateTime(2025, 1, 5),
                    ClientId = 3
                },
                new Order
                {
                    Date = new DateTime(2025, 2, 10),
                    ClientId = 3
                },
                new Order
                {
                    Date = new DateTime(2025, 3, 15),
                    ClientId = 3
                },
                new Order
                {
                    Date = new DateTime(2025, 1, 25),
                    ClientId = 4
                },
                new Order
                {
                    Date = new DateTime(2025, 2, 20),
                    ClientId = 5
                },
                new Order
                {
                    Date = new DateTime(2025, 3, 5),
                    ClientId = 5
                },
                new Order
                {
                    Date = new DateTime(2025, 1, 12),
                    ClientId = 6
                },
                new Order
                {
                    Date = new DateTime(2025, 2, 22),
                    ClientId = 7
                },
                new Order
                {
                    Date = new DateTime(2025, 3, 18),
                    ClientId = 8
                },
                new Order
                {
                    Date = new DateTime(2025, 1, 30),
                    ClientId = 9
                }
            };
            
            // Création de clients
            var clients = new List<Client>
            {
                new Client
                {
                    FirstName = "Jean",
                    LastName = "Dupont",
                    DateOfBirth = new DateTime(1980, 5, 15)
                },
                new Client
                {
                    FirstName = "Marie",
                    LastName = "Martin",
                    DateOfBirth = new DateTime(1985, 8, 22)
                },
                new Client
                {
                    FirstName = "Pierre",
                    LastName = "Dubois",
                    DateOfBirth = new DateTime(1975, 3, 10)
                },
                new Client
                {
                    FirstName = "Sophie",
                    LastName = "Bernard",
                    DateOfBirth = new DateTime(1990, 11, 7)
                },
                new Client
                {
                    FirstName = "Thomas",
                    LastName = "Lambert",
                    DateOfBirth = new DateTime(1982, 9, 30)
                },
                new Client
                {
                    FirstName = "Claire",
                    LastName = "Petit",
                    DateOfBirth = new DateTime(1988, 6, 18)
                },
                new Client
                {
                    FirstName = "Nicolas",
                    LastName = "Leroy",
                    DateOfBirth = new DateTime(1978, 1, 25)
                },
                new Client
                {
                    FirstName = "Isabelle",
                    LastName = "Moreau",
                    DateOfBirth = new DateTime(1992, 4, 12)
                },
                new Client
                {
                    FirstName = "Michel",
                    LastName = "Roux",
                    DateOfBirth = new DateTime(1970, 7, 8)
                },
                new Client
                {
                    FirstName = "Céline",
                    LastName = "Simon",
                    DateOfBirth = new DateTime(1986, 12, 3)
                }
            };
            
            // Création d'adresse
            var adresses = new List<Address>
            {
                new Address
                {
                    Street = "123 Rue Principale",
                    ZipCode = "75001",
                    City = "Paris",
                    Country = "France",
                    ClientId = 1
                },
                new Address
                {
                    Street = "456 Avenue des Champs-Élysées",
                    ZipCode = "75008",
                    City = "Paris",
                    Country = "France",
                    ClientId = 1
                },
                new Address
                {
                    Street = "789 Boulevard Haussmann",
                    ZipCode = "75009",
                    City = "Paris",
                    Country = "France",
                    ClientId = 2
                },
                new Address
                {
                    Street = "101 Rue de Rivoli",
                    ZipCode = "75001",
                    City = "Paris",
                    Country = "France",
                    ClientId = 3
                },
                new Address
                {
                    Street = "202 Avenue Montaigne",
                    ZipCode = "75008",
                    City = "Paris",
                    Country = "France",
                    ClientId = 4
                },
                new Address
                {
                    Street = "5 Piccadilly Circus",
                    ZipCode = "W1B 4HJ",
                    City = "London",
                    Country = "United Kingdom",
                    ClientId = 5
                },
                new Address
                {
                    Street = "350 Fifth Avenue",
                    ZipCode = "10118",
                    City = "New York",
                    Country = "USA",
                    ClientId = 5
                },
                new Address
                {
                    Street = "40 Rue du Colisée",
                    ZipCode = "75008",
                    City = "Paris",
                    Country = "France",
                    ClientId = 6
                },
                new Address
                {
                    Street = "25 Quai Voltaire",
                    ZipCode = "75007",
                    City = "Paris",
                    Country = "France",
                    ClientId = 5
                },
                new Address
                {
                    Street = "1 Place de la Concorde",
                    ZipCode = "75008",
                    City = "Paris",
                    Country = "France",
                    ClientId = 5
                }
            };
            
            // Ajout des données dans la database
            context.Clients.AddRange(clients);
            await context.SaveChangesAsync();
            
            context.Addresses.AddRange(adresses);
            await context.SaveChangesAsync();
            
            context.Products.AddRange(products);
            await context.SaveChangesAsync();
            
            context.Orders.AddRange(orders);
            await context.SaveChangesAsync();
            
            context.OrderProducts.AddRange(commandesproduit);
            await context.SaveChangesAsync();
            
            return Ok("données chargées avec succés");
        }
    }
}