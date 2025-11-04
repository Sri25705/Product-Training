using Microsoft.Data.SqlClient;
using BackendAPI.Models;

namespace BackendAPI.Repository
{
    public class ProductRepository
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public ProductRepository(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }

        // GET ALL PRODUCTS
        public List<Product> GetAllProducts()
        {
            var list = new List<Product>();
            using SqlConnection con = new SqlConnection(_connectionString);
            con.Open();

            string query = "SELECT * FROM Products";
            using SqlCommand cmd = new SqlCommand(query, con);
            using SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                list.Add(new Product
                {
                    ProductId = (int)rdr["ProductId"],
                    ProductName = rdr["ProductName"].ToString(),
                    Price = (decimal)rdr["Price"]
                });
            }

            return list;
        }

        // GET BY ID
        public Product? GetProductById(int id)
        {
            Product? product = null;

            using SqlConnection con = new SqlConnection(_connectionString);
            con.Open();

            string query = "SELECT * FROM Products WHERE ProductId = @id";
            using SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);

            using SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                product = new Product
                {
                    ProductId = (int)rdr["ProductId"],
                    ProductName = rdr["ProductName"].ToString(),
                    Price = (decimal)rdr["Price"]
                };
            }

            return product;
        }

        // ADD PRODUCT
        public bool AddProduct(Product product)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            con.Open();

            string query = "INSERT INTO Products (ProductName, Price) VALUES (@ProductName, @Price)";
            using SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
            cmd.Parameters.AddWithValue("@Price", product.Price);

            return cmd.ExecuteNonQuery() > 0;
        }

        // UPDATE PRODUCT
        public bool UpdateProduct(Product product)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            con.Open();

            string query = @"
                UPDATE Products 
                SET ProductName = @ProductName, Price = @Price 
                WHERE ProductId = @ProductId";

            using SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ProductId", product.ProductId);
            cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
            cmd.Parameters.AddWithValue("@Price", product.Price);

            return cmd.ExecuteNonQuery() > 0;
        }

        // DELETE PRODUCT
        public bool DeleteProduct(int id)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            con.Open();

            string query = "DELETE FROM Products WHERE ProductId = @id";
            using SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);

            return cmd.ExecuteNonQuery() > 0;
        }
    }
}
