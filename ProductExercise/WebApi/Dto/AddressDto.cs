namespace WebApi.Dto;

public class AddressDto
{
    public string? Street { get; set; }
    
    public string? ZipCode { get; set; }
    
    public string? City { get; set; }
    
    public string? Country { get; set; }
    
    public int ClientId { get; set; }
}