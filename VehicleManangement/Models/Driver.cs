namespace VehicleManangement.Models
{
    public class Driver
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? LicenceNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
        public int? AddressId { get; set; }
        public Address? Address { get; set; }
        public int? ClientId { get; set; }
        public Client? Client { get; set; }
    }
}
