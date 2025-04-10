using ProductExercise;

namespace WebApi.Dto;

public class OrderDto
{
    public DateTime Date { get; set; }
    
    public int ClientId { get; set; }
    
    public ICollection<OrderProduct> OrderProducts { get; set; }
}