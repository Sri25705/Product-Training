using Microsoft.Data.SqlClient;
using BackendAPI.Models;

namespace BackendAPI.Repository
{
    public class OrderRepository
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public OrderRepository(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }

        // GET ALL ORDERS
        public List<Order> GetOrders()
        {
            var list = new List<Order>();
            using SqlConnection con = new SqlConnection(_connectionString);
            con.Open();

            string query = "SELECT * FROM Orders";
            using SqlCommand cmd = new SqlCommand(query, con);
            using SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                list.Add(new Order
                {
                    OrderId = (int)rdr["OrderId"],
                    Id = (int)rdr["Id"],
                    OrderCreated = (DateTime)rdr["OrderCreated"],
                    Status = rdr["Status"].ToString() ?? "",
                    Subtotal = (decimal)rdr["Subtotal"],
                    DeliveryFee = (decimal)rdr["DeliveryFee"],
                    TotalAmount = (decimal)rdr["TotalAmount"],
                    Note = rdr["Note"]?.ToString(),
                    BagOption = rdr["BagOption"]?.ToString(),
                    Type = rdr["Type"]?.ToString()
                });
            }
            return list;
        }

        // GET ORDER BY ID
        public Order? GetOrderById(int id)
        {
            Order? order = null;

            using SqlConnection con = new SqlConnection(_connectionString);
            con.Open();

            string query = "SELECT * FROM Orders WHERE OrderId = @id";
            using SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);

            using SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                order = new Order
                {
                    OrderId = (int)rdr["OrderId"],
                    Id = (int)rdr["Id"],
                    OrderCreated = (DateTime)rdr["OrderCreated"],
                    Status = rdr["Status"].ToString() ?? "",
                    Subtotal = (decimal)rdr["Subtotal"],
                    DeliveryFee = (decimal)rdr["DeliveryFee"],
                    TotalAmount = (decimal)rdr["TotalAmount"],
                    Note = rdr["Note"]?.ToString(),
                    BagOption = rdr["BagOption"]?.ToString(),
                    Type = rdr["Type"]?.ToString()
                };
            }
            return order;
        }

        // ADD ORDER
        public int AddOrder(Order order)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            con.Open();

            string status = order.Type?.ToLower() == "upi" ? "Paid" : "Pending";

            string query = @"
        INSERT INTO Orders 
        (Id, OrderCreated, Status, Subtotal, DeliveryFee, TotalAmount, Note, BagOption, Type)
        OUTPUT INSERTED.OrderId
        VALUES 
        (@Id, @OrderCreated, @Status, @Subtotal, @DeliveryFee, @TotalAmount, @Note, @BagOption, @Type)
    ";

            using SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@Id", order.Id);
            cmd.Parameters.AddWithValue("@OrderCreated", DateTime.Now);
            cmd.Parameters.AddWithValue("@Status", status);
            cmd.Parameters.AddWithValue("@Subtotal", order.Subtotal);
            cmd.Parameters.AddWithValue("@DeliveryFee", order.DeliveryFee);
            cmd.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);
            cmd.Parameters.AddWithValue("@Note", order.Note ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@BagOption", order.BagOption ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Type", order.Type ?? (object)DBNull.Value);

            var result = cmd.ExecuteScalar();
            return result != null ? Convert.ToInt32(result) : 0;
        }


        public bool AddOrderItem(OrderItem item)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            con.Open();

            string query = @"
        INSERT INTO OrderItems (OrderId, ProductId, Qty, Price)
        VALUES (@OrderId, @ProductId, @Qty, @Price)";

            using SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@OrderId", item.OrderId);
            cmd.Parameters.AddWithValue("@ProductId", item.ProductId);
            cmd.Parameters.AddWithValue("@Qty", item.Qty);
            cmd.Parameters.AddWithValue("@Price", item.Price);

            return cmd.ExecuteNonQuery() > 0;
        }

        public int GetLatestOrderId(int userId)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            con.Open();

            string query = @"SELECT TOP 1 OrderId 
                     FROM Orders 
                     WHERE Id = @Id 
                     ORDER BY OrderCreated DESC";

            using SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", userId);

            var result = cmd.ExecuteScalar();

            return result != null ? Convert.ToInt32(result) : 0;
        }


        // UPDATE ORDER
        public bool UpdateOrder(Order order)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            con.Open();

            string query = @"
                UPDATE Orders
                SET 
                    Id = @Id,
                    OrderCreated = @OrderCreated,
                    Status = @Status,
                    Subtotal = @Subtotal,
                    DeliveryFee = @DeliveryFee,
                    TotalAmount = @TotalAmount,
                    Note = @Note,
                    BagOption = @BagOption,
                    Type = @Type
                WHERE OrderId = @OrderId
            ";

            using SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@OrderId", order.OrderId);
            cmd.Parameters.AddWithValue("@Id", order.Id);
            cmd.Parameters.AddWithValue("@OrderCreated", order.OrderCreated);
            cmd.Parameters.AddWithValue("@Status", order.Status);
            cmd.Parameters.AddWithValue("@Subtotal", order.Subtotal);
            cmd.Parameters.AddWithValue("@DeliveryFee", order.DeliveryFee);
            cmd.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);
            cmd.Parameters.AddWithValue("@Note", order.Note ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@BagOption", order.BagOption ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Type", order.Type ?? (object)DBNull.Value);

            return cmd.ExecuteNonQuery() > 0;
        }

        // DELETE ORDER
        public bool DeleteOrder(int orderId)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            con.Open();

            // Delete order items first (FK)
            string deleteItems = @"DELETE FROM OrderItems WHERE OrderId = @OrderId";
            using SqlCommand cmd1 = new SqlCommand(deleteItems, con);
            cmd1.Parameters.AddWithValue("@OrderId", orderId);
            cmd1.ExecuteNonQuery();

            //  Delete order
            string deleteOrder = @"DELETE FROM Orders WHERE OrderId = @OrderId";
            using SqlCommand cmd2 = new SqlCommand(deleteOrder, con);
            cmd2.Parameters.AddWithValue("@OrderId", orderId);

            int rows = cmd2.ExecuteNonQuery();
            return rows > 0;
        }


        public List<object> GetAllItems(int userId)
        {
            List<object> items = new();

            using SqlConnection con = new SqlConnection(_connectionString);
            con.Open();

            string query = @"
        SELECT o.OrderId, p.ProductName, oi.Qty, oi.Price
        FROM Orders o
        INNER JOIN OrderItems oi ON o.OrderId = oi.OrderId
        INNER JOIN Products p ON oi.ProductId = p.ProductId
        WHERE o.Id = @uid
        ORDER BY o.OrderCreated DESC";

            using SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@uid", userId);

            using SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                items.Add(new
                {
                    OrderId = (int)rdr["OrderId"],
                    ProductName = rdr["ProductName"].ToString(),
                    Qty = (int)rdr["Qty"],
                    Price = (decimal)rdr["Price"]
                });
            }

            return items;
        }


        // GET FULL ORDER DETAILS BY USER
        public object? GetOrderDetailsByUser(int userId)
        {
            Order? order = null;

            using SqlConnection con = new SqlConnection(_connectionString);
            con.Open();

            // ORDER
            string orderQuery = @"SELECT TOP 1 * FROM Orders WHERE Id = @id ORDER BY OrderCreated DESC";
            using (SqlCommand cmd = new SqlCommand(orderQuery, con))
            {
                cmd.Parameters.AddWithValue("@id", userId);

                using SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    order = new Order
                    {
                        OrderId = (int)rdr["OrderId"],
                        Id = (int)rdr["Id"],
                        OrderCreated = (DateTime)rdr["OrderCreated"],
                        Status = rdr["Status"].ToString() ?? "",
                        Subtotal = (decimal)rdr["Subtotal"],
                        DeliveryFee = (decimal)rdr["DeliveryFee"],
                        TotalAmount = (decimal)rdr["TotalAmount"],
                        Note = rdr["Note"]?.ToString(),
                        BagOption = rdr["BagOption"]?.ToString(),
                        Type = rdr["Type"]?.ToString()
                    };
                }
            }


            if (order == null) return null;

            // USER
            User? user = null;
            string userQuery = "SELECT * FROM Users WHERE Id = @uid";
            using (SqlCommand cmd = new SqlCommand(userQuery, con))
            {
                cmd.Parameters.AddWithValue("@uid", userId);

                using SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    user = new User
                    {
                        Id = (int)rdr["Id"],
                        Name = rdr["Name"].ToString(),
                        Phoneno = rdr["Phoneno"].ToString(),
                        Password = rdr["Password"].ToString(),
                        AddressLine = rdr["AddressLine"].ToString(),
                        BuildingName = rdr["BuildingName"].ToString(),
                        Street = rdr["Street"].ToString(),
                        PostalCode = rdr["PostalCode"].ToString()
                    };
                }
            }

            // ORDER ITEMS + PRODUCT JOIN
            List<object> items = new();

            string itemQuery = @"
    SELECT oi.OrderItemId, oi.ProductId, oi.Qty, oi.TotalPrice,
           p.ProductName, p.Price
    FROM OrderItems oi
    INNER JOIN Products p ON oi.ProductId = p.ProductId
    WHERE oi.OrderId = @oid";

            using (SqlCommand cmd = new SqlCommand(itemQuery, con))
            {
                cmd.Parameters.AddWithValue("@oid", order.OrderId);

                using SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    items.Add(new
                    {
                        OrderItemId = (int)rdr["OrderItemId"],
                        ProductId = (int)rdr["ProductId"],
                        ProductName = rdr["ProductName"].ToString(),
                        Qty = (int)rdr["Qty"],
                        Price = (decimal)rdr["Price"],
                        TotalPrice = (decimal)rdr["TotalPrice"]
                    });
                }
            }

            return new
            {
                Order = order,
                Customer = user,
                Items = items
            };

        }
    }
}
