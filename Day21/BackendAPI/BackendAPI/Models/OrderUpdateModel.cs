namespace BackendAPI.Models
{
   
        public class OrderUpdateModel
        {
            public int OrderId { get; set; }
            public int Qty { get; set; }
            public string? Note { get; set; }
            public string? BagOption { get; set; }
            public string? Type { get; set; }
        }

    }

