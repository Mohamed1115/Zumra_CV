namespace Zumra.DTOs.Response;

public class CartResponse
{
    public decimal Total { get; set; }
    public List<CartItemDto> Cart { get; set; } = new();
}