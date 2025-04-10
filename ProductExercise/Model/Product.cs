using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProductExercise;

[Table(nameof(Product))]
public class Product
{
    [Key]
    public int ProductId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    public string Description { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int UnitInStock { get; set; }

    public double Weight { get; set; }
    
    [JsonIgnore]
    public ICollection<OrderProduct> OrderProducts { get; set; }
}