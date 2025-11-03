namespace BackendAPI.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int Id { get; set; }
        public DateTime OrderCreated { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal Subtotal { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Note { get; set; }
        public string? BagOption { get; set; }
        public string? Type { get; set; }
    }
}

