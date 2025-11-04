using Microsoft.Data.SqlClient;
using BackendAPI.Models;

namespace BackendAPI.Repository
{
    public class UserRepository
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public UserRepository(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }

        // Check if username exists
        public bool UserExists(string name)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            con.Open();

            string query = "SELECT COUNT(*) FROM Users WHERE Name = @Name";

            using SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Name", name);

            int count = Convert.ToInt32(cmd.ExecuteScalar());
            return count > 0;
        }

        // Modified → return bool
        public bool Add(User user)
        {
            if (UserExists(user.Name))
                return false;

            using SqlConnection con = new SqlConnection(_connectionString);
            con.Open();

            string query = @"INSERT INTO Users 
            (Name, Phoneno, Password, AddressLine, BuildingName, Street, PostalCode)
            VALUES 
            (@Name, @Phoneno, @Password, @AddressLine, @BuildingName, @Street, @PostalCode)";

            using SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@Name", user.Name);
            cmd.Parameters.AddWithValue("@Phoneno", user.Phoneno);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@AddressLine", user.AddressLine ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@BuildingName", user.BuildingName ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Street", user.Street ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@PostalCode", user.PostalCode ?? (object)DBNull.Value);

            return cmd.ExecuteNonQuery() > 0;
        }

        public List<User> GetAll()
        {
            List<User> users = new();

            using SqlConnection con = new SqlConnection(_connectionString);
            con.Open();

            string query = "SELECT * FROM Users";

            using SqlCommand cmd = new SqlCommand(query, con);
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                users.Add(new User
                {
                    Id = (int)reader["Id"],
                    Name = reader["Name"].ToString(),
                    Phoneno = reader["Phoneno"].ToString(),
                    Password = reader["Password"].ToString(),
                    AddressLine = reader["AddressLine"].ToString(),
                    BuildingName = reader["BuildingName"].ToString(),
                    Street = reader["Street"].ToString(),
                    PostalCode = reader["PostalCode"].ToString()
                });
            }
            return users;
        }

        public User? GetById(int id)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            con.Open();

            string query = "SELECT * FROM Users WHERE Id = @Id";

            using SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", id);

            using SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new User
                {
                    Id = (int)reader["Id"],
                    Name = reader["Name"].ToString(),
                    Phoneno = reader["Phoneno"].ToString(),
                    Password = reader["Password"].ToString(),
                    AddressLine = reader["AddressLine"].ToString(),
                    BuildingName = reader["BuildingName"].ToString(),
                    Street = reader["Street"].ToString(),
                    PostalCode = reader["PostalCode"].ToString()
                };
            }
            return null;
        }

        public void Update(User user)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            con.Open();

            string query = @"
                UPDATE Users 
                SET Name = @Name,
                    Phoneno = @Phoneno,
                    Password = @Password,
                    AddressLine = @AddressLine,
                    BuildingName = @BuildingName,
                    Street = @Street,
                    PostalCode = @PostalCode
                WHERE Id = @Id";

            using SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@Id", user.Id);
            cmd.Parameters.AddWithValue("@Name", user.Name);
            cmd.Parameters.AddWithValue("@Phoneno", user.Phoneno);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@AddressLine", user.AddressLine ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@BuildingName", user.BuildingName ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Street", user.Street ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@PostalCode", user.PostalCode ?? (object)DBNull.Value);

            cmd.ExecuteNonQuery();
        }

        public User? Login(string name, string password)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            con.Open();

            string query = @"SELECT * FROM Users 
                     WHERE Name = @Name AND Password = @Password";

            using SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Password", password);

            using SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new User
                {
                    Id = (int)reader["Id"],
                    Name = reader["Name"].ToString(),
                    Phoneno = reader["Phoneno"].ToString(),
                    Password = reader["Password"].ToString(),
                    AddressLine = reader["AddressLine"].ToString(),
                    BuildingName = reader["BuildingName"].ToString(),
                    Street = reader["Street"].ToString(),
                    PostalCode = reader["PostalCode"].ToString()
                };
            }
            return null;
        }

        public bool Delete(int id)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            con.Open();

            string query = @"DELETE FROM Users WHERE Id = @Id";

            using SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", id);

            return cmd.ExecuteNonQuery() > 0;
        }
    }
}
