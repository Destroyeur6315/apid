using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProductExercise;

public class OrderProduct
{
    [JsonIgnore]
    [Key]
    public int OrderProductId { get; set; }
    
    [JsonIgnore]
    [Required]
    public int OrderId { get; set; }
    
    [JsonIgnore]
    [ForeignKey(nameof(OrderId))]
    public Order? Order { get; set; }

    [Required]
    public int ProductId { get; set; }

    [JsonIgnore]
    [ForeignKey(nameof(ProductId))]
    public Product? Product { get; set; }

    [Required]
    public int Quantity { get; set; }
}