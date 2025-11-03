namespace BackendAPI.Models
{
    public class OrderDetailsDto
    {
        public Order Order { get; set; }
        public User User { get; set; }
        public List<ProductItemDto> Products { get; set; } = new();
    }

    public class ProductItemDto
    {
        public string ProductName { get; set; } = "";
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
