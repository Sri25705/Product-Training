namespace BackendAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phoneno { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? AddressLine { get; set; }
        public string? BuildingName { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set; }
    }
}



