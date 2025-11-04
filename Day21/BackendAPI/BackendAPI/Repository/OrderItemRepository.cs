using Microsoft.Data.SqlClient;
using BackendAPI.Models;

namespace BackendAPI.Repository
{
    public class OrderItemRepository
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public OrderItemRepository(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }

        // GET ALL ITEMS
        public List<OrderItem> GetAll()
        {
            List<OrderItem> items = new();

            using SqlConnection con = new SqlConnection(_connectionString);
            con.Open();

            string query = "SELECT * FROM OrderItems";

            using SqlCommand cmd = new SqlCommand(query, con);
            using SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                items.Add(new OrderItem
                {
                    OrderItemId = (int)rdr["OrderItemId"],
                    OrderId = (int)rdr["OrderId"],
                    ProductId = (int)rdr["ProductId"],
                    Qty = (int)rdr["Qty"],
                    Price = (decimal)rdr["Price"],
                    TotalPrice = (decimal)rdr["TotalPrice"]
                });
            }

            return items;
        }

        // GET BY ORDERID
        public List<OrderItem> GetByOrderId(int orderId)
        {
            List<OrderItem> items = new();

            using SqlConnection con = new SqlConnection(_connectionString);
            con.Open();

            string query = @"SELECT * FROM OrderItems WHERE OrderId = @OrderId";

            using SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@OrderId", orderId);

            using SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                items.Add(new OrderItem
                {
                    OrderItemId = (int)rdr["OrderItemId"],
                    OrderId = (int)rdr["OrderId"],
                    ProductId = (int)rdr["ProductId"],
                    Qty = (int)rdr["Qty"],
                    Price = (decimal)rdr["Price"],
                    TotalPrice = (decimal)rdr["TotalPrice"]
                });
            }

            return items;
        }

        // ADD ITEM 
        public void Add(OrderItem i)
        {
            try
            {
                using SqlConnection con = new SqlConnection(_connectionString);
                con.Open();

                string query = @"
            INSERT INTO OrderItems (OrderId, ProductId, Qty, Price)
            VALUES (@OrderId, @ProductId, @Qty, @Price)";

                using SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@OrderId", i.OrderId);
                cmd.Parameters.AddWithValue("@ProductId", i.ProductId);
                cmd.Parameters.AddWithValue("@Qty", i.Qty);
                cmd.Parameters.AddWithValue("@Price", i.Price);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Insert Failed: " + ex.Message);
            }
        }

        // UPDATE ITEM
        public void Update(OrderItem i)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            con.Open();

            string query = @"
                UPDATE OrderItems 
                SET ProductId = @ProductId,
                    Qty = @Qty, 
                    Price = @Price
                WHERE OrderItemId = @OrderItemId";

            using SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@OrderItemId", i.OrderItemId);
            cmd.Parameters.AddWithValue("@ProductId", i.ProductId);
            cmd.Parameters.AddWithValue("@Qty", i.Qty);
            cmd.Parameters.AddWithValue("@Price", i.Price);

            cmd.ExecuteNonQuery();
        }

        // DELETE ITEM
        public void Delete(int orderItemId)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            con.Open();

            string query = "DELETE FROM OrderItems WHERE OrderItemId = @OrderItemId";

            using SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@OrderItemId", orderItemId);

            cmd.ExecuteNonQuery();
        }
    }
}

